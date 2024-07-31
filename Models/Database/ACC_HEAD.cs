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
        public string ac_parti { get; set; }
        public string particulars { get; set; }
        public string clos_flag { get; set; }

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
        public List<ACC_HEAD> getAchdListForVchEntry(string vch_achd)
        {
            List<ACC_HEAD> aclist = new List<ACC_HEAD>();
            string sql = "SELECT a.AC_HD,a.AC_DESC AS AC_PARTI FROM ACC_HEAD a where a.AC_HD LIKE '" + vch_achd.ToUpper() + "%' ORDER BY a.AC_HD";
            config.singleResult(sql);
            int i = 1;
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    if (i == 1)
                    {
                        ACC_HEAD ah1 = new ACC_HEAD();
                        ah1.ac_desc = "Select Achd";
                        ah1.ac_hd = "";
                        ah1.ac_parti = "Select Achd";
                        aclist.Add(ah1);
                    }
                    ACC_HEAD ah = new ACC_HEAD();
                    //ah.ac_desc = dr["ac_desc"].ToString();
                    ah.ac_hd = dr["ac_hd"].ToString();
                    ah.ac_parti = ah.ac_hd + " - " + dr["AC_PARTI"].ToString();
                    aclist.Add(ah);
                    i = i + 1;
                }
            }
            else aclist = null;
            return aclist;
        }
        public ACC_HEAD Getparticular(string achd, string acno, string branch_id)
        {
            ACC_HEAD ah = new ACC_HEAD();
            string sql;
            sql = "select * from ACC_HEAD WHERE AC_HD = '" + achd + "'";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                DataRow dr = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                ah.ac_lf_mast_fl = dr["AC_LF_MAST_FL"].ToString();
                ah.led_achd = dr["LED_ACHD"].ToString();
            }
            if (ah.ac_lf_mast_fl == "D")
            {
                sql = "SELECT AC_NAME AS NAME_OUT,AC_CLOSED AS CLOS_FLG FROM DEPOSIT_MAST WHERE BRANCH_ID='" + branch_id + "' AND ";
                sql = sql + "AC_HD='" + ah.led_achd + "' AND AC_NO='" + acno + "'";
            }
            if (ah.ac_lf_mast_fl == "L")
            {
                sql = "SELECT loanee_name AS NAME_OUT,CLOS_FLAG AS CLOS_FLG FROM loan_master WHERE BRANCH_ID='" + branch_id + "' AND ";
                sql = sql + "AC_HD='" + ah.led_achd + "' AND employee_ID='" + acno + "'";
            }
            if (ah.ac_lf_mast_fl == "M")
            {
                sql = "SELECT MEMBER_NAME AS NAME_OUT,MEMBER_CLOSED AS CLOS_FLG FROM MEMBER_MAST WHERE BRANCH_ID='" + branch_id + "' AND ";
                sql = sql + "MEMBER_ID='" + acno + "'";
            }
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                DataRow dr1 = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                ah.particulars = Convert.ToString(dr1["NAME_OUT"]);
                ah.clos_flag = Convert.ToString(dr1["CLOS_FLG"]);
            }
            return ah;
        }
        public List<ACC_HEAD> getac_hhdName(string name)
        {
            string sql = "";
            sql = "select * from acc_head where ac_hd LIKE '" + name + "%'";
            List<ACC_HEAD> acclst = new List<ACC_HEAD>();
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    ACC_HEAD ah = new ACC_HEAD();
                    ah.ac_hd = Convert.ToString(dr["ac_hd"]);
                    acclst.Add(ah);
                }
            }
            return acclst;
        }

        public string getac_hddesc(string ac_hd)
        {
            string sql = "";
            sql = "select * from acc_head where ac_hd ='" + ac_hd + "'";
            ACC_HEAD ah = new ACC_HEAD();
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {

                    ah.ac_desc = Convert.ToString(dr["AC_DESC"]);

                }
            }
            if (ac_hd.ToUpper() == "ALL")
            {
                ah.ac_desc = "All Account Head";
            }
            return ah.ac_desc;
        }
    }
}