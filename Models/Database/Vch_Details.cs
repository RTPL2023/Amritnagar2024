using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Amritnagar.Includes;

namespace Amritnagar.Models.Database
{
    public class Vch_Details
    {
        SQLConfig config = new SQLConfig();
        public string branch_id { get; set; }
        public string vch_date { get; set; }
        public string vch_no { get; set; }
        public decimal vch_srl { get; set; }
        public string vch_drcr { get; set; }
        public string ac_hd { get; set; }
        public string vch_pacno { get; set; }
        public string vch_acname { get; set; }
        public decimal vch_amt { get; set; }
        public string ref_ac_hd { get; set; }
        public string ref_pacno { get; set; }
        public string ref_oth { get; set; }
        public string insert_mode { get; set; }

        public List<Vch_Details> put_vch_detail(string branch, string Voucher_date, string Voucher_No)
        {
            string sql;
            sql = "select * from vch_detail where BRANCH_ID='" + branch + "' AND convert(varchar, VCH_DATE, 103) = convert(varchar, '" + Voucher_date + "', 103) and insert_mode='D' and vch_no='" + Voucher_No + "' order by vch_srl";
            config.singleResult(sql);
            List<Vch_Details> vdlist = new List<Vch_Details>();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    Vch_Details vd = new Vch_Details();
                    vd.vch_srl = Convert.ToInt32(dr["vch_srl"]);
                    vd.vch_drcr = dr["vch_drcr"].ToString();
                    vd.ac_hd = dr["ac_hd"].ToString();
                    vd.vch_pacno = dr["vch_pacno"].ToString();
                    vd.vch_acname = dr["vch_acname"].ToString();
                    vd.vch_amt = Convert.ToDecimal(dr["vch_amt"]);
                    vd.ref_ac_hd = dr["ref_ac_hd"].ToString();
                    vd.ref_pacno = dr["ref_pacno"].ToString();
                    vd.ref_oth = dr["ref_oth"].ToString();
                    // vd.vch_date = Convert.ToDateTime(dr["vch_date"]);
                    vdlist.Add(vd);
                }
            }
            return vdlist;
        }
    }
}