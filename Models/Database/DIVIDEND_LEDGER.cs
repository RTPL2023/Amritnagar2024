using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Amritnagar.Includes;
using Amritnagar.Models.ViewModel;

namespace Amritnagar.Models.Database
{
    public class DIVIDEND_LEDGER
    {
        SQLConfig config = new SQLConfig();

        public string forsave { get; set; }
        public string BRANCH_ID { get; set; }
        public string MEMBER_ID { get; set; }
        public DateTime VCH_DATE { get; set; }
        public string VCH_NO { get; set; }
        public int VCH_SRL { get; set; }
        public string VCH_TYPE { get; set; }
        public string VCH_ACHD { get; set; }
        public string DR_CR { get; set; }
        public int DIV_POST_YEAR_TO { get; set; }
        public string DIV_PAY_MODE { get; set; }
        public string CHQ_NO { get; set; }
        public DateTime CHQ_DT { get; set; }
        public string BANKCD { get; set; }
        public decimal TR_AMOUNT { get; set; }
        public decimal BAL_AMOUNT { get; set; }
        public string REF_AC_HD { get; set; }
        public string REF_PACNO { get; set; }
        public string REF_OTH { get; set; }
        public string INSERT_MODE { get; set; }



        public string SaveInDividentLedger(Member_Mast mm, DividendCalcAndPostViewModel model, decimal clos_prin, double xtot_dividend, int vchsrl)
        {
            string sql = string.Empty;


            sql = "SELECT * FROM DIVIDEND_LEDGER WHERE BRANCH_ID='" + model.branch + "' AND MEMBER_ID='" + mm.mem_id + "' ORDER BY VCH_DATE,VCH_NO,VCH_SRL";

            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                DataRow dr = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                decimal xbal = !Convert.IsDBNull(dr["BAL_AMOUNT"]) ? Convert.ToDecimal(dr["BAL_AMOUNT"]) : Convert.ToDecimal("00");

                try
                {
                    config.Insert("DIVIDEND_LEDGER", new Dictionary<String, object>()
                    {
                        { "BRANCH_ID",   model.branch },
                        { "MEMBER_ID",   mm.mem_id },
                        { "VCH_DATE",    Convert.ToDateTime(model.post_dt +" "+Convert.ToString(DateTime.Now.ToShortTimeString()))},
                        { "VCH_NO",      "DIVCR"+Convert.ToDateTime(model.fr_dt).Year.ToString().Substring(2,2)+"-"+Convert.ToDateTime(model.to_dt).Year.ToString().Substring(2,2)},
                        { "VCH_SRL",     vchsrl  },
                        { "VCH_TYPE",    "T" },
                        { "vch_achd",    "DIVPAY" },
                        { "DR_CR",       "C" },
                        { "div_post_year_to",  Convert.ToDateTime(model.to_dt).Year },
                        { "TR_AMOUNT",  xtot_dividend },
                        { "BAL_AMOUNT",    xbal+Convert.ToDecimal(xtot_dividend)},
                        { "INSERT_MODE", "SI"},

                    });
                }
                catch (Exception ex)
                {

                }
            }

            string msg = "Saved Successfully";
            return (msg);
        }

        public List<DIVIDEND_LEDGER> getdetails(string member_id, string branch)
        {
            List<DIVIDEND_LEDGER> lml = new List<DIVIDEND_LEDGER>();
            string sql = string.Empty;

            sql = "SELECT * FROM DIVIDEND_LEDGER WHERE BRANCH_ID='" + branch + "' AND MEMBER_ID='" + member_id + "' ORDER BY VCH_DATE,VCH_NO,VCH_SRL";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    DIVIDEND_LEDGER dl = new DIVIDEND_LEDGER();
                    dl.VCH_DATE = Convert.ToDateTime(dr["VCH_DATE"]);
                    dl.BAL_AMOUNT = !Convert.IsDBNull(dr["BAL_AMOUNT"]) ? Convert.ToDecimal(dr["BAL_AMOUNT"]) : Convert.ToDecimal("00");
                    dl.TR_AMOUNT = !Convert.IsDBNull(dr["TR_AMOUNT"]) ? Convert.ToDecimal(dr["TR_AMOUNT"]) : Convert.ToDecimal("00");
                    dl.VCH_NO = dr["VCH_NO"].ToString();
                    dl.DIV_POST_YEAR_TO = Convert.ToInt32(dr["DIV_POST_YEAR_TO"]);

                    lml.Add(dl);
                }
            }


            return lml;
        }
    }
}