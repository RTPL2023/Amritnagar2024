using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Amritnagar.Includes;
using Amritnagar.Models.ViewModel;

namespace Amritnagar.Models.Database
{
    public class Recovery_Schedule
    {
        SQLConfig config = new SQLConfig();
        public string branch_id { get; set; }      
        public string employer_cd { get; set; }
        public string unit { get; set; }
        public int employer_branch { get; set; }
        public string sch_date { get; set; }
        public string book_no { get; set; }
        public string emp_id { get; set; }
        public string ac_hd { get; set; }
        public string vch_pacno { get; set; }
        public string mem_name { get; set; }
        public string mem_type { get; set; }
        public string mem_category { get; set; }
        public decimal prin_bal { get; set; }
        public string over_due { get; set; }
        public decimal prin_amt { get; set; }
        public decimal int_amt { get; set; }
        public decimal int_rate { get; set; }
        public decimal TOTTF { get; set; }
        public decimal TOTSH { get; set; }
        public decimal TOTLICP { get; set; }
        public decimal TOTRTB { get; set; }
        public decimal TOTSFL { get; set; }
        public decimal TOTISFL { get; set; }
        public decimal TOTSJL { get; set; }
        public decimal TOTISJL { get; set; }
        public decimal TOTPSL { get; set; }
        public decimal TOTIPSL { get; set; }
        public decimal TOTDLL { get; set; }
        public decimal TOTIDLL { get; set; }
        public decimal TOTSL3 { get; set; }
        public decimal TOTSL3I { get; set; }
        public decimal TOTM12 { get; set; }
        public decimal TOTIM12 { get; set; }
        public decimal TOTM14 { get; set; }
        public decimal TOTIM14 { get; set; }
        public decimal TOTPSL1 { get; set; }
        public decimal TOTIPSL1 { get; set; }
        public decimal TOTSFL1 { get; set; }
        public decimal TOTISFL1 { get; set; }
        public decimal TOTSL4 { get; set; }
        public decimal TOTISL4 { get; set; }
        public decimal TOTSL6 { get; set; }
        public decimal TOTISL6 { get; set; }
        public decimal TOTSL7 { get; set; }
        public decimal TOTISL7 { get; set; }
        public decimal TOTSJL1 { get; set; }
        public decimal TOTISJL1 { get; set; }
        public decimal TOT_Deductable_Amount { get; set; }
        public string r1 { get; set; }
        public string r2 { get; set; }
        public string r3 { get; set; }
        public string r4 { get; set; }
        public string r5 { get; set; }
        public string r6 { get; set; }
        public string r7 { get; set; }
        public string r8 { get; set; }
        public string r9 { get; set; }
        public string r10 { get; set; }
        public string r11 { get; set; }
        public string r12 { get; set; }
        public string r13 { get; set; }
        public string g2r1 { get; set; }
        public string g2r2 { get; set; }
        public string g2r3 { get; set; }
        public string ac_desc { get; set; }

        public List<Recovery_Schedule> getdetails(string emp_branch, string book_no, string sch_date)
        {
            decimal TOTTF = 0;
            decimal TOTSH = 0;
            decimal TOTLICP = 0;
            decimal TOTRTB = 0;
            decimal TOTSFL = 0;
            decimal TOTISFL = 0;
            decimal TOTISJL = 0;
            decimal TOTSJL = 0;
            decimal TOTPSL = 0;
            decimal TOTIPSL = 0;
            decimal TOTDLL = 0;
            decimal TOTIDLL = 0;
            decimal TOTSL3 = 0;
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

            List<Recovery_Schedule> rgl = new List<Recovery_Schedule>();
            string sql = string.Empty;
            sql = "SELECT * FROM RECOVERY_SCHEDULE WHERE  EMPLOYER_BRANCH='" + emp_branch + "' AND  BOOK_NO='" + book_no + "' AND convert(varchar, SCH_DATE, 103) = convert(varchar, '" + sch_date + "', 103) ORDER BY EMPLOYEE_ID";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    Recovery_Schedule rs = new Recovery_Schedule();
                    rs.emp_id = Convert.ToString(dr["EMPLOYEE_ID"]);
                    rs.ac_hd = Convert.ToString(dr["AC_HD"]);
                    rs.mem_name = Convert.ToString(dr["MEMBER_NAME"]);
                    rs.prin_amt = !Convert.IsDBNull(dr["PRIN_AMT"]) ? Convert.ToDecimal(dr["PRIN_AMT"]) : Convert.ToDecimal("00");
                    rs.int_amt = !Convert.IsDBNull(dr["INT_AMT"]) ? Convert.ToDecimal(dr["INT_AMT"]) : Convert.ToDecimal("00");
                    rs.prin_bal = !Convert.IsDBNull(dr["PRIN_BAL"]) ? Convert.ToDecimal(dr["PRIN_BAL"]) : Convert.ToDecimal("00");

                    if (rs.ac_hd == "TF")
                    {
                        TOTTF = (TOTTF + rs.prin_amt + rs.int_amt);
                    }
                    if (rs.ac_hd == "SH")
                    {
                        TOTSH = (TOTSH + rs.prin_amt + rs.int_amt);
                    }
                    if (rs.ac_hd == "LICP")
                    {
                        TOTLICP = (TOTLICP + rs.prin_amt + rs.int_amt);
                    }
                    if (rs.ac_hd == "RTB")
                    {
                        TOTRTB = (TOTRTB + rs.prin_amt + rs.int_amt);
                    }
                    if (rs.ac_hd == "SFL")
                    {
                        TOTSFL = (TOTSFL + rs.prin_amt);
                        TOTISFL = (TOTISFL + rs.int_amt);
                        sql = "select * from loan_MASTER where employee_id='" + rs.emp_id + "'AND AC_HD='SFL'AND CLOS_FLAG IS NULL order by employee_id";
                        config.singleResult(sql);
                        if (config.dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr2 in config.dt.Rows)
                            {
                                rs.int_rate = !Convert.IsDBNull(dr2["INT_RATE"]) ? Convert.ToDecimal(dr2["INT_RATE"]) : Convert.ToDecimal("00");
                            }
                        }
                    }
                    if (rs.ac_hd == "SJL")
                    {
                        TOTSJL = (TOTSJL + rs.prin_amt);
                        TOTISJL = (TOTISJL + rs.int_amt);
                        sql = "select * from loan_MASTER where employee_id='" + rs.emp_id + "'AND AC_HD='SJL'AND CLOS_FLAG IS NULL order by employee_id";
                        config.singleResult(sql);
                        if (config.dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr3 in config.dt.Rows)
                            {
                                rs.int_rate = !Convert.IsDBNull(dr3["INT_RATE"]) ? Convert.ToDecimal(dr3["INT_RATE"]) : Convert.ToDecimal("00");
                            }
                        }
                    }
                    if (rs.ac_hd == "PSL")
                    {
                        TOTPSL = (TOTPSL + rs.prin_amt);
                        TOTIPSL = (TOTIPSL + rs.int_amt);
                        sql = "select * from loan_MASTER where employee_id='" + rs.emp_id + "'AND AC_HD='PSL'AND CLOS_FLAG IS NULL order by employee_id";
                        config.singleResult(sql);
                        if (config.dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr4 in config.dt.Rows)
                            {
                                rs.int_rate = !Convert.IsDBNull(dr4["INT_RATE"]) ? Convert.ToDecimal(dr4["INT_RATE"]) : Convert.ToDecimal("00");
                            }
                        }
                    }
                    if (rs.ac_hd == "DLL")
                    {
                        TOTDLL = (TOTDLL + rs.prin_amt);
                        TOTIDLL = (TOTIDLL + rs.int_amt);
                        sql = "select * from loan_MASTER where employee_id='" + rs.emp_id + "'AND AC_HD='DLL'AND CLOS_FLAG IS NULL order by employee_id";
                        config.singleResult(sql);
                        if (config.dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr5 in config.dt.Rows)
                            {
                                rs.int_rate = !Convert.IsDBNull(dr5["INT_RATE"]) ? Convert.ToDecimal(dr5["INT_RATE"]) : Convert.ToDecimal("00");
                            }
                        }
                    }
                    if (rs.ac_hd == "SL3")
                    {
                        TOTSL3 = (TOTSL3 + rs.prin_amt);
                        TOTSL3I = (TOTSL3I + rs.int_amt);
                        sql = "select * from loan_MASTER where employee_id='" + rs.emp_id + "'AND AC_HD='SL3'AND CLOS_FLAG IS NULL order by employee_id";
                        config.singleResult(sql);
                        if (config.dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr6 in config.dt.Rows)
                            {
                                rs.int_rate = !Convert.IsDBNull(dr6["INT_RATE"]) ? Convert.ToDecimal(dr6["INT_RATE"]) : Convert.ToDecimal("00");
                            }
                        }
                    }
                    if (rs.ac_hd == "M12")
                    {
                        TOTM12 = (TOTM12 + rs.prin_amt);
                        TOTIM12 = (TOTIM12 + rs.int_amt);
                        sql = "select * from loan_MASTER where employee_id='" + rs.emp_id + "'AND AC_HD='M12' AND CLOS_FLAG IS NULL order by employee_id";
                        config.singleResult(sql);
                        if (config.dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr7 in config.dt.Rows)
                            {
                                rs.int_rate = !Convert.IsDBNull(dr7["INT_RATE"]) ? Convert.ToDecimal(dr7["INT_RATE"]) : Convert.ToDecimal("00");
                            }
                        }
                    }
                    if (rs.ac_hd == "M14")
                    {
                        TOTM14 = (TOTM14 + rs.prin_amt);
                        TOTIM14 = (TOTIM14 + rs.int_amt);
                        sql = "select * from loan_MASTER where employee_id='" + rs.emp_id + "'AND AC_HD='M14' AND CLOS_FLAG IS NULL order by employee_id";
                        config.singleResult(sql);
                        if (config.dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr8 in config.dt.Rows)
                            {
                                rs.int_rate = !Convert.IsDBNull(dr8["INT_RATE"]) ? Convert.ToDecimal(dr8["INT_RATE"]) : Convert.ToDecimal("00");
                            }
                        }
                    }
                    if (rs.ac_hd == "PSL1")
                    {
                        TOTPSL1 = (TOTPSL1 + rs.prin_amt);
                        TOTIPSL1 = (TOTIPSL1 + rs.int_amt);
                        sql = "select * from loan_MASTER where employee_id='" + rs.emp_id + "'AND AC_HD='PSL1' AND CLOS_FLAG IS NULL order by employee_id";
                        config.singleResult(sql);
                        if (config.dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr9 in config.dt.Rows)
                            {
                                rs.int_rate = !Convert.IsDBNull(dr9["INT_RATE"]) ? Convert.ToDecimal(dr9["INT_RATE"]) : Convert.ToDecimal("00");
                            }
                        }
                    }
                    if (rs.ac_hd == "SFL1")
                    {
                        TOTSFL1 = (TOTSFL1 + rs.prin_amt);
                        TOTISFL1 = (TOTISFL1 + rs.int_amt);
                        sql = "select * from loan_MASTER where employee_id='" + rs.emp_id + "'AND AC_HD='SFL1' AND CLOS_FLAG IS NULL order by employee_id";
                        config.singleResult(sql);
                        if (config.dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr10 in config.dt.Rows)
                            {
                                rs.int_rate = !Convert.IsDBNull(dr10["INT_RATE"]) ? Convert.ToDecimal(dr10["INT_RATE"]) : Convert.ToDecimal("00");
                            }
                        }
                    }
                    if (rs.ac_hd == "SL4")
                    {
                        TOTSL4 = (TOTSL4 + rs.prin_amt);
                        TOTISL4 = (TOTISL4 + rs.int_amt);
                        sql = "select * from loan_MASTER where employee_id='" + rs.emp_id + "'AND AC_HD='SL4' AND CLOS_FLAG IS NULL order by employee_id";
                        config.singleResult(sql);
                        if (config.dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr11 in config.dt.Rows)
                            {
                                rs.int_rate = !Convert.IsDBNull(dr11["INT_RATE"]) ? Convert.ToDecimal(dr11["INT_RATE"]) : Convert.ToDecimal("00");
                            }
                        }
                    }
                    if (rs.ac_hd == "SL6")
                    {
                        TOTSL6 = (TOTSL6 + rs.prin_amt);
                        TOTISL6 = (TOTISL6 + rs.int_amt);
                        sql = "select * from loan_MASTER where employee_id='" + rs.emp_id + "'AND AC_HD='SL6' AND CLOS_FLAG IS NULL order by employee_id";
                        config.singleResult(sql);
                        if (config.dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr12 in config.dt.Rows)
                            {
                                rs.int_rate = !Convert.IsDBNull(dr12["INT_RATE"]) ? Convert.ToDecimal(dr12["INT_RATE"]) : Convert.ToDecimal("00");
                            }
                        }
                    }
                    if (rs.ac_hd == "SL7")
                    {
                        TOTSL7 = (TOTSL7 + rs.prin_amt);
                        TOTISL7 = (TOTISL7 + rs.int_amt);
                        sql = "select * from loan_MASTER where employee_id='" + rs.emp_id + "'AND AC_HD='SL7' AND CLOS_FLAG IS NULL order by employee_id";
                        config.singleResult(sql);
                        if (config.dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr13 in config.dt.Rows)
                            {
                                rs.int_rate = !Convert.IsDBNull(dr13["INT_RATE"]) ? Convert.ToDecimal(dr13["INT_RATE"]) : Convert.ToDecimal("00");
                            }
                        }
                    }
                    if (rs.ac_hd == "SJL1")
                    {
                        TOTSJL1 = (TOTSJL1 + rs.prin_amt);
                        TOTISJL1 = (TOTISJL1 + rs.int_amt);
                        sql = "select * from loan_MASTER where employee_id='" + rs.emp_id + "'AND AC_HD='SJL1' AND CLOS_FLAG IS NULL order by employee_id";
                        config.singleResult(sql);
                        if (config.dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr14 in config.dt.Rows)
                            {
                                rs.int_rate = !Convert.IsDBNull(dr14["INT_RATE"]) ? Convert.ToDecimal(dr14["INT_RATE"]) : Convert.ToDecimal("00");
                            }
                        }
                    }
                    rs.TOTDLL = TOTDLL;
                    rs.TOTIDLL = TOTIDLL;
                    rs.TOTTF = TOTTF;
                    rs.TOTRTB = TOTRTB;
                    rs.TOTSH = TOTSH;
                    rs.TOTLICP = TOTLICP;
                    rs.TOTSFL = TOTSFL;
                    rs.TOTISFL = TOTISFL;
                    rs.TOTISJL = TOTISJL;
                    rs.TOTSJL = TOTSJL;
                    rs.TOTPSL = TOTPSL;
                    rs.TOTIPSL = TOTIPSL;
                    rs.TOTSL3 = TOTSL3;
                    rs.TOTSL3I = TOTSL3I;
                    rs.TOTM12 = TOTM12;
                    rs.TOTIM12 = TOTIM12;
                    rs.TOTM14 = TOTM14;
                    rs.TOTIM14 = TOTIM14;
                    rs.TOTIPSL1 = TOTIPSL1;
                    rs.TOTPSL1 = TOTPSL1;
                    rs.TOTSFL1 = TOTSFL1;
                    rs.TOTISFL1 = TOTISFL1;
                    rs.TOTSL4 = TOTSL4;
                    rs.TOTISL4 = TOTISL4;
                    rs.TOTSL6 = TOTSL6;
                    rs.TOTISL6 = TOTISL6;
                    rs.TOTSL7 = TOTSL7;
                    rs.TOTISL7 = TOTISL7;
                    rs.TOTSJL1 = TOTSJL1;
                    rs.TOTISJL1 = TOTISJL1;
                    rs.TOT_Deductable_Amount = rs.TOTTF + rs.TOTRTB + rs.TOTSFL + rs.TOTISFL + rs.TOTSJL + rs.TOTISJL + rs.TOTPSL + rs.TOTIPSL + rs.TOTDLL + rs.TOTIDLL + rs.TOTSL3 + rs.TOTSL3I + rs.TOTM12 + rs.TOTIM12 + rs.TOTM14 + rs.TOTIM14 + rs.TOTPSL1 + rs.TOTIPSL1 + rs.TOTSFL1 + rs.TOTISFL1 + rs.TOTSL4 + rs.TOTISL4 + rs.TOTSH + rs.TOTSJL1 + rs.TOTISJL1 + rs.TOTSL6 + rs.TOTISL6 + rs.TOTSL7 + rs.TOTISL7 + rs.TOTLICP;
                    rgl.Add(rs);
                }
            }
            return rgl;
        }
        public List<Recovery_Schedule> getdetailsForDeductionSchedule(string emp_name, string branch, string mem_type, string mem_cat, string book_no, string sch_date)
        {
            string XBOOK_NO = "";
            string xemployee_ID = "";
            string XMEMBER_NM = "";
            string MAST_FLAG = "";
            string LED_TAB = "";
            List<Recovery_Schedule> rgl = new List<Recovery_Schedule>();
            string sql = string.Empty;
            sql = "SELECT * FROM MEMBER_MAST WHERE  EMPLOYER_CD='" + emp_name + "'";
            sql = sql + "  AND EMPLOYER_BRANCH='" + branch + "'";
            sql = sql + " AND  MEMBER_TYPE='" + mem_type + "' ";
            sql = sql + " AND MEM_CATEGORY='" + mem_cat + "' and book_no='" + book_no + "' and  MEMBER_RETIRED IS NULL";
            sql = sql + " AND MEMBER_TRANSFERED IS NULL AND IS_DEAD='A' AND MEMBER_CLOSED IS NULL ";
            sql = sql + " ORDER BY EMPLOYER_CD,EMPLOYER_BRANCH,BOOK_NO,EMPLOYEE_ID";
            sql = sql + "ORDER BY BOOK_NO,EMPLOYEE_ID,AC_HD,VCH_PACNO";
            if (book_no == "AL")
            {
                sql = "SELECT * FROM MEMBER_MAST WHERE   EMPLOYER_CD='" + emp_name + "'";
                sql = sql + "  AND EMPLOYER_BRANCH='" + branch + "'";
                sql = sql + " AND MEMBER_TYPE='" + mem_type + "'";
                sql = sql + " AND MEM_CATEGORY='" + mem_cat + "' and  MEMBER_RETIRED IS NULL";
                sql = sql + " AND MEMBER_TRANSFERED IS NULL AND IS_DEAD='A' AND MEMBER_CLOSED IS NULL AND BOOK_NO<>'LT' AND BOOK_NO<> 'ST'";
                sql = sql + " ORDER BY EMPLOYER_CD,EMPLOYER_BRANCH,BOOK_NO,EMPLOYEE_ID";
            }
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    Recovery_Schedule rs = new Recovery_Schedule();
                    XBOOK_NO = Convert.ToString(dr["book_no"]);
                    xemployee_ID = Convert.ToString(dr["EMPLOYEE_ID"]);
                    XMEMBER_NM = Convert.ToString(dr["MEMBER_NAME"]);

                    string ACHD_QRY = "SELECT * FROM DEDUCT_ACHD_MAST WHERE EMPLOYER_CD='" + emp_name + "' ";
                    ACHD_QRY = ACHD_QRY + "AND EMPLOYER_BRANCH='" + branch + "' ORDER BY AC_HD ";
                    config.singleResult(ACHD_QRY);
                    if (config.dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr1 in config.dt.Rows)
                        {
                            sql = "Select * from acc_head where ac_hd='" + Convert.ToString(dr["ac_hd"]) + "'";
                            config.singleResult(sql);
                            if (config.dt.Rows.Count > 0)
                            {
                                DataRow drac = (DataRow)config.dt.Rows[0];
                                MAST_FLAG = Convert.ToString(drac["AC_LF_MAST_FL"]);
                                if (MAST_FLAG == "L")
                                {
                                    LED_TAB = Convert.ToString(drac["LEDGER_TAB"]);
                                }
                            }
                        }
                    }
                }
            }
            return rgl;
        }
        public List<Recovery_Schedule> getdetailsForRecoveryFrmSalartDeduction(string deduc_achd, string branch, string empcd, string empbranch, string bookno, string sch_date)
        {
            string XMAN_NO = "";
            string YMAN_NO = "";
            List<Recovery_Schedule> rgl = new List<Recovery_Schedule>();
            string sql = string.Empty;
            sql = "SELECT * FROM RECOVERY_SCHEDULE WHERE BRANCH_ID='" + branch + "' ";
            sql = sql + "AND EMPLOYER_CD='" + empcd + "' AND EMPLOYER_BRANCH='" + empbranch + "'AND BOOK_NO='" + bookno + "'";
            sql = sql + " AND convert(varchar, SCH_DATE, 103) = convert(varchar, '" + sch_date + "', 103) ";
            if (deduc_achd != "Select")
            {
                sql = sql + "AND AC_HD='" + deduc_achd + "' ";
            }
            sql = sql + "ORDER BY BOOK_NO,EMPLOYEE_ID,AC_HD,VCH_PACNO";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                DataRow fs1 = (DataRow)config.dt.Rows[0];
                XMAN_NO = Convert.ToString(fs1["book_no"]) + Convert.ToString(fs1["EMPLOYEE_ID"]);
                foreach (DataRow dr in config.dt.Rows)
                {
                    Recovery_Schedule rs = new Recovery_Schedule();
                    YMAN_NO = Convert.ToString(dr["book_no"]) + Convert.ToString(dr["EMPLOYEE_ID"]);
                    rs.emp_id = Convert.ToString(dr["EMPLOYEE_ID"]);
                    rs.branch_id = Convert.ToString(dr["branch_id"]);
                    rs.employer_cd = Convert.ToString(dr["employer_cd"]);
                    rs.employer_branch = Convert.ToInt32(dr["employer_branch"]);
                    rs.sch_date = Convert.ToDateTime(dr["sch_date"]).ToString("dd/MM/yyyy").Replace("-","/");
                    rs.ac_hd = Convert.ToString(dr["AC_HD"]);
                    rs.mem_name = Convert.ToString(dr["MEMBER_NAME"]);
                    rs.book_no = Convert.ToString(dr["book_no"]);
                    rs.vch_pacno = Convert.ToString(dr["vch_pacno"]);
                    rs.prin_bal = !Convert.IsDBNull(dr["PRIN_BAL"]) ? Convert.ToDecimal(dr["PRIN_BAL"]) : Convert.ToDecimal("00");
                    rs.prin_amt = !Convert.IsDBNull(dr["PRIN_AMT"]) ? Convert.ToDecimal(dr["PRIN_AMT"]) : Convert.ToDecimal("00");
                    rs.int_amt = !Convert.IsDBNull(dr["INT_AMT"]) ? Convert.ToDecimal(dr["INT_AMT"]) : Convert.ToDecimal("00");
                    rgl.Add(rs);
                }
            }
            return rgl;
        }
        public void SaveDataInRecoverySchedule(Recovery_Schedule rs, string emp_name, string unit, string mem_type, string mem_cat, string book_no, string sch_date, string branch ,string xemployee_ID)
        {
            string SDL_QRY = "";
            SDL_QRY = "select * from recovery_schedule where branch_id='" + branch + "'";
            SDL_QRY = SDL_QRY + " and employer_cd='" + emp_name + "' and EMPLOYEE_ID ='"+xemployee_ID+"'";
            SDL_QRY = SDL_QRY + " and ac_hd='" + rs.r4 + "' and ";
            SDL_QRY = SDL_QRY + "convert(datetime, SCH_DATE, 103) = convert(datetime, '" + sch_date + "', 103)";
            SDL_QRY = SDL_QRY + " AND MEMBER_TYPE='" + mem_type + "' ";
            SDL_QRY = SDL_QRY + " AND MEM_CATEGORY='" + mem_cat + "'and BOOK_NO='" + rs.r1 + "'";
            config.singleResult(SDL_QRY);
            List<Recovery_Schedule> rslst = new List<Recovery_Schedule>();
            if (config.dt.Rows.Count > 0)
            {
                SDL_QRY = "delete from recovery_schedule where branch_id='" + branch + "'";
                SDL_QRY = SDL_QRY + " and employer_cd='" + emp_name + "' and EMPLOYEE_ID ='" + xemployee_ID + "' ";
                SDL_QRY = SDL_QRY + " and ac_hd='" + rs.r4 + "' and ";
                SDL_QRY = SDL_QRY + "convert(datetime, SCH_DATE, 103) = convert(datetime, '" + sch_date + "', 103)";
                SDL_QRY = SDL_QRY + " AND MEMBER_TYPE='" + mem_type + "' ";
                SDL_QRY = SDL_QRY + " AND MEM_CATEGORY='" + mem_cat + "'and BOOK_NO='" + rs.r1 + "'";
                config.Execute_Query(SDL_QRY);            
            }
            SDL_QRY = "INSERT INTO recovery_schedule(BRANCH_ID, EMPLOYER_CD, EMPLOYER_BRANCH, SCH_DATE, book_no, EMPLOYEE_ID , member_name, MEMBER_TYPE,MEM_CATEGORY,ac_hd ,vch_pacno ,prin_bal ,OVER_DUE,PRIN_AMT,INT_AMT )";
            SDL_QRY = SDL_QRY + "VALUES('" + branch + "', '" + emp_name + "', '" + unit + "', convert(datetime, '" + sch_date + "', 103), '" + rs.r1 + "', '" + rs.r2 + "', '" + rs.r3 + "', '" + mem_type + "', '" + mem_cat + "', '" + rs.r4 + "', '" + rs.r5 + "', " + Convert.ToDecimal(rs.r6) + ", " + Convert.ToDecimal(rs.r7) + ", " + Convert.ToDecimal(rs.r8) + ", " + Convert.ToDecimal(rs.r9) + ")";
            config.Execute_Query(SDL_QRY);
            //else
            //{
            //    if(rs.r4== "LICP")
            //    {
            //        SDL_QRY = "INSERT INTO recovery_schedule(BRANCH_ID, EMPLOYER_CD, EMPLOYER_BRANCH, SCH_DATE, book_no, EMPLOYEE_ID , member_name, MEMBER_TYPE,MEM_CATEGORY,ac_hd ,vch_pacno ,prin_bal ,OVER_DUE,PRIN_AMT,INT_AMT )";
            //        SDL_QRY = SDL_QRY + "VALUES('" + branch + "', '" + emp_name + "', '" + unit + "', convert(datetime, '" + sch_date + "', 103), '" + book_no + "', '" + rs.r2 + "', '" + rs.r3 + "', '" + mem_type + "', '" + mem_cat + "', '" + rs.r4 + "', '" + rs.r5 + "', " + Convert.ToDecimal(rs.r6) + ", " + Convert.ToDecimal(rs.r7) + ", " + Convert.ToDecimal(rs.r8) + ", " + Convert.ToDecimal(rs.r9) + ")";

            //        config.Execute_Query(SDL_QRY);
            //    }
            //}
        }
        public List<Recovery_Schedule> getdetailsForDeductionSchedule(string emp_name, string unit, string mem_type, string mem_cat, string book_no, string sch_date, string branch)
        {
            decimal totalR7 = 0;
            decimal totalR8 = 0;
            string XBOOK_NO = "";
            string xemployee_ID = "";
            string XMEMBER_NM = "";
            string MAST_FLAG = "";
            string LED_TAB = "";
            string MEMBER = "";
            decimal INT_RATE = 0;
            decimal prin_bal = 0;
            decimal INT_DUE = 0;
            decimal ACT_PAYABLE = 0;
            decimal ACT_PAID = 0;
            decimal PRIN_DUE = 0;
            decimal XPRIN_BAL = 0;
            decimal XPRIN_AMT = 0;
            decimal XPRIN_AMOUNT = 0;
            decimal XINT_CAL = 0;
            decimal YPRIN_BAL = 0;
            decimal YINT_CAL = 0;
            decimal INT_CAL = 0;
            decimal Tot_Due = 0;
            decimal dd = 0;
            int NO_OF_MONTH = 0;
            int NO_OF_OD = 0;
            int xcal_date = 0;
            decimal xirate = 0;
            bool CAL_LOAN_DUE = false;
            bool CAL_TF_DUE = false;
            // bool CAL_SHARE_DUE = false;
            bool CAL_RTB_DUE = false;
            DateTime xLoan_Dt;
            DateTime tf_eff_date;
            List<Recovery_Schedule> rslst = new List<Recovery_Schedule>();
            string sql = string.Empty;
            sql = "SELECT * FROM MEMBER_MAST WHERE  EMPLOYER_CD='" + emp_name + "'";
            sql = sql + "  AND EMPLOYER_BRANCH='" + unit + "'";
            sql = sql + " AND  MEMBER_TYPE='" + mem_type + "' ";
            sql = sql + " AND MEM_CATEGORY='" + mem_cat + "' and book_no='" + book_no + "' and  MEMBER_RETIRED IS NULL";
            sql = sql + " AND MEMBER_TRANSFERED IS NULL AND IS_DEAD='A' AND MEMBER_CLOSED IS NULL ";
            sql = sql + " ORDER BY EMPLOYER_CD,EMPLOYER_BRANCH,BOOK_NO,EMPLOYEE_ID";
            if (book_no == "AL")
            {
                sql = "SELECT * FROM MEMBER_MAST WHERE   EMPLOYER_CD='" + emp_name + "'";
                sql = sql + "  AND EMPLOYER_BRANCH='" + unit + "'";
                sql = sql + " AND MEMBER_TYPE='" + mem_type + "'";
                sql = sql + " AND MEM_CATEGORY='" + mem_cat + "' and  MEMBER_RETIRED IS NULL";
                sql = sql + " AND MEMBER_TRANSFERED IS NULL AND IS_DEAD='A' AND MEMBER_CLOSED IS NULL AND BOOK_NO<>'LT' AND BOOK_NO<> 'ST' And Book_no<>'00' And Book_no<>'01'";
                sql = sql + " ORDER BY EMPLOYER_CD,EMPLOYER_BRANCH,BOOK_NO,EMPLOYEE_ID";
            }
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    Recovery_Schedule rs1 = new Recovery_Schedule();
                    XBOOK_NO = Convert.ToString(dr["book_no"]);
                    xemployee_ID = Convert.ToString(dr["EMPLOYEE_ID"]);
                    XMEMBER_NM = Convert.ToString(dr["MEMBER_NAME"]);
                    MEMBER = Convert.ToString(dr["member_id"]);
                    string ACHD_QRY = "SELECT * FROM DEDUCT_ACHD_MAST WHERE EMPLOYER_CD='" + emp_name + "' ";
                    ACHD_QRY = ACHD_QRY + "AND EMPLOYER_BRANCH='" + unit + "' ORDER BY AC_HD ";
                    config.singleResult(ACHD_QRY);
                    if (config.dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr1 in config.dt.Rows)
                        {
                            Recovery_Schedule rs = new Recovery_Schedule();
                            string Acc_Head = Convert.ToString(dr1["ac_hd"]);
                            sql = "Select * from acc_head where ac_hd='" + Acc_Head + "'";
                            config.singleResult(sql);
                            if (config.dt.Rows.Count > 0)
                            {
                                DataRow drac = (DataRow)config.dt.Rows[0];
                                MAST_FLAG = Convert.ToString(drac["AC_LF_MAST_FL"]);
                                if (MAST_FLAG == "L") //--------Loan Master--------------
                                {
                                    LED_TAB = Convert.ToString(drac["LEDGER_TAB"]);
                                    sql = " select * from LOAN_MASTER where branch_id = '" + branch + "' and ac_hd = '" + Acc_Head + "' " +
                                        "and EMPLOYEE_ID = '" + xemployee_ID + "' And convert(datetime, loan_date, 103) <= convert(datetime, '" + sch_date + "', 103) AND" +
                                        " CLOS_FLAG IS NULL AND CLOS_DATE IS NULL AND FLAG IS NULL";
                                    config.singleResult(sql);
                                    if (config.dt.Rows.Count > 0)
                                    {
                                        DataRow drlm = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                                        string rSource = "SELECT * FROM LOAN_LEDGER WHERE BRANCH_ID='" + branch + "' AND ";
                                        rSource = rSource + "AC_HD='" + Acc_Head + "' and  EMPLOYEE_ID='" + xemployee_ID + "' AND convert(datetime, VCH_DATE, 103) <= convert(datetime, '" + sch_date + "', 103) ";
                                        rSource = rSource + "ORDER BY BRANCH_ID,AC_HD,employee_ID,VCH_DATE,VCH_NO,VCH_SRL";
                                        config.singleResult(rSource);
                                        if (config.dt.Rows.Count > 0)
                                        {
                                            DataRow drl = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                                            INT_RATE = Convert.ToDecimal(drlm["INT_RATE"]);
                                            // JLN_DATE = !loan_date
                                            prin_bal = !Convert.IsDBNull(drl["PRIN_BAL"]) ? Convert.ToDecimal(drl["PRIN_BAL"]) : Convert.ToDecimal("00");
                                            INT_DUE = !Convert.IsDBNull(drl["INT_DUE"]) ? Convert.ToDecimal(drl["INT_DUE"]) : Convert.ToDecimal("00");
                                            NO_OF_MONTH = Convert.ToInt32(Convert.ToDateTime(sch_date).Subtract(Convert.ToDateTime(drlm["loan_date"])).Days / (365.25 / 12));
                                            ACT_PAYABLE = Convert.ToDecimal(drlm["INSTL_AMOUNT"]) * NO_OF_MONTH;
                                            ACT_PAID = Convert.ToDecimal(drlm["loan_amt"]) - prin_bal;
                                            PRIN_DUE = ACT_PAYABLE - ACT_PAID;
                                            if (PRIN_DUE > 0) NO_OF_OD = Convert.ToInt32(PRIN_DUE / Convert.ToDecimal(drlm["INSTL_AMOUNT"]));
                                            if (NO_OF_OD == 0) NO_OF_OD = 1;
                                            xLoan_Dt = Convert.ToDateTime(drlm["loan_date"]);

                                            if (Convert.ToDateTime(drlm["loan_date"]).Month == Convert.ToDateTime(sch_date).Month)
                                            {
                                                if (Convert.ToDateTime(drlm["loan_date"]).Year == Convert.ToDateTime(sch_date).Year)
                                                {
                                                    if (NO_OF_MONTH == 0) NO_OF_OD = 1;
                                                }
                                            }
                                            if (Convert.ToDateTime(drlm["loan_date"]).Month == Convert.ToDateTime(sch_date).Month && Convert.ToDateTime(drlm["loan_date"]).Year == Convert.ToDateTime(sch_date).Year)
                                            {
                                                xLoan_Dt = Convert.ToDateTime(drlm["loan_date"]);
                                                NO_OF_MONTH = Convert.ToInt32(Convert.ToDateTime(sch_date).Subtract(Convert.ToDateTime(drlm["loan_date"])).Days / (365.25 / 12));
                                                var lndate = Convert.ToDateTime(drlm["loan_date"]);
                                                var scdate = Convert.ToDateTime(sch_date);
                                                var diffOfDates = scdate - lndate;
                                                xcal_date = diffOfDates.Days;
                                                XPRIN_BAL = Convert.ToDecimal(drl["prin_bal"]);
                                                drl = (DataRow)config.dt.Rows[0];
                                                if (Convert.ToDateTime(drl["VCH_DATE"]) == xLoan_Dt)
                                                {
                                                    XPRIN_AMT = Convert.ToDecimal(drl["PRIN_AMOUNT"]);
                                                }
                                                XPRIN_AMOUNT = Math.Round(XPRIN_BAL - XPRIN_AMT);
                                                XINT_CAL = (XPRIN_AMT * INT_RATE) / 36500 * xcal_date;
                                                YPRIN_BAL = prin_bal - XPRIN_AMT;
                                                YINT_CAL = YPRIN_BAL * INT_RATE / 100 / 12;
                                                INT_CAL = Math.Round(XINT_CAL + YINT_CAL);
                                            }
                                            else
                                            {
                                                INT_CAL = Math.Round(((prin_bal * INT_RATE) / 100) / 12, 0);
                                            }
                                            if (prin_bal > Convert.ToDecimal(drlm["INSTL_AMOUNT"]))
                                            {
                                                dd = Convert.ToDecimal(drlm["INSTL_AMOUNT"]);
                                            }
                                            else
                                            {
                                                dd = prin_bal;
                                            }
                                            dd = prin_bal > Convert.ToDecimal(drlm["INSTL_AMOUNT"]) ? Convert.ToDecimal(drlm["INSTL_AMOUNT"]) : prin_bal;
                                            if (Convert.ToDecimal(drl["prin_bal"]) != 0 && dd != 0)
                                            {
                                                rs.r1 = XBOOK_NO;
                                                rs.r2 = xemployee_ID;
                                                rs.r3 = XMEMBER_NM;
                                                rs.r4 = Acc_Head;
                                                rs.r5 = Convert.ToString(drl["EMPLOYEE_ID"]);
                                                rs.r6 = Convert.ToDecimal(drl["prin_bal"]).ToString("0.00");
                                                rs.r7 = NO_OF_OD.ToString("0.00");
                                                rs.r8 = dd.ToString("0.00");
                                                rs.r9 = INT_CAL.ToString("0.00");
                                                rs.r11 = prin_bal.ToString("0.00");
                                                rs.r12 = INT_DUE.ToString("0.00");
                                                rs.r13 = INT_RATE.ToString("0.00") + "%";
                                                rs.SaveDataInRecoverySchedule(rs, emp_name, unit, mem_type, mem_cat, book_no, sch_date, branch , xemployee_ID);
                                                rslst.Add(rs);
                                                CAL_LOAN_DUE = true;
                                            }
                                            if (CAL_LOAN_DUE == true)
                                            {
                                                Tot_Due = Tot_Due + dd+ INT_CAL;
                                                totalR7 = totalR7 + dd;
                                                totalR8 = totalR8 + INT_CAL;
                                            }
                                        }
                                        CAL_LOAN_DUE = false;
                                    }
                                }
                                if (MAST_FLAG == "M") //--------MEMBERSHIP MASTER FOR THRIFT FUND--------------
                                {
                                    LED_TAB = Convert.ToString(drac["LEDGER_TAB"]);
                                    switch (LED_TAB)
                                    {
                                        case "TF_LEDGER":
                                            decimal XXtf;
                                            decimal XTF = 0;
                                            decimal ACT_DUE = 0;

                                            sql = "SELECT * FROM TF_RATE ORDER BY EFF_DATE";
                                            config.singleResult(sql);
                                            if (config.dt.Rows.Count > 0)
                                            {
                                                DataRow drtr = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                                                if (Convert.ToDateTime(drtr["Eff_date"]) <= Convert.ToDateTime(sch_date))
                                                {
                                                    tf_eff_date = Convert.ToDateTime(drtr["Eff_date"]);
                                                    xirate = Convert.ToDecimal(drtr["TF_RATE"]);
                                                }
                                            }
                                            sql = "select * from member_mast where member_id='" + MEMBER + "'order by member_id";
                                            config.singleResult(sql);
                                            if (config.dt.Rows.Count > 0)
                                            {
                                                DataRow drmm = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                                                XXtf = !Convert.IsDBNull(dr["BLOOD_GROUP"]) ? Convert.ToDecimal(dr["BLOOD_GROUP"]) : Convert.ToDecimal("0");
                                                if (XXtf > 0)
                                                {
                                                    XTF = XXtf;
                                                }
                                                else
                                                {
                                                    XTF = 100;
                                                }
                                            }
                                            ACT_DUE = 0;
                                            prin_bal = 0;
                                            sql = "SELECT* FROM TF_LEDGER WHERE BRANCH_ID = '" + branch + "' AND MEMBER_ID='" + MEMBER + "' AND convert(datetime, VCH_DATE, 103) <= convert(datetime, '" + sch_date + "', 103) ORDER BY BRANCH_ID,MEMBER_ID,VCH_DATE,VCH_NO,VCH_SRL";
                                            config.singleResult(sql);
                                            if (config.dt.Rows.Count > 0)
                                            {
                                                DataRow drtl = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                                                sql = "SELECT * FROM TF_RATE ORDER BY EFF_DATE";
                                                config.singleResult(sql);
                                                if (config.dt.Rows.Count > 0)
                                                {
                                                    DataRow drtr = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                                                    xirate = xirate = Convert.ToDecimal(drtr["TF_RATE"]);
                                                }
                                                prin_bal = !Convert.IsDBNull(drtl["prin_bal"]) ? Convert.ToDecimal(drtl["prin_bal"]) : Convert.ToDecimal("0");
                                                ACT_DUE = xirate;
                                            }
                                            else
                                            {
                                                ACT_DUE = xirate;
                                            }
                                            rs.r1 = XBOOK_NO;
                                            rs.r2 = xemployee_ID;
                                            rs.r3 = XMEMBER_NM;
                                            rs.r4 = Acc_Head;
                                            rs.r5 = MEMBER;
                                            rs.r6 = prin_bal.ToString("0.00");
                                            rs.r8 = XTF.ToString("0.00");
                                            rs.SaveDataInRecoverySchedule(rs, emp_name, unit, mem_type, mem_cat, book_no, sch_date, branch, xemployee_ID);
                                            rslst.Add(rs);
                                            CAL_TF_DUE = true;
                                            if (CAL_TF_DUE == true)
                                            {
                                                Tot_Due = Tot_Due + XTF;
                                                totalR7 = totalR7 + XTF;
                                            }
                                            break;
                                        case "LICP_LEDGER":
                                            string xmem = "";
                                            string XBK = "";
                                            string xnm = "";
                                            sql = "SELECT * FROM LOAN_MASTER WHERE BRANCH_ID='MN' AND EMPLOYEE_ID='" + xemployee_ID + "' and LIC_PREMIUM IS NOT NULL order by ac_hd,employee_id";
                                            config.singleResult(sql);
                                            if (config.dt.Rows.Count > 0)
                                            {
                                                foreach (DataRow dr2 in config.dt.Rows)
                                                {
                                                    sql = "select * from member_mast where branch_id='MN' AND EMPLOYEE_ID='" + xemployee_ID + "' and member_closed is null ";
                                                    config.singleResult(sql);
                                                    if (config.dt.Rows.Count > 0)
                                                    {
                                                        DataRow drmm = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                                                        xmem = Convert.ToString(drmm["member_id"]);
                                                        XBK = Convert.ToString(drmm["book_no"]);
                                                        xnm = Convert.ToString(drmm["member_name"]);
                                                    }
                                                    rs.r1 = XBOOK_NO;
                                                    rs.r2 = xemployee_ID;
                                                    rs.r3 = XMEMBER_NM;
                                                    rs.r4 = "LICP";
                                                    rs.r5 = xmem;
                                                    rs.SaveDataInRecoverySchedule(rs, emp_name, unit, mem_type, mem_cat, book_no, sch_date, branch, xemployee_ID);
                                                    rslst.Add(rs);
                                                    break;
                                                }
                                            }
                                            break;
                                        //case "SHARE_LEDGER":
                                        //    decimal XXsh = 0;
                                        //    sql = "select * from member_mast where member_id='" + MEMBER + "'order by member_id";
                                        //    config.singleResult(sql);
                                        //    if (config.dt.Rows.Count > 0)
                                        //    {
                                        //        DataRow drmm = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                                        //        XXsh = !Convert.IsDBNull(drmm["SHARE"]) ? Convert.ToDecimal(drmm["SHARE"]) : Convert.ToDecimal("00");
                                        //        if (XXsh > 0)
                                        //        {
                                        //            XTF = XXsh;
                                        //        }
                                        //        else
                                        //        {
                                        //            XTF = 0;
                                        //        }
                                        //    }
                                        //    DUE_MN = 0;
                                        //    DUE_AMT = 0;
                                        //    ACT_DUE = 0;
                                        //    prin_bal = 0;
                                        //    sql = "SELECT * FROM SHARE_LEDGER WHERE BRANCH_ID='" + branch + "' AND MEMBER_ID='" + MEMBER + "' AND convert(datetime, VCH_DATE, 103) <= convert(datetime, '" + sch_date + "', 103) ORDER BY BRANCH_ID,MEMBER_ID,VCH_DATE,VCH_NO,VCH_SRL";
                                        //    config.singleResult(sql);
                                        //    if (config.dt.Rows.Count > 0)
                                        //    {
                                        //        DataRow drsl = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                                        //        prin_bal = !Convert.IsDBNull(drsl["BAL_AMOUNT"]) ? Convert.ToDecimal(drsl["BAL_AMOUNT"]) : Convert.ToDecimal("00");
                                        //    }
                                        //    else
                                        //    {
                                        //        prin_bal = 0;
                                        //    }
                                        //   
                                        //    CAL_SHARE_DUE = true;
                                        //    if (CAL_SHARE_DUE == true)
                                        //    {
                                        //        Tot_Due = Tot_Due + XXsh;
                                        //        totalR7 = totalR7 + XXsh;

                                        //    }
                                        //    break;
                                        case "RTB_LEDGER":
                                            sql = "SELECT * FROM TF_RATE ORDER BY EFF_DATE";
                                            config.singleResult(sql);
                                            if (config.dt.Rows.Count > 0)
                                            {
                                                DataRow drtr = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                                                if (Convert.ToDateTime(drtr["Eff_date"]) <= Convert.ToDateTime(sch_date))
                                                {
                                                    tf_eff_date = Convert.ToDateTime(drtr["Eff_date"]);
                                                    xirate = Convert.ToDecimal(drtr["TF_RATE"]);
                                                }
                                            }
                                            ACT_DUE = 0;
                                            prin_bal = 0;
                                            sql = "SELECT * FROM RTB_LEDGER WHERE BRANCH_ID='" + branch + "' AND MEMBER_ID='" + MEMBER + "' AND convert(datetime, VCH_DATE, 103) <= convert(datetime, '" + sch_date + "', 103) ORDER BY BRANCH_ID,MEMBER_ID,VCH_DATE,VCH_NO,VCH_SRL";
                                            config.singleResult(sql);
                                            if (config.dt.Rows.Count > 0)
                                            {
                                                DataRow drrl = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                                                sql = "SELECT * FROM RTB_RATE ORDER BY EFF_DATE";
                                                config.singleResult(sql);
                                                if (config.dt.Rows.Count > 0)
                                                {
                                                    DataRow drrr = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                                                    xirate = Convert.ToDecimal(drrr["RTB_RATE"]);
                                                }
                                                prin_bal = !Convert.IsDBNull(drrl["prin_bal"]) ? Convert.ToDecimal(drrl["prin_bal"]) : Convert.ToDecimal("0");
                                                ACT_DUE = xirate;
                                            }
                                            else
                                            {
                                                ACT_DUE = xirate;
                                            }
                                            rs.r1 = XBOOK_NO;
                                            rs.r2 = xemployee_ID;
                                            rs.r3 = XMEMBER_NM;
                                            rs.r4 = Acc_Head;
                                            rs.r5 = MEMBER;
                                            rs.r6 = prin_bal.ToString("0.00");
                                            rs.r8 = xirate.ToString("0.00");
                                            rs.SaveDataInRecoverySchedule(rs, emp_name, unit, mem_type, mem_cat, book_no, sch_date, branch, xemployee_ID);
                                            rslst.Add(rs);
                                            CAL_RTB_DUE = true;
                                            if (CAL_RTB_DUE == true)
                                            {
                                                Tot_Due = Tot_Due + xirate;
                                                totalR7 = totalR7 + xirate;
                                            }
                                            break;
                                    }
                                }
                            }
                        }
                    }
                    rs1.r9 = "TOTAL";
                    rs1.r10 = Tot_Due.ToString("0.00");
                    rslst.Add(rs1);
                    Tot_Due = 0;
                }
            }
            return rslst;
        }
        public List<Recovery_Schedule> getdetailsForDeductionScheduleAfterUpdate(string emp_name, string unit, string mem_type, string mem_cat, string book_no, string sch_date, string branch)
        {
            decimal totalR7 = 0;
            decimal totalR8 = 0;
            string XBOOK_NO = "";
            string xemployee_ID = "";
            string XMEMBER_NM = "";
            string MAST_FLAG = "";
            string LED_TAB = "";
            string MEMBER = "";
            decimal INT_RATE = 0;
            decimal prin_bal = 0;
            decimal INT_DUE = 0;
            decimal ACT_PAYABLE = 0;
            decimal ACT_PAID = 0;
            decimal PRIN_DUE = 0;
            decimal XPRIN_BAL = 0;
            decimal XPRIN_AMT = 0;
            decimal XPRIN_AMOUNT = 0;
            decimal XINT_CAL = 0;
            decimal YPRIN_BAL = 0;
            decimal YINT_CAL = 0;
            decimal INT_CAL = 0;
            decimal Tot_Due = 0;
            decimal dd = 0;
            int NO_OF_MONTH = 0;
            int NO_OF_OD = 0;
            int xcal_date = 0;
            decimal xirate = 0;
            bool CAL_LOAN_DUE = false;
            bool CAL_TF_DUE = false;
            // bool CAL_SHARE_DUE = false;
            bool CAL_RTB_DUE = false;
            DateTime xLoan_Dt;
            DateTime tf_eff_date;
            List<Recovery_Schedule> rslst = new List<Recovery_Schedule>();
            string sql = string.Empty;
            sql = "SELECT * FROM MEMBER_MAST WHERE  EMPLOYER_CD='" + emp_name + "'";
            sql = sql + "  AND EMPLOYER_BRANCH='" + unit + "'";
            sql = sql + " AND  MEMBER_TYPE='" + mem_type + "' ";
            sql = sql + " AND MEM_CATEGORY='" + mem_cat + "' and book_no='" + book_no + "' and  MEMBER_RETIRED IS NULL";
            sql = sql + " AND MEMBER_TRANSFERED IS NULL AND IS_DEAD='A' AND MEMBER_CLOSED IS NULL ";
            sql = sql + " ORDER BY EMPLOYER_CD,EMPLOYER_BRANCH,BOOK_NO,EMPLOYEE_ID";

            if (book_no == "AL")
            {
                sql = "SELECT * FROM MEMBER_MAST WHERE   EMPLOYER_CD='" + emp_name + "'";
                sql = sql + "  AND EMPLOYER_BRANCH='" + unit + "'";
                sql = sql + " AND MEMBER_TYPE='" + mem_type + "'";
                sql = sql + " AND MEM_CATEGORY='" + mem_cat + "' and  MEMBER_RETIRED IS NULL";
                sql = sql + " AND MEMBER_TRANSFERED IS NULL AND IS_DEAD='A' AND MEMBER_CLOSED IS NULL AND BOOK_NO<>'LT' AND BOOK_NO<>'00' AND BOOK_NO<>'01' AND BOOK_NO<> 'ST'";
                sql = sql + " ORDER BY EMPLOYER_CD,EMPLOYER_BRANCH,BOOK_NO,EMPLOYEE_ID";
            }
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    Recovery_Schedule rs1 = new Recovery_Schedule();
                    XBOOK_NO = Convert.ToString(dr["book_no"]);
                    xemployee_ID = Convert.ToString(dr["EMPLOYEE_ID"]);
                    XMEMBER_NM = Convert.ToString(dr["MEMBER_NAME"]);
                    MEMBER = Convert.ToString(dr["member_id"]);
                    string ACHD_QRY = "SELECT * FROM DEDUCT_ACHD_MAST WHERE EMPLOYER_CD='" + emp_name + "' ";
                    ACHD_QRY = ACHD_QRY + "AND EMPLOYER_BRANCH='" + unit + "' ORDER BY AC_HD ";
                    config.singleResult(ACHD_QRY);
                    if (config.dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr1 in config.dt.Rows)
                        {
                            Recovery_Schedule rs = new Recovery_Schedule();
                            string Acc_Head = Convert.ToString(dr1["ac_hd"]);
                            sql = "Select * from acc_head where ac_hd='" + Acc_Head + "'";
                            config.singleResult(sql);
                            if (config.dt.Rows.Count > 0)
                            {
                                DataRow drac = (DataRow)config.dt.Rows[0];
                                MAST_FLAG = Convert.ToString(drac["AC_LF_MAST_FL"]);
                                if (MAST_FLAG == "L") //--------Loan Master--------------
                                {
                                    LED_TAB = Convert.ToString(drac["LEDGER_TAB"]);
                                    sql = " select * from LOAN_MASTER where branch_id = '" + branch + "' and ac_hd = '" + Acc_Head + "' " +
                                        "and EMPLOYEE_ID = '" + xemployee_ID + "' And convert(datetime, loan_date, 103) <= convert(datetime, '" + sch_date + "', 103) AND" +
                                        " CLOS_FLAG IS NULL AND CLOS_DATE IS NULL AND FLAG IS NULL";
                                    config.singleResult(sql);
                                    if (config.dt.Rows.Count > 0)
                                    {
                                        DataRow drlm = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];

                                        string rSource = "SELECT * FROM LOAN_LEDGER WHERE BRANCH_ID='" + branch + "' AND ";
                                        rSource = rSource + "AC_HD='" + Acc_Head + "' and  EMPLOYEE_ID='" + xemployee_ID + "' AND convert(datetime, VCH_DATE, 103) <= convert(datetime, '" + sch_date + "', 103) ";
                                        rSource = rSource + "ORDER BY BRANCH_ID,AC_HD,employee_ID,VCH_DATE,VCH_NO,VCH_SRL";
                                        config.singleResult(rSource);
                                        if (config.dt.Rows.Count > 0)
                                        {
                                            DataRow drl = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                                            INT_RATE = Convert.ToDecimal(drlm["INT_RATE"]);
                                            // JLN_DATE = !loan_date
                                            prin_bal = !Convert.IsDBNull(drl["PRIN_BAL"]) ? Convert.ToDecimal(drl["PRIN_BAL"]) : Convert.ToDecimal("00");
                                            INT_DUE = !Convert.IsDBNull(drl["INT_DUE"]) ? Convert.ToDecimal(drl["INT_DUE"]) : Convert.ToDecimal("00");
                                            NO_OF_MONTH = Convert.ToInt32(Convert.ToDateTime(sch_date).Subtract(Convert.ToDateTime(drlm["loan_date"])).Days / (365.25 / 12));
                                            ACT_PAYABLE = Convert.ToDecimal(drlm["INSTL_AMOUNT"]) * NO_OF_MONTH;
                                            ACT_PAID = Convert.ToDecimal(drlm["loan_amt"]) - prin_bal;
                                            PRIN_DUE = ACT_PAYABLE - ACT_PAID;
                                            if (PRIN_DUE > 0) NO_OF_OD = Convert.ToInt32(PRIN_DUE / Convert.ToDecimal(drlm["INSTL_AMOUNT"]));
                                            if (NO_OF_OD == 0) NO_OF_OD = 1;
                                            xLoan_Dt = Convert.ToDateTime(drlm["loan_date"]);
                                            if (Convert.ToDateTime(drlm["loan_date"]).Month == Convert.ToDateTime(sch_date).Month)
                                            {
                                                if (Convert.ToDateTime(drlm["loan_date"]).Year == Convert.ToDateTime(sch_date).Year)
                                                {
                                                    if (NO_OF_MONTH == 0) NO_OF_OD = 1;
                                                }
                                            }
                                            if (Convert.ToDateTime(drlm["loan_date"]).Month == Convert.ToDateTime(sch_date).Month && Convert.ToDateTime(drlm["loan_date"]).Year == Convert.ToDateTime(sch_date).Year)
                                            {
                                                xLoan_Dt = Convert.ToDateTime(drlm["loan_date"]);
                                                NO_OF_MONTH = Convert.ToInt32(Convert.ToDateTime(sch_date).Subtract(Convert.ToDateTime(drlm["loan_date"])).Days / (365.25 / 12));
                                                var lndate = Convert.ToDateTime(drlm["loan_date"]);
                                                var scdate = Convert.ToDateTime(sch_date);
                                                var diffOfDates = scdate - lndate;
                                                xcal_date = diffOfDates.Days;
                                                XPRIN_BAL = Convert.ToDecimal(drl["prin_bal"]);
                                                drl = (DataRow)config.dt.Rows[0];
                                                if (Convert.ToDateTime(drl["VCH_DATE"]) == xLoan_Dt)
                                                {
                                                    XPRIN_AMT = Convert.ToDecimal(drl["PRIN_AMOUNT"]);
                                                }
                                                XPRIN_AMOUNT = Math.Round(XPRIN_BAL - XPRIN_AMT);
                                                XINT_CAL = (XPRIN_AMT * INT_RATE) / 36500 * xcal_date;
                                                YPRIN_BAL = prin_bal - XPRIN_AMT;
                                                YINT_CAL = YPRIN_BAL * INT_RATE / 100 / 12;
                                                INT_CAL = Math.Round(XINT_CAL + YINT_CAL);
                                            }
                                            else
                                            {
                                                INT_CAL = Math.Round(((prin_bal * INT_RATE) / 100) / 12, 0);
                                            }
                                            if (prin_bal > Convert.ToDecimal(drlm["INSTL_AMOUNT"]))
                                            {
                                                dd = Convert.ToDecimal(drlm["INSTL_AMOUNT"]);
                                            }
                                            else
                                            {
                                                dd = prin_bal;
                                            }
                                            dd = prin_bal > Convert.ToDecimal(drlm["INSTL_AMOUNT"]) ? Convert.ToDecimal(drlm["INSTL_AMOUNT"]) : prin_bal;
                                            if (Convert.ToDecimal(drl["prin_bal"]) != 0 && dd != 0)
                                            {
                                                rs.r1 = XBOOK_NO;
                                                rs.r2 = xemployee_ID;
                                                rs.r3 = XMEMBER_NM;
                                                rs.r4 = Acc_Head;
                                                rs.r5 = Convert.ToString(drl["EMPLOYEE_ID"]);
                                                rs.r6 = Convert.ToDecimal(drl["prin_bal"]).ToString("0.00");
                                                rs.r7 = NO_OF_OD.ToString("0.00");
                                                rs.r8 = dd.ToString("0.00");
                                                rs.r9 = INT_CAL.ToString("0.00");
                                                rs.r11 = prin_bal.ToString("0.00");
                                                rs.r12 = INT_DUE.ToString("0.00");
                                                rs.r13 = INT_RATE.ToString("0.00") + "%";
                                                //rs.SaveDataInRecoverySchedule(rs, emp_name, unit, mem_type, mem_cat, book_no, sch_date, branch, xemployee_ID);
                                                rslst.Add(rs);
                                                CAL_LOAN_DUE = true;
                                            }
                                            if (CAL_LOAN_DUE == true)
                                            {
                                                Tot_Due = Tot_Due + dd + INT_CAL;
                                                totalR7 = totalR7 + dd;
                                                totalR8 = totalR8 + INT_CAL;
                                            }
                                        }
                                        CAL_LOAN_DUE = false;
                                    }
                                }
                                if (MAST_FLAG == "M") //--------MEMBERSHIP MASTER FOR THRIFT FUND--------------
                                {
                                    LED_TAB = Convert.ToString(drac["LEDGER_TAB"]);
                                    switch (LED_TAB)
                                    {
                                        case "TF_LEDGER":
                                            decimal XXtf;
                                            decimal XTF = 0;
                                            decimal ACT_DUE = 0;

                                            sql = "SELECT * FROM TF_RATE ORDER BY EFF_DATE";
                                            config.singleResult(sql);
                                            if (config.dt.Rows.Count > 0)
                                            {
                                                DataRow drtr = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                                                if (Convert.ToDateTime(drtr["Eff_date"]) <= Convert.ToDateTime(sch_date))
                                                {
                                                    tf_eff_date = Convert.ToDateTime(drtr["Eff_date"]);
                                                    xirate = Convert.ToDecimal(drtr["TF_RATE"]);
                                                }
                                            }
                                            sql = "select * from member_mast where member_id='" + MEMBER + "'order by member_id";
                                            config.singleResult(sql);
                                            if (config.dt.Rows.Count > 0)
                                            {
                                                DataRow drmm = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                                                XXtf = !Convert.IsDBNull(dr["BLOOD_GROUP"]) ? Convert.ToDecimal(dr["BLOOD_GROUP"]) : Convert.ToDecimal("0");
                                                if (XXtf > 0)
                                                {
                                                    XTF = XXtf;
                                                }
                                                else
                                                {
                                                    XTF = 100;
                                                }
                                            }
                                            ACT_DUE = 0;
                                            prin_bal = 0;
                                            sql = "SELECT* FROM TF_LEDGER WHERE BRANCH_ID = '" + branch + "' AND MEMBER_ID='" + MEMBER + "' AND convert(datetime, VCH_DATE, 103) <= convert(datetime, '" + sch_date + "', 103) ORDER BY BRANCH_ID,MEMBER_ID,VCH_DATE,VCH_NO,VCH_SRL";
                                            config.singleResult(sql);
                                            if (config.dt.Rows.Count > 0)
                                            {
                                                DataRow drtl = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                                                sql = "SELECT * FROM TF_RATE ORDER BY EFF_DATE";
                                                config.singleResult(sql);
                                                if (config.dt.Rows.Count > 0)
                                                {
                                                    DataRow drtr = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                                                    xirate = xirate = Convert.ToDecimal(drtr["TF_RATE"]);
                                                }
                                                prin_bal = !Convert.IsDBNull(drtl["prin_bal"]) ? Convert.ToDecimal(drtl["prin_bal"]) : Convert.ToDecimal("0");
                                                ACT_DUE = xirate;
                                            }
                                            else
                                            {
                                                ACT_DUE = xirate;
                                            }
                                            rs.r1 = XBOOK_NO;
                                            rs.r2 = xemployee_ID;
                                            rs.r3 = XMEMBER_NM;
                                            rs.r4 = Acc_Head;
                                            rs.r5 = MEMBER;
                                            rs.r6 = prin_bal.ToString("0.00");
                                            rs.r8 = XTF.ToString("0.00");
                                            //rs.SaveDataInRecoverySchedule(rs, emp_name, unit, mem_type, mem_cat, book_no, sch_date, branch, xemployee_ID);
                                            rslst.Add(rs);
                                            CAL_TF_DUE = true;
                                            if (CAL_TF_DUE == true)
                                            {
                                                Tot_Due = Tot_Due + XTF;
                                                totalR7 = totalR7 + XTF;
                                            }
                                            break;
                                        case "LICP_LEDGER":
                                            string xmem = "";
                                            string XBK = "";
                                            string xnm = "";
                                            sql = "SELECT * FROM LOAN_MASTER WHERE BRANCH_ID='MN' AND EMPLOYEE_ID='" + xemployee_ID + "' and LIC_PREMIUM IS NOT NULL order by ac_hd,employee_id";
                                            config.singleResult(sql);
                                            if (config.dt.Rows.Count > 0)
                                            {
                                                foreach (DataRow dr2 in config.dt.Rows)
                                                {
                                                    sql = "select * from member_mast where branch_id='MN' AND EMPLOYEE_ID='" + xemployee_ID + "' and member_closed is null ";
                                                    config.singleResult(sql);
                                                    if (config.dt.Rows.Count > 0)
                                                    {
                                                        DataRow drmm = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                                                        xmem = Convert.ToString(drmm["member_id"]);
                                                        XBK = Convert.ToString(drmm["book_no"]);
                                                        xnm = Convert.ToString(drmm["member_name"]);
                                                    }
                                                    rs.r1 = XBOOK_NO;
                                                    rs.r2 = xemployee_ID;
                                                    rs.r3 = XMEMBER_NM;
                                                    rs.r4 = "LICP";
                                                    rs.r5 = xmem;
                                                   // rs.SaveDataInRecoverySchedule(rs, emp_name, unit, mem_type, mem_cat, book_no, sch_date, branch, xemployee_ID);
                                                    rslst.Add(rs);
                                                    break;
                                                }
                                            }
                                            break;
                                        //case "SHARE_LEDGER":
                                        //    decimal XXsh = 0;
                                        //    sql = "select * from member_mast where member_id='" + MEMBER + "'order by member_id";
                                        //    config.singleResult(sql);
                                        //    if (config.dt.Rows.Count > 0)
                                        //    {
                                        //        DataRow drmm = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                                        //        XXsh = !Convert.IsDBNull(drmm["SHARE"]) ? Convert.ToDecimal(drmm["SHARE"]) : Convert.ToDecimal("00");
                                        //        if (XXsh > 0)
                                        //        {
                                        //            XTF = XXsh;
                                        //        }
                                        //        else
                                        //        {
                                        //            XTF = 0;
                                        //        }
                                        //    }
                                        //    DUE_MN = 0;
                                        //    DUE_AMT = 0;
                                        //    ACT_DUE = 0;
                                        //    prin_bal = 0;
                                        //    sql = "SELECT * FROM SHARE_LEDGER WHERE BRANCH_ID='" + branch + "' AND MEMBER_ID='" + MEMBER + "' AND convert(datetime, VCH_DATE, 103) <= convert(datetime, '" + sch_date + "', 103) ORDER BY BRANCH_ID,MEMBER_ID,VCH_DATE,VCH_NO,VCH_SRL";
                                        //    config.singleResult(sql);
                                        //    if (config.dt.Rows.Count > 0)
                                        //    {
                                        //        DataRow drsl = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                                        //        prin_bal = !Convert.IsDBNull(drsl["BAL_AMOUNT"]) ? Convert.ToDecimal(drsl["BAL_AMOUNT"]) : Convert.ToDecimal("00");
                                        //    }
                                        //    else
                                        //    {
                                        //        prin_bal = 0;
                                        //    }
                                        //   
                                        //    CAL_SHARE_DUE = true;
                                        //    if (CAL_SHARE_DUE == true)
                                        //    {
                                        //        Tot_Due = Tot_Due + XXsh;
                                        //        totalR7 = totalR7 + XXsh;

                                        //    }
                                        //    break;
                                        case "RTB_LEDGER":
                                            sql = "SELECT * FROM TF_RATE ORDER BY EFF_DATE";
                                            config.singleResult(sql);
                                            if (config.dt.Rows.Count > 0)
                                            {
                                                DataRow drtr = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                                                if (Convert.ToDateTime(drtr["Eff_date"]) <= Convert.ToDateTime(sch_date))
                                                {
                                                    tf_eff_date = Convert.ToDateTime(drtr["Eff_date"]);
                                                    xirate = Convert.ToDecimal(drtr["TF_RATE"]);
                                                }
                                            }
                                            ACT_DUE = 0;
                                            prin_bal = 0;
                                            sql = "SELECT * FROM RTB_LEDGER WHERE BRANCH_ID='" + branch + "' AND MEMBER_ID='" + MEMBER + "' AND convert(datetime, VCH_DATE, 103) <= convert(datetime, '" + sch_date + "', 103) ORDER BY BRANCH_ID,MEMBER_ID,VCH_DATE,VCH_NO,VCH_SRL";
                                            config.singleResult(sql);
                                            if (config.dt.Rows.Count > 0)
                                            {
                                                DataRow drrl = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];

                                                sql = "SELECT * FROM RTB_RATE ORDER BY EFF_DATE";
                                                config.singleResult(sql);
                                                if (config.dt.Rows.Count > 0)
                                                {
                                                    DataRow drrr = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                                                    xirate = Convert.ToDecimal(drrr["RTB_RATE"]);
                                                }

                                                prin_bal = !Convert.IsDBNull(drrl["prin_bal"]) ? Convert.ToDecimal(drrl["prin_bal"]) : Convert.ToDecimal("0");
                                                ACT_DUE = xirate;
                                            }
                                            else
                                            {
                                                ACT_DUE = xirate;
                                            }
                                            rs.r1 = XBOOK_NO;
                                            rs.r2 = xemployee_ID;
                                            rs.r3 = XMEMBER_NM;
                                            rs.r4 = Acc_Head;
                                            rs.r5 = MEMBER;
                                            rs.r6 = prin_bal.ToString("0.00");
                                            rs.r8 = xirate.ToString("0.00");
                                            //rs.SaveDataInRecoverySchedule(rs, emp_name, unit, mem_type, mem_cat, book_no, sch_date, branch, xemployee_ID);
                                            rslst.Add(rs);
                                            CAL_RTB_DUE = true;
                                            if (CAL_RTB_DUE == true)
                                            {
                                                Tot_Due = Tot_Due + xirate;
                                                totalR7 = totalR7 + xirate;
                                            }
                                            break;
                                    }
                                }
                            }
                        }
                    }
                    //rs1.r9 = "TOTAL";
                    //rs1.r10 = Tot_Due.ToString("0.00");
                    //rslst.Add(rs1);
                    Tot_Due = 0;
                }
            }
            return rslst;
        }
        public Recovery_Schedule getPrinBalIntBalFromRecovery(string empCd, string unit,string mem_type,string mem_cat,string book_no,string sch_dt,string branch,string Empid,string achd)
        {
           string sql = "SELECT * FROM recovery_schedule WHERE  EMPLOYER_CD='" + empCd + "'";
            sql = sql + "  AND EMPLOYER_BRANCH='" + unit + "'  AND Branch_id='" + branch + "'";
            sql = sql + " AND  MEMBER_TYPE='" + mem_type + "' ";
            sql = sql + " AND MEM_CATEGORY='" + mem_cat + "' and book_no='" + book_no + "'and EMPLOYEE_ID='" + Empid + "'and ac_hd='" + achd + "' and convert(datetime, SCH_DATE, 103) = convert(datetime, '" + sch_dt + "', 103) ";          
            config.singleResult(sql);
            Recovery_Schedule rs = new Recovery_Schedule();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    rs.prin_bal = Convert.ToDecimal(dr["prin_bal"]);
                    rs.prin_amt = Convert.ToDecimal(dr["prin_amt"]);
                    rs.int_amt = Convert.ToDecimal(dr["int_amt"]);                   
                }
            }
            return rs;
        }
        public List<Recovery_Schedule> getdecachd(string emp_name, string unit)
        {
            string ACHD_QRY = "SELECT * FROM DEDUCT_ACHD_MAST WHERE EMPLOYER_CD='" + emp_name + "' ";
            ACHD_QRY = ACHD_QRY + "AND EMPLOYER_BRANCH='" + unit + "' ORDER BY AC_HD ";
            config.singleResult(ACHD_QRY);
            List<Recovery_Schedule> rslst = new List<Recovery_Schedule>();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    Recovery_Schedule rs = new Recovery_Schedule();
                    rs.ac_hd = Convert.ToString(dr["ac_hd"]);
                    rs.ac_desc = Convert.ToString(dr["AC_DESC"]);
                    rslst.Add(rs);
                }
            }
            return rslst;
        }
        public string updatloanblances(PrepOfDeductionScheduleViewModel model)
        {
            string sql = "";
            sql = "select * from recovery_schedule where branch_id='" + model.branch + "'";
            sql = sql + " and employer_cd='" + model.emp_name + "' and EMPLOYEE_ID ='" + model.employee_id + "' and ";
            sql = sql + " ac_hd='" + model.ac_hd + "' and ";
            sql = sql + "convert(datetime, SCH_DATE, 103) = convert(datetime, '" + model.sch_dt + "', 103)";
            sql = sql + " AND MEMBER_TYPE='" + model.mem_type + "' ";
            sql = sql + " AND MEM_CATEGORY='" + model.mem_cat + "'and BOOK_NO='" + model.book_no + "'"; 
            config.singleResult(sql);
            List<Recovery_Schedule> rslst = new List<Recovery_Schedule>();
            if (config.dt.Rows.Count > 0)
            {
                string qry = "Update recovery_schedule set prin_bal=" + Convert.ToDecimal(model.prin_bal) + ",OVER_DUE=" + Convert.ToDecimal(model.over_due) + ",PRIN_AMT=" + Convert.ToDecimal(model.prin_amt) + ",INT_AMT=" + Convert.ToDecimal(model.int_amt) + " where convert(varchar, SCH_DATE, 103) = convert(varchar, '" + model.sch_dt + "', 103) AND EMPLOYEE_ID='" + model.employee_id + "' and branch_id='" + model.branch + "' and employer_branch=" + model.unit + " and employer_cd='" + model.emp_name + "' and ac_hd='" + model.ac_hd + "' AND MEM_CATEGORY='" + model.mem_cat + "'and BOOK_NO='" + model.book_no + "'";
                config.Execute_Query(qry);
            }
            return "Updated";
        }
        public List<Recovery_Schedule> getdecschlist(string branch, string mem_cat, string sending_dt, string unit)
        {
            string XAC_NO = "";
            string YAC_NO = "";
            string xac_hd = "";
            string YAC_HD = "";
            string RO_COUNT = "";
            decimal xprin = 0;
            decimal xint = 0;
            decimal XTF = 0;
            List<Recovery_Schedule> rslst = new List<Recovery_Schedule>();
            string qryMEM = string.Empty;            
            qryMEM = "SELECT * FROM RECOVERY_SCHEDULE WHERE BRANCH_ID='" + branch + "' AND ";
            qryMEM = qryMEM + "convert(datetime, SCH_DATE, 103) = convert(datetime, '" + sending_dt + "', 103) AND ";
            qryMEM = qryMEM + "MEM_CATEGORY='" + mem_cat + "'";
            qryMEM = qryMEM + "ORDER BY EMPLOYER_BRANCH,BOOK_NO,EMPLOYEE_ID";                        
            config.singleResult(qryMEM);          
            if (config.dt.Rows.Count > 0)
            {
                //decimal NHD = 0;               
                foreach (DataRow dr in config.dt.Rows)
                {
                    Recovery_Schedule rs = new Recovery_Schedule();
                    rs.emp_id = Convert.ToString(dr["EMPLOYEE_ID"]);
                    rs.ac_hd = Convert.ToString(dr["ac_hd"]);
                    rs.mem_name = Convert.ToString(dr["member_name"]);
                    rs.unit = Convert.ToString(dr["EMPLOYER_BRANCH"]);
                    rs.prin_amt = Convert.ToDecimal(dr["PRIN_AMT"]);
                    rs.int_amt = Convert.ToDecimal(dr["INT_AMT"]);
                    rs.book_no = Convert.ToString(dr["book_no"]);
                    rslst.Add(rs);
                }              
            }                                 
            return rslst;
        }
    }
}