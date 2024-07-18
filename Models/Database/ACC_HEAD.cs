using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Amritnagar.Includes;

namespace Amritnagar.Models.Database
{
    public class ACC_HEAD
    {
        SQLConfig config = new SQLConfig();
        public string ac_hd { get; set; }
        public string ac_majgr { get; set; }
        public string ac_subgr { get; set; }
        public string ac_desc { get; set; }
        public string cb_flag { get; set; }
        public string ledger_tab { get; set; }
        public string led_achd { get; set; }
        public string ifcharge { get; set; }
        public string ac_lf_mast_fl { get; set; }
        public string is_clearing { get; set; }
        public string ledger_col { get; set; }
        public string acc_prefix { get; set; }
        public string is_miscdep { get; set; }
        public string is_miscpay { get; set; }
        public decimal miscdep_baseamt { get; set; }
        public string invest_flag { get; set; }
        public string ex_trial { get; set; }
        public string is_member_fund { get; set; }
        public string is_salary_deduction { get; set; }
        public string is_contra { get; set; }

        public List<ACC_HEAD> getglac_hd()
        {
            string sql = "Select * from  ACC_HEAD where ac_hd='GF' or ac_hd='LICP' or ac_hd='RTB'or AC_HD='TF'or AC_HD='SH'";
            config.singleResult(sql);
            List<ACC_HEAD> aclst = new List<ACC_HEAD>();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    ACC_HEAD ac = new ACC_HEAD();
                    ac.ac_hd = Convert.ToString(dr["AC_HD"]);
                    ac.ac_desc = Convert.ToString(dr["AC_DESC"]);
                    aclst.Add(ac);
                }
            }
            return aclst;
        }

        public List<ACC_HEAD> getglachd()
        {
            string sql = "  Select * from  ACC_HEAD where ac_hd='ISH' or ac_hd='SH'";
            config.singleResult(sql);
            List<ACC_HEAD> aclst = new List<ACC_HEAD>();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    ACC_HEAD ac = new ACC_HEAD();
                    ac.ac_hd = Convert.ToString(dr["AC_HD"]);
                    ac.ac_desc = Convert.ToString(dr["AC_DESC"]);
                    aclst.Add(ac);
                }
            }
            return aclst;
        }
    }
}