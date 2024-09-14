using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Amritnagar.Includes;

namespace Amritnagar.Models.Database
{
    public class Temp_Vch_Entry
    {
        SQLConfig config = new SQLConfig();
        public String drcr { get; set; }
        public DateTime vch_dt { get; set; }
        public string str_vchdt { get; set; }
        public Int32 srl { get; set; }
        public string ac_hd { get; set; }
        public string vch_pacno { get; set; }
        public String paid_to_rcv_frm { get; set; }
        public decimal amount { get; set; }
        public String ref_achd { get; set; }
        public String ref_acno { get; set; }
        public String ref_ac_particulars { get; set; }
        public String created_by { get; set; }
        public DateTime created_on { get; set; }
        public String computer_name { get; set; }
        public String modified_by { get; set; }
        public DateTime modified_on { get; set; }
        public String m_computer_name { get; set; }
        public String vch_no { get; set; }

        public int GetLastSerialNoBydate(string dt, string vchno)
        {
            Temp_Vch_Entry tve = new Temp_Vch_Entry();           
            string sql = "SELECT * FROM temp_vch_entry WHERE convert(datetime, VCH_DT, 103) = convert(datetime, '" + dt.Replace("-", "/") + "', 103) and vch_no='" + vchno + "' order by srl";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                DataRow dr = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                tve.srl = Convert.ToInt32(dr["srl"]) + 1;
            }
            else
            {
                tve.srl = 1;
            }
            return tve.srl;
        }
        public void SaveTempVchData(Temp_Vch_Entry tve)
        {
            //config.Insert("temp_vch_entry", new Dictionary<String, object>()
            //{
            //        {"srl",     tve.srl },
            //        { "drcr",   tve.drcr },
            //        { "ac_hd",  tve.ac_hd },
            //        { "vch_dt", tve.vch_dt},
            //        { "vch_pacno",  tve.vch_pacno },
            //        { "vch_no",     tve.vch_no },
            //        { "paid_to_rcv_frm",    tve.paid_to_rcv_frm },
            //        { "amount", tve.amount },
            //        { "ref_achd",   tve.ref_achd },
            //        { "ref_acno",   tve.ref_acno },
            //        { "ref_ac_particulars", tve.ref_ac_particulars },
            //        { "created_by", tve.created_by },
            //        { "created_on", tve.created_on},
            //        { "computer_name",  tve.computer_name }
            //});

            string qry = "Insert into temp_vch_entry (srl,drcr,ac_hd,vch_dt,vch_pacno,vch_no,paid_to_rcv_frm,amount,ref_achd,ref_acno,ref_ac_particulars,created_by,created_on,computer_name) values('" + Convert.ToInt32(tve.srl) + "',";      
            qry = qry + "'" + Convert.ToString(tve.drcr) + "','" + Convert.ToString(tve.ac_hd) + "'," + "convert(datetime, '" + tve.str_vchdt + "', 103),'" + Convert.ToString(tve.vch_pacno) + "','" + Convert.ToString(tve.vch_no) + "',";
            qry = qry + "'" + Convert.ToString(tve.paid_to_rcv_frm) + "','" + Convert.ToDecimal(tve.amount) + "','" + Convert.ToString(tve.ref_achd) + "','" + Convert.ToString(tve.ref_acno) + "','" + Convert.ToString(tve.ref_ac_particulars) + "','" + Convert.ToString(tve.created_by) + "',convert(datetime, '" + tve.created_on + "', 103)" + ",'" + Convert.ToString(tve.computer_name) +"')";
            config.Execute_Query(qry);
        }
        public List<Temp_Vch_Entry> GetTempVchDataByVchdate(string dt, string vchno)
        {
            List<Temp_Vch_Entry> tvel = new List<Temp_Vch_Entry>();
            string sql = "SELECT * FROM temp_vch_entry WHERE convert(datetime, VCH_DT, 103) = convert(datetime, '" + dt.Replace("-", "/") + "', 103) and vch_no='" + vchno + "' order by srl";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    Temp_Vch_Entry tve = new Temp_Vch_Entry();
                    tve.srl = Convert.ToInt32(dr["srl"]);
                    tve.drcr = Convert.ToString(dr["drcr"]);
                    tve.ac_hd = Convert.ToString(dr["ac_hd"]);
                    tve.vch_pacno = Convert.ToString(dr["vch_pacno"]);
                    tve.paid_to_rcv_frm = Convert.ToString(dr["paid_to_rcv_frm"]);
                    tve.amount = !Convert.IsDBNull(dr["amount"]) ? Convert.ToDecimal(dr["amount"]) : Convert.ToDecimal("00");
                    tve.ref_achd = Convert.ToString(dr["ref_achd"]);
                    tve.ref_acno = Convert.ToString(dr["ref_acno"]);
                    tve.ref_ac_particulars = Convert.ToString(dr["ref_ac_particulars"]);
                    tve.created_by = Convert.ToString(dr["created_by"]);
                    tve.created_on = Convert.ToDateTime(dr["created_on"]);
                    tve.computer_name = Convert.ToString(dr["computer_name"]);
                    tvel.Add(tve);
                }
            }
            return tvel;
        }
        public void DeleteTempData(string dt, string vchno)
        {
            string sql = "delete from temp_vch_entry where convert(datetime, Vch_Dt, 103) = convert(datetime, '" + dt.Replace("-", "/") + "', 103) and vch_no='" + vchno + "'";
            config.Execute_Query(sql);
        }
        public void DeleteTempDatabyvchno(string vchno)
        {
            string sql = "delete from temp_vch_entry where vch_no='" + vchno + "'";
            config.Execute_Query(sql);
        }
        public void UpdateTempVchData(Temp_Vch_Entry tve)
        {            
            string qry = string.Empty;        
            qry = "delete from temp_vch_entry where vch_no='" + tve.vch_no + "' and srl = '"+ tve.srl +"'";
            config.Execute_Query(qry);
            qry = "Insert into temp_vch_entry (srl,drcr,ac_hd,vch_dt,vch_pacno,vch_no,paid_to_rcv_frm,amount,ref_achd,ref_acno,ref_ac_particulars,created_by,created_on,computer_name) values('" + Convert.ToInt32(tve.srl) + "',";
            qry = qry + "'" + Convert.ToString(tve.drcr) + "','" + Convert.ToString(tve.ac_hd) + "'," + "convert(datetime, '" + tve.str_vchdt + "', 103),'" + Convert.ToString(tve.vch_pacno) + "','" + Convert.ToString(tve.vch_no) + "',";
            qry = qry + "'" + Convert.ToString(tve.paid_to_rcv_frm) + "','" + Convert.ToDecimal(tve.amount) + "','" + Convert.ToString(tve.ref_achd) + "','" + Convert.ToString(tve.ref_acno) + "','" + Convert.ToString(tve.ref_ac_particulars) + "','" + Convert.ToString(tve.created_by) + "',convert(datetime, '" + tve.created_on + "', 103)" + ",'" + Convert.ToString(tve.computer_name) + "')";
            config.Execute_Query(qry);
        }
    }
}