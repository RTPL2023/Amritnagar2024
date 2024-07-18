using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Amritnagar.Includes;

namespace Amritnagar.Models.Database
{
    public class FUNDDEP_MAST
    {
        SQLConfig config = new SQLConfig();
        public string ac_hd { get; set; }
        public string fund_desc { get; set; }
        public string int_appl { get; set; }
        public string int_achd { get; set; }
        public string ledger_tab { get; set; }
        public string is_deposit { get; set; }
        public decimal int_rate { get; set; }
        public decimal bal_amount { get; set; }
        public decimal prin_bal { get; set; }
        public decimal int_bal { get; set; }
        public DateTime vch_date { get; set; }
        public bool xok { get; set; }

        public List<FUNDDEP_MAST> getFundDepositInfo(string BranchID, string member_no)
        {
            List<FUNDDEP_MAST> fdml = new List<FUNDDEP_MAST>();
            string sql = "SELECT * From FUNDDEP_MAST";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    FUNDDEP_MAST fdm = new FUNDDEP_MAST();
                    fdm.xok = false;
                    fdm.ac_hd = Convert.ToString(dr["ac_hd"]);
                    fdm.is_deposit = Convert.ToString(dr["IS_DEPOSIT"]);
                    fdm.fund_desc = Convert.ToString(dr["fund_desc"]);
                    fdm.int_rate = Convert.ToDecimal(dr["INT_RATE"]);                    
                    string sql1 = "SELECT * From acc_head where ac_hd='" + fdm.ac_hd + "'";
                    config.singleResult(sql1);
                    if (config.dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr1 in config.dt.Rows)
                        {
                            fdm.ledger_tab = Convert.ToString(dr1["LEDGER_TAB"]);                             
                        }
                    }
                    string sql2 = "select * from " + fdm.ledger_tab + " where branch_id='" + BranchID + "' and member_id='" + member_no + "' order by vch_date,vch_no,vch_srl";
                    config.singleResult(sql2);
                    if(config.dt.Rows.Count > 0)
                    {
                        DataRow dr2 = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                        if (fdm.ledger_tab == "DIVIDEND_LEDGER")
                        {
                            fdm.bal_amount = !Convert.IsDBNull(dr2["bal_amount"]) ? Convert.ToDecimal(dr2["bal_amount"]) : Convert.ToDecimal(0);
                            fdm.vch_date = !Convert.IsDBNull(dr2["vch_date"]) ? Convert.ToDateTime(dr2["vch_date"]) : Convert.ToDateTime(null);
                            if (fdm.bal_amount > 0)
                            {
                                fdm.xok = true;
                            }
                        }
                        else if (fdm.ledger_tab == "TF_LEDGER" || fdm.ledger_tab == "GF_LEDGER")
                        {
                            fdm.prin_bal = !Convert.IsDBNull(dr2["prin_bal"]) ? Convert.ToDecimal(dr2["prin_bal"]) : Convert.ToDecimal(0);
                            fdm.int_bal = !Convert.IsDBNull(dr2["int_bal"]) ? Convert.ToDecimal(dr2["int_bal"]) : Convert.ToDecimal(0);
                            fdm.vch_date = !Convert.IsDBNull(dr2["vch_date"]) ? Convert.ToDateTime(dr2["vch_date"]) : Convert.ToDateTime(null);
                            if (fdm.prin_bal + fdm.int_bal > 0)
                            {
                                fdm.xok = true;
                            }
                        }
                    }                   
                    fdml.Add(fdm);
                }
            }
            return fdml;
        }

        public FUNDDEP_MAST getIntrateByachd(string ac_hd)
        {
            FUNDDEP_MAST fdm = new FUNDDEP_MAST();
            string sql = "SELECT * From FUNDDEP_MAST where ac_hd='" + ac_hd + "'";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    fdm.int_rate = Convert.ToDecimal(dr["int_rate"]);
                }
            }
            return fdm;
        }

        //public FUNDDEP_MAST get_fdep_int_payble(string ac_hd)
        //{
        //    FUNDDEP_MAST fdm = new FUNDDEP_MAST();
        //    decimal xtot_int = 0;
        //    string XLINTDATE = "";
        //    string xintto = "";
        //    string xintfr = "";
        //    string xstdt = "";
        //    decimal xfmnth = 0;

        //    int month = DateTime.Now.Month;
        //    int year = DateTime.Now.Year;
        //    int year1 = (DateTime.Now.Year) - 1;
        //    if (month > 3)
        //    {
        //        XLINTDATE = "31/03/" + year;
        //    }
        //    else
        //    {
        //        XLINTDATE = "31/03/" + year1;
        //    }
        //    xintfr = Convert.ToDateTime(XLINTDATE).AddDays(1).ToString();
        //    xfmnth = Convert.ToDecimal(Convert.ToDateTime(xintfr).Subtract(DateTime.Now).Days / (365.25 / 12));
        //    return fdm;
        //}

    }
}