using Amritnagar.Models.Database;
using Amritnagar.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Amritnagar.Controllers
{
    public class DividendController : Controller
    {
        //******************************** Dividend Calc And Posting ********************************
        [HttpGet]
        public ActionResult DividendCalcAndPosting(DividendCalcAndPostViewModel model)
        {
            UtilityController u = new UtilityController();
            model.BranchDesc = u.getBranchMastDetails();
            model.EmpBranchDesc = u.getEmployerBranchMastDetails();

            model.fr_dt = "01/04/" + Convert.ToString(DateTime.Now.Year - 1);
            model.to_dt = "31/03/" + Convert.ToString(DateTime.Now.Year);
            model.post_dt = model.to_dt;
            return View(model);
        }
        public JsonResult getLIstForDividendpost(DividendCalcAndPostViewModel model)
        {
            Member_Mast mm = new Member_Mast();
            initGrid(model);
            PopulateGrid(model);
            return Json(model);
        }
        public void initGrid(DividendCalcAndPostViewModel model)
        {
            int xmonths, cols;
            String xf_str, xstr;
            DateTime xl_dt;
            DateTime xfrm_dt = Convert.ToDateTime(model.fr_dt);

            xmonths = Convert.ToInt32(Convert.ToDateTime(model.to_dt).Subtract(Convert.ToDateTime(model.fr_dt)).Days / (365.25 / 12));
            var result = Convert.ToDateTime(model.fr_dt).CompareTo(Convert.ToDateTime(model.to_dt));
            model.tableelement = "<tr><th>Srl.</th><th>Membership No</th><th>Membership Date</th><th>Name</th><th>Op/Share Capital</th>";
            for (int i = 1; i <= xmonths; i++)
            {
                model.int_array[1, i] = xfrm_dt;
                xstr = xfrm_dt.ToString("MMM, yyyy");
                model.month_array[i - 1] = xstr;
                model.tableelement = model.tableelement + "<th>" + xstr + "</th>";
                xfrm_dt = xfrm_dt.AddMonths(1);
            }
            model.tableelement = model.tableelement + " <th>Cl/Share Capital</th><th>Cl/Dividend Payb.</th></tr>";
            model.xmonths = xmonths;
        }
        public void PopulateGrid(DividendCalcAndPostViewModel model)
        {
            // utility utl = new utility();
            decimal open_bal = 0;
            Double xtot_dividend = 0;

            decimal cls_bal = 0;

            decimal xtot_opn_bal = 0;
            decimal xtot_cls_bal = 0;

            double xtot_dividend_pay = 0;
            int i = 1;

            Member_Mast mm = new Member_Mast();
            List<Member_Mast> mmlst = new List<Member_Mast>();
            mmlst = mm.GetmemMastForDvidendCalculation(model);
            if (mmlst.Count > 0)
            {
                foreach (var a in mmlst)
                {
                    string xac = a.mem_id;
                    string xacnm = a.mem_name;

                    SHARE_LEDGER sl = new SHARE_LEDGER();
                    List<SHARE_LEDGER> sllst = new List<SHARE_LEDGER>();
                    sllst = sl.getShareLedherDetail(model.branch, xac);

                    if (sllst.Count > 0)
                    {
                        xtot_dividend = CAL_DIVIDEND(Convert.ToDateTime(model.fr_dt), Convert.ToDateTime(model.to_dt), sllst, model.xmonths, Convert.ToDecimal(model.inst), Convert.ToDecimal(0), model);

                        //   '----Fetch Opening Balance----
                        var result = sllst.FindLast(delegate (SHARE_LEDGER sbl)
                        {
                            return sbl.vch_date < Convert.ToDateTime(model.fr_dt);
                        });
                        if (result == null)
                        {
                            open_bal = 0;
                        }
                        else
                        {

                            open_bal = Convert.ToDecimal(result.bal_amount);
                        }
                        xtot_opn_bal = xtot_opn_bal + open_bal; //'--Total Opening Share Capital



                        var result2 = sllst.FindLast(delegate (SHARE_LEDGER sbl)
                        {
                            return sbl.vch_date <= Convert.ToDateTime(model.to_dt);
                        });
                        if (result2 == null)
                        {
                            cls_bal = 0;
                        }
                        else
                        {

                            cls_bal = Convert.ToDecimal(result2.bal_amount);


                        }
                        xtot_cls_bal = xtot_cls_bal + cls_bal; //'--Total Closing Share Capital

                    }

                    model.tableelement = model.tableelement + "<tr><td>" + i + "</td><td>" + xac + "</td><td>" + a.mem_date + "</td><td>" + xacnm + "</td><td>" + open_bal + "</td>";
                    for (int c = 1; c <= model.xmonths; c++)
                    {
                        model.tableelement = model.tableelement + "<td>" + model.int_array[2, c] + "</td>";
                    }
                    model.tableelement = model.tableelement + "<td>" + cls_bal + "</td><td>" + xtot_dividend + "</td></tr>";
                    xtot_dividend_pay = xtot_dividend_pay + xtot_dividend; //'--Total Dividend Payable
                    model.opsh_cap = xtot_opn_bal.ToString("0.00");
                    model.clsh_cap = xtot_cls_bal.ToString("0.00");
                    model.div_pay = xtot_dividend_pay.ToString("0.00");


                    if (model.forsave == "Savedata")
                    {
                        DIVIDEND_LEDGER dl = new DIVIDEND_LEDGER();
                        dl.SaveInDividentLedger(a, model, cls_bal, xtot_dividend, i);
                    }
                    i++;
                }
            }
        }
        public double CAL_DIVIDEND(DateTime xfrdt, DateTime XTODT, List<SHARE_LEDGER> sllst, int xformonths, decimal XINT_RATE, decimal XMAX_MINBAL, DividendCalcAndPostViewModel model)
        {
            double open_bal = 0; double clos_bal = 0; double xtot = 0;
            double xr_bal = 0; double xbal = 0; int xyear = 0; int xmonth = 0;
            int xm = 0;  //decimal K=0;
            //int mm = 0;
            int tt = 0;

            for (int K = 1; K <= 12; K++)
            {
                model.int_array[2, K] = 0;
            }
            double CAL_DIVIDEND = 0;
            open_bal = 0;
            clos_bal = 0;
            xtot = 0;
            xr_bal = 0;
            xbal = 0;
            xm = 1;
            if (sllst.Count == 0)
            {
                return xbal;
            }
            else
            {
                var result = sllst.FindLast(delegate (SHARE_LEDGER sbl)
                {
                    return sbl.vch_date < xfrdt;
                });
                if (result == null)
                {
                    open_bal = 0;
                }
                else
                {
                    open_bal = Convert.ToDouble(result.bal_amount);
                }
                //'----Fetch Minimum Monthly Balance----

                xr_bal = open_bal;
                int XMONTH = Convert.ToDateTime(model.int_array[1, xm]).Month; // int.Parse(int_array[1, xm].ToString("MM"));
                int XYEAR = Convert.ToDateTime(model.int_array[1, xm]).Year; //int.Parse(int_array[1, xm].ToString("yyyy"));
                model.int_array[2, xm] = Convert.ToInt32(xr_bal);
                foreach (SHARE_LEDGER sl in sllst)
                {
                    if (sl.vch_date > XTODT)
                        break;
                    if (sl.vch_date < xfrdt)
                    {
                        //do nothing
                    }
                    else
                    {
                        if (sl.vch_date.Month > XMONTH || sl.vch_date.Year > XYEAR)
                        {
                            if (sl.vch_date.Month == XMONTH && sl.vch_date.Year == XYEAR)
                            {

                            }
                            else
                            {
                                xm = xm + 1;
                                XMONTH = Convert.ToDateTime(model.int_array[1, xm]).Month;  //int.Parse(int_array[1, xm].ToString("MM"));
                                XYEAR = Convert.ToDateTime(model.int_array[1, xm]).Year; // int.Parse(int_array[1, xm].ToString("yyyy"));
                                model.int_array[2, xm] = Convert.ToInt32(xr_bal);
                            }
                        }
                        xbal = Convert.ToDouble(sl.bal_amount);
                        // String XDRCR = tf.dr_cr;
                        if (sl.vch_date.Day <= 10)
                            model.int_array[2, xm] = Convert.ToInt32(xbal);
                        else
                        {
                            if (xbal < model.int_array[2, xm])
                            {
                                model.int_array[2, xm] = Convert.ToInt32(xbal);
                            }
                        }
                        xr_bal = xbal;
                    }
                }
                if (xm < xformonths)
                {
                    for (int mm = xm + 1; mm <= xformonths; mm++)
                    {
                        model.int_array[2, mm] = xr_bal;
                    }
                }
            }
            if (model.int_array[2, xformonths] == 0)
            {
                xtot = 0;
            }
            else
            {
                for (int PP = 1; PP <= xformonths; PP++)
                {
                    xtot = xtot + model.int_array[2, PP];
                }
            }
            CAL_DIVIDEND = Convert.ToDouble(((xtot * Convert.ToDouble(XINT_RATE) / 1200) + 0.00000002));

            CAL_DIVIDEND = Math.Round(CAL_DIVIDEND, 0);
            return CAL_DIVIDEND;
        }
        public JsonResult getDividendLedgerBymember_id(DividendCalcAndPostViewModel model)
        {
            DIVIDEND_LEDGER dl = new DIVIDEND_LEDGER();
           
            List<DIVIDEND_LEDGER>dllst = new List<DIVIDEND_LEDGER>();
            dllst = dl.getdetails(model.member_id,model.branch);
            int i = 1;
            if (dllst.Count > 0)
            {
                foreach (var a in dllst)
                {
                    if (i == 1)
                    {
                        
                            model.tableelement1 = "<tr><th>Srl</th></th><th>Vch Date</th><th>vchno</th><th>Div YEar</th><th>Tr amt</th><th>Bal amt</th></tr>";
                            model.tableelement1 = model.tableelement1 + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.VCH_DATE + "</td><td>" + a.VCH_NO + "</td><td>" + a.DIV_POST_YEAR_TO + "</td><td>" + a.TR_AMOUNT + "</td><td>" + a.BAL_AMOUNT + "</td></tr>";
                      
                       
                    }
                    else
                    {

                        model.tableelement1 = model.tableelement1 + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.VCH_DATE + "</td><td>" + a.VCH_NO + "</td><td>" + a.DIV_POST_YEAR_TO + "</td><td>" + a.TR_AMOUNT + "</td><td>" + a.BAL_AMOUNT + "</td></tr>";

                    }
                    i = i + 1;
                }
            }
            else
            {
                model.tableelement1 = null;
            }
            return Json(model);
        }
        //******************************** Dividend Calc And Posting End ********************************

        [HttpGet]
        public ActionResult UnpaidDividendDetailList(UnpaidDividendDetailListViewModel model)
        {
            UtilityController u = new UtilityController();
            model.BranchDesc = u.getBranchMastDetails();

            return View(model);
        }
        [HttpGet]
        public ActionResult DividendWritOfSch(DividendWritOfSchViewModel model)
        {
            UtilityController u = new UtilityController();
            model.BranchDesc = u.getBranchMastDetails();

            return View(model);
        }
    }
}