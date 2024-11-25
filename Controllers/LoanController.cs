using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Amritnagar.Models.Database;
using Amritnagar.Models.ViewModel;
using System.IO;

namespace Amritnagar.Controllers
{
    public class LoanController : Controller
    {
        /********************************************Loan Master Start*******************************************/
        [HttpGet]
        public ActionResult LoanMasterEntry(LoanMasterEntryViewModel model)
        {
            UtilityController u = new UtilityController();
            model.BranchDesc = u.getBranchMastDetails();
            model.lnstatusdesc = u.getLoanStatusMastDetails();
            model.lntypedesc = u.getLoanTypeMastDetails();
            model.lnpurposedesc = u.getLoanPurposeMastDetails();
            return View(model);
        }
        public JsonResult SaveLoanMaster(LoanMasterEntryViewModel model)
        {
            Loan_Master lm = new Loan_Master();
            lm.branch_id = model.branch_id.ToUpper();
            lm.ac_hd = model.ac_hd;
            lm.emp_id = model.emp_id;
            lm.loan_dt = Convert.ToDateTime(Convert.ToDateTime(model.loan_dt).ToString("dd-MM-yyyy").Replace("-", "/"));
            lm.mem_id = model.mem_id;
            lm.ln_spcl = model.status_cd;
            lm.ln_purpose = model.ln_purpose.ToUpper();
            lm.book_no = model.book_no;
            lm.loanee_name = model.loanee_name.ToUpper();
            lm.loan_amt = Convert.ToDecimal(model.loan_amt);
            lm.inst_no = Convert.ToInt32(model.inst_no);
            lm.inst_rate = Convert.ToDecimal(model.inst_rate);
            lm.inst_amt = Convert.ToDecimal(model.inst_amt);
            lm.created_by = Convert.ToString(Session["Uid"]);
            model.msg = lm.Save(lm);
            Loan_Ledger ld = new Loan_Ledger();
            ld.branch_id = model.branch_id.ToUpper();
            ld.ac_hd = model.ac_hd;
            ld.emp_id = model.emp_id;
            ld.vch_no = model.vch_no;
            ld.vch_srl = 1;
            ld.dr_cr = "D";
            ld.vch_type = "B";
            ld.vch_achd = model.ac_hd;
            //if(ld.vch_dt)
            ld.vch_dt = Convert.ToDateTime(Convert.ToDateTime(model.vch_date).ToString("dd/MM/yyyy").Replace("-", "/"));
            ld.prin_amt = Convert.ToDecimal(model.prin_amt);
            ld.prin_bal = Convert.ToDecimal(model.prin_amt);
            ld.modified_by = lm.created_by;
            ld.created_by = lm.created_by;
            model.msg = ld.SaveLoanLedger(ld);
            Ledger ldd = new Ledger();
            ldd.ResetPrinBalIntDueForLoanLedgerfor_vch_entry("LOAN_LEDGER", model.ac_hd, model.emp_id, ld.vch_dt, ld.vch_no);
            return Json(model.msg);
        }
        public JsonResult getdetailsbyMemberId(string mem_id)
        {
            Member_Mast mm = new Member_Mast();
            LoanMasterEntryViewModel model = new LoanMasterEntryViewModel();
            mm = mm.getdetailsbymemberid(mem_id);
            model.mem_dt = mm.mem_date.ToString("dd-MM-yyyy").Replace("-", "/");
            model.loanee_name = mm.mem_name.ToUpper();
            model.book_no = mm.book_no;
            model.mem_type = mm.member_type.ToUpper();
            model.mem_cat = mm.member_category.ToUpper();
            model.gurdian_name = mm.guardian_name.ToUpper();
            return Json(model);
        }
        public JsonResult getdetailsbyEmpId(string branch_id, string ac_hd, string emp_id)
        {
            Member_Mast mm = new Member_Mast();
            LoanMasterEntryViewModel model = new LoanMasterEntryViewModel();
            mm = mm.getmemidbyempid(emp_id);
            model.mem_id = mm.mem_id;
            model.msg = mm.msg;
            Loan_Master lm = new Loan_Master();
            List<Loan_Master> lml = new List<Loan_Master>();
            lml = lm.getmemdetails(branch_id, ac_hd, emp_id);
            int i = 1;
            if (lml.Count > 0)
            {
                foreach (var a in lml)
                {
                    if (i == 1)
                    {
                        if (a.clos_flag == "C")
                        {
                            model.tableelement = "<tr><th>Srl</th></th><th>Loan Type</th><th>Loan Amount</th><th>Loan Date</th><th>Mode Of Loan</th></tr>";
                            model.tableelement = model.tableelement + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.ac_hd + "</td><td>" + a.loan_amt.ToString("0.00") + "</td><td>" + a.loan_dt.ToString("dd-MM-yyyy").Replace("-", "/") + "</td><td>" + "Closed" + "</td></tr>";
                        }
                        else
                        {
                            model.tableelement = "<tr><th>Srl</th></th><th>Loan Type</th><th>Loan Amount</th><th>Loan Date</th><th>Mode Of Loan</th></tr>";
                            model.tableelement = model.tableelement + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.ac_hd + "</td><td>" + a.loan_amt.ToString("0.00") + "</td><td>" + a.loan_dt.ToString("dd-MM-yyyy").Replace("-", "/") + "</td><td>" + "" + "</td></tr>";
                        }
                    }
                    else
                    {
                        if (a.clos_flag == "C")
                        {
                            model.tableelement = model.tableelement + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.ac_hd + "</td><td>" + a.loan_amt.ToString("0.00") + "</td><td>" + a.loan_dt.ToString("dd-MM-yyyy").Replace("-", "/") + "</td><td>" + "Closed" + "</td></tr>";
                        }
                        else
                        {
                            model.tableelement = model.tableelement + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.ac_hd + "</td><td>" + a.loan_amt.ToString("0.00") + "</td><td>" + a.loan_dt.ToString("dd-MM-yyyy").Replace("-", "/") + "</td><td>" + "" + "</td></tr>";
                        }
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
        public JsonResult getloandetailsbyLoanDate(string branch_id, string ac_hd, string emp_id, string loan_dt)
        {
            Loan_Master lm = new Loan_Master();
            LoanMasterEntryViewModel model = new LoanMasterEntryViewModel();
            lm = lm.getdetailsbyloandate(branch_id, ac_hd, emp_id, loan_dt);
            if (lm.msg == "Details Found")
            {
                model.status_cd = lm.ln_spcl;
                model.loan_amt = lm.loan_amt.ToString("0.00");
                model.inst_amt = lm.inst_amt.ToString("0.00");
                model.inst_no = lm.inst_no.ToString();
                model.inst_rate = lm.inst_rate.ToString("0.00");
                model.ln_purpose = lm.ln_purpose;
                model.lic_premium = lm.lic_premium;
                //model.vch_no = lm.vch_no;
                //model.vch_date = lm.vch_dt.ToString("dd-MM-yyyy").Replace("-", "/");
                model.msg = lm.msg;
            }
            else
            {
                model.inst_rate = lm.inst_rate.ToString("0.00");
                model.msg = lm.msg;
            }
            return Json(model);
        }
        public JsonResult getinstallmentno(LoanMasterEntryViewModel model)
        {
            int inst_no = 0;
            decimal loan_amt = 0;
            decimal inst_amt = 0;
            if (model.loan_amt == "")
            {
                loan_amt = 0;
            }
            else
            {
                loan_amt = Convert.ToDecimal(model.loan_amt);
            }
            if (model.inst_amt == "")
            {
                inst_amt = 0;
            }
            else
            {
                inst_amt = Convert.ToDecimal(model.inst_amt);
            }
            model.inst_no = Math.Round(loan_amt / inst_amt).ToString();
            return Json(model);
        }
        public JsonResult getloanledgerdetailsbyVchNo(string branch_id, string ac_hd, string emp_id, string vch_date, string vch_no)
        {
            Loan_Ledger ld = new Loan_Ledger();
            LoanMasterEntryViewModel model = new LoanMasterEntryViewModel();
            ld = ld.getdetailsbyVchNo(branch_id, ac_hd, emp_id, vch_date, vch_no);
            model.prin_amt = ld.prin_amt.ToString("0.00");
            return Json(model);
        }
        public JsonResult Update(LoanMasterEntryViewModel model)
        {
            Loan_Master lm = new Loan_Master();
            lm.lic_premium = model.lic_premium.ToUpper();
            lm.loan_dt = Convert.ToDateTime(Convert.ToDateTime(model.loan_dt).ToString("dd-MM-yyyy").Replace("-", "/"));
            lm.ac_hd = model.ac_hd;
            lm.emp_id = model.emp_id;
            lm.branch_id = model.branch_id;
            model.msg = lm.updateprimium(lm);
            return Json(model.msg);
        }
        public JsonResult ClosedLoan(LoanMasterEntryViewModel model)
        {
            Loan_Master lm = new Loan_Master();
            lm.branch_id = model.branch_id;
            lm.loan_dt = Convert.ToDateTime(Convert.ToDateTime(model.loan_dt).ToString("dd-MM-yyyy").Replace("-", "/"));
            lm.ac_hd = model.ac_hd;
            lm.emp_id = model.emp_id;
            lm.clos_flag = "C";
            model.msg = lm.CloseFlag(lm);
            return Json(model.msg);
        }
        public JsonResult SaveInstChng(LoanMasterEntryViewModel model)
        {
            Loan_Master lm = new Loan_Master();
            lm.branch_id = model.branch_id.ToUpper();
            lm.ac_hd = model.ac_hd;
            lm.emp_id = model.emp_id;
            lm.loan_dt = Convert.ToDateTime(Convert.ToDateTime(model.loan_dt).ToString("dd-MM-yyyy").Replace("-", "/"));
            lm.mem_id = model.mem_id;
            lm.ln_spcl = model.status_cd;
            lm.ln_purpose = model.ln_purpose.ToUpper();
            lm.book_no = model.book_no;
            lm.loanee_name = model.loanee_name.ToUpper();
            lm.loan_amt = Convert.ToDecimal(model.loan_amt);
            lm.inst_no = Convert.ToInt32(model.inst_no);
            lm.inst_rate = Convert.ToDecimal(model.inst_rate);
            lm.inst_amt = Convert.ToDecimal(model.inst_amt);
            lm.modified_by = Convert.ToString(Session["Uid"]);
            model.msg = lm.Save(lm);
            return Json(model.msg);
        }

        /********************************************Loan Master End********************************************************/

        /********************************************Loan Ledger Statement Start*******************************************/
        [HttpGet]
        public ActionResult DisplayloanLedger(DisplayloanLedgerViewModel model)
        {
            UtilityController u = new UtilityController();
            model.BranchDesc = u.getBranchMastDetails();
            model.lntypedesc = u.getLoanTypeMastDetails();
            model.lnpurposedesc = u.getLoanPurposeMastDetails();
            model.DesignationDesc = u.getDesignationMastDetails();
            model.ServiceDesc = u.getServiceStatusMastDetails();
            model.DepartmentDesc = u.getDepartmentMastDetails();
            model.EmpBranchDesc = u.getEmployerBranchMastDetails();
            model.EmpDesc = u.getEmployerMastDetails();
            return View(model);
        }
        public JsonResult getEmployeedetailsbyEmployeeId(string emp_id, string branch_id, string loan_type)
        {
            Loan_Master lm = new Loan_Master();
            Member_Mast mm = new Member_Mast();
            DisplayloanLedgerViewModel model = new DisplayloanLedgerViewModel();
            lm = lm.getmemidbyempid(emp_id, branch_id, loan_type);
            mm = mm.getdetails(branch_id, lm.mem_id);
            model.mem_id = lm.mem_id;
            model.org_emp = mm.emp_cd;
            model.unit = mm.emp_branch;
            model.book_no = mm.book_no;
            model.dept = mm.dept;
            model.deg = mm.desig;
            model.service_sts = mm.serv_sts;
            model.join_dt = Convert.ToDateTime(mm.join_dt).ToString("dd-MM-yyyy").Replace("-", "/");
            if (Convert.ToDateTime(mm.retmnt_dt).ToString("dd-MM-yyyy").Replace("-", "/") == "01/01/0001")
            {
                model.ret_dt = "";
            }
            else
            {
                model.ret_dt = Convert.ToDateTime(mm.retmnt_dt).ToString("dd-MM-yyyy").Replace("-", "/");
            }
            if (mm.mailAdd_house == null)
            {
                model.mail_h_no = "";
            }
            else
            {
                model.mail_h_no = mm.mailAdd_house;
            }
            if (mm.mailAdd_add1 == null)
            {
                model.mail_add1 = "";
            }
            else
            {
                model.mail_add1 = mm.mailAdd_add1.ToUpper();
            }
            if (mm.mailAdd_add2 == null)
            {
                model.mail_add2 = "";
            }
            else
            {
                model.mail_add2 = mm.mailAdd_add2.ToUpper();
            }
            if (mm.mailAdd_city == null)
            {
                model.mail_city = "";
            }
            else
            {
                model.mail_city = mm.mailAdd_city.ToUpper();
            }
            if (mm.mailAdd_dist == null)
            {
                model.mail_dist = "";
            }
            else
            {
                model.mail_dist = mm.mailAdd_dist.ToUpper();
            }
            if (mm.mailAdd_state == null)
            {
                model.mail_state = "";
            }
            else
            {
                model.mail_state = mm.mailAdd_state.ToUpper();
            }
            if (mm.mailAdd_pin == null)
            {
                model.mail_pin = "";
            }
            else
            {
                model.mail_pin = mm.mailAdd_pin;
            }
            model.msg = lm.msg;
            return Json(model);
        }
        public JsonResult getmemberdetailsbymemberid(string branch_id, string mem_id)
        {
            Member_Mast mm = new Member_Mast();
            Occupation_Mast ocm = new Occupation_Mast();
            Relation_Mast rlm = new Relation_Mast();
            Caste_Mast cm = new Caste_Mast();
            DisplayloanLedgerViewModel model = new DisplayloanLedgerViewModel();
            List<Member_Mast> mml = new List<Member_Mast>();
            mml = mm.getmemberdetailsbymemid(branch_id, mem_id);
            int i = 1;
            if (mml.Count > 0)
            {
                foreach (var a in mml)
                {
                    string occname = ocm.getoccupationnamebyid(a.occupation);
                    string reln_name = rlm.getrelationnamebyid(a.member_rel);
                    string caste_name = cm.getCastenamebyid(a.caste);
                    if (i == 1)
                    {
                        if (a.ltl_app == 0)
                        {
                            model.tableelement = "<tr><th>Srl</th></th><th>Member Id</th><th>Name</th><th>Guardians Name</th><th>Relation</th><th>Sex</th><th>Caste</th><th>Occupation</th><th>Sign</th><th>LTI</th></tr>";
                            model.tableelement = model.tableelement + "<tr><td>" + Convert.ToString(i) + "</td><td>" + mem_id + "</td><td>" + a.mem_name + "</td><td>" + a.guardian_name + "</td><td>" + reln_name + "</td><td>" + a.sex + "</td><td>" + caste_name + "</td><td>" + occname + "</td><td>" + a.ltl_app + "</td><td>" + "" + "</td></tr>";
                        }
                        else
                        {
                            model.tableelement = "<tr><th>Srl</th></th><th>Member Id</th><th>Name</th><th>Guardians Name</th><th>Relation</th><th>Sex</th><th>Caste</th><th>Occupation</th><th>Sign</th><th>LTI</th></tr>";
                            model.tableelement = model.tableelement + "<tr><td>" + Convert.ToString(i) + "</td><td>" + mem_id + "</td><td>" + a.mem_name + "</td><td>" + a.guardian_name + "</td><td>" + reln_name + "</td><td>" + a.sex + "</td><td>" + caste_name + "</td><td>" + occname + "</td><td>" + "" + "</td><td>" + a.ltl_app + "</td></tr>";
                        }

                    }
                    else
                    {
                        if (a.ltl_app == 0)
                        {
                            model.tableelement = "<tr><th>Srl</th></th><th>Member Id</th><th>Name</th><th>Guardians Name</th><th>Relation</th><th>Sex</th><th>Caste</th><th>Occupation</th><th>Sign</th><th>LTI</th></tr>";
                            model.tableelement = model.tableelement + "<tr><td>" + Convert.ToString(i) + "</td><td>" + mem_id + "</td><td>" + a.mem_name + "</td><td>" + a.guardian_name + "</td><td>" + reln_name + "</td><td>" + a.sex + "</td><td>" + a.caste + "</td><td>" + occname + "</td><td>" + a.ltl_app + "</td><td>" + "" + "</td></tr>";
                        }
                        else
                        {
                            model.tableelement = "<tr><th>Srl</th></th><th>Member Id</th><th>Name</th><th>Guardians Name</th><th>Relation</th><th>Sex</th><th>Caste</th><th>Occupation</th><th>Sign</th><th>LTI</th></tr>";
                            model.tableelement = model.tableelement + "<tr><td>" + Convert.ToString(i) + "</td><td>" + mem_id + "</td><td>" + a.mem_name + "</td><td>" + a.guardian_name + "</td><td>" + reln_name + "</td><td>" + a.sex + "</td><td>" + a.caste + "</td><td>" + occname + "</td><td>" + "" + "</td><td>" + a.ltl_app + "</td></tr>";
                        }
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
        public JsonResult getloandatabybranchac_hdandempid(string emp_id, string branch_id, string loan_type)
        {
            Loan_Master lm = new Loan_Master();
            DisplayloanLedgerViewModel model = new DisplayloanLedgerViewModel();
            lm = lm.getdetailsbybranchac_hdandempid(emp_id, branch_id, loan_type);
            if (Convert.ToDateTime(lm.cheque_dt).ToString("dd-MM-yyyy").Replace("-", "/") == "01/01/0001")
            {
                model.chq_dt = "";
            }
            else
            {
                model.chq_dt = lm.cheque_dt.ToString("dd-MM-yyyy").Replace("-", "/");
            }
            model.chq_no = lm.cheque_no;
            model.chq_bank_branch_ref = lm.bank_ref;
            model.sanc_dt = lm.loan_dt.ToString("dd-MM-yyyy").Replace("-", "/");
            model.loan_amt = lm.loan_amt.ToString("0.00");
            model.inst = Convert.ToString(lm.inst_no);
            model.int_rate = Convert.ToString(lm.inst_rate);
            model.inst_amt = lm.inst_amt.ToString("0.00");
            model.loan_purpose = lm.ln_purpose;
            return Json(model);
        }
        public JsonResult getloanledgerdetailsbybranchac_hdandempid(string emp_id, string branch_id, string loan_type)
        {
            DisplayloanLedgerViewModel model = new DisplayloanLedgerViewModel();
            Loan_Ledger ld = new Loan_Ledger();
            List<Loan_Ledger> ldl = new List<Loan_Ledger>();
            ldl = ld.getmemberdetailsbymemid(emp_id, branch_id, loan_type);
            int i = 1;
            model.tableelement = "<tr><th>Date</th><th>Transaction Particulars</th><th>Cheque No.</th><th>Dr Amount</th><th>Cr Amount</th><th>Prin/Balance</th><th>Int/Balance</th><th>Aint/Bal</th><th>Oth/Chrgs</th></tr>";
            if (ldl.Count > 0)
            {
                foreach (var a in ldl)
                {
                    string tagColor = "style='color: black;'";
                    string condition = "";
                    string condition1 = "";
                    if(a.trns_particular.Length > 10)
                    {
                        condition = a.trns_particular.Substring((a.trns_particular).Length - 10);
                    }
                    //condition = a.trns_particular.Substring((a.trns_particular).Length - 10);
                    if (a.trns_particular.Length >= 28)
                    {
                        condition1 = a.trns_particular.Substring(0, 27);
                    }
                    if (a.dr_amt > 0)
                    {
                        model.tableelement = model.tableelement + "<tr><td>" + a.vch_dt.ToString("dd/MM/yyyy").Replace("-", "/") + "</td><td style='width: 100%'>" + a.trns_particular + "</td><td>" + a.chq_no + "</td><td>" + a.dr_amt.ToString("0.00") + "</td><td></td><td>" + a.prin_bal.ToString("0.00") + "</td><td>" + a.int_due.ToString("0.00") + "</td><td>" + a.aint_due.ToString("0.00") + "</td><td>" + a.ichrg_due.ToString("0.00") + "</td></tr>";
                    }
                    if (a.cr_amt > 0)
                    {
                        if (condition == "@Principal" && condition1 == "ByTransfer SALARY DEDUCTION")
                        {
                            tagColor = "style='color: Green;'";
                        }
                        model.tableelement = model.tableelement + "<tr><td>" + a.vch_dt.ToString("dd/MM/yyyy").Replace("-", "/") + "</td><td style='width: 100%'>" + a.trns_particular + "</td><td>" + a.chq_no + "</td><td></td><td " + tagColor + ">" + a.cr_amt.ToString("0.00") + "</td><td>" + a.prin_bal.ToString("0.00") + "</td><td>" + a.int_due.ToString("0.00") + "</td><td>" + a.aint_due.ToString("0.00") + "</td><td>" + a.ichrg_due.ToString("0.00") + "</td></tr>";
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
        public ActionResult LoanLedgerDeatilsPrintFile(DisplayloanLedgerViewModel model, string todt)
        {
            if ((model.fr_dt == null) || (model.fr_dt == ""))
            {
                model.fr_dt = model.sanc_dt;
            }
            if ((model.to_dt == null) || (model.to_dt == ""))
            {
                model.to_dt = DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/");
            }
            Licence lc = new Licence();
            lc = lc.getlicencedetails();
            Member_Mast mm = new Member_Mast();
            mm = mm.getdetailsbymemberid(model.mem_id);
            Loan_Ledger ld = new Loan_Ledger();
            List<Loan_Ledger> ldl = new List<Loan_Ledger>();
            ldl = ld.getmemberdetailsbymemid(model.emp_id, model.branch_id, model.loan_type);
            int i = 1;
            decimal prin_bal = 0;
            decimal int_due = 0;
            decimal aint_due = 0;
            decimal dr_amt = 0;
            decimal cr_amt = 0;
            string trns_particular = "";
            Directory.CreateDirectory(Server.MapPath("~/wwwroot\\TextFiles"));
            using (StreamWriter sw = new StreamWriter(Server.MapPath("~/wwwroot\\TextFiles\\Loan_Ledger_Statement_List.txt")))
            {
                int Pg = 1;
                int Ln = 0;
                sw.WriteLine("                                                                Run Date: " + DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/") + "   Page# " + Pg);
                sw.WriteLine("                       " + lc.lic_name);
                sw.WriteLine("                               " + lc.lic_add1 + "," + lc.lic_add2 + " PHONE : " + lc.lic_phone);
                sw.WriteLine("");
                sw.WriteLine(model.loan_type.ToUpper() + " LEDGER STATEMENT FOR THE PERIOD " + model.fr_dt + " to " + model.to_dt);
                sw.WriteLine("----------------------------------------------------------------+---------------------------------------");
                sw.WriteLine("Loan Id.   : " + "".ToString().PadLeft(17) + model.emp_id + " | Loan Date       : " + model.sanc_dt);
                sw.WriteLine("Name       : " + "".ToString().PadLeft(6) + mm.mem_name + " | Loan Amount     : " + model.loan_amt);
                sw.WriteLine("Address    : " + "".ToString().PadLeft(6) + model.mail_add1 + " | Instalments     : " + model.inst);
                sw.WriteLine("             " + "".ToString().PadLeft(6) + model.mail_add2 + " " + model.mail_city + " " + model.mail_dist + " " + model.mail_state + " " + model.mail_pin + " | Instl.Amount    : " + "".ToString().PadLeft(9 - (model.inst_amt).ToString().Length) + model.inst_amt);
                sw.WriteLine("Member No  : " + "".ToString().PadLeft(6) + model.mem_id + " | To Repay within : ");
                sw.WriteLine("----------+---------------------------------+----------+----------+----------+------------+---------+----------");
                sw.WriteLine("Date      |Transaction Particulars          |Cheque No.|Debit Amt |Credit Amt|Prin Balance|Int.Bal. |AInt.Bal");
                sw.WriteLine("----------+---------------------------------+----------+----------+----------+------------+---------+----------");
                foreach (var am in ldl)
                {
                    if (am.prin_bal.ToString().Length > 12)
                    {
                        prin_bal = Convert.ToDecimal(Convert.ToString(am.prin_bal).Substring(0, 9));
                    }
                    else
                    {
                        prin_bal = am.prin_bal;
                    }
                    if (am.int_due.ToString().Length > 9)
                    {
                        int_due = Convert.ToDecimal(Convert.ToString(am.int_due).Substring(0, 8));
                    }
                    else
                    {
                        int_due = am.int_due;
                    }
                    if (am.aint_due.ToString().Length > 9)
                    {
                        aint_due = Convert.ToDecimal(Convert.ToString(am.aint_due).Substring(0, 8));
                    }
                    else
                    {
                        aint_due = am.aint_due;
                    }
                    if (am.dr_amt.ToString().Length > 10)
                    {
                        dr_amt = Convert.ToDecimal(Convert.ToString(am.dr_amt).Substring(0, 9));
                    }
                    else
                    {
                        dr_amt = am.dr_amt;
                    }
                    if (am.cr_amt.ToString().Length > 10)
                    {
                        cr_amt = Convert.ToDecimal(Convert.ToString(am.cr_amt).Substring(0, 9));
                    }
                    else
                    {
                        cr_amt = am.cr_amt;
                    }
                    if (am.trns_particular.ToString().Length > 33)
                    {
                        trns_particular = (am.trns_particular).Substring(0, 32);
                    }
                    else
                    {
                        trns_particular = am.trns_particular;
                    }
                    if (Ln > Pg * 62)
                    {
                        Pg = Pg + 1;
                        Ln = Ln + 7;
                        sw.WriteLine("----------+---------------------------------+----------+----------+----------+------------+---------+----------");
                        sw.WriteLine("                                                                Run Date: " + DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/") + "   Page# " + Pg);
                        sw.WriteLine("                       " + lc.lic_name);
                        sw.WriteLine("                               " + lc.lic_add1 + "," + lc.lic_add2 + " PHONE : " + lc.lic_phone);
                        sw.WriteLine("");
                        sw.WriteLine(model.loan_type.ToUpper() + " LEDGER STATEMENT FOR THE PERIOD " + model.fr_dt + " to " + model.to_dt);
                        sw.WriteLine("----------------------------------------------------------------+---------------------------------------");
                        sw.WriteLine("Loan Id.   : " + "".ToString().PadLeft(17) + model.emp_id + " | Loan Date       : " + model.sanc_dt);
                        sw.WriteLine("Name       : " + "".ToString().PadLeft(6) + mm.mem_name + " | Loan Amount     : " + model.loan_amt);
                        sw.WriteLine("Address    : " + "".ToString().PadLeft(6) + model.mail_add1 + " | Instalments     : " + model.inst);
                        sw.WriteLine("             " + "".ToString().PadLeft(6) + model.mail_add2 + " " + model.mail_city + " " + model.mail_dist + " " + model.mail_state + " " + model.mail_pin + " | Instl.Amount    : " + "".ToString().PadLeft(9 - (model.inst_amt).ToString().Length) + model.inst_amt);
                        sw.WriteLine("Member No  : " + "".ToString().PadLeft(6) + model.mem_id + " | To Repay within : ");
                        sw.WriteLine("----------+---------------------------------+----------+----------+----------+------------+---------+----------");
                        sw.WriteLine("Date      |Transaction Particulars          |Cheque No.|Debit Amt |Credit Amt|Prin Balance|Int.Bal. |AInt.Bal");
                        sw.WriteLine("----------+---------------------------------+----------+----------+----------+------------+---------+----------");
                        sw.WriteLine("");
                    }
                    if (am.vch_dt >= Convert.ToDateTime(model.fr_dt) && am.vch_dt <= Convert.ToDateTime(model.to_dt))
                    {
                        sw.WriteLine("".ToString().PadLeft(10 - ("").Length) + "".ToString() + "|"
                          + "".ToString().PadLeft(33 - ("By Balance B/F").ToString().Length) + "By Balance B/F" + "|"
                          + "".ToString().PadLeft(10 - ("").ToString().Length) + "".ToString() + "|"
                           + "".ToString().PadLeft(10 - ("").ToString().Length) + "".ToString() + "|"
                            + "".ToString().PadLeft(10 - ("").ToString().Length) + "".ToString() + "|"
                             + "".ToString().PadLeft(9 - (prin_bal).ToString().Length) + prin_bal.ToString("0.00") + "|"
                              + "".ToString().PadLeft(6 - (int_due).ToString().Length) + int_due.ToString("0.00") + "|"
                              + "".ToString().PadLeft(6 - (aint_due).ToString().Length) + aint_due.ToString("0.00") + "|");
                    }
                    else
                    {
                        if (am.dr_amt > 0)
                        {
                            sw.WriteLine("".ToString().PadLeft(10 - (am.vch_dt).ToString("dd/MM/yyyy").Replace("-", "/").Length) + (am.vch_dt).ToString("dd/MM/yyyy").Replace("-", "/").ToString() + "|"
                                + "".ToString().PadLeft(33 - (trns_particular).ToString().Length) + trns_particular + "|"
                                + "".ToString().PadLeft(10 - (am.chq_no).ToString().Length) + am.chq_no.ToString() + "|"
                               + "".ToString().PadLeft(7 - (dr_amt).ToString().Length) + dr_amt.ToString("0.00") + "|"
                               + "".ToString().PadLeft(10 - ("").ToString().Length) + "".ToString() + "|"
                                 + "".ToString().PadLeft(9 - (prin_bal).ToString().Length) + prin_bal.ToString("0.00") + "|"
                                  + "".ToString().PadLeft(6 - (int_due).ToString().Length) + int_due.ToString("0.00") + "|"
                                  + "".ToString().PadLeft(6 - (aint_due).ToString().Length) + aint_due.ToString("0.00") + "|");
                        }
                        if (am.cr_amt > 0)
                        {
                            sw.WriteLine("".ToString().PadLeft(10 - (am.vch_dt).ToString("dd/MM/yyyy").Replace("-", "/").Length) + (am.vch_dt).ToString("dd/MM/yyyy").Replace("-", "/").ToString() + "|"
                                 + "".ToString().PadLeft(33 - (trns_particular).ToString().Length) + trns_particular + "|"
                                 + "".ToString().PadLeft(10 - (am.chq_no).ToString().Length) + am.chq_no.ToString() + "|"
                                 + "".ToString().PadLeft(10 - ("").ToString().Length) + "".ToString() + "|"
                                + "".ToString().PadLeft(7 - (cr_amt).ToString().Length) + cr_amt.ToString("0.00") + "|"
                                  + "".ToString().PadLeft(9 - (prin_bal).ToString().Length) + prin_bal.ToString("0.00") + "|"
                                   + "".ToString().PadLeft(6 - (int_due).ToString().Length) + int_due.ToString("0.00") + "|"
                                   + "".ToString().PadLeft(6 - (aint_due).ToString().Length) + aint_due.ToString("0.00") + "|");
                        }
                    }
                    Ln = Ln + 1;
                    i = i + 1;
                }
            }
            UtilityController u = new UtilityController();
            var memory = u.DownloadTextFiles("Loan_Ledger_Statement_List.txt", Server.MapPath("~/wwwroot\\TextFiles"));
            if (System.IO.File.Exists(Server.MapPath("~/wwwroot\\TextFiles\\Loan_Ledger_Statement_List.txt")))
            {
                System.IO.File.Delete(Server.MapPath("~/wwwroot\\TextFiles\\Loan_Ledger_Statement_List.txt"));
            }
            return File(memory.ToArray(), "text/plain", "Loan_Ledger_Statement_List_" + DateTime.Now.ToShortDateString().Replace(" / ", "_") + ".txt");
        }
        /********************************************Loan Ledger Statement End*******************************************/

        /********************************************Loan Opening Closing List Start*******************************************/
        [HttpGet]
        public ActionResult NewLoanAndLoanClosingReg(MemberOpeningandClosingRegViewModel model)
        {
            UtilityController u = new UtilityController();
            model.BranchDesc = u.getBranchMastDetails();
            model.lntypedesc = u.getLoanTypeMastDetails();
            model.fr_dt = DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/");
            model.to_dt = DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/");
            return View(model);
        }
        public JsonResult getloanopeningclosinglist(MemberOpeningandClosingRegViewModel model)
        {
            Loan_Master lm = new Loan_Master();
            List<Loan_Master> lml = new List<Loan_Master>();
            lml = lm.getdetails(model.searchtype, model.branch_id, model.ac_hd, model.fr_dt, model.to_dt);
            string fd = string.Empty;
            int i = 1;
            decimal xtopprin = 0;
            decimal xcllnamt = 0;
            decimal xtclprin = 0;

            if (model.searchtype == "New Loan")
            {
                if (lml.Count > 0)
                {
                    foreach (var a in lml)
                    {
                        if (i == 1)
                        {
                            model.tableelement = "<tr><th>SR No</th><th>Loan Type</th><th>Employee Id</th><th>Loan Date</th><th>Loanee Name</th><th>Loan Amount</th><th>No.Of.Inst</th><th>Inst Amount</th></tr>";
                            model.tableelement = model.tableelement + "<tr><td>" + Convert.ToString(i) + "</td><td>" + model.ac_hd + "</td><td>" + a.emp_id + "</td><td>" + a.loan_dt.ToString("dd-MM-yyyy").Replace("-", "/") + "</td><td>" + a.loanee_name + "</td><td>" + a.loan_amt.ToString("0.00") + "</td><td>" + a.inst_no + "</td><td>" + a.inst_amt.ToString("0.00") + "</td></tr>";
                            xtopprin = (xtopprin + a.loan_amt);
                        }
                        else
                        {
                            model.tableelement = model.tableelement + "<tr><td>" + Convert.ToString(i) + "</td><td>" + model.ac_hd + "</td><td>" + a.emp_id + "</td><td>" + a.loan_dt.ToString("dd-MM-yyyy").Replace("-", "/") + "</td><td>" + a.loanee_name + "</td><td>" + a.loan_amt.ToString("0.00") + "</td><td>" + a.inst_no + "</td><td>" + a.inst_amt.ToString("0.00") + "</td></tr>";
                            xtopprin = (xtopprin + a.loan_amt);
                        }
                        i = i + 1;
                    }
                    model.tot_xtopprin = xtopprin.ToString("0.00");
                }
                else
                {
                    model.tableelement = null;
                }
            }
            else
            {
                if (lml.Count > 0)
                {
                    foreach (var a in lml)
                    {
                        if (i == 1)
                        {
                            model.tableelement = "<tr><th>SR No</th><th>Loan Type</th><th>Employee Id</th><th>Loan Date</th><th>Close Date</th><th>Loanee Name</th><th>Loan Amount</th><th>Cl/PrinAmount</th></tr>";
                            model.tableelement = model.tableelement + "<tr><td>" + Convert.ToString(i) + "</td><td>" + model.ac_hd + "</td><td>" + a.emp_id + "</td><td>" + a.loan_dt.ToString("dd-MM-yyyy").Replace("-", "/") + "</td><td>" + a.clos_dt.ToString("dd-MM-yyyy").Replace("-", "/") + "</td><td>" + a.loanee_name + "</td><td>" + a.loan_amt.ToString("0.00") + "</td><td>" + a.prin_amt.ToString("0.00") + "</td></tr>";
                            xcllnamt = (xcllnamt + a.loan_amt);
                            xtclprin = (xtclprin + a.prin_amt);
                        }
                        else
                        {
                            model.tableelement = model.tableelement + "<tr><td>" + Convert.ToString(i) + "</td><td>" + model.ac_hd + "</td><td>" + a.emp_id + "</td><td>" + a.loan_dt.ToString("dd-MM-yyyy").Replace("-", "/") + "</td><td>" + a.clos_dt.ToString("dd-MM-yyyy").Replace("-", "/") + "</td><td>" + a.loanee_name + "</td><td>" + a.loan_amt.ToString("0.00") + "</td><td>" + a.prin_amt.ToString("0.00") + "</td></tr>";
                            xcllnamt = (xcllnamt + a.loan_amt);
                            xtclprin = (xtclprin + a.prin_amt);
                        }
                        i = i + 1;
                    }
                    model.tot_cl_loan_amt = xcllnamt.ToString("0.00");
                    model.tot_cl_prin_amt = xtclprin.ToString("0.00");
                }
                else
                {
                    model.tableelement = null;
                }
            }
            return Json(model);
        }
        public ActionResult LoanOpeningClosingListPrintFile(MemberOpeningandClosingRegViewModel model)
        {
            decimal inst_amt = 0;
            decimal loan_amt = 0;
            Licence lc = new Licence();
            lc = lc.getlicencedetails();
            Loan_Master lm = new Loan_Master();
            List<Loan_Master> lml = new List<Loan_Master>();
            lml = lm.getdetails(model.searchtype, model.branch_id, model.ac_hd, model.fr_dt, model.to_dt);
            Directory.CreateDirectory(Server.MapPath("~/wwwroot\\TextFiles"));
            if (model.searchtype == "New Loan")
            {
                if (model.ac_hd == "" || model.ac_hd == null)
                {
                    using (StreamWriter sw = new StreamWriter(Server.MapPath("~/wwwroot\\TextFiles\\Loan_Opening_Closing_List.txt")))
                    {
                        int Pg = 1;
                        int Ln = 0;
                        int i = 1;
                        string serial = string.Empty;
                        sw.WriteLine("                       " + lc.lic_name);
                        sw.WriteLine("                               " + lc.lic_add1 + "," + lc.lic_add2 + " PHONE : " + lc.lic_phone);
                        sw.WriteLine("");
                        sw.WriteLine("                        LOAN OPENING LIST " + model.fr_dt + " TO " + model.to_dt);
                        sw.WriteLine("===========================================================================================================");
                        sw.WriteLine("Srl. |Loan Type|Employee Id| Loan Date |Loanee Name                     |Loan Amount|No of Inst|Inst Amount");
                        sw.WriteLine("===========================================================================================================");
                        foreach (var am in lml)
                        {
                            if (Convert.ToString(i).Length > 5)
                            {
                                serial = Convert.ToString(i).Substring(0, 4);
                            }
                            else
                            {
                                serial = Convert.ToString(i);
                            }
                            if (am.inst_amt.ToString().Length > 11)
                            {
                                inst_amt = Convert.ToDecimal(Convert.ToString(am.inst_amt).Substring(0, 10));
                            }
                            else
                            {
                                inst_amt = am.inst_amt;
                            }
                            if (am.loan_amt.ToString().Length > 11)
                            {
                                loan_amt = Convert.ToDecimal(Convert.ToString(am.loan_amt).Substring(0, 10));
                            }
                            else
                            {
                                loan_amt = am.loan_amt;
                            }
                            if (Ln > Pg * 65)
                            {
                                Pg = Pg + 1;
                                Ln = Ln + 7;
                                sw.WriteLine("");
                                sw.WriteLine("                       " + lc.lic_name);
                                sw.WriteLine("                               " + lc.lic_add1 + "," + lc.lic_add2 + " PHONE : " + lc.lic_phone);
                                sw.WriteLine("");
                                sw.WriteLine("                        LOAN OPENING LIST " + model.fr_dt + " TO " + model.to_dt);
                                sw.WriteLine("===========================================================================================================");
                                sw.WriteLine("Srl. |Loan Type|Employee Id| Loan Date |Loanee Name                     |Loan Amount|No of Inst|Inst Amount");
                                sw.WriteLine("===========================================================================================================");
                            }
                            // sw.WriteLine(serial + model.ac_hd.ToString().PadLeft(6) + "|" + am.emp_id.ToString().PadLeft(13) + "|" + am.loan_dt.ToString("dd-MM-yyyy").Replace("-", "/").ToString().PadLeft(12) + "|" + am.loanee_name.ToString().PadLeft(21) + "|" + am.loan_amt.ToString("0.00").ToString().PadLeft(23) + "|" + am.inst_no.ToString().PadLeft(7) + "|" + am.inst_amt.ToString("0.00").PadLeft(12));

                            sw.WriteLine("".ToString().PadLeft(5 - (serial).ToString().Length) + serial + "|" + "".ToString().PadLeft(9 - ("").ToString().Length) + "" + "|"
                                + "".ToString().PadLeft(11 - (am.emp_id).ToString().Length) + am.emp_id + "|"
                                + "".ToString().PadLeft(11 - (am.loan_dt).ToString("dd-MM-yyyy").Replace("-", "/").Length) + am.loan_dt.ToString("dd-MM-yyyy").Replace("-", "/") + "|"
                                + "".ToString().PadLeft(32 - (am.loanee_name).ToString().Length) + am.loanee_name + "|"
                                + "".ToString().PadLeft(8 - (loan_amt).ToString().Length) + loan_amt.ToString("0.00") + "|"
                                + "".ToString().PadLeft(10 - (am.inst_no).ToString().Length) + am.inst_no + "|"
                                + "".ToString().PadLeft(14 - (inst_amt).ToString().Length) + inst_amt.ToString("0.00") + "|");
                            Ln = Ln + 1;
                            i = i + 1;
                        }
                    }
                }
                else
                {
                    using (StreamWriter sw = new StreamWriter(Server.MapPath("~/wwwroot\\TextFiles\\Loan_Opening_Closing_List.txt")))
                    {
                        int Pg = 1;
                        int Ln = 0;
                        int i = 1;
                        string serial = string.Empty;
                        sw.WriteLine("                       " + lc.lic_name);
                        sw.WriteLine("                               " + lc.lic_add1 + "," + lc.lic_add2 + " PHONE : " + lc.lic_phone);
                        sw.WriteLine("");
                        sw.WriteLine("                        LOAN OPENING LIST " + model.fr_dt + " TO " + model.to_dt);
                        sw.WriteLine("===========================================================================================================");
                        sw.WriteLine("Srl. |Loan Type|Employee Id| Loan Date |Loanee Name                     |Loan Amount|No of Inst|Inst Amount");
                        sw.WriteLine("===========================================================================================================");
                        foreach (var am in lml)
                        {
                            if (Convert.ToString(i).Length > 5)
                            {
                                serial = Convert.ToString(i).Substring(0, 4);
                            }
                            else
                            {
                                serial = Convert.ToString(i);
                            }
                            if (am.inst_amt.ToString().Length > 11)
                            {
                                inst_amt = Convert.ToDecimal(Convert.ToString(am.inst_amt).Substring(0, 10));
                            }
                            else
                            {
                                inst_amt = am.inst_amt;
                            }
                            if (am.loan_amt.ToString().Length > 11)
                            {
                                loan_amt = Convert.ToDecimal(Convert.ToString(am.loan_amt).Substring(0, 10));
                            }
                            else
                            {
                                loan_amt = am.loan_amt;
                            }
                            if (Ln > Pg * 65)
                            {
                                Pg = Pg + 1;
                                Ln = Ln + 7;
                                sw.WriteLine("");
                                sw.WriteLine("                       " + lc.lic_name);
                                sw.WriteLine("                               " + lc.lic_add1 + "," + lc.lic_add2 + " PHONE : " + lc.lic_phone);
                                sw.WriteLine("");
                                sw.WriteLine("                        LOAN OPENING LIST " + model.fr_dt + " TO " + model.to_dt);
                                sw.WriteLine("===========================================================================================================");
                                sw.WriteLine("Srl. |Loan Type|Employee Id| Loan Date |Loanee Name                     |Loan Amount|No of Inst|Inst Amount");
                                sw.WriteLine("===========================================================================================================");
                            }
                            // sw.WriteLine(serial + model.ac_hd.ToString().PadLeft(6) + "|" + am.emp_id.ToString().PadLeft(13) + "|" + am.loan_dt.ToString("dd-MM-yyyy").Replace("-", "/").ToString().PadLeft(12) + "|" + am.loanee_name.ToString().PadLeft(21) + "|" + am.loan_amt.ToString("0.00").ToString().PadLeft(23) + "|" + am.inst_no.ToString().PadLeft(7) + "|" + am.inst_amt.ToString("0.00").PadLeft(12));

                            sw.WriteLine("".ToString().PadLeft(5 - (serial).ToString().Length) + serial + "|" + "".ToString().PadLeft(9 - (model.ac_hd).ToString().Length) + model.ac_hd + "|"
                                + "".ToString().PadLeft(11 - (am.emp_id).ToString().Length) + am.emp_id + "|"
                                + "".ToString().PadLeft(11 - (am.loan_dt).ToString("dd-MM-yyyy").Replace("-", "/").Length) + am.loan_dt.ToString("dd-MM-yyyy").Replace("-", "/") + "|"
                                + "".ToString().PadLeft(32 - (am.loanee_name).ToString().Length) + am.loanee_name + "|"
                                + "".ToString().PadLeft(8 - (loan_amt).ToString().Length) + loan_amt.ToString("0.00") + "|"
                                + "".ToString().PadLeft(10 - (am.inst_no).ToString().Length) + am.inst_no + "|"
                                + "".ToString().PadLeft(14 - (inst_amt).ToString().Length) + inst_amt.ToString("0.00") + "|");
                            Ln = Ln + 1;
                            i = i + 1;
                        }
                    }
                }
            }
            else
            {
                if (model.ac_hd == "" || model.ac_hd == null)
                {
                    using (StreamWriter sw = new StreamWriter(Server.MapPath("~/wwwroot\\TextFiles\\Loan_Opening_Closing_List.txt")))
                    {
                        int Pg = 1;
                        int Ln = 0;
                        int i = 1;
                        string serial = string.Empty;
                        sw.WriteLine("                       " + lc.lic_name);
                        sw.WriteLine("                               " + lc.lic_add1 + "," + lc.lic_add2 + " PHONE : " + lc.lic_phone);
                        sw.WriteLine("");
                        sw.WriteLine("                        LOAN CLOSING LIST " + model.fr_dt + " TO " + model.to_dt);
                        sw.WriteLine("================================================================================================");
                        sw.WriteLine("Srl. |Loan Type|Employee Id| Loan Date |Loanee Name                     |Loan Amount|Close Date");
                        sw.WriteLine("================================================================================================");
                        foreach (var am in lml)
                        {
                            if (Convert.ToString(i).Length > 5)
                            {
                                serial = Convert.ToString(i).Substring(0, 4);
                            }
                            else
                            {
                                serial = Convert.ToString(i);
                            }
                            if (am.inst_amt.ToString().Length > 11)
                            {
                                inst_amt = Convert.ToDecimal(Convert.ToString(am.inst_amt).Substring(0, 10));
                            }
                            else
                            {
                                inst_amt = am.inst_amt;
                            }
                            if (am.loan_amt.ToString().Length > 11)
                            {
                                loan_amt = Convert.ToDecimal(Convert.ToString(am.loan_amt).Substring(0, 10));
                            }
                            else
                            {
                                loan_amt = am.loan_amt;
                            }
                            if (Ln > Pg * 65)
                            {
                                Pg = Pg + 1;
                                Ln = Ln + 7;
                                sw.WriteLine("");
                                sw.WriteLine("                       " + lc.lic_name);
                                sw.WriteLine("                               " + lc.lic_add1 + "," + lc.lic_add2 + " PHONE : " + lc.lic_phone);
                                sw.WriteLine("");
                                sw.WriteLine("                        LOAN CLOSING LIST " + model.fr_dt + " TO " + model.to_dt);
                                sw.WriteLine("================================================================================================");
                                sw.WriteLine("Srl. |Loan Type|Employee Id| Loan Date |Loanee Name                     |Loan Amount|Close Date");
                                sw.WriteLine("================================================================================================");
                            }
                            //sw.WriteLine(serial + model.ac_hd.ToString().PadLeft(6) + "|" + am.emp_id.ToString().PadLeft(13) + "|" + am.loan_dt.ToString("dd-MM-yyyy").Replace("-", "/").ToString().PadLeft(14) + "|" + am.loanee_name.ToString().PadLeft(21) + "|" + am.loan_amt.ToString("0.00").ToString().PadLeft(21) + "|" + am.clos_dt.ToString("dd-MM-yyyy").Replace("-", "/").PadLeft(12));
                            sw.WriteLine("".ToString().PadLeft(5 - (serial).ToString().Length) + serial + "|" + "".ToString().PadLeft(9 - ("").ToString().Length) + "" + "|"
                                + "".ToString().PadLeft(11 - (am.emp_id).ToString().Length) + am.emp_id + "|"
                                + "".ToString().PadLeft(11 - (am.loan_dt).ToString("dd-MM-yyyy").Replace("-", "/").Length) + am.loan_dt.ToString("dd-MM-yyyy").Replace("-", "/") + "|"
                                + "".ToString().PadLeft(32 - (am.loanee_name).ToString().Length) + am.loanee_name + "|"
                                + "".ToString().PadLeft(8 - (loan_amt).ToString().Length) + loan_amt.ToString("0.00") + "|"
                                + "".ToString().PadLeft(11 - (am.clos_dt).ToString("dd-MM-yyyy").Replace("-", "/").Length) + am.clos_dt.ToString("dd-MM-yyyy").Replace("-", "/") + "|");
                            //+ "".ToString().PadLeft(11 - (inst_amt).ToString().Length) + inst_amt.ToString("0.00") + "|");
                            Ln = Ln + 1;
                            i = i + 1;
                        }
                    }
                }
                else
                {
                    using (StreamWriter sw = new StreamWriter(Server.MapPath("~/wwwroot\\TextFiles\\Loan_Opening_Closing_List.txt")))
                    {
                        int Pg = 1;
                        int Ln = 0;
                        int i = 1;
                        string serial = string.Empty;
                        sw.WriteLine("                       " + lc.lic_name);
                        sw.WriteLine("                               " + lc.lic_add1 + "," + lc.lic_add2 + " PHONE : " + lc.lic_phone);
                        sw.WriteLine("");
                        sw.WriteLine("                        LOAN CLOSING LIST " + model.fr_dt + " TO " + model.to_dt);
                        sw.WriteLine("================================================================================================");
                        sw.WriteLine("Srl. |Loan Type|Employee Id| Loan Date |Loanee Name                     |Loan Amount|Close Date");
                        sw.WriteLine("================================================================================================");
                        foreach (var am in lml)
                        {
                            if (Convert.ToString(i).Length > 5)
                            {
                                serial = Convert.ToString(i).Substring(0, 4);
                            }
                            else
                            {
                                serial = Convert.ToString(i);
                            }
                            if (am.inst_amt.ToString().Length > 11)
                            {
                                inst_amt = Convert.ToDecimal(Convert.ToString(am.inst_amt).Substring(0, 10));
                            }
                            else
                            {
                                inst_amt = am.inst_amt;
                            }
                            if (am.loan_amt.ToString().Length > 11)
                            {
                                loan_amt = Convert.ToDecimal(Convert.ToString(am.loan_amt).Substring(0, 10));
                            }
                            else
                            {
                                loan_amt = am.loan_amt;
                            }
                            if (Ln > Pg * 65)
                            {
                                Pg = Pg + 1;
                                Ln = Ln + 7;
                                sw.WriteLine("");
                                sw.WriteLine("                       " + lc.lic_name);
                                sw.WriteLine("                               " + lc.lic_add1 + "," + lc.lic_add2 + " PHONE : " + lc.lic_phone);
                                sw.WriteLine("");
                                sw.WriteLine("                        LOAN CLOSING LIST " + model.fr_dt + " TO " + model.to_dt);
                                sw.WriteLine("================================================================================================");
                                sw.WriteLine("Srl. |Loan Type|Employee Id| Loan Date |Loanee Name                     |Loan Amount|Close Date");
                                sw.WriteLine("================================================================================================");
                            }
                            //sw.WriteLine(serial + model.ac_hd.ToString().PadLeft(6) + "|" + am.emp_id.ToString().PadLeft(13) + "|" + am.loan_dt.ToString("dd-MM-yyyy").Replace("-", "/").ToString().PadLeft(14) + "|" + am.loanee_name.ToString().PadLeft(21) + "|" + am.loan_amt.ToString("0.00").ToString().PadLeft(21) + "|" + am.clos_dt.ToString("dd-MM-yyyy").Replace("-", "/").PadLeft(12));
                            sw.WriteLine("".ToString().PadLeft(5 - (serial).ToString().Length) + serial + "|" + "".ToString().PadLeft(9 - (model.ac_hd).ToString().Length) + model.ac_hd + "|"
                                + "".ToString().PadLeft(11 - (am.emp_id).ToString().Length) + am.emp_id + "|"
                                + "".ToString().PadLeft(11 - (am.loan_dt).ToString("dd-MM-yyyy").Replace("-", "/").Length) + am.loan_dt.ToString("dd-MM-yyyy").Replace("-", "/") + "|"
                                + "".ToString().PadLeft(32 - (am.loanee_name).ToString().Length) + am.loanee_name + "|"
                                + "".ToString().PadLeft(8 - (loan_amt).ToString().Length) + loan_amt.ToString("0.00") + "|"
                                + "".ToString().PadLeft(11 - (am.clos_dt).ToString("dd-MM-yyyy").Replace("-", "/").Length) + am.clos_dt.ToString("dd-MM-yyyy").Replace("-", "/") + "|");
                            //+ "".ToString().PadLeft(11 - (inst_amt).ToString().Length) + inst_amt.ToString("0.00") + "|");
                            Ln = Ln + 1;
                            i = i + 1;
                        }
                    }
                }
            }
            UtilityController u = new UtilityController();
            var memory = u.DownloadTextFiles("Loan_Opening_Closing_List.txt", Server.MapPath("~/wwwroot\\TextFiles"));
            if (System.IO.File.Exists(Server.MapPath("~/wwwroot\\TextFiles\\Loan_Opening_Closing_List.txt")))
            {
                System.IO.File.Delete(Server.MapPath("~/wwwroot\\TextFiles\\Loan_Opening_Closing_List.txt"));
            }
            return File(memory.ToArray(), "text/plain", "Loan_" + model.searchtype + "_List_" + DateTime.Now.ToShortDateString().Replace("/", "_") + ".txt");
        }
        /********************************************Loan Opening Closing List End*******************************************/

        /********************************************Loan Ledger Correction Open*******************************************/
        [HttpGet]
        public ActionResult LoanLedgerCorrection(LoanLedgerCorrectionViewModel model)
        {
            UtilityController u = new UtilityController();
            model.BranchDesc = u.getBranchMastDetails();
            model.lntypedesc = u.getLoanTypeMastDetails();
            return View(model);
        }
        public JsonResult GetloanTransactionDetailsBybranchidloantypeandemployeeid(LoanLedgerCorrectionViewModel model)
        {
            Loan_Ledger ld = new Loan_Ledger();
            List<Loan_Ledger> ldl = new List<Loan_Ledger>();
            ldl = ld.getalltransactiondetails(model.branch_id, model.ac_hd, model.emp_id);
            int i = 1;
            string chq_date = "";
            if (ldl.Count > 0)
            {
                foreach (var a in ldl)
                {
                    if (Convert.ToDateTime(a.chq_dt).ToString("dd-MM-yyyy").Replace("-", "/") == "01/01/0001")
                    {
                        chq_date = "";
                    }
                    else
                    {
                        chq_date = Convert.ToDateTime(a.chq_dt).ToString("dd-MM-yyyy").Replace("-", "/");
                    }
                    if (i == 1)
                    {
                        model.tableelement = "<tr><th>Sl</th><th>Voucher Date</th><th>Vch No</th><th>Vsr</th><th>Vtype</th><th>D/C</th><th>Prin Amt</th><th>Int Amt</th><th>Prin Balance</th><th>Int Balance</th><th>Chq No</th><th>Chq Dt</th><th>BankCD</th></tr>";
                        model.tableelement = model.tableelement + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.vch_dt + "</td><td>" + a.vch_no + "</td><td>" + a.vch_srl + "</td><td>" + a.vch_type + "</td><td>" + a.dr_cr + "</td><td>" + a.prin_amt.ToString("0.00") + "</td><td>" + a.int_amt.ToString("0.00") + "</td><td>" + a.prin_bal.ToString("0.00") + "</td><td>" + a.int_due.ToString("0.00") + "</td><td>" + a.chq_no + "</td><td>" + chq_date + "</td><td>" + a.bank_cd + "</td></tr>";
                        //model.tableelement = model.tableelement + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.vch_dt.ToString("dd/MM/yyyy HH:mm:ss").Replace("-","/") + "</td><td>" + a.vch_no + "</td><td>" + a.vch_srl + "</td><td>" + a.vch_type + "</td><td>" + a.dr_cr + "</td><td>" + a.prin_amt.ToString("0.00") + "</td><td>" + a.int_amt.ToString("0.00") + "</td><td>" + a.prin_bal.ToString("0.00") + "</td><td>" + a.int_due.ToString("0.00") + "</td><td>" + a.chq_no + "</td><td>" + chq_date + "</td><td>" + a.bank_cd + "</td></tr>";                       
                    }
                    else
                    {
                        model.tableelement = model.tableelement + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.vch_dt + "</td><td>" + a.vch_no + "</td><td>" + a.vch_srl + "</td><td>" + a.vch_type + "</td><td>" + a.dr_cr + "</td><td>" + a.prin_amt.ToString("0.00") + "</td><td>" + a.int_amt.ToString("0.00") + "</td><td>" + a.prin_bal.ToString("0.00") + "</td><td>" + a.int_due.ToString("0.00") + "</td><td>" + a.chq_no + "</td><td>" + chq_date + "</td><td>" + a.bank_cd + "</td></tr>";
                        //model.tableelement = model.tableelement + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.vch_dt.ToString("dd/MM/yyyy HH:mm:ss").Replace("-", "/") + "</td><td>" + a.vch_no + "</td><td>" + a.vch_srl + "</td><td>" + a.vch_type + "</td><td>" + a.dr_cr + "</td><td>" + a.prin_amt.ToString("0.00") + "</td><td>" + a.int_amt.ToString("0.00") + "</td><td>" + a.prin_bal.ToString("0.00") + "</td><td>" + a.int_due.ToString("0.00") + "</td><td>" + a.chq_no + "</td><td>" + chq_date + "</td><td>" + a.bank_cd + "</td></tr>";
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
        public JsonResult SaveLoanLedgerModification(LoanLedgerCorrectionViewModel model)
        {
            Loan_Ledger ld = new Loan_Ledger();
            ld.branch_id = model.branch_id;
            ld.ac_hd = model.ac_hd;
            ld.emp_id = model.emp_id;
            ld.vch_dt = Convert.ToDateTime(model.date);
            ld.vch_no = model.vch_no;
            ld.prin_amt = Convert.ToDecimal(model.prnt_amt);
            ld.prin_bal = Convert.ToDecimal(model.prnt_bal);
            ld.chq_no = model.chq_no;
            if (model.chq_dt == null)
            {
                ld.chq_dt = null;
            }
            else
            {
                ld.chq_dt = Convert.ToDateTime(model.chq_dt);
            }
            ld.vch_type = model.vch_type;
            ld.dr_cr = model.drcr;
            ld.bank_cd = model.bank_cd;
            ld.vch_srl = Convert.ToDecimal(model.vch_srl);
            ld.int_amt = Convert.ToDecimal(model.int_amt);
            ld.int_due = Convert.ToDecimal(model.int_bal);
            ld.modified_by = Convert.ToString(Session["Uid"]);
            ld.created_by = Convert.ToString(Session["Uid"]);
            model.msg = ld.checkandsaveloanledger(ld);
            Ledger ldd = new Ledger();
            ldd.ResetPrinBalIntDueForLoanLedgerfor_vch_entry("LOAN_LEDGER", model.ac_hd, model.emp_id, ld.vch_dt, ld.vch_no);
            return Json(model.msg);
        }
        public JsonResult UpdateLoanLedger(LoanLedgerCorrectionViewModel model)
        {
            Loan_Ledger ldd = new Loan_Ledger();
            ldd.ResetPrinBalIntDueForLoanLedger_LTL_STL_FES(model.branch_id.ToUpper(), model.ac_hd, model.emp_id);
            return Json("Over");
        }
        public JsonResult DeleteLedgerRecord(LoanLedgerCorrectionViewModel model)
        {
            Loan_Ledger ld = new Loan_Ledger();
            ld.DeleteRecord(model);
            return Json("Record Deleted");
        }
        /********************************************Loan Ledger Correction End*******************************************/

        /********************************************Loan Detail List Start***********************************************/
        //[HttpGet]
        //public ActionResult LoanDetailList(LoanDetailListViewModel model)
        //{
        //    UtilityController u = new UtilityController();
        //    model.BranchDesc = u.getBranchMastDetails();
        //    model.lntypedesc = u.getLoanTypeMastDetails();
        //    return View(model);
        //}
        //public JsonResult Getexistingloandetailsbybranchac_hdanddate(LoanDetailListViewModel model)
        //{
        //    Loan_Ledger ld = new Loan_Ledger();
        //    List<Loan_Ledger> ldl = new List<Loan_Ledger>();
        //    ldl = ld.getallexistingloandetails(model.branch_id, model.ac_hd, model.on_date);            
        //    string vch_date = "";
        //    int i = 1;            
        //    if (ldl.Count > 0)
        //    {
        //        foreach (var a in ldl)
        //        {
        //            ld.get_current_due(model.branch_id, model.ac_hd, model.on_date, a.emp_id, a.loan_amt, a.inst_no, a.int_rate, a.ln_spcl);
        //            if (Convert.ToDateTime(a.vch_dt).ToString("dd-MM-yyyy").Replace("-", "/") == "01/01/0001")
        //            {
        //                vch_date = "";
        //            }
        //            else
        //            {
        //                vch_date = Convert.ToDateTime(a.vch_dt).ToString("dd-MM-yyyy").Replace("-", "/");
        //            }
        //            if (i == 1)
        //            {
        //                model.tableelement = "<tr><th>Sl</th><th>EmpId</th><th>Name Of Lonee</th><th>Loan Date</th><th>Loan Amount</th><th>Instl</th><th>Instl Amt</th><th>Last/Led Dt.</th><th>Principal Balance</th><th>Int Recv</th><th>Aint Recv</th></tr>";
        //                model.tableelement = model.tableelement + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.emp_id + "</td><td>" + a.loanee_name + "</td><td>" + Convert.ToDateTime(a.loan_dt).ToString("dd-MM-yyyy").Replace("-", "/") + "</td><td>" + a.loan_amt.ToString("0.00") + "</td><td>" + a.inst_no + "</td><td>" + "" + "</td><td>" + vch_date + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td></tr>";
        //            }
        //            else
        //            {
        //                model.tableelement = model.tableelement + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.emp_id + "</td><td>" + a.loanee_name + "</td><td>" + Convert.ToDateTime(a.loan_dt).ToString("dd-MM-yyyy").Replace("-", "/") + "</td><td>" + a.loan_amt.ToString("0.00") + "</td><td>" + a.inst_no + "</td><td>" + "" + "</td><td>" + vch_date + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + "" + "</td></tr>";
        //            }
        //            i = i + 1;
        //        }
        //    }
        //    else
        //    {
        //        model.tableelement = null;
        //    }
        //    return Json(model);
        //}


        /************************************************Loan Detail List End**********************************************************/

        /********************************************Monthly Interest Posting Start***********************************************/
        [HttpGet]
        public ActionResult MonthlyInterestScheduleForLoan(MonthlyInterestScheduleForLoanViewModel model)
        {
            UtilityController u = new UtilityController();
            model.BranchDesc = u.getBranchMastDetails();
            model.lntypedesc = u.getLoanTypeMastDetails();
            model.EmpBranchDesc = u.getEmployerBranchMastDetails();
            DateTime now = DateTime.Now;
            model.sch_date = Convert.ToString(new DateTime(now.Year, now.Month, 1));
            model.sch_date = Convert.ToDateTime(model.sch_date).AddMonths(1).AddDays(-1).ToString("dd-MM-yyyy").Replace("-", "/");
            model.fr_dt = new DateTime(now.Year, now.Month, 1).ToString("dd-MM-yyyy").Replace("-", "/");
            model.to_dt = model.sch_date;
            model.vch_dt = model.sch_date;
            Member_Mast mm = new Member_Mast();
            mm.updatebooknumber();
            return View(model);
        }
        public JsonResult populateDataMonthlyInterestScheduleForLoan(MonthlyInterestScheduleForLoanViewModel model)
        {
            Loan_Master lm = new Loan_Master();
            List<Loan_Master> lmlst = new List<Loan_Master>();

            lmlst = lm.getmonthlyIntrestList(model);

            int i = 1;
            if (lmlst.Count > 0)
            {
                foreach (var a in lmlst)
                {
                    lm = lm.getInterestAmtfromRecovery_Schedule(model, a.emp_id);
                    if (i == 1)
                    {
                        model.tableelement = "<tr><th>Sl</th><th>Emp.ID</th><th>Name Of Lonee</th><th>Loan Date</th><th>Loan Amount</th><th>Instl</th><th>Principal Balance</th><th>Int.Debitable</th><th>Int.Due Balance</th></tr>";
                        model.tableelement = model.tableelement + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.emp_id + "</td><td>" + a.loanee_name + "</td><td>" + Convert.ToDateTime(a.loan_dt).ToString("dd-MM-yyyy").Replace("-", "/") + "</td><td>" + a.loan_amt.ToString("0.00") + "</td><td>" + a.inst_rate.ToString("0.00") + "</td><td>" + a.prin_amt.ToString("0.00") + "</td><td>" + lm.intdbt.ToString("0.00") + "</td><td>" + a.intdue.ToString("0.00") + "</td></tr>";
                    }
                    else
                    {
                        model.tableelement = model.tableelement + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.emp_id + "</td><td>" + a.loanee_name + "</td><td>" + Convert.ToDateTime(a.loan_dt).ToString("dd-MM-yyyy").Replace("-", "/") + "</td><td>" + a.loan_amt.ToString("0.00") + "</td><td>" + a.inst_rate.ToString("0.00") + "</td><td>" + a.prin_amt.ToString("0.00") + "</td><td>" + lm.intdbt.ToString("0.00") + "</td><td>" + a.intdue.ToString("0.00") + "</td></tr>";
                    }
                    model.prcl_bal = Convert.ToDecimal(Convert.ToDecimal(model.prcl_bal) + a.prin_amt).ToString("0.00");
                    model.inst_debt = Convert.ToDecimal(Convert.ToDecimal(model.inst_debt) + lm.intdbt).ToString("0.00");
                    //ws.Cells[string.Format("A{0}", rowstart)].Value = i;
                    //ws.Cells[string.Format("B{0}", rowstart)].Value = a.emp_id;
                    //ws.Cells[string.Format("C{0}", rowstart)].Value = a.loanee_name;
                    //ws.Cells[string.Format("D{0}", rowstart)].Value = a.loan_dt;
                    //ws.Cells[string.Format("E{0}", rowstart)].Value = a.loan_amt;
                    //ws.Cells[string.Format("F{0}", rowstart)].Value = a.inst_rate;
                    //ws.Cells[string.Format("G{0}", rowstart)].Value = a.prin_amt;
                    //ws.Cells[string.Format("H{0}", rowstart)].Value = a.intdbt;
                    //ws.Cells[string.Format("I{0}", rowstart)].Value = a.intdue;
                    //rowstart++;
                    i = i + 1;
                }
            }
            else
            {
                model.tableelement = null;
            }
            return Json(model);
        }
        public JsonResult postMonthlyIntrest(MonthlyInterestScheduleForLoanViewModel model)
        {
            Loan_Master lm = new Loan_Master();
            string MSG = lm.PostMonthlyIntrest(model);
            return Json(MSG);
        }

        /********************************************Monthly Interest Posting End***********************************************/
    }
}