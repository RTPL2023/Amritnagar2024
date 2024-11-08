using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Amritnagar.Includes;
using Amritnagar.Models.ViewModel;

namespace Amritnagar.Models.Database
{
    public class Recovery_Get
    {
        SQLConfig config = new SQLConfig();
        public string branch_id { get; set; }
        public string eMPLOYER_cD { get; set; }
        public string eMPLOYER_bRANCH { get; set; }
        public string bOOK_nO { get; set; }
        public string eMPLOYEE_iD { get; set; }
        public string aC_hD { get; set; }
        public string vCH_pACNO { get; set; }
        public string rECOVERY_mN { get; set; }
        public string rECOVERY_yR { get; set; }
        public decimal pRIN_aMT { get; set; }
        public decimal iNT_aMT { get; set; }
        public decimal prin_bal { get; set; }
        public decimal TOTTF { get; set; }
        public decimal TOTSH { get; set; }
        public decimal TOTLICP { get; set; }
        public decimal TOTRTB { get; set; }
        public decimal TOTSFL { get; set; }
        public decimal TOTISFL { get; set; }
        public decimal TOTSJL { get; set; }
        public decimal TOTISJL { get; set; }
        public decimal TOTPSL { get; set; }
        public decimal TOTIPSL { get; set; }
        public decimal TOTDLL { get; set; }
        public decimal TOTIDLL { get; set; }
        public decimal TOTSL3 { get; set; }
        public decimal TOTSL3I { get; set; }
        public decimal TOTM12 { get; set; }
        public decimal TOTIM12 { get; set; }
        public decimal TOTM14 { get; set; }
        public decimal TOTIM14 { get; set; }
        public decimal TOTPSL1 { get; set; }
        public decimal TOTIPSL1 { get; set; }
        public decimal TOTSFL1 { get; set; }
        public decimal TOTISFL1 { get; set; }
        public decimal TOTSL4 { get; set; }
        public decimal TOTISL4 { get; set; }
        public decimal TOTSL6 { get; set; }
        public decimal TOTISL6 { get; set; }
        public decimal TOTSL7 { get; set; }
        public decimal TOTISL7 { get; set; }
        public decimal TOTSJL1 { get; set; }
        public decimal TOTISJL1 { get; set; }
        public string mEMBER_iD { get; set; }
        public string mem_name { get; set; }
        public DateTime sCH_dATE { get; set; }
        public DateTime rECOVERY_dATE { get; set; }

        public List<Recovery_Get> getdetails(string emp_branch, string book_no, string rec_dt)
        {
            decimal TOTTF = 0;
            decimal TOTSH = 0;
            decimal TOTLICP = 0;
            decimal TOTRTB = 0;
            decimal TOTSFL = 0;
            decimal TOTISFL = 0;
            decimal TOTISJL = 0;
            decimal TOTSJL = 0;
            decimal TOTPSL = 0;
            decimal TOTIPSL = 0;
            decimal TOTDLL = 0;
            decimal TOTIDLL = 0;
            decimal TOTSL3 = 0;
            decimal TOTSL3I = 0;
            decimal TOTM12 = 0;
            decimal TOTIM12 = 0;
            decimal TOTM14 = 0;
            decimal TOTIM14 = 0;
            decimal TOTPSL1 = 0;
            decimal TOTIPSL1 = 0;
            decimal TOTSFL1 = 0;
            decimal TOTISFL1 = 0;
            decimal TOTSL4 = 0;
            decimal TOTISL4 = 0;
            decimal TOTSL6 = 0;
            decimal TOTISL6 = 0;
            decimal TOTSL7 = 0;
            decimal TOTISL7 = 0;
            decimal TOTSJL1 = 0;
            decimal TOTISJL1 = 0;
            List<Recovery_Get> rgl = new List<Recovery_Get>();
            string sql = string.Empty;
            sql = "select * from recovery_get where convert(varchar, RECOVERY_DATE, 103) = convert(varchar, '" + rec_dt + "', 103) order by employee_id";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    Recovery_Get rg = new Recovery_Get();
                    rg.eMPLOYEE_iD = Convert.ToString(dr["EMPLOYEE_ID"]);
                    sql = "select * from member_mast where employee_id='" + rg.eMPLOYEE_iD + "' AND MEMBER_CLOSED IS NULL  order by employee_id";
                    config.singleResult(sql);
                    if (config.dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr1 in config.dt.Rows)
                        {
                            rg.mEMBER_iD = Convert.ToString(dr1["MEMBER_ID"]);
                            try
                            {
                                config.Update("member_mast", new Dictionary<String, object>()
                                {
                                { "MEMBER_ID",        rg.mEMBER_iD},
                                }, new Dictionary<string, object>()
                                {
                                { "EMPLOYEE_ID",     rg.eMPLOYEE_iD },
                                });
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }
                }
            }
            sql = "SELECT * FROM RECOVERY_GET WHERE  EMPLOYER_BRANCH='" + emp_branch + "' AND  BOOK_NO='" + book_no + "' AND convert(varchar, RECOVERY_DATE, 103) = convert(varchar, '" + rec_dt + "', 103) ORDER BY MEMBER_ID";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr2 in config.dt.Rows)
                {
                    Recovery_Get rg = new Recovery_Get();
                    rg.eMPLOYEE_iD = Convert.ToString(dr2["EMPLOYEE_ID"]);
                    rg.aC_hD = Convert.ToString(dr2["AC_HD"]);
                    rg.pRIN_aMT = !Convert.IsDBNull(dr2["PRIN_AMT"]) ? Convert.ToDecimal(dr2["PRIN_AMT"]) : Convert.ToDecimal(00);
                    rg.iNT_aMT = !Convert.IsDBNull(dr2["INT_AMT"]) ? Convert.ToDecimal(dr2["INT_AMT"]) : Convert.ToDecimal(00);
                    sql = "select * from member_mast where employee_id='" + rg.eMPLOYEE_iD + "' AND MEMBER_CLOSED IS NULL  order by MEMBER_ID";
                    config.singleResult(sql);
                    if (config.dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr3 in config.dt.Rows)
                        {
                            rg.mem_name = Convert.ToString(dr3["MEMBER_NAME"]);
                            rg.mEMBER_iD = Convert.ToString(dr3["MEMBER_ID"]);
                        }
                    }
                    if (rg.aC_hD == "TF")
                    {
                        TOTTF = (TOTTF + rg.pRIN_aMT + rg.iNT_aMT);
                        sql = "select * from TF_LEDGER where BRANCH_ID='MN' AND MEMBER_ID='" + rg.mEMBER_iD + "' order by MEMBER_ID,VCH_DATE,VCH_NO,VCH_SRL";
                        config.singleResult(sql);
                        if (config.dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr4 in config.dt.Rows)
                            {
                                rg.prin_bal = !Convert.IsDBNull(dr4["PRIN_BAL"]) ? Convert.ToDecimal(dr4["PRIN_BAL"]) : Convert.ToDecimal(00);
                            }
                        }
                    }
                    if (rg.aC_hD == "SH")
                    {
                        TOTSH = (TOTSH + rg.pRIN_aMT + rg.iNT_aMT);
                    }
                    if (rg.aC_hD == "LICP")
                    {
                        TOTLICP = (TOTLICP + rg.pRIN_aMT + rg.iNT_aMT);
                    }
                    if (rg.aC_hD == "RTB")
                    {
                        TOTRTB = (TOTRTB + rg.pRIN_aMT + rg.iNT_aMT);
                    }
                    if (rg.aC_hD == "SFL")
                    {
                        TOTSFL = (TOTSFL + rg.pRIN_aMT);
                        TOTISFL = (TOTISFL + rg.iNT_aMT);
                        sql = "select * from loan_ledger where employee_id='" + rg.eMPLOYEE_iD + "' AND AC_HD='SFL'order by employee_id,vch_date,VCH_NO,VCH_SRL";
                        config.singleResult(sql);
                        if (config.dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr5 in config.dt.Rows)
                            {
                                rg.prin_bal = !Convert.IsDBNull(dr5["PRIN_BAL"]) ? Convert.ToDecimal(dr5["PRIN_BAL"]) : Convert.ToDecimal(00);
                            }
                        }
                    }
                    if (rg.aC_hD == "SJL")
                    {
                        TOTSJL = (TOTSJL + rg.pRIN_aMT);
                        TOTISJL = (TOTISJL + rg.iNT_aMT);
                        sql = "select * from loan_ledger where employee_id='" + rg.eMPLOYEE_iD + "' AND AC_HD='SFL'order by employee_id,vch_date,VCH_NO,VCH_SRL";
                        config.singleResult(sql);
                        if (config.dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr6 in config.dt.Rows)
                            {
                                rg.prin_bal = !Convert.IsDBNull(dr6["PRIN_BAL"]) ? Convert.ToDecimal(dr6["PRIN_BAL"]) : Convert.ToDecimal(00);
                            }
                        }
                    }
                    if (rg.aC_hD == "PSL")
                    {
                        TOTPSL = (TOTPSL + rg.pRIN_aMT);
                        TOTIPSL = (TOTIPSL + rg.iNT_aMT);
                        sql = "select * from loan_ledger where employee_id='" + rg.eMPLOYEE_iD + "' AND AC_HD='PSL'order by employee_id,vch_date,VCH_NO,VCH_SRL";
                        config.singleResult(sql);
                        if (config.dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr7 in config.dt.Rows)
                            {
                                rg.prin_bal = !Convert.IsDBNull(dr7["PRIN_BAL"]) ? Convert.ToDecimal(dr7["PRIN_BAL"]) : Convert.ToDecimal(00);
                            }
                        }
                    }
                    if (rg.aC_hD == "DLL")
                    {
                        TOTDLL = (TOTDLL + rg.pRIN_aMT);
                        TOTIDLL = (TOTIDLL + rg.iNT_aMT);
                        sql = "select * from loan_ledger where employee_id='" + rg.eMPLOYEE_iD + "' AND AC_HD='DLL'order by employee_id,vch_date,VCH_NO,VCH_SRL";
                        config.singleResult(sql);
                        if (config.dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr8 in config.dt.Rows)
                            {
                                rg.prin_bal = !Convert.IsDBNull(dr8["PRIN_BAL"]) ? Convert.ToDecimal(dr8["PRIN_BAL"]) : Convert.ToDecimal(00);
                            }
                        }
                    }
                    if (rg.aC_hD == "SL3")
                    {
                        TOTSL3 = (TOTSL3 + rg.pRIN_aMT);
                        TOTSL3I = (TOTSL3I + rg.iNT_aMT);
                        sql = "select * from loan_ledger where employee_id='" + rg.eMPLOYEE_iD + "' AND AC_HD='SL3'order by employee_id,vch_date,VCH_NO,VCH_SRL";
                        config.singleResult(sql);
                        if (config.dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr9 in config.dt.Rows)
                            {
                                rg.prin_bal = !Convert.IsDBNull(dr9["PRIN_BAL"]) ? Convert.ToDecimal(dr9["PRIN_BAL"]) : Convert.ToDecimal(00);
                            }
                        }
                    }
                    if (rg.aC_hD == "M12")
                    {
                        TOTM12 = (TOTM12 + rg.pRIN_aMT);
                        TOTIM12 = (TOTIM12 + rg.iNT_aMT);
                        sql = "select * from loan_ledger where employee_id='" + rg.eMPLOYEE_iD + "' AND AC_HD='M12'order by employee_id,vch_date,VCH_NO,VCH_SRL";
                        config.singleResult(sql);
                        if (config.dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr10 in config.dt.Rows)
                            {
                                rg.prin_bal = !Convert.IsDBNull(dr10["PRIN_BAL"]) ? Convert.ToDecimal(dr10["PRIN_BAL"]) : Convert.ToDecimal(00);
                            }
                        }
                    }
                    if (rg.aC_hD == "M14")
                    {
                        TOTM14 = (TOTM14 + rg.pRIN_aMT);
                        TOTIM14 = (TOTIM14 + rg.iNT_aMT);
                        sql = "select * from loan_ledger where employee_id='" + rg.eMPLOYEE_iD + "' AND AC_HD='M14'order by employee_id,vch_date,VCH_NO,VCH_SRL";
                        config.singleResult(sql);
                        if (config.dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr11 in config.dt.Rows)
                            {
                                rg.prin_bal = !Convert.IsDBNull(dr11["PRIN_BAL"]) ? Convert.ToDecimal(dr11["PRIN_BAL"]) : Convert.ToDecimal(00);
                            }
                        }
                    }
                    if (rg.aC_hD == "PSL1")
                    {
                        TOTPSL1 = (TOTPSL1 + rg.pRIN_aMT);
                        TOTIPSL1 = (TOTIPSL1 + rg.iNT_aMT);
                        sql = "select * from loan_ledger where employee_id='" + rg.eMPLOYEE_iD + "' AND AC_HD='PSL1'order by employee_id,vch_date,VCH_NO,VCH_SRL";
                        config.singleResult(sql);
                        if (config.dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr12 in config.dt.Rows)
                            {
                                rg.prin_bal = !Convert.IsDBNull(dr12["PRIN_BAL"]) ? Convert.ToDecimal(dr12["PRIN_BAL"]) : Convert.ToDecimal(00);
                            }
                        }
                    }
                    if (rg.aC_hD == "SFL1")
                    {
                        TOTSFL1 = (TOTSFL1 + rg.pRIN_aMT);
                        TOTISFL1 = (TOTISFL1 + rg.iNT_aMT);
                        sql = "select * from loan_ledger where employee_id='" + rg.eMPLOYEE_iD + "' AND AC_HD='SFL1'order by employee_id,vch_date,VCH_NO,VCH_SRL";
                        config.singleResult(sql);
                        if (config.dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr13 in config.dt.Rows)
                            {
                                rg.prin_bal = !Convert.IsDBNull(dr13["PRIN_BAL"]) ? Convert.ToDecimal(dr13["PRIN_BAL"]) : Convert.ToDecimal(00);
                            }
                        }
                    }
                    if (rg.aC_hD == "SL4")
                    {
                        TOTSL4 = (TOTSL4 + rg.pRIN_aMT);
                        TOTISL4 = (TOTISL4 + rg.iNT_aMT);
                        sql = "select * from loan_ledger where employee_id='" + rg.eMPLOYEE_iD + "' AND AC_HD='SL4'order by employee_id,vch_date,VCH_NO,VCH_SRL";
                        config.singleResult(sql);
                        if (config.dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr14 in config.dt.Rows)
                            {
                                rg.prin_bal = !Convert.IsDBNull(dr14["PRIN_BAL"]) ? Convert.ToDecimal(dr14["PRIN_BAL"]) : Convert.ToDecimal(00);
                            }
                        }
                    }
                    if (rg.aC_hD == "SL6")
                    {
                        TOTSL6 = (TOTSL6 + rg.pRIN_aMT);
                        TOTISL6 = (TOTISL6 + rg.iNT_aMT);
                        sql = "select * from loan_ledger where employee_id='" + rg.eMPLOYEE_iD + "' AND AC_HD='SL6'order by employee_id,vch_date,VCH_NO,VCH_SRL";
                        config.singleResult(sql);
                        if (config.dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr15 in config.dt.Rows)
                            {
                                rg.prin_bal = !Convert.IsDBNull(dr15["PRIN_BAL"]) ? Convert.ToDecimal(dr15["PRIN_BAL"]) : Convert.ToDecimal(00);
                            }
                        }
                    }
                    if (rg.aC_hD == "SL7")
                    {
                        TOTSL7 = (TOTSL7 + rg.pRIN_aMT);
                        TOTISL7 = (TOTISL7 + rg.iNT_aMT);
                        sql = "select * from loan_ledger where employee_id='" + rg.eMPLOYEE_iD + "' AND AC_HD='SL7'order by employee_id,vch_date,VCH_NO,VCH_SRL";
                        config.singleResult(sql);
                        if (config.dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr16 in config.dt.Rows)
                            {
                                rg.prin_bal = !Convert.IsDBNull(dr16["PRIN_BAL"]) ? Convert.ToDecimal(dr16["PRIN_BAL"]) : Convert.ToDecimal(00);
                            }
                        }
                    }
                    if (rg.aC_hD == "SJL1")
                    {
                        TOTSJL1 = (TOTSJL1 + rg.pRIN_aMT);
                        TOTISJL1 = (TOTISJL1 + rg.iNT_aMT);
                        sql = "select * from loan_ledger where employee_id='" + rg.eMPLOYEE_iD + "' AND AC_HD='SJL1'order by employee_id,vch_date,VCH_NO,VCH_SRL";
                        config.singleResult(sql);
                        if (config.dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr17 in config.dt.Rows)
                            {
                                rg.prin_bal = !Convert.IsDBNull(dr17["PRIN_BAL"]) ? Convert.ToDecimal(dr17["PRIN_BAL"]) : Convert.ToDecimal(00);
                            }
                        }
                    }
                    rg.TOTDLL = TOTDLL;
                    rg.TOTIDLL = TOTIDLL;
                    rg.TOTTF = TOTTF;
                    rg.TOTRTB = TOTRTB;
                    rg.TOTSH = TOTSH;
                    rg.TOTLICP = TOTLICP;
                    rg.TOTSFL = TOTSFL;
                    rg.TOTISFL = TOTISFL;
                    rg.TOTISJL = TOTISJL;
                    rg.TOTSJL = TOTSJL;
                    rg.TOTPSL = TOTPSL;
                    rg.TOTIPSL = TOTIPSL;
                    rg.TOTSL3 = TOTSL3;
                    rg.TOTSL3I = TOTSL3I;
                    rg.TOTM12 = TOTM12;
                    rg.TOTIM12 = TOTIM12;
                    rg.TOTM14 = TOTM14;
                    rg.TOTIM14 = TOTIM14;
                    rg.TOTIPSL1 = TOTIPSL1;
                    rg.TOTPSL1 = TOTPSL1;
                    rg.TOTSFL1 = TOTSFL1;
                    rg.TOTISFL1 = TOTISFL1;
                    rg.TOTSL4 = TOTSL4;
                    rg.TOTISL4 = TOTISL4;
                    rg.TOTSL6 = TOTSL6;
                    rg.TOTISL6 = TOTISL6;
                    rg.TOTSL7 = TOTSL7;
                    rg.TOTISL7 = TOTISL7;
                    rg.TOTSJL1 = TOTSJL1;
                    rg.TOTISJL1 = TOTISJL1;
                    rgl.Add(rg);
                }
            }
            return rgl;
        }
        public string saveRecovery(RecoveryFrmSalaryDeductionViewModel model, string user_id)
        {
            string sql = "";
            string msg = "";
            sql = "SELECT * FROM RECOVERY_GET WHERE EMPLOYER_CD='" + model.emplyer_name + "' AND EMPLOYER_BRANCH='" + model.emp_unit + "' and book_no='" + model.book_no + "'  AND convert(date, SCH_DATE, 103) = convert(date, '" + model.sch_dt + "', 103)";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                sql = "Delete FROM RECOVERY_GET WHERE EMPLOYER_CD='" + model.emplyer_name + "' AND EMPLOYER_BRANCH='" + model.emp_unit + "' and book_no='" + model.book_no + "'  AND convert(datetime, SCH_DATE, 103) = convert(datetime, '" + model.sch_dt + "', 103)";
                config.Execute_Query(sql);
            }
            Recovery_Schedule rs = new Recovery_Schedule();
            List<Recovery_Schedule> rslst = new List<Recovery_Schedule>();
            rslst = rs.getdetailsForRecoveryFrmSalartDeduction(model.ded_achd, model.branch, model.emplyer_name, model.emp_unit, model.book_no, model.sch_dt);
            int i = 1;
            string vch_no = "";
            foreach (var a in rslst)
            {
                if (i == 1)
                {
                    vch_no = "SLDED" + "00001";
                }
                else
                {
                    string code = (i).ToString().PadLeft(5, '0');
                    vch_no = "SLDED" + code;
                }
                //string vch_no = "SLDED" + i + "00000";
                string mm = Convert.ToDateTime(model.rec_dt).Month.ToString();
                string yy = Convert.ToDateTime(model.rec_dt).Year.ToString();
                try
                {
                    sql = "INSERT INTO RECOVERY_GET(BRANCH_ID, EMPLOYER_CD, EMPLOYER_BRANCH, book_no, EMPLOYEE_ID , SCH_DATE, ac_hd ,vch_pacno ,RECOVERY_DATE ,recovery_mn,recovery_yr,PRIN_AMT,INT_AMT,Create_date,create_time,created_by,device_name )";
                    sql = sql + "VALUES('" + model.branch + "', '" + model.emplyer_name + "', '" + model.emp_unit + "', '" + model.book_no + "','" + a.emp_id + "', convert(datetime, '" + model.sch_dt + "', 103),'" + a.ac_hd + "', '" + a.vch_pacno + "', convert(datetime, '" + model.rec_dt + "', 103), '" + mm + "', '" + yy + "'," + Convert.ToDecimal(a.prin_amt) + ", " + Convert.ToDecimal(a.int_amt) + ", '" + DateTime.Now.ToString("dd/MM/yyyy") + "', '" + DateTime.Now.ToShortTimeString() + "', '" + user_id + "', '" + Environment.MachineName + "')";
                    config.Execute_Query(sql);
                    // msg = "update Done";
                }
                catch (Exception ex)
                {
                    msg = "error " + ex;
                }
                i++;
            }
            return msg;
        }
        public void UpdateLedgerListByEmpId(RecoveryFrmSalaryDeductionViewModel model, string user_id)
        {
            string sql = "Select * from RECOVERY_GET where BRANCH_ID='" + model.branch + "' and book_no='" + model.book_no + "' and EMPLOYEE_ID='" + model.employee_id + "'and EMPLOYER_CD='" + model.emplyer_name + "' and convert(varchar, SCH_DATE, 103) = convert(varchar, '" + model.sch_dt + "', 103) AND convert(varchar, RECOVERY_DATE, 103) = convert(varchar, '" + model.rec_dt + "', 103) AND AC_HD='" + model.ac_hd + "'";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                string qry = "Update RECOVERY_GET set PRIN_AMT=" + model.over_due + ",INT_AMT=" + model.int_amt + ", modified_date = '" + DateTime.Now.ToString("dd/MM/yyyy") + "', modified_time = '" + DateTime.Now.ToShortTimeString() + "', modified_by = '" + user_id + "', m_device_name = '" + Environment.MachineName + "' where convert(varchar, RECOVERY_DATE, 103) = convert(varchar, '" + model.rec_dt + "', 103) and convert(varchar, SCH_DATE, 103) = convert(varchar, '" + model.sch_dt + "', 103)   AND BRANCH_ID='" + model.branch + "' and EMPLOYEE_ID='" + model.employee_id + "'and EMPLOYER_CD='" + model.emplyer_name + "' AND AC_HD='" + model.ac_hd + "'";
                config.Execute_Query(qry);
            }
        }
        public Recovery_Get getPrinBalIntBal(string achd, string branch, string empcd, string empId, string bookno, string sch_date, int empbranch)
        {
            Recovery_Get rg = new Recovery_Get();
            string sql = "Select * from RECOVERY_GET where BRANCH_ID='" + branch + "' and book_no='" + bookno + "' and EMPLOYEE_ID='" + empId + "'and EMPLOYER_CD='" + empcd + "' and EMPLOYER_BRANCH='" + empbranch + "' and convert(varchar, SCH_DATE, 103) = convert(varchar, '" + sch_date + "', 103)  AND AC_HD='" + achd + "'";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    rg.pRIN_aMT = !Convert.IsDBNull(dr["PRIN_AMT"]) ? Convert.ToDecimal(dr["PRIN_AMT"]) : Convert.ToDecimal("00");
                    rg.iNT_aMT = !Convert.IsDBNull(dr["INT_AMT"]) ? Convert.ToDecimal(dr["INT_AMT"]) : Convert.ToDecimal("00");
                }
            }
            return rg;
        }
        public string SaveInLedger(RecoveryFrmSalaryDeductionViewModel model)
        {
            string sql = "";
            string msg = "";
            sql = "SELECT * FROM RECOVERY_GET WHERE EMPLOYER_CD='" + model.emplyer_name + "' AND EMPLOYER_BRANCH='" + model.emp_unit + "' and book_no='" + model.book_no + "'  AND convert(datetime, SCH_DATE, 103) = convert(datetime, '" + model.sch_dt + "', 103)";
            config.singleResult(sql);
            int i = 1;
            string vch_no = "";
            if (config.dt.Rows.Count > 0)
            { 
                foreach (DataRow dr in config.dt.Rows)
                {
                    if (i == 1)
                    {
                        vch_no = "SLDED" + "00001";
                    }
                    else
                    {
                        string code = (i).ToString().PadLeft(5, '0');
                        vch_no = "SLDED" + code;
                    }
                    Recovery_Get rg = new Recovery_Get();
                    rg.aC_hD = Convert.ToString(dr["aC_hD"]);
                    rg.vCH_pACNO = Convert.ToString(dr["vCH_pACNO"]);
                    rg.branch_id = Convert.ToString(dr["branch_id"]);
                    rg.sCH_dATE = Convert.ToDateTime(dr["sch_date"]);
                    rg.rECOVERY_dATE = Convert.ToDateTime(dr["rECOVERY_dATE"]);
                    rg.pRIN_aMT = !Convert.IsDBNull(dr["PRIN_AMT"]) ? Convert.ToDecimal(dr["PRIN_AMT"]) : Convert.ToDecimal("00");
                    rg.iNT_aMT = !Convert.IsDBNull(dr["INT_AMT"]) ? Convert.ToDecimal(dr["INT_AMT"]) : Convert.ToDecimal("00");
                    if (rg.iNT_aMT > 0)
                    {
                        ADD_LEDGER("I" +rg.aC_hD, rg.vCH_pACNO, vch_no, 1, "C", rg.iNT_aMT, "SD", rg.branch_id, rg.sCH_dATE, rg.rECOVERY_dATE, rg.vCH_pACNO);
                    }
                    if (rg.pRIN_aMT > 0)
                    {
                        ADD_LEDGER(rg.aC_hD, rg.vCH_pACNO, vch_no, 2, "C", rg.pRIN_aMT, "SD", model.branch, rg.sCH_dATE, rg.rECOVERY_dATE, rg.vCH_pACNO);
                    }
                    i++;
                }
            }
            return msg;
        }

        //public string saveRecovery(RecoveryFrmSalaryDeductionViewModel model)
        //{
        //    string sql = "";
        //    string msg = "";
        //    sql = "SELECT * FROM RECOVERY_GET WHERE EMPLOYER_CD='" + model.emplyer_name + "' AND EMPLOYER_BRANCH='" + model.emp_unit + "' and book_no='" + model.book_no + "'  AND convert(datetime, SCH_DATE, 103) = convert(datetime, '" + model.sch_dt + "', 103)";
        //    config.singleResult(sql);
        //    if (config.dt.Rows.Count > 0)
        //    {
        //        sql = "Delete FROM RECOVERY_GET WHERE EMPLOYER_CD='" + model.emplyer_name + "' AND EMPLOYER_BRANCH='" + model.emp_unit + "' and book_no='" + model.book_no + "'  AND convert(datetime, SCH_DATE, 103) = convert(datetime, '" + model.sch_dt + "', 103)";
        //        config.Execute_Query(sql);
        //    }
        //    Recovery_Schedule rs = new Recovery_Schedule();
        //    List<Recovery_Schedule> rslst = new List<Recovery_Schedule>();
        //    rslst = rs.getdetailsForRecoveryFrmSalartDeduction(model.ded_achd, model.branch, model.emplyer_name, model.emp_unit, model.book_no, model.sch_dt);
        //    int i = 1;
        //    string vch_no = "";
        //    foreach (var a in rslst)
        //    {

        //        if (i == 1)
        //        {
        //            vch_no = "SLDED" + "00001";

        //        }
        //        else
        //        {
        //            string code = (i).ToString().PadLeft(5, '0');
        //            vch_no = "SLDED" + code;
        //        }
        //        //string vch_no = "SLDED" + i + "00000";
        //        string mm = Convert.ToDateTime(model.rec_dt).Month.ToString();
        //        string yy = Convert.ToDateTime(model.rec_dt).Year.ToString();
        //        try
        //        {
        //            sql = "INSERT INTO RECOVERY_GET(BRANCH_ID, EMPLOYER_CD, EMPLOYER_BRANCH, book_no, EMPLOYEE_ID , SCH_DATE, ac_hd ,vch_pacno ,RECOVERY_DATE ,recovery_mn,recovery_yr,PRIN_AMT,INT_AMT )";
        //            sql = sql + "VALUES('" + model.branch + "', '" + model.emplyer_name + "', '" + model.emp_unit + "', '" + model.book_no + "','" + a.emp_id + "', convert(datetime, '" + model.sch_dt + "', 103),'" + a.ac_hd + "', '" + a.vch_pacno + "', convert(datetime, '" + model.rec_dt + "', 103), '" + mm + "', '" + yy + "'," + Convert.ToDecimal(a.prin_amt) + ", " + Convert.ToDecimal(a.int_amt) + ")";
        //            config.Execute_Query(sql);
        //            msg = "update Done";
        //        }
        //        catch (Exception ex)
        //        {
        //            msg = "error " + ex;
        //        }
        //        if (a.int_amt > 0)
        //        {
        //            ADD_LEDGER("I" + a.ac_hd, a.vch_pacno, vch_no, 1, "C", a.int_amt, "SD", model.branch, model.sch_dt, model.rec_dt, a.vch_pacno);
        //        }
        //        if (a.prin_amt > 0)
        //        {
        //            ADD_LEDGER(a.ac_hd, a.vch_pacno, vch_no, 1, "C", a.prin_amt, "SD", model.branch, model.sch_dt, model.rec_dt, a.vch_pacno);
        //        }
        //        i++;
        //    }
        //    return msg;
        //}
        public void ADD_LEDGER(string XACHD, string xacno, string xvch_no, int XVCH_SRL, string VCH_DRCR, decimal XVCH_AMT, string XINSERT_MODE, string branch, DateTime sch_dt, DateTime rec_dt, string member_id)
        {
            string XLEDGER_TAB, XMAST_FLAG, xvchno;
            decimal LBAL_PRIN = 0;
            decimal LBAL_INT = 0;
            decimal LBAL_AINT = 0;
            decimal LBAL_CH = 0;
            decimal TR_AMT = 0;
            decimal TRAMT_PRIN = 0;
            decimal TRAMT_INT = 0;
            string xledtable = "";
            string xledachd = "";
            string XLEDGER_COL = "";
            string tm = DateTime.Now.ToString("HH:mm:ss");
            string sql = "Select * from ACC_HEAD  where ac_hd='" + XACHD + "'";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                DataRow dr = (DataRow)config.dt.Rows[0];
                xledtable = Convert.ToString(dr["LEDGER_TAB"]);
                xledachd = Convert.ToString(dr["LED_ACHD"]);
                XLEDGER_COL = Convert.ToString(dr["LEDGER_COL"]);
                string QRY1 = "";
                if (Convert.ToString(dr["IFCHARGE"]) != "")
                {
                    QRY1 = "SELECT * FROM " + xledtable + " WHERE branch_id='" + branch + "'AND AC_HD='" + xledachd + "' AND convert(varchar, vch_DATE, 103) = convert(varchar, '" + sch_dt + "', 103)";
                }
                else
                {
                    if (xacno != "")
                    {
                        switch (xledtable)
                        {
                            case "DEPOSIT_LEDGER":
                                QRY1 = "SELECT * FROM " + xledtable + " WHERE branch_id='" + branch + "' AND AC_HD='" + xledachd + "' AND AC_NO='" + xacno + "' ORDER BY VCH_DATE,VCH_NO,VCH_SRL";
                                break;
                            case "SHARE_LEDGER":
                            case "GF_LEDGER":
                            case "TF_LEDGER":
                            case "LICP_LEDGER":
                            case "DIVIDEND_LEDGER":
                            case "RTB_LEDGER":
                                QRY1 = "SELECT * FROM " + xledtable + " WHERE branch_id='" + branch + "' AND MEMBER_ID='" + xacno + "' ORDER BY VCH_DATE,VCH_NO,VCH_SRL";
                                break;
                            case "LOAN_LEDGER":
                                QRY1 = "SELECT * FROM " + xledtable + " WHERE branch_id='" + branch + "' AND AC_HD='" + xledachd + "' AND EMPLOYEE_ID='" + xacno + "' ORDER BY VCH_DATE,VCH_NO,VCH_SRL";
                                break;
                        }
                    }
                }
                if (XLEDGER_COL == "")
                {
                    XLEDGER_COL = "P";
                }
                switch (xledtable.ToUpper())
                {
                    case "TF_LEDGER":
                        config.singleResult(QRY1);
                        if (config.dt.Rows.Count > 0)
                        {
                            DataRow dr1 = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                            string vchdt = Convert.ToDateTime(dr1["VCH_DATE"]).ToString("dd/MM/yyyy");
                            if (Convert.ToDateTime(vchdt) <= Convert.ToDateTime(rec_dt))
                            {
                                LBAL_PRIN = !Convert.IsDBNull(dr1["prin_bal"]) ? Convert.ToDecimal(dr1["prin_bal"]) : Convert.ToDecimal("0");
                                LBAL_INT = !Convert.IsDBNull(dr1["int_bal"]) ? Convert.ToDecimal(dr1["int_bal"]) : Convert.ToDecimal("0");
                            }
                            else
                            {
                                LBAL_PRIN = 0;
                                LBAL_INT = 0;
                            }
                        }
                        else
                        {
                            LBAL_PRIN = 0;
                            LBAL_INT = 0;
                        }
                        TR_AMT = XVCH_AMT;
                        if (XLEDGER_COL == "P")
                        {
                            config.Insert("TF_LEDGER", new Dictionary<String, object>()
                            {
                            { "BRANCH_ID",     branch },
                            { "member_id",   member_id },
                            { "vch_date",Convert.ToDateTime( rec_dt)},
                            { "vch_no",    xvch_no },
                            { "vch_srl",  XVCH_SRL },
                            { "VCH_TYPE",    "T" },
                            { "vch_achd",     XACHD},
                            { "DR_CR",       VCH_DRCR},
                            { "INSERT_MODE",  XINSERT_MODE},
                            { "ref_oth",    "SALARY DEDUCTION (" +Convert.ToDateTime(sch_dt).ToString("MMM")+ "-" +Convert.ToDateTime(sch_dt).Year+ ")" },
                            { "PRIN_AMOUNT",      TR_AMT },
                            { "prin_bal",    LBAL_PRIN + TR_AMT },
                            { "int_bal",  LBAL_INT },
                            });
                        }
                        if (XLEDGER_COL == "I")
                        {
                            config.Insert("TF_LEDGER", new Dictionary<String, object>()
                            {
                            { "BRANCH_ID",     branch },
                            { "member_id",   member_id },
                            { "vch_date",    Convert.ToDateTime( rec_dt)},
                            { "vch_no",    xvch_no },
                            { "vch_srl",  XVCH_SRL },
                            { "VCH_TYPE",    "T" },
                            { "vch_achd",     XACHD},
                            { "DR_CR",       VCH_DRCR},
                            { "INSERT_MODE",  XINSERT_MODE},
                            { "ref_oth",    "SALARY DEDUCTION (" +Convert.ToDateTime(sch_dt).ToString("MMM")+ "-" +Convert.ToDateTime(sch_dt).Year+ ")" },
                            { "INT_AMOUNT",      TR_AMT },
                            { "prin_bal",    LBAL_PRIN  },
                            { "int_bal",  LBAL_INT+ TR_AMT },
                            });
                        }

                        //string sqll = "SELECT * FROM TF_LEDGER WHERE VCH_NO='" + xvch_no + "' AND convert(varchar, VCH_DATE, 103) = convert(varchar, '" + rec_dt + "', 103)";
                        //config.singleResult(sqll);
                        //if (config.dt.Rows.Count > 0)
                        //{
                        //    DataRow dr1 = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                        //    LBAL_PRIN = !Convert.IsDBNull(dr1["prin_bal"]) ? Convert.ToDecimal(dr1["prin_bal"]) : Convert.ToDecimal("0");
                        //    LBAL_INT = !Convert.IsDBNull(dr1["int_bal"]) ? Convert.ToDecimal(dr1["int_bal"]) : Convert.ToDecimal("0");
                        //    TRAMT_PRIN = !Convert.IsDBNull(dr1["PRIN_AMOUNT"]) ? Convert.ToDecimal(dr1["PRIN_AMOUNT"]) : Convert.ToDecimal("0");
                        //    TRAMT_INT = !Convert.IsDBNull(dr1["INT_AMOUNT"]) ? Convert.ToDecimal(dr1["INT_AMOUNT"]) : Convert.ToDecimal("0");
                        //    string drcr = Convert.ToString(dr1["dr_cr"]);
                        //    config.Update("TF_LEDGER", new Dictionary<String, object>()
                        //        {
                        //        { "prin_bal",   LBAL_PRIN  + (drcr=="D"?-TRAMT_PRIN:TRAMT_PRIN)},
                        //        { "int_bal",   LBAL_INT     + (drcr=="D"?TRAMT_INT:-TRAMT_INT)},
                        //        }, new Dictionary<string, object>()
                        //        {
                        //        { "VCH_NO",    xvch_no },
                        //        { "VCH_DATE",    Convert.ToDateTime(rec_dt+" "+tm) },
                        //        });
                        //}
                        break;
                    case "LICP_LEDGER":
                        config.singleResult(QRY1);
                        if (config.dt.Rows.Count > 0)
                        {
                            DataRow dr1 = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                            string vchdt = Convert.ToDateTime(dr1["VCH_DATE"]).ToString("dd/MM/yyyy");
                            if (Convert.ToDateTime(vchdt) <= Convert.ToDateTime(rec_dt))
                            {
                                LBAL_PRIN = !Convert.IsDBNull(dr1["prin_bal"]) ? Convert.ToDecimal(dr1["prin_bal"]) : Convert.ToDecimal("0");
                                LBAL_INT = !Convert.IsDBNull(dr1["int_bal"]) ? Convert.ToDecimal(dr1["int_bal"]) : Convert.ToDecimal("0");
                            }
                            else
                            {
                                LBAL_PRIN = 0;
                                LBAL_INT = 0;
                            }
                        }
                        else
                        {
                            LBAL_PRIN = 0;
                            LBAL_INT = 0;
                        }
                        TR_AMT = XVCH_AMT;
                        if (XLEDGER_COL == "P")
                        {
                            config.Insert("LICP_LEDGER", new Dictionary<String, object>()
                            {
                            { "BRANCH_ID",     branch },
                            { "member_id",   member_id },
                            { "vch_date", Convert.ToDateTime( rec_dt)},
                            { "vch_no",    xvch_no },
                            { "vch_srl",  XVCH_SRL },
                            { "VCH_TYPE",    "T" },
                            { "vch_achd",     XACHD},
                            { "DR_CR",       VCH_DRCR},
                            { "INSERT_MODE",  XINSERT_MODE},
                            { "ref_oth",    "SALARY DEDUCTION (" +Convert.ToDateTime(sch_dt).ToString("MMM")+ "-" +Convert.ToDateTime(sch_dt).Year+ ")" },
                            { "PRIN_AMOUNT",      TR_AMT },
                            { "prin_bal",    LBAL_PRIN + TR_AMT },
                            { "int_bal",  LBAL_INT },
                            });
                        }
                        if (XLEDGER_COL == "I")
                        {
                            config.Insert("LICP_LEDGER", new Dictionary<String, object>()
                            {
                            { "BRANCH_ID",     branch },
                            { "member_id",   member_id },
                            { "vch_date",  Convert.ToDateTime( rec_dt)},
                            { "vch_no",    xvch_no },
                            { "vch_srl",  XVCH_SRL },
                            { "VCH_TYPE",    "T" },
                            { "vch_achd",     XACHD},
                            { "DR_CR",       VCH_DRCR},
                            { "INSERT_MODE",  XINSERT_MODE},
                            { "ref_oth",    "SALARY DEDUCTION (" +Convert.ToDateTime(sch_dt).ToString("MMM")+ "-" +Convert.ToDateTime(sch_dt).Year+ ")" },
                            { "INT_AMOUNT",      TR_AMT },
                            { "prin_bal",    LBAL_PRIN  },
                            { "int_bal",  LBAL_INT+ TR_AMT },
                            });
                        }

                        //sqll = "SELECT * FROM LICP_LEDGER WHERE VCH_NO='" + xacno + "' AND convert(datetime, VCH_DATE, 103) = convert(datetime, '" + rec_dt + "', 103)";
                        //config.singleResult(sqll);
                        //if (config.dt.Rows.Count > 0)
                        //{
                        //    DataRow dr1 = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                        //    LBAL_PRIN = !Convert.IsDBNull(dr1["prin_bal"]) ? Convert.ToDecimal(dr1["prin_bal"]) : Convert.ToDecimal("0");
                        //    LBAL_INT = !Convert.IsDBNull(dr1["int_bal"]) ? Convert.ToDecimal(dr1["int_bal"]) : Convert.ToDecimal("0");

                        //    config.singleResult(QRY1);
                        //    foreach (DataRow drr in config.dt.Rows)
                        //    {
                        //        TRAMT_PRIN = !Convert.IsDBNull(drr["PRIN_AMOUNT"]) ? Convert.ToDecimal(drr["PRIN_AMOUNT"]) : Convert.ToDecimal("0");
                        //        TRAMT_INT = !Convert.IsDBNull(drr["INT_AMOUNT"]) ? Convert.ToDecimal(drr["INT_AMOUNT"]) : Convert.ToDecimal("0");
                        //        string drcr = Convert.ToString(dr1["dr_cr"]);
                        //        config.Update("TF_LEDGER", new Dictionary<String, object>()
                        //        {
                        //        { "prin_bal",   LBAL_PRIN  + (drcr=="D"?-TRAMT_PRIN:TRAMT_PRIN)},
                        //        { "int_bal",   LBAL_INT     + (drcr=="D"?-TRAMT_INT:TRAMT_INT)},
                        //        }, new Dictionary<string, object>()
                        //        {
                        //        { "VCH_NO",    xvch_no },
                        //        { "VCH_DATE",    Convert.ToDateTime(rec_dt) },
                        //        });
                        //        LBAL_PRIN = !Convert.IsDBNull(drr["prin_bal"]) ? Convert.ToDecimal(drr["prin_bal"]) : Convert.ToDecimal("0");
                        //        LBAL_INT = !Convert.IsDBNull(drr["int_bal"]) ? Convert.ToDecimal(drr["int_bal"]) : Convert.ToDecimal("0");
                        //    }

                        //}
                        break;

                    case "RTB_LEDGER":
                        config.singleResult(QRY1);
                        if (config.dt.Rows.Count > 0)
                        {
                            DataRow dr1 = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                            string vchdt = Convert.ToDateTime(dr1["VCH_DATE"]).ToString("dd/MM/yyyy");
                            if (Convert.ToDateTime(vchdt) <= Convert.ToDateTime(rec_dt))
                            {
                                LBAL_PRIN = !Convert.IsDBNull(dr1["prin_bal"]) ? Convert.ToDecimal(dr1["prin_bal"]) : Convert.ToDecimal("0");
                                LBAL_INT = !Convert.IsDBNull(dr1["int_bal"]) ? Convert.ToDecimal(dr1["int_bal"]) : Convert.ToDecimal("0");
                            }
                            else
                            {
                                LBAL_PRIN = 0;
                                LBAL_INT = 0;
                            }
                        }
                        else
                        {
                            LBAL_PRIN = 0;
                            LBAL_INT = 0;
                        }
                        TR_AMT = XVCH_AMT;
                        if (XLEDGER_COL == "P")
                        {
                            config.Insert("RTB_LEDGER", new Dictionary<String, object>()
                            {
                            { "BRANCH_ID",     branch },
                            { "member_id",   member_id },
                            { "vch_date",   Convert.ToDateTime( rec_dt)},
                            { "vch_no",    xvch_no },
                            { "vch_srl",  XVCH_SRL },
                            { "VCH_TYPE",    "T" },
                            { "vch_achd",     XACHD},
                            { "DR_CR",       VCH_DRCR},
                            { "INSERT_MODE",  XINSERT_MODE},
                            { "ref_oth",    "SALARY DEDUCTION (" +Convert.ToDateTime(sch_dt).ToString("MMM")+ "-" +Convert.ToDateTime(sch_dt).Year+ ")" },
                            { "PRIN_AMOUNT",      TR_AMT },
                            { "prin_bal",    LBAL_PRIN + TR_AMT },
                            { "int_bal",  LBAL_INT },
                            });
                        }
                        if (XLEDGER_COL == "I")
                        {
                            config.Insert("RTB_LEDGER", new Dictionary<String, object>()
                            {
                            { "BRANCH_ID",     branch },
                            { "member_id",   member_id },
                            { "vch_date",   Convert.ToDateTime( rec_dt)},
                            { "vch_no",    xvch_no },
                            { "vch_srl",  XVCH_SRL },
                            { "VCH_TYPE",    "T" },
                            { "vch_achd",     XACHD},
                            { "DR_CR",       VCH_DRCR},
                            { "INSERT_MODE",  XINSERT_MODE},
                            { "ref_oth",    "SALARY DEDUCTION (" +Convert.ToDateTime(sch_dt).ToString("MMM")+ "-" +Convert.ToDateTime(sch_dt).Year+ ")" },
                            { "INT_AMOUNT",      TR_AMT },
                            { "prin_bal",    LBAL_PRIN  },
                            { "int_bal",  LBAL_INT+ TR_AMT },
                            });
                        }

                        //sqll = "SELECT * FROM RTB_LEDGER WHERE VCH_NO='" + xvch_no + "' AND convert(varchar, VCH_DATE, 103) = convert(varchar, '" + rec_dt + "', 103)";
                        //config.singleResult(sqll);
                        //if (config.dt.Rows.Count > 0)
                        //{
                        //    DataRow dr1 = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                        //    LBAL_PRIN = !Convert.IsDBNull(dr1["prin_bal"]) ? Convert.ToDecimal(dr1["prin_bal"]) : Convert.ToDecimal("0");
                        //    LBAL_INT = !Convert.IsDBNull(dr1["int_bal"]) ? Convert.ToDecimal(dr1["int_bal"]) : Convert.ToDecimal("0");
                        //    TRAMT_PRIN = !Convert.IsDBNull(dr1["PRIN_AMOUNT"]) ? Convert.ToDecimal(dr1["PRIN_AMOUNT"]) : Convert.ToDecimal("0");
                        //    TRAMT_INT = !Convert.IsDBNull(dr1["INT_AMOUNT"]) ? Convert.ToDecimal(dr1["INT_AMOUNT"]) : Convert.ToDecimal("0");
                        //    string drcr = Convert.ToString(dr1["dr_cr"]);
                        //    config.Update("RTB_LEDGER", new Dictionary<String, object>()
                        //        {
                        //        { "prin_bal",   LBAL_PRIN  + (drcr=="D"?-TRAMT_PRIN:TRAMT_PRIN)},
                        //        { "int_bal",   LBAL_INT + (drcr=="D"?TRAMT_INT:-TRAMT_INT)},
                        //        }, new Dictionary<string, object>()
                        //        {
                        //        { "VCH_NO",    xvch_no },
                        //        { "VCH_DATE",    Convert.ToDateTime(rec_dt+" "+tm) },
                        //        });
                        //}
                        break;
                    case "SHARE_LEDGER":
                        config.singleResult(QRY1);
                        if (config.dt.Rows.Count > 0)
                        {
                            DataRow dr1 = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                            string vchdt = Convert.ToDateTime(dr1["VCH_DATE"]).ToString("dd/MM/yyyy");
                            if (Convert.ToDateTime(vchdt) <= Convert.ToDateTime(rec_dt))
                            {
                                LBAL_PRIN = !Convert.IsDBNull(dr1["prin_bal"]) ? Convert.ToDecimal(dr1["prin_bal"]) : Convert.ToDecimal("0");
                                LBAL_INT = !Convert.IsDBNull(dr1["int_bal"]) ? Convert.ToDecimal(dr1["int_bal"]) : Convert.ToDecimal("0");
                            }
                            else
                            {
                                LBAL_PRIN = 0;
                                LBAL_INT = 0;
                            }
                        }
                        else
                        {
                            LBAL_PRIN = 0;
                            LBAL_INT = 0;
                        }
                        TR_AMT = XVCH_AMT;
                        if (XLEDGER_COL == "P")
                        {
                            config.Insert("SHARE_LEDGER", new Dictionary<String, object>()
                            {
                            { "BRANCH_ID",     branch },
                            { "member_id",   member_id },
                            { "vch_date",      Convert.ToDateTime( rec_dt)},
                            { "vch_no",    xvch_no },
                            { "vch_srl",  XVCH_SRL },
                            { "VCH_TYPE",    "T" },
                            { "vch_achd",     XACHD},
                            { "DR_CR",       VCH_DRCR},
                            { "INSERT_MODE",  XINSERT_MODE},
                            { "ref_oth",    "SALARY DEDUCTION (" +Convert.ToDateTime(sch_dt).ToString("MMM")+ "-" +Convert.ToDateTime(sch_dt).Year+ ")" },
                            { "PRIN_AMOUNT",      TR_AMT },
                            { "prin_bal",    LBAL_PRIN + TR_AMT },
                           // { "int_bal",  LBAL_INT },
                            });
                        }


                        //sqll = "SELECT * FROM SHARE_LEDGER WHERE VCH_NO='" + xacno + "' AND convert(datetime, VCH_DATE, 103) = convert(datetime, '" + rec_dt + "', 103)";
                        //config.singleResult(sqll);
                        //if (config.dt.Rows.Count > 0)
                        //{
                        //    DataRow dr1 = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                        //    LBAL_PRIN = !Convert.IsDBNull(dr1["prin_bal"]) ? Convert.ToDecimal(dr1["prin_bal"]) : Convert.ToDecimal("0");
                        //    //LBAL_INT = !Convert.IsDBNull(dr1["int_bal"]) ? Convert.ToDecimal(dr1["int_bal"]) : Convert.ToDecimal("0");
                        //    TRAMT_PRIN = !Convert.IsDBNull(dr1["PRIN_AMOUNT"]) ? Convert.ToDecimal(dr1["PRIN_AMOUNT"]) : Convert.ToDecimal("0");
                        //    // TRAMT_INT = !Convert.IsDBNull(dr1["INT_AMOUNT"]) ? Convert.ToDecimal(dr1["INT_AMOUNT"]) : Convert.ToDecimal("0");

                        //    config.Update("SHARE_LEDGER", new Dictionary<String, object>()
                        //        {
                        //        { "prin_bal",   LBAL_PRIN  + (Convert.ToString(dr1["dr_cr"])=="D"?-TRAMT_PRIN:TRAMT_PRIN)},
                        //      //  { "int_bal",   LBAL_INT     + (Convert.ToString(dr1["dr_cr"])=="D"?-TRAMT_INT:TRAMT_INT)},
                        //        }, new Dictionary<string, object>()
                        //        {
                        //        { "VCH_NO",    xvch_no },
                        //        { "VCH_DATE",    Convert.ToDateTime(rec_dt+" "+tm) },
                        //        });
                        //}
                        break;
                    case "LOAN_LEDGER":
                        config.singleResult(QRY1);
                        //string dr_cr = "";

                        if (config.dt.Rows.Count > 0)
                        {
                            DataRow dr1 = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                            string vdate = Convert.ToDateTime(dr1["VCH_DATE"]).ToString("dd/MM/yyyy");

                            if (Convert.ToDateTime(vdate) <= Convert.ToDateTime(rec_dt))
                            {
                                LBAL_PRIN = !Convert.IsDBNull(dr1["prin_bal"]) ? Convert.ToDecimal(Convert.ToDecimal(dr1["prin_bal"]).ToString("0.00")) : Convert.ToDecimal("0");
                                LBAL_INT = !Convert.IsDBNull(dr1["INT_DUE"]) ? Convert.ToDecimal(Convert.ToDecimal(dr1["INT_DUE"]).ToString("0.00")) : Convert.ToDecimal("0");
                                LBAL_AINT = !Convert.IsDBNull(dr1["AINT_DUE"]) ? Convert.ToDecimal(Convert.ToDecimal(dr1["AINT_DUE"]).ToString("0.00")) : Convert.ToDecimal("0");
                                LBAL_CH = !Convert.IsDBNull(dr1["ichrg_due"]) ? Convert.ToDecimal(Convert.ToDecimal(dr1["ichrg_due"]).ToString("0.00")) : Convert.ToDecimal("0");
                                //dr_cr = Convert.ToString(dr1["dr_cr"]);
                            }
                            else
                            {
                                LBAL_PRIN = 0;
                                LBAL_INT = 0;
                                LBAL_AINT = 0;
                                LBAL_CH = 0;
                            }
                        }
                        else
                        {
                            LBAL_PRIN = 0;
                            LBAL_INT = 0;
                            LBAL_AINT = 0;
                            LBAL_CH = 0;
                        }
                        TR_AMT = XVCH_AMT;
                        if (XLEDGER_COL == "P")
                        {
                            config.Insert("LOAN_LEDGER", new Dictionary<String, object>()
                            {
                            { "BRANCH_ID",     branch },
                            { "ac_hd",     xledachd },
                            { "EMPLOYEE_ID",   member_id },
                            { "vch_date", Convert.ToDateTime(rec_dt)},
                            { "vch_no",    xvch_no },
                            { "vch_srl",  XVCH_SRL },
                            { "VCH_TYPE",    "T" },
                            { "vch_achd",     XACHD},
                            { "DR_CR",       VCH_DRCR},
                            { "INSERT_MODE",  XINSERT_MODE},
                            { "ref_oth",    "SALARY DEDUCTION (" +Convert.ToDateTime(sch_dt).ToString("MMM")+ "-" +Convert.ToDateTime(sch_dt).Year+ ")" },
                            { "PRIN_AMOUNT",      TR_AMT },
                            //{ "prin_bal",    LBAL_PRIN + (dr_cr=="D"?TR_AMT: -TR_AMT) },
                            { "prin_bal",    LBAL_PRIN + (VCH_DRCR=="D"?TR_AMT: -TR_AMT) },
                            { "INT_DUE",  LBAL_INT },
                            { "AINT_DUE",  LBAL_AINT },
                            { "ichrg_due",  LBAL_CH },
                            });
                        }
                        if (XLEDGER_COL == "I")
                        {
                            try
                            {
                                config.Insert("LOAN_LEDGER", new Dictionary<String, object>()
                                {
                                { "BRANCH_ID",     branch },
                                { "ac_hd",     xledachd },
                                { "EMPLOYEE_ID",   member_id },
                                { "vch_date",    Convert.ToDateTime(rec_dt)},
                                { "vch_no",    xvch_no },
                                { "vch_srl",  XVCH_SRL },
                                { "VCH_TYPE",    "T" },
                                { "vch_achd",     XACHD},
                                { "DR_CR",       VCH_DRCR},
                                { "INSERT_MODE",  XINSERT_MODE},
                                { "ref_oth",    "SALARY DEDUCTION (" +Convert.ToDateTime(sch_dt).ToString("MMM")+ "-" +Convert.ToDateTime(sch_dt).Year+ ")" },
                                { "INT_AMOUNT", Convert.ToDecimal(TR_AMT )},
                                { "prin_bal", Convert.ToDecimal(LBAL_PRIN )},
                                //{ "INT_DUE", Convert.ToDecimal(LBAL_INT  + (dr_cr=="D"?TR_AMT:-TR_AMT)) },
                                { "INT_DUE", Convert.ToDecimal(LBAL_INT  + (VCH_DRCR=="D"?TR_AMT:-TR_AMT)) },
                                { "AINT_DUE",Convert.ToDecimal(LBAL_AINT) },
                                { "ichrg_due",Convert.ToDecimal(LBAL_CH )},
                                });
                            }

                            catch (Exception ex)
                            {

                            }
                        }
                        if (XLEDGER_COL == "A")
                        {
                            config.Insert("LOAN_LEDGER", new Dictionary<String, object>()
                            {
                            { "BRANCH_ID",     branch },
                            { "ac_hd",     xledachd },
                            { "EMPLOYEE_ID",   member_id },
                            { "vch_date",Convert.ToDateTime( rec_dt)},
                            { "vch_no",    xvch_no },
                            { "vch_srl",  XVCH_SRL },
                            { "VCH_TYPE",    "T" },
                            { "vch_achd",     XACHD},
                            { "DR_CR",       VCH_DRCR},
                            { "INSERT_MODE",  XINSERT_MODE},
                            { "ref_oth",    "SALARY DEDUCTION (" +Convert.ToDateTime(sch_dt).ToString("MMM")+ "-" +Convert.ToDateTime(sch_dt).Year+ ")" },
                            { "aint_amount",      TR_AMT },
                            { "prin_bal",    LBAL_PRIN },
                            { "INT_DUE",  LBAL_INT },
                            //{ "AINT_DUE",  LBAL_AINT + (dr_cr=="D"?TR_AMT: -TR_AMT)},
                            { "AINT_DUE",  LBAL_AINT + (VCH_DRCR=="D"?TR_AMT: -TR_AMT)},
                            { "ichrg_due",  LBAL_CH },
                            });
                        }
                        if (XLEDGER_COL == "C")
                        {
                            config.Insert("LOAN_LEDGER", new Dictionary<String, object>()
                            {
                            { "BRANCH_ID",     branch },
                            { "ac_hd",     xledachd },
                            { "EMPLOYEE_ID",   member_id },
                            { "vch_date",Convert.ToDateTime( rec_dt)},
                            { "vch_no",    xvch_no },
                            { "vch_srl",  XVCH_SRL },
                            { "VCH_TYPE",    "T" },
                            { "vch_achd",     XACHD},
                            { "DR_CR",       VCH_DRCR},
                            { "INSERT_MODE",  XINSERT_MODE},
                            { "ref_oth",    "SALARY DEDUCTION (" +Convert.ToDateTime(sch_dt).ToString("MMM")+ "-" +Convert.ToDateTime(sch_dt).Year+ ")" },
                            { "ichrg_amount",      TR_AMT },
                            { "prin_bal",    LBAL_PRIN  },
                            { "INT_DUE",  LBAL_INT },
                            { "AINT_DUE",  LBAL_AINT },
                            //{ "ichrg_due",  LBAL_CH + (dr_cr=="D"?TR_AMT: -TR_AMT)},
                            { "ichrg_due",  LBAL_CH + (VCH_DRCR=="D"?TR_AMT: -TR_AMT)},
                            });
                        }


                        //sqll = "SELECT * FROM LOAN_LEDGER WHERE VCH_NO='" + xvch_no + "' AND convert(varchar, VCH_DATE, 103) = convert(varchar, '" + rec_dt + "', 103)";
                        //config.singleResult(sqll);
                        //if (config.dt.Rows.Count > 0)
                        //{
                        //    DataRow dr1 = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                        //    LBAL_PRIN = !Convert.IsDBNull(dr1["prin_bal"]) ? Convert.ToDecimal(dr1["prin_bal"]) : Convert.ToDecimal("0");
                        //    LBAL_INT = !Convert.IsDBNull(dr1["INT_DUE"]) ? Convert.ToDecimal(dr1["INT_DUE"]) : Convert.ToDecimal("0");
                        //    TRAMT_PRIN = !Convert.IsDBNull(dr1["PRIN_AMOUNT"]) ? Convert.ToDecimal(dr1["PRIN_AMOUNT"]) : Convert.ToDecimal("0");
                        //    TRAMT_INT = !Convert.IsDBNull(dr1["INT_AMOUNT"]) ? Convert.ToDecimal(dr1["INT_AMOUNT"]) : Convert.ToDecimal("0");
                        //    string drcr = Convert.ToString(dr1["dr_cr"]);
                        //    config.Update("LOAN_LEDGER", new Dictionary<String, object>()
                        //        {
                        //        { "prin_bal",   LBAL_PRIN  + (drcr=="D"?TRAMT_PRIN:-TRAMT_PRIN)},
                        //        { "INT_DUE",   LBAL_INT     + (drcr=="D"?TRAMT_INT:-TRAMT_INT)},
                        //        }, new Dictionary<string, object>()
                        //        {
                        //        { "VCH_NO",    xvch_no },
                        //        { "VCH_DATE",    Convert.ToDateTime(rec_dt+" "+tm) },
                        //        });
                        //}
                        break;
                }
            }
        }
    }
}