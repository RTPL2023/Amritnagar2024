using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Amritnagar.Includes;

namespace Amritnagar.Models.Database
{
    public class Licence
    {
        SQLConfig config = new SQLConfig();
        public string lic_shname { get; set; }
        public string lic_name { get; set; }
        public string lic_add1 { get; set; }
        public string lic_add2 { get; set; }
        public string lic_phone { get; set; }
        public string chiksum { get; set; }


        public Licence getlicencedetails()
        {
            Licence lc = new Licence();
            string sql = "Select * from LICENSEE";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    lc.lic_shname = Convert.ToString(dr["LIC_SHNAME"]);
                    lc.lic_name = Convert.ToString(dr["LIC_NAME"]);
                    lc.lic_add1 = Convert.ToString(dr["LIC_ADD1"]);
                    lc.lic_add2 = Convert.ToString(dr["LIC_ADD2"]);
                    lc.lic_phone = Convert.ToString(dr["LIC_PHONE"]);
                    lc.chiksum = Convert.ToString(dr["CHKSUM"]);                   
                }
            }
            return lc;
        }
    }
}