using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Amritnagar.Models.Database;
using Amritnagar.Models.ViewModel;
using System.Data;
using System.IO;

namespace Amritnagar.Controllers
{
    public class SalaryDeductionController : Controller
    {
        /********************************************Fresh List Start*******************************************/
        public ActionResult FreshList(FreshListViewModel model)
        {
            UtilityController u = new UtilityController();
            model.EmpBranchDesc = u.getEmployerBranchMastDetails();
            model.percnt = Convert.ToString(25);
            return View(model);
        }
        public JsonResult getfreshlistbyempbranchbooknoanddate(FreshListViewModel model)
        {
            Recovery_Get rg = new Recovery_Get();
            List<Recovery_Get> rgl = new List<Recovery_Get>();
            rgl = rg.getdetails(model.emp_branch, model.book_no, model.rec_dt);
            int i = 1;
            if (rgl.Count > 0)
            {
                foreach (var a in rgl)
                {
                    if (i == 1)
                    {
                        model.tableelement = "<tr><th>SR No</th><th>Member Id</th><th>Employee Id</th><th>Name Of Member</th><th>A/c Hd</th><th>Prin Amount</th><th>Int Amount</th><th>Prin Bal</th></tr>";
                        model.tableelement = model.tableelement + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.mEMBER_iD + "</td><td>" + a.eMPLOYEE_iD + "</td><td>" + a.mem_name + "</td><td>" + a.aC_hD + "</td><td>" + a.pRIN_aMT.ToString("0.00") + "</td><td>" + a.iNT_aMT.ToString("0.00") + "</td><td>" + a.prin_bal.ToString("0.00") + "</td></tr>";
                    }
                    else
                    {
                        model.tableelement = model.tableelement + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.mEMBER_iD + "</td><td>" + a.eMPLOYEE_iD + "</td><td>" + a.mem_name + "</td><td>" + a.aC_hD + "</td><td>" + a.pRIN_aMT.ToString("0.00") + "</td><td>" + a.iNT_aMT.ToString("0.00") + "</td><td>" + a.prin_bal.ToString("0.00") + "</td></tr>";
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
        public JsonResult gettotalpositionlist(FreshListViewModel model)
        {
            Recovery_Get rg = new Recovery_Get();
            List<Recovery_Get> rgl = new List<Recovery_Get>();
            rgl = rg.getdetails(model.emp_branch, model.book_no, model.rec_dt);
            int j = 1;
            int i = rgl.Count;
            if (rgl.Count > 0)
            {
                foreach (var a in rgl)
                {
                    if (j == i)
                    {
                        model.tableelement = "<tr><th>Thrift Fund</th><th>RTB</th><th>SFL</th><th>INT SFL</th><th>SJL</th><th>INT SJL</th><th>PSL</th><th>INT PSL</th><th>DLL</th><th>INT DLL</th><th>SL3</th><th>ISL3</th><th>M12</th><th>MI12</th><th>M14</th><th>MI14</th><th>PSL1</th><th>IPSL1</th><th>SFL1</th><th>ISFL1</th><th>SL4</th><th>ISL4</th><th>SHARE</th><th>SJL1</th><th>ISJL1</th><th>SL6</th><th>ISL6</th><th>SL7</th><th>ISL7</th><th>LICP PREM</th></tr>";
                        model.tableelement = model.tableelement + "<tr><td>" + a.TOTTF.ToString("0.00") + "</td><td>" + a.TOTRTB.ToString("0.00") + "</td><td>" + a.TOTSFL + "</td><td>" + a.TOTISFL.ToString("0.00") + "</td><td>" + a.TOTSJL.ToString("0.00") + "</td><td>" + a.TOTISJL.ToString("0.00") + "</td><td>" + a.TOTPSL.ToString("0.00") + "</td><td>" + a.TOTIPSL.ToString("0.00") + "</td><td>" + a.TOTDLL.ToString("0.00") + "</td><td>" + a.TOTIDLL.ToString("0.00") + "</td><td>" + a.TOTSL3.ToString("0.00") + "</td><td>" + a.TOTSL3I.ToString("0.00") + "</td><td>" + a.TOTM12.ToString("0.00") + "</td><td>" + a.TOTIM12.ToString("0.00") + "</td><td>" + a.TOTM14.ToString("0.00") + "</td><td>" + a.TOTIM14.ToString("0.00") + "</td><td>" + a.TOTPSL1.ToString("0.00") + "</td><td>" + a.TOTIPSL1.ToString("0.00") + "</td><td>" + a.TOTSFL1.ToString("0.00") + "</td><td>" + a.TOTISFL1.ToString("0.00") + "</td><td>" + a.TOTSL4.ToString("0.00") + "</td><td>" + a.TOTISL4.ToString("0.00") + "</td><td>" + a.TOTSH.ToString("0.00") + "</td><td>" + a.TOTSJL.ToString("0.00") + "</td><td>" + a.TOTISJL1.ToString("0.00") + "</td><td>" + a.TOTSL6.ToString("0.00") + "</td><td>" + a.TOTISL6.ToString("0.00") + "</td><td>" + a.TOTSL7.ToString("0.00") + "</td><td>" + a.TOTISL7.ToString("0.00") + "</td><td>" + a.TOTLICP.ToString("0.00") + "</td></tr>";
                    }
                    //else
                    //{
                    //    model.tableelement = model.tableelement + "<tr><td>" + a.TOTTF.ToString("0.00") + "</td><td>" + a.TOTRTB.ToString("0.00") + "</td><td>" + a.TOTSFL + "</td><td>" + a.TOTISFL.ToString("0.00") + "</td><td>" + a.TOTSJL.ToString("0.00") + "</td><td>" + a.TOTISJL.ToString("0.00") + "</td><td>" + a.TOTPSL.ToString("0.00") + "</td><td>" + a.TOTIPSL.ToString("0.00") + "</td><td>" + a.TOTDLL.ToString("0.00") + "</td><td>" + a.TOTIDLL.ToString("0.00") + "</td><td>" + a.TOTSL3.ToString("0.00") + "</td><td>" + a.TOTSL3I.ToString("0.00") + "</td><td>" + a.TOTM12.ToString("0.00") + "</td><td>" + a.TOTIM12.ToString("0.00") + "</td><td>" + a.TOTM14.ToString("0.00") + "</td><td>" + a.TOTIM14.ToString("0.00") + "</td><td>" + a.TOTPSL1.ToString("0.00") + "</td><td>" + a.TOTIPSL1.ToString("0.00") + "</td><td>" + a.TOTSFL1.ToString("0.00") + "</td><td>" + a.TOTISFL1.ToString("0.00") + "</td><td>" + a.TOTSL4.ToString("0.00") + "</td><td>" + a.TOTISL4.ToString("0.00") + "</td><td>" + a.TOTSH.ToString("0.00") + "</td><td>" + a.TOTSJL.ToString("0.00") + "</td><td>" + a.TOTISJL1.ToString("0.00") + "</td><td>" + a.TOTSL6.ToString("0.00") + "</td><td>" + a.TOTISL6.ToString("0.00") + "</td><td>" + a.TOTSL7.ToString("0.00") + "</td><td>" + a.TOTISL7.ToString("0.00") + "</td><td>" + a.TOTLICP.ToString("0.00") + "</td></tr>";
                    //}
                    j = j + 1;
                }
            }
            else
            {
                model.tableelement = null;
            }
            return Json(model);
        }
        public JsonResult getloandetailsbydaterange(FreshListViewModel model)
        {
            Loan_Master lm = new Loan_Master();
            List<Loan_Master> lml = new List<Loan_Master>();
            lml = lm.getdetailsbydaterange(model.fr_dt, model.to_dt);
            int i = 1;
            string brirth_dt = "";
            string joining_dt = "";
            double xmdiff = 0;
            double xmdiff1 = 0;
            //double xmdiff2 = 0;
            double xmd = 0;
            double xmd1 = 0;
            double xac = 0;
            double xbase = 0;
            decimal xact = 0;
            decimal xact1 = 0;
            //string dob_date = model.dob;
            if (lml.Count > 0)
            {
                foreach (var a in lml)
                {
                    if (Convert.ToDateTime(a.birth_date).ToString("dd-MM-yyyy").Replace("-", "/") == "01/01/0001")
                    {
                        brirth_dt = "";
                    }
                    else
                    {
                        brirth_dt = Convert.ToDateTime(a.birth_date).ToString("dd-MM-yyyy").Replace("-", "/");
                    }
                    if (Convert.ToDateTime(a.joining_dt).ToString("dd-MM-yyyy").Replace("-", "/") == "01/01/0001")
                    {
                        joining_dt = "";
                    }
                    else
                    {
                        joining_dt = Convert.ToDateTime(a.joining_dt).ToString("dd-MM-yyyy").Replace("-", "/");
                    }
                    //xmdiff = (a.birth_date).Month - Convert.ToDateTime(model.dob).Month;
                    xmdiff = Convert.ToDateTime(model.dob).Subtract(a.birth_date).Days / (365.25 / 12);
                    xmdiff1 = xmdiff / 12;
                    xmd = Convert.ToDouble(Convert.ToString(xmdiff1).Substring(0, 2));
                    xmd1 = xmd * 12;
                    xac = xmdiff * xmd1;
                    if (xac > 6)
                    {
                        xbase = Math.Round(xmd) + 1;
                    }
                    else
                    {
                        xbase = Math.Round(xmdiff1);
                    }
                    xact = (a.loan_amt) * Convert.ToDecimal(model.percnt) / 100;
                    xact1 = (a.loan_amt) - xact;
                    if (i == 1)
                    {
                        model.tableelement = "<tr><th>Srl</th><th>Name Of Member</th><th>Age</th><th>Member No</th><th>Man No</th><th>Doj</th><th>DoB</th><th>Loan Amount</th><th>E Loan Dt</th><th>Figure</th></tr>";
                        model.tableelement = model.tableelement + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.loanee_name + "</td><td>" + xbase + "</td><td>" + a.mem_id + "</td><td>" + a.emp_id + "</td><td>" + joining_dt + "</td><td>" + brirth_dt + "</td><td>" + a.loan_amt.ToString("0.00") + "</td><td>" + a.loan_dt.ToString("dd-MM-yyyy").Replace("-", "/") + "</td><td>" + xact1.ToString("0.00") + "</td></tr>";
                    }
                    else
                    {
                        model.tableelement = model.tableelement + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.loanee_name + "</td><td>" + xbase + "</td><td>" + a.mem_id + "</td><td>" + a.emp_id + "</td><td>" + joining_dt + "</td><td>" + brirth_dt + "</td><td>" + a.loan_amt.ToString("0.00") + "</td><td>" + a.loan_dt.ToString("dd-MM-yyyy").Replace("-", "/") + "</td><td>" + xact1.ToString("0.00") + "</td></tr>";
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
        public JsonResult Update(FreshListViewModel model)
        {
            Member_Mast mm = new Member_Mast();
            mm.tf_buffer = Convert.ToDecimal(model.edit_age);
            mm.mem_id = model.mem_no;
            mm.branch_id = "MN";
            model.msg = mm.updatetfbuffer(mm);
            return Json(model.msg);
        }
        public ActionResult getlicprimiumlistprintfile(FreshListViewModel model)
        {
            Loan_Master lm = new Loan_Master();
            List<Loan_Master> lml = new List<Loan_Master>();
            lml = lm.getdetailsbydaterange(model.fr_dt, model.to_dt);
            int i = 1;
            string brirth_dt = "";
            string joining_dt = "";
            Directory.CreateDirectory(Server.MapPath("~/wwwroot\\TextFiles"));
            using (StreamWriter sw = new StreamWriter(Server.MapPath("~/wwwroot\\TextFiles\\Insurance_Primium_List.txt")))
            {
                int Pg = 1;
                int Ln = 0;
                string serial = string.Empty;
                string loanee_name = "";
                string loan_dt = "";
                string mem_id = "";
                decimal loan_amt = 0;
                sw.WriteLine("AMRITNAGAR COLLIERY EMPLOYEES' CO-OPERATIVE CREDIT SOCIETY LIMITED");
                sw.WriteLine("REGD. NO. 333 . DATED : 02/02/1977");
                sw.WriteLine("Amritnagar Colliery , P.O - Raniganj , Dist - Burdwan ");
                sw.WriteLine("");
                sw.WriteLine("List of Insurance Premium Against Taken Loan For The Year :" + model.premium_yr);
                sw.WriteLine("");
                sw.WriteLine("               " + model.pan_no);
                sw.WriteLine("");
                sw.WriteLine("------------------------------------------------------------------------------------------------");       
                sw.WriteLine("Srl  | Name of Members         | Member No  | Date of    | Date of    | Loan       | E Loan    |");               
                sw.WriteLine("     |                         | MAN NO     | Joining    | Birth      | Amount     | Date      |");
                sw.WriteLine("------------------------------------------------------------------------------------------------");               
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
                    if (Convert.ToDateTime(am.birth_date).ToString("dd-MM-yyyy").Replace("-", "/") == "01/01/0001")
                    {
                        brirth_dt = "";
                    }
                    else if (Convert.ToDateTime(am.birth_date).ToString("dd-MM-yyyy").Replace("-", "/").Length > 12)
                    {
                        brirth_dt = (am.birth_date).ToString("dd-MM-yyyy").Replace("-", "/").Substring(0, 11);
                    }
                    else
                    {
                        brirth_dt = Convert.ToDateTime(am.birth_date).ToString("dd-MM-yyyy").Replace("-", "/");
                    }
                    if (Convert.ToDateTime(am.joining_dt).ToString("dd-MM-yyyy").Replace("-", "/") == "01/01/0001")
                    {
                        joining_dt = "";
                    }
                    else if (Convert.ToDateTime(am.joining_dt).ToString("dd-MM-yyyy").Replace("-", "/").Length > 12)
                    {
                        joining_dt = (am.joining_dt).ToString("dd-MM-yyyy").Replace("-", "/").Substring(0, 11);
                    }
                    else
                    {
                        joining_dt = Convert.ToDateTime(am.joining_dt).ToString("dd-MM-yyyy").Replace("-", "/");
                    }
                    if (am.loanee_name.ToString().Length > 25)
                    {
                        loanee_name = (am.loanee_name).Substring(0, 24);
                    }
                    else
                    {
                        loanee_name = am.loanee_name;
                    }
                    if (am.mem_id.ToString().Length > 12)
                    {
                        mem_id = (am.mem_id).Substring(0, 11);
                    }
                    else
                    {
                        mem_id = am.mem_id;
                    }
                    if (am.loan_amt.ToString().Length > 12)
                    {
                        loan_amt = Convert.ToDecimal((am.loan_amt).ToString().Substring(0, 11));
                    }
                    else
                    {
                        loan_amt = Convert.ToDecimal(am.loan_amt);
                    }
                    if (Convert.ToDateTime(am.loan_dt).ToString("dd-MM-yyyy").Replace("-", "/").Length > 11)
                    {
                        loan_dt = (am.loan_dt).ToString("dd-MM-yyyy").Replace("-", "/").Substring(0, 10);
                    }
                    else
                    {
                        loan_dt = Convert.ToDateTime(am.loan_dt).ToString("dd-MM-yyyy").Replace("-", "/");
                    }
                    if (Ln > Pg * 65)
                    {
                        Pg = Pg + 1;
                        Ln = Ln + 7;
                        sw.WriteLine("");
                        sw.WriteLine("AMRITNAGAR COLLIERY EMPLOYEES' CO-OPERATIVE CREDIT SOCIETY LIMITED");
                        sw.WriteLine("REGD. NO. 333 . DATED : 02/02/1977");
                        sw.WriteLine("Amritnagar Colliery , P.O - Raniganj , Dist - Burdwan ");
                        sw.WriteLine("");
                        sw.WriteLine("List of Insurance Premium Against Taken Loan For The Year :" + model.premium_yr);
                        sw.WriteLine("");
                        sw.WriteLine("               " + model.pan_no);
                        sw.WriteLine("");
                        sw.WriteLine("------------------------------------------------------------------------------------------------");
                        sw.WriteLine("Srl  | Name of Members         | Member No  | Date of    | Date of    | Loan       | E Loan    |");
                        sw.WriteLine("     |                         | MAN NO     | Joining    | Birth      | Amount     | Date      |");
                        sw.WriteLine("------------------------------------------------------------------------------------------------");                        
                    }
                    sw.WriteLine("".ToString().PadLeft(5 - (serial).ToString().Length) + serial + "|"
                         + "".ToString().PadLeft(25 - (loanee_name).ToString().Length) + loanee_name + "|"
                           + "".ToString().PadLeft(12 - (mem_id).ToString().Length) + mem_id + "|"
                           + "".ToString().PadLeft(12 - (joining_dt).ToString().Length) + joining_dt + "|"
                            + "".ToString().PadLeft(12 - (brirth_dt).ToString().Length) + brirth_dt + "|"
                             + "".ToString().PadLeft(12 - (loan_amt).ToString("0.00").Length) + loan_amt.ToString("0.00") + "|"
                              + "".ToString().PadLeft(11 - (loan_dt).ToString().Length) + loan_dt + "|");                                                   
                    Ln = Ln + 1;
                    i = i + 1;
                }
                sw.WriteLine("");
                sw.WriteLine("Contact No : " + model.contc_no);
                sw.WriteLine("E Mail Id  : " + model.email);
                sw.WriteLine("");
                sw.WriteLine("");
                sw.WriteLine("                                        SECRETARY ");
                sw.WriteLine("                                        AMRITNAGAR C.E.CO-OP. CR. S LTD.");
            }
            UtilityController u = new UtilityController();
            var memory = u.DownloadTextFiles("Insurance_Primium_List.txt", Server.MapPath("~/wwwroot\\TextFiles"));
            if (System.IO.File.Exists(Server.MapPath("~/wwwroot\\TextFiles\\Insurance_Primium_List.txt")))
            {
                System.IO.File.Delete(Server.MapPath("~/wwwroot\\TextFiles\\Insurance_Primium_List.txt"));
            }
            return File(memory.ToArray(), "text/plain", "Insurance_Primium_List_" + DateTime.Now.ToShortDateString().Replace(" / ", "_") + ".txt");
        }
        public ActionResult getfreshlistprintfile(FreshListViewModel model)
        {
            Recovery_Get rg = new Recovery_Get();
            List<Recovery_Get> rgl = new List<Recovery_Get>();
            rgl = rg.getdetails(model.emp_branch, model.book_no, model.rec_dt);
            int i = 1;
            decimal TOTTF = 0;     
            decimal TOTRTB = 0;
            decimal TOTSFL = 0;          
            decimal TOTSJL = 0;
            decimal TOTPSL = 0;
            decimal TOTDLL = 0;
            decimal TOTSL3 = 0;
            Directory.CreateDirectory(Server.MapPath("~/wwwroot\\TextFiles"));
            using (StreamWriter sw = new StreamWriter(Server.MapPath("~/wwwroot\\TextFiles\\Fresh_List.txt")))
            {
                int Pg = 1;
                int Ln = 0;
                string mem_id = "";
                string mem_name = "";
                string emp_id = "";
                string aC_hD = "";
                decimal pRIN_aMT = 0;
                decimal iNT_aMT = 0;
                decimal prin_bal = 0;
                string serial = string.Empty;
                sw.WriteLine(" BOOK - NO =" + model.book_no);
                sw.WriteLine("Unit :" + model.emp_branch);
                sw.WriteLine("Amritnagar Colliery  Employees' Co-op. Credit Society Ltd  Pg No : "+ Pg);
                sw.WriteLine("Freshlist   " + "  " + model.rec_dt);
                sw.WriteLine("==============================================================================================");
                sw.WriteLine("Srl |Member No.|Employee Id | Member Name             |A/C Head|Prin Amt.|Int.Amt |Prin Balan|");
                sw.WriteLine("==============================================================================================");              
                foreach (var am in rgl)
                {                    
                    if (Ln > Pg * 65)
                    {
                        Pg = Pg + 1;
                        Ln = Ln + 7;
                        sw.WriteLine(" BOOK - NO =" + model.book_no);
                        sw.WriteLine("Unit :" + model.emp_branch);
                        sw.WriteLine("Amritnagar Colliery  Employees' Co-op. Credit Society Ltd  Pg No : "+ Pg);
                        sw.WriteLine("Freshlist   " + "  " + model.rec_dt);
                        sw.WriteLine("==============================================================================================");
                        sw.WriteLine("Srl |Member No.|Employee Id | Member Name             |A/C Head|Prin Amt.|Int.Amt |Prin Balan|");
                        sw.WriteLine("==============================================================================================");                       
                    }
                    if (Convert.ToString(i).Length > 4)
                    {
                        serial = Convert.ToString(i).Substring(0, 3);
                    }
                    else
                    {
                        serial = Convert.ToString(i);
                    }
                    if (am.mEMBER_iD == null)
                    {
                        mem_id = "";
                    }
                    else if(am.mEMBER_iD.ToString().Length > 10)
                    {
                        mem_id = (am.mEMBER_iD).Substring(0, 9);
                    }
                    else
                    {
                        mem_id = am.mEMBER_iD;
                    }
                    if (am.mem_name == null)
                    {
                        mem_name = "";
                    }
                    else if (am.mem_name.ToString().Length > 25)
                    {
                        mem_name = (am.mem_name).Substring(0, 24);
                    }
                    else
                    {
                        mem_name = am.mem_name;
                    }
                    if(am.eMPLOYEE_iD.ToString().Length > 12)
                    {
                        emp_id = (am.eMPLOYEE_iD).Substring(0, 11);
                    }
                    else
                    {
                        emp_id = am.eMPLOYEE_iD;
                    }
                    if (am.aC_hD.ToString().Length > 8)
                    {
                        aC_hD = (am.aC_hD).Substring(0, 7);
                    }
                    else
                    {
                        aC_hD = am.aC_hD;
                    }
                    if (am.pRIN_aMT.ToString().Length > 9)
                    {
                        pRIN_aMT = Convert.ToDecimal((am.pRIN_aMT).ToString().Substring(0, 8));
                    }
                    else
                    {
                        pRIN_aMT = Convert.ToDecimal(am.pRIN_aMT);
                    }
                    if (am.iNT_aMT.ToString().Length > 8)
                    {
                        iNT_aMT = Convert.ToDecimal((am.iNT_aMT).ToString().Substring(0, 7));
                    }
                    else
                    {
                        iNT_aMT = Convert.ToDecimal(am.iNT_aMT);
                    }
                    if (am.prin_bal.ToString().Length > 10)
                    {
                        prin_bal = Convert.ToDecimal((am.prin_bal).ToString().Substring(0, 9));
                    }
                    else
                    {
                        prin_bal = Convert.ToDecimal(am.prin_bal);
                    }
                    sw.WriteLine("".ToString().PadLeft(4 - (serial).ToString().Length) + serial + "|"
                         + "".ToString().PadLeft(10 - (mem_id).ToString().Length) + mem_id + "|"
                           + "".ToString().PadLeft(12 - (emp_id).ToString().Length) + emp_id + "|"
                           + "".ToString().PadLeft(25 - (mem_name).ToString().Length) + mem_name + "|"
                            + "".ToString().PadLeft(8 - (aC_hD).ToString().Length) + aC_hD.ToString() + "|"
                             + "".ToString().PadLeft(9 - (pRIN_aMT).ToString("0.00").Length) + pRIN_aMT.ToString("0.00") + "|"
                              + "".ToString().PadLeft(8 - (iNT_aMT).ToString("0.00").Length) + iNT_aMT.ToString("0.00") + "|"
                               + "".ToString().PadLeft(10 - (prin_bal).ToString("0.00").Length) + prin_bal.ToString("0.00") + "|");                  
                    Ln = Ln + 1;
                    i = i + 1;
                    TOTTF =  am.TOTTF;
                    TOTRTB = am.TOTRTB;
                    TOTSFL = am.TOTSFL;                    
                    TOTSJL = am.TOTSJL;
                    TOTPSL = am.TOTPSL;
                    TOTDLL = am.TOTDLL;
                    TOTSL3 = am.TOTSL3;                    
                }
                sw.WriteLine("");
                sw.WriteLine("Total - Position");
                sw.WriteLine(" TF" + "  " + TOTTF.ToString("0.00"));
                sw.WriteLine(" RTB" + "  " + TOTRTB.ToString("0.00"));
                sw.WriteLine(" SFL" + "  " + TOTSFL.ToString("0.00"));
                sw.WriteLine(" SJL" + "  " + TOTSJL.ToString("0.00"));
                sw.WriteLine(" PSL" + "  " + TOTPSL.ToString("0.00"));
                sw.WriteLine(" DLL" + "  " + TOTDLL.ToString("0.00"));
                sw.WriteLine("SL3 " + "  " + TOTSL3.ToString("0.00"));
            }
            UtilityController u = new UtilityController();
            var memory = u.DownloadTextFiles("Fresh_List.txt", Server.MapPath("~/wwwroot\\TextFiles"));
            if (System.IO.File.Exists(Server.MapPath("~/wwwroot\\TextFiles\\Fresh_List.txt")))
            {
                System.IO.File.Delete(Server.MapPath("~/wwwroot\\TextFiles\\Fresh_List.txt"));
            }
            return File(memory.ToArray(), "text/plain", "Fresh_List_" + DateTime.Now.ToShortDateString().Replace(" / ", "_") + ".txt");
        }
        /********************************************Fresh List End*******************************************/

        /********************************************Sending Schedule Report Start*******************************************/
        public ActionResult SendingScheduleReport(SendingScheduleReportViewModel model)
        {
            UtilityController u = new UtilityController();
            model.EmpBranchDesc = u.getEmployerBranchMastDetails();
            return View(model);
        }
        public JsonResult getschedulesendingreportbyempbranchbooknoanddate(SendingScheduleReportViewModel model)
        {
            Recovery_Schedule rs = new Recovery_Schedule();
            List<Recovery_Schedule> rsl = new List<Recovery_Schedule>();
            rsl = rs.getdetails(model.emp_branch, model.book_no, model.sch_dt);
            int i = 1;
            if (rsl.Count > 0)
            {
                model.tableelement = "<tr><th>SR No</th><th>Employee Id</th><th>Name Of Member</th><th>A/c Hd</th><th>Prin Amount</th><th>Int Amount</th><th>Prin Bal</th><th>Int Rate</th></tr>";
                foreach (var a in rsl)
                {
                    if(a.ac_hd == "TF" || a.ac_hd == "SH" || a.ac_hd == "LICP" || a.ac_hd == "RTB")
                    {
                        if (i == 1)
                        {
                            model.tableelement = model.tableelement + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.emp_id + "</td><td>" + a.mem_name + "</td><td>" + a.ac_hd + "</td><td>" + a.prin_amt.ToString("0.00") + "</td><td>" + a.int_amt.ToString("0.00") + "</td><td>" + "" + "</td><td>" + a.int_rate.ToString("0.00") + "</td></tr>";
                        }
                        else
                        {
                            model.tableelement = model.tableelement + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.emp_id + "</td><td>" + a.mem_name + "</td><td>" + a.ac_hd + "</td><td>" + a.prin_amt.ToString("0.00") + "</td><td>" + a.int_amt.ToString("0.00") + "</td><td>" + "" + "</td><td>" + a.int_rate.ToString("0.00") + "</td></tr>";
                        }
                    }
                    else
                    {
                        if (i == 1)
                        {
                            model.tableelement = model.tableelement + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.emp_id + "</td><td>" + a.mem_name + "</td><td>" + a.ac_hd + "</td><td>" + a.prin_amt.ToString("0.00") + "</td><td>" + a.int_amt.ToString("0.00") + "</td><td>" + a.prin_bal.ToString("0.00") + "</td><td>" + a.int_rate.ToString("0.00") + "</td></tr>";
                        }
                        else
                        {
                            model.tableelement = model.tableelement + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.emp_id + "</td><td>" + a.mem_name + "</td><td>" + a.ac_hd + "</td><td>" + a.prin_amt.ToString("0.00") + "</td><td>" + a.int_amt.ToString("0.00") + "</td><td>" + a.prin_bal.ToString("0.00") + "</td><td>" + a.int_rate.ToString("0.00") + "</td></tr>";
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
        public JsonResult gettotalpositionreport(SendingScheduleReportViewModel model)
        {
            Recovery_Schedule rs = new Recovery_Schedule();
            List<Recovery_Schedule> rsl = new List<Recovery_Schedule>();
            rsl = rs.getdetails(model.emp_branch, model.book_no, model.sch_dt);
            int j = 1;
            int i = rsl.Count;
            if (rsl.Count > 0)
            {
                foreach (var a in rsl)
                {
                    if (j == i)
                    {
                        model.tableelement = "<tr><th>Thrift Fund</th><th>RTB</th><th>SFL</th><th>INT SFL</th><th>SJL</th><th>INT SJL</th><th>PSL</th><th>INT PSL</th><th>DLL</th><th>INT DLL</th><th>SL3</th><th>ISL3</th><th>M12</th><th>MI12</th><th>Total Deductable Amount</th><th>M14</th><th>MI14</th><th>PSL1</th><th>IPSL1</th><th>SFL1</th><th>ISFL1</th><th>SL4</th><th>ISL4</th><th>SHARE</th><th>SJL1</th><th>ISJL1</th><th>SL6</th><th>ISL6</th><th>SL7</th><th>ISL7</th><th>LICP</th></tr>";
                        model.tableelement = model.tableelement + "<tr><td>" + a.TOTTF.ToString("0.00") + "</td><td>" + a.TOTRTB.ToString("0.00") + "</td><td>" + a.TOTSFL + "</td><td>" + a.TOTISFL.ToString("0.00") + "</td><td>" + a.TOTSJL.ToString("0.00") + "</td><td>" + a.TOTISJL.ToString("0.00") + "</td><td>" + a.TOTPSL.ToString("0.00") + "</td><td>" + a.TOTIPSL.ToString("0.00") + "</td><td>" + a.TOTDLL.ToString("0.00") + "</td><td>" + a.TOTIDLL.ToString("0.00") + "</td><td>" + a.TOTSL3.ToString("0.00") + "</td><td>" + a.TOTSL3I.ToString("0.00") + "</td><td>" + a.TOTM12.ToString("0.00") + "</td><td>" + a.TOTIM12.ToString("0.00") + "</td><td>" + a.TOT_Deductable_Amount.ToString("0.00") + "</td><td>" + a.TOTM14.ToString("0.00") + "</td><td>" + a.TOTIM14.ToString("0.00") + "</td><td>" + a.TOTPSL1.ToString("0.00") + "</td><td>" + a.TOTIPSL1.ToString("0.00") + "</td><td>" + a.TOTSFL1.ToString("0.00") + "</td><td>" + a.TOTISFL1.ToString("0.00") + "</td><td>" + a.TOTSL4.ToString("0.00") + "</td><td>" + a.TOTISL4.ToString("0.00") + "</td><td>" + a.TOTSH.ToString("0.00") + "</td><td>" + a.TOTSJL.ToString("0.00") + "</td><td>" + a.TOTISJL1.ToString("0.00") + "</td><td>" + a.TOTSL6.ToString("0.00") + "</td><td>" + a.TOTISL6.ToString("0.00") + "</td><td>" + a.TOTSL7.ToString("0.00") + "</td><td>" + a.TOTISL7.ToString("0.00") + "</td><td>" + a.TOTLICP.ToString("0.00") + "</td></tr>";
                    }                    
                    j = j + 1;
                }
            }
            else
            {
                model.tableelement = null;
            }
            return Json(model);
        }
        public ActionResult getschedulereportprintfile(SendingScheduleReportViewModel model)
        {
            Recovery_Schedule rs = new Recovery_Schedule();
            List<Recovery_Schedule> rsl = new List<Recovery_Schedule>();
            rsl = rs.getdetails(model.emp_branch, model.book_no, model.sch_dt);
            int i = 1;
            decimal TOTTF = 0;
            decimal TOTRTB = 0;
            decimal TOTSFL = 0;
            decimal TOTSJL = 0;
            decimal TOTPSL = 0;
            decimal TOTDLL = 0;
            decimal TOTSL3 = 0;
            decimal xtot = 0;
            decimal xpamt = 0;
            decimal XIAMT = 0;
            decimal TOTISFL = 0;
            decimal TOTISJL = 0;                      
            decimal TOTIPSL = 0;            
            decimal TOTIDLL = 0;           
            decimal TOTSL3I = 0;
            decimal TOTM12 = 0;
            decimal TOTIM12 = 0;
            decimal TOTM14 = 0;
            decimal TOTIM14 = 0;
            decimal TOTPSL1 = 0;
            decimal TOTIPSL1 = 0;
            decimal TOTSFL1 = 0;
            decimal TOTISFL1 = 0;
            decimal TOTSL4 = 0;
            decimal TOTISL4 = 0;
            decimal TOTSL6 = 0;
            decimal TOTISL6 = 0;
            decimal TOTSL7 = 0;
            decimal TOTISL7 = 0;
            decimal TOTSJL1 = 0;
            decimal TOTISJL1 = 0;
            decimal TOTSH = 0;          
            decimal TOTLICP = 0;
            decimal TOT_Deductable_Amount = 0;
            string prin_amount = "";
            string int_amount = "";
            string prin_bal = "";
            string int_rate = "";
            Directory.CreateDirectory(Server.MapPath("~/wwwroot\\TextFiles"));
            using (StreamWriter sw = new StreamWriter(Server.MapPath("~/wwwroot\\TextFiles\\Schedule_Report.txt")))
            {
                int Pg = 1;
                int Ln = 0;                
                string serial = string.Empty;
                sw.WriteLine(" BOOK - NO = " + model.book_no);
                sw.WriteLine("Unit : " + model.emp_branch);
                sw.WriteLine("Amritnagar Colliery  Employees' Co-op. Credit Society Ltd  Pg No : " + Pg);
                sw.WriteLine("Salary Sending Sheet   " + "  " + model.sch_dt);
                sw.WriteLine("===========================================================================");
                sw.WriteLine("Srl |Empl.Id | Member Name        |A/C Head|Prin Amt.|Int.Amt |P/BAL  |Int ");
                sw.WriteLine("===========================================================================");
                string emp = "";
                foreach (var am in rsl)
                {
                    var a = rsl.First();
                    string XEMPID = a.emp_id;
                    string YEMPID = am.emp_id;                   
                    if (emp != am.emp_id)
                    {
                        if(emp == "")
                        {
                            emp = am.emp_id;
                        }
                        else
                        {
                            xtot = xpamt + XIAMT;
                            sw.WriteLine("                                           " + "         " + "        Total  " + "  " + xtot.ToString("0.00"));
                            // sw.WriteLine("----------------------------------------------------------------------------");
                            xpamt = xpamt + am.prin_amt;
                            XIAMT = XIAMT + am.int_amt;
                            xtot = 0;
                            xpamt = 0;
                            XIAMT = 0;
                            emp = am.emp_id;
                            sw.WriteLine("----------------------------------------------------------------------------");
                        }                       
                    }                  
                    prin_amount = am.prin_amt.ToString("0.00");
                    int_amount = am.int_amt.ToString("0.00");
                    prin_bal = am.prin_bal.ToString("0.00");
                    int_rate = am.int_rate.ToString("0.00");
                    if (Convert.ToString(i).Length == 1)
                    {
                        serial = "000" + Convert.ToString(i);
                    }
                    else if (Convert.ToString(i).Length == 2)
                    {
                        serial = "00" + Convert.ToString(i);
                    }
                    else if (Convert.ToString(i).Length == 3)
                    {
                        serial = "0" + Convert.ToString(i);
                    }
                    else
                    {
                        serial = Convert.ToString(i);
                    }                                                    
                    if (Ln > Pg * 65)
                    {
                        Pg = Pg + 1;
                        Ln = Ln + 7;
                        sw.WriteLine(" BOOK - NO = " + model.book_no);
                        sw.WriteLine("Unit : " + model.emp_branch);
                        sw.WriteLine("Amritnagar Colliery  Employees' Co-op. Credit Society Ltd  Pg No : " + Pg);
                        sw.WriteLine("Salary Sending Sheet   " + "  " + model.sch_dt);
                        sw.WriteLine("===========================================================================");
                        sw.WriteLine("Srl |Empl.Id | Member Name        |A/C Head|Prin Amt.|Int.Amt |P/BAL  |Int ");
                        sw.WriteLine("===========================================================================");
                    }
                    if (am.ac_hd == "TF" || am.ac_hd == "SH" || am.ac_hd == "LICP" || am.ac_hd == "RTB")
                    {
                        if (am.mem_name.Length > 20)
                        {
                            am.mem_name = am.mem_name.Substring(0, 20);
                        }
                        if (prin_amount.ToString().Length > 9)
                        {
                            prin_amount = prin_amount.Substring(0, 8);
                        }
                        if (int_amount.ToString().Length > 8)
                        {
                            int_amount = int_amount.Substring(0, 7);
                        }
                        if (prin_bal.ToString().Length > 7)
                        {
                            prin_bal = prin_bal.Substring(0, 7);
                        }
                        if (int_rate.ToString().Length > 10)
                        {
                            int_rate = int_rate.Substring(0, 7);
                        }
                        sw.WriteLine(serial + "|" + "".ToString().PadLeft(8 - (am.emp_id).ToString().Length) + am.emp_id + "|"
                           + "".ToString().PadLeft(20 - (am.mem_name).ToString().Length) + am.mem_name + "|"
                           + "".ToString().PadLeft(8 - (am.ac_hd).ToString().Length) + am.ac_hd + "|"
                            + "".ToString().PadLeft(9 - (prin_amount).ToString().Length) + prin_amount.ToString() + "|"
                             + "".ToString().PadLeft(8 - (int_amount).ToString().Length) + int_amount.ToString() + "|"
                              + "".ToString().PadLeft(7 - ("").ToString().Length) + "" + "|"
                               + "".ToString().PadLeft(7 - (int_rate).ToString().Length) + int_rate.ToString() + "|");                      
                    }
                    else
                    {
                        if(am.mem_name.Length > 20)
                        {
                            am.mem_name = am.mem_name.Substring(0, 20);
                        }
                        if (prin_amount.ToString().Length > 9)
                        {
                            prin_amount = prin_amount.Substring(0, 8);
                        }
                        if (int_amount.ToString().Length > 8)
                        {
                            int_amount = int_amount.Substring(0, 7);
                        }
                        if (prin_bal.ToString().Length > 7)
                        {
                            prin_bal = prin_bal.Substring(0, 7);
                        }
                        if (int_rate.ToString().Length > 10)
                        {
                            int_rate = int_rate.Substring(0, 7);
                        }
                        sw.WriteLine(serial + "|" + "".ToString().PadLeft(8 - (am.emp_id).ToString().Length) + am.emp_id + "|"
                           + "".ToString().PadLeft(20 - (am.mem_name).ToString().Length) + am.mem_name + "|"
                           + "".ToString().PadLeft(8 - (am.ac_hd).ToString().Length) + am.ac_hd + "|"
                            + "".ToString().PadLeft(9 - (prin_amount).ToString().Length) + prin_amount.ToString() + "|"
                             + "".ToString().PadLeft(8 - (int_amount).ToString().Length) + int_amount.ToString() + "|"
                              + "".ToString().PadLeft(7 - (prin_bal).ToString().Length) + prin_bal.ToString() + "|"
                               + "".ToString().PadLeft(7 - (int_rate).ToString().Length) + int_rate.ToString() + "|");                      
                    }
                    Ln = Ln + 1;
                    i = i + 1;
                    //if (XEMPID != YEMPID)
                    //{
                    //   // sw.WriteLine("----------------------------------------------------------------------------");
                    //}
                    //else
                    //{
                    //    sw.WriteLine("");
                    //}
                    xpamt = xpamt + am.prin_amt;
                    XIAMT = XIAMT + am.int_amt;                  
                    TOTTF = am.TOTTF;
                    TOTRTB = am.TOTRTB;
                    TOTSFL = am.TOTSFL;
                    TOTSJL = am.TOTSJL;
                    TOTISJL = am.TOTISJL;
                    TOTPSL = am.TOTPSL;
                    TOTDLL = am.TOTDLL;
                    TOTSL3 = am.TOTSL3;
                    TOTISFL = am.TOTISFL;
                    TOTIPSL = am.TOTIPSL;
                    TOTSL3 = am.TOTSL3;
                    TOTSL3I = am.TOTSL3I;
                    TOTM12 = am.TOTM12;
                    TOTIM12 = am.TOTIM12;
                    TOTM14 = am.TOTM14;
                    TOTIM14 = am.TOTIM14;
                    TOTIPSL1 = am.TOTIPSL1;
                    TOTPSL1 = am.TOTPSL1;
                    TOTSFL1 = am.TOTSFL1;
                    TOTISFL1 = am.TOTISFL1;
                    TOTSL4 = am.TOTSL4;
                    TOTISL4 = am.TOTISL4;
                    TOTSL6 = am.TOTSL6;
                    TOTISL6 = am.TOTISL6;
                    TOTSL7 = am.TOTSL7;
                    TOTISL7 = am.TOTISL7;
                    TOTSJL1 = am.TOTSJL1;
                    TOTISJL1 = am.TOTISJL1;
                    TOTSH = am.TOTSH;
                    TOTLICP = am.TOTLICP;
                    TOT_Deductable_Amount = am.TOT_Deductable_Amount;
                    xtot = xpamt + XIAMT;
                }               
                sw.WriteLine("");
                sw.WriteLine("                                            Total  " + "  " + xtot.ToString("0.00"));
                sw.WriteLine("");
                sw.WriteLine("Total - Position");
                sw.WriteLine(" TF" + "  " + TOTTF.ToString("0.00"));
                sw.WriteLine(" RTB" + "  " + TOTRTB.ToString("0.00"));
                sw.WriteLine(" SFL" + "  " + TOTSFL.ToString("0.00") + "  Interest " + TOTISFL.ToString("0.00"));
                sw.WriteLine(" SJL" + "  " + TOTSJL.ToString("0.00") + "  Interest " + TOTISJL.ToString("0.00"));
                sw.WriteLine(" PSL" + "  " + TOTPSL.ToString("0.00") + "  Interest " + TOTIPSL.ToString("0.00"));
                sw.WriteLine(" DLL" + "  " + TOTDLL.ToString("0.00") + "  Interest " + TOTIDLL.ToString("0.00"));
                sw.WriteLine(" SL3" + "  " + TOTSL3.ToString("0.00") + "  Interest " + TOTSL3I.ToString("0.00"));
                sw.WriteLine(" M12" + "  " + TOTM12.ToString("0.00") + "  Interest " + TOTIM12.ToString("0.00"));
                sw.WriteLine("TOTAL AMOUNT" + "  " + TOT_Deductable_Amount.ToString("0.00"));
                sw.WriteLine(" M14" + "  " + TOTM14.ToString("0.00") + "  Interest " + TOTIM14.ToString("0.00"));
                sw.WriteLine(" PSL1" + "  " + TOTM14.ToString("0.00") + "  Interest " + TOTIPSL1.ToString("0.00"));
                sw.WriteLine(" SFL1" + "  " + TOTM14.ToString("0.00") + "  Interest " + TOTISFL1.ToString("0.00"));
                sw.WriteLine(" SL4" + "  " + TOTSL4.ToString("0.00") + "  Interest " + TOTISL4.ToString("0.00"));
                sw.WriteLine(" SHARE" + "  " + TOTSH.ToString("0.00"));
                sw.WriteLine(" SJL1" + "  " + TOTSJL1.ToString("0.00") + "  Interest " + TOTISJL1.ToString("0.00"));
                sw.WriteLine(" SL6" + "  " + TOTSL6.ToString("0.00") + "  Interest " + TOTISL6.ToString("0.00"));
                sw.WriteLine(" SL7" + "  " + TOTSL7.ToString("0.00") + "  Interest " + TOTISL7.ToString("0.00"));
                sw.WriteLine(" LICP" + "  " + TOTLICP.ToString("0.00"));
            }
            UtilityController u = new UtilityController();
            var memory = u.DownloadTextFiles("Schedule_Report.txt", Server.MapPath("~/wwwroot\\TextFiles"));
            if (System.IO.File.Exists(Server.MapPath("~/wwwroot\\TextFiles\\Schedule_Report.txt")))
            {
                System.IO.File.Delete(Server.MapPath("~/wwwroot\\TextFiles\\Schedule_Report.txt"));
            }
            return File(memory.ToArray(), "text/plain", "Schedule_Report_" + DateTime.Now.ToShortDateString().Replace(" / ", "_") + ".txt");
        }

        /********************************************Sending Schedule Report End*******************************************/
    }
}
             
    
