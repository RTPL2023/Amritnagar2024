﻿using System;
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
        public DateTime eff_date{ get; set; }
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

        public string SaveInLedger(Member_Mast mm, MemDepositeFundIntPaySchViewModel model,decimal clos_prin , double xtot_int)
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

        public List<TF_Ledger> getdataByledgerTab(MemDepositeFundLedgerStmntViewModel model)
        {

            string sql;
            string acc = "";
            decimal xbal = 0;
            List<TF_Ledger> tflst = new List<TF_Ledger>();
            if (model.gl_achd == "TF")
            {
                acc = "TF_LEDGER";
            }
            else if (model.gl_achd == "GF")
            {
                acc = "GF_LEDGER";
            }
            sql = "SELECT * FROM " +acc+ " WHERE BRANCH_ID='" +model.branch+ "' AND MEMBER_ID='" +model.mem_no+ "' ORDER BY VCH_DATE,VCH_NO,VCH_SRL";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    TF_Ledger tfl = new TF_Ledger();
                    tfl.dr_cr = dr["dr_cr"].ToString();
                    tfl.vch_type = dr["VCH_TYPE"].ToString();
                    tfl.prin_amount = !Convert.IsDBNull(dr["prin_amount"]) ? Convert.ToDecimal(dr["prin_amount"]) : Convert.ToDecimal("00");
                    tfl.int_amount = !Convert.IsDBNull(dr["INT_AMOUNT"]) ? Convert.ToDecimal(dr["INT_AMOUNT"]) : Convert.ToDecimal("00");
                    tfl.prin_bal = !Convert.IsDBNull(dr["prin_bal"]) ? Convert.ToDecimal(dr["prin_bal"]) : Convert.ToDecimal("00");
                    tfl.int_bal = !Convert.IsDBNull(dr["int_bal"]) ? Convert.ToDecimal(dr["int_bal"]) : Convert.ToDecimal("00");
                    tfl.ref_ac_hd = Convert.ToString(dr["ref_ac_hd"]);
                    tfl.ref_pacno = Convert.ToString(dr["ref_pacno"]);
                    tfl.prin_bal = Convert.ToDecimal(dr["prin_bal"]);
                    //  tfl.chq_no = dr["chq_no"].ToString();
                    tfl.vch_date = Convert.ToDateTime(dr["vch_date"]);
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
                    tfl.tf_rate =Convert.ToDecimal(dr["TF_RATE"]);
                    tflst.Add(tfl);
                }
            }
            return tflst;




        }
    }
}