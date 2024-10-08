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
using OfficeOpenXml;
using ClosedXML.Excel;
using ExcelDataReader;
using Amritnagar.Includes;
using System.Drawing;

namespace Amritnagar.Controllers
{
    public class SalaryDeductionController : Controller
    {
        SQLConfig config = new SQLConfig();

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
                sw.WriteLine("Amritnagar Colliery  Employees' Co-op. Credit Society Ltd  Pg No : " + Pg);
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
                        sw.WriteLine("Amritnagar Colliery  Employees' Co-op. Credit Society Ltd  Pg No : " + Pg);
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
                    else if (am.mEMBER_iD.ToString().Length > 10)
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
                    if (am.eMPLOYEE_iD.ToString().Length > 12)
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
                    TOTTF = am.TOTTF;
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
                    if (a.ac_hd == "TF" || a.ac_hd == "SH" || a.ac_hd == "LICP" || a.ac_hd == "RTB")
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
                        if (emp == "")
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
        /********************************************Prep Of Deduction Schedule Start*******************************************/
        [HttpGet]
        public ActionResult PrepOfDeductionSchedule(PrepOfDeductionScheduleViewModel model)
        {
            UtilityController u = new UtilityController();
            model.BranchDesc = u.getBranchMastDetails();
            model.TypeDesc = u.getTypeMastDetails();
            model.EmpDesc = u.getEmployerMastDetails();
            model.EmpBranchDesc = u.getEmployerBranchMastDetails();
            model.CategoryDesc = u.getCategoryMastDetails();
            model.sch_dt = DateTime.Now.ToShortDateString();
            return View(model);
        }
        public static int[] findIndex(string[,] stringArr, string keyString)
        {
            // initialising result array to -1 in case keyString
            // is not found
            int[] result = { -1, -1 };
            // iteration over all the elements of the 2-D array
            // rows
            for (int i = 0; i < stringArr.Length; i++)
            {
                // columns
                for (int j = 0; j < stringArr.GetLength(0); j++)
                {
                    // if keyString is found
                    if (stringArr[i, j].Equals(keyString))
                    {
                        result[0] = i;
                        result[1] = j;
                        return result;
                    }
                }
            }
            // if keyString is not found then -1 is returned
            return result;
        }
        public JsonResult GetListAfterUpdate(string emp_name, string unit, string mem_type, string mem_cat, string book_no, string sch_date, string branch)
        {
            PrepOfDeductionScheduleViewModel model = new PrepOfDeductionScheduleViewModel();
            List<Recovery_Schedule> rslst = new List<Recovery_Schedule>();
            Recovery_Schedule rs = new Recovery_Schedule();
            rslst = rs.getdecachd(emp_name, unit);
            string[] arrachd = new string[rslst.Count * 4];
            int i = 0;
            foreach (var a in rslst)
            {
                arrachd[i] = a.ac_hd;
                arrachd[i + 1] = a.ac_desc;
                arrachd[i + 2] = "0";
                arrachd[i + 3] = "0";
                i = i + 4;
            }
            rslst = rs.getdetailsForDeductionScheduleAfterUpdate(emp_name, unit, mem_type, mem_cat, book_no, sch_date, branch);
            if (rslst.Count > 0)
            {
                model.grid1 = "<tr id='1'><th>Book No.</th><th>Man Number</th><th>Name Of Member</th><th>A/C Head</th><th>A/C Number</th><th>Principal Bal.</th><th>inst.Amount</th><th>interest Amt.</th><th>Total</th></tr>";
                string emp = "";
                int idd = 0;
                decimal xprin = 0;
                decimal xint = 0;
                int l = 0;
                foreach (var a in rslst)
                {
                    idd = idd + 1;
                    //int[] result = findIndex(arrachd, a.r4);
                    int k = rslst.Where(b => b != null && b.r2 == a.r2).Count();
                    model.grid1 = model.grid1 + "<tr id=" + Convert.ToString(idd)+"><td>" + a.r1 + "</td><td>" + a.r2 + "</td><td>" + a.r3 + "</td>";
                    rs = rs.getPrinBalIntBalFromRecovery(emp_name, unit, mem_type, mem_cat, a.r1, sch_date, branch, a.r2, a.r4);
                    model.grid1 = model.grid1 + "<td>" + a.r4 + "</td>" +
                       "<td>" + a.r5 + "</td><td>" + rs.prin_bal.ToString("0.00") + "</td>" +
                        "<td>" + rs.prin_amt.ToString("0.00") + "</td><td>" + rs.int_amt.ToString("0.00") + "</td></tr>";
                    xprin = xprin + rs.prin_amt;
                    xint = xint + rs.int_amt;
                    l = l + 1;
                    if (l == k)
                    {
                        k = 0;
                        model.grid1 = model.grid1 + "<tr id=" + Convert.ToString(idd) + " style =\"background-color:pink\"><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td>" + (xprin + xint).ToString("0.00") + " </td></tr>";
                        l = 0;
                        xprin = 0;
                        xint = 0;
                    }
                    if (a.r4 != null)
                    {
                        var index = Array.FindIndex(arrachd, row => row.Contains(a.r4));
                        if (rs.prin_amt > 0)
                        {
                            arrachd[index + 2] = Convert.ToString(Convert.ToDecimal(arrachd[index + 2]) + Convert.ToDecimal(rs.prin_amt));
                         }
                         if (rs.int_amt > 0)
                         {
                            arrachd[index + 3] = Convert.ToString(Convert.ToDecimal(arrachd[index + 3]) + Convert.ToDecimal(rs.int_amt));
                         }
                     }                                      
                }
            }
            model.grid2 = "";
            model.grid2 = "<tr></th><th>Account Head Particulars</th><th>Principal Amount</th><th>Interest Amount</th><th>Total Amount</th></tr>";
            int j = 0;
            for (i = 0; i < (arrachd.Length) / 4; i++)
            {
                decimal totalamt = (Convert.ToDecimal(arrachd[j + 2]) + Convert.ToDecimal(arrachd[j + 3]));
                model.grid2 = model.grid2 + "<tr><td>" + arrachd[j + 1] + "</td><td>" + arrachd[j + 2] + "</td><td>" + arrachd[j + 3] + "</td><td>" + totalamt + "</td></tr>";
                model.prnt_bal = (Convert.ToDecimal(model.prnt_bal) + Convert.ToDecimal(arrachd[j + 2])).ToString("0.00");
                model.int_bal = (Convert.ToDecimal(model.int_bal) + Convert.ToDecimal(arrachd[j + 3])).ToString("0.00");
                model.tot_bal = (Convert.ToDecimal(model.tot_bal) + totalamt).ToString("0.00");
                j = j + 4;
            }
            return Json(model);
        }
        public JsonResult updateBlanceofLoans(PrepOfDeductionScheduleViewModel model)
        {
            Recovery_Schedule rs = new Recovery_Schedule();
            string msg = rs.updatloanblances(model);
            return Json(msg);
        }
        public JsonResult getdetailsForDeductionSchedule(string emp_name, string unit, string mem_type, string mem_cat, string book_no, string sch_date, string branch)
        {
            PrepOfDeductionScheduleViewModel model = new PrepOfDeductionScheduleViewModel();
            List<Recovery_Schedule> rslst = new List<Recovery_Schedule>();
            Recovery_Schedule rs = new Recovery_Schedule();
            rslst = rs.getdecachd(emp_name, unit);
            string[] arrachd = new string[rslst.Count * 4];
            int i = 0;
            foreach (var a in rslst)
            {
                arrachd[i] = a.ac_hd;
                arrachd[i + 1] = a.ac_desc;
                arrachd[i + 2] = "0";
                arrachd[i + 3] = "0";
                i = i + 4;
            }
            rslst = rs.getdetailsForDeductionSchedule(emp_name, unit, mem_type, mem_cat, book_no, sch_date, branch);
            if (rslst.Count > 0)
            {
                model.grid1 = "<tr id='1'><th>Book No.</th><th>Man Number</th><th>Name Of Member</th><th>A/C Head</th><th>A/C Number</th><th>Principal Bal.</th><th>inst.Amount</th><th>interest Amt.</th><th>Total</th></tr>";
                string emp = "";
                int rowsp = 0;

                foreach (var a in rslst)
                {
                    //int[] result = findIndex(arrachd, a.r4);
                    //if (book_no != "AL")
                    //{


                    if (a.r4 != null)
                    {
                        var index = Array.FindIndex(arrachd, row => row.Contains(a.r4));
                        if (a.r8 != null && a.r8 != "")
                        {
                            arrachd[index + 2] = Convert.ToString(Convert.ToDecimal(arrachd[index + 2]) + Convert.ToDecimal(a.r8));
                        }
                        if (a.r9 != null && a.r9 != "")
                        {
                            arrachd[index + 3] = Convert.ToString(Convert.ToDecimal(arrachd[index + 3]) + Convert.ToDecimal(a.r9));
                        }
                    }
                    model.grid1 = model.grid1 + "<tr><td>" + a.r1 + "</td><td>" + a.r2 + "</td><td>" + a.r3 + "</td>";
                    model.grid1 = model.grid1 + "<td>" + a.r4 + "</td>" +
                       "<td>" + a.r5 + "</td><td>" + a.r6 + "</td>" +
                        "<td>" + a.r8 + "</td><td>" + a.r9 + "</td><td>" + a.r10 + "</td></tr>";
                }
            }
            //if (book_no == "AL")
            //{
            //    rslst = rs.getdetailsForDeductionSchedule(emp_name, "both", mem_type, mem_cat, book_no, sch_date, branch);
            //    if (rslst.Count > 0)
            //    {

            //        foreach (var a in rslst)
            //        {
            //            if (a.r4 != null)
            //            {
            //                var index = Array.FindIndex(arrachd, row => row.Contains(a.r4));
            //                if (a.r8 != null && a.r8 != "")
            //                {
            //                    arrachd[index + 2] = Convert.ToString(Convert.ToDecimal(arrachd[index + 2]) + Convert.ToDecimal(a.r8));
            //                }
            //                if (a.r9 != null && a.r9 != "")
            //                {
            //                    arrachd[index + 3] = Convert.ToString(Convert.ToDecimal(arrachd[index + 3]) + Convert.ToDecimal(a.r9));
            //                }
            //            }
            //        }
            //    }
            //}
            model.grid2 = "";
            model.grid2 = "<tr></th><th>Account Head Particulars</th><th>Principal Amount</th><th>Interest Amount</th><th>Total Amount</th></tr>";
            int j = 0;
            for (i = 0; i < (arrachd.Length) / 4; i++)
            {
                decimal totalamt = (Convert.ToDecimal(arrachd[j + 2]) + Convert.ToDecimal(arrachd[j + 3]));
                model.grid2 = model.grid2 + "<tr><td>" + arrachd[j + 1] + "</td><td>" + arrachd[j + 2] + "</td><td>" + arrachd[j + 3] + "</td><td>" + totalamt + "</td></tr>";
                model.prnt_bal = (Convert.ToDecimal(model.prnt_bal) + Convert.ToDecimal(arrachd[j + 2])).ToString("0.00");
                model.int_bal = (Convert.ToDecimal(model.int_bal) + Convert.ToDecimal(arrachd[j + 3])).ToString("0.00");
                model.tot_bal = (Convert.ToDecimal(model.tot_bal) + totalamt).ToString("0.00");
                j = j + 4;
            }
            return Json(model);
        }
        public JsonResult GetBothcoliarylist(string emp_name, string unit, string mem_type, string mem_cat, string book_no, string sch_date, string branch)
        {
            PrepOfDeductionScheduleViewModel model = new PrepOfDeductionScheduleViewModel();
            List<Recovery_Schedule> rslst = new List<Recovery_Schedule>();
            Recovery_Schedule rs = new Recovery_Schedule();
            rslst = rs.getdecachd(emp_name, unit);
            string[] arrachd = new string[rslst.Count * 4];
            int i = 0;
            foreach (var a in rslst)
            {
                arrachd[i] = a.ac_hd;
                arrachd[i + 1] = a.ac_desc;
                arrachd[i + 2] = "0";
                arrachd[i + 3] = "0";
                i = i + 4;
            }
            if (book_no.ToUpper() == "AL")
            {
                rslst = rs.getaLLCOLIARYData(emp_name, mem_type, mem_cat, sch_date, branch, book_no, unit);
                if (rslst.Count > 0)
                {
                    model.grid1 = "<tr id='1'><th>Book No.</th><th>Man Number</th><th>Name Of Member</th><th>A/C Head</th><th>A/C Number</th><th>Principal Bal.</th><th>inst.Amount</th><th>interest Amt.</th><th>Total</th></tr>";
                    string emp = "";
                    int idd = 0;
                    decimal xprin = 0;
                    decimal xint = 0;
                    int l = 0;

                    foreach (var a in rslst)
                    {
                        idd = idd + 1;
                        //int[] result = findIndex(arrachd, a.r4);
                        int k = rslst.Where(b => b != null && b.emp_id == a.emp_id).Count();
                        if (unit == Convert.ToString(a.employer_branch))
                        {
                            model.grid1 = model.grid1 + "<tr id=" + Convert.ToString(idd) + "><td>" + a.book_no + "</td><td>" + a.emp_id + "</td><td>" + a.mem_name + "</td>";
                            //rs = rs.getPrinBalIntBalFromRecovery(emp_name, unit, mem_type, mem_cat, a.r1, sch_date, branch, a.r2, a.r4);
                            model.grid1 = model.grid1 + "<td>" + a.ac_hd + "</td>" +
                               "<td>" + a.vch_pacno + "</td><td>" + a.prin_bal.ToString("0.00") + "</td>" +
                                "<td>" + a.prin_amt.ToString("0.00") + "</td><td>" + a.int_amt.ToString("0.00") + "</td></tr>";
                            xprin = xprin + a.prin_amt;
                            xint = xint + a.int_amt;
                            l = l + 1;
                            if (l == k)
                            {
                                k = 0;
                                model.grid1 = model.grid1 + "<tr id=" + Convert.ToString(idd) + " style =\"background-color:pink\"><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td>" + (xprin + xint).ToString("0.00") + " </td></tr>";
                                l = 0;
                                xprin = 0;
                                xint = 0;
                            }
                        }
                        if (a.ac_hd != null)
                        {
                            var index = Array.FindIndex(arrachd, row => row.Contains(a.ac_hd));
                            if (a.prin_amt > 0)
                            {
                                arrachd[index + 2] = Convert.ToString(Convert.ToDecimal(arrachd[index + 2]) + Convert.ToDecimal(a.prin_amt));
                            }
                            if (a.int_amt > 0)
                            {
                                arrachd[index + 3] = Convert.ToString(Convert.ToDecimal(arrachd[index + 3]) + Convert.ToDecimal(a.int_amt));
                            }
                        }
                    }
                }

            }
            else
            {
                rslst = rs.getaLLCOLIARYData(emp_name, mem_type, mem_cat, sch_date, branch, book_no, unit);
                if (rslst.Count > 0)
                {
                    model.grid1 = "<tr id='1'><th>Book No.</th><th>Man Number</th><th>Name Of Member</th><th>A/C Head</th><th>A/C Number</th><th>Principal Bal.</th><th>inst.Amount</th><th>interest Amt.</th><th>Total</th></tr>";
                    string emp = "";
                    int idd = 0;
                    decimal xprin = 0;
                    decimal xint = 0;
                    int l = 0;

                    foreach (var a in rslst)
                    {
                        idd = idd + 1;
                        //int[] result = findIndex(arrachd, a.r4);
                        int k = rslst.Where(b => b != null && b.emp_id == a.emp_id).Count();
                        model.grid1 = model.grid1 + "<tr id=" + Convert.ToString(idd) + "><td>" + a.book_no + "</td><td>" + a.emp_id + "</td><td>" + a.mem_name + "</td>";
                        //rs = rs.getPrinBalIntBalFromRecovery(emp_name, unit, mem_type, mem_cat, a.r1, sch_date, branch, a.r2, a.r4);
                        model.grid1 = model.grid1 + "<td>" + a.ac_hd + "</td>" +
                           "<td>" + a.vch_pacno + "</td><td>" + a.prin_bal.ToString("0.00") + "</td>" +
                            "<td>" + a.prin_amt.ToString("0.00") + "</td><td>" + a.int_amt.ToString("0.00") + "</td></tr>";
                        xprin = xprin + a.prin_amt;
                        xint = xint + a.int_amt;
                        l = l + 1;
                        if (l == k)
                        {
                            k = 0;
                            model.grid1 = model.grid1 + "<tr id=" + Convert.ToString(idd) + " style =\"background-color:pink\"><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td>" + (xprin + xint).ToString("0.00") + " </td></tr>";
                            l = 0;
                            xprin = 0;
                            xint = 0;
                        }
                        if (a.ac_hd != null)
                        {
                            var index = Array.FindIndex(arrachd, row => row.Contains(a.ac_hd));
                            if (a.prin_amt > 0)
                            {
                                arrachd[index + 2] = Convert.ToString(Convert.ToDecimal(arrachd[index + 2]) + Convert.ToDecimal(a.prin_amt));
                            }
                            if (a.int_amt > 0)
                            {
                                arrachd[index + 3] = Convert.ToString(Convert.ToDecimal(arrachd[index + 3]) + Convert.ToDecimal(a.int_amt));
                            }
                        }
                    }
                }
            }

            model.grid2 = "";
            model.grid2 = "<tr></th><th>Account Head Particulars</th><th>Principal Amount</th><th>Interest Amount</th><th>Total Amount</th></tr>";
            int j = 0;
            for (i = 0; i < (arrachd.Length) / 4; i++)
            {
                decimal totalamt = (Convert.ToDecimal(arrachd[j + 2]) + Convert.ToDecimal(arrachd[j + 3]));
                model.grid2 = model.grid2 + "<tr><td>" + arrachd[j + 1] + "</td><td>" + arrachd[j + 2] + "</td><td>" + arrachd[j + 3] + "</td><td>" + totalamt + "</td></tr>";
                model.prnt_bal = (Convert.ToDecimal(model.prnt_bal) + Convert.ToDecimal(arrachd[j + 2])).ToString("0.00");
                model.int_bal = (Convert.ToDecimal(model.int_bal) + Convert.ToDecimal(arrachd[j + 3])).ToString("0.00");
                model.tot_bal = (Convert.ToDecimal(model.tot_bal) + totalamt).ToString("0.00");
                j = j + 4;
            }
            return Json(model);
        }
        /********************************************Prep Of Deduction Schedule Start*******************************************/

        /********************************************Recovery From Salary Deduction End*******************************************/
        [HttpGet]
        public ActionResult RecoveryFrmSalaryDeduction(RecoveryFrmSalaryDeductionViewModel model)
        {
            UtilityController u = new UtilityController();
            model.BranchDesc = u.getBranchMastDetails();
            model.EmpDesc = u.getEmployerMastDetails();
            model.EmpBranchDesc = u.getEmployerBranchMastDetails();
            model.sch_dt = DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/");
            model.rec_dt = DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/");
            return View(model);
        }
        public ActionResult getdeductlist(string emplyer_name, string emp_unit)
        {
            Deduct_ACHD_Mast dam = new Deduct_ACHD_Mast();
            var subcategories = dam.getdedtList(emplyer_name, emp_unit);
            return Json(subcategories, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getRecoveryFrmSalaryDeductionList(RecoveryFrmSalaryDeductionViewModel model)
        {
            Recovery_Schedule rs = new Recovery_Schedule();
            List<Recovery_Schedule> rslst = new List<Recovery_Schedule>();
            rslst = rs.getdecachd(model.emplyer_name, model.emp_unit);
            string[] arrachd = new string[rslst.Count * 4];
            int i = 0;
            foreach (var a in rslst)
            {
                arrachd[i] = a.ac_hd;
                arrachd[i + 1] = a.ac_desc;
                arrachd[i + 2] = "0";
                arrachd[i + 3] = "0";
                i = i + 4;
            }
            Recovery_Get rg = new Recovery_Get();
            // string msg = rg.saveRecovery(model);
            rslst = rs.getdetailsForRecoveryFrmSalartDeduction(model.ded_achd, model.branch, model.emplyer_name, model.emp_unit, model.book_no, model.sch_dt);
            int l = 0;
            if (rslst.Count > 0)
            {
                model.grid1 = "<tr><th style =\"display:none\">Srl</th></th><th>Book No.</th><th>Man Number</th><th>Name Of Member</th><th>A/C Head</th><th>A/C Number</th><th>Principal Bal.</th><th>Principal Ded.</th><th>Intrest Amt.</th><th>Total</th></tr>";
                string emp = "";
                decimal xprin = 0;
                decimal xint = 0;
                int rowsp = 0;
                foreach (var a in rslst)
                {
                    int j = rslst.Where(b => b != null && b.emp_id == a.emp_id).Count();

                    model.grid1 = model.grid1 + "<tr><td style =\"display:none\">" + Convert.ToString(i) + "</td><td>" + a.book_no + "</td>";

                    //if (emp != a.emp_id)
                    //{
                    //    rowsp = 0;
                    //    model.grid1 = model.grid1 + "<td rowspan=\"" + j + "\">" + a.emp_id + " </td><td rowspan=\"" + j + "\">" + a.mem_name + " </td>";
                    //    rowsp++;
                    //}
                    //if (rowsp == 0)
                    //{
                    //    model.grid1 = model.grid1 + "<td rowspan=\"" + j + "\">" + a.emp_id + " </td><td rowspan=\"" + j + "\">" + a.mem_name + " </td>";
                    //    rowsp++;
                    //}
                    if (a.ac_hd != null)
                    {
                        var index = Array.FindIndex(arrachd, row => row.Contains(a.ac_hd));
                        if (a.prin_amt != 00 && a.prin_amt != 0)
                        {
                            arrachd[index + 2] = Convert.ToString(Convert.ToDecimal(arrachd[index + 2]) + Convert.ToDecimal(a.prin_amt));
                        }
                        if (a.int_amt != 00 && a.int_amt != 0)
                        {
                            arrachd[index + 3] = Convert.ToString(Convert.ToDecimal(arrachd[index + 3]) + Convert.ToDecimal(a.int_amt));
                        }
                    }
                    rg = rg.getPrinBalIntBal(a.ac_hd, a.branch_id, a.employer_cd, a.emp_id, a.book_no, a.sch_date, a.employer_branch);
                    model.grid1 = model.grid1 + "<td>" + a.emp_id + "</td><td>" + a.mem_name + "</td><td>" + a.ac_hd + "</td><td>" + a.vch_pacno + "</td><td>" + a.prin_bal.ToString("0.00") + "</td><td>" + rg.pRIN_aMT.ToString("0.00") + "</td><td>" + rg.iNT_aMT.ToString("0.00") + "</td></tr>";
                    emp = a.emp_id;
                    xprin = xprin + rg.pRIN_aMT;
                    xint = xint + rg.iNT_aMT;
                    l = l + 1;
                    if (l == j)
                    {
                        j = 0;
                        model.grid1 = model.grid1 + "<tr style =\"background-color:pink\"><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td>" + (xprin + xint).ToString("0.00") + " </td></tr>";
                        l = 0;
                        xprin = 0;
                        xint = 0;
                    }
                }
                model.grid2 = "";
                model.grid2 = "<tr></th><th>Account Head Particulars</th><th>Principal Amount</th><th>Interest Amount</th><th>Total Amount</th></tr>";
                int k = 0;
                for (i = 0; i < (arrachd.Length) / 4; i++)
                {
                    decimal totalamt = (Convert.ToDecimal(arrachd[k + 2]) + Convert.ToDecimal(arrachd[k + 3]));
                    model.grid2 = model.grid2 + "<tr><td>" + arrachd[k + 1] + "</td><td>" + Convert.ToDecimal(arrachd[k + 2]).ToString("0.00") + "</td><td>" + Convert.ToDecimal(arrachd[k + 3]).ToString("0.00") + "</td><td>" + totalamt.ToString("0.00") + "</td></tr>";
                    model.prnt_bal = (Convert.ToDecimal(model.prnt_bal) + Convert.ToDecimal(arrachd[k + 2])).ToString("0.00");
                    model.int_bal = (Convert.ToDecimal(model.int_bal) + Convert.ToDecimal(arrachd[k + 3])).ToString("0.00");
                    model.tot_bal = (Convert.ToDecimal(model.tot_bal) + totalamt).ToString("0.00");
                    k = k + 4;
                }
            }
            else
            {
                model.grid1 = "";
            }

            return Json(model);
        }
        public ActionResult UpdateLedgerListByEmpId(RecoveryFrmSalaryDeductionViewModel model)
        {
            Recovery_Get rg = new Recovery_Get();
            rg.UpdateLedgerListByEmpId(model);
            return Json("Updated");
        }
        public ActionResult saveRecoveryGet(RecoveryFrmSalaryDeductionViewModel model)
        {
            Recovery_Get rg = new Recovery_Get();
            string msg = rg.saveRecovery(model);
            return Json(msg);
        }
        public ActionResult SaveInledger(RecoveryFrmSalaryDeductionViewModel model)
        {
            Recovery_Get rg = new Recovery_Get();
            rg.SaveInLedger(model);
            return Json("Updated");
        }
        /********************************************Recovery From Salary Deduction End*******************************************/

        /********************************************General Deduction Schedule Start*******************************************/
        [HttpGet]
        public ActionResult GeneralDeductionschedule(GeneralDedscheduleViewModel model)
        {
            UtilityController u = new UtilityController();
            model.EmpBranchDesc = u.getEmployerBranchMastDetails();
            model.CategoryDesc = u.getCategoryMastDetails();
            model.BranchDesc = u.getBranchMastDetails();
            model.code = "35";
            return View(model);
        }
        public JsonResult PopulateDeductionScheduleList(GeneralDedscheduleViewModel model)
        {
            Recovery_Schedule rs = new Recovery_Schedule();
            List<Recovery_Schedule> rslst = new List<Recovery_Schedule>();
            rslst = rs.getdecschlist(model.branch, model.mem_cat, model.sending_dt, model.unit);
            int i = 1;
            int k = 1;
            decimal xprin = 0;
            decimal xint = 0;
            string emp = "";
            decimal tot_mem = 0;
            decimal tot_prin_amt = 0;
            decimal tot_int_amt = 0;
            decimal tot_tf_amt = 0;
            decimal tot_amt = 0;
            if (rslst.Count > 0)
            {
                model.tableelement = "<tr><th>Srl</th><th>EmpId</th><th>Member Name</th><th>AC/Hd</th><th>Prin Amt</th><th>Int Amt</th><th>Employer Branch</th><th>Book No</th><th>Tf Amount</th><th>Total Amount</th></tr>";
                foreach (var a in rslst)
                {
                    int j = rslst.Where(b => b != null && b.emp_id == a.emp_id).Count();
                    if (a.ac_hd != "TF")
                    {
                        //model.tableelement = model.tableelement + "<tr><td>" + Convert.ToInt32(i) + "</td><td>" + a.emp_id + "</td><td>" + a.mem_name + "</td><td>" + a.ac_hd + "</td><td>" + a.prin_amt.ToString("0.00") + "</td><td>" + a.int_amt.ToString("0.00") + "</td><td>" + a.unit + "</td><td>" + a.book_no + "</td><td>" + "" + "</td></tr>";
                        tot_prin_amt = tot_prin_amt + a.prin_amt;
                        tot_int_amt = tot_int_amt + a.int_amt;
                    }
                    else
                    {
                        //model.tableelement = model.tableelement + "<tr><td>" + Convert.ToInt32(i) + "</td><td>" + a.emp_id + "</td><td>" + a.mem_name + "</td><td>" + a.ac_hd + "</td><td>" + "" + "</td><td>" + "" + "</td><td>" + a.unit + "</td><td>" + a.book_no + "</td><td>" + a.prin_amt.ToString("0.00") + "</td></tr>";
                        tot_tf_amt = tot_tf_amt + a.prin_amt;
                    }
                    emp = a.emp_id;
                    xprin = xprin + a.prin_amt;
                    xint = xint + a.int_amt;
                    if (i == j)
                    {
                        j = 0;
                        model.tableelement = model.tableelement + "<tr><td>" + Convert.ToInt32(k) + "</td><td>" + a.emp_id + "</td><td>" + a.mem_name + "</td><td></td><td></td><td></td><td>" + a.unit + "</td><td>" + a.book_no + "</td><td></td><td>" + (xprin + xint).ToString("0.00") + "</td></tr>";
                        //model.tableelement = model.tableelement + "<tr style =\"background-color:pink\"><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td>" + (xprin + xint).ToString("0.00") + " </td></tr>";
                        tot_amt = tot_amt + xprin + xint;
                        i = 0;
                        xprin = 0;
                        xint = 0;
                        tot_mem = tot_mem + 1;
                        k = k + 1;
                    }
                    i = i + 1;
                }
                model.tableelement1 = "<tr><th>Total Member</th><th>Total Principal Amount</th><th>Total Interest Amount</th><th>Total Tf Amount</th><th>Total Amt</th></tr>";
                model.tableelement1 = model.tableelement1 + "<tr><td>" + tot_mem + "</td><td>" + tot_prin_amt.ToString("0.00") + "</td><td>" + tot_int_amt.ToString("0.00") + "</td><td>" + tot_tf_amt.ToString("0.00") + "</td><td>" + tot_amt.ToString("0.00") + "</td></tr>";
            }
            else
            {
                model.tableelement = null;
            }
            return Json(model);
        }
        public ActionResult ExportExcelForDeductionScheduleList(GeneralDedscheduleViewModel model)
        {
            decimal total_amt = 0;
            decimal tot_amt = 0;
            decimal xprin = 0;
            decimal xint = 0;
            string emp = "";
            Recovery_Schedule rs = new Recovery_Schedule();
            List<Recovery_Schedule> rslst = new List<Recovery_Schedule>();
            rslst = rs.getdecschlist(model.branch, model.mem_cat, model.sending_dt, model.unit);
            using (var workbook = new XLWorkbook())
            {
                int k = 1;
                int i = 1;
                var worksheet = workbook.Worksheets.Add("LoanData");
                worksheet.Style.Font.FontName = "Arial";
                worksheet.Style.Font.FontSize = 10;
                worksheet.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                worksheet.Style.Alignment.Vertical = XLAlignmentVerticalValues.Bottom;
                worksheet.Style.NumberFormat.Format = "@";
                var currentRow = 2;

                foreach (var a in rslst)
                {
                    int j = rslst.Where(b => b != null && b.emp_id == a.emp_id).Count();
                    emp = a.emp_id;
                    xprin = xprin + a.prin_amt;
                    xint = xint + a.int_amt;
                    if (i == j)
                    {
                        j = 0;
                        total_amt = total_amt + xprin + xint;
                        //ws.Cells[string.Format("A{0}", rowstart)].Value = Convert.ToString(k);
                        //ws.Cells[string.Format("B{0}", rowstart)].Value = a.unit;
                        //ws.Cells[string.Format("C{0}", rowstart)].Value = a.emp_id;
                        //ws.Cells[string.Format("D{0}", rowstart)].Value = total_amt;
                        //ws.Cells[string.Format("E{0}", rowstart)].Value = model.year;
                        //ws.Cells[string.Format("F{0}", rowstart)].Value = model.month_code;
                        //ws.Cells[string.Format("G{0}", rowstart)].Value = model.code;
                        worksheet.Cell(currentRow, 1).Value = Convert.ToString(k);
                        worksheet.Cell(currentRow, 2).Value = a.unit;
                        worksheet.Cell(currentRow, 3).Value = a.emp_id;
                        worksheet.Cell(currentRow, 4).Value = total_amt.ToString("0.00");
                        worksheet.Cell(currentRow, 5).Value = model.year;
                        worksheet.Cell(currentRow, 6).Value = model.month_code;
                        worksheet.Cell(currentRow, 7).Value = model.code;

                        tot_amt = tot_amt + total_amt;
                        currentRow++;
                        i = 0;
                        xprin = 0;
                        xint = 0;
                        total_amt = 0;
                        k++;
                    }
                    i++;
                }
                worksheet.Cell((currentRow + 2), 4).Value = tot_amt.ToString("0.00");
                string fname = Convert.ToDateTime(model.sending_dt).ToString("MMMM").ToUpper();
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "" + model.unit + "_CO-OPERATIVE_FOR_" + fname + "-" + model.year + ".xls");
                }
            }
        }
        /********************************************General Deduction Schedule End*******************************************/
    }
}
