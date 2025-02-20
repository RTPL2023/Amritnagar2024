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
        public decimal prin_bal { get; set; }
        public decimal int_bal { get; set; }
        public decimal lbal_aint { get; set; }
        public decimal lbal_ch { get; set; }
        public decimal inst_amt { get; set; }
        public int inst_no { get; set; }
        public int od_month_upto { get; set; }
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
        public string int_scheme_cd { get; set; }
        public string aint_scheme_cd { get; set; }
        public string int_rate_cd { get; set; }
        public string aint_rate_cd { get; set; }
        public string INT_SCHEME_NAME { get; set; }
        public string INT_SCHEME_DESC { get; set; }
        public string INT_CATEGORY { get; set; }
        public decimal INT_ADDL_TDL { get; set; }
        public decimal INT_FRQ_MM { get; set; }
        public string INT_PRODUCT_FLAG { get; set; }
        public string INT_COMM_FLAG { get; set; }
        public decimal INT_DUE_DAY { get; set; }
        public decimal INT_RND_FLAG { get; set; }
        public string INT_RND_STAGE { get; set; }
        public string AINT_SCHEME_NAME { get; set; }
        public string AINT_SCHEME_DESC { get; set; }
        public string AINT_CHECK_OD { get; set; }
        public string AINT_APPLY_ON { get; set; }
        public string AINT_ODPERIOD_TYPE { get; set; }
        public string status_snm { get; set; }
        public decimal AINT_PERIOD_OD { get; set; }
        public decimal AINT_DUE_DAY { get; set; }
        public decimal AINT_RND_FLAG { get; set; }
        public string AINT_RND_STAGE { get; set; }
        public decimal xodprin { get; set; }
        public decimal UPPER_LIMIT { get; set; }
        public decimal INT_RATE { get; set; }
        public decimal AINT_RATE { get; set; }
        public string COMMENTS { get; set; }
        public string is_lessint_SPCL { get; set; }
        public string is_addint { get; set; }
        public DateTime WEFF_DATE { get; set; }
        public DateTime WEFF_DATE_AINT_DTL { get; set; }

        public decimal intdbt { get; set; }
        public decimal intdue { get; set; }
        public string arr_2_1 { get; set; }
        public string arr_1_1 { get; set; }
        public string arr_3_2 { get; set; }
        public string arr_3_3 { get; set; }
        public string arr_3_4 { get; set; }
        public string created_by { get; set; }
        public string modified_by { get; set; }

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
                        { "Modified_By",   lm.modified_by},
                        { "Modified_Date", DateTime.Now.ToString("dd-MM-yyyy").Replace("-","/")},
                        { "Modified_Time", System.DateTime.Now.ToShortTimeString()},
                        { "M_Device_Name", Environment.MachineName},
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
                        { "Created_by",  lm.created_by},
                        { "Create_date", DateTime.Now.ToString("dd-MM-yyyy").Replace("-","/")},
                        { "Create_Time", System.DateTime.Now.ToShortTimeString()},
                        { "Device_name", Environment.MachineName},
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
            string sql = "select * from loan_master where branch_id='" + branch_id + "' and ac_hd='" + ac_hd + "' and employee_id='" + emp_id + "' order by LOAN_DATE desc";
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
            sql = "SELECT * FROM LOAN_MASTER WHERE BRANCH_ID='" + lm.branch_id + "' AND AC_HD='" + lm.ac_hd + "' AND EMPLOYEE_ID='" + lm.emp_id + "'AND convert(datetime, LOAN_DATE, 103) = convert(datetime, '" + lm.loan_dt + "', 103)";
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
            string msg = "";
            sql = "SELECT * FROM LOAN_MASTER WHERE BRANCH_ID='" + lm.branch_id + "' AND AC_HD='" + lm.ac_hd + "' AND EMPLOYEE_ID='" + lm.emp_id + "'AND convert(datetime, LOAN_DATE, 103) = convert(datetime, '" + lm.loan_dt + "', 103)";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                try
                {
                    config.Update("LOAN_MASTER", new Dictionary<String, object>()
                    {
                        { "CLOS_FLAG",  lm.clos_flag },
                        { "CLOS_DATE",  DateTime.Now.ToString("dd-MM-yyyy").Replace("-", "/")},
                    }, new Dictionary<string, object>()
                    {
                        { "BRANCH_ID",  lm.branch_id },
                        { "LOAN_DATE",  lm.loan_dt },
                        { "EMPLOYEE_ID",lm.emp_id },
                        { "AC_HD",lm.ac_hd },
                    });
                    msg = "Closed";
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                msg = "Invalid Loan Date Supplied";
            }
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
                    //sql = "SELECT * FROM LOAN_LEDGER WHERE BRANCH_ID='" + branch_id + "' AND AC_HD='" + ac_hd + "' AND EMPLOYEE_ID='" + emp_id + "' ORDER BY VCH_DATE,VCH_NO,VCH_SRL,EMPLOYEE_ID";
                    //config.singleResult(sql);
                    //if (config.dt.Rows.Count > 0)
                    //{
                    //    DataRow dr1 = (DataRow)config.dt.Rows[0];
                    //    lm.vch_dt = Convert.ToDateTime(dr1["VCH_DATE"]);
                    //    lm.vch_no = Convert.ToString(dr1["VCH_NO"]);
                    //    lm.prin_amt = Convert.ToDecimal(dr1["PRIN_AMOUNT"]);
                    //}
                    lm.msg = "Details Found";
                }
                
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
                if (ac_hd == "" || ac_hd == null)
                {
                    sql = "select * FROM loan_master WHERE BRANCH_ID='" + branch_id + "' AND ";
                    sql = sql + "convert(date, loan_date, 103) >= convert(date, '" + fr_dt + "', 103) and convert(date, loan_date, 103) <= convert(date, '" + to_dt + "', 103)";
                    sql = sql + "ORDER BY ac_hd,EMPLOYEE_id,loan_date";
                    config.singleResult(sql);
                }
                else
                {
                    sql = "select * FROM loan_master WHERE BRANCH_ID='" + branch_id + "' AND AC_HD = '" + ac_hd + "' and convert(date, loan_date, 103) >= convert(date, '" + fr_dt + "', 103) and convert(date, loan_date, 103) <= convert(date, '" + to_dt + "', 103)" +
                    "ORDER BY ac_hd,EMPLOYEE_id,loan_date";
                    config.singleResult(sql);
                }
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
                if (ac_hd == "" || ac_hd == null)
                {
                    sql = "select * FROM loan_master WHERE BRANCH_ID='" + branch_id + "' AND ";
                    sql = sql + "iif(clos_date is null,convert(date, '31/03/2099', 103),convert(date, CLOS_DATE, 103)) >= convert(date, '" + fr_dt + "', 103) and convert(date, CLOS_DATE, 103) <= convert(date, '" + to_dt + "', 103) ";
                    sql = sql + "AND clos_flag is not null ORDER BY ac_hd,EMPLOYEE_id,clos_date";
                    config.singleResult(sql);
                }
                else
                {
                    sql = "select * FROM loan_master WHERE BRANCH_ID='" + branch_id + "' AND ";
                    sql = sql + "AC_HD = '" + ac_hd + "' and iif(clos_date is null,convert(date, '31/03/2099', 103),convert(date, CLOS_DATE, 103)) >= convert(date, '" + fr_dt + "', 103) and convert(date, CLOS_DATE, 103) <= convert(date, '" + to_dt + "', 103) ";
                    sql = sql + "AND clos_flag is not null ORDER BY ac_hd,EMPLOYEE_id,clos_date";
                    config.singleResult(sql);
                }
                //sql = "select * FROM loan_master WHERE BRANCH_ID='" + branch_id + "' AND AC_HD = '" + ac_hd + "' and convert(datetime, CLOS_DATE, 103) >= convert(datetime, '" + fr_dt + "', 103) and convert(datetime, CLOS_DATE, 103) <= convert(datetime, '" + to_dt + "', 103) AND clos_flag = 'C'" +
                //    "ORDER BY ac_hd,EMPLOYEE_id,clos_date";
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
                        sql = "select * from loan_ledger where BRANCH_ID='" + branch_id + "' AND ac_hd='" + ac_hd + "' and EMPLOYEE_id='" + lm.emp_id + "' and convert(varchar, vch_date, 103) = convert(varchar, '" + lm.clos_dt + "', 103) AND DR_CR='C' order by vch_date,vch_srl";
                        config.singleResult(sql);                     
                        if (config.dt.Rows.Count > 0)
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
        public Loan_Master getInterestAmtfromRecovery_Schedule(MonthlyInterestScheduleForLoanViewModel model,string emp_id)
        {           
            string qryMEM = string.Empty;
            qryMEM = "SELECT * FROM RECOVERY_SCHEDULE WHERE BRANCH_ID='" + model.branch_id + "' AND ";
            qryMEM = qryMEM + "convert(datetime, SCH_DATE, 103) = convert(datetime, '" + model.sch_date + "', 103) AND ac_hd='"+model.ln_achd+"' and";
            qryMEM = qryMEM + " book_no='" + model.book_no + "' and employer_branch='" + model.colliery_code + "' and EMPLOYEE_ID='"+ emp_id + "'";
            qryMEM = qryMEM + "ORDER BY EMPLOYER_BRANCH,BOOK_NO,EMPLOYEE_ID";

            config.singleResult(qryMEM);
            Loan_Master lm = new Loan_Master();
            if (config.dt.Rows.Count > 0)
            {
                //decimal NHD = 0;               
                foreach (DataRow dr in config.dt.Rows)
                {
                  
                  
                    lm.intdbt = Convert.ToDecimal(dr["INT_AMT"]);

                 
                }
            }
            return lm;
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
                            //LBAL_PRIN = Convert.ToDecimal(dr3["prin_bal"]);
                            LBAL_PRIN = !Convert.IsDBNull(dr3["prin_bal"]) ? Convert.ToDecimal(dr3["prin_bal"]) : Convert.ToDecimal("0.00");
                            //LBAL_INT = Convert.ToDecimal(dr3["INT_DUE"]);
                            LBAL_INT = !Convert.IsDBNull(dr3["INT_DUE"]) ? Convert.ToDecimal(dr3["INT_DUE"]) : Convert.ToDecimal("0.00");
                            if (LBAL_PRIN > 0)
                            {
                                FLAG = Convert.ToString(dr["FLAG"]);
                                if (lm.loan_dt.Month == Convert.ToDateTime(model.fr_dt).Month && lm.loan_dt.Year == Convert.ToDateTime(model.fr_dt).Year)
                                {
                                    //cal_date = Convert.ToDateTime(lm.loan_dt - Convert.ToDateTime(model.fr_dt).AddDays(1)).Day;
                                    cal_date = Convert.ToInt32(lm.loan_dt.Subtract(Convert.ToDateTime(model.fr_dt)).TotalDays) + 1;
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
                lm = lm.getInterestAmtfromRecovery_Schedule(model, a.emp_id);
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
                        { "INT_AMOUNT", lm.intdbt },
                        { "prin_bal",    a.prin_amt },
                        { "INT_DUE",     lm.intdbt+a.intdue },
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
        //                lm.prin_bal = !Convert.IsDBNull(dr1["prin_bal"]) ? Convert.ToDecimal(dr1["prin_bal"]) : Convert.ToDecimal("00");
        //                //lm.int_due = !Convert.IsDBNull(dr1["int_due"]) ? Convert.ToDecimal(dr1["int_due"]) : Convert.ToDecimal("00");
        //                //lm.tot_rcv = lm.prin_bal + lm.int_due;
        //            }
        //            lml.Add(lm);
        //        }
        //    }
        //    return lml;
        //}
        public List<Loan_Master> getmemberloandetails(string BranchID, string member_id)
        {
            List<Loan_Master> lml = new List<Loan_Master>();
            string sql = "SELECT distinct A.ac_hd  as dachd  FROM LOAN_MASTER A,LNTYPE_MAST B,ACC_HEAD D WHERE ";
            sql = sql + "(A.AC_HD=B.AC_HD) AND (B.AC_HD=D.AC_HD) AND ";
            sql = sql + "B.MEMBER_REQD='Y' AND (A.BRANCH_ID='" + BranchID + "' AND A.MEMBER_ID='" + member_id + "') AND A.CLOS_FLAG IS NULL ";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                DataTable dtab = config.dt;             
                foreach(DataRow drr in dtab.Rows)
                {
                    string dachd = Convert.ToString(drr["dachd"]);
                    sql = "SELECT A.*,D.AC_DESC,D.LEDGER_TAB FROM LOAN_MASTER A,LNTYPE_MAST B,ACC_HEAD D WHERE ";
                    sql = sql + "(A.AC_HD=B.AC_HD) AND (B.AC_HD=D.AC_HD) AND ";
                    sql = sql + "B.MEMBER_REQD='Y' AND (A.BRANCH_ID='" + BranchID + "' AND A.MEMBER_ID='" + member_id + "') AND A.CLOS_FLAG IS NULL and A.AC_HD='"+ dachd + "'";
                    sql = sql + "ORDER BY A.LOAN_DATE,A.AC_HD,A.EMPLOYEE_ID";                  
                    config.singleResult(sql);
                    if (config.dt.Rows.Count > 0)
                    {
                        DataRow dr = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];                       
                        Loan_Master lm = new Loan_Master();
                        lm.ac_desc = dr["AC_DESC"].ToString();
                        lm.ac_hd = dr["ac_hd"].ToString();
                        lm.emp_id = dr["EMPLOYEE_ID"].ToString();
                        lm.loan_dt = !Convert.IsDBNull(dr["LOAN_DATE"]) ? Convert.ToDateTime(dr["LOAN_DATE"]) : Convert.ToDateTime(null);
                        lm.loan_amt = !Convert.IsDBNull(dr["LOAN_AMT"]) ? Convert.ToDecimal(dr["LOAN_AMT"]) : Convert.ToDecimal("0.00");
                        lm.inst_rate = !Convert.IsDBNull(dr["INT_RATE"]) ? Convert.ToDecimal(dr["INT_RATE"]) : Convert.ToDecimal("0.00");
                        lm.inst_no = Convert.ToInt32(dr["NO_INSTL"]);
                        lm.ledger_tab = Convert.ToString(dr["LEDGER_TAB"]);
                        lm.ln_spcl = Convert.ToString(dr["ln_SPEACIAL"]);
                        lm.branch_id = BranchID;
                        lm.mem_id = member_id;
                        //lml.Add(lm);
                        sql = "SELECT * FROM " + lm.ledger_tab + " WHERE BRANCH_ID='" + BranchID + "' AND AC_HD='" + lm.ac_hd + "' AND employee_ID='" + lm.emp_id + "' ORDER BY VCH_DATE,VCH_NO,VCH_SRL";
                        config.singleResult(sql);
                        if (config.dt.Rows.Count > 0)
                        {
                            DataRow dr1 = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                            lm.prin_bal = !Convert.IsDBNull(dr1["prin_bal"]) ? Convert.ToDecimal(dr1["prin_bal"]) : Convert.ToDecimal("00");
                            lml.Add(lm);
                        }                       
                    }
                }
            }                          
            return lml;
        }
        public Loan_Master getloanledger(string ledger_tab, string ac_hd, string branch_id, string emp_id, decimal loan_amt, int inst_no, string ln_spcl, decimal inst_rate, DateTime loan_dt)
        {
            Loan_Master lm = new Loan_Master();
            DataTable dt_rsled = new DataTable();
            string sql = string.Empty;
            sql = "SELECT * FROM " + ledger_tab + " WHERE BRANCH_ID='" + branch_id + "' AND AC_HD='" + ac_hd + "' AND employee_ID='" + emp_id + "' ORDER BY VCH_DATE,VCH_NO,VCH_SRL";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                dt_rsled = config.dt;
                //DataRow dr1 = (DataRow)dt_rsled.Rows[dt_rsled.Rows.Count - 1];
                lm = get_loan_current_due(ac_hd, loan_amt, inst_no, ln_spcl, inst_rate, loan_dt, dt_rsled);
            }
            return lm;
        }
        public List<Loan_Master> getmemberotherloandetails(string BranchID, string member_id)
        {
            string sql = "SELECT A.*,D.AC_DESC,D.LEDGER_TAB FROM LOAN_MASTER A,LNTYPE_MAST B,ACC_HEAD D,LOAN_CUSTOMER C WHERE ";
            sql = sql + "(A.AC_HD=B.AC_HD) AND (B.AC_HD=D.AC_HD) AND (A.BRANCH_ID=C.BRANCH_ID AND A.AC_HD=C.AC_HD AND ";
            sql = sql + "A.employee_ID=C.employee_ID) AND B.MEMBER_REQD<>'Y' AND (C.BRANCH_ID='" + BranchID + "' AND C.CUST_ID IN ('" + member_id + "')) AND A.CLOS_FLAG IS NULL ";
            sql = sql + "ORDER BY A.LOAN_DATE,A.AC_HD,A.employee_ID";
            List<Loan_Master> lml = new List<Loan_Master>();
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    Loan_Master lm = new Loan_Master();
                    lm.ac_desc = dr["AC_DESC"].ToString();
                    lm.ac_hd = dr["ac_hd"].ToString();
                    lm.emp_id = dr["EMPLOYEE_ID"].ToString();
                    lm.loan_dt = !Convert.IsDBNull(dr["LOAN_DATE"]) ? Convert.ToDateTime(dr["LOAN_DATE"]) : Convert.ToDateTime(null);
                    lm.loan_amt = !Convert.IsDBNull(dr["LOAN_AMT"]) ? Convert.ToDecimal(dr["LOAN_AMT"]) : Convert.ToDecimal("0.00");
                    lm.inst_no = Convert.ToInt32(dr["NO_INSTL"]);
                    lm.ledger_tab = Convert.ToString(dr["LEDGER_TAB"]);
                    sql = "SELECT * FROM " + lm.ledger_tab + " WHERE BRANCH_ID='" + BranchID + "' AND AC_HD='" + lm.ac_hd + "' AND employee_ID='" + lm.emp_id + "' ORDER BY VCH_DATE,VCH_NO,VCH_SRL";
                    config.singleResult(sql);
                    if (config.dt.Rows.Count > 0)
                    {
                        DataRow dr1 = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                        lml.Add(lm);
                    }
                }
            }
            return lml;
        }
        public Loan_Master get_loan_current_due(string ac_hd, decimal loan_amt, int inst_no, string ln_spcl, decimal inst_rate, DateTime loan_dt, DataTable dt_rsled /*string branch_id, string member_id*/)
        {
            List<Loan_Master> lml = new List<Loan_Master>();
            Loan_Master lm = new Loan_Master();
            string sql = string.Empty;
            DataTable dt_lnscheme = new DataTable();
            DataTable dt_rslntype = new DataTable();
            DataTable dt_rsaintschm = new DataTable();
            DataTable dt_rsaintrate = new DataTable();
            DataTable dt_rsintrate = new DataTable();
            sql = "select * from LNSTATUS_MAST ORDER BY OD_MONTH_UPTO";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr6 in config.dt.Rows)
                {
                    lm.od_month_upto = !Convert.IsDBNull(dr6["OD_MONTH_UPTO"]) ? Convert.ToInt32(dr6["OD_MONTH_UPTO"]) : Convert.ToInt32("0");
                }
            }
            sql = "select * from lntype_mast where ac_hd='" + ac_hd + "'";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                dt_rslntype = config.dt;
                foreach (DataRow dr in dt_rslntype.Rows)
                {
                    lm.int_scheme_cd = !Convert.IsDBNull(dr["INT_SCHEME_CD"]) ? Convert.ToString(dr["INT_SCHEME_CD"]) : Convert.ToString("");
                    lm.aint_scheme_cd = !Convert.IsDBNull(dr["AINT_SCHEME_CD"]) ? Convert.ToString(dr["AINT_SCHEME_CD"]) : Convert.ToString("");
                    lm.int_rate_cd = !Convert.IsDBNull(dr["INT_RATE_CD"]) ? Convert.ToString(dr["INT_RATE_CD"]) : Convert.ToString("");
                    lm.aint_rate_cd = !Convert.IsDBNull(dr["AINT_RATE_CD"]) ? Convert.ToString(dr["AINT_RATE_CD"]) : Convert.ToString("");
                    lm.is_lessint_SPCL = !Convert.IsDBNull(dr["is_lessint_SPCL"]) ? Convert.ToString(dr["is_lessint_SPCL"]) : Convert.ToString("");
                    lm.is_addint = !Convert.IsDBNull(dr["IS_ADDINT"]) ? Convert.ToString(dr["IS_ADDINT"]) : Convert.ToString("");
                    sql = "SELECT * FROM LNSCHEME_INT WHERE INT_SCHEME_CD='" + lm.int_scheme_cd + "'";
                    config.singleResult(sql);
                    if (config.dt.Rows.Count > 0)
                    {
                        dt_lnscheme = config.dt;
                        foreach (DataRow dr1 in dt_lnscheme.Rows)
                        {
                            lm.int_scheme_cd = !Convert.IsDBNull(dr1["INT_SCHEME_CD"]) ? Convert.ToString(dr1["INT_SCHEME_CD"]) : Convert.ToString("");
                            lm.INT_SCHEME_NAME = !Convert.IsDBNull(dr1["INT_SCHEME_NAME"]) ? Convert.ToString(dr1["INT_SCHEME_NAME"]) : Convert.ToString("");
                            lm.INT_SCHEME_DESC = !Convert.IsDBNull(dr1["INT_SCHEME_DESC"]) ? Convert.ToString(dr1["INT_SCHEME_DESC"]) : Convert.ToString("");
                            lm.INT_CATEGORY = !Convert.IsDBNull(dr1["INT_CATEGORY"]) ? Convert.ToString(dr1["INT_CATEGORY"]) : Convert.ToString("");
                            lm.INT_PRODUCT_FLAG = !Convert.IsDBNull(dr1["INT_PRODUCT_FLAG"]) ? Convert.ToString(dr1["INT_PRODUCT_FLAG"]) : Convert.ToString("");
                            lm.INT_COMM_FLAG = !Convert.IsDBNull(dr1["INT_COMM_FLAG"]) ? Convert.ToString(dr1["INT_COMM_FLAG"]) : Convert.ToString("");
                            lm.INT_RND_STAGE = !Convert.IsDBNull(dr1["INT_RND_STAGE"]) ? Convert.ToString(dr1["INT_RND_STAGE"]) : Convert.ToString("");
                            lm.INT_ADDL_TDL = !Convert.IsDBNull(dr1["INT_ADDL_TDL"]) ? Convert.ToDecimal(dr1["INT_ADDL_TDL"]) : Convert.ToDecimal("0.00");
                            lm.INT_FRQ_MM = !Convert.IsDBNull(dr1["INT_FRQ_MM"]) ? Convert.ToDecimal(dr1["INT_FRQ_MM"]) : Convert.ToDecimal("0.00");
                            lm.INT_DUE_DAY = !Convert.IsDBNull(dr1["INT_DUE_DAY"]) ? Convert.ToDecimal(dr1["INT_DUE_DAY"]) : Convert.ToDecimal("0.00");
                            lm.INT_RND_FLAG = !Convert.IsDBNull(dr1["INT_RND_FLAG"]) ? Convert.ToDecimal(dr1["INT_RND_FLAG"]) : Convert.ToDecimal("0.00");
                        }
                    }
                    sql = "SELECT * FROM LNSCHEME_AINT WHERE AINT_SCHEME_CD='" + lm.aint_scheme_cd + "'";
                    config.singleResult(sql);
                    if (config.dt.Rows.Count > 0)
                    {
                        dt_rsaintschm = config.dt;
                        foreach (DataRow dr2 in dt_rsaintschm.Rows)
                        {
                            lm.aint_scheme_cd = !Convert.IsDBNull(dr2["AINT_SCHEME_CD"]) ? Convert.ToString(dr2["AINT_SCHEME_CD"]) : Convert.ToString("");
                            lm.AINT_SCHEME_NAME = !Convert.IsDBNull(dr2["AINT_SCHEME_NAME"]) ? Convert.ToString(dr2["AINT_SCHEME_NAME"]) : Convert.ToString("");
                            lm.AINT_SCHEME_DESC = !Convert.IsDBNull(dr2["AINT_SCHEME_DESC"]) ? Convert.ToString(dr2["AINT_SCHEME_DESC"]) : Convert.ToString("");
                            lm.AINT_CHECK_OD = !Convert.IsDBNull(dr2["AINT_CHECK_OD"]) ? Convert.ToString(dr2["AINT_CHECK_OD"]) : Convert.ToString("");
                            lm.AINT_APPLY_ON = !Convert.IsDBNull(dr2["AINT_APPLY_ON"]) ? Convert.ToString(dr2["AINT_APPLY_ON"]) : Convert.ToString("");
                            lm.AINT_ODPERIOD_TYPE = !Convert.IsDBNull(dr2["AINT_ODPERIOD_TYPE"]) ? Convert.ToString(dr2["AINT_ODPERIOD_TYPE"]) : Convert.ToString("");
                            lm.AINT_RND_STAGE = !Convert.IsDBNull(dr2["AINT_RND_STAGE"]) ? Convert.ToString(dr2["AINT_RND_STAGE"]) : Convert.ToString("");
                            lm.AINT_PERIOD_OD = !Convert.IsDBNull(dr2["AINT_PERIOD_OD"]) ? Convert.ToDecimal(dr2["AINT_PERIOD_OD"]) : Convert.ToDecimal("0.00");
                            lm.AINT_DUE_DAY = !Convert.IsDBNull(dr2["AINT_DUE_DAY"]) ? Convert.ToDecimal(dr2["AINT_DUE_DAY"]) : Convert.ToDecimal("0.00");
                            lm.AINT_RND_FLAG = !Convert.IsDBNull(dr2["AINT_RND_FLAG"]) ? Convert.ToDecimal(dr2["AINT_RND_FLAG"]) : Convert.ToDecimal("0.00");
                        }
                    }
                    sql = "SELECT * FROM LNINTRATE_DTL WHERE INT_RATE_CD='" + lm.int_rate_cd + "'ORDER BY WEFF_DATE,UPPER_LIMIT";
                    config.singleResult(sql);
                    if (config.dt.Rows.Count > 0)
                    {
                        dt_rsintrate = config.dt;
                        foreach (DataRow dr3 in dt_rsintrate.Rows)
                        {
                            lm.WEFF_DATE = !Convert.IsDBNull(dr3["WEFF_DATE"]) ? Convert.ToDateTime(dr3["WEFF_DATE"]) : Convert.ToDateTime(null);
                            lm.int_rate_cd = !Convert.IsDBNull(dr3["INT_RATE_CD"]) ? Convert.ToString(dr3["INT_RATE_CD"]) : Convert.ToString("");
                            lm.UPPER_LIMIT = !Convert.IsDBNull(dr3["UPPER_LIMIT"]) ? Convert.ToDecimal(dr3["UPPER_LIMIT"]) : Convert.ToDecimal("0.00");
                            lm.INT_RATE = !Convert.IsDBNull(dr3["INT_RATE"]) ? Convert.ToDecimal(dr3["INT_RATE"]) : Convert.ToDecimal("0.00");
                            lm.COMMENTS = !Convert.IsDBNull(dr3["COMMENTS"]) ? Convert.ToString(dr3["COMMENTS"]) : Convert.ToString("");
                        }
                    }
                    sql = "SELECT * FROM LNAINTRATE_DTL WHERE AINT_RATE_CD='" + lm.aint_rate_cd + "' ORDER BY WEFF_DATE";
                    config.singleResult(sql);
                    if (config.dt.Rows.Count > 0)
                    {
                        dt_rsaintrate = config.dt;
                        foreach (DataRow dr4 in dt_rsaintrate.Rows)
                        {
                            lm.WEFF_DATE_AINT_DTL = !Convert.IsDBNull(dr4["WEFF_DATE"]) ? Convert.ToDateTime(dr4["WEFF_DATE"]) : Convert.ToDateTime(null);
                            lm.aint_rate_cd = !Convert.IsDBNull(dr4["AINT_RATE_CD"]) ? Convert.ToString(dr4["AINT_RATE_CD"]) : Convert.ToString("");
                            lm.AINT_RATE = !Convert.IsDBNull(dr4["AINT_RATE"]) ? Convert.ToDecimal(dr4["AINT_RATE"]) : Convert.ToDecimal("0.00");
                        }
                    }
                }
            }
            decimal xinstl = 0;
            decimal XLESS_INT = 0;
            decimal xirate = 0;
            DateTime xfrdt = Convert.ToDateTime(DateTime.Now.ToString("dd-MM-yyyy").Replace("-", "/"));
            if ((loan_amt % inst_no) > 0)
            {
                xinstl = (loan_amt / inst_no) + 1;
            }
            else
            {
                xinstl = loan_amt / inst_no;
            }
            if (lm.is_lessint_SPCL != null)
            {
                sql = "select * from lncategory_mast where ln_speacial='" + ln_spcl + "'";
                config.singleResult(sql);
                if (config.dt.Rows.Count > 0)
                {
                    foreach (DataRow dr5 in config.dt.Rows)
                    {
                        XLESS_INT = !Convert.IsDBNull(dr5["LESS_INT"]) ? Convert.ToDecimal(dr5["LESS_INT"]) : Convert.ToDecimal("0.00");
                    }
                }
            }
            if (lm.INT_CATEGORY == "F")
            {
                xirate = inst_rate;
            }
            else
            {
                xirate = 0;
            }
            lm = CAL_LOAN_RECV(loan_dt, xinstl, loan_amt, xfrdt, dt_rsled, dt_lnscheme, dt_rsaintschm, dt_rsintrate, dt_rsaintrate, dt_rslntype, xirate, XLESS_INT, null, 0);
            //lm.prin_bal = Convert.ToDecimal(LN_RECV_ARRAY[1, 1]);
            //lm.int_bal = Convert.ToDecimal(LN_RECV_ARRAY[3, 2]);
            //lm.lbal_aint = Convert.ToDecimal(LN_RECV_ARRAY[3, 3]);
            //lm.lbal_ch = Convert.ToDecimal(LN_RECV_ARRAY[3, 4]);
            lm.prin_bal = Convert.ToDecimal(lm.arr_1_1);
            lm.int_bal = Convert.ToDecimal(lm.arr_3_2);
            lm.lbal_aint = Convert.ToDecimal(lm.arr_3_3);
            lm.lbal_ch = Convert.ToDecimal(lm.arr_3_4);
            int xoddue = 0;
            string xodfrom = "";
            lm.xodprin = 0;
            lm.xodprin = Convert.ToDecimal(lm.arr_2_1);
            if (lm.xodprin > 0 && lm.is_addint != "Y")
            {
                int add_month = Convert.ToInt32(((loan_amt - lm.prin_bal) / xinstl) + 1);
                DateTime XCLRUPTO = loan_dt.AddMonths(add_month);
                xoddue = Convert.ToInt32(DateTime.Now.Subtract(XCLRUPTO).Days / (365.25 / 12));
                xodfrom = XCLRUPTO.ToString("dd/MM/yyyy");
            }
            else
            {
                lm.xodprin = 0;
                xoddue = 0;
                xodfrom = "";
            }
            if (lm.od_month_upto > xoddue)
            {
                if (config.dt.Rows.Count > 0)
                {
                    DataRow dr8 = (DataRow)config.dt.Rows[0];
                    lm.status_snm = !Convert.IsDBNull(dr8["STATUS_SNM"]) ? Convert.ToString(dr8["STATUS_SNM"]) : Convert.ToString("");
                }
            }
            //lml.Add(lm);
            return lm;
        }
        public Loan_Master CAL_LOAN_RECV(DateTime XLOAN_DATE, decimal XINSTL_PRIN, decimal XLOAN_AMT, DateTime LCAL_DATE, DataTable dt_rsled, DataTable dt_lnscheme, DataTable dt_rsaintschm, DataTable dt_rsintrate, DataTable dt_rsaintrate, DataTable dt_rslntype, decimal FIXED_INTRT, decimal LESS_INT, string before_date, int out_intrt)
        {
            List<Loan_Master> lmlst = new List<Loan_Master>();
            Loan_Master lm = new Loan_Master();
            //string[] LN_RECV_ARRAY = new string[16];
            string arr_1_1 = "";
            string arr_1_2 = "";
            string arr_1_3 = "";
            string arr_1_4 = "";
            string arr_2_1 = "";
            string arr_2_2 = "";
            string arr_2_3 = "";
            string arr_2_4 = "";
            string arr_3_1 = "";
            decimal arr_3_2 = 0;
            decimal arr_3_3 = 0;
            string arr_3_4 = "";
            string arr_4_1 = "";
            string arr_4_2 = "";
            string arr_4_3 = "";
            string arr_4_4 = "";
            string[,] LN_RECV_ARRAY = new string[4, 1];
            int i = 0;
            int j = 0;
            //for (i = 1; i <= 4; i++)
            //{
            //    for (j = 1; j <= 4; j++)
            //    {
            //        LN_RECV_ARRAY[i, j] = null;                   
            //    }
            //}
            decimal xlprbal = 0;
            decimal xlintbal = 0;
            decimal xlaintbal = 0;
            decimal xchbal = 0;
            decimal xrprin = 0;
            decimal xrint = 0;
            decimal xraint = 0;
            DateTime xlast_trdt = new DateTime();
            DateTime XEFF_DT = new DateTime();
            if (dt_rsled.Rows.Count == 0)
            {
                return lm;
            }
            if (dt_lnscheme.Rows.Count == 0)
            {
                return lm;
            }
            if (dt_rsintrate.Rows.Count == 0)
            {
                if (FIXED_INTRT == 0)
                {
                    return lm;
                }
            }
            if (before_date == null)
            {
                DataRow dr = (DataRow)dt_rsled.Rows[dt_rsled.Rows.Count - 1];
                xlprbal = !Convert.IsDBNull(dr["prin_bal"]) ? Convert.ToDecimal(dr["prin_bal"]) : Convert.ToDecimal("0");
                xlintbal = !Convert.IsDBNull(dr["INT_DUE"]) ? Convert.ToDecimal(dr["INT_DUE"]) : Convert.ToDecimal("0");
                xlaintbal = !Convert.IsDBNull(dr["AINT_DUE"]) ? Convert.ToDecimal(dr["AINT_DUE"]) : Convert.ToDecimal("0");
                xchbal = !Convert.IsDBNull(dr["ichrg_due"]) ? Convert.ToDecimal(dr["ichrg_due"]) : Convert.ToDecimal("0");
                lm.vch_dt = !Convert.IsDBNull(dr["VCH_DATE"]) ? Convert.ToDateTime(dr["VCH_DATE"]) : Convert.ToDateTime(null);
            }
            else
            {
                if (dt_rsled.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt_rsled.Rows)
                    {
                        lm.vch_dt = !Convert.IsDBNull(dr["VCH_DATE"]) ? Convert.ToDateTime(dr["VCH_DATE"]) : Convert.ToDateTime(null);
                        if (lm.vch_dt < Convert.ToDateTime(before_date))
                        {
                            xlprbal = !Convert.IsDBNull(dr["prin_bal"]) ? Convert.ToDecimal(dr["prin_bal"]) : Convert.ToDecimal("0");
                            xlintbal = !Convert.IsDBNull(dr["INT_DUE"]) ? Convert.ToDecimal(dr["INT_DUE"]) : Convert.ToDecimal("0");
                            xlaintbal = !Convert.IsDBNull(dr["AINT_DUE"]) ? Convert.ToDecimal(dr["AINT_DUE"]) : Convert.ToDecimal("0");
                            xchbal = !Convert.IsDBNull(dr["ichrg_due"]) ? Convert.ToDecimal(dr["ichrg_due"]) : Convert.ToDecimal("0");
                        }
                    }
                }
            }
            //LN_RECV_ARRAY[1, 1] = xlprbal.ToString("0.00");
            //LN_RECV_ARRAY[1, 2] = xlintbal.ToString("0.00");
            //LN_RECV_ARRAY[1, 3] = xlaintbal.ToString("0.00");
            //LN_RECV_ARRAY[1, 4] = xchbal.ToString("0.00");

            //LN_RECV_ARRAY[2, 2] = xlintbal.ToString("0.00");
            //LN_RECV_ARRAY[2, 3] = xlaintbal.ToString("0.00");
            //LN_RECV_ARRAY[2, 4] = xchbal.ToString("0.00");

            arr_1_1 = xlprbal.ToString("0.00");
            arr_1_2 = xlintbal.ToString("0.00");
            arr_1_3 = xlaintbal.ToString("0.00");
            arr_1_4 = xchbal.ToString("0.00");

            arr_2_2 = xlintbal.ToString("0.00");
            arr_2_3 = xlaintbal.ToString("0.00");
            arr_2_4 = xchbal.ToString("0.00");


            xlast_trdt = lm.vch_dt;
            decimal xodprin = 0;
            xodprin = (Convert.ToInt32(Convert.ToDateTime(LCAL_DATE).Subtract(Convert.ToDateTime(XLOAN_DATE.AddDays(1))).Days / (365.25 / 12)) * XINSTL_PRIN) - (XLOAN_AMT - xlprbal);
            if (xodprin < 0)
            {
                xodprin = 0;
            }
            if (xodprin > xlprbal)
            {
                xodprin = xlprbal;
            }
            // LN_RECV_ARRAY[2, 1] = xodprin.ToString("0.00");
            arr_2_1 = xlintbal.ToString("0.00");
            //  LN_RECV_ARRAY[3, 1] = ((Convert.ToInt32(Convert.ToDateTime(LCAL_DATE).Subtract(Convert.ToDateTime(XLOAN_DATE)).Days / (365.25 / 12)) * XINSTL_PRIN) - (XLOAN_AMT - xlprbal)).ToString("0.00");
            arr_3_1 = ((Convert.ToInt32(Convert.ToDateTime(LCAL_DATE).Subtract(Convert.ToDateTime(XLOAN_DATE)).Days / (365.25 / 12)) * XINSTL_PRIN) - (XLOAN_AMT - xlprbal)).ToString("0.00");
            //if(Convert.ToInt32(LN_RECV_ARRAY[3, 1]) < 0)
            //{
            //    LN_RECV_ARRAY[3, 1] = 0.ToString("0.00");
            //}
            if (Convert.ToDecimal(arr_3_1) < 0)
            {
                arr_3_1 = 0.ToString("0.00");
            }
            //if (Convert.ToInt32(LN_RECV_ARRAY[3, 1]) > xlprbal)
            //{
            //    LN_RECV_ARRAY[3, 1] = xlprbal.ToString("0.00");
            //}
            if (Convert.ToDecimal(arr_3_1) > xlprbal)
            {
                arr_3_1 = xlprbal.ToString("0.00");
            }
            //LN_RECV_ARRAY[3, 4] = xchbal.ToString("0.00");
            arr_3_4 = xchbal.ToString("0.00");
            DateTime xfrdt = new DateTime();
            DateTime XTODT = new DateTime();
            DateTime XCALUPTO = new DateTime();
            string int_prdct_flag = "";
            decimal XRATE = 0;
            decimal XCALINT = 0;
            if (dt_lnscheme.Rows.Count > 0)
            {
                DataRow dr = (DataRow)dt_lnscheme.Rows[dt_lnscheme.Rows.Count - 1];
                int_prdct_flag = dr["INT_PRODUCT_FLAG"].ToString();
            }
            switch (int_prdct_flag)
            {
                case "D":
                    int XIPERIOD = 0;

                    XIPERIOD = Convert.ToDateTime(LCAL_DATE).Subtract(xlast_trdt).Days;
                    if (XIPERIOD == 0)
                    {
                        XCALINT = 0;
                    }
                    else
                    {
                        if (FIXED_INTRT != 0)
                        {
                            XRATE = FIXED_INTRT - LESS_INT;

                            if (out_intrt != 0)
                            {
                                switch (out_intrt)
                                {
                                    case 1:

                                        break;
                                    case 2:

                                        break;
                                }
                            }
                            XCALINT = (xlprbal * XIPERIOD * FIXED_INTRT) / 36500;
                        }
                        else
                        {
                            XCALINT = 0;
                            xfrdt = xlast_trdt;
                            XTODT = xfrdt;
                            XCALUPTO = xlast_trdt;
                            if (dt_rsintrate.Rows.Count > 0)
                            {
                                foreach (DataRow dr in dt_rsintrate.Rows)
                                {
                                    if (XTODT >= LCAL_DATE)
                                    {
                                        break;
                                    }
                                    DateTime weff_date = !Convert.IsDBNull(dr["weff_date"]) ? Convert.ToDateTime(dr["weff_date"]) : Convert.ToDateTime(null);
                                    decimal UPPER_LIMIT = !Convert.IsDBNull(dr["UPPER_LIMIT"]) ? Convert.ToDecimal(dr["UPPER_LIMIT"]) : Convert.ToDecimal("0.00");
                                    decimal INT_RATE = !Convert.IsDBNull(dr["INT_RATE"]) ? Convert.ToDecimal(dr["INT_RATE"]) : Convert.ToDecimal("0.00");
                                    if (weff_date <= xfrdt)
                                    {
                                        DataRow dr1 = (DataRow)dt_rsintrate.Rows[dt_rsintrate.Rows.Count - 1];
                                        XEFF_DT = weff_date;
                                    }
                                    if (weff_date == XEFF_DT && (UPPER_LIMIT >= XLOAN_AMT))
                                    {
                                        DataRow dr2 = (DataRow)dt_rsintrate.Rows[0];
                                    }
                                    XRATE = INT_RATE - LESS_INT;
                                    if (weff_date > xfrdt)
                                    {
                                        DataRow dr3 = (DataRow)dt_rsintrate.Rows[0];
                                        XTODT = LCAL_DATE;
                                    }
                                    else
                                    {
                                        if (weff_date > LCAL_DATE)
                                        {
                                            XTODT = LCAL_DATE;
                                        }
                                        else
                                        {
                                            XTODT = weff_date.AddDays(-1);
                                        }
                                    }
                                    if (out_intrt != 0)
                                    {
                                        switch (out_intrt)
                                        {
                                            case 1:
                                                break;
                                            case 2:
                                                break;
                                        }
                                    }
                                    XCALINT = XCALINT + ((xlprbal * (Convert.ToDateTime(XTODT).Subtract(XCALUPTO).Days) * XRATE));
                                    xfrdt = XTODT.AddDays(1);
                                    XCALUPTO = XCALUPTO.AddDays(Convert.ToDateTime(XTODT).Subtract(XCALUPTO).Days);
                                }
                            }
                            if (XCALINT < 0)
                            {
                                XCALINT = 0;
                            }
                            XCALINT = XCALINT + xlintbal;
                            //LN_RECV_ARRAY[3, 2] = Math.Round(XCALINT, 0).ToString("0.00");
                            arr_3_2 = Math.Round(XCALINT, 0);
                        }
                    }
                    break;
            }
            decimal XAION = 0;
            int XAIPERIOD = 0;
            int XPOD = 0;
            int XINSTPAYB = 0;
            decimal XPAYB = 0;
            decimal XLESSPAID = 0;
            decimal XCALAINT = 0;
            decimal AINT_PERIOD_OD = 0;
            Boolean XAIOK = true;
            DateTime XCLRUPTO = new DateTime();
            XCALAINT = 0;
            string IS_ADDINT = "";
            string XPTYPE = "";
            string AINT_APPLY_ON = "";
            string AINT_CHECK_OD = "";
            string AINT_ODPERIOD_TYPE = "";
            if (dt_rslntype.Rows.Count > 0)
            {
                DataRow dr = (DataRow)dt_rslntype.Rows[dt_rslntype.Rows.Count - 1];
                IS_ADDINT = dr["IS_ADDINT"].ToString();
            }
            if (dt_rsaintschm.Rows.Count > 0)
            {
                DataRow dr = (DataRow)dt_rsaintschm.Rows[dt_rsaintschm.Rows.Count - 1];
                AINT_APPLY_ON = !Convert.IsDBNull(dr["AINT_APPLY_ON"]) ? Convert.ToString(dr["AINT_APPLY_ON"]) : Convert.ToString("");
                AINT_CHECK_OD = !Convert.IsDBNull(dr["AINT_CHECK_OD"]) ? Convert.ToString(dr["AINT_CHECK_OD"]) : Convert.ToString("");
                AINT_ODPERIOD_TYPE = !Convert.IsDBNull(dr["AINT_ODPERIOD_TYPE"]) ? Convert.ToString(dr["AINT_ODPERIOD_TYPE"]) : Convert.ToString("");
                AINT_PERIOD_OD = !Convert.IsDBNull(dr["AINT_PERIOD_OD"]) ? Convert.ToDecimal(dr["AINT_PERIOD_OD"]) : Convert.ToDecimal("0.00");
            }
            if (IS_ADDINT != null && IS_ADDINT != "")
            {
                switch (AINT_APPLY_ON)
                {
                    case "P":
                        XAION = xlprbal;
                        break;
                    case "I":
                        XAION = XCALINT;
                        break;
                    case "B":
                        XAION = xlprbal + XCALINT;
                        break;
                    case "A":
                        XAION = XCALINT + xlaintbal;
                        break;
                }
                XAIPERIOD = 0;
                XAIOK = false;
                if (xlaintbal > 0)
                {
                    XAIOK = true;
                    XAIPERIOD = Convert.ToDateTime(LCAL_DATE).Subtract(xlast_trdt).Days;
                }
                else
                {
                    switch (AINT_CHECK_OD)
                    {
                        case "P":
                            switch (AINT_ODPERIOD_TYPE)
                            {
                                case "M":
                                    XPOD = Convert.ToInt32(AINT_PERIOD_OD);
                                    XINSTPAYB = Convert.ToInt32(Convert.ToDateTime(xlast_trdt).Subtract(Convert.ToDateTime(XLOAN_DATE.AddDays(1))).Days / (365.25 / 12) - XPOD);
                                    if (XINSTPAYB > 0)
                                    {
                                        XPAYB = XINSTPAYB * XINSTL_PRIN;
                                        XLESSPAID = XPAYB - (XLOAN_AMT - xlprbal);
                                        if (XLESSPAID > 0)
                                        {
                                            XAIOK = true;
                                            XAIPERIOD = Convert.ToDateTime(LCAL_DATE).Subtract(xlast_trdt).Days;
                                        }
                                    }
                                    if (XAIOK == false)
                                    {
                                        XINSTPAYB = Convert.ToInt32(Convert.ToDateTime(LCAL_DATE).Subtract(Convert.ToDateTime(XLOAN_DATE.AddDays(1))).Days / (365.25 / 12) - XPOD);
                                        if (XINSTPAYB > 0)
                                        {
                                            XPAYB = XINSTPAYB * XINSTL_PRIN;
                                            XLESSPAID = XPAYB - (XLOAN_AMT - xlprbal);
                                            if (XLESSPAID > 0)
                                            {
                                                XAIOK = true;
                                                decimal X_CLRUPTO = ((XLOAN_AMT - xlprbal) / XINSTL_PRIN) + 1;
                                                XCLRUPTO = Convert.ToDateTime(Convert.ToDateTime(LCAL_DATE).AddDays(Convert.ToInt32(X_CLRUPTO)));
                                                XAIPERIOD = Convert.ToDateTime(LCAL_DATE).Subtract(XCLRUPTO).Days;
                                            }
                                        }
                                    }
                                    break;
                                case "I":
                                    int XIPERIOD_AI = 0;
                                    DateTime XAINTDT = new DateTime();
                                    DateTime weff_date = new DateTime();
                                    DateTime XADDAINTDT = new DateTime();
                                    decimal XCALINT_AI = 0;
                                    decimal UPPER_LIMIT = 0;
                                    decimal INT_RATE = 0;
                                    decimal XADDAIPERIOD = 0;
                                    XCALINT_AI = 0;
                                    XPTYPE = AINT_ODPERIOD_TYPE;
                                    XPOD = Convert.ToInt32(AINT_CHECK_OD);
                                    XAINTDT = Convert.ToDateTime(Convert.ToDateTime(LCAL_DATE).AddDays(Convert.ToInt32(-XPOD)));
                                    XIPERIOD_AI = Convert.ToDateTime(LCAL_DATE).Subtract(XAINTDT).Days;
                                    if (FIXED_INTRT != 0)
                                    {
                                        XRATE = FIXED_INTRT - LESS_INT;
                                        XCALINT_AI = (xlprbal * XIPERIOD_AI * FIXED_INTRT) / 36500;
                                    }
                                    else
                                    {
                                        xfrdt = XAINTDT;
                                        XTODT = xfrdt;
                                        XCALUPTO = XAINTDT;
                                        if (dt_rsintrate.Rows.Count > 0)
                                        {
                                            foreach (DataRow dr in dt_rsintrate.Rows)
                                            {
                                                if (XTODT >= LCAL_DATE)
                                                {
                                                    break;
                                                }
                                                weff_date = !Convert.IsDBNull(dr["weff_date"]) ? Convert.ToDateTime(dr["weff_date"]) : Convert.ToDateTime(null);
                                                UPPER_LIMIT = !Convert.IsDBNull(dr["UPPER_LIMIT"]) ? Convert.ToDecimal(dr["UPPER_LIMIT"]) : Convert.ToDecimal("0.00");
                                                INT_RATE = !Convert.IsDBNull(dr["INT_RATE"]) ? Convert.ToDecimal(dr["INT_RATE"]) : Convert.ToDecimal("0.00");
                                                if (weff_date <= xfrdt)
                                                {
                                                    DataRow dr1 = (DataRow)dt_rsintrate.Rows[dt_rsintrate.Rows.Count - 1];
                                                    XEFF_DT = weff_date;
                                                }
                                                if (weff_date == XEFF_DT && (UPPER_LIMIT >= XLOAN_AMT))
                                                {
                                                    DataRow dr2 = (DataRow)dt_rsintrate.Rows[0];
                                                }
                                                XRATE = INT_RATE - LESS_INT;
                                                if (weff_date > xfrdt)
                                                {
                                                    DataRow dr3 = (DataRow)dt_rsintrate.Rows[0];
                                                    XTODT = LCAL_DATE;
                                                }
                                                else
                                                {
                                                    if (WEFF_DATE > LCAL_DATE)
                                                    {
                                                        XTODT = LCAL_DATE;
                                                    }
                                                    else
                                                    {
                                                        XTODT = Convert.ToDateTime(Convert.ToDateTime(WEFF_DATE).AddDays(-1));
                                                    }
                                                }
                                                XCALINT_AI = XCALINT_AI + ((xlprbal * (Convert.ToDateTime(XTODT).Subtract(XCALUPTO).Days) * XRATE) / 36500);
                                                xfrdt = XTODT.AddDays(1);
                                                XCALUPTO = XCALUPTO.AddDays(Convert.ToDateTime(XTODT).Subtract(XCALUPTO).Days);
                                            }
                                        }
                                    }
                                    if (XCALINT > XCALINT_AI)
                                    {
                                        XAIOK = true;
                                        XAIPERIOD = Convert.ToDateTime(LCAL_DATE).Subtract(xlast_trdt).Days;
                                        if (xlintbal > 0)
                                        {
                                            if (weff_date <= xlast_trdt)
                                            {
                                                DataRow dr1 = (DataRow)dt_rsintrate.Rows[dt_rsintrate.Rows.Count - 1];
                                                XEFF_DT = WEFF_DATE;
                                            }
                                            if (weff_date == XEFF_DT && (UPPER_LIMIT >= XLOAN_AMT))
                                            {
                                                DataRow dr2 = (DataRow)dt_rsintrate.Rows[0];
                                                XRATE = INT_RATE - LESS_INT;
                                            }
                                            if (xlprbal > 0)
                                            {
                                                XADDAIPERIOD = Math.Round((36500 * xlintbal) / (XRATE * xlprbal), 0);
                                            }
                                            else
                                            {
                                                XADDAIPERIOD = 0;
                                            }
                                            if (XPTYPE == "D")
                                            {
                                                XADDAINTDT = xlast_trdt.AddDays(-XPOD);
                                            }
                                            else if (XPTYPE == "D")
                                            {
                                                XADDAINTDT = xlast_trdt.AddMonths(-XPOD);
                                            }
                                            else if (XPTYPE == "Y")
                                            {
                                                XADDAINTDT = xlast_trdt.AddYears(-XPOD);
                                            }
                                            int add_days = Convert.ToInt32(xlast_trdt.AddDays(Convert.ToDouble(-XADDAIPERIOD)));
                                            if (add_days > Convert.ToInt32(XADDAINTDT))
                                            {
                                                XAIPERIOD = Convert.ToInt32(XAIPERIOD + XADDAIPERIOD);
                                            }
                                            XAIPERIOD = XAIPERIOD - 1;
                                            if (XAIPERIOD < 0)
                                            {
                                                XAIPERIOD = 0;
                                            }
                                        }
                                    }
                                    break;
                            }
                            break;
                    }
                }
                if (XAIOK == true)
                {
                    XCALAINT = 0;
                    xfrdt = LCAL_DATE.AddDays(-XAIPERIOD);
                    XTODT = xfrdt;
                    XCALUPTO = xfrdt;
                    if (dt_rsaintrate.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt_rsaintrate.Rows)
                        {
                            if (XTODT >= LCAL_DATE)
                            {
                                break;
                            }
                            DateTime weff_date = !Convert.IsDBNull(dr["weff_date"]) ? Convert.ToDateTime(dr["weff_date"]) : Convert.ToDateTime(null);
                            UPPER_LIMIT = !Convert.IsDBNull(dr["UPPER_LIMIT"]) ? Convert.ToDecimal(dr["UPPER_LIMIT"]) : Convert.ToDecimal("0.00");
                            decimal AINT_RATE = !Convert.IsDBNull(dr["AINT_RATE"]) ? Convert.ToDecimal(dr["AINT_RATE"]) : Convert.ToDecimal("0.00");
                            if (weff_date <= xfrdt)
                            {
                                DataRow dr1 = (DataRow)dt_rsintrate.Rows[dt_rsintrate.Rows.Count - 1];
                                XRATE = AINT_RATE;
                            }
                            if (weff_date > xfrdt)
                            {
                                DataRow dr2 = (DataRow)dt_rsintrate.Rows[0];
                                XTODT = LCAL_DATE;
                            }
                            else
                            {
                                XTODT = weff_date.AddDays(-1);
                            }
                            XCALAINT = XCALAINT + ((XAION * (Convert.ToDateTime(XTODT).Subtract(XCALUPTO).Days) * XRATE) / 36500);
                            xfrdt = XTODT.AddDays(1);
                            XCALUPTO = XCALUPTO.AddDays(Convert.ToDateTime(XTODT).Subtract(XCALUPTO).Days);
                        }
                    }
                }
                XCALAINT = XCALAINT + xlaintbal;
                //LN_RECV_ARRAY[3, 3] = Math.Round(XCALAINT, 0).ToString("0.00");
                arr_3_3 = Math.Round(XCALAINT, 0);
            }
            //LN_RECV_ARRAY[4, 1] = xlprbal.ToString("0.00");
            //LN_RECV_ARRAY[4, 2] = LN_RECV_ARRAY[3, 2];
            //LN_RECV_ARRAY[4, 3] = LN_RECV_ARRAY[3, 3];
            //LN_RECV_ARRAY[4, 4] = LN_RECV_ARRAY[3, 4];

            arr_4_1 = xlprbal.ToString("0.00");
            arr_4_2 = arr_3_2.ToString("0.00");
            arr_4_3 = arr_3_3.ToString("0.00");
            arr_4_4 = arr_3_4;
            lm.arr_1_1 = arr_1_1;
            lm.arr_3_2 = Convert.ToDecimal(arr_3_2).ToString("0.00");
            lm.arr_3_3 = Convert.ToDecimal(arr_3_3).ToString("0.00"); ;
            lm.arr_3_4 = arr_3_4;
            lm.arr_2_1 = arr_2_1;
            return lm;
        }
    }
}