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
        public string vch_type { get; set; }

        public List<Vch_Details> put_vch_detail(string branch, string Voucher_date, string Voucher_No)
        {
            string sql;
            sql = "select vd.*, vh.VCH_TYPE from vch_detail vd, VCH_HEADER vh where vd.vch_no = vh.vch_no AND vd.BRANCH_ID='" + branch + "' AND convert(varchar, vd.VCH_DATE, 103) = convert(varchar, '" + Voucher_date + "', 103) and vd.insert_mode='D' and vd.vch_no='" + Voucher_No + "' order by vd.vch_srl";
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
                    vd.vch_type = dr["VCH_TYPE"].ToString();
                    // vd.vch_date = Convert.ToDateTime(dr["vch_date"]);                  
                    vdlist.Add(vd);
                }
            }
            return vdlist;
        }
        public void Check_DeleteVchDetail(String vch_date, String txtvch_No, string branchid)
        {
            Vch_Details vd = new Vch_Details();
            string sql = "select * from vch_detail where BRANCH_ID='" + branchid + "' AND convert(varchar, vch_date, 103) = '" + vch_date.Replace("-", "/") + "' and vch_no='" + txtvch_No + "' order by branch_id,vch_date,vch_no,vch_srl";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    int i = 0;
                    vd.ac_hd = Convert.ToString(dr["ac_hd"]);
                    vd.vch_pacno = Convert.ToString(dr["vch_pacno"]);
                    vd.vch_srl = Convert.ToDecimal(dr["vch_srl"]);
                    Ledger ld = new Ledger();
                    ld = ld.GET_GEN_LEDGER(vd.ac_hd, vd.vch_pacno, vch_date, branchid, Convert.ToInt32(vch_srl));
                    if (ld.query != "")
                    {
                        ld.Delete_LEDGER(vd.ac_hd, vd.vch_pacno, vch_date, Convert.ToInt32(vd.vch_srl), ld.query, ld.table, txtvch_No, branchid);
                    }
                }
                sql = "Delete from vch_detail where BRANCH_ID = '"+ branchid + "' AND convert(varchar, vch_date, 103) = '" + vch_date.Replace("-", "/") + "' and vch_no = '" + txtvch_No + "'";
                config.Execute_Query(sql);
            }
        }
        public void SaveUpdateVchDetail(string vch_date, string txtvch_No, string branch_id, string vch_type)
        {
            Temp_Vch_Entry tve = new Temp_Vch_Entry();
            List<Temp_Vch_Entry> tvel = new List<Temp_Vch_Entry>();
            tvel = tve.GetTempVchDataByVchdate(vch_date, txtvch_No);
            foreach (var a in tvel)
            {
                Vch_Details vd = new Vch_Details();
                vd.branch_id = branch_id;
                vd.vch_date = vch_date;
                vd.vch_no = txtvch_No;
                vd.vch_type = vch_type;
                vd.vch_srl = a.srl;                               
                vd.vch_drcr = a.drcr;
                vd.ac_hd = a.ac_hd;
                vd.vch_pacno = a.vch_pacno;
                vd.vch_acname = a.paid_to_rcv_frm;
                vd.vch_amt = Convert.ToDecimal(a.amount);
                vd.ref_ac_hd = a.ref_achd;
                vd.ref_pacno = a.ref_acno;
                vd.ref_oth = a.ref_ac_particulars;              
                vd.insert_mode = "D";                
                config.Insert("vch_detail", new Dictionary<String, object>()
                {
                    {"branch_id",   vd.branch_id },
                    { "vch_date",   Convert.ToDateTime(vd.vch_date) },
                    { "vch_no", vd.vch_no },
                    { "vch_srl",    vd.vch_srl },
                    { "VCH_DRCR",   vd.vch_drcr },
                    { "ac_hd",  vd.ac_hd },
                    { "vch_pacno",  vd.vch_pacno },
                    { "vch_acname", vd.vch_acname },
                    { "vch_amt",    vd.vch_amt },
                    {"ref_ac_hd",   vd.ref_ac_hd},
                    { "ref_pacno",  vd.ref_pacno},
                    { "ref_oth",    vd.ref_oth},
                    { "insert_mode",    vd.insert_mode },                    
                });
                Ledger ld = new Ledger();
                ld.AddLedger(vd);
            }
        }
    }
}