using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Amritnagar.Models.Database;
using Amritnagar.Models.ViewModel;
using System.IO;

namespace Amritnagar.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        [HttpGet]
        public ActionResult OnLineCashReceive(OnLineCashReceiveViewModel model)
        {
            UtilityController u = new UtilityController();

            model.BranchDesc = u.getBranchMastDetails();
            model.TypeDesc = u.getTypeMastDetails();

            return View(model);
        }
        [HttpGet]
        public ActionResult OnLineCashPayment(OnLineCashPaymentViewModel model)
        {
            UtilityController u = new UtilityController();

            model.BranchDesc = u.getBranchMastDetails();
            model.TypeDesc = u.getTypeMastDetails();

            return View(model);
        }
        [HttpGet]
        public ActionResult OnLineCashRecLoan(OnLineCashRecLoanViewModel model)
        {
            UtilityController u = new UtilityController();

            model.BranchDesc = u.getBranchMastDetails();
            model.TypeDesc = u.getTypeMastDetails();

            return View(model);
        }
        [HttpGet]
        public ActionResult LoanTransfCreditEntry(LoanTransfCreditEntryViewModel model)
        {
            UtilityController u = new UtilityController();

            model.BranchDesc = u.getBranchMastDetails();
            model.TypeDesc = u.getTypeMastDetails();

            return View(model);
        }
        /********************************************Vouchar Entry Start*******************************************/
        [HttpGet]
        public ActionResult VoucherEntry(VoucherEntryViewModel model)
        {
            UtilityController u = new UtilityController();
            model.BranchDesc = u.getBranchMastDetails();
            model.vch_date = DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/");
            return View(model);
        }
        public JsonResult GetVoucherDetailsList(string branch, string vch_date, string vch_no)
        {
            Vch_Details vd = new Vch_Details();
            List<Vch_Details> vdlist = new List<Vch_Details>();
            VoucherEntryViewModel model = new VoucherEntryViewModel();
            model.debt_amt = "0";
            model.crdt_amt = "0";
            model.net_amt = "0";
            //model.vch_DrCr = "0";
            vdlist = vd.put_vch_detail(branch, vch_date, vch_no);
            string TableElement = string.Empty;
            int i = 1;
            if (vdlist.Count > 0)
            {
                foreach (var a in vdlist)
                {
                    if (i == 1)
                    {
                        model.tableElement = "<tr><th> Srl</th><th>Dr/Cr</th><th>Account Head</th><th>Account No</th><th>Paid To/Recd.From/Particulars</th><th>Amount</th><th>Ref.A/C Head</th><th>Ref.A/C No</th><th>Ref.A/C Particulars</th></tr>";
                        model.tableElement = model.tableElement + "<tr><td>" + i.ToString() + "</td><td>" + a.vch_drcr + "</td><td>" + a.ac_hd + "</td><td>" + a.vch_pacno + "</td><td>" + a.vch_acname + "</td><td>" + a.vch_amt.ToString("0.00") + "</td>";
                        model.tableElement = model.tableElement + "<td>" + a.ref_ac_hd + "</td><td>" + a.ref_pacno + "</td><td>" + a.ref_oth + "</td></tr>";
                    }
                    else
                    {
                        model.tableElement = model.tableElement + "<tr><td>" + i.ToString() + "</td><td>" + a.vch_drcr + "</td><td>" + a.ac_hd + "</td><td>" + a.vch_pacno + "</td><td>" + a.vch_acname + "</td><td>" + a.vch_amt.ToString("0.00") + "</td>";
                        model.tableElement = model.tableElement + "<td>" + a.ref_ac_hd + "</td><td>" + a.ref_pacno + "</td><td>" + a.ref_oth + "</td></tr>";
                    }
                    if (a.vch_drcr == "D")
                    {
                        model.debt_amt = (Convert.ToDecimal(model.debt_amt) + a.vch_amt).ToString("0.00");
                        model.net_amt = (Convert.ToDecimal(model.net_amt) - a.vch_amt).ToString("0.00");
                    }
                    if (a.vch_drcr == "C")
                    {
                        model.crdt_amt = (Convert.ToDecimal(model.crdt_amt) + a.vch_amt).ToString("0.00");
                        model.net_amt = (Convert.ToDecimal(model.net_amt) + a.vch_amt).ToString("0.00");
                    }
                    i = i + 1;
                }
            }
            return Json(model);
        }
        public JsonResult FillVoucherNo(string branch, string vch_date)
        {
            Vch_header vh = new Vch_header();
            var vchnos = vh.VchNo(branch, vch_date);
            return Json(vchnos, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Add_Vch_In_Temp_table(string vch_date, string drcr, string vch_achd, string vchpacno, string particular, string amount, string ref_ac_hd, string refacno, string refParticular, string vch_no)
        {
            Temp_Vch_Entry tve = new Temp_Vch_Entry();
            int serial = tve.GetLastSerialNoBydate(vch_date, vch_no);
            tve.srl = serial;
            tve.drcr = drcr;
            tve.ac_hd = vch_achd;
            tve.vch_pacno = vchpacno;
            tve.vch_no = vch_no;
            tve.paid_to_rcv_frm = particular;
            //tve.vch_dt = Convert.ToDateTime(vch_date);
            tve.str_vchdt = vch_date.Replace("-", "/");
            tve.amount = Convert.ToDecimal(amount);
            tve.ref_achd = ref_ac_hd;
            tve.ref_acno = refacno;
            tve.ref_ac_particulars = refParticular;
            tve.created_by = Convert.ToString(Session["Uid"]);
            tve.created_on = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/"));
            tve.computer_name = Environment.MachineName;
            tve.SaveTempVchData(tve);
            return Json("Added");
        }
        public JsonResult GetTempData(string vch_date, string vch_no)
        {
            Temp_Vch_Entry tve = new Temp_Vch_Entry();
            List<Temp_Vch_Entry> tvel = new List<Temp_Vch_Entry>();
            tvel = tve.GetTempVchDataByVchdate(vch_date, vch_no);
            string TableElement = string.Empty;
            int i = 1;
            foreach (var a in tvel)
            {
                if (i == 1)
                {
                    TableElement = "<tr><th> Srl</th><th> Dr/Cr </th><th>Account Head</th><th>Account No</th><th>Paid To/Recd.From/Particulars</th><th>Amount</th><th>Ref.A/C Head</th><th>Ref.A/C No</th><th>Ref.A/C Particulars</th></tr>";
                    TableElement = TableElement + "<tr  id=" + Convert.ToString(i) + "><td>" + a.srl + "</td><td>" + a.drcr + "</td><td>" + a.ac_hd + "</td><td>" + a.vch_pacno + "</td><td>" + a.paid_to_rcv_frm + "</td><td>" + a.amount + "</td>";
                    TableElement = TableElement + "<td>" + a.ref_achd + "</td><td>" + a.ref_acno + "</td><td>" + a.ref_ac_particulars + "</td></tr>";
                }
                else
                {
                    TableElement = TableElement + "<tr  id=" + Convert.ToString(i) + "><td>" + a.srl + "</td><td>" + a.drcr + "</td><td>" + a.ac_hd + "</td><td>" + a.vch_pacno + "</td><td>" + a.paid_to_rcv_frm + "</td><td>" + a.amount + "</td>";
                    TableElement = TableElement + "<td>" + a.ref_achd + "</td><td>" + a.ref_acno + "</td><td>" + a.ref_ac_particulars + "</td></tr>";
                }
                i = i + 1;
            }
            return Json(TableElement);
        }


        /********************************************Vouchar Entry End*******************************************/

        [HttpGet]
        public ActionResult CashAccountReport(CashAccountReportViewModel model)
        {
            UtilityController u = new UtilityController();

            model.BranchDesc = u.getBranchMastDetails();


            return View(model);
        }
        [HttpGet]
        public ActionResult DayBookReport(DayBookReportViewModel model)
        {
            UtilityController u = new UtilityController();

            model.BranchDesc = u.getBranchMastDetails();


            return View(model);
        }

        [HttpGet]
        public ActionResult CashBookReport(CashBookReportViewModel model)
        {
            UtilityController u = new UtilityController();

            model.BranchDesc = u.getBranchMastDetails();


            return View(model);
        }
        [HttpGet]
        public ActionResult CashBankPositionReport(CashBankPositionReportViewModel model)
        {
            UtilityController u = new UtilityController();

            model.BranchDesc = u.getBranchMastDetails();


            return View(model);
        }
        [HttpGet]
        public ActionResult TrialBalanceReport(TrialBalanceReportViewModel model)
        {
            UtilityController u = new UtilityController();

            model.BranchDesc = u.getBranchMastDetails();


            return View(model);
        }
        public JsonResult getdataforTrialBlance(TrialBalanceReportViewModel model)
        {
            List<GL_BALNCE> glblst = new List<GL_BALNCE>();
            GL_BALNCE gl = new GL_BALNCE();

            glblst = gl.gettrialbalancelist(model);
            if (glblst.Count > 0)
            {
                model.tableele = "<tr><th> Major Group</th><th> Account Head </th><th>Debit Balance</th><th>Credit Balance</th><th>Group Total</th></tr>";

                foreach (var a in glblst)
                {
                    model.tableele = model.tableele + "<tr><td>" + a.majorgroup + "</td><td>" + a.acchd + "</td><td>" + a.dbalance.ToString("0.00") + "</td><td>" + a.cbalance.ToString("0.00") + "</td><td>" + a.grouptotal.ToString("0.00") + "</td></tr>";

                    model.label1 = (Convert.ToDecimal(model.label1) + Convert.ToDecimal(a.dbalance)).ToString("0.00");
                    model.label2 = (Convert.ToDecimal(model.label2) + Convert.ToDecimal(a.cbalance)).ToString("0.00");


                }
            }

            return Json(model);
        }
        public JsonResult SaveDataoftrialblance(TrialBalanceReportViewModel model)
        {
           
            GL_BALNCE gl = new GL_BALNCE();

         string msg= gl.SaveInDividentLedger(model);

            return Json(msg);
        }
        [HttpGet]
        public ActionResult GeneralLedgerReport(GeneralLedgerReportViewModel model)
        {
            UtilityController u = new UtilityController();

            model.BranchDesc = u.getBranchMastDetails();


            return View(model);
        }
    }
}