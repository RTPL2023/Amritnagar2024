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
        /********************************************Online Cash Recieve Start*******************************************/
        [HttpGet]
        public ActionResult OnLineCashReceive(OnLineCashReceiveViewModel model)
        {
            UtilityController u = new UtilityController();
            model.BranchDesc = u.getBranchMastDetails();
            model.TypeDesc = u.getTypeMastDetails();
            model.CounterDesc = u.getCounterMast();
            model.achddesc = u.getAcc_hd();
            model.date = DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/");
            return View(model);
        }
        public JsonResult getpersonalaccountinformation(OnLineCashReceiveViewModel model)
        {
            Loan_Ledger ld = new Loan_Ledger();
            List<Loan_Ledger> ldl = new List<Loan_Ledger>();
            ldl = ld.gepesonalandledgerdetails(model.branch, model.acc_no, model.achd, model.op_dt, model.date);
            int i = 1;
            string str_if_lti = "";
            if (ldl.Count > 0)
            {
                foreach (var a in ldl)
                {       
                    if(a.if_lti == 1)
                    {
                        str_if_lti = "YES";
                    }
                    else
                    {
                        str_if_lti = "NO";
                    }
                    if (i == 1)
                    {
                        model.tableelement = "<tr><th>Srl</th><th>CustomerId</th><th>Name of Customer</th><th>Sex</th><th>Occupation</th><th>Sign</th><th>LTI</th><th>PAN No</th></tr>";
                        model.tableelement = model.tableelement + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.cus_id + "</td><td>" + a.cus_name + "</td><td>" + a.sex + "</td><td>" + a.occp + "</td><td>" + a.sign + "</td><td>" + str_if_lti + "</td><td>" + a.pan_no + "</td></tr>";                       
                    }
                    else
                    {
                        model.tableelement = model.tableelement + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.cus_id + "</td><td>" + a.cus_name + "</td><td>" + a.sex + "</td><td>" + a.occp + "</td><td>" + a.sign + "</td><td>" + str_if_lti + "</td><td>" + a.pan_no + "</td></tr>";
                    }
                    i = i + 1;
                    if(a.cus_name == null)
                    {
                        model.name = "";
                    }
                    else
                    {
                        model.name = a.cus_name.ToUpper();
                    }                    
                    model.op_dt = a.open_dt.ToString("dd/MM/yyyy").Replace("-", "/");
                    if(a.led_base_amt > 0)
                    {
                        model.amt = a.led_base_amt.ToString("0.00");
                    }
                }
            }
            else
            {
                model.tableelement = null;
            }
            return Json(model);
        }
        public JsonResult getpersonalledgerinformation(OnLineCashReceiveViewModel model)
        {
            Loan_Ledger ld = new Loan_Ledger();
            List<Loan_Ledger> ldl = new List<Loan_Ledger>();
            ldl = ld.gepesonalandledgerdetails(model.branch, model.acc_no, model.achd, model.op_dt, model.date);
            int i = 1;
            decimal amt = 0;
            decimal base_amt = 0;
            string ledger_tab = "";
            string tf_buffer = "";
            if (ldl.Count > 0)
            {
                foreach (var a in ldl)
                {
                    if (i == 1)
                    {
                        if(a.dr_cr == "D")
                        {
                            if(a.prin_bal > 0)
                            {
                                model.tableelement = "<tr><th>Date</th><th>Particulars</th><th>Debit Amount</th><th>Credit Amount</th><th>Prin Capital Balance</th><th>Interest</th></tr>";
                                model.tableelement = model.tableelement + "<tr><td>" + a.vch_dt.ToString("dd/MM/yyyy").Replace("-", "/") + "</td><td>" + a.xpart + "</td><td>" + a.xamt.ToString("0.00") + "</td><td>" + "" + "</td><td>" + a.prin_bal.ToString("0.00") + "</td><td>" + "" + "</td></tr>";
                            }
                            amt = a.prin_bal;
                            if (a.int_bal > 0)
                            {
                                model.tableelement = "<tr><th>Date</th><th>Particulars</th><th>Debit Amount</th><th>Credit Amount</th><th>Prin Capital Balance</th><th>Interest</th></tr>";
                                model.tableelement = model.tableelement + "<tr><td>" + a.vch_dt.ToString("dd/MM/yyyy").Replace("-", "/") + "</td><td>" + a.xpart + "</td><td>" + a.xamt.ToString("0.00") + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + a.int_bal.ToString("0.00") + "</td></tr>";
                            }                          
                        }
                        else
                        {
                            if (a.prin_bal > 0)
                            {
                                model.tableelement = "<tr><th>Date</th><th>Particulars</th><th>Debit Amount</th><th>Credit Amount</th><th>Prin Capital Balance</th><th>Interest</th></tr>";
                                model.tableelement = model.tableelement + "<tr><td>" + a.vch_dt.ToString("dd/MM/yyyy").Replace("-", "/") + "</td><td>" + a.xpart + "</td><td>" + "" + "</td><td>" + a.xamt.ToString("0.00") + "</td><td>" + a.prin_bal.ToString("0.00") + "</td><td>" + "" + "</td></tr>";
                            }
                            amt = a.prin_bal;
                            if (a.int_bal > 0)
                            {
                                model.tableelement = "<tr><th>Date</th><th>Particulars</th><th>Debit Amount</th><th>Credit Amount</th><th>Prin Capital Balance</th><th>Interest</th></tr>";
                                model.tableelement = model.tableelement + "<tr><td>" + a.vch_dt.ToString("dd/MM/yyyy").Replace("-", "/") + "</td><td>" + a.xpart + "</td><td>" + "" + "</td><td>" + a.xamt.ToString("0.00") + "</td><td>" + "" + "</td><td>" + a.int_bal.ToString("0.00") + "</td></tr>";
                            }
                        }                        
                    }
                    else
                    {
                        if (a.dr_cr == "D")
                        {
                            if (a.prin_bal > 0)
                            {
                                model.tableelement = "<tr><th>Date</th><th>Particulars</th><th>Debit Amount</th><th>Credit Amount</th><th>Prin Capital Balance</th><th>Interest</th></tr>";
                                model.tableelement = model.tableelement + "<tr><td>" + a.vch_dt.ToString("dd/MM/yyyy").Replace("-", "/") + "</td><td>" + a.xpart + "</td><td>" + a.xamt.ToString("0.00") + "</td><td>" + "" + "</td><td>" + a.prin_bal.ToString("0.00") + "</td><td>" + "" + "</td></tr>";
                            }
                            amt = a.prin_bal;
                            if (a.int_bal > 0)
                            {
                                model.tableelement = "<tr><th>Date</th><th>Particulars</th><th>Debit Amount</th><th>Credit Amount</th><th>Prin Capital Balance</th><th>Interest</th></tr>";
                                model.tableelement = model.tableelement + "<tr><td>" + a.vch_dt.ToString("dd/MM/yyyy").Replace("-", "/") + "</td><td>" + a.xpart + "</td><td>" + a.xamt.ToString("0.00") + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + a.int_bal.ToString("0.00") + "</td></tr>";
                            }
                        }
                        else
                        {
                            if (a.prin_bal > 0)
                            {
                                model.tableelement = "<tr><th>Date</th><th>Particulars</th><th>Debit Amount</th><th>Credit Amount</th><th>Prin Capital Balance</th><th>Interest</th></tr>";
                                model.tableelement = model.tableelement + "<tr><td>" + a.vch_dt.ToString("dd/MM/yyyy").Replace("-", "/") + "</td><td>" + a.xpart + "</td><td>" + "" + "</td><td>" + a.xamt.ToString("0.00") + "</td><td>" + a.prin_bal.ToString("0.00") + "</td><td>" + "" + "</td></tr>";
                                amt = a.prin_bal;
                            }
                            amt = a.prin_bal;
                            if (a.int_bal > 0)
                            {
                                model.tableelement = "<tr><th>Date</th><th>Particulars</th><th>Debit Amount</th><th>Credit Amount</th><th>Prin Capital Balance</th><th>Interest</th></tr>";
                                model.tableelement = model.tableelement + "<tr><td>" + a.vch_dt.ToString("dd/MM/yyyy").Replace("-", "/") + "</td><td>" + a.xpart + "</td><td>" + "" + "</td><td>" + a.xamt.ToString("0.00") + "</td><td>" + "" + "</td><td>" + a.int_bal.ToString("0.00") + "</td></tr>";
                            }
                        }
                    }                       
                    ledger_tab = a.ledger_tab;                               
                    tf_buffer = a.tf_buffer.ToString();
                    base_amt = a.led_base_amt;
                    i = i + 1;                   
                }
                ld = ld.PROCESS_DUE(amt, Convert.ToDateTime(model.op_dt), Convert.ToDateTime(model.date), ledger_tab, Convert.ToDecimal(tf_buffer));
                if(ld.XPDUPTO.ToString("dd/MM/yyyy").Replace("-", "/") == "01/01/0001")
                {
                    model.clrd_upto = "";
                }
                else
                {
                    model.clrd_upto = ld.XPDUPTO.ToString("dd/MM/yyyy").Replace("-", "/");
                }
                if(ld.due > 0)
                {
                    model.amt_due = ld.due.ToString("0.00");
                }
                else
                {
                    model.amt_due = "";
                }               
                if(base_amt > 0)
                {
                    model.amt = base_amt.ToString("0.00");
                }
            }
            else
            {
                model.tableelement = null;
            }
            return Json(model);
        }
        public JsonResult SaveCashRecieptData(string branch, string date, string shift, string counter, string vch_no, string achd, string acc_no, string name, string amt)
        {            
            TVCH_HEADER vh = new TVCH_HEADER();
            TVCH_DETAIL vd = new TVCH_DETAIL();
            vh.SaveUpdateTVCH_Header(branch, date, shift, counter, vch_no);
            vd.Check_DeleteTVCH_Detail(branch, date, shift, vch_no, counter);
            vd.SaveUpdateTVch_Detail(date, vch_no, branch, shift, counter, acc_no, name, achd, amt);
            return Json("OVER");
        }
        public JsonResult GetRecieptdetails(OnLineCashReceiveViewModel model)
        {
            TVCH_DETAIL tvd = new TVCH_DETAIL();
            Loan_Ledger ld = new Loan_Ledger();
            List<TVCH_DETAIL> tvdl = new List<TVCH_DETAIL>();
            tvdl = tvd.getpersonalrecieptdetails(model.branch, model.shift, model.date, model.counter);
            int i = 1;           
            if (tvdl.Count > 0)
            {
                foreach (var a in tvdl)
                {                  
                    if (i == 1)
                    {
                        model.tableelement = "<tr><th>Srl</th><th>A/C Head</th><th>Voucher No</th><th>Account No</th><th>Recieved From Particulars</th><th>Amount</th></tr>";
                        model.tableelement = model.tableelement + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.ac_hd + "</td><td>" + a.trn_no + "</td><td>" + a.vch_pacno + "</td><td>" + a.vch_acname + "</td><td>" + a.vch_amt.ToString("0.00") + "</td></tr>";
                    }
                    else
                    {
                        model.tableelement = model.tableelement + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.ac_hd + "</td><td>" + a.trn_no + "</td><td>" + a.vch_pacno + "</td><td>" + a.vch_acname + "</td><td>" + a.vch_amt.ToString("0.00") + "</td></tr>";
                    }
                    i = i + 1;                  
                }
                
            }
            else
            {
                model.tableelement = null;
            }
            return Json(model);
        }
        public JsonResult GetRecieptSummary(OnLineCashReceiveViewModel model)
        {
            decimal GT_HD = 0;
            decimal GT_AMT = 0;
            TVCH_DETAIL tvd = new TVCH_DETAIL();
            List<TVCH_DETAIL> tvdl = new List<TVCH_DETAIL>();
            tvdl = tvd.getsummaryreciept(model.branch, model.shift, model.date, model.counter);
            int i = 1;
            if (tvdl.Count > 0)
            {
                foreach (var a in tvdl)
                {
                    if (i == 1)
                    {
                        model.tableelement = "<tr><th>Srl</th><th>A/C Head</th><th>Heads</th><th>A/C Deposit</th></tr>";
                        model.tableelement = model.tableelement + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.ac_hd + "</td><td>" + a.tot_cnt + "</td><td>" + a.tot_depamt.ToString("0.00") + "</td></tr>";
                    }
                    else
                    {
                        model.tableelement = model.tableelement + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.ac_hd + "</td><td>" + a.tot_cnt + "</td><td>" + a.tot_depamt.ToString("0.00") + "</td></tr>";
                    }
                    i = i + 1;
                }
            }
            else
            {
                model.tableelement = null;
            }
            return Json(model);
        }
        public JsonResult Get_Last_TR(OnLineCashReceiveViewModel model)
        {
            TVCH_DETAIL vd = new TVCH_DETAIL();
            vd = vd.getlasttr(model.branch, model.shift, model.date, model.counter);
            model.gt_head = vd.tot_cnt.ToString();
            model.gt_amt = vd.tot_amt.ToString("0.00");
            return Json(model);
        }
        public JsonResult getvchno(OnLineCashReceiveViewModel model)
        {
            TVCH_DETAIL vd = new TVCH_DETAIL();
            vd = vd.getlastvch_no(model.branch, model.shift, model.date, model.counter);
            model.vch_no = vd.trn_no;
            return Json(model);
        }
        public JsonResult DeleteCashReciept(OnLineCashReceiveViewModel model)
        {
            TVCH_DETAIL vd = new TVCH_DETAIL();
            vd.Delete_Cash_Reciept(model.achd, model.vch_no, model.date, model.acc_no);
            return Json("Record Deleted");
        }

       
        /********************************************Online Cash Recieve End*******************************************/
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
                    model.vch_type = a.vch_type;
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
        public JsonResult FillVoucherType()
        {
            Vch_Type vh = new Vch_Type();
            var vchtypes = vh.VchType();
            return Json(vchtypes, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Add_Vch_In_Temp_table(string vch_date, string drcr, string vch_achd, string vchpacno, string particular, string amount, string ref_ac_hd, string refacno, string refParticular, string vch_no, string vch_type)
        {
            Temp_Vch_Entry tve = new Temp_Vch_Entry();
            int serial = tve.GetLastSerialNoBydate(vch_date, vch_no);
            tve.srl = serial;
            tve.drcr = drcr;
            tve.ac_hd = vch_achd;
            tve.vch_pacno = vchpacno;
            tve.vch_no = vch_no;
            tve.paid_to_rcv_frm = particular;
            tve.vch_type = vch_type;
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
        public JsonResult GetParticularsForvchEntry(string vch_achd, string vchpacno, string branch)
        {
            ACC_HEAD ah = new ACC_HEAD();
            VoucherEntryViewModel model = new VoucherEntryViewModel();
            ah = ah.Getparticular(vch_achd, vchpacno, branch);
            model.particular = ah.particulars;
            model.clos_flag = ah.clos_flag;
            return Json(model);
        }
        public JsonResult AchdListForVoucherEntry(string vch_achd)
        {
            List<ACC_HEAD> aclist = new List<ACC_HEAD>();
            VoucherEntryViewModel ve = new VoucherEntryViewModel();
            ACC_HEAD acchd = new ACC_HEAD();
            aclist = acchd.getAchdListForVchEntry(vch_achd);
            return Json(aclist, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetamountForvchEntry(string vch_date, string vch_no)
        {
            Temp_Vch_Entry tve = new Temp_Vch_Entry();
            List<Temp_Vch_Entry> tvel = new List<Temp_Vch_Entry>();
            VoucherEntryViewModel model = new VoucherEntryViewModel();
            model.debt_amt = "0";
            model.crdt_amt = "0";
            tvel = tve.GetTempVchDataByVchdate(vch_date, vch_no);
            if (tvel.Count > 0)
            {
                foreach (var a in tvel)
                {
                    if (a.drcr == "D" || a.drcr == "d")
                    {
                        model.debt_amt = Convert.ToString(Convert.ToDecimal(model.debt_amt) + a.amount);
                    }
                    else
                    {
                        model.crdt_amt = Convert.ToString(Convert.ToDecimal(model.crdt_amt) + a.amount);
                    }
                }
            }
            return Json(model);
        }
        public JsonResult GetamountForvchModify(string amount1, string drcr1)
        {
            Temp_Vch_Entry tve = new Temp_Vch_Entry();        
            VoucherEntryViewModel model = new VoucherEntryViewModel();
            model.debt_amt = "0";
            model.crdt_amt = "0";
            if(drcr1 == "D" || drcr1 == "d")
            {
                model.debt_amt = amount1;
            }
            else
            {
                model.crdt_amt = amount1;
            }
            return Json(model);
        }
        public JsonResult SaveVoucherData(string vch_date, string txtvch_No, string vch_Type, string Vch_narr, string branch_id)
        {
            Vch_header vh = new Vch_header();
            Vch_Details vd = new Vch_Details();
            vh.SaveUpdateVoucherHeader(vch_date, txtvch_No, vch_Type, Vch_narr, branch_id);
            vd.Check_DeleteVchDetail(vch_date, txtvch_No, branch_id);
            vd.SaveUpdateVchDetail(vch_date, txtvch_No, branch_id, vch_Type);
            Temp_Vch_Entry tve = new Temp_Vch_Entry();
            tve.DeleteTempData(vch_date, txtvch_No);
            return Json("OVER");
        }
        public JsonResult Delete_Vch_In_Temp_table_byVchNo(string vch_no)
        {
            Temp_Vch_Entry tve = new Temp_Vch_Entry();
            tve.DeleteTempDatabyvchno(vch_no);
            return Json("OVER");
        }
        public JsonResult DeleteAndAddNewRowInTempTable(string vch_date, string drcr, string vch_achd, string vchpacno, string particular, string amount, string ref_ac_hd, string refacno, string refParticular, string txtvch_No, int srl, string vch_type)
        {
            Temp_Vch_Entry tve = new Temp_Vch_Entry();
            tve.srl = srl;
            tve.drcr = drcr;
            tve.ac_hd = vch_achd;
            tve.vch_pacno = vchpacno;
            tve.vch_no = txtvch_No;
            tve.paid_to_rcv_frm = particular;
            tve.vch_type = vch_type;
            //tve.vch_dt = Convert.ToDateTime(vch_date);
            tve.str_vchdt = vch_date.Replace("-", "/");
            tve.amount = Convert.ToDecimal(amount);
            tve.ref_achd = ref_ac_hd;
            tve.ref_acno = refacno;
            tve.ref_ac_particulars = refParticular;
            tve.created_by = Convert.ToString(Session["Uid"]);
            tve.created_on = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/"));
            tve.computer_name = Environment.MachineName;
            tve.UpdateTempVchData(tve);
            return Json("Over");
        }
        public JsonResult gettempdetails(string vch_date)
        {
            VoucherEntryViewModel model = new VoucherEntryViewModel();
            Temp_Vch_Entry tve = new Temp_Vch_Entry();
            List<Temp_Vch_Entry> tvel = new List<Temp_Vch_Entry>();
            tvel = tve.getdetails(vch_date);
            int i = 1;                    
            if (tvel.Count > 0)
            {
                foreach (var a in tvel)
                {            
                    if(a.vch_type == "C")
                    {
                        a.vch_type = "CASH";
                    }
                    if (a.vch_type == "T")
                    {
                        a.vch_type = "TRANSFER";
                    }
                    if (a.vch_type == "J")
                    {
                        a.vch_type = "JOURNAL";
                    }
                    if (a.vch_type == "B")
                    {
                        a.vch_type = "BANK";
                    }
                    if (i == 1)
                    {
                        model.tableelement = "<tr><th>Srl</th><th>DrCr</th><th>A/C Head</th><th>Vch No</th><th>A/C No</th><th>Paid To</th><th>Amount</th><th>Ref A/C Head</th><th>Ref A/C No</th><th>Ref Particular</th><th>Type</th></tr>";
                        model.tableelement = model.tableelement + "<tr><td>" + a.srl + "</td><td>" + a.drcr + "</td><td>" + a.ac_hd + "</td><td>" + a.vch_no + "</td><td>" + a.vch_pacno + "</td><td>" + a.paid_to_rcv_frm + "</td><td>" + a.amount + "</td><td>" + a.ref_achd + "</td><td>" + a.ref_acno + "</td><td>" + a.ref_ac_particulars + "</td><td>" + a.vch_type + "</td></tr>";
                    }
                    else
                    {
                        model.tableelement = model.tableelement + "<tr><td>" + a.srl + "</td><td>" + a.drcr + "</td><td>" + a.ac_hd + "</td><td>" + a.vch_no + "</td><td>" + a.vch_pacno + "</td><td>" + a.paid_to_rcv_frm + "</td><td>" + a.amount + "</td><td>" + a.ref_achd + "</td><td>" + a.ref_acno + "</td><td>" + a.ref_ac_particulars + "</td><td>" + a.vch_type + "</td></tr>";
                    }
                    i = i + 1;
                }
            }
            else
            {
                model.tableelement = null;
            }
            return Json(model);
        }

        /********************************************Vouchar Entry End*******************************************/
        //********************************Cash Account Report Start*********************************************/
        [HttpGet]
        public ActionResult CashAccountReport(CashAccountReportViewModel model)
        {
            UtilityController u = new UtilityController();
            model.BranchDesc = u.getBranchMastDetails();
            return View(model);
        }
        public JsonResult SaveDataforCashAccount(CashAccountReportViewModel model)
        {
            AccountsUtility au = new AccountsUtility();
            au.CashAccountSave(model);
            return Json("Saved");
        }
        public JsonResult getcashaccountlist(DayBookReportViewModel model)
        {
            List<AccountsUtility> aulst = new List<AccountsUtility>();
            AccountsUtility au = new AccountsUtility();
            aulst = au.getCashAccountlistbydaywise();
            if (aulst.Count > 0)
            {
                model.tableele = "<tr><th> AC MAJGR CR</th><th>AC MAJGR DESC CR</th><th>AC hd CR</th><th>AC  DESC CR</th><th>CASH CR</th>" +
                    "<th>TRANS CR</th><th>majgr CR</th><th>AC MAJGR DR</th><th>AC MAJGR DESC DR</th><th>AC hd DR</th><th>AC  DESC DR</th>" +
                    "<th>CASH DR</th><th>TRANS DR</th><th>TOTAL DR</th><th>MAJGR CASH CR</th><th>MAJGR TRANS CR</th><th>MAJGR TOT CR</th>" +
                    "<th>MAJGR CASH DR</th><th>MAJGR TRANS DR</th><th>MAJGR TOT DR</th></tr>";

                foreach (var a in aulst)
                {
                    model.tableele = model.tableele + "<tr><td>" + a.ac_majgr_cr + "</td><td>" + a.ac_majgrdesc_cr + "</td><td>" + a.ac_hd_cr + "</td><td>"
                        + a.ac_desc_cr + "</td><td>" + a.cash_cr.ToString("0.00") + "</td><td>" + a.trans_cr.ToString("0.00") + "</td><td>" + a.total_cr.ToString("0.00") + "</td>" +
                        "<td>" + a.ac_majgr_dr + "</td><td>" + a.ac_majgrdesc_dr + "</td><td>" + a.ac_hd_dr + "</td><td>" + a.ac_desc_dr + "</td><td>" + a.cash_dr.ToString("0.00") + "</td><td>" + a.trans_dr.ToString("0.00") + "</td><td>" + a.total_dr.ToString("0.00") + "</td><td>" + a.majgr_cash_cr.ToString("0.00") + "</td><td>" + a.majgr_trans_cr.ToString("0.00") + "</td><td>" + a.majgr_tot_cr.ToString("0.00") + "</td><td>" + a.majgr_cash_dr.ToString("0.00") + "</td><td>" + a.majgr_trans_dr.ToString("0.00") + "</td><td>" + a.majgr_tot_dr.ToString("0.00") + "</td></tr>";
                }
            }
            return Json(model);
        }
        public ActionResult CashAccountPrintFiles(string fr_dt, string to_dt, string branch)
        {
            List<AccountsUtility> aulst = new List<AccountsUtility>();
            AccountsUtility au = new AccountsUtility();
            GL_BALNCE gl = new GL_BALNCE();
            string opdtstr = "";
            gl = gl.getopeningbalanceforcashaccount(branch, fr_dt);
            if(gl.gl_date!= null)
            {
                opdtstr = "(Cl/Balance of " + gl.gl_date.ToString("dd/MM/yyyy").Replace("-","/") + ")";
            }
            else
            {
                opdtstr = "";
            }
            aulst = au.getCashAccountlistbydaywise();           
            Directory.CreateDirectory(Server.MapPath("~/wwwroot\\TextFiles"));                   
            using (StreamWriter sw = new StreamWriter(Server.MapPath("~/wwwroot\\TextFiles\\Cash_Account_Details.txt")))
            {
                int Pg = 1;
                int Ln = 0;
                int i = 1;
                string cr_cash = "";
                decimal tot_cash_cr = 0;
                decimal tot_cash_dr = 0;
                decimal tot_transfer_cr = 0;
                decimal tot_transfer_dr = 0;
                decimal cl_bal = 0;
                string cr_transfer = "";
                string dr_cash = "";
                string dr_transfer = "";
                string tot_cr = "";
                string tot_dr = "";
                string cr_particulars = "";
                string dr_particulars = "";
                sw.WriteLine("                                                             AMRIT NAGAR COL. EMP.CO.CR.SO.LTD.                                          ");
                sw.WriteLine("                                                                RANIGANJ,BURDWAN WEST BENGAL");
                sw.WriteLine("______________________________________________________________________________________________________________________________________________________________________________");
                sw.WriteLine("");
                sw.WriteLine("                                                      CASH ACCOUNT FOR THE PERIOD FROM " + fr_dt + " TO " + to_dt + "                        Run Date: " + DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/") + "   Page: " + Pg);
                sw.WriteLine("R E C E I P T S                                                                                                                                              P A Y M E N T S");
                sw.WriteLine("____________________________________________________________________________________________________________________________________________________________________________");
                sw.WriteLine("ACCOUNT PARTICULARS                              CASH        TRANSFER         TOTAL |ACCOUNT PARTICULARS                              CASH        TRANSFER         TOTAL");
                sw.WriteLine("____________________________________________________________________________________________________________________________________________________________________________");
                foreach (var am in aulst)
                {                    
                    if (am.ac_desc_cr.ToString().Length > 25)
                    {
                        cr_particulars = (am.ac_desc_cr).Substring(0, 24);
                    }
                    else
                    {
                        cr_particulars = am.ac_desc_cr;
                    }
                    if (am.ac_desc_dr.ToString().Length > 23)
                    {
                        dr_particulars = (am.ac_desc_dr).Substring(0, 22);
                    }
                    else
                    {
                        dr_particulars = am.ac_desc_dr;
                    }
                    if (am.cash_cr.ToString().Length > 20)
                    {
                        cr_cash = Convert.ToString(am.cash_cr).Substring(0, 19);
                    }
                    else if (am.cash_cr == Convert.ToDecimal(0.0000))
                    {
                        cr_cash = "";
                    }
                    else
                    {                        
                        cr_cash = am.cash_cr.ToString("0.00");
                    }
                    if (am.trans_cr.ToString().Length > 16)
                    {
                        cr_transfer = Convert.ToString(am.trans_cr).Substring(0, 15);
                    }
                    else if (am.trans_cr == Convert.ToDecimal(0.0000))
                    {
                        cr_transfer = "";
                    }
                    else
                    {
                        cr_transfer = am.trans_cr.ToString("0.00");
                    }
                    if (am.cash_dr.ToString().Length > 20)
                    {
                        dr_cash = Convert.ToString(am.cash_dr).Substring(0, 19);
                    }
                    else if (am.cash_dr == Convert.ToDecimal(0.0000))
                    {
                        dr_cash = "";
                    }
                    else
                    {
                        dr_cash = am.cash_dr.ToString("0.00");
                    }
                    if (am.trans_dr.ToString().Length > 14)
                    {
                        dr_transfer = Convert.ToString(am.trans_dr).Substring(0, 13);
                    }
                    else if (am.trans_dr == Convert.ToDecimal(0.0000))
                    {
                        dr_transfer = "";
                    }
                    else
                    {
                        dr_transfer = am.trans_dr.ToString("0.00");
                    }
                    if (am.total_cr.ToString().Length > 13)
                    {
                        tot_cr = Convert.ToString(am.total_cr).Substring(0, 12);
                    }
                    else if (am.total_cr == Convert.ToDecimal(0.0000))
                    {
                        tot_cr = "";
                    }
                    else
                    {
                        tot_cr = am.total_cr.ToString("0.00");
                    }
                    if (am.total_dr.ToString().Length > 16)
                    {
                        tot_dr = Convert.ToString(am.total_dr).Substring(0, 15);
                    }
                    else if (am.total_dr == Convert.ToDecimal(0.0000))
                    {
                        tot_dr = "";
                    }
                    else
                    {
                        tot_dr = am.total_dr.ToString("0.00");
                    }                   
                    if (Ln > Pg * 65)
                    {
                        Pg = Pg + 1;
                        Ln = Ln + 7;
                        sw.WriteLine("                                                             AMRIT NAGAR COL. EMP.CO.CR.SO.LTD.                                          ");
                        sw.WriteLine("                                                                RANIGANJ,BURDWAN WEST BENGAL");
                        sw.WriteLine("________________________________________________________________________________________________________________________________________________________________________");
                        sw.WriteLine("");
                        sw.WriteLine("                                                      CASH ACCOUNT FOR THE PERIOD FROM " + fr_dt + " TO " + to_dt + "                        Run Date: " + DateTime.Now.ToString("dd/MM/yyyy").Replace("-","/") + "   Page: " + Pg);
                        sw.WriteLine("R E C E I P T S                                                                                                                                              P A Y M E N T S");
                        sw.WriteLine("____________________________________________________________________________________________________________________________________________________________________________");
                        sw.WriteLine("ACCOUNT PARTICULARS                              CASH        TRANSFER         TOTAL |ACCOUNT PARTICULARS                              CASH        TRANSFER         TOTAL");
                        sw.WriteLine("____________________________________________________________________________________________________________________________________________________________________________");
                    }
                    sw.WriteLine("".ToString().PadLeft(25 - (cr_particulars).Length) + cr_particulars
                    + "".ToString().PadLeft(30 - (cr_cash).Length) + cr_cash
                    + "".ToString().PadLeft(16 - (cr_transfer).Length) + cr_transfer
                    + "".ToString().PadLeft(13 - (tot_cr).Length) + tot_cr + "|"
                    + "".ToString().PadLeft(23 - (dr_particulars).Length) + dr_particulars
                    + "".ToString().PadLeft(32 - (dr_cash).Length) + dr_cash
                    + "".ToString().PadLeft(14 - (dr_transfer).Length) + dr_transfer
                    + "".ToString().PadLeft(16 - (tot_dr).Length) + tot_dr);                          
                    Ln = Ln + 1;
                    i = i + 1;
                    tot_cash_cr = tot_cash_cr + am.cash_cr;
                    tot_cash_dr = tot_cash_dr + am.cash_dr;
                    tot_transfer_cr = tot_transfer_cr + am.trans_cr;
                    tot_transfer_dr = tot_transfer_dr + am.trans_dr;
                }
                decimal cr_cash_tot = 0;
                decimal dr_cash_tot = 0;
                decimal cr_transfer_tot = 0;
                decimal dr_transfer_tot = 0;
                if (tot_cash_cr.ToString().Length > 13)
                {
                    cr_cash_tot = Convert.ToDecimal((tot_cash_cr).ToString().Substring(0, 12));
                }
                else
                {
                    cr_cash_tot = tot_cash_cr;
                }
                if (tot_cash_dr.ToString().Length > 13)
                {
                    dr_cash_tot = Convert.ToDecimal((tot_cash_dr).ToString().Substring(0, 12));
                }
                else
                {
                    dr_cash_tot = tot_cash_dr;
                }
                if (tot_transfer_cr.ToString().Length > 17)
                {
                    cr_transfer_tot = Convert.ToDecimal((tot_transfer_cr).ToString().Substring(0, 16));
                }
                else
                {
                    cr_transfer_tot = tot_transfer_cr;
                }
                if (tot_transfer_dr.ToString().Length > 17)
                {
                    dr_transfer_tot = Convert.ToDecimal((tot_transfer_dr).ToString().Substring(0, 16));
                }
                else
                {
                    dr_transfer_tot = tot_transfer_dr;
                }
                cl_bal = gl.gl_bal + ((cr_cash_tot + cr_transfer_tot) - (tot_cash_dr + tot_transfer_dr));
                sw.WriteLine("______________________________________________________________________________________________________________________________________________________________________________");
                sw.WriteLine("TOTAL RECIEPTS                              " + "".ToString().PadLeft(13 - (cr_cash_tot).ToString().Length) + cr_cash_tot.ToString("0.00") 
                    + "".ToString().PadLeft(17 - (cr_transfer_tot).ToString().Length) + cr_transfer_tot.ToString("0.00") 
                    + "".ToString().PadLeft(16 - (cr_cash_tot + cr_transfer_tot).ToString().Length) + (cr_cash_tot + cr_transfer_tot).ToString("0.00") 
                    + "|" + "TOTAL PAYMENTS                               " + "".ToString().PadLeft(13 - (dr_cash_tot).ToString().Length) + dr_cash_tot.ToString("0.00") 
                    + "".ToString().PadLeft(17 - (dr_transfer_tot).ToString().Length) + dr_transfer_tot.ToString("0.00")
                    + "".ToString().PadLeft(16 - (tot_cash_dr + tot_transfer_dr).ToString().Length) + (dr_cash_tot + dr_transfer_tot).ToString("0.00"));
                sw.WriteLine("CASH OPENING BALAN" + opdtstr + "".ToString().PadLeft(13 - (gl.gl_bal).ToString().Length) + gl.gl_bal.ToString("0.00")
                    + "".ToString().PadLeft(29 - ("").ToString().Length) + "" + "|CASH CLOSING BALANCE" + "".ToString().PadLeft(38 - (cl_bal).ToString().Length) + cl_bal.ToString("0.00"));
                sw.WriteLine("_______________________________________________________________________________________________________________________________________________________________________________");              
                sw.WriteLine("<< G R A N D  T O T A L >>  " + "".ToString().PadLeft(29 - (cr_cash_tot + gl.gl_bal).ToString().Length) + (cr_cash_tot + gl.gl_bal).ToString("0.00")
                    + "".ToString().PadLeft(18 - (cr_transfer_tot).ToString().Length) + cr_transfer_tot.ToString("0.00")
                    + "".ToString().PadLeft(15 - (cr_cash_tot + gl.gl_bal + cr_transfer_tot).ToString().Length) + (cr_cash_tot + gl.gl_bal + cr_transfer_tot).ToString("0.00")
                    + "|" + "<< G R A N D  T O T A L >>  " + "".ToString().PadLeft(30 - (dr_cash_tot + cl_bal).ToString().Length) + (dr_cash_tot + cl_bal).ToString("0.00")
                    + "".ToString().PadLeft(17 - (dr_transfer_tot).ToString().Length) + dr_transfer_tot.ToString("0.00")
                    + "".ToString().PadLeft(16 - (dr_cash_tot + cl_bal + dr_transfer_tot).ToString().Length) + (dr_cash_tot + cl_bal + dr_transfer_tot).ToString("0.00"));               
                sw.WriteLine("________________________________________________________________________________________________________________________________________________________________________________");
                sw.WriteLine("");
                sw.WriteLine("Cash Account Approved On:                              Prepared By            Signature Of Accountant            Signature Of E.O./Manager/Secretary      Signature Of Treasurer");
            }            
            UtilityController u = new UtilityController();
            var memory = u.DownloadTextFiles("Cash_Account_Details.txt", Server.MapPath("~/wwwroot\\TextFiles"));
            if (System.IO.File.Exists(Server.MapPath("~/wwwroot\\TextFiles\\Cash_Account_Details.txt")))
            {
                System.IO.File.Delete(Server.MapPath("~/wwwroot\\TextFiles\\Cash_Account_Details.txt"));
            }
            return File(memory.ToArray(), "text/plain", "Cash_Account_Details_" + DateTime.Now.ToShortDateString().Replace("/", "_") + ".txt");
        }

        //********************************Cash Account Report End******************************************

        //********************************Day Book Report start**********************************************
        [HttpGet]
        public ActionResult DayBookReport(DayBookReportViewModel model)
        {
            UtilityController u = new UtilityController();

            model.BranchDesc = u.getBranchMastDetails();


            return View(model);
        }
        public JsonResult getAccounthead(string ac_hd)
        {
            ACC_HEAD mi = new ACC_HEAD();
            var achdname = mi.getac_hhdName(ac_hd);
            return Json(achdname, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult getaccountheadparticulars(string ac_hd)
        //{
        //    ACC_HEAD mi = new ACC_HEAD();
        //    string achddesc = mi.getac_hddesc(ac_hd);
        //    return Json(achddesc);
        //}
        public JsonResult SaveDataforDaybook(DayBookReportViewModel model)
        {
            AccountsUtility au = new AccountsUtility();
            au.daybookSave(model);
            return Json("Saved");
        }
        public JsonResult getdaybooklist(DayBookReportViewModel model)
        {
            List<AccountsUtility> aulst = new List<AccountsUtility>();
            AccountsUtility au = new AccountsUtility();
            aulst = au.getdaybooklistbydaywise(model);
            if (aulst.Count > 0)
            {
                model.tableele = "<tr><th> AC_HD</th><th> Account Head </th><th>CASH DR Balance</th><th>BANK DR Balance</th><th>TRANS DR Balance</th><th>CASH CR Balance</th><th>BANK CR Balance</th><th>TRANS CR Balance</th></tr>";
                foreach (var a in aulst)
                {
                    model.tableele = model.tableele + "<tr><td>" + a.ac_hd + "</td><td>" + a.ac_desc + "</td><td>" + a.cash_dr.ToString("0.00") + "</td><td>"
                        + a.bank_dr.ToString("0.00") + "</td><td>" + a.trans_dr.ToString("0.00") + "</td><td>" + a.cash_cr.ToString("0.00") + "</td><td>" + a.bank_cr.ToString("0.00") + "</td><td>" + a.trans_cr.ToString("0.00") + "</td></tr>";
                    model.label1 = (Convert.ToDecimal(model.label1) + a.cash_dr).ToString("0.00");
                    model.label2 = (Convert.ToDecimal(model.label2) + a.bank_dr).ToString("0.00");
                    model.label3 = (Convert.ToDecimal(model.label3) + a.trans_dr).ToString("0.00");
                    model.label4 = (Convert.ToDecimal(model.label3) + a.cash_cr).ToString("0.00");
                    model.label5 = (Convert.ToDecimal(model.label5) + a.bank_cr).ToString("0.00");
                    model.label6 = (Convert.ToDecimal(model.label6) + a.trans_cr).ToString("0.00");
                }
            }
            return Json(model);
        }

        //********************************Day Book Report End******************************************

        //********************************Cash Book Report Start******************************************
        [HttpGet]
        public ActionResult CashBookReport(CashBookReportViewModel model)
        {
            UtilityController u = new UtilityController();

            model.BranchDesc = u.getBranchMastDetails();


            return View(model);
        }
        public JsonResult populateCashBook(CashBookReportViewModel model)
        {
            AccountsUtility au = new AccountsUtility();
            List<AccountsUtility> aulst = new List<AccountsUtility>();
            if (model.book_type == "Journal Book")
                aulst = au.populate_journalBook(model);
            if (model.book_type == "Cash Book")
                aulst = au.PopulateCashbook(model);
            if (aulst.Count > 0)
            {
                if (model.book_type == "Cash Book")
                {
                    model.tablee = "<tr><th>ac hd dr</th><th> ac desc dr </th><th>ac majgr dr</th><th>ac subgr dr</th><th>cash dr</th><th>bank dr</th><th>trans dr</th><th>tot dr</th><th> ac hd cr</th><th> ac desc cr </th><th>ac majgr cr</th><th>ac subgr cr</th><th>cash cr</th><th>bank cr</th><th>trans cr</th><th>tot cr</th></tr>";
                }
                else
                {
                    model.tablee = "<tr><th>vch date</th><th> vch no </th><th>vch srl</th><th>ac hd</th><th>AC DESC</th><th>VCH DRCR</th><th>vch pacno</th><th>vch acname</th><th>VCH NARR</th><th>DEBIT AMT</th><th>CREDIT AMT</th></tr>";
                }
                foreach (var a in aulst)
                {
                    if (model.book_type == "Cash Book")
                    {
                        model.tablee = model.tablee+ "<tr><td>" + a.ac_hd_dr+"</td><td>"+ a.ac_desc_dr +"</td><td>"+a.ac_majgr_dr+"</td><td>"+a.ac_subgr_dr+"</td><td>"+a.cash_dr.ToString("0.00") + "</td><td>"+a.bank_dr.ToString("0.00") + "</td><td>"+a.trans_dr.ToString("0.00") + "</td><td>"+a.tot_dr.ToString("0.00") + "</td><td> "+a.ac_hd_cr+"</td><td>"+ a.ac_desc_cr +"</td><td>"+a.ac_majgr_cr+"</td><td>"+a.ac_subgr_cr+ "</td><td>" + a.cash_cr.ToString("0.00") + " </td><td>"+a.bank_cr.ToString("0.00") + "</td><td>"+a.trans_cr.ToString("0.00") + "</td><td>"+a.tot_cr.ToString("0.00") + "</td></tr>";
                    }
                    else
                    {
                        model.tablee = model.tablee+ "<tr><td>" + a.vch_date + "</td><td>" + a.vch_no + "</td><td>" + a.vch_srl + "</td><td>" + a.ac_hd + "</td><td>" + a.ac_desc + "</td><td>" + a.vch_drcr + "</td><td>" + a.vch_pacno + "</td><td>" + a.vch_acname + "</td><td> " + a.vch_narr + "</td><td>" + a.debit_amt.ToString("0.00") + "</td><td>" + a.credit_amt.ToString("0.00") + "</td></tr>";
                    }
                }               
            }
            return Json(model);
        }
        public JsonResult UpdateGenaralLedger(CashBookReportViewModel model)
        {
            AccountsUtility au = new AccountsUtility();
            string msg = au.updateGenaralLedger(model);
            return Json(msg);
        }
        public ActionResult getcashbookprintfile(CashBookReportViewModel model)
        {
            AccountsUtility au = new AccountsUtility();
            List<AccountsUtility> aulst = new List<AccountsUtility>();
            if (model.book_type == "Journal Book")
            {
                aulst = au.populate_journalBook(model);
            }              
            if (model.book_type == "Cash Book")
            {
                GL_BALNCE gl = new GL_BALNCE();
                string opdtstr = "";
                string cldtstr = "";
                string op_cash_in_word = "";
                string cl_cash_in_word = "";
                decimal closed_cash = 0;
                decimal closed_bank = 0;
                gl = gl.getopeningbalanceforcashbook(model.branch, model.fr_dt);                               
                if (gl.gl_date != null)
                {
                    opdtstr = "(Cl/Bal of " + gl.gl_date.ToString("dd/MM/yyyy").Replace("-", "/") + ")";
                }
                else
                {
                    opdtstr = "";
                }
                cldtstr = "(As On " + DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/") + ")";              
                aulst = au.PopulateCashbook(model);
                using (StreamWriter sw = new StreamWriter(Server.MapPath("~/wwwroot\\TextFiles\\Cash_Book_Details.txt")))
                {
                    int Pg = 1;
                    int Ln = 0;
                    int i = 1;
                    string cr_cash = "";
                    string cr_bank = "";
                    string dr_bank = "";
                    decimal tot_cash_cr = 0;
                    decimal tot_cash_dr = 0;
                    decimal tot_transfer_cr = 0;
                    decimal tot_transfer_dr = 0;
                    decimal tot_bank_dr = 0;
                    decimal tot_bank_cr = 0;
                    string cr_transfer = "";
                    string dr_cash = "";
                    string dr_transfer = "";
                    string tot_cr = "";
                    string tot_dr = "";
                    decimal totcr = 0;
                    decimal totdr = 0;
                    string cr_particulars = "";
                    string dr_particulars = "";
                    sw.WriteLine("                                          AMRIT NAGAR COL. EMP.CO.CR.SO.LTD.(MAIN BRANCH)                                          ");
                    sw.WriteLine("                                           GENERAL CASH BOOK FOR  " + model.fr_dt);
                    sw.WriteLine("                                                                                                                                    Run Date: " + DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/") + "   Page: " + Pg);
                    sw.WriteLine("");
                    sw.WriteLine("R E C E I P T S                                                                                                                                              P A Y M E N T S");
                    sw.WriteLine("---------------------------------------------+------------+------------+------------+--------------++----------------------------------------------+------------+------------+------------+--------------");
                    sw.WriteLine("ACCOUNT PARTICULARS                          |  TRANSFER  |    CASH    |    BANK    |    TOTAL     || ACCOUNT PARTICULARS                          |  TRANSFER  |    CASH    |    BANK    |    TOTAL");
                    sw.WriteLine("---------------------------------------------+------------+------------+------------+--------------++----------------------------------------------+------------+------------+------------+--------------");
                    foreach (var am in aulst)
                    {
                        
                        if (am.ac_desc_cr.ToString().Length > 25)
                        {
                            cr_particulars = (am.ac_desc_cr).Substring(0, 24);
                        }
                        else
                        {
                            cr_particulars = am.ac_desc_cr;
                        }
                        if (am.ac_desc_dr.ToString().Length > 23)
                        {
                            dr_particulars = (am.ac_desc_dr).Substring(0, 22);
                        }
                        else
                        {
                            dr_particulars = am.ac_desc_dr;
                        }
                        if (am.cash_cr.ToString().Length > 13)
                        {
                            cr_cash = Convert.ToString(am.cash_cr).Substring(0, 12);
                        }
                        else if (am.cash_cr == Convert.ToDecimal(0.0000))
                        {
                            cr_cash = "";
                        }
                        else
                        {
                            cr_cash = am.cash_cr.ToString("0.00");
                        }
                        if (am.trans_cr.ToString().Length > 16)
                        {
                            cr_transfer = Convert.ToString(am.trans_cr).Substring(0, 15);
                        }
                        else if (am.trans_cr == Convert.ToDecimal(0.0000))
                        {
                            cr_transfer = "";
                        }
                        else
                        {
                            cr_transfer = am.trans_cr.ToString("0.00");
                        }
                        if (am.bank_cr.ToString().Length > 16)
                        {
                            cr_bank = Convert.ToString(am.bank_cr).Substring(0, 15);
                        }
                        else if (am.bank_cr == Convert.ToDecimal(0.0000))
                        {
                            cr_bank = "";
                        }
                        else
                        {
                            cr_bank = am.bank_cr.ToString("0.00");
                        }
                        if (am.bank_dr.ToString().Length > 13)
                        {
                            dr_bank = Convert.ToString(am.bank_cr).Substring(0, 12);
                        }
                        else if (am.bank_dr == Convert.ToDecimal(0.0000))
                        {
                            dr_bank = "";
                        }
                        else
                        {
                            dr_bank = am.bank_dr.ToString("0.00");
                        }
                        if (am.cash_dr.ToString().Length > 17)
                        {
                            dr_cash = Convert.ToString(am.cash_dr).Substring(0, 16);
                        }
                        else if (am.cash_dr == Convert.ToDecimal(0.0000))
                        {
                            dr_cash = "";
                        }
                        else
                        {
                            dr_cash = am.cash_dr.ToString("0.00");
                        }
                        if (am.trans_dr.ToString().Length > 14)
                        {
                            dr_transfer = Convert.ToString(am.trans_dr).Substring(0, 13);
                        }
                        else if (am.trans_dr == Convert.ToDecimal(0.0000))
                        {
                            dr_transfer = "";
                        }
                        else
                        {
                            dr_transfer = am.trans_dr.ToString("0.00");
                        }
                        if (am.tot_cr.ToString().Length > 12)
                        {
                            tot_cr = Convert.ToString(am.tot_cr).Substring(0, 11);
                        }
                        else if (am.tot_cr == Convert.ToDecimal(0.0000))
                        {
                            tot_cr = "";
                        }
                        else
                        {
                            tot_cr = am.tot_cr.ToString("0.00");
                        }
                        if (am.tot_dr.ToString().Length > 15)
                        {
                            tot_dr = Convert.ToString(am.tot_dr).Substring(0, 14);
                        }
                        else if (am.tot_dr == Convert.ToDecimal(0.0000))
                        {
                            tot_dr = "";
                        }
                        else
                        {
                            tot_dr = am.tot_dr.ToString("0.00");
                        }
                        if (Ln > Pg * 65)
                        {
                            Pg = Pg + 1;
                            Ln = Ln + 7;
                            sw.WriteLine("                                          AMRIT NAGAR COL. EMP.CO.CR.SO.LTD.(MAIN BRANCH)                                          ");
                            sw.WriteLine("                                           GENERAL CASH BOOK FOR  " + model.fr_dt);
                            sw.WriteLine("                                                                                                                                    Run Date: " + DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/") + "   Page: " + Pg);
                            sw.WriteLine("");
                            sw.WriteLine("R E C E I P T S                                                                                                                                              P A Y M E N T S");
                            sw.WriteLine("---------------------------------------------+------------+------------+------------+--------------++----------------------------------------------+------------+------------+------------+--------------");
                            sw.WriteLine("ACCOUNT PARTICULARS                          |  TRANSFER  |    CASH    |    BANK    |    TOTAL     || ACCOUNT PARTICULARS                          |  TRANSFER  |    CASH    |    BANK    |    TOTAL");
                            sw.WriteLine("---------------------------------------------+------------+------------+------------+--------------++----------------------------------------------+------------+------------+------------+--------------");
                        }
                        sw.WriteLine("".ToString().PadLeft(25 - (cr_particulars).Length) + cr_particulars
                        + "".ToString().PadLeft(33 - (cr_transfer).Length) + cr_transfer
                        + "".ToString().PadLeft(13 - (cr_cash).Length) + cr_cash
                        + "".ToString().PadLeft(16 - (cr_bank).Length) + cr_bank
                        + "".ToString().PadLeft(12 - (tot_cr).Length) + tot_cr + "|"+ "|"
                        + "".ToString().PadLeft(23 - (dr_particulars).Length) + dr_particulars
                        + "".ToString().PadLeft(32 - (dr_transfer).Length) + dr_transfer
                        + "".ToString().PadLeft(17 - (dr_cash).Length) + dr_cash
                        + "".ToString().PadLeft(13 - (dr_bank).Length) + dr_bank
                        + "".ToString().PadLeft(15 - (tot_dr).Length) + tot_dr);
                        Ln = Ln + 1;
                        i = i + 1;
                        tot_cash_cr = tot_cash_cr + am.cash_cr;
                        tot_cash_dr = tot_cash_dr + am.cash_dr;
                        tot_transfer_cr = tot_transfer_cr + am.trans_cr;
                        tot_transfer_dr = tot_transfer_dr + am.trans_dr;
                        tot_bank_cr = tot_bank_cr + am.bank_cr;
                        tot_bank_dr = tot_bank_dr + am.bank_dr;
                        //if (tot_cr != null && tot_cr!="")
                        //{
                        //    totcr = Convert.ToDecimal(tot_cr);
                        //}                      
                        //if (tot_dr != null && tot_dr != "")
                        //{
                        //    totdr = Convert.ToDecimal(tot_dr);
                        //}
                        //closed_cash = gl.op_cash + Convert.ToDecimal(totcr) - Convert.ToDecimal(totdr);
                        //closed_bank = gl.op_bank + Convert.ToDecimal(totcr) - Convert.ToDecimal(totdr);                        
                    }
                    decimal cr_cash_tot = 0;
                    decimal dr_cash_tot = 0;
                    decimal cr_transfer_tot = 0;
                    decimal dr_transfer_tot = 0;
                    decimal cr_bank_tot = 0;
                    decimal dr_bank_tot = 0;
                    decimal op_cash = 0;
                    decimal op_bank = 0;
                    decimal cl_bank = 0;                   
                    if (tot_cash_cr.ToString().Length > 13)
                    {
                        cr_cash_tot = Convert.ToDecimal((tot_cash_cr).ToString().Substring(0, 12));
                    }
                    else
                    {
                        cr_cash_tot = tot_cash_cr;
                    }
                    if (tot_bank_cr.ToString().Length > 13)
                    {
                        cr_bank_tot = Convert.ToDecimal((tot_bank_cr).ToString().Substring(0, 12));
                    }
                    else
                    {
                        cr_bank_tot = tot_bank_cr;
                    }
                    if (tot_cash_dr.ToString().Length > 13)
                    {
                        dr_cash_tot = Convert.ToDecimal((tot_cash_dr).ToString().Substring(0, 12));
                    }
                    else
                    {
                        dr_cash_tot = tot_cash_dr;
                    }
                    if (tot_bank_dr.ToString().Length > 14)
                    {
                        dr_bank_tot = Convert.ToDecimal((tot_bank_dr).ToString().Substring(0, 13));
                    }
                    else
                    {
                        dr_bank_tot = tot_bank_dr;
                    }
                    if (tot_transfer_cr.ToString().Length > 29)
                    {
                        cr_transfer_tot = Convert.ToDecimal((tot_transfer_cr).ToString().Substring(0, 28));
                    }
                    else
                    {
                        cr_transfer_tot = tot_transfer_cr;
                    }
                    if (tot_transfer_dr.ToString().Length > 30)
                    {
                        dr_transfer_tot = Convert.ToDecimal((tot_transfer_dr).ToString().Substring(0, 29));
                    }
                    else
                    {
                        dr_transfer_tot = tot_transfer_dr;
                    }
                    if (gl.op_cash.ToString().Length > 25)
                    {
                        op_cash = Convert.ToDecimal((gl.op_cash).ToString().Substring(0, 24));
                    }
                    else
                    {
                        op_cash = gl.op_cash;
                    }
                    if (gl.op_bank.ToString().Length > 14)
                    {
                        op_bank = Convert.ToDecimal((gl.op_bank).ToString().Substring(0, 13));
                    }
                    else
                    {
                        op_bank = gl.op_bank;
                    }
                    closed_cash = gl.op_cash + ((cr_cash_tot + cr_transfer_tot + cr_bank_tot) - (dr_cash_tot + dr_bank_tot + dr_transfer_tot));
                    closed_bank = gl.op_bank + ((cr_cash_tot + cr_transfer_tot + cr_bank_tot) - (dr_cash_tot + dr_bank_tot + dr_transfer_tot));
                    op_cash_in_word = wordtonumber(Convert.ToInt32(Math.Abs(gl.op_cash))) + " Only";
                    cl_cash_in_word = wordtonumber(Convert.ToInt32(Math.Abs(closed_cash))) + " Only";
                    if (closed_bank.ToString().Length > 14)
                    {
                        cl_bank = Convert.ToDecimal((closed_bank).ToString().Substring(0, 13));
                    }
                    else
                    {
                        cl_bank = closed_bank;
                    }
                    sw.WriteLine("---------------------------------------------------------------------------------------------------++----------------------------------------------------------------------------------------------------");
                    sw.WriteLine("TOTAL RECIEPTS                              " + "".ToString().PadLeft(13 - (cr_transfer_tot).ToString().Length) + cr_transfer_tot.ToString("0.00")
                        + "".ToString().PadLeft(19 - (cr_cash_tot).ToString().Length) + cr_cash_tot.ToString("0.00")
                        + "".ToString().PadLeft(13 - (cr_bank_tot).ToString().Length) + cr_bank_tot.ToString("0.00")
                        + "".ToString().PadLeft(18 - (cr_cash_tot + cr_transfer_tot + cr_bank_tot).ToString().Length) + (cr_cash_tot + cr_transfer_tot + cr_bank_tot).ToString("0.00") + "|"
                        + "|" + "TOTAL PAYMENTS                               " + "".ToString().PadLeft(13 - (dr_transfer_tot).ToString().Length) + dr_transfer_tot.ToString("0.00") 
                        + "".ToString().PadLeft(19 - (dr_cash_tot).ToString().Length) + dr_cash_tot.ToString("0.00")
                        + "".ToString().PadLeft(14 - (dr_bank_tot).ToString().Length) + dr_bank_tot.ToString("0.00")
                        + "".ToString().PadLeft(17 - (tot_cash_dr + tot_transfer_dr + dr_bank_tot).ToString().Length) + (dr_cash_tot + dr_transfer_tot + dr_bank_tot).ToString("0.00"));
                    sw.WriteLine("                                                                                                   ||                                                                                               ");
                    sw.WriteLine("CASH OPENING BALAN" + opdtstr + "".ToString().PadLeft(25 - (op_cash).ToString().Length) + op_cash.ToString("0.00") 
                        + "".ToString().PadLeft(14 - (op_bank).ToString().Length) + op_bank.ToString("0.00") 
                        + "".ToString().PadLeft(17 - (op_cash + op_bank).ToString().Length) + (op_cash + op_bank).ToString("0.00") 
                        + "||CASH CLOSING BALANCE" + cldtstr + "".ToString().PadLeft(37 - (closed_cash).ToString().Length) + closed_cash.ToString("0.00") 
                        + "".ToString().PadLeft(14 - (cl_bank).ToString().Length) + cl_bank.ToString("0.00") 
                        + "".ToString().PadLeft(17 - (closed_cash + cl_bank).ToString().Length) + (closed_cash + cl_bank).ToString("0.00"));
                    sw.WriteLine("===================================================================================================++====================================================================================================");
                    sw.WriteLine("<< G R A N D  T O T A L >>  " + "".ToString().PadLeft(29 - (cr_transfer_tot).ToString().Length) + (cr_transfer_tot).ToString("0.00")
                        + "".ToString().PadLeft(18 - (cr_cash_tot + op_cash).ToString().Length) + (cr_cash_tot + op_cash).ToString("0.00")
                        + "".ToString().PadLeft(14 - (cr_bank_tot + op_bank).ToString().Length) + (cr_bank_tot + op_bank).ToString("0.00")
                        + "".ToString().PadLeft(18 - (cr_transfer_tot + cr_cash_tot + op_cash + cr_bank_tot + op_bank).ToString().Length) + (cr_transfer_tot + cr_cash_tot + op_cash + cr_bank_tot + op_bank).ToString("0.00") + "|"
                        + "|" + "<< G R A N D  T O T A L >>  " + "".ToString().PadLeft(30 - (dr_transfer_tot).ToString().Length) + (dr_transfer_tot).ToString("0.00")
                        + "".ToString().PadLeft(17 - (dr_cash_tot + closed_cash).ToString().Length) + (dr_cash_tot + closed_cash).ToString("0.00")
                        + "".ToString().PadLeft(17 - (dr_bank_tot + cl_bank).ToString().Length) + (dr_bank_tot + cl_bank).ToString("0.00")
                        + "".ToString().PadLeft(16 - (dr_transfer_tot + dr_cash_tot + closed_cash + dr_bank_tot + cl_bank).ToString().Length) + (dr_transfer_tot + dr_cash_tot + closed_cash + dr_bank_tot + cl_bank).ToString("0.00"));
                    sw.WriteLine("===================================================================================================++====================================================================================================");
                    sw.WriteLine("Opening Cash :                                                                                        Closing Cash :                                                                                     ");
                    sw.WriteLine("Rupees".ToString().PadLeft(48 - (op_cash_in_word).ToString().Length) + (op_cash_in_word)
                        + "Rupees".ToString().PadLeft(118 - (cl_cash_in_word).ToString().Length) + (cl_cash_in_word));
                    sw.WriteLine("=========================================================================================================================================================================================================");
                    sw.WriteLine("                                                                                                                                                                                            End of Report");
                    sw.WriteLine("");
                    sw.WriteLine("Checked & Verified :                                                Authenticated :");
                    sw.WriteLine("");
                    sw.WriteLine("");
                    sw.WriteLine("Signature of Accountant                                             Signature of Manager");
                    sw.WriteLine("                                            Bank Seal");
                }
            }                            
            UtilityController u = new UtilityController();
            var memory = u.DownloadTextFiles("Cash_Book_Details.txt", Server.MapPath("~/wwwroot\\TextFiles"));
            if (System.IO.File.Exists(Server.MapPath("~/wwwroot\\TextFiles\\Cash_Book_Details.txt")))
            {
                System.IO.File.Delete(Server.MapPath("~/wwwroot\\TextFiles\\Cash_Book_Details.txt"));
            }
            return File(memory.ToArray(), "text/plain", "Cash_Book_Details_" + DateTime.Now.ToShortDateString().Replace("/", "_") + ".txt");            
        }      
        public string wordtonumber(long number)
        {
            string word = "";
            if (number == 0)
            {
                word += "Zero";
            }
            if ((number / 100000) > 0)
            {
                word += wordtonumber(number / 100000) + " Lakh ";
                number %= 100000;
            }
            if ((number / 1000) > 0)
            {
                word += wordtonumber((number / 1000)) + " Thousand ";
                number %= 1000;
            }
            if ((number / 100) > 0)
            {
                word += wordtonumber((number / 100)) + " Hundred ";
                number %= 100;
            }
            if (number > 0)
            {
                var unitmap = new[] {"One", "Two", "Three","Four","Five","Six","Seven","Eight","Nine","Ten","Eleven","Twelve","Thirteen","Fourteen","Fifteen","Sixteen","Seventeen","Eighteen",
                    "Nineteen" };
                var tensMap = new[] { "Ten", "Twenty", "Thirty", "Fourty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
                if (number < 20)
                {
                    word += " " + unitmap[number - 1];
                }
                else
                {
                    word += tensMap[(number / 10) - 1];
                    if ((number % 10) > 0)
                        word += " " + unitmap[(number % 10) - 1];

                }
            }
            return word;
        }

        //********************************Cash Book Report End****************************************************

        //********************************Cash Bank Position Report Start******************************************
        [HttpGet]
        public ActionResult CashBankPositionReport(CashBankPositionReportViewModel model)
        {
            UtilityController u = new UtilityController();
            model.as_on_dt = DateTime.Now.ToString("dd-MM-yyyy").Replace("-", "/");
            model.BranchDesc = u.getBranchMastDetails();
            return View(model);
        }
        public JsonResult getcashBankPositionReport(CashBankPositionReportViewModel model)
        {
            AccountsUtility au = new AccountsUtility();
            model = au.getCashBankPositionReport(model);
            return Json(model);
        }
        //********************************Trial Balance Report Start******************************************
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
        //********************************Trial Balance Report End******************************************

        //********************************General Ledger Start******************************************
        [HttpGet]
        public ActionResult GeneralLedgerReport(GeneralLedgerReportViewModel model)
        {
            UtilityController u = new UtilityController();
            model.BranchDesc = u.getBranchMastDetails();
            model.fr_dt = DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/");
            model.to_dt = DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/");
            return View(model);
        }
        public JsonResult AchdListForAccountsReports(string vch_achd)
        {
            List<ACC_HEAD> aclist = new List<ACC_HEAD>();
            VoucherEntryViewModel ve = new VoucherEntryViewModel();
            ACC_HEAD acchd = new ACC_HEAD();
            aclist = acchd.getAchdListForAccountsReports(vch_achd);
            return Json(aclist, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getaccountheadparticulars(string ac_hd)
        {
            ACC_HEAD mi = new ACC_HEAD();
            string achddesc = mi.getac_hddesc(ac_hd);
            return Json(achddesc);
        }
        public JsonResult Populate_general_ledger(GeneralLedgerReportViewModel model)
        {
            Rep_Acc_Genled rag = new Rep_Acc_Genled();
            rag.Check_Delete_SaveGenled(model.branch, model.ac_hd, model.fr_dt, model.to_dt);
            return Json("Over");
        }
        public JsonResult getgeneralledgerdetails(GeneralLedgerReportViewModel model)
        {
            Rep_Acc_Genled rag = new Rep_Acc_Genled();
            List<Rep_Acc_Genled> raglst = new List<Rep_Acc_Genled>();
            raglst = rag.getdetails(model.fr_dt, model.to_dt, model.ac_hd);
            int i = 1;
            if (raglst.Count > 0)
            {
                foreach (var a in raglst)
                {
                    if (i == 1)
                    {
                        model.tableelement = "<tr><th>Sr No</th><th>Ac_Hd</th><th>Ac_Desc</th><th>Ac_Majgrdesc</th><th>GL Type</th><th>GL Date</th><th>Cash Cr</th><th>Bank Cr</th><th>Trans Cr</th><th>Journal Cr</th><th>Total Cr</th><th>Cash Dr</th><th>Bank Dr</th><th>Trans Dr</th><th>Journal Dr</th><th>Total Dr</th><th>GL Bal</th></tr>";
                        model.tableelement = model.tableelement + "<tr><td>" + Convert.ToInt32(i) + "</td><td>" + a.ac_hd + "</td><td>" + a.ac_desc + "</td><td>" + a.ac_majgrdesc + "</td><td>" + a.gl_type + "</td><td>" + a.gl_date.ToString("dd/MM/yyyy").Replace("-", "/") + "</td><td>" + a.cash_cr.ToString("0.00") + "</td><td>" + a.bank_cr.ToString("0.00") + "</td><td>" + a.trans_cr.ToString("0.00") + "</td><td>" + a.journal_cr.ToString("0.00") + "</td><td>" + a.total_cr.ToString("0.00") + "</td><td>" + a.cash_dr.ToString("0.00") + "</td><td>" + a.bank_dr.ToString("0.00") + "</td><td>" + a.trans_dr.ToString("0.00") + "</td><td>" + a.journal_dr.ToString("0.00") + "</td><td>" + a.total_dr.ToString("0.00") + "</td><td>" + a.gl_bal.ToString("0.00") + "</td></tr>";
                    }
                    else
                    {
                        model.tableelement = model.tableelement + "<tr><td>" + Convert.ToInt32(i) + "</td><td>" + a.ac_hd + "</td><td>" + a.ac_desc + "</td><td>" + a.ac_majgrdesc + "</td><td>" + a.gl_type + "</td><td>" + a.gl_date.ToString("dd/MM/yyyy").Replace("-", "/") + "</td><td>" + a.cash_cr.ToString("0.00") + "</td><td>" + a.bank_cr.ToString("0.00") + "</td><td>" + a.trans_cr.ToString("0.00") + "</td><td>" + a.journal_cr.ToString("0.00") + "</td><td>" + a.total_cr.ToString("0.00") + "</td><td>" + a.cash_dr.ToString("0.00") + "</td><td>" + a.bank_dr.ToString("0.00") + "</td><td>" + a.trans_dr.ToString("0.00") + "</td><td>" + a.journal_dr.ToString("0.00") + "</td><td>" + a.total_dr.ToString("0.00") + "</td><td>" + a.gl_bal.ToString("0.00") + "</td></tr>";
                    }
                    i = i + 1;
                }
            }
            else
            {
                model.tableelement = null;
            }
            return Json(model);
        }

        /********************************************General Ledger End*******************************************/
    }
}