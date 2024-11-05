using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Amritnagar.Includes;
namespace Amritnagar.Models.Database
{
    public class Rep_Acc_Genled
    {
        SQLConfig config = new SQLConfig();
        public string gl_type { get; set; }
        public string ac_majgr { get; set; }
        public string ac_majgrdesc { get; set; }
        public string ac_hd { get; set; }
        public string vch_ac_hd { get; set; }
        public string ac_desc { get; set; }
        public string xmgrdesc { get; set; }
        public DateTime gl_date { get; set; }
        public decimal cash_cr { get; set; }
        public decimal bank_cr { get; set; }
        public decimal trans_cr { get; set; }
        public decimal journal_cr { get; set; }
        public decimal total_cr { get; set; }
        public decimal cash_dr { get; set; }
        public decimal bank_dr { get; set; }
        public decimal trans_dr { get; set; }
        public decimal journal_dr { get; set; }
        public decimal total_dr { get; set; }
        public decimal gl_bal { get; set; }
        public bool GETBAL { get; set; }
        public decimal XACBAL { get; set; }
        public decimal bank_cont_cr { get; set; }
        public decimal bank_cont_dr { get; set; }

        public void Check_Delete_SaveGenled(string branch, string ac_hd, string fr_dt, string to_dt)
        {            
            Rep_Acc_Genled rag = new Rep_Acc_Genled();
            string sql = string.Empty;
            DateTime XOPDT = new DateTime();
            DateTime xcldt = new DateTime();
            sql = "SELECT * FROM REP_ACC_GENLED";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {               
                sql = "Delete from REP_ACC_GENLED";
                config.Execute_Query(sql);                                         
            }
            sql = "select * from acc_head order by ac_hd";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {                
                XOPDT = Convert.ToDateTime(Convert.ToDateTime(fr_dt).AddDays(-1).ToString("dd-MM-yyyy").Replace("-", "/"));
                xcldt = Convert.ToDateTime(Convert.ToDateTime(to_dt).AddDays(1).ToString("dd-MM-yyyy").Replace("-", "/"));
                //for (int i = 1; i <= config.dt.Rows.Count; i++)
                //{
                    foreach (DataRow dr in config.dt.Rows)
                    {
                        //DataRow dr = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                        rag.ac_hd = Convert.ToString(dr["ac_hd"]);
                        rag.ac_desc = Convert.ToString(dr["ac_desc"]);
                        rag.ac_majgr = Convert.ToString(dr["ac_majgr"]);
                        rag.xmgrdesc = GET_MGR(rag.ac_majgr);
                        rag.XACBAL = 0;
                        rag.GETBAL = true;
                        if (rag.ac_hd != "CASH" && rag.ac_hd != "BANK")
                        {
                            sql = "SELECT * FROM GL_BALNCE ";
                            sql = sql + "WHERE branch_id='" + branch + "' and ";
                            sql = sql + "convert(datetime, GL_DATE, 103) < convert(datetime, '" + fr_dt + "', 103)";
                            sql = sql + "AND AC_HD='" + rag.ac_hd + "' ORDER BY GL_DATE";
                            config.singleResult(sql);
                            if (config.dt.Rows.Count == 0)
                            {
                                rag.GETBAL = false;
                                rag.XACBAL = 0;
                            }
                            else
                            {
                                DataRow dr1 = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                                rag.GETBAL = true;
                                rag.XACBAL = !Convert.IsDBNull(dr1["gl_bal"]) ? Convert.ToDecimal(dr1["gl_bal"]) : Convert.ToDecimal(00);
                            }
                            sql = "SELECT A.AC_HD, A.VCH_DATE AS TR_DATE,";
                            sql = sql + "SUM(IIF(A.VCH_DRCR='C' AND B.VCH_TYPE='C', A.VCH_AMT,0)) AS CASH_CR,";
                            sql = sql + "SUM(IIF(A.VCH_DRCR='C' AND B.VCH_TYPE='B',A.VCH_AMT,0)) AS BANK_CR,";
                            sql = sql + "SUM(IIF(A.VCH_DRCR='C' AND B.VCH_TYPE='T',A.VCH_AMT,0)) AS TRANS_CR,";
                            sql = sql + "SUM(IIF(A.VCH_DRCR='C' AND B.VCH_TYPE='J',A.VCH_AMT,0)) AS JOURNAL_CR,";
                            sql = sql + "SUM(IIF(A.VCH_DRCR='D' AND B.VCH_TYPE='C',A.VCH_AMT,0)) AS CASH_DR,";
                            sql = sql + "SUM(IIF(A.VCH_DRCR='D' AND B.VCH_TYPE='B',A.VCH_AMT,0)) AS BANK_DR,";
                            sql = sql + "SUM(IIF(A.VCH_DRCR='D' AND B.VCH_TYPE='T',A.VCH_AMT,0)) AS TRANS_DR,";
                            sql = sql + "SUM(IIF(A.VCH_DRCR='D' AND B.VCH_TYPE='J',A.VCH_AMT,0)) AS JOURNAL_DR ";
                            sql = sql + "FROM VCH_DETAIL A,VCH_HEADER B WHERE (A.BRANCH_ID=B.BRANCH_ID AND A.VCH_DATE=B.VCH_DATE AND A.VCH_NO=B.VCH_NO) ";
                            sql = sql + "AND convert(datetime, A.VCH_DATE, 103) >= convert(datetime, '" + fr_dt + "', 103) and convert(datetime, A.VCH_DATE, 103) <= convert(datetime, '" + to_dt + "', 103) AND ";
                            sql = sql + "A.BRANCH_ID='" + branch + "' AND AC_HD='" + rag.ac_hd + "'";
                            sql = sql + "GROUP BY A.AC_HD,A.VCH_DATE";
                            config.singleResult(sql);
                            if (config.dt.Rows.Count == 0)
                            {                                
                                if (rag.GETBAL == true)
                                {
                                    config.Insert("rep_acc_genled", new Dictionary<String, object>()
                                    {
                                        {"GL_TYPE",   "O" },
                                        {"ac_hd", rag.ac_hd },
                                        {"ac_majgr",    rag.ac_majgr},
                                        {"AC_DESC",   rag.ac_desc},
                                        {"ac_majgrdesc", rag.xmgrdesc },
                                        {"GL_DATE",    XOPDT},
                                        {"gl_bal",    rag.XACBAL}
                                    });
                                    config.Insert("rep_acc_genled", new Dictionary<String, object>()
                                    {
                                        {"GL_TYPE",   "C" },
                                        {"ac_hd", rag.ac_hd },
                                        {"ac_majgr",    rag.ac_majgr},
                                        {"AC_DESC",   rag.ac_desc},
                                        {"ac_majgrdesc", rag.xmgrdesc },
                                        {"GL_DATE",    xcldt},
                                        {"gl_bal",    rag.XACBAL}
                                    });
                                }
                            }
                            else
                            {                               
                                config.Insert("rep_acc_genled", new Dictionary<String, object>()
                                {
                                    {"GL_TYPE",   "O" },
                                    {"ac_hd", rag.ac_hd },
                                    {"ac_majgr",    rag.ac_majgr},
                                    {"AC_DESC",   rag.ac_desc},
                                    {"ac_majgrdesc", rag.xmgrdesc },
                                    {"GL_DATE",    xcldt},
                                    {"gl_bal",    rag.XACBAL}
                                });
                                foreach (DataRow dr3 in config.dt.Rows)
                                {
                                    if (config.dt.Rows.Count == 0)
                                    {
                                        break;
                                    }
                                    rag.vch_ac_hd = Convert.ToString(dr3["ac_hd"]);
                                    if (rag.vch_ac_hd != rag.ac_hd)
                                    {
                                        break;
                                    }
                                    rag.gl_date = !Convert.IsDBNull(dr3["tr_date"]) ? Convert.ToDateTime(dr3["tr_date"]) : Convert.ToDateTime(null);
                                    rag.cash_cr = !Convert.IsDBNull(dr3["CASH_CR"]) ? Convert.ToDecimal(dr3["CASH_CR"]) : Convert.ToDecimal(00);
                                    rag.bank_cr = !Convert.IsDBNull(dr3["BANK_CR"]) ? Convert.ToDecimal(dr3["BANK_CR"]) : Convert.ToDecimal(00);
                                    rag.trans_cr = !Convert.IsDBNull(dr3["TRANS_CR"]) ? Convert.ToDecimal(dr3["TRANS_CR"]) : Convert.ToDecimal(00);
                                    rag.journal_cr = !Convert.IsDBNull(dr3["JOURNAL_CR"]) ? Convert.ToDecimal(dr3["JOURNAL_CR"]) : Convert.ToDecimal(00);
                                    rag.total_cr = rag.cash_cr + rag.bank_cr + rag.trans_cr + rag.journal_cr;
                                    rag.cash_dr = !Convert.IsDBNull(dr3["CASH_DR"]) ? Convert.ToDecimal(dr3["CASH_DR"]) : Convert.ToDecimal(00);
                                    rag.bank_dr = !Convert.IsDBNull(dr3["BANK_DR"]) ? Convert.ToDecimal(dr3["BANK_DR"]) : Convert.ToDecimal(00);
                                    rag.trans_dr = !Convert.IsDBNull(dr3["TRANS_DR"]) ? Convert.ToDecimal(dr3["TRANS_DR"]) : Convert.ToDecimal(00);
                                    rag.journal_dr = !Convert.IsDBNull(dr3["JOURNAL_DR"]) ? Convert.ToDecimal(dr3["JOURNAL_DR"]) : Convert.ToDecimal(00);
                                    rag.total_dr = rag.cash_dr + rag.bank_dr + rag.trans_dr + rag.journal_dr;
                                    rag.XACBAL = rag.XACBAL + rag.total_cr - rag.total_dr;
                                    config.Insert("rep_acc_genled", new Dictionary<String, object>()
                                    {
                                        {"GL_TYPE",   "T" },
                                        {"ac_hd", rag.ac_hd },
                                        {"ac_majgr",    rag.ac_majgr},
                                        {"AC_DESC",   rag.ac_desc},
                                        {"ac_majgrdesc", rag.xmgrdesc },
                                        {"GL_DATE",    rag.gl_date},
                                        {"CASH_CR",    rag.cash_cr},
                                        {"BANK_CR",    rag.bank_cr},
                                        {"TRANS_CR",    rag.trans_cr},
                                        {"JOURNAL_CR",    rag.journal_cr},
                                        {"TOTAL_CR",    rag.total_cr},
                                        {"CASH_DR",    rag.cash_dr},
                                        {"BANK_DR",    rag.bank_dr},
                                        {"TRANS_DR",    rag.trans_dr},
                                        {"JOURNAL_DR",    rag.journal_dr},
                                        {"TOTAL_DR",    rag.total_dr},
                                        {"gl_bal",    rag.XACBAL}
                                    });
                                }
                                config.Insert("rep_acc_genled", new Dictionary<String, object>()
                                {
                                    {"GL_TYPE",   "C" },
                                    {"ac_hd", rag.ac_hd },
                                    {"ac_majgr",    rag.ac_majgr},
                                    {"AC_DESC",   rag.ac_desc},
                                    {"ac_majgrdesc", rag.xmgrdesc },
                                    {"GL_DATE",    xcldt},
                                    {"gl_bal",    rag.XACBAL}
                                });                               
                            }
                        }
                    }
                //}
            }
            sql = "SELECT * FROM ACC_HEAD WHERE IS_CONTRA='Y'";
            config.singleResult(sql);
            string CHKBNK = "";
            int B = 1;          
            if (config.dt.Rows.Count > 0)
            {            
                foreach (DataRow dr8 in config.dt.Rows)
                {
                    rag.ac_hd = Convert.ToString(dr8["ac_hd"]);
                    for(B=1; B<= config.dt.Rows.Count; B++)
                    {
                        if (CHKBNK != "")
                        {
                            CHKBNK = CHKBNK + ",";
                        }
                        CHKBNK = CHKBNK + rag.ac_hd;
                    }                   
                }
            }
            decimal XCASHBAL = 0;
            decimal XBANKBAL = 0;            
            sql = "SELECT * FROM GL_BALNCE ";
            sql = sql + "WHERE branch_id='" +branch+ "' and ";
            sql = sql + "convert(datetime, GL_DATE, 103) < convert(datetime, '" + fr_dt + "', 103)";
            sql = sql + "AND AC_HD='CASH' ORDER BY GL_DATE";
            config.singleResult(sql);
            if(config.dt.Rows.Count == 0)
            {
                XCASHBAL = 0;
            }            
            else
            {
                DataRow dr9 = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                XCASHBAL = !Convert.IsDBNull(dr9["gl_bal"]) ? Convert.ToDecimal(dr9["gl_bal"]) : Convert.ToDecimal(00);
            }
            sql = "SELECT * FROM GL_BALNCE ";
            sql = sql + "WHERE branch_id='" + branch + "' and ";
            sql = sql + "convert(datetime, GL_DATE, 103) < convert(datetime, '" + fr_dt + "', 103)";
            sql = sql + "AND AC_HD='BANK' ORDER BY GL_DATE";
            config.singleResult(sql);
            if(config.dt.Rows.Count == 0)
            {
                XBANKBAL = 0;
            }            
            else
            {
                DataRow dr5 = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                XBANKBAL = !Convert.IsDBNull(dr5["gl_bal"]) ? Convert.ToDecimal(dr5["gl_bal"]) : Convert.ToDecimal(00);
            }
            bool XCASHBF = false;
            bool XBANKBF = false;
            string xacdesc1 = "";
            string XMGR1 = "";
            string XMGRDESC1 = "";
            string xacdesc2 = "";
            string XMGR2 = "";
            string XMGRDESC2 = "";
            sql = "select * from acc_head where AC_HD='CASH' order by ac_hd";
            config.singleResult(sql);
            if(config.dt.Rows.Count == 0)
            {
                xacdesc1 = "";
                XMGR1 = "";
                XMGRDESC1 = "";
            }
            else
            {
                DataRow dr6 = (DataRow)config.dt.Rows[0];
                xacdesc1 = Convert.ToString(dr6["AC_DESC"]);
                XMGR1 = Convert.ToString(dr6["ac_majgr"]);
                XMGRDESC1 = GET_MGR(XMGR1);
            }
            sql = "select * from acc_head where AC_HD='BANK' order by ac_hd";
            config.singleResult(sql);
            if (config.dt.Rows.Count == 0)
            {
                xacdesc2 = "";
                XMGR2 = "";
                XMGRDESC2 = "";
            }
            else
            {
                DataRow dr7 = (DataRow)config.dt.Rows[0];
                xacdesc2 = Convert.ToString(dr7["AC_DESC"]);
                XMGR2 = Convert.ToString(dr7["ac_majgr"]);
                XMGRDESC2 = GET_MGR(XMGR1);
            }
            if (XCASHBAL != 0)
            {
                config.Insert("rep_acc_genled", new Dictionary<String, object>()
                {
                    {"GL_TYPE",   "O" },
                    {"ac_hd", "CASH" },
                    {"ac_majgr",    XMGR1},
                    {"AC_DESC",   xacdesc1},
                    {"ac_majgrdesc", XMGRDESC1 },
                    {"GL_DATE",    XOPDT},
                    {"gl_bal",    XCASHBAL}
                });
            }
            if (XBANKBAL != 0)
            {
                config.Insert("rep_acc_genled", new Dictionary<String, object>()
                {
                    {"GL_TYPE",   "O" },
                    {"ac_hd",   "BANK" },
                    {"ac_majgr",    XMGR2},
                    {"AC_DESC",   xacdesc2},
                    {"ac_majgrdesc", XMGRDESC2 },
                    {"GL_DATE",    XOPDT},
                    {"gl_bal",    XBANKBAL}
                });
            }
            sql = "SELECT A.VCH_DATE AS TR_DATE,";
            sql = sql + "SUM(IIF(A.VCH_DRCR='D' AND B.VCH_TYPE='C', A.VCH_AMT,0)) AS CASH_CR,";
            sql = sql + "SUM(IIF(A.VCH_DRCR='D' AND B.VCH_TYPE='B',A.VCH_AMT,0)) AS BANK_CR,";
            sql = sql + "SUM(IIF(A.VCH_DRCR='C' AND B.VCH_TYPE='C',A.VCH_AMT,0)) AS CASH_DR,";
            sql = sql + "SUM(IIF(A.VCH_DRCR='C' AND B.VCH_TYPE='B',A.VCH_AMT,0)) AS BANK_DR,";
            if (CHKBNK != "")
            {
                sql = sql + "SUM(IIF(A.VCH_DRCR='D' AND A.AC_HD IN ('" + CHKBNK + "'),A.VCH_AMT,0)) AS BANK_CONT_DR,";
                sql = sql + "SUM(IIF(A.VCH_DRCR='C' AND A.AC_HD IN ('" + CHKBNK + "'),A.VCH_AMT,0)) AS BANK_CONT_CR ";
            }
            else
            {
                sql = sql + "0 AS BANK_CONT_CR,0 AS BANK_CONT_DR";
            }
            sql = sql + "FROM VCH_DETAIL A,VCH_HEADER B WHERE (A.VCH_DATE=B.VCH_DATE AND A.VCH_NO=B.VCH_NO) ";
            sql = sql + "AND convert(datetime, A.VCH_DATE, 103) >= convert(datetime, '" + fr_dt + "', 103) and convert(datetime, A.VCH_DATE, 103) <= convert(datetime, '" + to_dt + "', 103) AND ";
            sql = sql + "A.BRANCH_ID='" + branch + "'";
            sql = sql + "GROUP BY A.VCH_DATE";
            config.singleResult(sql);
            if(config.dt.Rows.Count > 0)
            {
                //for(int i=1; i<= config.dt.Rows.Count; i++)
                //{
                    foreach(DataRow dr8 in config.dt.Rows)
                    {
                        rag.gl_date = !Convert.IsDBNull(dr8["tr_date"]) ? Convert.ToDateTime(dr8["tr_date"]) : Convert.ToDateTime(null);
                        rag.cash_cr = !Convert.IsDBNull(dr8["CASH_CR"]) ? Convert.ToDecimal(dr8["CASH_CR"]) : Convert.ToDecimal(00);
                        rag.bank_cr = !Convert.IsDBNull(dr8["BANK_CR"]) ? Convert.ToDecimal(dr8["BANK_CR"]) : Convert.ToDecimal(00);
                        rag.cash_dr = !Convert.IsDBNull(dr8["CASH_DR"]) ? Convert.ToDecimal(dr8["CASH_DR"]) : Convert.ToDecimal(00);
                        rag.bank_dr = !Convert.IsDBNull(dr8["BANK_DR"]) ? Convert.ToDecimal(dr8["BANK_DR"]) : Convert.ToDecimal(00);
                        rag.bank_cont_cr = !Convert.IsDBNull(dr8["BANK_CONT_CR"]) ? Convert.ToDecimal(dr8["BANK_CONT_CR"]) : Convert.ToDecimal(00);
                        rag.bank_cont_dr = !Convert.IsDBNull(dr8["BANK_CONT_DR"]) ? Convert.ToDecimal(dr8["BANK_CONT_DR"]) : Convert.ToDecimal(00);
                        decimal tot_bank_cr = rag.bank_cr + rag.bank_cont_cr;
                        decimal tot_bank_dr = rag.bank_dr + rag.bank_cont_dr;
                        if (rag.cash_cr > 0 && rag.cash_dr > 0)
                        {
                            if (XCASHBF == false)
                            {
                                config.Insert("rep_acc_genled", new Dictionary<String, object>()
                                {
                                    {"GL_TYPE",   "O" },
                                    {"ac_hd",   "CASH" },
                                    {"ac_majgr",    XMGR1},
                                    {"AC_DESC",   xacdesc1},
                                    {"ac_majgrdesc", XMGRDESC1 },
                                    {"GL_DATE",    XOPDT},
                                    {"gl_bal",    XCASHBAL}
                                });
                                XCASHBF = true;
                            }
                            XCASHBAL = XCASHBAL + rag.cash_cr - rag.cash_dr;
                            config.Insert("rep_acc_genled", new Dictionary<String, object>()
                            {
                                {"GL_TYPE",   "T" },
                                {"ac_hd",   "CASH" },
                                {"ac_majgr",    XMGR1},
                                {"AC_DESC",   xacdesc1},
                                {"ac_majgrdesc", XMGRDESC1 },
                                {"GL_DATE",     rag.gl_date},
                                {"CASH_CR",     rag.cash_cr},
                                {"TOTAL_CR",     rag.cash_cr},
                                {"CASH_DR",     rag.cash_dr},
                                {"TOTAL_DR",     rag.cash_dr},
                                {"gl_bal",    XCASHBAL}
                            });
                        }
                        if (tot_bank_cr > 0 && tot_bank_dr > 0)
                        {
                            if (XBANKBF == false)
                            {
                                config.Insert("rep_acc_genled", new Dictionary<String, object>()
                                {
                                    {"GL_TYPE",   "O" },
                                    {"ac_hd",   "BANK" },
                                    {"ac_majgr",    XMGR2},
                                    {"AC_DESC",   xacdesc2},
                                    {"ac_majgrdesc", XMGRDESC2 },
                                    {"GL_DATE",    XOPDT},
                                    {"gl_bal",    XBANKBAL}
                                });
                                XBANKBF = true;
                            }
                            XBANKBAL = XBANKBAL + (rag.bank_cr - rag.bank_dr) + (rag.bank_cont_cr - rag.bank_cont_dr);
                            config.Insert("rep_acc_genled", new Dictionary<String, object>()
                            {
                                {"GL_TYPE",   "T" },
                                {"ac_hd",   "BANK" },
                                {"ac_majgr",    XMGR2},
                                {"AC_DESC",   xacdesc2},
                                {"ac_majgrdesc", XMGRDESC2 },
                                {"GL_DATE",    rag.gl_date},
                                {"BANK_CR",    tot_bank_cr},
                                {"TOTAL_CR",    tot_bank_cr},
                                {"BANK_DR",    tot_bank_dr},
                                {"TOTAL_DR",    tot_bank_dr},
                                {"gl_bal",    XBANKBAL}
                            });
                        }
                    }
                //}
            }
            if (XCASHBF == true)
            {
                config.Insert("rep_acc_genled", new Dictionary<String, object>()
                {
                    {"GL_TYPE",   "C" },
                    {"ac_hd",   "CASH" },
                    {"ac_majgr",    XMGR1},
                    {"AC_DESC",   xacdesc1},
                    {"ac_majgrdesc", XMGRDESC1 },
                    {"GL_DATE",    xcldt},
                    {"gl_bal",    XCASHBAL}
                });
            }
            if (XBANKBF == true)
            {
                config.Insert("rep_acc_genled", new Dictionary<String, object>()
                {
                    {"GL_TYPE",   "C" },
                    {"ac_hd",   "BANK" },
                    {"ac_majgr",    XMGR2},
                    {"AC_DESC",   xacdesc2},
                    {"ac_majgrdesc", XMGRDESC2 },
                    {"GL_DATE",    xcldt},
                    {"gl_bal",    XBANKBAL}
                });
            }           
        }
        public string GET_MGR(string XMGR)
        {
            string get_XMGR = "";
            string sql = "select * from ACC_HEAD_MGR where AC_MAJGR='" + XMGR + "'";
            config.singleResult(sql);
            if(config.dt.Rows.Count > 0)
            {
                DataRow dr = (DataRow)config.dt.Rows[0];
                get_XMGR = Convert.ToString(dr["ac_majgrdesc"]);
            }
            return get_XMGR;
        }
        public List<Rep_Acc_Genled> getdetails(string fr_dt, string to_dt, string ac_hd)
        {
            List<Rep_Acc_Genled> raglst = new List<Rep_Acc_Genled>();
            string sql = string.Empty;
            if(ac_hd == "ALL" || ac_hd == "all")
            {
                sql = "select * from rep_acc_genled where convert(datetime, GL_DATE, 103) >= convert(datetime, '" + fr_dt + "', 103) and convert(datetime, GL_DATE, 103) <= convert(datetime, '" + to_dt + "', 103) order by GL_DATE";
                config.singleResult(sql);
            }
            else
            {
                sql = "select * from rep_acc_genled where convert(datetime, GL_DATE, 103) >= convert(datetime, '" + fr_dt + "', 103) and convert(datetime, GL_DATE, 103) <= convert(datetime, '" + to_dt + "', 103) and AC_HD ='" + ac_hd + "' order by GL_DATE";
                config.singleResult(sql);
            }            
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    Rep_Acc_Genled rag = new Rep_Acc_Genled();
                    rag.gl_type = dr["GL_TYPE"].ToString();
                    rag.gl_date = !Convert.IsDBNull(dr["GL_DATE"]) ? Convert.ToDateTime(dr["GL_DATE"]) : Convert.ToDateTime("");                    
                    rag.ac_majgr = Convert.ToString(dr["AC_MAJGR"]);
                    rag.ac_majgrdesc = Convert.ToString(dr["AC_MAJGRDESC"]);
                    rag.ac_hd = Convert.ToString(dr["AC_HD"]);
                    rag.ac_desc = Convert.ToString(dr["AC_DESC"]);
                    rag.cash_cr = !Convert.IsDBNull(dr["CASH_CR"]) ? Convert.ToDecimal(dr["CASH_CR"]) : Convert.ToDecimal("0.00");
                    rag.bank_cr = !Convert.IsDBNull(dr["BANK_CR"]) ? Convert.ToDecimal(dr["BANK_CR"]) : Convert.ToDecimal("0.00");
                    rag.trans_cr = !Convert.IsDBNull(dr["TRANS_CR"]) ? Convert.ToDecimal(dr["TRANS_CR"]) : Convert.ToDecimal("0.00");
                    rag.journal_cr = !Convert.IsDBNull(dr["JOURNAL_CR"]) ? Convert.ToDecimal(dr["JOURNAL_CR"]) : Convert.ToDecimal("0.00");
                    rag.total_cr = !Convert.IsDBNull(dr["TOTAL_CR"]) ? Convert.ToDecimal(dr["TOTAL_CR"]) : Convert.ToDecimal("0.00");
                    rag.cash_dr = !Convert.IsDBNull(dr["CASH_DR"]) ? Convert.ToDecimal(dr["CASH_DR"]) : Convert.ToDecimal("0.00");
                    rag.bank_dr = !Convert.IsDBNull(dr["BANK_DR"]) ? Convert.ToDecimal(dr["BANK_DR"]) : Convert.ToDecimal("0.00");
                    rag.trans_dr = !Convert.IsDBNull(dr["TRANS_DR"]) ? Convert.ToDecimal(dr["TRANS_DR"]) : Convert.ToDecimal("0.00");
                    rag.journal_dr = !Convert.IsDBNull(dr["JOURNAL_DR"]) ? Convert.ToDecimal(dr["JOURNAL_DR"]) : Convert.ToDecimal("0.00");
                    rag.total_dr = !Convert.IsDBNull(dr["TOTAL_DR"]) ? Convert.ToDecimal(dr["TOTAL_DR"]) : Convert.ToDecimal("0.00");
                    rag.gl_bal = !Convert.IsDBNull(dr["GL_BAL"]) ? Convert.ToDecimal(dr["GL_BAL"]) : Convert.ToDecimal("0.00");                   
                    raglst.Add(rag);
                }
            }
            return raglst;
        }

    }
}