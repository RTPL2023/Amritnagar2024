using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Amritnagar.Includes;

namespace Amritnagar.Models.Database
{
    public class Vch_Type
    {
        SQLConfig config = new SQLConfig();
        public string vch_type_cd { get; set; }
        public string vch_type_des { get; set; }
        public string value { get; set; }
        public string text { get; set; }

        public List<Vch_Type> VchType()
        {
            List<Vch_Type> vhlist = new List<Vch_Type>();
            string sql = "select * from VCH_TYPE";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                int i = 0;
                foreach (DataRow dr in config.dt.Rows)
                {
                    if (i == 0)
                    {
                        Vch_Type vh1 = new Vch_Type();
                        vh1.value = Convert.ToString("Select Voucher Type");
                        vh1.text = Convert.ToString("Select Voucher Type");
                        vhlist.Add(vh1);
                    }                  
                    Vch_Type vh = new Vch_Type();
                    vh.value = dr["VCH_TYPE_CD"].ToString();
                    vh.text = dr["VCH_TYPE_DES"].ToString();
                    vhlist.Add(vh);                 
                    i = i + 1;
                }
            }
            else
            {
                Vch_Type vh1 = new Vch_Type();
                vh1.value = Convert.ToString("Select Voucher");
                vh1.text = Convert.ToString("Select Voucher");
                vhlist.Add(vh1);
            }
            return vhlist;
        }
    }
}