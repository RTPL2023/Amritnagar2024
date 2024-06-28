using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Amritnagar.Includes;

namespace Amritnagar.Models.Database
{
    public class LnStatus_Mast
    {
        SQLConfig config = new SQLConfig();
        public string status_cd { get; set; }
        public string status_desc { get; set; }
        public string sec_provision { get; set; }
        public string unsec_provision { get; set; }
        public string od_month_upto { get; set; }
        public string status_snm { get; set; }
        public string status_mgr { get; set; }

        public List<LnStatus_Mast> getLoanStatusMast()
        {
            string sql = "Select * from  LNSTATUS_MAST";
            config.singleResult(sql);
            List<LnStatus_Mast> lsml = new List<LnStatus_Mast>();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    LnStatus_Mast lsm = new LnStatus_Mast();
                    lsm.status_cd = Convert.ToString(dr["STATUS_CD"]);
                    lsm.status_desc = Convert.ToString(dr["STATUS_DESC"]);
                    lsml.Add(lsm);
                }
            }
            return lsml;
        }

    }
}