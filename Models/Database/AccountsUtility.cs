using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Amritnagar.Includes;
using Amritnagar.Models.ViewModel;

namespace Amritnagar.Models.Database
{
    public class AccountsUtility
    {

        SQLConfig config = new SQLConfig();
        public string branch_id { get; set; }
        public string ac_hd { get; set; }
        public string ac_desc { get; set; }
        public string ac_majgr { get; set; }
        public string ac_majgrdesc { get; set; }
        public string ac_subgr { get; set; }
        public string ac_subgrdesc { get; set; }
        public string vch_no_dr { get; set; }
        public string vch_pacno_dr { get; set; }
        public string vch_acname_dr { get; set; }
        public decimal cash_dr { get; set; }
        public decimal bank_dr { get; set; }
        public decimal trans_dr { get; set; }
        public string vch_no_cr { get; set; }
        public string vch_pacno_cr { get; set; }
        public string vch_acname_cr { get; set; }
        public decimal cash_cr { get; set; }
        public decimal bank_cr { get; set; }
        public decimal trans_cr { get; set; }
        public string ac_majgr_cr { get; set; }
        public string ac_majgrdesc_cr { get; set; }
        public string ac_hd_cr { get; set; }
        public string ac_desc_cr { get; set; }

        public decimal total_cr { get; set; }
        public string ac_majgr_dr { get; set; }
        public string ac_majgrdesc_dr { get; set; }
        public string ac_hd_dr { get; set; }
        public string ac_desc_dr { get; set; }
        public string ac_subgr_cr { get; set; }
        public string ac_subgr_dr { get; set; }

        public decimal total_dr { get; set; }
        public decimal majgr_cash_cr { get; set; }
        public decimal majgr_trans_cr { get; set; }
        public decimal majgr_tot_cr { get; set; }
        public decimal majgr_cash_dr { get; set; }
        public decimal majgr_trans_dr { get; set; }
        public decimal majgr_tot_dr { get; set; }
        public DateTime gl_date { get; set; }
        public DateTime vch_date { get; set; }
        public string bnk_inv { get; set; }
        public decimal op_bal { get; set; }
        public decimal tot_cr { get; set; }
        public decimal tot_dr { get; set; }
        public decimal cl_bal { get; set; }

        public string vch_no { get; set; }
        public decimal vch_srl { get; set; }
        public string vch_drcr { get; set; }
        public string vch_pacno { get; set; }
        public string vch_acname { get; set; }
        public string vch_narr { get; set; }
        public decimal debit_amt { get; set; }
        public decimal credit_amt { get; set; }

        public string daybookSave(DayBookReportViewModel model)
        {
            string sql = string.Empty;
            sql = "delete FROM REP_ACC_DAYBOOK";
            config.Execute_Query(sql);
            string xacstr = "";
            int i = 1;
            if (model.ac_hd.ToUpper() == "ALL")
            {
                sql = "select * from acc_head";
                config.singleResult(sql);
                if (config.dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in config.dt.Rows)
                    {
                        if (i < config.dt.Rows.Count)
                        {
                            xacstr = xacstr + "'" + Convert.ToString(dr["ac_hd"]) + "',";
                        }
                        else
                        {
                            xacstr = xacstr + "'" + Convert.ToString(dr["ac_hd"]) + "'";
                        }
                        i++;
                    }
                }
            }
            else
            {
                xacstr = "'" + model.ac_hd + "'";
            }


            string acstr = "B.AC_HD IN (" + xacstr + ")";
            sql = "SELECT A.BRANCH_ID,A.VCH_DATE,B.AC_HD,B.VCH_NO,A.VCH_TYPE,B.VCH_SRL,B.VCH_DRCR,C.AC_MAJGR,C.AC_SUBGR,C.AC_DESC,";
            sql = sql + " B.VCH_PACNO,B.VCH_ACNAME,B.VCH_AMT FROM VCH_HEADER A,VCH_DETAIL B,ACC_HEAD C";
            sql = sql + " WHERE (A.BRANCH_ID=B.BRANCH_ID AND A.VCH_DATE=B.VCH_DATE AND";
            sql = sql + " A.VCH_NO=B.VCH_NO) AND B.AC_HD=C.AC_HD AND (";
            sql = sql + acstr + ") ";
            sql = sql + "AND  convert(varchar, A.VCH_DATE, 103) = convert(varchar, '" + model.daybook_dt + "', 103)";
            sql = sql + " AND A.BRANCH_ID='" + model.branch + "'";
            sql = sql + " ORDER BY B.AC_HD,A.VCH_DATE,B.VCH_NO,B.VCH_SRL";
            config.singleResult(sql);
            List<AccountsUtility> aulst = new List<AccountsUtility>();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    AccountsUtility au = new AccountsUtility();

                    if (dr["VCH_DRCR"].ToString() == "D")
                    {
                        au.ac_hd = Convert.ToString(dr["ac_hd"]);
                        au.ac_desc = Convert.ToString(dr["AC_DESC"]);
                        au.ac_majgr = Convert.ToString(dr["ac_majgr"]);
                        au.ac_majgrdesc = "";
                        au.ac_subgr = Convert.ToString(dr["AC_SUBGR"]);
                        au.ac_subgrdesc = "";
                        au.vch_no_dr = Convert.ToString(dr["vch_no"]);
                        au.vch_pacno_dr = Convert.ToString(dr["vch_pacno"]);
                        au.vch_acname_dr = Convert.ToString(dr["vch_acname"]);
                        if (Convert.ToString(dr["VCH_TYPE"]) == "C")
                            au.cash_dr = Convert.ToDecimal(dr["vch_amt"]);
                        if (Convert.ToString(dr["VCH_TYPE"]) == "B")
                            au.bank_dr = Convert.ToDecimal(dr["vch_amt"]);
                        if (Convert.ToString(dr["VCH_TYPE"]) == "T")
                            au.trans_dr = Convert.ToDecimal(dr["vch_amt"]);
                        if (Convert.ToString(dr["VCH_TYPE"]) == "J")
                            au.trans_dr = Convert.ToDecimal(dr["vch_amt"]);
                        au.saveinREP_ACC_DAYBOOK(au);
                    }
                    else
                    {
                        au.ac_hd = Convert.ToString(dr["ac_hd"]);
                        au.ac_desc = Convert.ToString(dr["AC_DESC"]);
                        au.ac_majgr = Convert.ToString(dr["ac_majgr"]);
                        au.ac_majgrdesc = "";
                        au.ac_subgr = Convert.ToString(dr["AC_SUBGR"]);
                        au.ac_subgrdesc = "";

                        au.vch_no_cr = Convert.ToString(dr["vch_no"]);
                        au.vch_pacno_cr = Convert.ToString(dr["vch_pacno"]);
                        au.vch_acname_cr = Convert.ToString(dr["vch_acname"]);
                        if (Convert.ToString(dr["VCH_TYPE"]) == "C")
                            au.cash_cr = Convert.ToDecimal(dr["vch_amt"]);
                        if (Convert.ToString(dr["VCH_TYPE"]) == "B")
                            au.bank_cr = Convert.ToDecimal(dr["vch_amt"]);
                        if (Convert.ToString(dr["VCH_TYPE"]) == "T")
                            au.trans_cr = Convert.ToDecimal(dr["vch_amt"]);
                        if (Convert.ToString(dr["VCH_TYPE"]) == "J")
                            au.trans_cr = Convert.ToDecimal(dr["vch_amt"]);
                        au.saveinREP_ACC_DAYBOOK(au);

                    }

                }

            }

            string msg = "Saved Successfully";
            return (msg);
        }


        public void saveinREP_ACC_DAYBOOK(AccountsUtility au)
        {
            try
            {
                config.Insert("REP_ACC_DAYBOOK", new Dictionary<String, object>()
                {
                        { "AC_HD",      au.ac_hd },
                        { "AC_DESC",      au.ac_desc },
                        { "AC_MAJGR",    au.ac_majgr },
                        { "AC_MAJGRDESC",      au.ac_majgrdesc },
                        { "AC_SUBGR",     au.ac_subgr },
                        { "AC_SUBGRDESC",        au.ac_subgrdesc},
                        { "VCH_NO_DR",    au.vch_no_dr },
                        { "VCH_PACNO_DR",      au.vch_pacno_dr },
                        { "VCH_ACNAME_DR",       au.vch_acname_dr },
                        { "CASH_DR",       au.cash_dr},
                        { "BANK_DR",       au.bank_dr},
                        { "TRANS_DR",      au.trans_dr },
                        { "VCH_NO_CR",      au.vch_no_cr },
                        { "VCH_PACNO_CR",      au.vch_pacno_cr },
                        { "VCH_ACNAME_CR",      au.vch_acname_cr },
                        { "CASH_CR",      au.cash_cr },
                        { "BANK_CR",      au.bank_cr },
                        { "TRANS_CR",      au.trans_cr },

                });
            }
            catch (Exception x)
            {

            }

        }
        public List<AccountsUtility> getdaybooklistbydaywise(DayBookReportViewModel model)
        {
            decimal LBAL = 0;
            decimal d = 0;
            decimal c = 0;
            decimal xgrptot = 0;
            int xstart = 0;
            string xpmgrp = "";
            string sql = "SELECT * FROM REP_ACC_DAYBOOK";

            config.singleResult(sql);


            List<AccountsUtility> aulst = new List<AccountsUtility>();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {

                    AccountsUtility au = new AccountsUtility();
                    au.ac_hd = Convert.ToString(dr["ac_hd"]);
                    au.ac_desc = Convert.ToString(dr["AC_DESC"]);
                    au.ac_majgr = Convert.ToString(dr["ac_majgr"]);
                    au.ac_majgrdesc = "";
                    au.ac_subgr = Convert.ToString(dr["AC_SUBGR"]);
                    au.ac_subgrdesc = "";
                    au.vch_no_dr = Convert.ToString(dr["vch_no_dr"]);
                    au.vch_pacno_dr = Convert.ToString(dr["vch_pacno_dr"]);
                    au.vch_acname_dr = Convert.ToString(dr["vch_acname_dr"]);
                    au.cash_dr = Convert.ToDecimal(dr["cash_dr"]);
                    au.bank_dr = Convert.ToDecimal(dr["bank_dr"]);
                    au.trans_dr = Convert.ToDecimal(dr["trans_dr"]);
                    au.trans_dr = Convert.ToDecimal(dr["trans_dr"]);
                    au.vch_no_cr = Convert.ToString(dr["vch_no_cr"]);
                    au.vch_pacno_cr = Convert.ToString(dr["vch_pacno_cr"]);
                    au.vch_acname_cr = Convert.ToString(dr["vch_acname_cr"]);
                    au.cash_cr = Convert.ToDecimal(dr["cash_cr"]);
                    au.bank_cr = Convert.ToDecimal(dr["bank_cr"]);
                    au.trans_cr = Convert.ToDecimal(dr["trans_cr"]);
                    au.trans_cr = Convert.ToDecimal(dr["trans_cr"]);
                    aulst.Add(au);
                }


            }
            return aulst;
        }
        public string CashAccountSave(CashAccountReportViewModel model)
        {
            decimal TOT_MGCASH_DR = 0;
            decimal TOT_MGTRAN_DR = 0;
            decimal TOT_MGTOT_DR = 0;
            decimal TOT_MGCASH_CR = 0;
            decimal TOT_MGTRAN_CR = 0;
            decimal TOT_MGTOT_CR = 0;
            decimal TOTAL_CR = 0;
            decimal TOTAL_DR = 0;
            string sql = string.Empty;
            sql = "delete FROM REP_ACC_CASHACC";
            config.Execute_Query(sql);
            int i = 1;
            sql = "SELECT c.ac_majgr, a.ac_hd,";
            sql = sql + "sum(iif(a.vch_drcr='C' and b.vch_type='C',vch_amt,0)) AS cash_cr,";
            sql = sql + "sum(iif(a.vch_drcr='C' and b.vch_type='T',vch_amt,0)) AS tran_cr,";
            sql = sql + "sum(iif(a.vch_drcr='D' and b.vch_type='C',vch_amt,0)) AS cash_dr,";
            sql = sql + "sum(iif(a.vch_drcr='D' and b.vch_type='T',vch_amt,0)) AS tran_dr ";
            sql = sql + "FROM vch_detail AS a, vch_header AS b, acc_head AS c ";
            sql = sql + "WHERE ((a.branch_id=b.branch_id and convert(date,a.VCH_DATE, 103)=convert(date,b.VCH_DATE, 103) and a.vch_no=b.vch_no) and ";
            sql = sql + "(a.ac_hd=c.ac_hd)) and  convert(date, a.VCH_DATE, 103)>=  convert(date, '" + model.fr_dt + "', 103) AND convert(date, a.VCH_DATE, 103)<= convert(date, '" + model.to_dt + "', 103)  ";
            sql = sql + "and b.vch_type in ('C','T') GROUP BY c.ac_majgr, a.ac_hd";
            config.singleResult(sql); //convert(varchar, A.VCH_DATE, 103) = convert(varchar, '" + model.daybook_dt + "', 103)
            List<AccountsUtility> aulst = new List<AccountsUtility>();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    AccountsUtility au = new AccountsUtility();
                    TOTAL_CR = 0;
                    TOTAL_DR = 0;
                    TOTAL_CR = Convert.ToDecimal(dr["CASH_CR"]) + Convert.ToDecimal(dr["TRAN_CR"]);
                    TOTAL_DR = Convert.ToDecimal(dr["CASH_DR"]) + Convert.ToDecimal(dr["TRAN_DR"]);
                    TOT_MGCASH_DR = TOT_MGCASH_DR + Convert.ToDecimal(dr["CASH_DR"]);
                    TOT_MGTRAN_DR = TOT_MGTRAN_DR + Convert.ToDecimal(dr["TRAN_DR"]);
                    TOT_MGTOT_DR = TOT_MGTOT_DR + TOTAL_DR;
                    TOT_MGCASH_CR = TOT_MGCASH_CR + Convert.ToDecimal(dr["CASH_CR"]);
                    TOT_MGTRAN_CR = TOT_MGTRAN_CR + Convert.ToDecimal(dr["TRAN_CR"]);
                    TOT_MGTOT_CR = TOT_MGTOT_CR + TOTAL_CR;
                    if (TOTAL_CR > 0)
                    {
                        au.ac_majgr_cr = Convert.ToString(dr["ac_majgr"]);
                        au.ac_majgrdesc_cr = get_majgr(Convert.ToString(dr["ac_majgr"]));
                        au.ac_hd_cr = Convert.ToString(dr["ac_hd"]);
                        au.ac_desc_cr = get_ac_desc(Convert.ToString(dr["ac_hd"]));
                        au.cash_cr = Convert.ToDecimal(dr["CASH_CR"]);
                        au.trans_cr = Convert.ToDecimal(dr["TRAN_CR"]);
                        au.total_cr = TOTAL_CR;
                    }
                    if (TOTAL_DR > 0)
                    {
                        au.ac_majgr_dr = Convert.ToString(dr["ac_majgr"]);
                        au.ac_majgrdesc_dr = get_majgr(Convert.ToString(dr["ac_majgr"]));
                        au.ac_hd_dr = Convert.ToString(dr["ac_hd"]);
                        au.ac_desc_dr = get_ac_desc(Convert.ToString(dr["ac_hd"]));
                        au.cash_dr = Convert.ToDecimal(dr["CASH_dr"]);
                        au.trans_dr = Convert.ToDecimal(dr["TRAN_dr"]);
                        au.total_dr = TOTAL_DR;
                    }
                    if (TOT_MGTOT_CR > 0)
                    {
                        au.majgr_cash_cr = TOT_MGCASH_CR;
                        au.majgr_trans_cr = TOT_MGTRAN_CR;
                        au.majgr_tot_cr = TOT_MGTOT_CR;
                    }
                    if (TOT_MGTOT_DR > 0)
                    {
                        au.majgr_cash_dr = TOT_MGCASH_DR;
                        au.majgr_trans_dr = TOT_MGTRAN_DR;
                        au.majgr_tot_dr = TOT_MGTOT_DR;
                    }
                    // XMGR = Convert.ToString(dr["ac_majgr"]);
                    saveinrep_acc_cashacc(au);
                    TOT_MGCASH_DR = 0;
                    TOT_MGTRAN_DR = 0;
                    TOT_MGTOT_DR = 0;
                    TOT_MGCASH_CR = 0;
                    TOT_MGTRAN_CR = 0;
                    TOT_MGTOT_CR = 0;
                }

            }

            string msg = "Saved Successfully";
            return (msg);
        }
        public string get_majgr(string ac_majgr)
        {
            string ac_majgrdesc = "";
            string sql = "select * from ACC_HEAD_MGR where ac_majgr='" + ac_majgr + "'";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                DataRow dr = (DataRow)config.dt.Rows[0];
                ac_majgrdesc = Convert.ToString(dr["ac_majgrdesc"]);
            }
            return ac_majgrdesc;
        }
        public string get_ac_desc(string ac_hd)
        {
            string ac_desc = "";
            string sql = "select * from Acc_head where AC_HD='" + ac_hd + "'";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                DataRow dr = (DataRow)config.dt.Rows[0];
                ac_desc = Convert.ToString(dr["AC_DESC"]);
            }
            return ac_desc;
        }
        public void saveinrep_acc_cashacc(AccountsUtility au)
        {
            try
            {
                config.Insert("rep_acc_cashacc", new Dictionary<String, object>()
                {
                      { "ac_majgr_cr ",au. ac_majgr_cr },
                      { "AC_MAJGRDESC_CR ",au. ac_majgrdesc_cr },
                      { "ac_hd_cr ", au.ac_hd_cr },
                      { "ac_desc_cr ", au. ac_desc_cr},
                      { "CASH_CR ", au.cash_cr },
                      { "TRANS_CR ",  au.trans_cr},
                      { "TOTAL_CR ", au.total_cr },
                      { "ac_majgr_dr ", au.ac_majgr_dr },
                      { "AC_MAJGRDESC_DR ",  au.ac_majgrdesc_dr},
                      { "ac_hd_dr ", au. ac_hd_dr},
                      { "ac_desc_dr ", au.ac_desc_dr },
                      { "CASH_DR ", au. cash_dr},
                      { "TRANS_DR ", au. trans_dr},
                      { "TOTAL_DR ", au.total_dr },
                      { "MAJGR_CASH_CR ", au.majgr_cash_cr },
                      { "MAJGR_TRANS_CR ", au. majgr_trans_cr},
                      { "MAJGR_TOT_CR ", au.majgr_tot_cr },
                      { "MAJGR_CASH_DR ", au.majgr_cash_dr },
                      { "MAJGR_TRANS_DR ",au. majgr_trans_dr },
                      { "MAJGR_TOT_DR ", au.majgr_tot_dr },
                });
            }
            catch (Exception x)
            {

            }

        }
        public List<AccountsUtility> getCashAccountlistbydaywise()
        {
            string sql = "SELECT * FROM rep_acc_cashacc";
            config.singleResult(sql);
            List<AccountsUtility> aulst = new List<AccountsUtility>();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    AccountsUtility au = new AccountsUtility();
                    au.ac_majgr_cr = Convert.ToString(dr["ac_majgr_cr"]);
                    au.ac_majgrdesc_cr = Convert.ToString(dr["ac_majgrdesc_cr"]);
                    au.ac_hd_cr = Convert.ToString(dr["ac_hd_cr"]);
                    au.ac_desc_cr = Convert.ToString(dr["ac_desc_cr"]);
                    au.cash_cr = Convert.ToDecimal(dr["CASH_CR"]);
                    au.trans_cr = Convert.ToDecimal(dr["trans_cr"]);
                    au.total_cr = Convert.ToDecimal(dr["total_cr"]);
                    au.ac_majgr_dr = Convert.ToString(dr["ac_majgr_dr"]);
                    au.ac_majgrdesc_dr = Convert.ToString(dr["ac_majgrdesc_dr"]);
                    au.ac_hd_dr = Convert.ToString(dr["ac_hd_dr"]);
                    au.ac_desc_dr = Convert.ToString(dr["ac_desc_dr"]);
                    au.cash_dr = Convert.ToDecimal(dr["cash_dr"]);
                    au.trans_dr = Convert.ToDecimal(dr["trans_dr"]);
                    au.total_dr = Convert.ToDecimal(dr["total_dr"]);
                    au.majgr_cash_cr = Convert.ToDecimal(dr["majgr_cash_cr"]);
                    au.majgr_trans_cr = Convert.ToDecimal(dr["majgr_trans_cr"]);
                    au.majgr_tot_cr = Convert.ToDecimal(dr["majgr_tot_cr"]);
                    au.majgr_cash_dr = Convert.ToDecimal(dr["majgr_cash_dr"]);
                    au.majgr_trans_dr = Convert.ToDecimal(dr["majgr_trans_dr"]);
                    au.majgr_tot_dr = Convert.ToDecimal(dr["majgr_tot_dr"]);
                    aulst.Add(au);
                }
            }
            return aulst;
        }
        public CashBankPositionReportViewModel getCashBankPositionReport(CashBankPositionReportViewModel model)
        {
            decimal GD_OPBAL = 0;
            decimal GD_CREDIT = 0;
            decimal GD_DEBIT = 0;
            decimal GD_CLBAL = 0;
            decimal GL_OPBAL = 0;
            decimal GL_CREDIT = 0;
            decimal GL_DEBIT = 0;
            decimal GL_CLBAL = 0;
            decimal XOPBAL = 0;
            decimal XDEBIT = 0;
            decimal XCREDIT = 0;
            decimal XCLBAL = 0;
            string sql = string.Empty;
            string XACHD = "";
            sql = "delete FROM BANK_INV_BALNCE WHERE BRANCH_ID = '" + model.branch + "' AND convert(varchar, GL_DATE, 103) = convert(varchar, '" + model.as_on_dt + "', 103)";
            config.Execute_Query(sql);
            int i = 1;
            sql = "SELECT AC_HD,AC_DESC,'B' AS BNK_INV FROM ACC_HEAD WHERE CB_FLAG='B' ORDER BY AC_DESC";
            config.singleResult(sql);
            List<AccountsUtility> aulst = new List<AccountsUtility>();
            model.table1 = "<tr><th> Account Head </th><th>Opening Balance</th><th>Receipts</th><th>Payments</th><th>ClosingBalance</th></tr>";
            model.table2 = "<tr><th> Account Head </th><th>Opening Balance</th><th>Receipts</th><th>Payments</th><th>ClosingBalance</th></tr>";
            model.table3 = "<tr><th> Account Head </th><th>Opening Balance</th><th>Receipts</th><th>Payments</th><th>ClosingBalance</th></tr>";
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    AccountsUtility au = new AccountsUtility();
                    XACHD = Convert.ToString(dr["ac_hd"]);
                    XOPBAL = 0;
                    XDEBIT = 0;
                    XCREDIT = 0;
                    XCLBAL = 0;
                    sql = "SELECT * FROM GL_BALNCE WHERE BRANCH_ID='" + model.branch + "' AND AC_HD='" + XACHD + "' AND convert(date, GL_DATE, 103) < convert(date, '" + model.as_on_dt + "', 103) ORDER BY GL_DATE";
                    config.singleResult(sql);
                    if (config.dt.Rows.Count > 0)
                    {
                        DataRow dr1 = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                        XOPBAL = !Convert.IsDBNull(dr1["gl_bal"]) ? Convert.ToDecimal(dr1["gl_bal"]) : Convert.ToDecimal("0");
                    }
                    string QRY1 = "SELECT SUM(IIF(VCH_DRCR='D',VCH_AMT,0)) AS TOT_DR,";
                    QRY1 = QRY1 + "SUM(IIF(VCH_DRCR='C',VCH_AMT,0)) AS TOT_CR FROM VCH_DETAIL WHERE ";
                    QRY1 = QRY1 + "BRANCH_ID='" + model.branch + "' AND AC_HD='" + XACHD + "' AND ";
                    QRY1 = QRY1 + "convert(varchar, VCH_DATE, 103) = convert(varchar, '" + model.as_on_dt + "', 103)";
                    config.singleResult(QRY1);
                    if (config.dt.Rows.Count > 0)
                    {
                        DataRow dr1 = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                        XDEBIT = !Convert.IsDBNull(dr1["tot_dr"]) ? Convert.ToDecimal(dr1["tot_dr"]) : Convert.ToDecimal("0");
                        XCREDIT = !Convert.IsDBNull(dr1["tot_cr"]) ? Convert.ToDecimal(dr1["tot_cr"]) : Convert.ToDecimal("0");
                    }
                    XCLBAL = XOPBAL + XCREDIT - XDEBIT;
                    if (XOPBAL != 0 || XCLBAL != 0 || XCREDIT != 0 || XDEBIT != 0)
                    {
                        model.table1 = model.table1 + "<tr><td>" + Convert.ToString(dr["AC_DESC"]) + "</td><td>" + Math.Abs(XOPBAL).ToString("0.00") + "</td><td>" + XCREDIT.ToString("0.00") + "</td><td>" + XDEBIT.ToString("0.00") + "</td><td>" + Math.Abs(XCLBAL).ToString("0.00") + "</td></tr>";
                        au.ac_hd = XACHD;
                        au.branch_id = model.branch;
                        au.gl_date = Convert.ToDateTime(model.as_on_dt);
                        au.bnk_inv = "B";
                        au.op_bal = XOPBAL;
                        au.tot_cr = XCREDIT;
                        au.tot_dr = XDEBIT;
                        au.cl_bal = XCLBAL;
                        saveinbank_inv_balnce(au);
                    }
                    GD_OPBAL = GD_OPBAL + XOPBAL;
                    GD_CREDIT = GD_CREDIT + XCREDIT;
                    GD_DEBIT = GD_DEBIT + XDEBIT;
                    GD_CLBAL = GD_CLBAL + XCLBAL;
                }
            }
            model.td_opbal = Math.Abs(GD_OPBAL).ToString("0.00");
            model.td_credit = GD_CREDIT.ToString("0.00");
            model.td_debit = GD_DEBIT.ToString("0.00");
            model.td_clbal = Math.Abs(GD_CLBAL).ToString("0.00");
            sql = "SELECT AC_HD,AC_DESC,'I' AS BNK_INV FROM ACC_HEAD WHERE AC_LF_MAST_FL='I' AND LEDGER_COL='P' ORDER BY AC_DESC";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    AccountsUtility au = new AccountsUtility();
                    XACHD = Convert.ToString(dr["ac_hd"]);
                    XOPBAL = 0;
                    XDEBIT = 0;
                    XCREDIT = 0;
                    XCLBAL = 0;
                    sql = "SELECT * FROM GL_BALNCE WHERE BRANCH_ID='" + model.branch + "' AND AC_HD='" + XACHD + "' AND convert(date, GL_DATE, 103) < convert(date, '" + model.as_on_dt + "', 103) ORDER BY GL_DATE";
                    config.singleResult(sql);
                    {
                        DataRow dr1 = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                        XOPBAL = !Convert.IsDBNull(dr1["gl_bal"]) ? Convert.ToDecimal(dr1["gl_bal"]) : Convert.ToDecimal("0");
                    }
                    string QRY1 = "SELECT SUM(IIF(VCH_DRCR='D',VCH_AMT,0)) AS TOT_DR,";
                    QRY1 = QRY1 + "SUM(IIF(VCH_DRCR='C',VCH_AMT,0)) AS TOT_CR FROM VCH_DETAIL WHERE ";
                    QRY1 = QRY1 + "BRANCH_ID='" + model.branch + "' AND AC_HD='" + XACHD + "' AND ";
                    QRY1 = QRY1 + "convert(varchar, VCH_DATE, 103) = convert(varchar, '" + model.as_on_dt + "', 103)";
                    config.singleResult(QRY1);
                    if (config.dt.Rows.Count > 0)
                    {
                        DataRow dr1 = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                        XDEBIT = !Convert.IsDBNull(dr1["tot_dr"]) ? Convert.ToDecimal(dr1["tot_dr"]) : Convert.ToDecimal("0");
                        XCREDIT = !Convert.IsDBNull(dr1["tot_cr"]) ? Convert.ToDecimal(dr1["tot_cr"]) : Convert.ToDecimal("0");
                    }
                    XCLBAL = XOPBAL + XCREDIT - XDEBIT;
                    if (XOPBAL != 0 || XCLBAL != 0 || XCREDIT != 0 || XDEBIT != 0)
                    {
                        model.table2 = model.table2 + "<tr><td>" + Convert.ToString(dr["AC_DESC"]) + "</td><td>" + Math.Abs(XOPBAL).ToString("0.00") + "</td><td>" + XCREDIT.ToString("0.00") + "</td><td>" + XDEBIT.ToString("0.00") + "</td><td>" + Math.Abs(XCLBAL).ToString("0.00") + "</td></tr>";
                        au.ac_hd = XACHD;
                        au.branch_id = model.branch;
                        au.gl_date = Convert.ToDateTime(model.as_on_dt);
                        au.bnk_inv = "I";
                        au.op_bal = XOPBAL;
                        au.tot_cr = XCREDIT;
                        au.tot_dr = XDEBIT;
                        au.cl_bal = XCLBAL;
                        saveinbank_inv_balnce(au);
                    }
                    GL_OPBAL = GL_OPBAL + XOPBAL;
                    GL_CREDIT = GL_CREDIT + XCREDIT;
                    GL_DEBIT = GL_DEBIT + XDEBIT;
                    GL_CLBAL = GL_CLBAL + XCLBAL;
                }
            }
            model.tl_opbal = Math.Abs(GL_OPBAL).ToString("0.00");
            model.tl_credit = GL_CREDIT.ToString("0.00");
            model.tl_debit = GL_DEBIT.ToString("0.00");
            model.tl_clbal = Math.Abs(GL_CLBAL).ToString("0.00");
            //---------Populate Cash Balance----------------
            XACHD = "CASH";
            XOPBAL = 0;
            XDEBIT = 0;
            XCREDIT = 0;
            XCLBAL = 0;
            sql = "SELECT * FROM GL_BALNCE WHERE BRANCH_ID='" + model.branch + "' AND AC_HD='" + XACHD + "' AND convert(date, GL_DATE, 103) < convert(date, '" + model.as_on_dt + "', 103) ORDER BY GL_DATE";
            config.singleResult(sql);
            {
                DataRow dr1 = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                XOPBAL = !Convert.IsDBNull(dr1["gl_bal"]) ? Convert.ToDecimal(dr1["gl_bal"]) : Convert.ToDecimal("0");
            }
            sql = "SELECT SUM(IIF(A.VCH_DRCR='D',A.VCH_AMT,0)) AS TOT_CR,";
            sql = sql + "SUM(IIF(A.VCH_DRCR='C',A.VCH_AMT,0)) AS TOT_DR FROM VCH_DETAIL A,VCH_HEADER B WHERE ";
            sql = sql + "(A.BRANCH_ID=B.BRANCH_ID AND convert(varchar,A.VCH_DATE, 103)=convert(varchar,B.VCH_DATE, 103) AND A.VCH_NO=B.VCH_NO) AND ";
            sql = sql + "B.BRANCH_ID='" + model.branch + "' AND B.VCH_TYPE='C' AND ";
            sql = sql + "convert(varchar,B.VCH_DATE, 103) = convert(varchar, '" + model.as_on_dt + "', 103)";
            config.singleResult(sql);

            if (config.dt.Rows.Count > 0)
            {
                DataRow dr1 = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                XDEBIT = !Convert.IsDBNull(dr1["tot_dr"]) ? Convert.ToDecimal(dr1["tot_dr"]) : Convert.ToDecimal("0");
                XCREDIT = !Convert.IsDBNull(dr1["tot_cr"]) ? Convert.ToDecimal(dr1["tot_cr"]) : Convert.ToDecimal("0");
            }
            XCLBAL = XOPBAL + XCREDIT - XDEBIT;
            model.table3 = model.table3 + "<tr><td>CASH IN HAND</td><td>" + Math.Abs(XOPBAL).ToString("0.00") + "</td><td>" + XCREDIT.ToString("0.00") + "</td><td>" + XDEBIT.ToString("0.00") + "</td><td>" + Math.Abs(XCLBAL).ToString("0.00") + "</td></tr>";
            AccountsUtility au1 = new AccountsUtility();
            au1.ac_hd = XACHD;
            au1.branch_id = model.branch;
            au1.gl_date = Convert.ToDateTime(model.as_on_dt);
            au1.bnk_inv = "C";
            au1.op_bal = XOPBAL;
            au1.tot_cr = XCREDIT;
            au1.tot_dr = XDEBIT;
            au1.cl_bal = XCLBAL;
            saveinbank_inv_balnce(au1);
            return model;
        }
        public void saveinbank_inv_balnce(AccountsUtility au)
        {
            try
            {
                config.Insert("BANK_INV_BALNCE", new Dictionary<String, object>()
                {
                    { "BRANCH_ID ",au. branch_id },
                    { "ac_hd ",au. ac_hd },
                    { "GL_DATE ", au.gl_date },
                    { "BNK_INV ", au. bnk_inv},
                    { "op_bal ", au.op_bal },
                    { "tot_cr ",  au.tot_cr},
                    { "tot_dr ", au.tot_dr },
                    { "CL_BAL ", au.cl_bal },
                });
            }
            catch (Exception x)
            {

            }
        }
        public List<AccountsUtility> PopulateCashbook(CashBookReportViewModel model)
        {
            string sql = string.Empty;
            decimal cash_opbal = 0;
            decimal cash_closbal = 0;
            decimal totcash_dr = 0;
            decimal totcash_cr = 0;
            string xacstr = "";
            string qry_vc = "";
            string acstr = "";
            string pac = "";
            decimal cntdr = 0;
            decimal cntcr = 0;
            decimal cnttot = 0;
            decimal cr_cash = 0;
            decimal cr_bank = 0;
            decimal cr_trans = 0;
            decimal cr_total = 0;
            decimal dr_cash = 0;
            decimal dr_bank = 0;
            decimal dr_trans = 0;
            decimal dr_total = 0;
            decimal cont_cr = 0;
            decimal cont_dr = 0;
            string xmgroup = "";
            string xsgroup = "";
            string xpart = "";
            string tac = "";
            sql = "delete FROM rep_acc_cashbook";
            config.Execute_Query(sql);

            int i = 1;
            if (model.ac_hd.ToUpper() == "ALL")
            {
                sql = "select * from acc_head";
                config.singleResult(sql);
                if (config.dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in config.dt.Rows)
                    {
                        if (i < config.dt.Rows.Count)
                        {
                            xacstr = xacstr + "'" + Convert.ToString(dr["ac_hd"]) + "',";
                        }
                        else
                        {
                            xacstr = xacstr + "'" + Convert.ToString(dr["ac_hd"]) + "'";
                        }
                        i++;
                    }
                }
            }
            else
            {
                xacstr = "'" + model.ac_hd + "'";
            }
            acstr = "B.AC_HD IN (" + xacstr + ")";
            sql = "SELECT A.BRANCH_ID,A.VCH_DATE,B.AC_HD,B.VCH_NO,A.VCH_TYPE,B.VCH_SRL,B.VCH_DRCR,C.AC_MAJGR,C.AC_SUBGR,C.AC_DESC,C.IS_CONTRA,";
            sql = sql + " B.VCH_PACNO,B.VCH_ACNAME,B.VCH_AMT FROM VCH_HEADER A,VCH_DETAIL B,ACC_HEAD C";
            sql = sql + " WHERE (A.BRANCH_ID=B.BRANCH_ID AND convert(datetime, A.VCH_DATE, 103)=convert(datetime, B.VCH_DATE,103) AND";
            sql = sql + " A.VCH_NO=B.VCH_NO) AND B.AC_HD=C.AC_HD AND (";
            sql = sql + acstr + ") ";
            sql = sql + "AND convert(varchar, A.VCH_DATE, 103) = convert(varchar, '" + model.fr_dt + "', 103) AND";
            sql = sql + " A.BRANCH_ID='" + model.branch + "' AND A.VCH_TYPE IN ('C','B','T') ";
            sql = sql + " ORDER BY B.AC_HD,A.VCH_DATE,B.VCH_NO,B.VCH_SRL";
            config.singleResult(sql);
            List<AccountsUtility> aulst = new List<AccountsUtility>();
            if (config.dt.Rows.Count > 0)
            {
                //DataRow dr1 = (DataRow)config.dt.Rows[0];
                //pac = Convert.ToString(dr1["ac_hd"]);
                //xmgroup = Convert.ToString(dr1["ac_majgr"]);
                //xsgroup = Convert.ToString(dr1["AC_SUBGR"]);
                //xpart = Convert.ToString(dr1["AC_DESC"]);
                foreach (DataRow dr in config.dt.Rows)
                {
                    pac = Convert.ToString(dr["ac_hd"]);
                    xmgroup = Convert.ToString(dr["ac_majgr"]);
                    xsgroup = Convert.ToString(dr["AC_SUBGR"]);
                    xpart = Convert.ToString(dr["AC_DESC"]);
                    AccountsUtility au = new AccountsUtility();
                    if (Convert.ToString(dr["VCH_DRCR"]) == "C")
                    {
                        if (Convert.ToString(dr["VCH_TYPE"]) == "C")
                        {
                            cr_cash = cr_cash + Convert.ToDecimal(dr["vch_amt"]);
                            totcash_cr = totcash_cr + Convert.ToDecimal(dr["vch_amt"]);
                        }
                        if (Convert.ToString(dr["VCH_TYPE"]) == "B")
                        {
                            cr_bank = cr_bank + Convert.ToDecimal(dr["vch_amt"]);
                        }
                        if (Convert.ToString(dr["VCH_TYPE"]) == "T")
                        {
                            cr_trans = cr_trans + Convert.ToDecimal(dr["vch_amt"]);
                        }
                        if (Convert.ToString(dr["IS_CONTRA"]) == "Y")
                        {
                            cont_dr = cont_dr + Convert.ToDecimal(dr["vch_amt"]);
                        }
                        cr_total = cr_total + Convert.ToDecimal(dr["vch_amt"]);
                    }
                    else
                    {
                        if (Convert.ToString(dr["VCH_TYPE"]) == "C")
                        {
                            dr_cash = dr_cash + Convert.ToDecimal(dr["vch_amt"]);
                            totcash_dr = totcash_dr + Convert.ToDecimal(dr["vch_amt"]);
                        }
                        if (Convert.ToString(dr["VCH_TYPE"]) == "B")
                        {
                            dr_bank = dr_bank + Convert.ToDecimal(dr["vch_amt"]);
                        }
                        if (Convert.ToString(dr["VCH_TYPE"]) == "T")
                        {
                            dr_trans = dr_trans + Convert.ToDecimal(dr["vch_amt"]);
                        }
                        if (Convert.ToString(dr["IS_CONTRA"]) == "Y")
                        {
                            cont_cr = cont_cr + Convert.ToDecimal(dr["vch_amt"]);
                        }
                        dr_total = dr_total + Convert.ToDecimal(dr["vch_amt"]);
                    }
                    if (cr_total > 0)
                    {
                        au.ac_hd_cr = pac;
                        au.ac_desc_cr = xpart;
                        au.ac_majgr_cr = xmgroup;
                        au.ac_subgr_cr = xsgroup;
                        au.cash_cr = cr_cash;
                        au.bank_cr = cr_bank;
                        au.trans_cr = cr_trans;
                        au.tot_cr = cr_total;
                    }
                    if (dr_total > 0)
                    {
                        au.ac_hd_dr = pac;
                        au.ac_desc_dr = xpart;
                        au.ac_majgr_dr = xmgroup;
                        au.ac_subgr_dr = xsgroup;
                        au.cash_dr = dr_cash;
                        au.bank_dr = dr_bank;
                        au.trans_dr = dr_trans;
                        au.tot_dr = dr_total;
                    }
                    if (cont_dr > 0)
                    {
                        au.ac_hd_dr = pac;
                        au.ac_desc_dr = xpart + " # CONTRA #";
                        au.ac_majgr_dr = xmgroup;
                        au.ac_subgr_dr = xsgroup;
                        au.cash_dr = 0;
                        au.bank_dr = cont_dr;
                        au.trans_dr = 0;
                        au.tot_dr = cont_dr;
                    }
                    if (cont_cr > 0)
                    {
                        au.ac_hd_cr = pac;
                        au.ac_desc_cr = xpart + " # CONTRA #";
                        au.ac_majgr_cr = xmgroup;
                        au.ac_subgr_cr = xsgroup;
                        au.cash_cr = 0;
                        au.bank_cr = cont_cr;
                        au.trans_cr = 0;
                        au.tot_cr = cont_cr;
                    }
                    saveinrep_acc_cashbook(au);
                    cr_total = 0;
                    dr_total = 0;
                    cont_dr = 0;
                    cont_cr = 0;
                    cr_trans = 0;
                    cr_bank = 0;
                    cr_cash = 0;
                    dr_bank = 0;
                    dr_cash = 0;
                    dr_trans = 0;
                }
            }
            sql = "Select * from rep_acc_cashbook";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    AccountsUtility au = new AccountsUtility();
                    au.ac_hd_dr = Convert.ToString(dr["ac_hd_dr"]);
                    au.ac_desc_dr = Convert.ToString(dr["ac_desc_dr"]);
                    au.ac_majgr_dr = Convert.ToString(dr["ac_majgr_dr"]);
                    au.ac_subgr_dr = Convert.ToString(dr["ac_subgr_dr"]);
                    au.cash_dr = Convert.ToDecimal(dr["cash_dr"]);
                    au.bank_dr = Convert.ToDecimal(dr["bank_dr"]);
                    au.trans_dr = Convert.ToDecimal(dr["trans_dr"]);
                    au.tot_dr = Convert.ToDecimal(dr["tot_dr"]);
                    au.ac_hd_cr = Convert.ToString(dr["ac_hd_cr"]);
                    au.ac_desc_cr = Convert.ToString(dr["ac_desc_cr"]);
                    au.ac_majgr_cr = Convert.ToString(dr["ac_majgr_cr"]);
                    au.ac_subgr_cr = Convert.ToString(dr["ac_subgr_cr"]);
                    au.cash_cr = Convert.ToDecimal(dr["cash_cr"]);
                    au.bank_cr = Convert.ToDecimal(dr["bank_cr"]);
                    au.trans_cr = Convert.ToDecimal(dr["trans_cr"]);
                    au.tot_cr = Convert.ToDecimal(dr["tot_cr"]);
                    aulst.Add(au);
                }
            }
            return (aulst);
        }
        public void saveinrep_acc_cashbook(AccountsUtility au)
        {
            try
            {
                config.Insert("rep_acc_cashbook", new Dictionary<String, object>()
                {
                    { "ac_hd_dr ",au. ac_hd_dr },
                    { "ac_desc_dr ",au. ac_desc_dr },
                    { "ac_majgr_dr ", au.ac_majgr_dr },
                    { "ac_subgr_dr ", au. ac_subgr_dr},
                    { "CASH_DR ", au.cash_dr },
                    { "BANK_DR ",  au.bank_dr},
                    { "TRANS_DR ", au.trans_dr },
                    { "tot_dr ", au.tot_dr },
                    { "ac_hd_cr ", au.ac_hd_cr },
                    { "ac_desc_cr ", au.ac_desc_cr },
                    { "ac_majgr_cr ", au.ac_majgr_cr },
                    { "ac_subgr_cr ", au.ac_subgr_cr },
                    { "CASH_CR ", au.cash_cr },
                    { "BANK_CR ", au.bank_cr },
                    { "TRANS_CR ", au.trans_cr },
                    { "tot_cr ", au.tot_cr },
                });
            }
            catch (Exception x)
            {

            }

        }
        public List<AccountsUtility> populate_journalBook(CashBookReportViewModel model)
        {
            string sql = string.Empty;
            string xacstr = "";
            string acstr = "";
            sql = "delete FROM rep_acc_journal";
            config.Execute_Query(sql);
            int i = 1;
            if (model.ac_hd.ToUpper() == "ALL")
            {
                sql = "select * from acc_head";
                config.singleResult(sql);
                if (config.dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in config.dt.Rows)
                    {
                        if (i < config.dt.Rows.Count)
                        {
                            xacstr = xacstr + "'" + Convert.ToString(dr["ac_hd"]) + "',";
                        }
                        else
                        {
                            xacstr = xacstr + "'" + Convert.ToString(dr["ac_hd"]) + "'";
                        }
                        i++;
                    }
                }
            }
            else
            {
                xacstr = "'" + model.ac_hd + "'";
            }
            acstr = "B.AC_HD IN (" + xacstr + ")";
            sql = "SELECT A.VCH_DATE,A.VCH_NO,A.VCH_NARR,";
            sql = sql + "B.AC_HD,B.VCH_SRL,B.VCH_DRCR,B.VCH_PACNO,B.VCH_ACNAME,IIF(B.VCH_DRCR='C',B.VCH_AMT,0) AS CREDIT_AMT,IIF(B.VCH_DRCR='D',B.VCH_AMT,0) AS DEBIT_AMT,";
            sql = sql + "C.AC_DESC FROM VCH_HEADER A,VCH_DETAIL B,ACC_HEAD C";
            sql = sql + " WHERE ((A.BRANCH_ID=B.BRANCH_ID AND A.VCH_DATE=B.VCH_DATE AND";
            sql = sql + " A.VCH_NO=B.VCH_NO) AND (B.AC_HD=C.AC_HD) AND (";
            sql = sql + acstr + ")) AND ";
            sql = sql + " convert(varchar, A.VCH_DATE, 103) BETWEEN  convert(varchar, '" + model.fr_dt + "', 103) AND  convert(varchar, '" + model.to_dt + "', 103) ";
            sql = sql + "AND (A.BRANCH_ID='" + model.branch + "' AND A.VCH_TYPE='J')";
            sql = sql + " ORDER BY A.VCH_DATE,A.VCH_NO,B.VCH_SRL";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    AccountsUtility au = new AccountsUtility();
                    try
                    {
                        config.Insert("rep_acc_journal", new Dictionary<String, object>()
                        {
                            { "vch_date ",Convert.ToDateTime(dr["vch_date"])},
                            { "vch_no ",Convert.ToString (dr["vch_no"])},
                            { "vch_srl ",Convert.ToDecimal (dr["vch_srl"])},
                            { "ac_hd ", Convert.ToString (dr["ac_hd"])},
                            { "AC_DESC ",Convert.ToString (dr["AC_DESC"])},
                            { "VCH_DRCR ",Convert.ToString (dr["VCH_DRCR"])},
                            { "vch_pacno ",Convert.ToString (dr["vch_pacno"])},
                            { "vch_acname ",Convert.ToString (dr["vch_acname"])},
                            { "VCH_NARR ",Convert.ToString (dr["VCH_NARR"]) },
                            { "DEBIT_AMT ",Convert.ToDecimal (dr["DEBIT_AMT"])},
                            { "CREDIT_AMT ",Convert.ToDecimal (dr["CREDIT_AMT"])},
                        });
                    }
                    catch (Exception x)
                    {

                    }
                }
            }
            List<AccountsUtility> aulst = new List<AccountsUtility>();
            sql = "Select * from rep_acc_journal";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    AccountsUtility au = new AccountsUtility();
                    au.vch_date = Convert.ToDateTime(dr["vch_date"]);
                    au.vch_no = Convert.ToString(dr["vch_no"]);
                    au.vch_srl = Convert.ToDecimal(dr["vch_srl"]);
                    au.ac_hd = Convert.ToString(dr["ac_hd"]);
                    au.ac_desc = Convert.ToString(dr["AC_DESC"]);
                    au.vch_drcr = Convert.ToString(dr["VCH_DRCR"]);
                    au.vch_pacno = Convert.ToString(dr["vch_pacno"]);
                    au.vch_acname = Convert.ToString(dr["vch_acname"]);
                    au.vch_narr = Convert.ToString(dr["VCH_NARR"]);
                    au.debit_amt = Convert.ToDecimal(dr["DEBIT_AMT"]);
                    au.credit_amt = Convert.ToDecimal(dr["CREDIT_AMT"]);
                    aulst.Add(au);
                }
            }
            return aulst;
        }
        public string updateGenaralLedger(CashBookReportViewModel model)
        {
            string msg = "";
            try
            {
                DateTime cdt = new DateTime();
                DateTime pdt = new DateTime();
                decimal xcr_amt = 0;
                decimal xdr_amt = 0;
                decimal xcash = 0;
                decimal xbank = 0;
                decimal xnetamt = 0;
                decimal xglbal = 0;
                string XACHD = "";
                string sql = string.Empty;

                //sql = "delete  FROM GL_BALNCE WHERE BRANCH_ID='" + model.branch + "' AND ";
                //sql = sql + "convert(varchar,GL_DATE, 103) >= convert(varchar, '" + model.fr_dt + "', 103)";
                //sql = sql + "ORDER BY BRANCH_ID,GL_DATE,AC_HD";           
                //config.Execute_Query(sql);

                sql = "Select * FROM GL_BALNCE WHERE BRANCH_ID='" + model.branch + "' AND ";
                sql = sql + "convert(date,GL_DATE, 103) >= convert(date, '" + model.fr_dt + "', 103)";
                sql = sql + "ORDER BY BRANCH_ID,GL_DATE,AC_HD";
                config.singleResult(sql);
                if (config.dt.Rows.Count > 0)
                {
                    sql = "delete  FROM GL_BALNCE WHERE BRANCH_ID='" + model.branch + "' AND ";
                    sql = sql + "convert(date,GL_DATE, 103) >= convert(date, '" + model.fr_dt + "', 103)";
                    config.Execute_Query(sql);
                }



                sql = "SELECT convert(date, A.VCH_DATE, 103) AS TR_DATE,A.AC_HD,";
                sql = sql + "SUM(IIF(A.VCH_DRCR='C' AND B.VCH_TYPE='C', A.VCH_AMT,0)) AS CASH_CR,";
                sql = sql + "SUM(IIF(A.VCH_DRCR='C' AND B.VCH_TYPE='B',A.VCH_AMT,0)) AS BANK_CR,";
                sql = sql + "SUM(IIF(A.VCH_DRCR='C' AND (B.VCH_TYPE='T' OR B.VCH_TYPE='J'),A.VCH_AMT,0)) AS TRANS_CR,";
                sql = sql + "SUM(IIF(A.VCH_DRCR='D' AND B.VCH_TYPE='C',A.VCH_AMT,0)) AS CASH_DR,";
                sql = sql + "SUM(IIF(A.VCH_DRCR='D' AND B.VCH_TYPE='B',A.VCH_AMT,0)) AS BANK_DR,";
                sql = sql + "SUM(IIF(A.VCH_DRCR='D' AND (B.VCH_TYPE='T' OR B.VCH_TYPE='J'),A.VCH_AMT,0)) AS TRANS_DR ";
                sql = sql + "FROM VCH_DETAIL A,VCH_HEADER B WHERE (convert(date, A.VCH_DATE, 103)=convert(date, B.VCH_DATE, 103) AND A.VCH_NO=B.VCH_NO) ";
                sql = sql + "And convert(date, A.VCH_DATE, 103) >= convert(date, '" + model.fr_dt + "', 103) And ";
                sql = sql + "A.BRANCH_ID='" + model.branch + "' ";
                sql = sql + "GROUP BY convert(date, A.VCH_DATE, 103),A.AC_HD";
                config.singleResult(sql); //convert(varchar, A.VCH_DATE, 103) = convert(varchar, '" + model.daybook_dt + "', 103)
                List<AccountsUtility> aulst = new List<AccountsUtility>();
                DataTable dt1 = config.dt;
                if (dt1.Rows.Count == 0)
                {
                    msg = "No Voucher found to Update G/L";
                }
                if (dt1.Rows.Count > 0)
                {
                    string str_pdt = "";
                    string str_cdt = "";
                    DataRow dr2 = (DataRow)dt1.Rows[0];
                    pdt = Convert.ToDateTime(Convert.ToDateTime(dr2["tr_date"]).ToString("dd-MM-yyyy").Replace("-", "/"));
                    str_pdt = pdt.ToString("dd/MM/yyyy");
                    foreach (DataRow dr in dt1.Rows)
                    {
                        AccountsUtility au = new AccountsUtility();
                        //XACHD = Convert.ToString(dr["ac_hd"]);
                        //cdt = Convert.ToDateTime(dr["tr_date"]);
                        //for (int i = 1; i <= dt1.Rows.Count; i++)
                        //{
                        cdt = Convert.ToDateTime(Convert.ToDateTime(dr["tr_date"]).ToString("dd-MM-yyyy").Replace("-", "/"));
                        str_cdt = cdt.ToString("dd/MM/yyyy");
                        if (str_cdt != str_pdt)
                        {
                            if (xcash != 0)
                            {
                                sql = "SELECT * FROM GL_BALNCE WHERE BRANCH_ID='" + model.branch + "' AND ";
                                sql = sql + "AC_HD='CASH' ";
                                sql = sql + "ORDER BY GL_DATE";
                                config.singleResult(sql);
                                DataTable dt2 = config.dt;
                                xglbal = 0;
                                if (dt2.Rows.Count > 0)
                                {
                                    DataRow dr1 = (DataRow)dt2.Rows[dt2.Rows.Count - 1];
                                    //if (Convert.ToDateTime(dr1["gl_date"]) < Convert.ToDateTime(dr1["tr_date"]))
                                    if (Convert.ToDateTime(Convert.ToDateTime(dr1["gl_date"]).ToString("dd-MM-yyyy").Replace("-", "/")) < pdt)
                                    {
                                        xglbal = Convert.ToDecimal(dr1["gl_bal"]);
                                    }
                                }
                                config.Insert("GL_BALNCE", new Dictionary<String, object>()
                                    {
                                        { "branch_id",      model.branch},
                                        { "ac_hd",     "CASH" },
                                        //{ "GL_DATE",   Convert.ToDateTime(dr1["tr_date"])},
                                        { "GL_DATE",   pdt},
                                        { "gl_bal",    xglbal + xcash },
                                    });
                            }
                            if (xbank != 0)
                            {
                                sql = "SELECT * FROM GL_BALNCE WHERE BRANCH_ID='" + model.branch + "' AND ";
                                sql = sql + "AC_HD='BANK' ";
                                sql = sql + "ORDER BY GL_DATE";
                                config.singleResult(sql);
                                DataTable dt3 = config.dt;
                                xglbal = 0;
                                if (dt3.Rows.Count > 0)
                                {
                                    DataRow dr1 = (DataRow)dt3.Rows[dt3.Rows.Count - 1];
                                    //if (Convert.ToDateTime(dr1["gl_date"]) < Convert.ToDateTime(dr1["tr_date"]))
                                    if (Convert.ToDateTime(Convert.ToDateTime(dr1["gl_date"]).ToString("dd-MM-yyyy").Replace("-", "/")) < pdt)
                                    {
                                        xglbal = Convert.ToDecimal(dr1["gl_bal"]);
                                    }
                                }
                                config.Insert("GL_BALNCE", new Dictionary<String, object>()
                                    {
                                        { "branch_id",    model.branch },
                                        { "ac_hd",    "BANK" },
                                        //{ "GL_DATE",     Convert.ToDateTime(dr1["tr_date"])},
                                        { "GL_DATE",     pdt},
                                        { "gl_bal",    xglbal + xbank },
                                    });
                            }
                            pdt = cdt;
                            str_pdt = str_cdt;
                            xcash = 0;
                            xbank = 0;
                        }
                        XACHD = Convert.ToString(dr["ac_hd"]);
                        xcr_amt = Convert.ToDecimal(dr["CASH_CR"]) + Convert.ToDecimal(dr["BANK_CR"]) + Convert.ToDecimal(dr["TRANS_CR"]);
                        xdr_amt = Convert.ToDecimal(dr["CASH_DR"]) + Convert.ToDecimal(dr["BANK_DR"]) + Convert.ToDecimal(dr["TRANS_DR"]);
                        xnetamt = xcr_amt - xdr_amt;
                        xcash = xcash - Convert.ToDecimal(dr["CASH_CR"]) + Convert.ToDecimal(dr["CASH_DR"]);
                        xbank = xbank - Convert.ToDecimal(dr["BANK_CR"]) + Convert.ToDecimal(dr["BANK_DR"]);
                        sql = "select * from acc_head where AC_HD='" + XACHD + "'";
                        config.singleResult(sql);
                        DataTable dt4 = config.dt;
                        if (dt4.Rows.Count > 0)
                        {
                            DataRow dr1 = (DataRow)dt4.Rows[0];
                            if (Convert.ToString(dr1["IS_CONTRA"]) == "Y")
                            {
                                xbank = xbank + xnetamt;
                            }
                        }
                        sql = "SELECT * FROM GL_BALNCE WHERE BRANCH_ID='" + model.branch + "' AND ";
                        sql = sql + "AC_HD='" + XACHD + "' ";
                        sql = sql + "ORDER BY GL_DATE";
                        config.singleResult(sql);
                        DataTable dt5 = config.dt;
                        xglbal = 0;
                        if (dt5.Rows.Count > 0)
                        {
                            DataRow dr1 = (DataRow)dt5.Rows[dt5.Rows.Count - 1];
                            //if (Convert.ToDateTime(dr1["gl_date"]) < Convert.ToDateTime(dr1["tr_date"]))
                            if (Convert.ToDateTime(Convert.ToDateTime(dr1["gl_date"]).ToString("dd-MM-yyyy").Replace("-", "/")) < cdt)
                            {
                                xglbal = Convert.ToDecimal(dr1["gl_bal"]);
                            }
                        }
                        config.Insert("GL_BALNCE", new Dictionary<String, object>()
                            {
                                { "branch_id",     model.branch },
                                { "ac_hd",      XACHD},
                                //{ "GL_DATE",   Convert.ToDateTime(dr1["tr_date"])},
                                { "GL_DATE",   cdt},
                                { "gl_bal",     xglbal + xnetamt },
                            });
                        // }
                        //XACHD = Convert.ToString(dr["ac_hd"]);
                        //xcr_amt = Convert.ToDecimal(dr["CASH_CR"]) + Convert.ToDecimal(dr["BANK_CR"]) + Convert.ToDecimal(dr["TRANS_CR"]);
                        //xdr_amt = Convert.ToDecimal(dr["CASH_DR"]) + Convert.ToDecimal(dr["BANK_DR"]) + Convert.ToDecimal(dr["TRANS_DR"]);
                        //xnetamt = xcr_amt - xdr_amt;
                        //xcash = xcash - Convert.ToDecimal(dr["CASH_CR"]) + Convert.ToDecimal(dr["CASH_DR"]);
                        //xbank = xbank - Convert.ToDecimal(dr["BANK_CR"]) + Convert.ToDecimal(dr["BANK_DR"]);
                        //sql = "select * from acc_head where AC_HD='" + XACHD + "'";
                        //config.singleResult(sql);
                        //if (config.dt.Rows.Count > 0)
                        //{
                        //    DataRow dr1 = (DataRow)config.dt.Rows[0];
                        //    if (Convert.ToString(dr1["IS_CONTRA"]) == "Y")
                        //    {
                        //        xbank = xbank + xnetamt;
                        //    }
                        //}
                        //sql = "SELECT * FROM GL_BALNCE WHERE BRANCH_ID='" + model.branch + "' AND ";
                        //sql = sql + "AC_HD='" + XACHD + "' ";
                        //sql = sql + "ORDER BY GL_DATE";
                        //config.singleResult(sql);
                        //xglbal = 0;
                        //if (config.dt.Rows.Count > 0)
                        //{
                        //    DataRow dr1 = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                        //    //if (Convert.ToDateTime(dr1["gl_date"]) < Convert.ToDateTime(dr1["tr_date"]))
                        //    if (Convert.ToDateTime(dr1["gl_date"]) < cdt)
                        //    {
                        //        xglbal = Convert.ToDecimal(dr1["gl_bal"]);
                        //    }                      
                        //}
                        //config.Insert("GL_BALNCE", new Dictionary<String, object>()
                        //{
                        //    { "branch_id",     model.branch },
                        //    { "ac_hd",      XACHD},
                        //    //{ "GL_DATE",   Convert.ToDateTime(dr1["tr_date"])},
                        //    { "GL_DATE",   cdt},
                        //    { "gl_bal",     xglbal + xnetamt },
                        //});
                        //if (xcash != 0)
                        //{
                        //    sql = "SELECT * FROM GL_BALNCE WHERE BRANCH_ID='" + model.branch + "' AND ";
                        //    sql = sql + "AC_HD='CASH' ";
                        //    sql = sql + "ORDER BY GL_DATE";
                        //    config.singleResult(sql);
                        //    xglbal = 0;
                        //    if (config.dt.Rows.Count > 0)
                        //    {
                        //        DataRow dr1 = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                        //        //if (Convert.ToDateTime(dr1["gl_date"]) < Convert.ToDateTime(dr1["tr_date"]))
                        //        if (Convert.ToDateTime(dr1["gl_date"]) < pdt)
                        //        {
                        //            xglbal = Convert.ToDecimal(dr1["gl_bal"]);
                        //        }
                        //        config.Insert("GL_BALNCE", new Dictionary<String, object>()
                        //        {
                        //            { "branch_id",      model.branch},
                        //            { "ac_hd",     "CASH" },
                        //            //{ "GL_DATE",   Convert.ToDateTime(dr1["tr_date"])},
                        //            { "GL_DATE",   pdt},
                        //            { "gl_bal",    xglbal + xcash },
                        //        });
                        //    }
                        //}
                        //if (xbank != 0)
                        //{
                        //    sql = "SELECT * FROM GL_BALNCE WHERE BRANCH_ID='" + model.branch + "' AND ";
                        //    sql = sql + "AC_HD='BANK' ";
                        //    sql = sql + "ORDER BY GL_DATE";
                        //    config.singleResult(sql);
                        //    xglbal = 0;
                        //    if (config.dt.Rows.Count > 0)
                        //    {
                        //        DataRow dr1 = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                        //        //if (Convert.ToDateTime(dr1["gl_date"]) < Convert.ToDateTime(dr1["tr_date"]))
                        //        if (Convert.ToDateTime(dr1["gl_date"]) < pdt)
                        //        {
                        //            xglbal = Convert.ToDecimal(dr1["gl_bal"]);
                        //        }                           
                        //    }
                        //    config.Insert("GL_BALNCE", new Dictionary<String, object>()
                        //    {
                        //        { "branch_id",    model.branch },
                        //        { "ac_hd",    "BANK" },
                        //        //{ "GL_DATE",     Convert.ToDateTime(dr1["tr_date"])},
                        //        { "GL_DATE",     pdt},
                        //        { "gl_bal",    xglbal + xbank },
                        //    });
                        //}
                        //}
                        //}
                    }
                    if (xcash != 0)
                    {
                        sql = "SELECT * FROM GL_BALNCE WHERE BRANCH_ID='" + model.branch + "' AND ";
                        sql = sql + "AC_HD='CASH' ";
                        sql = sql + "ORDER BY GL_DATE";
                        config.singleResult(sql);
                        DataTable dt6 = config.dt;
                        xglbal = 0;
                        if (dt6.Rows.Count > 0)
                        {
                            DataRow dr1 = (DataRow)dt6.Rows[dt6.Rows.Count - 1];
                            //if (Convert.ToDateTime(dr1["gl_date"]) < Convert.ToDateTime(dr1["tr_date"]))
                            if (Convert.ToDateTime(Convert.ToDateTime(dr1["gl_date"]).ToString("dd-MM-yyyy").Replace("-", "/")) < pdt)
                            {
                                xglbal = Convert.ToDecimal(dr1["gl_bal"]);
                            }
                            config.Insert("GL_BALNCE", new Dictionary<String, object>()
                            {
                                { "branch_id",      model.branch},
                                { "ac_hd",     "CASH" },
                                //{ "GL_DATE",   Convert.ToDateTime(dr1["tr_date"])},
                                { "GL_DATE",   pdt},
                                { "gl_bal",    xglbal + xcash },
                            });
                        }
                    }
                    if (xbank != 0)
                    {
                        sql = "SELECT * FROM GL_BALNCE WHERE BRANCH_ID='" + model.branch + "' AND ";
                        sql = sql + "AC_HD='BANK' ";
                        sql = sql + "ORDER BY GL_DATE";
                        config.singleResult(sql);
                        DataTable dt7 = config.dt;
                        xglbal = 0;
                        if (dt7.Rows.Count > 0)
                        {
                            DataRow dr1 = (DataRow)dt7.Rows[dt7.Rows.Count - 1];
                            //if (Convert.ToDateTime(dr1["gl_date"]) < Convert.ToDateTime(dr1["tr_date"]))
                            if (Convert.ToDateTime(Convert.ToDateTime(dr1["gl_date"]).ToString("dd-MM-yyyy").Replace("-", "/")) < pdt)
                            {
                                xglbal = Convert.ToDecimal(dr1["gl_bal"]);
                            }
                        }
                        config.Insert("GL_BALNCE", new Dictionary<String, object>()
                        {
                            { "branch_id",    model.branch },
                            { "ac_hd",    "BANK" },
                            //{ "GL_DATE",     Convert.ToDateTime(dr1["tr_date"])},
                            { "GL_DATE",     pdt},
                            { "gl_bal",    xglbal + xbank },
                        });
                    }
                    msg = "Updated Successfully";
                }              
            }
            catch (Exception ex)
            {
                string x;
            }
            return (msg);
        }
    }
}