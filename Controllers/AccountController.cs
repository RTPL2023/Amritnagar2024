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
        public JsonResult FillVoucherType()
        {
            Vch_Type vh = new Vch_Type();
            var vchtypes = vh.VchType();
            return Json(vchtypes, JsonRequestBehavior.AllowGet);
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
                    if (a.drcr == "D")
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

        /********************************************Vouchar Entry End*******************************************/
        //********************************Cash Account Report Start******************************************

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
                        + a.ac_desc_cr + "</td><td>" + a.cash_cr.ToString("0.00") + "</td><td>" + a.trans_cr + "</td><td>" + a.total_cr.ToString("0.00") + "</td>" +
                        "<td>" + a.ac_majgr_dr + "</td><td>" + a.ac_majgrdesc_dr + "</td><td>" + a.ac_hd_dr + "</td><td>" + a.ac_desc_dr + "</td><td>" + a.cash_dr + "</td><td>" + a.trans_dr + "</td><td>" + a.total_dr + "</td><td>" + a.majgr_cash_cr + "</td><td>" + a.majgr_trans_cr + "</td><td>" + a.majgr_tot_cr + "</td><td>" + a.majgr_cash_dr + "</td><td>" + a.majgr_trans_dr + "</td><td>" + a.majgr_tot_dr + "</td></tr>";

                }

            }

            return Json(model);
        }
        //********************************Cash Account Report End******************************************

        //--------------------------------Day Book Report start-------------------------------------------

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
        public JsonResult getaccountheadparticulars(string ac_hd)
        {
            ACC_HEAD mi = new ACC_HEAD();
            string achddesc = mi.getac_hddesc(ac_hd);
            return Json(achddesc);
        }
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
                    model.tablee = "<tr><th> ac hd dr</th><th> ac desc dr </th><th>ac majgr dr</th><th>ac subgr dr</th><th>cash dr</th><th>bank dr</th><th>trans dr</th><th>tot dr</th><th> ac hd cr</th><th> ac desc cr </th><th>ac majgr cr</th><th>ac subgr cr</th><th>cash cr</th><th>bank cr</th><th>trans cr</th><th>tot cr</th></tr>";
                }
                else
                {
                    model.tablee = "<tr><th> vch date</th><th> vch no </th><th>vch srl</th><th>ac hd</th><th>AC DESC</th><th>VCH DRCR</th><th>vch pacno</th><th>vch acname</th><th>VCH NARR</th><th>DEBIT AMT</th><th>CREDIT AMT</th></tr>";
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


        //********************************Cash Book Report End******************************************

        //********************************Cash Bank Position Report Start******************************************

        [HttpGet]
        public ActionResult CashBankPositionReport(CashBankPositionReportViewModel model)
        {
            UtilityController u = new UtilityController();

            model.BranchDesc = u.getBranchMastDetails();
            return View(model);
        }
        public JsonResult getcashBankPositionReport(CashBankPositionReportViewModel model)
        {

            AccountsUtility au = new AccountsUtility();

            model = au.getCashBankPositionReport(model);

            return Json(model);
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
            raglst = rag.getdetails(model.fr_dt, model.to_dt);
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