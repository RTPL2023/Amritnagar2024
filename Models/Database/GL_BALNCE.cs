using Amritnagar.Includes;
using Amritnagar.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;



namespace Amritnagar.Models.Database
{
    public class GL_BALNCE
    {
        SQLConfig config = new SQLConfig();

        public string branch_id { get; set; }
        public DateTime gl_date { get; set; }
        public string ac_hd { get; set; }
        public string ac_desc { get; set; }
        public decimal gl_bal { get; set; }
        public decimal clbal { get; set; }
        public decimal op_cash { get; set; }
        public decimal op_bank { get; set; }
        public decimal dbalance { get; set; }
        public decimal cbalance { get; set; }
        public decimal grouptotal { get; set; }
        public decimal tot_dr { get; set; }
        public decimal tot_cr { get; set; }
        public string majorgroup { get; set; }
        public string acchd { get; set; }
        public string ac_majgr { get; set; }
        public string ac_majgrdesc { get; set; }
        public string ac_subgr { get; set; }
        public string ac_subgrdesc { get; set; }

        public List<GL_BALNCE> gettrialbalancelist(TrialBalanceReportViewModel model)
        {
            decimal LBAL = 0;
            decimal d = 0;
            decimal c = 0;
            decimal xgrptot = 0;
            int xstart = 0;
            string xpmgrp = "";
            string sql = "SELECT A.AC_HD, A.GL_DATE, A.GL_BAL, B.AC_DESC,B.AC_MAJGR,B.AC_SUBGR,C.AC_MAJGRDESC,D.AC_SUBGRDESC ";
            sql = sql + "FROM GL_BALNCE AS A, ACC_HEAD AS B,ACC_HEAD_MGR AS C,ACC_HEAD_SGR AS D ";
            sql = sql + "WHERE A.BRANCH_ID='" + model.branch + "' AND (convert(datetime, GL_DATE, 103) IN (SELECT MAX(GL_DATE) FROM GL_BALNCE WHERE ";
            sql = sql + "BRANCH_ID='" + model.branch + "' AND convert(datetime, GL_DATE, 103) <= convert(datetime, '" + model.gl_date + "', 103) AND AC_HD=A.AC_HD))AND ";
            sql = sql + "((A.AC_HD=B.AC_HD) AND (B.AC_MAJGR=C.AC_MAJGR) AND (B.AC_MAJGR=D.AC_MAJGR AND B.AC_SUBGR=D.AC_SUBGR))";
            sql = sql + "AND B.EX_TRIAL IS NULL ";
            sql = sql + "ORDER BY B.AC_MAJGR,B.AC_SUBGR,A.AC_HD";
            config.singleResult(sql);
            GL_BALNCE gl1 = new GL_BALNCE();
            List<GL_BALNCE> glblst = new List<GL_BALNCE>();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    GL_BALNCE gl = new GL_BALNCE();
                    LBAL = !Convert.IsDBNull(dr["gl_bal"]) ? Convert.ToDecimal(dr["gl_bal"]) : Convert.ToDecimal("00");
                    if (LBAL != 00)
                    {
                        xpmgrp = Convert.ToString(dr["ac_majgr"]);
                        if (xpmgrp != null && xpmgrp != "")
                        {
                            if (xstart == 1)
                            {
                                gl.grouptotal = Math.Abs(xgrptot);
                            }
                            xpmgrp = Convert.ToString(dr["ac_majgr"]);
                            xgrptot = 0;
                        }
                        xstart = 1;
                        gl.majorgroup = Convert.ToString(dr["ac_majgr"]) + " - " + Convert.ToString(dr["ac_majgrdesc"]);
                        gl.acchd = Convert.ToString(dr["ac_hd"]) + " - " + Convert.ToString(dr["AC_DESC"]);
                        xgrptot = xgrptot + LBAL;
                        if (LBAL < 0)
                        {
                            gl.dbalance = Math.Abs(LBAL);
                            //gl.cbalance = "";
                            d = d + Math.Abs(LBAL);
                        }
                        if (LBAL > 0)
                        {
                            //gl.dbalance = "";
                            gl.cbalance = Math.Abs(LBAL);
                            c = c + Math.Abs(LBAL);
                        }
                    }
                    glblst.Add(gl);
                }
                if (xstart == 1)
                {
                    gl1.grouptotal = Math.Abs(xgrptot);
                }
                glblst.Add(gl1);
            }
            return glblst;
        }
        public string SaveInDividentLedger(TrialBalanceReportViewModel model)
        {
            string sql = string.Empty;
            sql = "truncate table rep_acc_trial";
            config.Execute_Query(sql);
            decimal dr_balance = 0;
            decimal cr_balance = 0;
            sql = "SELECT A.AC_HD, A.GL_DATE, A.GL_BAL, B.AC_DESC,B.AC_MAJGR,B.AC_SUBGR,C.AC_MAJGRDESC,D.AC_SUBGRDESC ";
            sql = sql + "FROM GL_BALNCE AS A, ACC_HEAD AS B,ACC_HEAD_MGR AS C,ACC_HEAD_SGR AS D ";
            sql = sql + "WHERE A.BRANCH_ID='" + model.branch + "' AND (convert(datetime, GL_DATE, 103) IN (SELECT MAX(GL_DATE) FROM GL_BALNCE WHERE ";
            sql = sql + "BRANCH_ID='" + model.branch + "' AND convert(datetime, GL_DATE, 103) <= convert(datetime, '" + model.gl_date + "', 103) AND AC_HD=A.AC_HD))AND ";
            sql = sql + "((A.AC_HD=B.AC_HD) AND (B.AC_MAJGR=C.AC_MAJGR) AND (B.AC_MAJGR=D.AC_MAJGR AND B.AC_SUBGR=D.AC_SUBGR))";
            sql = sql + "AND B.EX_TRIAL IS NULL ";
            sql = sql + "ORDER BY B.AC_MAJGR,B.AC_SUBGR,A.AC_HD";
            config.singleResult(sql);
            GL_BALNCE gl1 = new GL_BALNCE();
            List<GL_BALNCE> glblst = new List<GL_BALNCE>();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    GL_BALNCE gl = new GL_BALNCE();
                    gl.ac_hd = Convert.ToString(dr["ac_hd"]);
                    gl.ac_desc = Convert.ToString(dr["AC_DESC"]);
                    gl.ac_majgr = Convert.ToString(dr["ac_majgr"]);
                    gl.ac_majgrdesc = Convert.ToString(dr["ac_majgrdesc"]);
                    gl.ac_subgr = Convert.ToString(dr["AC_SUBGR"]);
                    gl.ac_subgrdesc = Convert.ToString(dr["AC_SUBGRDESC"]);
                    gl.gl_bal = !Convert.IsDBNull(dr["gl_bal"]) ? Convert.ToDecimal(dr["gl_bal"]) : Convert.ToDecimal("00");
                    if (gl.gl_bal<0)
                    {
                        dr_balance = gl.gl_bal;
                    }
                    else
                    {
                        cr_balance = gl.gl_bal;
                    }
                    gl.gl_date = Convert.ToDateTime(dr["GL_DATE"]);
                    try
                    {
                        config.Insert("rep_acc_trial", new Dictionary<String, object>()
                        {
                            { "ac_hd",   gl.ac_hd },
                            { "AC_DESC",   gl.ac_desc },
                            { "ac_majgr",    gl.ac_majgr},
                            { "ac_majgrdesc",  gl.ac_majgrdesc   },
                            { "ac_subgr",     gl.ac_subgr },
                            { "ac_subgrdesc",   gl.ac_subgrdesc },
                            { "dr_balance", Convert.ToDecimal(dr_balance)},
                            { "cr_balance",  Convert.ToDecimal(cr_balance)},
                            { "gl_date",    Convert.ToDateTime(  gl.gl_date )},
                        });
                    }
                    catch (Exception ex)
                    {

                    }
                    dr_balance = 0;
                    cr_balance = 0;
                }
            }
            string msg = "Saved Successfully";
            return (msg);
        }
        public GL_BALNCE getopeningbalanceforcashaccount(string branch, string fr_dt)
        {
            string qry;
            qry = "select * from gl_balnce where branch_id='" + branch + "'";
            qry = qry + " and  convert(datetime, GL_DATE, 103) < convert(datetime, '" + fr_dt + "', 103)";
            qry = qry + " and  (ac_hd ='CASH')  order by gl_date desc";
            config.singleResult(qry);
            GL_BALNCE gl = new GL_BALNCE();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    gl.gl_bal = !Convert.IsDBNull(dr["gl_bal"]) ? Convert.ToDecimal(dr["gl_bal"]) : Convert.ToDecimal("00");
                    gl.gl_date = !Convert.IsDBNull(dr["GL_DATE"]) ? Convert.ToDateTime(dr["GL_DATE"]) : Convert.ToDateTime(null);
                }
            }            
            return gl;
        }
        public GL_BALNCE getopeningbalanceforcashbook(string branch, string fr_dt)
        {
            string qry;
            GL_BALNCE gl = new GL_BALNCE();
            qry = "select * from gl_balnce where branch_id='" + branch + "'";
            qry = qry + " and  convert(datetime, GL_DATE, 103) < convert(datetime, '" + fr_dt + "', 103)";
            qry = qry + " and (ac_hd in ('CASH'))  order by gl_date desc";
            config.singleResult(qry);
            
            if (config.dt.Rows.Count > 0)
            {
                DataRow dr = (DataRow)config.dt.Rows[0];
                                   
                  gl.op_cash = !Convert.IsDBNull(dr["gl_bal"]) ? Convert.ToDecimal(dr["gl_bal"]) : Convert.ToDecimal("00");
                  gl.gl_date = !Convert.IsDBNull(dr["GL_DATE"]) ? Convert.ToDateTime(dr["GL_DATE"]) : Convert.ToDateTime(null);
                  gl.op_cash = Math.Abs(gl.op_cash);                                   
            }
            qry = "select * from gl_balnce where branch_id='" + branch + "'";
            qry = qry + " and  convert(datetime, GL_DATE, 103) < convert(datetime, '" + fr_dt + "', 103)";
            qry = qry + " and (ac_hd in ('BANK'))  order by gl_date desc";
            config.singleResult(qry);
            if (config.dt.Rows.Count > 0)
            {
                DataRow dr = (DataRow)config.dt.Rows[0];              
                gl.op_bank = !Convert.IsDBNull(dr["gl_bal"]) ? Convert.ToDecimal(dr["gl_bal"]) : Convert.ToDecimal("00");
                gl.gl_date = !Convert.IsDBNull(dr["GL_DATE"]) ? Convert.ToDateTime(dr["GL_DATE"]) : Convert.ToDateTime(null);
                gl.op_bank = Math.Abs(gl.op_bank);
            }
            return gl;
        }
    }
}