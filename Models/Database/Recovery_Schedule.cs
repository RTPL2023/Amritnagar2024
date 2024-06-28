using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Amritnagar.Includes;

namespace Amritnagar.Models.Database
{
    public class Recovery_Schedule
    {
        SQLConfig config = new SQLConfig();
        public string branch_id { get; set; }
        public string employer_cd { get; set; }
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
    }
}