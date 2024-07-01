using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Amritnagar.Includes;

namespace Amritnagar.Models.Database
{
    public class SHARE_LEDGER
    {
        SQLConfig config = new SQLConfig();
        public string branch_id { get; set; }
        public string member_id { get; set; }
        public DateTime vch_date { get; set; }
        public string vch_no { get; set; }
        public decimal vch_srl { get; set; }
        public string vch_type { get; set; }
        public string vch_achd { get; set; }
        public string dr_cr { get; set; }
        public string chq_no { get; set; }
        public DateTime chq_dt { get; set; }
        public string bankcd { get; set; }
        public decimal tr_amount { get; set; }
        public decimal bal_amount { get; set; }
        public decimal units { get; set; }
        public decimal face_val { get; set; }
        public string ref_ac_hd { get; set; }
        public string ref_pacno { get; set; }
        public string ref_oth { get; set; }
        public string certificate_no { get; set; }
        public DateTime certificate_date { get; set; }
        public string cert_prnt_flag { get; set; }
        public decimal dist_no_from { get; set; }
        public decimal dist_no_to { get; set; }
        public string dist_range { get; set; }
        public string insert_mode { get; set; }
        public string ledger_tab { get; set; }
        public string acc_desc { get; set; }
        public string ac_hd { get; set; }
        public string particular { get; set; }
        public decimal TOT_UNITDR { get; set; }
        public decimal TOT_UNITCR { get; set; }
        public decimal TOT_AMTDR { get; set; }
        public decimal TOT_AMTCR { get; set; }
        public decimal balance_unit { get; set; }
        public decimal share_capital { get; set; }
        public decimal MISCDEP_BASEAMT { get; set; }
        public DateTime mem_date { get; set; }
        public string mem_id { get; set; }
        public string mem_name { get; set; }

        public List<SHARE_LEDGER> getShareLedgerDetail(string BranchID, string member_id)
        {
            string sql = "SELECT vch_achd,sum(iif(dr_cr='D',-tr_amount,tr_amount)) as bal_amt from share_ledger ";
            sql = sql + "where branch_id='" + BranchID + "' and member_id='" + member_id + "' group by vch_achd";
            List<SHARE_LEDGER> sll = new List<SHARE_LEDGER>();          
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                SHARE_LEDGER sl = new SHARE_LEDGER();
                DataRow dr = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                sl.vch_achd = Convert.ToString(dr["vch_achd"]);
                sl.bal_amount = !Convert.IsDBNull(dr["bal_amt"]) ? Convert.ToDecimal(dr["bal_amt"]) : Convert.ToDecimal(00);                
                sql = "select * from acc_head where ac_hd='" + sl.vch_achd + "'";
                config.singleResult(sql);
                if (config.dt.Rows.Count > 0)
                {
                    DataRow dr1 = (DataRow)config.dt.Rows[0];
                    //sl.ledger_tab = Convert.ToString(dr1["ledger_tab"]);
                    sl.acc_desc = Convert.ToString(dr1["ac_DESC"]);
                    sll.Add(sl);
                }
            }
            return sll;
        }
        public List<SHARE_LEDGER> getdetailsbymemid(string BranchID, string member_id)
        {
            string XTR_TYPE = "";
            string sql = "SELECT * FROM share_LEDGER WHERE BRANCH_ID='" + BranchID + "' AND MEMBER_ID='" + member_id + "' ORDER BY VCH_DATE,VCH_NO,VCH_SRL";
            List<SHARE_LEDGER> sll = new List<SHARE_LEDGER>();
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach(DataRow dr in config.dt.Rows)
                {
                    SHARE_LEDGER sl = new SHARE_LEDGER();
                    sl.vch_type = Convert.ToString(dr["VCH_TYPE"]);
                    sl.dr_cr = Convert.ToString(dr["DR_CR"]);
                    sl.ref_ac_hd = !Convert.IsDBNull(dr["REF_AC_HD"]) ? Convert.ToString(dr["REF_AC_HD"]) : Convert.ToString("");
                    sl.ref_pacno = !Convert.IsDBNull(dr["ref_pacno"]) ? Convert.ToString(dr["ref_pacno"]) : Convert.ToString("");
                    sl.ref_oth = !Convert.IsDBNull(dr["ref_oth"]) ? Convert.ToString(dr["ref_oth"]) : Convert.ToString("");
                    sl.insert_mode = !Convert.IsDBNull(dr["INSERT_MODE"]) ? Convert.ToString(dr["INSERT_MODE"]) : Convert.ToString("");
                    sl.units = !Convert.IsDBNull(dr["UNITS"]) ? Convert.ToDecimal(dr["UNITS"]) : Convert.ToDecimal(00);
                    sl.vch_date = !Convert.IsDBNull(dr["vch_date"]) ? Convert.ToDateTime(dr["vch_date"]) : Convert.ToDateTime(null);
                    sl.face_val = !Convert.IsDBNull(dr["face_val"]) ? Convert.ToDecimal(dr["face_val"]) : Convert.ToDecimal(00);
                    sl.bal_amount = !Convert.IsDBNull(dr["BAL_AMOUNT"]) ? Convert.ToDecimal(dr["BAL_AMOUNT"]) : Convert.ToDecimal(00);
                    sl.tr_amount = !Convert.IsDBNull(dr["TR_AMOUNT"]) ? Convert.ToDecimal(dr["TR_AMOUNT"]) : Convert.ToDecimal(00);
                    sl.certificate_no = !Convert.IsDBNull(dr["CERTIFICATE_NO"]) ? Convert.ToString(dr["CERTIFICATE_NO"]) : Convert.ToString("");
                    sl.certificate_date = !Convert.IsDBNull(dr["CERTIFICATE_DATE"]) ? Convert.ToDateTime(dr["CERTIFICATE_DATE"]) : Convert.ToDateTime(null);
                    sl.dist_no_from = !Convert.IsDBNull(dr["dist_no_from"]) ? Convert.ToDecimal(dr["dist_no_from"]) : Convert.ToDecimal(00);
                    sl.dist_no_to = !Convert.IsDBNull(dr["dist_no_to"]) ? Convert.ToDecimal(dr["dist_no_to"]) : Convert.ToDecimal(00);
                    sl.dist_range = sl.dist_no_from.ToString() + "-" + sl.dist_no_to.ToString();
                    if (sl.dr_cr == "D")
                    {
                        XTR_TYPE = "To ";
                    }
                    else
                    {
                        XTR_TYPE = "By ";
                    }
                    if(sl.vch_type == "C")
                    {
                        XTR_TYPE = XTR_TYPE + "Cash";
                    }
                    if (sl.vch_type == "B")
                    {
                        XTR_TYPE = XTR_TYPE + "Bank";
                    }
                    if (sl.vch_type == "T")
                    {
                        XTR_TYPE = XTR_TYPE + "Transfer";
                    }
                    if (sl.vch_type == "J")
                    {
                        XTR_TYPE = XTR_TYPE + "Journal";
                    }                   
                    if (sl.ref_ac_hd != "")
                    {
                        if(sl.ref_pacno != "")
                        {
                            XTR_TYPE = XTR_TYPE + "(" + sl.ref_ac_hd + "/" + sl.ref_pacno;
                            if (sl.ref_oth != "")
                            {
                                XTR_TYPE = XTR_TYPE +  "/" + sl.ref_oth + ")";
                            }
                            else
                            {
                                XTR_TYPE = XTR_TYPE + ")";
                            }
                        }                       
                    }
                    if (sl.insert_mode == "BL")
                    {
                        XTR_TYPE = XTR_TYPE + " (Balance From Ledger)";
                    }
                    sl.particular = XTR_TYPE;
                    sll.Add(sl);
                }
            }
            return sll;
        }
        public List<SHARE_LEDGER> getledgerlistsbymemid(string BranchID, string member_id)
        {               
            string sql = string.Empty;
            sql = "SELECT VCH_ACHD,SUM(IIF(DR_CR='D',UNITS,0)) AS TOT_UNITDR,SUM(IIF(DR_CR='C',UNITS,0)) AS TOT_UNITCR,";
            sql = sql + "SUM(IIF(DR_CR='D',TR_AMOUNT,0)) AS TOT_AMTDR,SUM(IIF(DR_CR='C',TR_AMOUNT,0)) AS TOT_AMTCR";
            sql = sql + " FROM SHARE_LEDGER WHERE BRANCH_ID='" + BranchID + "' AND MEMBER_ID='" + member_id + "' GROUP BY VCH_ACHD";
            List<SHARE_LEDGER> sll = new List<SHARE_LEDGER>();
            //SHARE_LEDGER sl = new SHARE_LEDGER();
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    SHARE_LEDGER sl = new SHARE_LEDGER();
                    sl.vch_achd = Convert.ToString(dr["vch_achd"]);                    
                    sl.TOT_UNITDR = !Convert.IsDBNull(dr["TOT_AMTDR"]) ? Convert.ToDecimal(dr["TOT_AMTDR"]) : Convert.ToDecimal(00);
                    sl.TOT_UNITCR = !Convert.IsDBNull(dr["TOT_UNITCR"]) ? Convert.ToDecimal(dr["TOT_UNITCR"]) : Convert.ToDecimal(00);
                    sl.TOT_AMTDR = !Convert.IsDBNull(dr["TOT_AMTDR"]) ? Convert.ToDecimal(dr["TOT_AMTDR"]) : Convert.ToDecimal(00);
                    sl.TOT_AMTCR = !Convert.IsDBNull(dr["TOT_AMTCR"]) ? Convert.ToDecimal(dr["TOT_AMTCR"]) : Convert.ToDecimal(00);
                    sl.balance_unit = sl.TOT_UNITCR - TOT_UNITDR;
                    sl.share_capital = sl.TOT_AMTCR - sl.TOT_AMTDR;
                    sql = "select * from ACC_HEAD where AC_HD='" + sl.vch_achd + "'";
                    config.singleResult(sql);
                    if (config.dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr1 in config.dt.Rows)
                        {
                            sl.acc_desc = Convert.ToString(dr1["AC_DESC"]);
                            sl.MISCDEP_BASEAMT = !Convert.IsDBNull(dr1["MISCDEP_BASEAMT"]) ? Convert.ToDecimal(dr1["MISCDEP_BASEAMT"]) : Convert.ToDecimal(00);
                        }
                    }
                    sll.Add(sl);
                }
            }
            return sll;
        }
        public List<SHARE_LEDGER> getdetails(string BranchID, string on_dt, string gl_hd)
        {
            List<SHARE_LEDGER> sll = new List<SHARE_LEDGER>();
            string sql = string.Empty;
            if(gl_hd != "")
            {
                sql = "SELECT * FROM ACC_HEAD WHERE AC_HD='" + gl_hd + "'";
                config.singleResult(sql);   
                if(config.dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in config.dt.Rows)
                    {
                        SHARE_LEDGER sl = new SHARE_LEDGER();
                        sl.ac_hd = Convert.ToString(dr["ac_hd"]);
                        sl.acc_desc = Convert.ToString(dr["AC_DESC"]) + "(" + Convert.ToString(dr["ac_hd"]) + ")";
                        sl.MISCDEP_BASEAMT = !Convert.IsDBNull(dr["MISCDEP_BASEAMT"]) ? Convert.ToDecimal(dr["MISCDEP_BASEAMT"]) : Convert.ToDecimal(00);
                        sql = "SELECT * FROM MEMBER_MAST WHERE BRANCH_ID='" + BranchID + "' AND ";
                        sql = sql + "MEMBER_TYPE='GEN' AND convert(datetime, MEMBER_DATE, 103) <= convert(datetime, '" + on_dt + "', 103) AND ";
                        sql = sql + "IIF(MEMBER_CLOSDT is NULL,convert(datetime, '31/03/2099', 103),convert(datetime, MEMBER_CLOSDT, 103)) >= convert(datetime, '" + on_dt + "', 103) ORDER BY member_id";
                        config.singleResult(sql);
                        if (config.dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr1 in config.dt.Rows)
                            {
                                //SHARE_LEDGER sl = new SHARE_LEDGER();
                                sl.mem_date = Convert.ToDateTime(dr1["MEMBER_DATE"]);
                                sl.mem_name = Convert.ToString(dr1["member_name"]);
                                sl.mem_id = Convert.ToString(dr1["MEMBER_ID"]);
                                sql = "SELECT SUM(IIF(DR_CR='D',-UNITS,UNITS)) AS BAL_UNIT,SUM(IIF(DR_CR='D',-TR_AMOUNT,TR_AMOUNT)) AS BAL_AMT ";
                                sql = sql + "FROM SHARE_LEDGER WHERE BRANCH_ID='" + BranchID + "' AND MEMBER_ID='" + sl.mem_id + "' AND convert(datetime, VCH_DATE, 103) <= convert(datetime, '" + on_dt + "', 103)";
                                sql = sql + " AND VCH_ACHD='" + gl_hd + "'";
                                config.singleResult(sql);
                                if (config.dt.Rows.Count > 0)
                                {
                                    foreach (DataRow dr2 in config.dt.Rows)
                                    {
                                        sl.bal_amount = !Convert.IsDBNull(dr2["bal_amt"]) ? Convert.ToDecimal(dr2["bal_amt"]) : Convert.ToDecimal(00);
                                        sl.balance_unit = !Convert.IsDBNull(dr2["bal_unit"]) ? Convert.ToDecimal(dr2["bal_unit"]) : Convert.ToDecimal(00);
                                        sll.Add(sl);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                sql = "SELECT * FROM ACC_HEAD order by AC_HD";
                config.singleResult(sql);
                if(config.dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in config.dt.Rows)
                    {
                        SHARE_LEDGER sl = new SHARE_LEDGER();
                        sl.ac_hd = Convert.ToString(dr["ac_hd"]);
                        sl.acc_desc = Convert.ToString(dr["AC_DESC"]) + "(" + Convert.ToString(dr["ac_hd"]) + ")";
                        sl.MISCDEP_BASEAMT = !Convert.IsDBNull(dr["MISCDEP_BASEAMT"]) ? Convert.ToDecimal(dr["MISCDEP_BASEAMT"]) : Convert.ToDecimal(00);
                        sql = "SELECT * FROM MEMBER_MAST WHERE BRANCH_ID='" + BranchID + "' AND ";
                        sql = sql + "MEMBER_TYPE='GEN' AND convert(datetime, MEMBER_DATE, 103) <= convert(datetime, '" + on_dt + "', 103) AND ";
                        sql = sql + "IIF(MEMBER_CLOSDT is NULL,convert(datetime, '31/03/2099', 103),convert(datetime, MEMBER_CLOSDT, 103)) >= convert(datetime, '" + on_dt + "', 103) ORDER BY member_id";
                        config.singleResult(sql);
                        if (config.dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr1 in config.dt.Rows)
                            {
                                //SHARE_LEDGER sl = new SHARE_LEDGER();
                                sl.mem_date = Convert.ToDateTime(dr1["MEMBER_DATE"]);
                                sl.mem_name = Convert.ToString(dr1["member_name"]);
                                sl.mem_id = Convert.ToString(dr1["MEMBER_ID"]);
                                sql = "SELECT SUM(IIF(DR_CR='D',-UNITS,UNITS)) AS BAL_UNIT,SUM(IIF(DR_CR='D',-TR_AMOUNT,TR_AMOUNT)) AS BAL_AMT ";
                                sql = sql + "FROM SHARE_LEDGER WHERE BRANCH_ID='" + BranchID + "' AND MEMBER_ID='" + sl.mem_id + "' AND convert(datetime, VCH_DATE, 103) <= convert(datetime, '" + on_dt + "', 103)";
                                sql = sql + " AND VCH_ACHD='" + sl.ac_hd + "'";
                                config.singleResult(sql);
                                if (config.dt.Rows.Count > 0)
                                {
                                    foreach (DataRow dr2 in config.dt.Rows)
                                    {
                                        sl.bal_amount = !Convert.IsDBNull(dr2["bal_amt"]) ? Convert.ToDecimal(dr2["bal_amt"]) : Convert.ToDecimal(00);
                                        sl.balance_unit = !Convert.IsDBNull(dr2["bal_unit"]) ? Convert.ToDecimal(dr2["bal_unit"]) : Convert.ToDecimal(00);
                                        sll.Add(sl);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return sll;
        }
    }
}