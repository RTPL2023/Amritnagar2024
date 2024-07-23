using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Amritnagar.Includes;
namespace Amritnagar.Models.Database
{
    public class Vch_header
    {
        SQLConfig config = new SQLConfig();
        public string branch_id { get; set; }
        public string vch_date { get; set; }
        public string vch_no { get; set; }
        public string vch_type { get; set; }
        public string vch_narr { get; set; }
        public string insert_mode { get; set; }
        public string value { get; set; }
        public string text { get; set; }

        public List<Vch_header> VchNo(string branch, string vch_date)
        {
            List<Vch_header> vhlist = new List<Vch_header>();
            string sql = "select * from vch_header where BRANCH_ID='"+ branch + "' AND convert(varchar, vch_date, 103) = '" + vch_date + "' and insert_mode='D' order by branch_id,vch_date,vch_no";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                int i = 0;
                foreach (DataRow dr in config.dt.Rows)
                {
                    if (i == 0)
                    {
                        Vch_header vh1 = new Vch_header();
                        vh1.value = Convert.ToString("Select Voucher");
                        vh1.text = Convert.ToString("Select Voucher");
                        vhlist.Add(vh1);
                    }
                    else
                    {
                        Vch_header vh = new Vch_header();
                        vh.value = dr["vch_no"].ToString();
                        vh.text = dr["vch_no"].ToString();
                        vhlist.Add(vh);
                    }                  
                    i = i + 1;
                }
            }
            else
            {
                Vch_header vh1 = new Vch_header();
                vh1.value = Convert.ToString("Select Voucher");
                vh1.text = Convert.ToString("Select Voucher");
                vhlist.Add(vh1);
            }
            return vhlist;
        }
        public void SaveUpdateVoucherHeader(string vchdt, string vchno, string vchtype, string vchnarr, string branch_id)
        {
            string voucher_type = string.Empty;
            if (vchtype == "Cash")
            {
                voucher_type = "C";
            }
            if (vchtype == "Transfer")
            {
                voucher_type = "T";
            }
            string sql = "select * from VCH_HEADER where BRANCH_ID = '"+ branch_id + "' AND convert(varchar, vch_date, 103) = '" + vchdt.Replace("-", "/") + "' and insert_mode = 'D' and vch_no='" + vchno + "' order by branch_id,vch_date,vch_no";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                sql = "Delete from vch_header where convert(varchar, vch_date, 103) = '" + vchdt.Replace("-", "/") + "' and insert_mode = 'D' and vch_no='" + vchno + "'";
                config.Execute_Query(sql);
                config.Insert("vch_header", new Dictionary<String, object>()
                {
                    {"branch_id",   branch_id },
                    {"vch_no", vchno },
                    {"vch_narr",    vchnarr},
                    {"vch_date",   vchdt},
                    {"insert_mode","D" },
                    {"vch_type",voucher_type}                    
                });               
            }
            else
            {
                config.Insert("vch_header", new Dictionary<String, object>()
                {
                    {"branch_id",   branch_id },
                    {"vch_no", vchno },
                    {"vch_narr",    vchnarr},
                    {"vch_date",   vchdt},
                    {"insert_mode","D" },
                    {"vch_type",voucher_type}
                });
            }
        }
    }
}