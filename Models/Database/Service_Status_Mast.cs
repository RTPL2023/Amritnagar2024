using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Amritnagar.Includes;

namespace Amritnagar.Models.Database
{
    public class Service_Status_Mast
    {
        SQLConfig config = new SQLConfig();

        public string serv_status { get; set; }
        public string serv_status_desc { get; set; }

        public List<Service_Status_Mast> getServiceStatusMast()
        {
            string sql = "Select * from  SERV_STATUS_MAST";
            config.singleResult(sql);
            List<Service_Status_Mast> ssml = new List<Service_Status_Mast>();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    Service_Status_Mast ssm = new Service_Status_Mast();
                    ssm.serv_status = Convert.ToString(dr["SERV_STATUS"]);
                    ssm.serv_status_desc = Convert.ToString(dr["SERV_STATUS_DESC"]);
                    ssml.Add(ssm);
                }
            }
            return ssml;
        }
    }
}