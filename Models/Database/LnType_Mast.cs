using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Amritnagar.Includes;


namespace Amritnagar.Models.Database
{
    public class LnType_Mast
    {
        SQLConfig config = new SQLConfig();
        public string ac_hd { get; set; }
        public string lntype_desc { get; set; }
        public string loan_type { get; set; }
        public string member_reqd { get; set; }
        public string sec_flag { get; set; }
        public string is_stndrd { get; set; }
        public string pledge_reqd { get; set; }
        public string is_adjustable { get; set; }
        public string is_staff_loan { get; set; }
        public string perc_share { get; set; }
        public string int_ac_hd { get; set; }
        public string is_addint { get; set; }
        public string aint_ac_hd { get; set; }
        public string intr_ac_hd { get; set; }
        public string aintr_ac_hd { get; set; }
        public string ch_ac_hd { get; set; }
        public string is_rebate { get; set; }
        public string int_scheme_cd { get; set; }
        public string aint_scheme_cd { get; set; }
        public string rebate_scheme_cd { get; set; }
        public string repay_scheme_cd { get; set; }
        public string notice_scheme_cd { get; set; }
        public string letter_scheme_cd { get; set; }
        public string lndoc_scheme_cd { get; set; }
        public string int_rate_cd { get; set; }
        public string aint_rate_cd { get; set; }
        public string is_lessint_spcl { get; set; }
        public string max_surity { get; set; }
        public string updt_rt { get; set; }


        public List<LnType_Mast> getLoanTypeMast()
        {
            string sql = "Select * from  LNTYPE_MAST";
            config.singleResult(sql);
            List<LnType_Mast> ltml = new List<LnType_Mast>();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    LnType_Mast ltm = new LnType_Mast();
                    ltm.ac_hd = Convert.ToString(dr["AC_HD"]);
                    ltm.lntype_desc = Convert.ToString(dr["LNTYPE_DESC"]);
                    ltml.Add(ltm);
                }
            }
            return ltml;
        }
    }
}