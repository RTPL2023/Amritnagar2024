using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Amritnagar.Includes;


namespace Amritnagar.Models.Database
{
    public class LnPurpose_Mast
    {
        SQLConfig config = new SQLConfig();
        public string ln_purpose { get; set; }
        public string purpose_desc { get; set; }

        public List<LnPurpose_Mast> getAllLoanPurposeList()
        {
            string sql = "Select * from LNPURPOSE_MAST order by LN_PURPOSE";
            config.singleResult(sql);
            List<LnPurpose_Mast> lpml = new List<LnPurpose_Mast>();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    LnPurpose_Mast lpm = new LnPurpose_Mast();
                    lpm.ln_purpose = Convert.ToString(dr["LN_PURPOSE"]);
                    lpm.purpose_desc = Convert.ToString(dr["PURPOSE_DESC"]);
                    lpml.Add(lpm);
                }
            }
            return lpml;
        }
        public string CheckAndSaveLoanPurpose(LnPurpose_Mast lpm)
        {
            string sql = "Select * from LNPURPOSE_MAST where LN_PURPOSE='" + lpm.ln_purpose + "'";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                config.Update("LNPURPOSE_MAST", new Dictionary<String, object>()
                    {
                    { "PURPOSE_DESC",   lpm.purpose_desc },
                }, new Dictionary<string, object>()
                {
                    { "LN_PURPOSE",     lpm.ln_purpose },
                });
            }
            else
            {
                config.Insert("LNPURPOSE_MAST", new Dictionary<string, object>()
                {
                    { "PURPOSE_DESC",     lpm.purpose_desc },
                    { "LN_PURPOSE",   lpm.ln_purpose },
                });
            }
            string msg = "Saved Successfully";
            return (msg);
        }
        public void DeleteLoanPurpose(string ln_purpose)
        {
            LnPurpose_Mast lpm = new LnPurpose_Mast();
            string sql = "Delete from LNPURPOSE_MAST where LN_PURPOSE= '" + ln_purpose + "'";
            config.Execute_Query(sql);
        }
    }
}