using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Amritnagar.Includes;
using Amritnagar.Models.ViewModel;

namespace Amritnagar.Models.Database
{
    public class Loan_Ledger
    {
        SQLConfig config = new SQLConfig();
        public string branch_id { get; set; }
        public string ac_hd { get; set; }
        public string emp_id { get; set; }
        public DateTime vch_dt { get; set; }
        public string vch_no { get; set; }
        public decimal vch_srl { get; set; }
        public string vch_type { get; set; }
        public string vch_achd { get; set; }
        public string dr_cr { get; set; }
        public string chq_no { get; set; }
        public Nullable<DateTime> chq_dt { get; set; }
        public Nullable<DateTime> loan_dt { get; set; }
        public Nullable<DateTime> eff_date { get; set; }
        public DateTime XPDUPTO { get; set; }
        public DateTime open_dt { get; set; }
        public string bank_cd { get; set; }
        public decimal prin_amt { get; set; }
        public decimal int_amt { get; set; }
        public decimal aint_amt { get; set; }
        public decimal ichrg_amt { get; set; }
        public decimal prin_bal { get; set; }
        public decimal coop_prin_bal { get; set; }
        public decimal int_due { get; set; }
        public decimal aint_due { get; set; }
        public decimal ichrg_due { get; set; }
        public string ref_ac_hd { get; set; }
        public string ref_pacno { get; set; }
        public string ref_oth { get; set; }
        public string insert_mode { get; set; }
        public string created_by { get; set; }
        public DateTime created_on { get; set; }
        public string modified_by { get; set; }
        public DateTime modified_on { get; set; }
        public string approval_flag { get; set; }
        public string approved_by { get; set; }
        public string book_no { get; set; }
        public string employer_branch { get; set; }
        public string trns_particular { get; set; }
        public string loanee_name { get; set; }
        public string ledger_tab { get; set; }
        public string led_ac_hd { get; set; }
        public string ledger_col { get; set; }
        public decimal cr_amt { get; set; }
        public decimal dr_amt { get; set; }
        public decimal loan_amt { get; set; }
        public decimal int_rate { get; set; }
        public int inst_no { get; set; }
        public string int_scheme_cd { get; set; }
        public string aint_scheme_cd { get; set; }
        public string int_rate_cd { get; set; }
        public string aint_rate_cd { get; set; }
        public string int_category { get; set; }
        public string voucher_date { get; set; }
        public string is_lessint_spcl { get; set; }
        public string aint_scheme_name { get; set; }
        public string weff_date { get; set; }
        public decimal upper_limit { get; set; }
        public string comments { get; set; }
        public decimal aint_rate { get; set; }
        public string int_scheme_name { get; set; }
        public string int_scheme_desc { get; set; }
        public string xmast { get; set; }
        public decimal int_addl_tdl { get; set; }
        public decimal led_base_amt { get; set; }
        public int int_frq_mm { get; set; }
        public string int_product_flag { get; set; }
        public string int_comm_flag { get; set; }
        public int int_due_day { get; set; }
        public int int_rnd_flag { get; set; }
        public string int_rnd_stage { get; set; }
        public string aint_check_od { get; set; }
        public string aint_apply_on { get; set; }
        public string aint_odperiod_type { get; set; }
        public int aint_period_od { get; set; }
        public int aint_due_day { get; set; }
        public int aint_rnd_flag { get; set; }
        public int if_lti { get; set; }
        public string aint_rnd_stage { get; set; }
        public string aint_scheme_desc { get; set; }
        public string cus_id { get; set; }
        public string cus_name { get; set; }
        public string sex { get; set; }
        public string occp { get; set; }
        public string sign { get; set; }
        public string lti { get; set; }
        public string pan_no { get; set; }
        public decimal xirate { get; set; }
        public decimal xinstl { get; set; }
        public decimal xless_int { get; set; }
        public decimal tf_rate { get; set; }
        public decimal xamt { get; set; }
        public decimal int_bal { get; set; }
        public decimal tr_amt { get; set; }
        public decimal bal_amt { get; set; }
        public decimal due { get; set; }
        public decimal tf_buffer { get; set; }
        public string ln_spcl { get; set; }
        public string xpart { get; set; }
        public string ac_closed { get; set; }
        public string msg { get; set; }


        public string SaveLoanLedger(Loan_Ledger ld)
        {
            string sql = string.Empty;
            sql = "SELECT * FROM LOAN_LEDGER WHERE AC_HD='" + ld.ac_hd + "' AND EMPLOYEE_ID='" + ld.emp_id + "' AND convert(varchar, VCH_DATE, 103) = convert(varchar, '" + ld.vch_dt + "', 103) AND VCH_NO='" + ld.vch_no + "'ORDER BY VCH_DATE,VCH_NO,VCH_SRL";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                //DataRow dr = (DataRow)config.dt.Rows[config.dt.Rows.Count - 0];
                try
                {
                    config.Update("LOAN_LEDGER", new Dictionary<String, object>()
                        {
                        { "VCH_SRL",        ld.vch_srl},
                        { "VCH_TYPE",    ld.vch_type },
                        { "DR_CR",      ld.dr_cr },
                        { "PRIN_AMOUNT",       ld.prin_amt },
                        { "PRIN_BAL",       ld.prin_bal},
                        { "VCH_ACHD",       ld.vch_achd},
                        { "MODIFIED_BY",       ld.modified_by},
                        { "MODIFIED_ON",       DateTime.Now},
                        }, new Dictionary<string, object>()
                        {
                        { "BRANCH_ID",      ld.branch_id },
                        { "EMPLOYEE_ID",    ld.emp_id },
                        { "VCH_DATE",      ld.vch_dt },
                        { "AC_HD",         ld.ac_hd },
                        { "VCH_NO",        ld.vch_no },
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
                    config.Insert("LOAN_LEDGER", new Dictionary<String, object>()
                    {
                        { "BRANCH_ID",      ld.branch_id },
                        { "EMPLOYEE_ID",    ld.emp_id },
                        { "VCH_DATE",      ld.vch_dt },
                        { "VCH_NO",     ld.vch_no },
                        { "VCH_SRL",        ld.vch_srl},
                        { "VCH_TYPE",    ld.vch_type },
                        { "DR_CR",      ld.dr_cr },
                        { "PRIN_AMOUNT",       ld.prin_amt },
                        { "PRIN_BAL",       ld.prin_bal},
                        { "VCH_ACHD",       ld.vch_achd},
                        { "AC_HD",      ld.ac_hd },
                        { "CREATED_BY",      ld.created_by },
                        { "CREATED_ON",      DateTime.Now },
                    });
                }
                catch (Exception x)
                {

                }
            }
            string msg = "Saved Successfully";
            return (msg);
        }
        public void ResetPrinBalIntDueForLoanLedger_LTL_STL_FES(string branch, string achd, string empid)
        {
            Loan_Ledger ld = new Loan_Ledger();
            decimal lbal_prin = 0;
            decimal lbal_int = 0;
            int i = 1;
            string sql = "SELECT * FROM LOAN_LEDGER WHERE BRANCH_ID='" + branch + "' AND AC_HD='" + achd + "' AND EMPLOYEE_ID='" + empid + "' ORDER BY BRANCH_ID,VCH_DATE,EMPLOYEE_ID";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    if (i == 1)
                    {
                        lbal_prin = !Convert.IsDBNull(dr["prin_bal"]) ? Convert.ToDecimal(dr["prin_bal"]) : Convert.ToDecimal(00);
                        lbal_int = !Convert.IsDBNull(dr["INT_DUE"]) ? Convert.ToDecimal(dr["INT_DUE"]) : Convert.ToDecimal(00);
                    }
                    else
                    {
                        ld.dr_cr = Convert.ToString(dr["dr_cr"]);
                        if (ld.dr_cr == "D")
                        {
                            lbal_prin = lbal_prin + (!Convert.IsDBNull(dr["prin_amount"]) ? Convert.ToDecimal(dr["prin_amount"]) : Convert.ToDecimal(00));
                            lbal_int = lbal_int + (!Convert.IsDBNull(dr["INT_AMOUNT"]) ? Convert.ToDecimal(dr["INT_AMOUNT"]) : Convert.ToDecimal(00));
                        }
                        else
                        {
                            lbal_prin = lbal_prin - (!Convert.IsDBNull(dr["prin_amount"]) ? Convert.ToDecimal(dr["prin_amount"]) : Convert.ToDecimal(00));
                            lbal_int = lbal_int - (!Convert.IsDBNull(dr["INT_AMOUNT"]) ? Convert.ToDecimal(dr["INT_AMOUNT"]) : Convert.ToDecimal(00));
                        }
                        string VOUCHER_DATE = ((Convert.ToDateTime(dr["vch_date"])).ToString("dd/MM/yyyy")).Replace("-", "/");
                        ld.vch_no = Convert.ToString(dr["vch_no"]);
                        ld.vch_srl = Convert.ToInt32(dr["vch_srl"]);
                        string qry = "Update loan_ledger set prin_bal=" + lbal_prin + ",int_Due=" + lbal_int + " where convert(varchar, VCH_DATE, 103) = convert(varchar, '" + VOUCHER_DATE + "', 103) AND EMPLOYEE_ID='" + empid + "' and vch_no='" + ld.vch_no + "' and vch_srl=" + ld.vch_srl + "";
                        config.Execute_Query(qry);
                    }
                    i = i + 1;
                }
            }
        }
        public Loan_Ledger getdetailsbyVchNo(string branch_id, string ac_hd, string emp_id, string vch_date, string vch_no)
        {
            Loan_Ledger ld = new Loan_Ledger();
            string sql = "SELECT * FROM LOAN_LEDGER WHERE BRANCH_ID='" + branch_id + "' AND AC_HD='" + ac_hd + "' AND EMPLOYEE_ID='" + emp_id + "' AND convert(varchar, VCH_DATE, 103) = convert(varchar, '" + vch_date + "', 103) AND VCH_NO='" + vch_no + "'  ORDER BY VCH_DATE,VCH_NO,VCH_SRL,EMPLOYEE_ID";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    ld.prin_amt = Convert.ToDecimal(dr["PRIN_AMOUNT"]);
                }
            }
            return ld;
        }
        public List<Loan_Ledger> getmemberdetailsbymemid(string emp_id, string branch_id, string loan_type)
        {
            string sql = "SELECT * FROM LOAN_LEDGER WHERE BRANCH_ID='" + branch_id + "' AND AC_HD='" + loan_type + "' AND employee_ID='" + emp_id + "' order by vch_date,vch_no,vch_srl";
            string xtr_ty = "";
            decimal xdramt = 0;
            decimal xcramt = 0;
            config.singleResult(sql);
            List<Loan_Ledger> ldl = new List<Loan_Ledger>();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    xdramt = 0;
                    xcramt = 0;
                    Loan_Ledger ld = new Loan_Ledger();
                    ld.dr_cr = Convert.ToString(dr["DR_CR"]);
                    ld.vch_type = Convert.ToString(dr["VCH_TYPE"]);
                    ld.insert_mode = Convert.ToString(dr["INSERT_MODE"]);
                    ld.chq_no = Convert.ToString(dr["CHQ_NO"]);
                    ld.prin_amt = !Convert.IsDBNull(dr["PRIN_AMOUNT"]) ? Convert.ToDecimal(dr["PRIN_AMOUNT"]) : Convert.ToDecimal("0.00");
                    ld.int_amt = !Convert.IsDBNull(dr["INT_AMOUNT"]) ? Convert.ToDecimal(dr["INT_AMOUNT"]) : Convert.ToDecimal("0.00");
                    ld.aint_amt = !Convert.IsDBNull(dr["AINT_AMOUNT"]) ? Convert.ToDecimal(dr["AINT_AMOUNT"]) : Convert.ToDecimal("0.00");
                    ld.ichrg_amt = !Convert.IsDBNull(dr["ICHRG_AMOUNT"]) ? Convert.ToDecimal(dr["ICHRG_AMOUNT"]) : Convert.ToDecimal("0.00");
                    ld.ichrg_due = !Convert.IsDBNull(dr["ICHRG_DUE"]) ? Convert.ToDecimal(dr["ICHRG_DUE"]) : Convert.ToDecimal("0.00");
                    ld.prin_bal = !Convert.IsDBNull(dr["PRIN_BAL"]) ? Convert.ToDecimal(dr["PRIN_BAL"]) : Convert.ToDecimal("0.00");
                    ld.int_due = !Convert.IsDBNull(dr["INT_DUE"]) ? Convert.ToDecimal(dr["INT_DUE"]) : Convert.ToDecimal("0.00");
                    ld.aint_due = !Convert.IsDBNull(dr["AINT_DUE"]) ? Convert.ToDecimal(dr["AINT_DUE"]) : Convert.ToDecimal("0.00");
                    ld.ref_ac_hd = !Convert.IsDBNull(dr["REF_AC_HD"]) ? Convert.ToString(dr["REF_AC_HD"]) : Convert.ToString("");
                    ld.ref_pacno = !Convert.IsDBNull(dr["REF_PACNO"]) ? Convert.ToString(dr["REF_PACNO"]) : Convert.ToString("");
                    ld.vch_dt = !Convert.IsDBNull(dr["VCH_DATE"]) ? Convert.ToDateTime(dr["VCH_DATE"]) : Convert.ToDateTime("");
                    if (ld.dr_cr == "C")
                    {
                        xtr_ty = "By";
                        if (ld.prin_amt > 0)
                        {
                            xcramt = ld.prin_amt;
                        }
                        else if (ld.int_amt > 0)
                        {
                            xcramt = ld.int_amt;
                        }
                        else if (ld.aint_amt > 0)
                        {
                            xcramt = ld.aint_amt;
                        }
                        else if (ld.ichrg_amt > 0)
                        {
                            xcramt = ld.ichrg_amt;
                        }
                    }
                    if (ld.dr_cr == "D")
                    {
                        xtr_ty = "To ";
                        if (ld.prin_amt > 0)
                        {
                            xdramt = ld.prin_amt;
                        }
                        else if (ld.int_amt > 0)
                        {
                            xdramt = ld.int_amt;
                        }
                        else if (ld.aint_amt > 0)
                        {
                            xdramt = ld.aint_amt;
                        }
                        else if (ld.ichrg_amt > 0)
                        {
                            xdramt = ld.ichrg_amt;
                        }
                    }
                    if (ld.vch_type == "C")
                    {
                        xtr_ty = xtr_ty + "Cash";
                    }
                    if (ld.vch_type == "T")
                    {
                        xtr_ty = xtr_ty + "Transfer";
                    }
                    if (ld.vch_type == "B")
                    {
                        xtr_ty = xtr_ty + "Bank";
                    }
                    if (ld.vch_type == "J")
                    {
                        xtr_ty = xtr_ty + "Journal";
                    }
                    if (ld.insert_mode == "BF")
                    {
                        xtr_ty = xtr_ty + " (Balance From Ledger)";
                    }
                    if (ld.insert_mode == "BF")
                    {
                        xtr_ty = xtr_ty + " -Balance B/F";
                    }
                    if (ld.insert_mode == "LR")
                    {
                        xtr_ty = xtr_ty + "-Receipt";
                    }
                    if (ld.insert_mode == "II")
                    {
                        xtr_ty = "By Calculation";
                    }
                    if (ld.insert_mode == "LC")
                    {
                        xtr_ty = xtr_ty + "-Closure";
                    }
                    if (ld.insert_mode == "LI")
                    {
                        xtr_ty = xtr_ty + "-Schedule";
                    }
                    if (ld.insert_mode == "QI")
                    {
                        xtr_ty = xtr_ty + "-Qtrly Schedule";
                    }
                    if (ld.insert_mode == "SD")
                    {
                        xtr_ty = xtr_ty + " " + Convert.ToString(dr["REF_OTH"]);
                    }
                    if (ld.prin_amt > 0)
                    {
                        xtr_ty = xtr_ty + " @Principal";
                    }
                    if (ld.int_amt > 0)
                    {
                        xtr_ty = xtr_ty = xtr_ty + " @Interest";
                    }
                    if (ld.aint_amt > 0)
                    {
                        xtr_ty = xtr_ty + " @Addl.Interest";
                    }
                    if (ld.ichrg_amt > 0)
                    {
                        xtr_ty = xtr_ty + " @Misc.Charges";
                    }
                    if (ld.ref_ac_hd != "")
                    {
                        xtr_ty = xtr_ty + "(" + ld.ref_ac_hd;
                        if (ld.ref_pacno != "")
                        {
                            xtr_ty = xtr_ty + "/" + ld.ref_pacno;
                        }
                        xtr_ty = xtr_ty + ")";
                    }
                    ld.trns_particular = xtr_ty;
                    ld.cr_amt = xcramt;
                    ld.dr_amt = xdramt;
                    ldl.Add(ld);
                }
            }
            return ldl;
        }
        public List<Loan_Ledger> getalltransactiondetails(string branch_id, string ac_hd, string emp_id)
        {
            List<Loan_Ledger> ldl = new List<Loan_Ledger>();
            string sql = string.Empty;
            sql = "select * from loan_ledger where branch_id='" + branch_id + "' and ac_hd='" + ac_hd + "' and employee_id='" + emp_id + "' order by branch_id,ac_hd,employee_id,vch_date,vch_no";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    Loan_Ledger ld = new Loan_Ledger();
                    ld.emp_id = dr["EMPLOYEE_ID"].ToString();
                    ld.vch_dt = !Convert.IsDBNull(dr["VCH_DATE"]) ? Convert.ToDateTime(dr["VCH_DATE"]) : Convert.ToDateTime("");
                    //ld.voucher_date = !Convert.IsDBNull(dr["VCH_DATE"])?Convert.ToDateTime(dr["VCH_DATE"]).ToString("dd/MM/yyyy HH:mm:ss").Replace("/","-") : Convert.ToString("");
                    ld.vch_no = Convert.ToString(dr["vch_no"]);
                    ld.vch_srl = Convert.ToDecimal(dr["vch_srl"]);
                    ld.vch_type = Convert.ToString(dr["VCH_TYPE"]);
                    ld.dr_cr = Convert.ToString(dr["DR_CR"]);
                    ld.prin_bal = !Convert.IsDBNull(dr["PRIN_BAL"]) ? Convert.ToDecimal(dr["PRIN_BAL"]) : Convert.ToDecimal("0.00");
                    ld.prin_amt = !Convert.IsDBNull(dr["PRIN_AMOUNT"]) ? Convert.ToDecimal(dr["PRIN_AMOUNT"]) : Convert.ToDecimal("0.00");
                    ld.int_amt = !Convert.IsDBNull(dr["INT_AMOUNT"]) ? Convert.ToDecimal(dr["INT_AMOUNT"]) : Convert.ToDecimal("0.00");
                    ld.int_due = !Convert.IsDBNull(dr["INT_DUE"]) ? Convert.ToDecimal(dr["INT_DUE"]) : Convert.ToDecimal("0.00");
                    ld.chq_no = !Convert.IsDBNull(dr["CHQ_NO"]) ? Convert.ToString(dr["CHQ_NO"]) : Convert.ToString("");
                    ld.chq_dt = !Convert.IsDBNull(dr["CHQ_DT"]) ? Convert.ToDateTime(dr["CHQ_DT"]) : Convert.ToDateTime(null);
                    ld.bank_cd = !Convert.IsDBNull(dr["BANKCD"]) ? Convert.ToString(dr["BANKCD"]) : Convert.ToString("");
                    ldl.Add(ld);
                }
            }
            return ldl;
        }
        public string checkandsaveloanledger(Loan_Ledger ld)
        {
            string dt = ld.vch_dt.ToString("dd/MM/yyyy");
            string tm = ld.vch_dt.ToString("HH:mm:ss");
            //string sql = string.Empty;
            string sql = "select * from loan_ledger where branch_id='" + branch_id + "' and ac_hd='" + ac_hd + "' and employee_id='" + emp_id + "' AND convert(varchar, VCH_DATE, 103) = convert(varchar, '" + dt + "', 103) AND convert(varchar, VCH_DATE, 108) = convert(varchar, '" + tm + "', 108) AND VCH_NO='" + ld.vch_no + "' AND VCH_SRL='" + ld.vch_srl + "'";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {                
                try
                {
                    string qry = "";
                    if (ld.chq_dt == null)
                    {
                        qry = "Update loan_ledger set VCH_TYPE='" + ld.vch_type + "',CHQ_NO='" + ld.chq_no +
                      "',BANKCD='" + ld.bank_cd + "',INT_DUE=" + ld.int_due + ",INT_AMOUNT=" + ld.int_amt + "," +
                      "DR_CR='" + ld.dr_cr + "',PRIN_AMOUNT=" + ld.prin_amt + ",PRIN_BAL=" + ld.prin_bal + " , Modified_Date = "+ DateTime.Now + ", MODIFIED_BY = '"+ ld.modified_by + "'" +
                      " where convert(varchar, VCH_DATE, 103) = convert(varchar, '" + dt.Replace("-", "/") + "', 103) AND convert(varchar, VCH_DATE, 108) = convert(varchar, '" + tm + "', 108) AND EMPLOYEE_ID='" + ld.emp_id + "' " +
                      "and BRANCH_ID='" + ld.branch_id + "' and AC_HD='" + ld.ac_hd + "' and VCH_SRL=" + ld.vch_srl + "";
                    }
                    else
                    {
                        qry = "Update loan_ledger set VCH_TYPE='" + ld.vch_type + "',CHQ_NO='" + ld.chq_no + "',CHQ_DT='" + ld.chq_dt + "'" +
                      ",BANKCD='" + ld.bank_cd + "',INT_DUE=" + ld.int_due + ",INT_AMOUNT=" + ld.int_amt + "," +
                      "DR_CR='" + ld.dr_cr + "',PRIN_AMOUNT=" + ld.prin_amt + ",PRIN_BAL=" + ld.prin_bal + ", Modified_Date = " + DateTime.Now + ", MODIFIED_BY = '" + ld.modified_by + "'" +
                      " where convert(varchar, VCH_DATE, 103) = convert(varchar, '" + dt/*.Replace("-", "/")*/ + "', 103) AND convert(varchar, VCH_DATE, 108) = convert(varchar, '" + tm + "', 108) AND EMPLOYEE_ID='" + ld.emp_id + "' " +
                      "and BRANCH_ID='" + ld.branch_id + "' and AC_HD='" + ld.ac_hd + "' and VCH_SRL=" + ld.vch_srl + "";
                    }
                    config.Execute_Query(qry);

                    //config.Update("LOAN_LEDGER", new Dictionary<String, object>()
                    //    {
                    //    { "VCH_TYPE",    ld.vch_type },
                    //    { "DR_CR",      ld.dr_cr },
                    //    { "PRIN_AMOUNT",       ld.prin_amt },
                    //    { "PRIN_BAL",       ld.prin_bal},
                    //    { "INT_AMOUNT",       ld.int_amt},
                    //    { "INT_DUE",       ld.int_due},
                    //    { "BANKCD",       ld.bank_cd},
                    //    { "CHQ_NO",       ld.chq_no},
                    //    { "CHQ_DT",       ld.chq_dt},
                    //    }, new Dictionary<string, object>()
                    //    {
                    //    { "BRANCH_ID",      ld.branch_id },
                    //    { "EMPLOYEE_ID",    ld.emp_id },
                    //    { "VCH_DATE",      ld.vch_dt },
                    //    { "AC_HD",         ld.ac_hd },
                    //    { "VCH_NO",        ld.vch_no },
                    //    { "VCH_SRL",        ld.vch_srl},
                    //    });
                    
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                try
                {
                    config.Insert("LOAN_LEDGER", new Dictionary<String, object>()
                    {
                        { "BRANCH_ID",      ld.branch_id },
                        { "EMPLOYEE_ID",    ld.emp_id },
                        { "VCH_DATE",      ld.vch_dt },
                        { "AC_HD",         ld.ac_hd },
                        { "VCH_NO",        ld.vch_no },
                        { "VCH_SRL",        ld.vch_srl},
                        { "VCH_TYPE",    ld.vch_type },
                        { "DR_CR",      ld.dr_cr },
                        { "PRIN_AMOUNT",       ld.prin_amt },
                        { "PRIN_BAL",       ld.prin_bal},
                        { "PINT_AMOUNT",       ld.int_amt},
                        { "INT_DUE",       ld.int_due},
                        { "BANKCD",       ld.bank_cd},
                        { "CHQ_NO",       ld.chq_no},
                        { "CHQ_DT",       ld.chq_dt},
                        { "CREATED_BY",   ld.created_by },
                        { "CREATED_ON",   DateTime.Now },
                    });                   
                }
                catch (Exception x)
                {

                }
            }
            string msg = "Saved Successfully";
            return (msg);
        }
        public void DeleteRecord(LoanLedgerCorrectionViewModel model)
        {
            Loan_Ledger ld = new Loan_Ledger();
            string dt = Convert.ToDateTime(model.date).ToString("dd/MM/yyyy");
            string tm = Convert.ToDateTime(model.date).ToString("HH:mm:ss");
            string sql = "Delete from loan_ledger where branch_id='" + model.branch_id + "' and ac_hd='" + model.ac_hd + "' and employee_id='" + model.emp_id + "'AND convert(varchar, VCH_DATE, 103) = convert(varchar, '" + dt + "', 103) AND convert(varchar, VCH_DATE, 108) = convert(varchar, '" + tm + "', 108) AND VCH_NO='" + model.vch_no + "' AND VCH_SRL='" + model.vch_srl + "'";
            config.Execute_Query(sql);
        }
        public List<Loan_Ledger> gepesonalandledgerdetails(string branch, string acc_no, string achd, string op_dt, string date)
        {
            List<Loan_Ledger> ldl = new List<Loan_Ledger>();
            Loan_Ledger ld = new Loan_Ledger();
            string sql = string.Empty;
            string xpart = "";
            sql = "Select * from  ACC_HEAD  order by AC_HD";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    ld.ac_hd = Convert.ToString(dr["AC_HD"]);
                    if(ld.ac_hd== "SL6")
                    {

                    }
                    ld.ledger_tab = Convert.ToString(dr["LEDGER_TAB"]);
                    ld.ledger_col = Convert.ToString(dr["LEDGER_COL"]);
                    ld.led_ac_hd = Convert.ToString(dr["LED_ACHD"]);
                    ld.xmast = Convert.ToString(dr["AC_LF_MAST_FL"]);
                    ld.led_base_amt = !Convert.IsDBNull(dr["MISCDEP_BASEAMT"]) ? Convert.ToDecimal(dr["MISCDEP_BASEAMT"]) : Convert.ToDecimal("0.00");
                    if(ld.xmast == "D")
                    {
                        sql = "SELECT * FROM DEPOSIT_MAST WHERE BRANCH_ID='" + branch + "'AND ";
                        sql = sql + "AC_HD='" + ld.ac_hd + "' AND AC_NO='" + acc_no + "'";
                        config.singleResult(sql);
                        if(config.dt.Rows.Count > 0)
                        { 
                            foreach(DataRow dr1 in config.dt.Rows)
                            {
                                ld.cus_name = Convert.ToString(dr1["ac_name"]);
                                ld.ac_closed = Convert.ToString(dr1["ac_closed"]);
                                ld.open_dt = !Convert.IsDBNull(dr1["open_date"]) ? Convert.ToDateTime(dr1["open_date"]) : Convert.ToDateTime(null);
                            }
                        }
                        sql = "SELECT A.CUST_ID,A.CUST_NAME,a.sex,a.occup_id,A.DESIGNATION,B.SIGN_FLAG FROM CUSTOMER A,DEPOSIT_CUSTOMER B ";
                        sql = sql + "WHERE (A.BRANCH_ID=B.BRANCH_ID AND A.CUST_ID=B.CUST_ID) AND ";
                        sql = sql + "(B.BRANCH_ID='" + branch + "' AND B.AC_HD='" + ld.led_ac_hd + "' AND ";
                        sql = sql + "B.AC_NO='" + acc_no + "')";
                        config.singleResult(sql);
                        if(config.dt.Rows.Count > 0)
                        {
                            foreach(DataRow dr2 in config.dt.Rows)
                            {
                                ld.cus_id = Convert.ToString(dr2["cust_id"]);
                                ld.cus_name = Convert.ToString(dr2["cust_name"]);
                                ld.sex = Convert.ToString(dr2["SEX"]);
                                ld.occp = Convert.ToString(dr2["OCCUP_ID"]);
                                ld.sign = Convert.ToString(dr2["SIGN_FLAG"]);
                            }
                        }
                        if(ld.ledger_tab != "")
                        {
                            Ledger l = new Ledger();
                            l = l.GET_LEDGER_QRY(ld.led_ac_hd, acc_no, ld.ledger_tab, branch);
                            string QRYLED = l.query;
                            config.singleResult(QRYLED);
                            if(config.dt.Rows.Count > 0)
                            {
                                DataRow dr3 = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                                ld.vch_dt = !Convert.IsDBNull(dr3["vch_date"]) ? Convert.ToDateTime(dr3["vch_date"]) : Convert.ToDateTime(null);
                                ld.int_amt = !Convert.IsDBNull(dr3["INT_AMOUNT"]) ? Convert.ToDecimal(dr3["INT_AMOUNT"]) : Convert.ToDecimal("0.00");
                                ld.prin_amt = !Convert.IsDBNull(dr3["PRIN_AMOUNT"]) ? Convert.ToDecimal(dr3["PRIN_AMOUNT"]) : Convert.ToDecimal("0.00");
                                ld.prin_bal = !Convert.IsDBNull(dr3["prin_bal"]) ? Convert.ToDecimal(dr3["prin_bal"]) : Convert.ToDecimal("0.00");
                                ld.int_bal = !Convert.IsDBNull(dr3["INT_BAL"]) ? Convert.ToDecimal(dr3["INT_BAL"]) : Convert.ToDecimal("0.00");
                                ld.dr_cr = Convert.ToString(dr3["DR_CR"]);
                                ld.vch_type = Convert.ToString(dr3["VCH_TYPE"]);
                                if(ld.dr_cr == "D")
                                {
                                    xpart = "To ";
                                }
                                else
                                {
                                    xpart = "By ";
                                }
                                if(ld.vch_type == "C")
                                {
                                    xpart = xpart + "Cash ";
                                }
                                if (ld.vch_type == "B")
                                {
                                    xpart = xpart + "Bank ";
                                }
                                if (ld.vch_type == "T")
                                {
                                    xpart = xpart + "Transfer ";
                                }
                                if (ld.vch_type == "J")
                                {
                                    xpart = xpart + "Journal ";
                                }
                                if(ld.int_amt > 0)
                                {
                                    xpart = xpart + "@Interest";
                                }
                                ld.xamt = ld.prin_amt + ld.int_amt;                               
                            }
                        }
                        if(ld.ac_closed == "C")
                        {
                            ld.msg = "A/C is Closed";
                        }
                        if (ld.ac_closed == "T")
                        {
                            ld.msg = "A/C is Subject to Closure";
                        }
                    }
                    if(ld.xmast == "L")
                    {
                        sql = "SELECT * FROM LOAN_MASTER WHERE BRANCH_ID='" +branch+ "' AND ";
                        sql = sql + "AC_HD='" + ld.ac_hd + "' AND employee_ID='" + acc_no + "'";
                        config.singleResult(sql);
                        if (config.dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr1 in config.dt.Rows)
                            {
                                ld.cus_name = Convert.ToString(dr1["loanee_name"]);
                                ld.open_dt = !Convert.IsDBNull(dr1["loan_date"]) ? Convert.ToDateTime(dr1["loan_date"]) : Convert.ToDateTime(null);
                            }
                        }
                    }
                    if(ld.xmast == "M")
                    {
                        sql = "SELECT * FROM MEMBER_MAST WHERE BRANCH_ID='" +branch+ "' AND ";
                        sql = sql + "MEMBER_ID='" + acc_no + "'";
                        config.singleResult(sql);
                        if (config.dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr1 in config.dt.Rows)
                            {
                                ld.cus_name = Convert.ToString(dr1["member_name"]);
                                ld.ac_closed = Convert.ToString(dr1["MEMBER_CLOSED"]);
                                ld.open_dt = !Convert.IsDBNull(dr1["MEMBER_DATE"]) ? Convert.ToDateTime(dr1["MEMBER_DATE"]) : Convert.ToDateTime(null);
                                ld.cus_id = Convert.ToString(dr1["EMPLOYEE_ID"]);
                                ld.cus_name = Convert.ToString(dr1["member_name"]);
                                ld.sex = Convert.ToString(dr1["SEX"]);
                                ld.occp = Convert.ToString(dr1["OCCUP_ID"]);
                                ld.if_lti = !Convert.IsDBNull(dr1["IF_LTI"]) ? Convert.ToInt32(dr1["IF_LTI"]) : Convert.ToInt32("0.00");
                                ld.pan_no = !Convert.IsDBNull(dr1["PAN_NO"]) ? Convert.ToString(dr1["PAN_NO"]) : Convert.ToString("");
                                ld.tf_buffer = !Convert.IsDBNull(dr1["TF_BUFFER"]) ? Convert.ToDecimal(dr1["TF_BUFFER"]) : Convert.ToDecimal("0.00");
                            }
                        }                
                        if (ld.ledger_tab != "")
                        {
                            Ledger l = new Ledger();
                            l = l.GET_LEDGER_QRY(ld.led_ac_hd, acc_no, ld.ledger_tab, branch);
                            string QRYLED = l.query;
                            config.singleResult(QRYLED);
                            if (config.dt.Rows.Count > 0)
                            {
                                DataRow dr3 = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                                ld.vch_dt = !Convert.IsDBNull(dr3["vch_date"]) ? Convert.ToDateTime(dr3["vch_date"]) : Convert.ToDateTime(null);                                                                            
                                ld.dr_cr = Convert.ToString(dr3["DR_CR"]);
                                ld.vch_type = Convert.ToString(dr3["VCH_TYPE"]);
                                if (ld.dr_cr == "D")
                                {
                                    xpart = "To ";
                                }
                                else
                                {
                                    xpart = "By ";
                                }
                                if (ld.vch_type == "C")
                                {
                                    xpart = xpart + "Cash ";
                                }
                                if (ld.vch_type == "B")
                                {
                                    xpart = xpart + "Bank ";
                                }
                                if (ld.vch_type == "T")
                                {
                                    xpart = xpart + "Transfer ";
                                }
                                if (ld.vch_type == "J")
                                {
                                    xpart = xpart + "Journal ";
                                }
                                if(ld.ledger_tab == "GF_LEDGER" || ld.ledger_tab == "TF_LEDGER")
                                {
                                    ld.int_amt = !Convert.IsDBNull(dr3["INT_AMOUNT"]) ? Convert.ToDecimal(dr3["INT_AMOUNT"]) : Convert.ToDecimal("0.00");
                                    ld.int_amt = !Convert.IsDBNull(dr3["INT_AMOUNT"]) ? Convert.ToDecimal(dr3["INT_AMOUNT"]) : Convert.ToDecimal("0.00");
                                    ld.prin_amt = !Convert.IsDBNull(dr3["PRIN_AMOUNT"]) ? Convert.ToDecimal(dr3["PRIN_AMOUNT"]) : Convert.ToDecimal("0.00");
                                    ld.prin_bal = !Convert.IsDBNull(dr3["prin_bal"]) ? Convert.ToDecimal(dr3["prin_bal"]) : Convert.ToDecimal("0.00");
                                    ld.int_bal = !Convert.IsDBNull(dr3["INT_BAL"]) ? Convert.ToDecimal(dr3["INT_BAL"]) : Convert.ToDecimal("0.00");
                                    if (ld.int_amt > 0)
                                    {
                                        xpart = xpart + "@Interest";
                                    }
                                    ld.xamt = ld.prin_amt + ld.int_amt;
                                }                               
                                else if (ld.ledger_tab == "SHARE_LEDGER" || ld.ledger_tab == "DIVIDEND_LEDGER")
                                {
                                    ld.xamt = !Convert.IsDBNull(dr3["TR_AMOUNT"]) ? Convert.ToDecimal(dr3["TR_AMOUNT"]) : Convert.ToDecimal("0.00");
                                    ld.prin_bal = !Convert.IsDBNull(dr3["BAL_AMOUNT"]) ? Convert.ToDecimal(dr3["BAL_AMOUNT"]) : Convert.ToDecimal("0.00");                                    
                                }
                            }
                        }
                        if(ld.ac_closed == "C")
                        {
                            ld.msg = "A/C is Closed";
                        }
                        if(ld.ac_closed == "T")
                        {
                            ld.msg = "Membership is Subject to Closure";
                        }
                    }
                    if(ld.xmast == "C")
                    {

                    }                   
                    //ld = PROCESS_DUE(prin_amt, Convert.ToDateTime(op_dt), Convert.ToDateTime(date), ld.ledger_tab, ld.tf_buffer);                                  
                    ld.xpart = xpart;                    
                }
            }
            ldl.Add(ld);
            return ldl;
        }
        public Loan_Ledger PROCESS_DUE(decimal XPAID, DateTime XFROM, DateTime XTO, string ledger_tab, decimal tf_buffer)
        {
            Loan_Ledger ld = new Loan_Ledger();
            decimal XBUFFER = 0;           
            decimal XAMT = 0;
            decimal XPAYBL = 0;
            decimal XNETPAYBL = 0;
            decimal XRATE = 0;
            decimal XDUE = 0;
            int XPRD = 0;
            DateTime xfrdt = new DateTime();
            DateTime XTODT = new DateTime();
            DateTime XPDUPTO = new DateTime();
            if (ledger_tab == "TF_LEDGER")
            {
                XBUFFER = tf_buffer;
                string sql = "SELECT * FROM TF_RATE ORDER BY EFF_DATE";
                config.singleResult(sql);
                if(config.dt.Rows.Count > 0)
                {
                    foreach(DataRow dr in config.dt.Rows)
                    {                       
                        ld.eff_date = !Convert.IsDBNull(dr["EFF_DATE"]) ? Convert.ToDateTime(dr["EFF_DATE"]) : Convert.ToDateTime(null);
                        ld.tf_rate = !Convert.IsDBNull(dr["TF_RATE"]) ? Convert.ToDecimal(dr["TF_RATE"]) : Convert.ToDecimal("0.00");
                        XPAID = XPAID + XBUFFER;
                        XAMT = XPAID;
                        xfrdt = XFROM;
                        XPAYBL = 0;
                        XNETPAYBL = 0;
                        XTODT = XFROM;
                        XPDUPTO = xfrdt;
                        if(XTODT >= XTO)
                        {
                            break;
                        }
                        if (ld.eff_date <= xfrdt)
                        {
                            DataRow dr1 = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                            ld.eff_date = !Convert.IsDBNull(dr1["EFF_DATE"]) ? Convert.ToDateTime(dr1["EFF_DATE"]) : Convert.ToDateTime(null);
                            if (ld.eff_date > xfrdt)
                            {
                                DataRow dr2 = (DataRow)config.dt.Rows[0];
                                ld.eff_date = !Convert.IsDBNull(dr2["EFF_DATE"]) ? Convert.ToDateTime(dr2["EFF_DATE"]) : Convert.ToDateTime(null);
                                xfrdt = Convert.ToDateTime(ld.eff_date);
                            }
                            XRATE = ld.tf_rate;
                            if (ld.eff_date > xfrdt)
                            {
                                DataRow dr3 = (DataRow)config.dt.Rows[0];
                                XTODT = XTO;
                            }
                            else
                            {
                                if(ld.eff_date > XTO)
                                {
                                    XTODT = XTO;
                                }
                                else
                                {
                                    XTODT= Convert.ToDateTime(ld.eff_date).AddDays(-1);
                                }
                            }
                            XPRD = Convert.ToInt32(XTODT.Subtract(xfrdt).Days / (365.25 / 12)) + 1;
                            XPAYBL = (XPRD * XRATE);
                            decimal cal = (XAMT / XRATE) - 1;
                            if (XAMT >= XRATE)
                            {
                                XPDUPTO =  Convert.ToDateTime(xfrdt.Subtract(Convert.ToDateTime(cal)).Days / (365.25 / 12));
                                XAMT = XAMT - XPAYBL;
                            }
                            XNETPAYBL = XNETPAYBL + XPAYBL;
                            xfrdt = Convert.ToDateTime(XTODT).AddDays(1);
                        }
                    }
                    XDUE = XNETPAYBL - XPAID;
                    if(XDUE > 0)
                    {
                        ld.due = XDUE;
                    }
                    if(XPDUPTO > XFROM)
                    {
                        ld.XPDUPTO = XPDUPTO;
                    }
                }
            }
            return ld;
        }

        //public List<Loan_Ledger> getallexistingloandetails(string branch_id, string ac_hd, string on_date)
        //{
        //    List<Loan_Ledger> ldl = new List<Loan_Ledger>();
        //    string sql = string.Empty;
        //    sql = "select * from loan_master where branch_id='" + branch_id + "' and ac_hd = '" + ac_hd + "' and convert(varchar, loan_date, 103) <= convert(varchar, '" + on_date + "', 103) order by loan_date,EMPLOYEE_ID";
        //    sql = "select * from loan_master where branch_id='" + branch_id + "' and ";
        //    sql = sql + "ac_hd='" + ac_hd + "' and convert(varchar, loan_date, 103) <= convert(varchar, '" + on_date + "', 103) and ";
        //    sql = sql + "IIF(clos_date is NULL,convert(datetime, '31/03/2099', 103),convert(datetime, clos_date, 103)) >= convert(datetime, '" + on_date + "', 103) order by loan_date,EMPLOYEE_ID";
        //    config.singleResult(sql);
        //    if (config.dt.Rows.Count > 0)
        //    {
        //        foreach (DataRow dr in config.dt.Rows)
        //        {
        //            Loan_Ledger ld = new Loan_Ledger();
        //            ld.emp_id = dr["EMPLOYEE_ID"].ToString();
        //            ld.loanee_name = !Convert.IsDBNull(dr["LOANEE_NAME"]) ? Convert.ToString(dr["LOANEE_NAME"]) : Convert.ToString("");
        //            ld.loan_dt = !Convert.IsDBNull(dr["LOAN_DATE"]) ? Convert.ToDateTime(dr["LOAN_DATE"]) : Convert.ToDateTime(null);
        //            ld.loan_amt = !Convert.IsDBNull(dr["LOAN_AMT"]) ? Convert.ToDecimal(dr["LOAN_AMT"]) : Convert.ToDecimal(00);
        //            ld.int_rate = !Convert.IsDBNull(dr["INT_RATE"]) ? Convert.ToDecimal(dr["INT_RATE"]) : Convert.ToDecimal(00);
        //            ld.inst_no = !Convert.IsDBNull(dr["NO_INSTL"]) ? Convert.ToInt32(dr["NO_INSTL"]) : Convert.ToInt32(00);
        //            ld.ln_spcl = !Convert.IsDBNull(dr["LN_SPEACIAL"]) ? Convert.ToString(dr["LN_SPEACIAL"]) : Convert.ToString("");
        //            sql = "select * from loan_ledger where branch_id='" + branch_id + "' and ac_hd='" + ac_hd + "' and EMPLOYEE_id='" + ld.emp_id + "' and convert(varchar, vch_date, 103) <= convert(varchar, '" + on_date + "', 103) order by vch_date,vch_no,vch_srl";
        //            config.singleResult(sql);
        //            if (config.dt.Rows.Count > 0)
        //            {
        //                foreach (DataRow dr1 in config.dt.Rows)
        //                {
        //                    ld.vch_dt = !Convert.IsDBNull(dr1["VCH_DATE"]) ? Convert.ToDateTime(dr1["VCH_DATE"]) : Convert.ToDateTime("");
        //                }
        //            }
        //            ldl.Add(ld);
        //        }
        //    }
        //    return ldl;
        //}

        //public Loan_Ledger get_current_due(string branch_id, string ac_hd, string on_date, string empid, decimal loan_amt, decimal insl_no, decimal int_rate, string ln_special)
        //{
        //    List<Loan_Ledger> ldl = new List<Loan_Ledger>();
        //    Loan_Ledger ld = new Loan_Ledger();
        //    string sql = string.Empty;
        //    sql = "select * from loan_ledger where branch_id='" + branch_id + "' and ac_hd='" + ac_hd + "' and EMPLOYEE_id='" + empid + "' and convert(varchar, vch_date, 103) <= convert(varchar, '" + on_date + "', 103) order by vch_date,vch_no,vch_srl";
        //    config.singleResult(sql);
        //    DataTable loanledgerdattab = config.dt;
        //    if (config.dt.Rows.Count > 0)
        //    {
        //        Loan_Ledger ld = new Loan_Ledger();
        //        sql = "select * from lntype_mast where ac_hd='" + ac_hd + "'";
        //        config.singleResult(sql);
        //        if (config.dt.Rows.Count > 0)
        //        {
        //            foreach (DataRow dr in config.dt.Rows)
        //            {
        //                ld.int_scheme_cd = !Convert.IsDBNull(dr["INT_SCHEME_CD"]) ? Convert.ToString(dr["INT_SCHEME_CD"]) : Convert.ToString("");
        //                ld.aint_scheme_cd = !Convert.IsDBNull(dr["AINT_SCHEME_CD"]) ? Convert.ToString(dr["AINT_SCHEME_CD"]) : Convert.ToString("");
        //                ld.int_rate_cd = !Convert.IsDBNull(dr["INT_RATE_CD"]) ? Convert.ToString(dr["INT_RATE_CD"]) : Convert.ToString("");
        //                ld.aint_rate_cd = !Convert.IsDBNull(dr["AINT_RATE_CD"]) ? Convert.ToString(dr["AINT_RATE_CD"]) : Convert.ToString("");
        //                ld.is_lessint_spcl = !Convert.IsDBNull(dr["IS_LESSINT_SPCL"]) ? Convert.ToString(dr["IS_LESSINT_SPCL"]) : Convert.ToString("");
        //            }
        //        }
        //        sql = "SELECT * FROM LNSCHEME_INT WHERE INT_SCHEME_CD='" + ld.int_scheme_cd + "'";
        //        config.singleResult(sql);
        //        DataTable loanschemeint = config.dt;
        //        if (config.dt.Rows.Count > 0)
        //        {
        //            foreach (DataRow dr1 in config.dt.Rows)
        //            {
        //                ld.int_category = !Convert.IsDBNull(dr1["INT_CATEGORY"]) ? Convert.ToString(dr1["INT_CATEGORY"]) : Convert.ToString("");
        //                ld.int_scheme_cd = !Convert.IsDBNull(dr1["int_scheme_cd"]) ? Convert.ToString(dr1["int_scheme_cd"]) : Convert.ToString("");
        //                ld.int_scheme_name = !Convert.IsDBNull(dr1["int_scheme_name"]) ? Convert.ToString(dr1["int_scheme_name"]) : Convert.ToString("");
        //                ld.int_scheme_desc = !Convert.IsDBNull(dr1["int_scheme_desc"]) ? Convert.ToString(dr1["int_scheme_desc"]) : Convert.ToString("");
        //                ld.int_addl_tdl = !Convert.IsDBNull(dr1["int_addl_tdl"]) ? Convert.ToDecimal(dr1["int_addl_tdl"]) : Convert.ToDecimal("00");
        //                ld.int_frq_mm = !Convert.IsDBNull(dr1["int_addl_tdl"]) ? Convert.ToInt32(dr1["int_addl_tdl"]) : Convert.ToInt32("00");
        //                ld.int_product_flag = !Convert.IsDBNull(dr1["int_product_flag"]) ? Convert.ToString(dr1["int_product_flag"]) : Convert.ToString("");
        //                ld.int_comm_flag = !Convert.IsDBNull(dr1["int_comm_flag"]) ? Convert.ToString(dr1["int_comm_flag"]) : Convert.ToString("");
        //                ld.int_due_day = !Convert.IsDBNull(dr1["int_due_day"]) ? Convert.ToInt32(dr1["int_due_day"]) : Convert.ToInt32("00");
        //                ld.int_rnd_flag = !Convert.IsDBNull(dr1["int_rnd_flag"]) ? Convert.ToInt32(dr1["int_rnd_flag"]) : Convert.ToInt32("00");
        //                ld.int_rnd_stage = !Convert.IsDBNull(dr1["int_rnd_stage"]) ? Convert.ToString(dr1["int_rnd_stage"]) : Convert.ToString("");
        //            }
        //        }
        //        sql = "SELECT * FROM LNSCHEME_AINT WHERE AINT_SCHEME_CD='" + ld.aint_scheme_cd + "'";
        //        config.singleResult(sql);
        //        DataTable loanschemeaint = config.dt;
        //        if (config.dt.Rows.Count > 0)
        //        {
        //            foreach (DataRow dr2 in config.dt.Rows)
        //            {
        //                ld.aint_scheme_name = !Convert.IsDBNull(dr2["AINT_SCHEME_NAME"]) ? Convert.ToString(dr2["AINT_SCHEME_NAME"]) : Convert.ToString("");
        //                ld.aint_scheme_cd = !Convert.IsDBNull(dr2["AINT_SCHEME_CD"]) ? Convert.ToString(dr2["AINT_SCHEME_CD"]) : Convert.ToString("");
        //                ld.aint_scheme_desc = !Convert.IsDBNull(dr2["aint_scheme_desc"]) ? Convert.ToString(dr2["aint_scheme_desc"]) : Convert.ToString("");
        //                ld.aint_check_od = !Convert.IsDBNull(dr2["aint_check_od"]) ? Convert.ToString(dr2["aint_check_od"]) : Convert.ToString("");
        //                ld.aint_apply_on = !Convert.IsDBNull(dr2["aint_apply_on"]) ? Convert.ToString(dr2["aint_apply_on"]) : Convert.ToString("");
        //                ld.aint_odperiod_type = !Convert.IsDBNull(dr2["aint_odperiod_type"]) ? Convert.ToString(dr2["aint_odperiod_type"]) : Convert.ToString("");
        //                ld.aint_period_od = !Convert.IsDBNull(dr2["aint_period_od"]) ? Convert.ToInt32(dr2["aint_period_od"]) : Convert.ToInt32("00");
        //                ld.aint_due_day = !Convert.IsDBNull(dr2["aint_due_day"]) ? Convert.ToInt32(dr2["aint_due_day"]) : Convert.ToInt32("00");
        //                ld.aint_rnd_flag = !Convert.IsDBNull(dr2["aint_rnd_flag"]) ? Convert.ToInt32(dr2["aint_rnd_flag"]) : Convert.ToInt32("00");
        //                ld.aint_rnd_stage = !Convert.IsDBNull(dr2["aint_rnd_stage"]) ? Convert.ToString(dr2["aint_rnd_stage"]) : Convert.ToString("");
        //            }
        //        }
        //        sql = "SELECT * FROM LNINTRATE_DTL WHERE INT_RATE_CD='" + ld.int_rate_cd + "'ORDER BY WEFF_DATE,UPPER_LIMIT";
        //        config.singleResult(sql);
        //        DataTable loanintrate = config.dt;
        //        if (config.dt.Rows.Count > 0)
        //        {
        //            foreach (DataRow dr3 in config.dt.Rows)
        //            {
        //                ld.int_rate_cd = !Convert.IsDBNull(dr3["int_rate_cd"]) ? Convert.ToString(dr3["int_rate_cd"]) : Convert.ToString("");
        //                ld.weff_date = !Convert.IsDBNull(dr3["weff_date"]) ? Convert.ToString(dr3["weff_date"]) : Convert.ToString("");
        //                ld.upper_limit = !Convert.IsDBNull(dr3["upper_limit"]) ? Convert.ToDecimal(dr3["upper_limit"]) : Convert.ToDecimal("00");
        //                ld.int_rate = !Convert.IsDBNull(dr3["int_rate"]) ? Convert.ToDecimal(dr3["int_rate"]) : Convert.ToDecimal("00");
        //                ld.comments = !Convert.IsDBNull(dr3["comments"]) ? Convert.ToString(dr3["comments"]) : Convert.ToString("");
        //            }
        //        }
        //        sql = "SELECT * FROM LNAINTRATE_DTL WHERE AINT_RATE_CD='" + ld.aint_rate_cd + "' ORDER BY WEFF_DATE";
        //        config.singleResult(sql);
        //        DataTable loanaintrate = config.dt;
        //        if (config.dt.Rows.Count > 0)
        //        {
        //            foreach (DataRow dr4 in config.dt.Rows)
        //            {
        //                ld.aint_rate_cd = !Convert.IsDBNull(dr4["aint_rate_cd"]) ? Convert.ToString(dr4["aint_rate_cd"]) : Convert.ToString("");
        //                ld.weff_date = !Convert.IsDBNull(dr4["weff_date"]) ? Convert.ToString(dr4["weff_date"]) : Convert.ToString("");
        //                ld.aint_rate = !Convert.IsDBNull(dr4["aint_rate"]) ? Convert.ToDecimal(dr4["aint_rate"]) : Convert.ToDecimal("00");
        //            }
        //        }
        //        if (ld.int_category == "F")
        //        {
        //            ld.xirate = int_rate;
        //        }
        //        else
        //        {
        //            ld.xirate = 0;
        //        }
        //        if (loan_amt % insl_no > 0)
        //        {
        //            ld.xinstl = (loan_amt / insl_no) + 1;
        //        }
        //        else
        //        {
        //            ld.xinstl = (loan_amt / insl_no);
        //        }
        //        if (ld.is_lessint_spcl != null)
        //        {
        //            sql = "select * from lncategory_mast where ln_speacial='" + ln_special + "'";
        //            config.singleResult(sql);
        //            if (config.dt.Rows.Count > 0)
        //            {
        //                foreach (DataRow dr5 in config.dt.Rows)
        //                {
        //                    ld.xless_int = !Convert.IsDBNull(dr5["LESS_INT"]) ? Convert.ToDecimal(dr5["LESS_INT"]) : Convert.ToDecimal("00");
        //                }
        //            }
        //        }
        //        ldl.Add(ld);
        //    }
        //    return ld;
        //}
    }
}
