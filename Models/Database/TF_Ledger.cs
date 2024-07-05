using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Amritnagar.Includes;
using Amritnagar.Models.ViewModel;

namespace Amritnagar.Models.Database
{
    public class TF_Ledger
    {
        SQLConfig config = new SQLConfig();
        public string branch_id { get; set; }
        public string member_id { get; set; }
        public DateTime vch_date { get; set; }
        public DateTime eff_date { get; set; }
        public string vch_no { get; set; }
        public decimal vch_srl { get; set; }
        public string vch_type { get; set; }
        public string vch_achd { get; set; }
        public string dr_cr { get; set; }
        public decimal prin_amount { get; set; }
        public decimal prin_bal { get; set; }
        public decimal int_amount { get; set; }
        public decimal int_bal { get; set; }
        public decimal tf_rate { get; set; }
        public string ref_ac_hd { get; set; }
        public string ref_pacno { get; set; }
        public string ref_oth { get; set; }
        public string insert_mode { get; set; }
        public string book_no { get; set; }
        public List<TF_Ledger> getgetGFandTFDetail(string AcHd, string branch, string fr_dt, string mem_id)
        {

            string sql;
            string acc = "";
            List<TF_Ledger> tflst = new List<TF_Ledger>();
            if (AcHd == "TF")
            {
                acc = "TF_LEDGER";
            }
            else if (AcHd == "GF")
            {
                acc = "GF_LEDGER";
            }
            sql = "SELECT * FROM " + acc + " WHERE BRANCH_ID='" + branch + "' and ";
            sql = sql + "member_id='" + mem_id + "' order by vch_date,VCH_NO,VCH_SRL";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    TF_Ledger tfl = new TF_Ledger();
                    tfl.prin_amount = !Convert.IsDBNull(dr["prin_amount"]) ? Convert.ToDecimal(dr["prin_amount"]) : Convert.ToDecimal("00");
                    tfl.prin_bal = Convert.ToDecimal(dr["prin_bal"]);
                    //  tfl.chq_no = dr["chq_no"].ToString();
                    tfl.vch_date = Convert.ToDateTime(dr["vch_date"]);
                    tfl.insert_mode = dr["insert_mode"].ToString();

                    tflst.Add(tfl);
                }

            }

            return tflst;




        }

        public string SaveInLedger(Member_Mast mm, MemDepositeFundIntPaySchViewModel model, decimal clos_prin, double xtot_int)
        {
            string sql = string.Empty;
            string acc = "";
            if (model.ac_hd == "TF")
            {
                acc = "TF_LEDGER";
            }
            else if (model.ac_hd == "GF")
            {
                acc = "GF_LEDGER";
            }
            sql = "SELECT * FROM " + acc + " WHERE BRANCH_ID='" + model.branch + "' and ";
            sql = sql + "member_id='" + mm.mem_id + "' order by vch_date,VCH_NO,VCH_SRL";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                try
                {
                    config.Insert(acc, new Dictionary<String, object>()
                    {
                        { "BRANCH_ID",   model.branch },
                        { "MEMBER_ID",   mm.mem_id },
                        { "VCH_DATE",    Convert.ToDateTime(model.post_dt +" "+Convert.ToString(DateTime.Now.ToShortTimeString()))},
                        { "VCH_NO",      "SI"+model.ac_hd+"01" },
                        { "VCH_SRL",     "1"},
                        { "VCH_TYPE",    "T" },
                        { "DR_CR",       "C" },
                        { "INT_AMOUNT",  xtot_int },
                        { "PRIN_BAL",    Convert.ToDecimal(clos_prin.ToString("0.00"))},
                        { "INSERT_MODE", "SI"},
                        { "vch_achd",    ("INTP"+model.ac_hd).Substring(0,6) },
                    });
                }
                catch (Exception ex)
                {

                }
            }

            string msg = "Saved Successfully";
            return (msg);
        }

        public List<TF_Ledger> getdataByledgerTab(string gl_achd, string branch, string mem_no)
        {

            string sql;
            string acc = "";
            decimal xbal = 0;
            List<TF_Ledger> tflst = new List<TF_Ledger>();
            if (gl_achd == "TF")
            {
                acc = "TF_LEDGER";
            }
            else if (gl_achd == "GF")
            {
                acc = "GF_LEDGER";
            }
            else if (gl_achd == "SH")
            {
                acc = "SHARE_LEDGER";
            }
            sql = "SELECT * FROM " + acc + " WHERE BRANCH_ID='" + branch + "' AND MEMBER_ID='" + mem_no + "' ORDER BY VCH_DATE,VCH_NO,VCH_SRL";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    TF_Ledger tfl = new TF_Ledger();
                    tfl.dr_cr = dr["dr_cr"].ToString();
                    tfl.vch_type = dr["VCH_TYPE"].ToString();
                    tfl.vch_no = dr["vch_no"].ToString();
                    tfl.vch_achd = dr["vch_achd"].ToString();
                    tfl.vch_date = Convert.ToDateTime(dr["vch_date"]);
                    tfl.vch_srl = Convert.ToInt32(dr["vch_srl"]);

                    if (gl_achd == "SH")
                    {
                        tfl.prin_amount = !Convert.IsDBNull(dr["TR_AMOUNT"]) ? Convert.ToDecimal(dr["TR_AMOUNT"]) : Convert.ToDecimal("00");
                        tfl.prin_bal = !Convert.IsDBNull(dr["BAL_AMOUNT"]) ? Convert.ToDecimal(dr["BAL_AMOUNT"]) : Convert.ToDecimal("00");
                    }
                    else
                    {
                        tfl.prin_amount = !Convert.IsDBNull(dr["prin_amount"]) ? Convert.ToDecimal(dr["prin_amount"]) : Convert.ToDecimal("00");
                        tfl.int_amount = !Convert.IsDBNull(dr["INT_AMOUNT"]) ? Convert.ToDecimal(dr["INT_AMOUNT"]) : Convert.ToDecimal("00");
                        tfl.prin_bal = !Convert.IsDBNull(dr["prin_bal"]) ? Convert.ToDecimal(dr["prin_bal"]) : Convert.ToDecimal("00");
                        tfl.int_bal = !Convert.IsDBNull(dr["int_bal"]) ? Convert.ToDecimal(dr["int_bal"]) : Convert.ToDecimal("00");
                    }
                    tfl.ref_ac_hd = Convert.ToString(dr["ref_ac_hd"]);
                    tfl.ref_pacno = Convert.ToString(dr["ref_pacno"]);

                    tfl.insert_mode = dr["insert_mode"].ToString();
                    tflst.Add(tfl);
                }
            }
            return tflst;
        }

        public List<TF_Ledger> getTfRate()
        {
            List<TF_Ledger> tflst = new List<TF_Ledger>();
            string sql;
            sql = "SELECT * FROM TF_RATE";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    TF_Ledger tfl = new TF_Ledger();
                    tfl.eff_date = Convert.ToDateTime(dr["EFF_DATE"]);
                    tfl.tf_rate = Convert.ToDecimal(dr["TF_RATE"]);
                    tflst.Add(tfl);
                }
            }
            return tflst;




        }

        public string UpdateGFandTFLedger(Tf_Gf_LedgerViewModel model)
        {
            string sql = string.Empty;
            string qry = "";
            string acc = "";
            string msg = "";
            if (model.achd == "TF")
            {
                acc = "TF_LEDGER";
            }
            else if (model.achd == "GF")
            {
                acc = "GF_LEDGER";
            }
            else if (model.achd == "SH")
            {
                acc = "SHARE_LEDGER";
            }
            sql = "Select * from " + acc + " where branch_id='" + model.branch + "' and MEMBER_ID='" + model.mem_no + "' order by vch_date,vch_no,vch_srl";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                try
                {
                    if (model.achd == "SH")
                    {
                         qry = "Update " + acc + " set VCH_DATE = convert(datetime, '" + model.vch_date + "', 103), VCH_TYPE='" + model.vch_type + "',vch_no='" + model.vch_no + "',";
                        qry = qry + "vch_srl=" + model.srl + ",DR_CR='" + model.drcr + "',TR_AMOUNT=" + model.prin_amt + ",INSERT_MODE='" + model.ins_md + "'";
                        qry = qry + ",BRANCH_ID='" + model.branch + "',VCH_ACHD='" + model.vch_achd + "' ";
                        qry = qry + "where convert(datetime, VCH_DATE, 103) = convert(datetime, '" + model.con_vch_date + "', 103) and vch_no='" + model.con_vch_no + "' " +
                            "and VCH_TYPE='" + model.con_vch_type + "' and vch_srl='" + model.con_srl + "' and DR_CR='" + model.con_drcr + "' and member_id='" + model.mem_no + "'";
                    }
                    else
                    {
                         qry = "Update " + acc + " set VCH_DATE = convert(datetime, '" + model.vch_date + "', 103), VCH_TYPE='" + model.vch_type + "',vch_no='" + model.vch_no + "',";
                        qry = qry + "vch_srl=" + model.srl + ",DR_CR='" + model.drcr + "',prin_amount=" + model.prin_amt + ",INT_AMOUNT=" + model.int_amt + ",INSERT_MODE='" + model.ins_md + "'";
                        qry = qry + ",BRANCH_ID='" + model.branch + "',VCH_ACHD='" + model.vch_achd + "' ";
                        qry = qry + "where convert(datetime, VCH_DATE, 103) = convert(datetime, '" + model.con_vch_date + "', 103) and vch_no='" + model.con_vch_no + "' " +
                            "and VCH_TYPE='" + model.con_vch_type + "' and vch_srl='" + model.con_srl + "' and DR_CR='" + model.con_drcr + "' and member_id='" + model.mem_no + "'";
                    }
                    config.Execute_Query(qry);
                    msg = "Saved Successfully";
                }
                catch (Exception ex)
                {
                    msg = "err " + ex;
                }
            }
            if (model.achd == "SH")
            {
                ResetBalAmtForshare(acc, model.mem_no, model.con_vch_date);
            }
            else {
                ResetPrinBalIntBalForLedgerModification(acc, model.mem_no, model.con_vch_date);

            }


            return (msg);
        }
        public void ResetPrinBalIntBalForLedgerModification(string xledtab, string xacnoOrContno, string dt)
        {

            TF_Ledger tfl = new TF_Ledger();
            decimal lbal_prin = 0;
            decimal lbal_int = 0;
            decimal Tr_prin = 0;
            decimal Tr_int = 0;
            int unit = 0;
            string sql = "";


            sql = "SELECT * FROM " + xledtab + " WHERE Member_id='" + xacnoOrContno + "'  and convert(datetime, VCH_DATE, 103) < convert(datetime, '" + dt + "', 103) ORDER BY VCH_DATE,VCH_NO,VCH_SRL";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                DataRow ldr = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                lbal_prin = !Convert.IsDBNull(ldr["prin_bal"]) ? Convert.ToDecimal(ldr["prin_bal"]) : Convert.ToDecimal(00);
               // lbal_int = !Convert.IsDBNull(ldr["INT_bal"]) ? Convert.ToDecimal(ldr["INT_bal"]) : Convert.ToDecimal(00);
            }

            sql = "SELECT * FROM " + xledtab + " WHERE Member_id='" + xacnoOrContno + "' and convert(datetime, VCH_DATE, 103) >= convert(datetime, '" + dt + "', 103) ORDER BY VCH_DATE,VCH_NO,VCH_SRL";
            config.singleResult(sql);

            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    tfl.dr_cr = Convert.ToString(dr["dr_cr"]);
                    if (tfl.dr_cr == "D")
                    {
                        lbal_prin = lbal_prin - (!Convert.IsDBNull(dr["prin_amount"]) ? Convert.ToDecimal(dr["prin_amount"]) : Convert.ToDecimal(00));
                       // lbal_int = lbal_int - (!Convert.IsDBNull(dr["INT_AMOUNT"]) ? Convert.ToDecimal(dr["INT_AMOUNT"]) : Convert.ToDecimal(00));
                    }
                    else
                    {

                        //lbal_int = lbal_int + (!Convert.IsDBNull(dr["INT_AMOUNT"]) ? Convert.ToDecimal(dr["INT_AMOUNT"]) : Convert.ToDecimal(00));
                       lbal_prin = lbal_prin + (!Convert.IsDBNull(dr["prin_amount"]) ? Convert.ToDecimal(dr["prin_amount"]) : Convert.ToDecimal(00));

                    }

                    tfl.vch_date = Convert.ToDateTime(dr["vch_date"]);
                    tfl.vch_no = Convert.ToString(dr["vch_no"]);
                    tfl.vch_srl = Convert.ToInt32(dr["vch_srl"]);
                    string qry = "";

                    qry = "Update " + xledtab + " set prin_bal=" + lbal_prin + " where convert(datetime, VCH_DATE, 103) = convert(datetime, '" + tfl.vch_date + "', 103) and vch_no='" + tfl.vch_no + "' and vch_srl=" + tfl.vch_srl + " and Member_id='" + xacnoOrContno + "'";

                    config.Execute_Query(qry);
                }
            }
        }

        public void ResetBalAmtForshare(string xledtab, string xacnoOrContno, string dt)
        {
            TF_Ledger tfl = new TF_Ledger();
            decimal lbal_prin = 0;
            decimal lbal_int = 0;
            decimal Tr_prin = 0;
            decimal Tr_int = 0;
            int unit = 0;
            string sql = "";
            sql = "SELECT * FROM " + xledtab + " WHERE Member_id='" + xacnoOrContno + "'  and convert(datetime, VCH_DATE, 103) < convert(datetime, '" + dt + "', 103) ORDER BY VCH_DATE,VCH_NO,VCH_SRL";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                DataRow ldr = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                Tr_int = !Convert.IsDBNull(ldr["BAL_AMOUNT"]) ? Convert.ToDecimal(ldr["BAL_AMOUNT"]) : Convert.ToDecimal(00);
                // lbal_int = !Convert.IsDBNull(ldr["int_bal"]) ? Convert.ToDecimal(ldr["int_bal"]) : Convert.ToDecimal(00);
            }
            sql = "SELECT * FROM " + xledtab + " WHERE Member_id='" + xacnoOrContno + "' and convert(datetime, VCH_DATE, 103) >= convert(datetime, '" + dt + "', 103) ORDER BY VCH_DATE,VCH_NO,VCH_SRL";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    tfl.dr_cr = Convert.ToString(dr["Dr_cr"]);
                    unit = Convert.ToInt32(dr["TR_AMOUNT"]) / 10;
                    if (tfl.dr_cr == "D")
                    {
                        Tr_int = Tr_int - Convert.ToDecimal(dr["TR_AMOUNT"]);
                        // lbal_int = lbal_int + (!Convert.IsDBNull(dr["INT_AMOUNT"]) ? Convert.ToDecimal(dr["INT_AMOUNT"]) : Convert.ToDecimal(00));
                    }
                    else
                    {
                        Tr_int = Tr_int + Convert.ToDecimal(dr["TR_AMOUNT"]);
                        // lba
                    }
                    tfl.vch_date = Convert.ToDateTime(dr["vch_date"]);
                    tfl.vch_no = Convert.ToString(dr["vch_no"]);
                    tfl.vch_srl = Convert.ToInt32(dr["vch_srl"]);
                    string qry = "";
                    qry = "Update " + xledtab + " set units=" + unit + ",BAL_AMOUNT=" + Tr_int + " where convert(datetime, VCH_DATE, 103) = convert(datetime, '" + tfl.vch_date + "', 103) and vch_no='" + tfl.vch_no + "' and vch_srl=" + tfl.vch_srl + " and Member_id='" + xacnoOrContno + "'";
                    config.Execute_Query(qry);

                }
            }
        }
    }
}