using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Amritnagar.Includes;

namespace Amritnagar.Models.Database
{
    public class TVCH_DETAIL
    {
        SQLConfig config = new SQLConfig();
        public string branch_id { get; set; }
        public DateTime trn_date { get; set; }
        public string trn_shift { get; set; }
        public decimal counter_no { get; set; }
        public int tot_cnt { get; set; }
        public decimal tot_depamt { get; set; }
        public string trn_no { get; set; }
        public decimal trn_srl { get; set; }
        public string token_no { get; set; }
        public string vch_drcr { get; set; }
        public string ac_hd { get; set; }
        public string vch_pacno { get; set; }
        public string vch_acname { get; set; }
        public decimal vch_amt { get; set; }
        public string is_self { get; set; }
        public string is_chq { get; set; }
        public string chq_no { get; set; }
        public string chq_date { get; set; }
        public string bankcd { get; set; }
        public string chq_mode { get; set; }
        public string bearer_branch { get; set; }
        public string ref_ac_hd { get; set; }
        public string ref_pacno { get; set; }
        public string ref_oth { get; set; }
        public string insert_mode { get; set; }
        public string conf_flag { get; set; }
        public string cnt_prfx { get; set; }
        public decimal tot_amt { get; set; }

        public void Check_DeleteTVCH_Detail(String branch, String date, string shift, string vch_no, string counter)
        {
            TVCH_DETAIL vd = new TVCH_DETAIL();
            string sql = string.Empty;
            string Shift_type = string.Empty;
            if (shift == "EVENING")
            {
                Shift_type = "E";
            }
            else if (shift == "GENERAL")
            {
                Shift_type = "G";
            }
            else if (shift == "MORNING")
            {
                Shift_type = "M";
            }
            sql = "SELECT * FROM TVCH_DETAIL WHERE BRANCH_ID='" + branch + "' AND ";
            sql = sql + "convert(varchar, TRN_DATE, 103) = '" + date.Replace("-", "/") + "' AND ";
            sql = sql + "TRN_SHIFT='" + Shift_type + "' AND ";
            sql = sql + "INSERT_MODE='MR'";
            sql = sql + "AND COUNTER_NO='" + counter + "'";
            sql = sql + " ORDER BY TRN_DATE";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    int i = 0;
                    vd.ac_hd = Convert.ToString(dr["AC_HD"]);
                    vd.vch_pacno = Convert.ToString(dr["VCH_PACNO"]);
                    vd.trn_srl = Convert.ToDecimal(dr["TRN_SRL"]);
                    Ledger ld = new Ledger();
                    ld = ld.GET_GEN_LEDGER_For_TVCH(vd.ac_hd, vd.vch_pacno, date, branch, Convert.ToInt32(trn_srl));
                    if (ld.query != "")
                    {
                        ld.Delete_LEDGER_For_TVCH(vd.ac_hd, vd.vch_pacno, date, Convert.ToInt32(vd.trn_srl), ld.query, ld.table, vch_no, branch);
                    }
                }
                sql = "Delete from TVCH_DETAIL where BRANCH_ID = '" + branch + "' AND convert(varchar, TRN_DATE, 103) = '" + date.Replace("-", "/") + "' and TRN_NO = '" + vch_no + "'";
                config.Execute_Query(sql);
            }
        }
        public void SaveUpdateTVch_Detail(string vch_date, string txtvch_No, string branch_id, string shift, string counter, string acc_no, string name, string achd, string amt)
        {
            string Shift_type = string.Empty;
            if (shift == "EVENING")
            {
                Shift_type = "E";
            }
            else if (shift == "GENERAL")
            {
                Shift_type = "G";
            }
            else if (shift == "MORNING")
            {
                Shift_type = "M";
            }
            TVCH_DETAIL vd = new TVCH_DETAIL();
            vd.branch_id = branch_id;
            vd.trn_date = Convert.ToDateTime(vch_date.Replace("-", "/") +" "+ DateTime.Now.ToShortTimeString());
            vd.trn_no = txtvch_No;
            vd.trn_shift = Shift_type;
            vd.counter_no = Convert.ToDecimal(counter);
            vd.trn_srl = 1;
            vd.vch_drcr = "C";
            vd.ac_hd = achd;
            vd.vch_pacno = acc_no;
            vd.vch_acname = name;
            vd.vch_amt = Convert.ToDecimal(amt);
            vd.insert_mode = "MR";         
            config.Insert("TVCH_DETAIL", new Dictionary<String, object>()
            {
                {"branch_id",   vd.branch_id },
                { "trn_date",   vd.trn_date },
                { "trn_shift", vd.trn_shift },
                { "trn_no",    vd.trn_no },
                { "VCH_DRCR",   vd.vch_drcr },
                { "ac_hd",  vd.ac_hd },
                { "vch_pacno",  vd.vch_pacno },
                { "vch_acname", vd.vch_acname },
                { "vch_amt",    vd.vch_amt },
                {"counter_no",   vd.counter_no},
                { "trn_srl",  vd.trn_srl},                    
                { "insert_mode",    vd.insert_mode},
            });
            Ledger ld = new Ledger();
            ld.AddLedger_For_TVCH(vd);
        }
        public List<TVCH_DETAIL> getpersonalrecieptdetails(string branch, string shift, string date, string counter)
        {
            string sql;
            string Shift_type = string.Empty;
            if (shift == "EVENING")
            {
                Shift_type = "E";
            }
            else if (shift == "GENERAL")
            {
                Shift_type = "G";
            }
            else if (shift == "MORNING")
            {
                Shift_type = "M";
            }
            sql = "SELECT * FROM TVCH_DETAIL WHERE BRANCH_ID='" + branch + "' AND ";
            sql = sql + "convert(varchar, TRN_DATE, 103) = '" + date.Replace("-", "/") + "' AND ";
            sql = sql + "TRN_SHIFT='" + Shift_type + "' AND ";
            sql = sql + "INSERT_MODE='MR'";
            sql = sql + "AND COUNTER_NO='" + counter + "'";
            sql = sql + " ORDER BY TRN_DATE";
            config.singleResult(sql);
            List<TVCH_DETAIL> vdlist = new List<TVCH_DETAIL>();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    TVCH_DETAIL vd = new TVCH_DETAIL();
                    vd.trn_no = Convert.ToString(dr["trn_no"]);
                    vd.ac_hd = dr["ac_hd"].ToString();
                    vd.vch_pacno = dr["vch_pacno"].ToString();
                    vd.vch_acname = dr["vch_acname"].ToString();
                    vd.vch_amt = Convert.ToDecimal(dr["vch_amt"]);
                    vdlist.Add(vd);
                }
            }
            return vdlist;
        }
        public List<TVCH_DETAIL> getsummaryreciept(string branch, string shift, string date, string counter)
        {
            string sql;
            string Shift_type = string.Empty;
            if (shift == "EVENING")
            {
                Shift_type = "E";
            }
            else if (shift == "GENERAL")
            {
                Shift_type = "G";
            }
            else if (shift == "MORNING")
            {
                Shift_type = "M";
            }
            sql = "SELECT COUNTER_NO,AC_HD,COUNT(*)AS TOT_CNT,SUM(VCH_AMT) AS TOT_DEPAMT FROM TVCH_DETAIL WHERE BRANCH_ID='" + branch + "' AND ";
            sql = sql + "convert(varchar, TRN_DATE, 103) = '" + date.Replace("-", "/") + "' AND ";
            sql = sql + "TRN_SHIFT='" + Shift_type + "' AND ";
            sql = sql + "INSERT_MODE='MR'";
            sql = sql + "AND COUNTER_NO='" + counter + "'";
            sql = sql + " GROUP BY COUNTER_NO,AC_HD";
            config.singleResult(sql);
            List<TVCH_DETAIL> vdlist = new List<TVCH_DETAIL>();
            if (config.dt.Rows.Count > 0)
            {
                foreach(DataRow dr in config.dt.Rows)
                {
                    TVCH_DETAIL vd = new TVCH_DETAIL();                  
                    vd.ac_hd = dr["ac_hd"].ToString();
                    vd.tot_cnt = Convert.ToInt32(dr["TOT_CNT"]);                  
                    vd.tot_depamt = Convert.ToDecimal(dr["tot_depamt"]);
                    vdlist.Add(vd);
                }                              
            }
            return vdlist;
        }
        public TVCH_DETAIL getlasttr(string branch, string shift, string date, string counter)
        {
            TVCH_DETAIL tvd = new TVCH_DETAIL();
            string sql = string.Empty;
            string Shift_type = string.Empty;
            if (shift == "EVENING")
            {
                Shift_type = "E";
            }
            else if (shift == "GENERAL")
            {
                Shift_type = "G";
            }
            else if (shift == "MORNING")
            {
                Shift_type = "M";
            }
            sql = "SELECT MAX(TRN_NO)AS LAST_SRL,COUNT(*) AS TOT_CNT,SUM(VCH_AMT) AS TOT_AMT FROM TVCH_DETAIL WHERE BRANCH_ID='" + branch + "' AND ";
            sql = sql + "convert(varchar, TRN_DATE, 103) = '" + date.Replace("-", "/") + "' AND ";
            sql = sql + "TRN_SHIFT='" + Shift_type + "' AND ";
            sql = sql + "INSERT_MODE='MR'";
            sql = sql + "AND COUNTER_NO='" + counter + "'";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {                    
                    tvd.tot_cnt = !Convert.IsDBNull(dr["TOT_CNT"]) ? Convert.ToInt32(dr["TOT_CNT"]) : Convert.ToInt32("0.00");                   
                    tvd.tot_amt = !Convert.IsDBNull(dr["TOT_AMT"]) ? Convert.ToDecimal(dr["TOT_AMT"]) : Convert.ToDecimal("0.00");                   
                }
            }          
            return tvd;
        }
        public TVCH_DETAIL getlastvch_no(string branch, string shift, string date, string counter)
        {
            TVCH_DETAIL vd = new TVCH_DETAIL();
            string sql = string.Empty;
            string cnt = "";
            string CSRL = "";
            string Shift_type = string.Empty;
            if (shift == "EVENING")
            {
                Shift_type = "E";
            }
            else if (shift == "GENERAL")
            {
                Shift_type = "G";
            }
            else if (shift == "MORNING")
            {
                Shift_type = "M";
            }
            sql = "SELECT MAX(TRN_NO)AS LAST_SRL,COUNT(*) AS TOT_CNT,SUM(VCH_AMT) AS TOT_AMT FROM TVCH_DETAIL WHERE BRANCH_ID='" + branch + "' AND ";
            sql = sql + "convert(varchar, TRN_DATE, 103) = '" + date.Replace("-", "/") + "' AND ";
            sql = sql + "TRN_SHIFT='" + Shift_type + "' AND ";
            sql = sql + "INSERT_MODE='MR'";
            sql = sql + "AND COUNTER_NO='" + counter + "'";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {              
                DataRow dr = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                if (dr["LAST_SRL"]!=null && Convert.ToString(dr["LAST_SRL"])!="")
                {
                    vd.trn_no = dr["LAST_SRL"].ToString();
                    cnt = vd.getcounterprefix(counter);
                    CSRL = Convert.ToString(Convert.ToInt32(vd.trn_no.Substring(4, 6)) + 1);
                    for(int i=CSRL.Length;i<6;i++)
                    {
                        CSRL = "0" + CSRL;
                    }
                    vd.trn_no = Shift_type + cnt + "MR" + CSRL;
                }
                else
                {
                    cnt = vd.getcounterprefix(counter);
                    vd.trn_no = Shift_type + cnt + "MR" + "000001";
                }                
            }
            else
            {
                cnt = vd.getcounterprefix(counter);
                vd.trn_no = Shift_type + cnt + "MR" + "000001";
            }
            return vd;
        }
        public string getcounterprefix(string counter)
        {
            TVCH_DETAIL tvd = new TVCH_DETAIL();
            string sql = "Select COUNTER_PREFIX from COUNTER_MAST where COUNTER_NO = '" + counter + "'";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    tvd.cnt_prfx = dr["COUNTER_PREFIX"].ToString();
                }              
            }           
            return tvd.cnt_prfx;
        }
        public void Delete_Cash_Reciept(string achd, string vch_no, string date, string acc_no)
        {
            string sql = string.Empty;
            sql = "delete from TVCH_HEADER where TRN_NO='" + vch_no + "' and convert(varchar, TRN_DATE, 103) = '" + date.Replace("-", "/") + "'";
            config.Execute_Query(sql);
            sql = "delete from TVCH_DETAIL where TRN_NO='" + vch_no + "' and convert(varchar, TRN_DATE, 103) = '" + date.Replace("-", "/") + "'";
            config.Execute_Query(sql);
            sql = "select * from ACC_HEAD WHERE AC_HD='" + achd + "'";
            ACC_HEAD ah = new ACC_HEAD();
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                DataRow dr = (DataRow)config.dt.Rows[0];
                ah.ledger_tab = Convert.ToString(dr["ledger_tab"]);
                ah.led_achd = Convert.ToString(dr["led_achd"]);
                if(ah.ledger_tab == "GF_LEDGER" || ah.ledger_tab == "TF_LEDGER")
                {
                    sql = "delete from " + ah.ledger_tab + " where VCH_NO='" + vch_no + "' and convert(varchar, VCH_DATE, 103) = '" + date.Replace("-", "/") + "'";
                    config.singleResult(sql);
                    Ledger ld = new Ledger();
                    ld.ResetPrinBalIntBalForGF_TF__For_TVCH(ah.ledger_tab, ah.led_achd, acc_no, Convert.ToDateTime(date), vch_no);
                }
                if(ah.ledger_tab == "SHARE_LEDGER" || ah.ledger_tab == "DIVIDEND_LEDGER")
                {
                    sql = "delete from " + ah.ledger_tab + " where VCH_NO='" + vch_no + "' and convert(varchar, VCH_DATE, 103) = '" + date.Replace("-", "/") + "'";
                    config.singleResult(sql);
                    Ledger ld = new Ledger();
                    ld.ResetPrinBalIntBalForShare_DividendLedger_For_TVCH(ah.ledger_tab, acc_no, Convert.ToDateTime(date), vch_no);
                }
                if(ah.ledger_tab == "LOAN_LEDGER")
                {
                    sql = "delete from " + ah.ledger_tab + " where VCH_NO='" + vch_no + "' and convert(varchar, VCH_DATE, 103) = '" + date.Replace("-", "/") + "'";
                    config.singleResult(sql);
                    Ledger ld = new Ledger();
                    ld.ResetPrinBalIntDueForLoanLedger_For_TVCH(ah.ledger_tab, ah.led_achd, acc_no, Convert.ToDateTime(date), vch_no);
                }
            }
        }
    }
}