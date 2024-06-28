using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Amritnagar.Includes;
using Amritnagar.Models.ViewModel;

namespace Amritnagar.Models.Database
{
    public class Loan_Master
    {
        SQLConfig config = new SQLConfig();
        public string branch_id { get; set; }
        public string ac_hd { get; set; }
        public string emp_id { get; set; }
        public DateTime loan_dt { get; set; }        
        public string mem_id { get; set; }
        public string ln_spcl { get; set; }
        public string loanee_name { get; set; }
        public string gurdian_name { get; set; }
        public decimal loan_amt { get; set; }
        public decimal prin_amt { get; set; }
        public decimal inst_amt { get; set; }
        public int inst_no { get; set; }
        public decimal inst_rate { get; set; }
        public string inst_rate_cd { get; set; }
        public string ln_purpose { get; set; }
        public string lic_premium { get; set; }
        public string mem_type { get; set; }
        public string mem_cat { get; set; }
        public string book_no { get; set; }
        public DateTime sanction_dt { get; set; }
        public string sanction_ref { get; set; }
        public string cheque_no { get; set; }
        public DateTime cheque_dt { get; set; }
        public string bank_ref { get; set; }
        public string clos_flag { get; set; }
        public DateTime clos_dt { get; set; }
        public string clos_type { get; set; }
        public string clos_adjusted { get; set; }
        public string clos_adj_nloan_id { get; set; }
        public string flag { get; set; }
        public string msg { get; set; }
        public DateTime vch_dt { get; set; }
        public DateTime birth_date { get; set; }
        public DateTime joining_dt { get; set; }
        public string vch_no { get; set; }
        public string employer_branch { get; set; }
        public string ledger_tab { get; set; }
        public string ac_desc { get; set; }

        public decimal intdbt { get; set; }
        public decimal intdue { get; set; }

        public string Save(Loan_Master lm)
        {
            string sql = string.Empty;
            sql = "SELECT * FROM LOAN_MASTER WHERE BRANCH_ID='" + lm.branch_id + "' AND AC_HD='" + lm.ac_hd + "' AND EMPLOYEE_ID='" + lm.emp_id + "' AND convert(datetime, LOAN_DATE, 103) = convert(datetime, '" + lm.loan_dt + "', 103)";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                try
                {
                    config.Update("LOAN_MASTER", new Dictionary<String, object>()
                    {                       
                        { "MEMBER_ID",      lm.mem_id },
                        { "LN_SPEACIAL",    lm.ln_spcl },
                        { "BOOK_NO",        lm.book_no},
                        { "LN_PURPOSE",    lm.ln_purpose },
                        { "LOANEE_NAME",   lm.loanee_name },
                        { "LOAN_AMT",       lm.loan_amt },
                        { "NO_INSTL",   lm.inst_no},
                        { "INT_RATE",   lm.inst_rate},
                        { "INSTL_AMOUNT",   lm.inst_amt},                       
                    }, new Dictionary<string, object>()
                    {
                        { "BRANCH_ID",  lm.branch_id },
                        { "LOAN_DATE",  lm.loan_dt },
                        { "EMPLOYEE_ID",lm.emp_id },
                        { "AC_HD",lm.ac_hd },
                    });
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                try
                {
                    config.Insert("LOAN_MASTER", new Dictionary<String, object>()
                    {
                        { "MEMBER_ID",      lm.mem_id },
                        { "LN_SPEACIAL",    lm.ln_spcl },
                        { "BOOK_NO",        lm.book_no},
                        { "LN_PURPOSE",    lm.ln_purpose },
                        { "LOANEE_NAME",   lm.loanee_name },
                        { "LOAN_AMT",       lm.loan_amt },
                        { "NO_INSTL",       lm.inst_no},
                        { "INT_RATE",       lm.inst_rate},
                        { "INSTL_AMOUNT",   lm.inst_amt},
                        { "BRANCH_ID",      lm.branch_id },
                        { "LOAN_DATE",      lm.loan_dt },
                        { "EMPLOYEE_ID",    lm.emp_id },
                        { "AC_HD",  lm.ac_hd },
                    });
                }
                catch (Exception x)
                {

                }
            }           
            string msg = "Saved Successfully";
            return (msg);
        }
        public List<Loan_Master> getmemdetails(string branch_id, string ac_hd, string emp_id)
        {
            string sql = "select * from loan_master where branch_id='" + branch_id + "' and ac_hd='" + ac_hd + "' and employee_id='" + emp_id + "' order by branch_id,ac_hd,employee_id";
            config.singleResult(sql);
            List<Loan_Master> lml = new List<Loan_Master>();           
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    Loan_Master lm = new Loan_Master();
                    lm.ac_hd = Convert.ToString(dr["AC_HD"]);
                    lm.loan_amt = Convert.ToDecimal(dr["LOAN_AMT"]);
                    lm.loan_dt = Convert.ToDateTime(dr["LOAN_DATE"]);
                    lm.clos_flag = Convert.ToString(dr["CLOS_FLAG"]);
                    lml.Add(lm);
                }
            }            
            return lml;
        }
        public string updateprimium(Loan_Master lm)
        {
            string sql = string.Empty;
            sql = "SELECT * FROM LOAN_MASTER WHERE BRANCH_ID='"+ lm.branch_id +"' AND AC_HD='" + lm.ac_hd + "' AND EMPLOYEE_ID='" + lm.emp_id + "'AND convert(datetime, LOAN_DATE, 103) = convert(datetime, '" + lm.loan_dt + "', 103)";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                try
                {
                    config.Update("LOAN_MASTER", new Dictionary<String, object>()
                    {
                        { "LIC_PREMIUM",  lm.lic_premium },                       
                    }, new Dictionary<string, object>()
                    {
                        { "BRANCH_ID",  lm.branch_id },
                        { "LOAN_DATE",  lm.loan_dt },
                        { "EMPLOYEE_ID",lm.emp_id },
                        { "AC_HD",lm.ac_hd },
                    });
                }
                catch (Exception ex)
                {

                }
            }         
            string msg = "Over";
            return (msg);
        }
        public string CloseFlag(Loan_Master lm)
        {
            string sql = string.Empty;
            sql = "SELECT * FROM LOAN_MASTER WHERE BRANCH_ID='" + lm.branch_id + "' AND AC_HD='" + lm.ac_hd + "' AND EMPLOYEE_ID='" + lm.emp_id + "'AND convert(datetime, LOAN_DATE, 103) = convert(datetime, '" + lm.loan_dt + "', 103)";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                try
                {
                    config.Update("LOAN_MASTER", new Dictionary<String, object>()
                    {
                        { "CLOS_FLAG",  lm.clos_flag },
                        { "CLOS_DATE",  DateTime.Now.ToString("dd-MM-yyyy").Replace("-", "/") },
                    }, new Dictionary<string, object>()
                    {
                        { "BRANCH_ID",  lm.branch_id },
                        { "LOAN_DATE",  lm.loan_dt },
                        { "EMPLOYEE_ID",lm.emp_id },
                        { "AC_HD",lm.ac_hd },
                    });
                }
                catch (Exception ex)
                {

                }
            }
            string msg = "Closed";
            return (msg);
        }
        public Loan_Master getdetailsbyloandate(string branch_id, string ac_hd, string emp_id, string loan_dt)
        {
            Loan_Master lm = new Loan_Master();
            string sql = string.Empty;
            sql = "select* from loan_master where branch_id = '" + branch_id + "' and ac_hd = '" + ac_hd + "' and employee_id = '" + emp_id + "' and convert(datetime, LOAN_DATE, 103) = convert(datetime, '" + loan_dt + "', 103) order by branch_id,ac_hd,employee_id";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    lm.ln_spcl = Convert.ToString(dr["LN_SPEACIAL"]);
                    lm.loan_amt = Convert.ToDecimal(dr["LOAN_AMT"]);
                    lm.inst_amt = Convert.ToDecimal(dr["INSTL_AMOUNT"]);
                    lm.inst_no = Convert.ToInt32(dr["NO_INSTL"]);
                    lm.inst_rate = Convert.ToDecimal(dr["INT_RATE"]);
                    lm.ln_purpose = Convert.ToString(dr["LN_PURPOSE"]);
                    lm.lic_premium = !Convert.IsDBNull(dr["LIC_PREMIUM"]) ? Convert.ToString(dr["LIC_PREMIUM"]) : Convert.ToString("");
                    lm.msg = "Details Found";
                }
            //    sql = "SELECT * FROM LOAN_LEDGER WHERE BRANCH_ID='" + branch_id + "' AND AC_HD='" + ac_hd + "' AND EMPLOYEE_ID='" + emp_id + "' ORDER BY VCH_DATE,VCH_NO,VCH_SRL,EMPLOYEE_ID";
            //    config.singleResult(sql);
            //    if (config.dt.Rows.Count > 0)
            //    {
            //        DataRow dr1 = (DataRow)config.dt.Rows[0];
            //        lm.vch_dt = Convert.ToDateTime(dr1["VCH_DATE"]);
            //        lm.vch_no = Convert.ToString(dr1["VCH_NO"]);
            //    }
            }
            else
            {
                sql = "select * from lntype_mast where ac_hd='" + ac_hd + "' order by ac_hd";
                config.singleResult(sql);
                if (config.dt.Rows.Count > 0)
                {                   
                    foreach (DataRow dr1 in config.dt.Rows)
                    {
                        lm.inst_rate_cd = !Convert.IsDBNull(dr1["INT_RATE_CD"]) ? Convert.ToString(dr1["INT_RATE_CD"]) : Convert.ToString("");
                        sql = "select * from LNINTRATE_DTL where INT_RATE_CD='" + lm.inst_rate_cd + "'";
                        config.singleResult(sql);
                        if (config.dt.Rows.Count > 0)
                        {
                            DataRow dr2 = (DataRow)config.dt.Rows[0];
                            lm.inst_rate = !Convert.IsDBNull(dr2["INT_RATE"]) ? Convert.ToDecimal(dr2["INT_RATE"]) : Convert.ToDecimal("0.00");
                        }
                    }
                }              
            }
            return lm;
        }
        public Loan_Master getmemidbyempid(string emp_id, string branch_id, string loan_type)
        {
            Loan_Master lm = new Loan_Master();
            string sql = "SELECT * FROM LOAN_MASTER WHERE BRANCH_ID='" + branch_id + "' AND AC_HD='" + loan_type + "' AND EMPLOYEE_ID='" + emp_id + "' ORDER BY LOAN_DATE";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    lm.mem_id = dr["MEMBER_ID"].ToString();                    
                }
            }
            else
            {
                lm.msg = "No Record Found";
            }
            return lm;
        }
        public Loan_Master getdetailsbybranchac_hdandempid(string emp_id, string branch_id, string loan_type)
        {
            Loan_Master lm = new Loan_Master();
            string sql = "SELECT * FROM LOAN_MASTER WHERE BRANCH_ID='" + branch_id + "' AND AC_HD='" + loan_type + "' AND EMPLOYEE_ID='" + emp_id + "' ORDER BY LOAN_DATE";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    lm.cheque_no = Convert.ToString(dr["CHEQUE_NO"]);
                    lm.cheque_dt = !Convert.IsDBNull(dr["CHEQUE_DATE"]) ? Convert.ToDateTime(dr["CHEQUE_DATE"]) : Convert.ToDateTime(null);                    
                    lm.bank_ref = Convert.ToString(dr["BANK_REF"]);
                    lm.loan_dt = Convert.ToDateTime(dr["LOAN_DATE"]);
                    lm.loan_amt = Convert.ToDecimal(dr["LOAN_AMT"]);
                    lm.inst_no = Convert.ToInt32(dr["NO_INSTL"]);
                    lm.inst_rate = Convert.ToDecimal(dr["INT_RATE"]);
                    lm.inst_amt = Convert.ToDecimal(dr["INSTL_AMOUNT"]);
                    lm.ln_purpose = Convert.ToString(dr["LN_PURPOSE"]);
                }
            }            
            return lm;
        }
        public List<Loan_Master> getdetails(string searchtype, string branch_id, string ac_hd, string fr_dt, string to_dt)
        {
            List<Loan_Master> lml = new List<Loan_Master>();
            string sql = string.Empty;
            if (searchtype == "New Loan")
            {
                sql = "select * FROM loan_master WHERE BRANCH_ID='" + branch_id + "' AND AC_HD = '"+ ac_hd + "' and convert(datetime, loan_date, 103) >= convert(datetime, '" + fr_dt + "', 103) and convert(datetime, loan_date, 103) <= convert(datetime, '" + to_dt + "', 103)" +
                    "ORDER BY ac_hd,EMPLOYEE_id,loan_date";
                config.singleResult(sql);
                if (config.dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in config.dt.Rows)
                    {
                        Loan_Master lm = new Loan_Master();
                        lm.emp_id = dr["EMPLOYEE_ID"].ToString();                                              
                        lm.loan_dt = !Convert.IsDBNull(dr["LOAN_DATE"]) ? Convert.ToDateTime(dr["LOAN_DATE"]) : Convert.ToDateTime(null);
                        lm.loan_amt = !Convert.IsDBNull(dr["LOAN_AMT"]) ? Convert.ToDecimal(dr["LOAN_AMT"]) : Convert.ToDecimal("0.00");                      
                        lm.loanee_name = dr["LOANEE_NAME"].ToString();
                        lm.inst_no = Convert.ToInt32(dr["NO_INSTL"]);
                        lm.inst_amt = (lm.loan_amt / lm.inst_no);                       
                        lml.Add(lm);
                    }
                }
            }
            else
            {
                sql = "select * FROM loan_master WHERE BRANCH_ID='" + branch_id + "' AND AC_HD = '" + ac_hd + "' and convert(datetime, loan_date, 103) >= convert(datetime, '" + fr_dt + "', 103) and convert(datetime, loan_date, 103) <= convert(datetime, '" + to_dt + "', 103) AND clos_flag = 'C'" +
                    "ORDER BY ac_hd,EMPLOYEE_id,clos_date";
                config.singleResult(sql);
                if (config.dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in config.dt.Rows)
                    {
                        Loan_Master lm = new Loan_Master();
                        lm.emp_id = dr["EMPLOYEE_ID"].ToString();
                        lm.loan_dt = !Convert.IsDBNull(dr["LOAN_DATE"]) ? Convert.ToDateTime(dr["LOAN_DATE"]) : Convert.ToDateTime(null);
                        lm.clos_dt = !Convert.IsDBNull(dr["CLOS_DATE"]) ? Convert.ToDateTime(dr["CLOS_DATE"]) : Convert.ToDateTime("31/12/2099");
                        lm.loan_amt = !Convert.IsDBNull(dr["LOAN_AMT"]) ? Convert.ToDecimal(dr["LOAN_AMT"]) : Convert.ToDecimal("0.00");
                        lm.loanee_name = dr["LOANEE_NAME"].ToString();
                        lm.inst_no = Convert.ToInt32(dr["NO_INSTL"]);
                        lm.inst_amt = (lm.loan_amt / lm.inst_no);
                        sql = "select * from loan_ledger where BRANCH_ID='" + branch_id + "' AND ac_hd='" + ac_hd + "' and EMPLOYEE_id='" + lm.emp_id + "' and convert(datetime, vch_date, 103) = convert(datetime, '" + lm.clos_dt + "', 103) AND DR_CR='C' order by vch_date,vch_srl";
                        config.singleResult(sql);
                        if(config.dt.Rows.Count > 0)
                        {
                            DataRow dr1 = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                            lm.prin_amt = !Convert.IsDBNull(dr["PRIN_AMOUNT"]) ? Convert.ToDecimal(dr["PRIN_AMOUNT"]) : Convert.ToDecimal("0.00");
                        }
                        lml.Add(lm);
                    }
                }
            }           
            return lml;
        }
        public List<Loan_Master> getdetailsbydaterange(string fr_dt, string to_dt)
        {
            string sql = string.Empty;
            sql = "select * from loan_master where branch_id='MN' AND convert(datetime, loan_date, 103) >= convert(datetime, '" + fr_dt + "', 103) and convert(datetime, loan_date, 103) <= convert(datetime, '" + to_dt + "', 103) order by loan_date ";
            config.singleResult(sql);
            List<Loan_Master> lml = new List<Loan_Master>();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    Loan_Master lm = new Loan_Master();                    
                    lm.mem_id = Convert.ToString(dr["MEMBER_ID"]);
                    lm.emp_id = Convert.ToString(dr["EMPLOYEE_ID"]);
                    lm.loanee_name = !Convert.IsDBNull(dr["LOANEE_NAME"]) ? Convert.ToString(dr["LOANEE_NAME"]) : Convert.ToString("");
                    lm.loan_amt = !Convert.IsDBNull(dr["LOAN_AMT"]) ? Convert.ToDecimal(dr["LOAN_AMT"]) : Convert.ToDecimal(00);
                    lm.loan_dt = !Convert.IsDBNull(dr["LOAN_DATE"]) ? Convert.ToDateTime(dr["LOAN_DATE"]) : Convert.ToDateTime(null);
                    sql = "SELECT * FROM MEMBER_MAST WHERE BRANCH_ID='MN' AND member_id='" + lm.mem_id + "'";
                    config.singleResult(sql);
                    if (config.dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr1 in config.dt.Rows)
                        {
                            lm.birth_date = !Convert.IsDBNull(dr1["BIRTH_DATE"]) ? Convert.ToDateTime(dr1["BIRTH_DATE"]) : Convert.ToDateTime(null);
                            lm.joining_dt = !Convert.IsDBNull(dr1["DATE_OF_JOINING"]) ? Convert.ToDateTime(dr1["DATE_OF_JOINING"]) : Convert.ToDateTime(null);
                        }
                    }
                    lml.Add(lm);
                }
            }
            return lml;
        }
        public List<Loan_Master> getloansuritydetails(string branch_id, string member_no)
        {
            List<Loan_Master> lml = new List<Loan_Master>();
            string sql = string.Empty;
            sql = "select a.ac_hd,a.loanee_name,a.employee_id,a.loan_date,a.loan_amt from loan_master a,loan_surity b" +
                " where a.branch_id = b.branch_id and a.employee_id=b.employee_id and a.ac_hd=b.ac_hd and a.clos_flag is null" +
                " and a.branch_id='" + branch_id + "' and b.smember_id='" + member_no + "' order by a.loan_date,a.employee_id";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    Loan_Master lm = new Loan_Master();
                    lm.ac_hd = Convert.ToString(dr["AC_HD"]);
                    lm.emp_id = Convert.ToString(dr["EMPLOYEE_ID"]);
                    lm.loanee_name = !Convert.IsDBNull(dr["LOANEE_NAME"]) ? Convert.ToString(dr["LOANEE_NAME"]) : Convert.ToString("");
                    lm.loan_dt = !Convert.IsDBNull(dr["LOAN_DATE"]) ? Convert.ToDateTime(dr["LOAN_DATE"]) : Convert.ToDateTime(null);
                    lm.loan_amt = !Convert.IsDBNull(dr["LOAN_AMT"]) ? Convert.ToDecimal(dr["LOAN_AMT"]) : Convert.ToDecimal(00);
                    lml.Add(lm);
                }
            }
            return lml;
        }
        public List<Loan_Master> getmonthlyIntrestList(MonthlyInterestScheduleForLoanViewModel model)
        {
            decimal xr = 0;
            decimal tot_prin = 0;
            decimal tot_int = 0;
            decimal tot_aint = 0;
            decimal XINT_RATE = 0;
            decimal xirate = 0;
            string xacno = "";
            string FLAG = "";
            decimal LAST_BAL = 0;
            decimal PRIN_DUE = 0;
            decimal INT_DUE = 0;
            decimal LBAL_PRIN = 0;
            decimal LBAL_INT = 0;
            decimal XPRIN_BAL = 0;
            decimal XPRIN_AMT = 0;
            decimal XPRIN_AMOUNT = 0;
            decimal XINT_CAL = 0;
            decimal YINT_CAL = 0;
            decimal YPRIN_BAL = 0;
            int cal_date = 0;
            List<Loan_Master> lml = new List<Loan_Master>();
            string sql = string.Empty;
            sql = "select * from loan_master where branch_id='" + model.branch_id + "' and EMPLOYER_BRANCH='" + model.colliery_code + "' and ";
            sql = sql + "ac_hd='" + model.ln_achd + "' and BOOK_NO='" + model.book_no + "' and convert(datetime, loan_date, 103) <= convert(datetime, '" + model.sch_date + "', 103) and ";
            sql = sql + "clos_flag is null and clos_date is null AND FLAG IS NULL order by employee_id";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    Loan_Master lm = new Loan_Master();

                    xacno = Convert.ToString(dr["EMPLOYEE_ID"]);
                    sql = "";
                    sql = "select * from loan_ledger where branch_id='" + model.branch_id + "' and ";
                    sql = sql + "ac_hd='" + model.ln_achd + "' and employee_id='" + xacno + "' and convert(datetime, vch_date, 103) <= convert(datetime, '" + model.sch_date + "', 103)  order by vch_date,vch_no,vch_srl";
                    config.singleResult(sql);
                    if (config.dt.Rows.Count > 0)
                    {
                        lm.emp_id = Convert.ToString(dr["EMPLOYEE_ID"]);
                        lm.loanee_name = Convert.ToString(dr["loanee_name"]);
                        lm.loan_dt = Convert.ToDateTime(dr["loan_date"]);
                        lm.loan_amt = Convert.ToDecimal(dr["loan_amt"]);
                        XINT_RATE = Convert.ToDecimal(dr["INT_RATE"]);
                        sql = "";
                        sql = "select * from LNTYPE_MAST where ac_hd='" + model.ln_achd + "'";
                        config.singleResult(sql);
                        if (config.dt.Rows.Count > 0)
                        {
                            DataRow dr1 = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                            sql = "select * from lnscheme_int where int_scheme_cd='" + Convert.ToString(dr1["INT_SCHEME_CD"]) + "'";
                            config.singleResult(sql);
                            if (config.dt.Rows.Count > 0)
                            {
                                DataRow dr2 = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                                if (Convert.ToString(dr2["int_category"]) == "F")
                                {
                                    xirate = XINT_RATE;
                                }
                                else
                                {
                                    sql = "SELECT * FROM LNINTRATE_DTL WHERE INT_RATE_CD='" + Convert.ToString(dr1["INT_RATE_CD"]) + "' and convert(datetime, weff_date, 103) <= convert(datetime, '" + model.fr_dt + "', 103)  ORDER BY WEFF_DATE,UPPER_LIMIT";
                                    config.singleResult(sql);
                                    if (config.dt.Rows.Count > 0)
                                    {
                                        DataRow dr3 = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                                        xirate = Convert.ToDecimal(dr3["INT_RATE"]);
                                    }
                                }
                            }

                        }
                        lm.inst_rate = xirate;
                        sql = "";
                        sql = "SELECT * FROM LOAN_LEDGER WHERE BRANCH_ID='" + model.branch_id + "' AND ";
                        sql = sql + "AC_HD='" + model.ln_achd + "' and  employee_ID='" + lm.emp_id + "' ";
                        sql = sql + "and convert(datetime, VCH_DATE, 103) <= convert(datetime, '" + model.sch_date + "', 103) ORDER BY BRANCH_ID,AC_HD,employee_ID,VCH_DATE,VCH_NO,VCH_SRL";
                        config.singleResult(sql);
                        if (config.dt.Rows.Count > 0)
                        {
                            LAST_BAL = 0;
                            PRIN_DUE = 0;
                            INT_DUE = 0;
                            DataRow dr3 = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                            LBAL_PRIN = Convert.ToDecimal(dr3["prin_bal"]);
                            LBAL_INT = Convert.ToDecimal(dr3["INT_DUE"]);
                            if (LBAL_PRIN > 0)
                            {
                                FLAG = Convert.ToString(dr["FLAG"]);
                                if (lm.loan_dt.Month == Convert.ToDateTime(model.fr_dt).Month && lm.loan_dt.Year == Convert.ToDateTime(model.fr_dt).Year)
                                {
                                    cal_date = Convert.ToDateTime(lm.loan_dt - Convert.ToDateTime(model.fr_dt).AddDays(+1)).Day;
                                    XPRIN_BAL = Convert.ToDecimal(dr3["prin_bal"]);
                                    DataRow dr4 = (DataRow)config.dt.Rows[0];
                                    if (Convert.ToDateTime(dr4["VCH_DATE"]) == lm.loan_dt)
                                    {
                                        XPRIN_AMT = Convert.ToDecimal(dr3["PRIN_AMOUNT"]);
                                    }
                                    XPRIN_AMOUNT = XPRIN_BAL - XPRIN_AMT;
                                    XINT_CAL = (XPRIN_AMT) * (xirate) / 36500 * cal_date;
                                    YPRIN_BAL = (LBAL_PRIN) - (XPRIN_AMT);
                                    YINT_CAL = (YPRIN_BAL) * (xirate) / 100 / 12;
                                    INT_DUE = Math.Round((XINT_CAL) + (YINT_CAL));
                                }
                                else
                                {
                                    INT_DUE = Math.Round((((LBAL_PRIN) * (xirate)) / 100) / 12, 0);
                                }
                            }

                        }
                        lm.prin_amt = LBAL_PRIN;
                        lm.intdbt = INT_DUE;
                        lm.intdue = LBAL_INT;

                    }
                    lml.Add(lm);
                }

            }
            return lml;
        }
        public string PostMonthlyIntrest(MonthlyInterestScheduleForLoanViewModel model)
        {
            string vch_achd = "";
            Loan_Master lm = new Loan_Master();
            List<Loan_Master> lmlst = new List<Loan_Master>();
            lmlst = lm.getmonthlyIntrestList(model);
            int i = 10001;
            foreach (var a in lmlst)
            {
                string sql = string.Empty;
                sql = "SELECT * FROM LOAN_LEDGER WHERE BRANCH_ID='" + model.branch_id + "' AND ";
                sql = sql + "Ac_hd='" + model.ln_achd + "' and employee_id='" + a.emp_id + "' ORDER BY VCH_DATE,VCH_NO,VCH_SRL";
                config.singleResult(sql);
                if (config.dt.Rows.Count > 0)
                {
                    sql = "Select * from LNTYPE_MAST where ac_hd='" + model.ln_achd + "'";
                    config.singleResult(sql);
                    if (config.dt.Rows.Count > 0)
                    {
                        DataRow dr = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                        vch_achd = Convert.ToString(dr["int_achd"]);
                    }
                    try
                    {
                        config.Insert("LOAN_LEDGER", new Dictionary<String, object>()
                        {
                        { "branch_id",   model.branch_id },
                        { "ac_hd",       model.ln_achd },
                        { "EMPLOYEE_ID", a.emp_id},
                        { "Vch_date",    Convert.ToDateTime(model.vch_dt +" "+Convert.ToString(DateTime.Now.ToShortTimeString())) },
                        { "vch_no",      "SI"+model.ln_achd+i  },
                        { "vch_srl",     "1" },
                        { "VCH_TYPE",    "T"},
                        { "vch_achd",    vch_achd},
                        { "DR_CR",       "D" },
                        { "ref_ac_hd",   model.ln_achd },
                        { "ref_pacno",   a.emp_id },
                        { "INT_AMOUNT",  a.intdbt },
                        { "prin_bal",    a.prin_amt },
                        { "INT_DUE",     a.intdbt+a.intdue },
                        { "INSERT_MODE", "SI"},
                        });
                    }
                    catch (Exception ex)
                    {

                    }
                }
                i++;
            }

            string msg = "OVER";
            return (msg);
        }

        //public List<Loan_Master> GetLoanInfo(string BranchID, string member_no)
        //{
        //    List<Loan_Master> lml = new List<Loan_Master>();
        //    string sql = "SELECT A.*,D.AC_DESC,D.LEDGER_TAB FROM LOAN_MASTER A,LNTYPE_MAST B,ACC_HEAD D WHERE";
        //    sql = sql + " (A.AC_HD=B.AC_HD) AND (B.AC_HD=D.AC_HD) AND  ";
        //    sql = sql + "B.MEMBER_REQD='Y' AND (A.BRANCH_ID='" + BranchID + "' AND A.MEMBER_ID='" + member_no + "') AND A.CLOS_FLAG IS NULL ";
        //    sql = sql + "ORDER BY A.LOAN_DATE,A.AC_HD,A.EMPLOYEE_ID";
        //    config.singleResult(sql);
        //    if (config.dt.Rows.Count > 0)
        //    {
        //        foreach (DataRow dr in config.dt.Rows)
        //        {
        //            Loan_Master lm = new Loan_Master();                   
        //            lm.ac_desc = dr["AC_DESC"].ToString();
        //            lm.ac_hd = dr["AC_HD"].ToString();
        //            lm.ledger_tab = dr["LEDGER_TAB"].ToString();
        //            lm.emp_id = dr["EMPLOYEE_ID"].ToString();
        //            lm.loan_dt = !Convert.IsDBNull(dr["LOAN_DATE"]) ? Convert.ToDateTime(dr["LOAN_DATE"]) : Convert.ToDateTime(null);                  
        //            lm.loan_amt = !Convert.IsDBNull(dr["LOAN_AMT"]) ? Convert.ToDecimal(dr["LOAN_AMT"]) : Convert.ToDecimal("00");
        //            lm.inst_no = !Convert.IsDBNull(dr["NO_INSTL"]) ? Convert.ToInt32(dr["NO_INSTL"]) : Convert.ToInt32("00");
        //            //lm.is_secured = Convert.ToString(dr["is_secured"]);
        //            string sql1 = "SELECT * FROM " + lm.ledger_tab + " WHERE BRANCH_ID='" + BranchID + "' AND AC_HD='" + lm.ac_hd + "' AND employee_ID='" + lm.emp_id + "' ORDER BY VCH_DATE,VCH_NO,VCH_SRL";
        //            config.singleResult(sql1);
        //            if (config.dt.Rows.Count > 0)
        //            {
        //                DataRow dr1 = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
        //                //lm.prin_bal = !Convert.IsDBNull(dr1["prin_bal"]) ? Convert.ToDecimal(dr1["prin_bal"]) : Convert.ToDecimal("00");
        //                //lm.int_due = !Convert.IsDBNull(dr1["int_due"]) ? Convert.ToDecimal(dr1["int_due"]) : Convert.ToDecimal("00");
        //                //lm.tot_rcv = lm.prin_bal + lm.int_due;
        //            }
        //            lml.Add(lm);
        //        }
        //    }
        //    return lml;
        //}
    }
}