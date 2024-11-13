using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Amritnagar.Includes;

namespace Amritnagar.Models.Database
{
    public class Ledger
    {
        SQLConfig config = new SQLConfig();
        public string query { get; set; }
        public string table { get; set; }
        public string dr_cr { get; set; }
        public string vch_no { get; set; }
        public int vch_srl { get; set; }
        public string member_id { get; set; }
        public string vch_type { get; set; }
        public string branch_id { get; set; }
        public string ac_hd { get; set; }
        public string vch_achd { get; set; }
        public DateTime vch_date { get; set; }
        public int div_post_year_to { get; set; }
        public string ref_ac_hd { get; set; }
        public string ref_pacno { get; set; }
        public string ref_oth { get; set; }
        public string insert_mode { get; set; }
        public string employee_id { get; set; }
        public decimal prin_amount { get; set; }
        public decimal int_amount { get; set; }
        public decimal prin_bal { get; set; }
        public decimal int_bal { get; set; }
        public decimal tran_amount { get; set; }
        public decimal bal_amount { get; set; }
        public decimal face_val { get; set; }
        public decimal units { get; set; }
        public decimal int_due { get; set; }
        public decimal aint_due { get; set; }
        public decimal ichrg_due { get; set; }
        public decimal ichrg_amt { get; set; }
        public decimal aint_amt { get; set; }
        public decimal int_amt { get; set; }
        public decimal cr_amt { get; set; }
        public decimal dr_amt { get; set; }
        public string chq_no { get; set; }
        public string trns_particular { get; set; }
        public string certificate_no { get; set; }
        public DateTime certificate_date { get; set; }

        public decimal dist_no_from { get; set; }
        public decimal dist_no_to { get; set; }
        public string dist_range { get; set; }


        public Ledger GET_GEN_LEDGER(string XACHD, string xacno, string vch_date, string branchid, int vch_srl)
        {
            int i = 0;
            Ledger ld = new Ledger();
            string sql = "select * from ACC_HEAD WHERE AC_HD='" + XACHD + "'";
            config.singleResult(sql);
            ACC_HEAD ah = new ACC_HEAD();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {                    
                    ah.ledger_tab = Convert.ToString(dr["ledger_tab"]);
                    ah.led_achd = Convert.ToString(dr["led_achd"]);
                    ah.ifcharge = Convert.ToString(dr["ifcharge"]);
                    ld.table = ah.ledger_tab;
                    if (ah.ledger_tab == "")
                    {
                        ld.query = "";
                    }
                    if (ah.ifcharge != "")
                    {
                        sql = "SELECT * FROM " + ah.ledger_tab + " WHERE branch_id='"+ branchid + "' AND AC_HD='" + ah.led_achd + "' AND convert(datetime, VCH_DATE, 103) >= convert(datetime, '" + vch_date.Replace("-", "/") + "', 103)";
                    }
                    else
                    {
                        if (xacno == "")
                        {
                            ld.query = "";
                            return ld;
                        }
                        if (ah.ledger_tab == "DEPOSIT_LEDGER")
                        {
                            sql = "SELECT * FROM " + ah.ledger_tab + " WHERE branch_id='" + branchid + "' AND AC_HD='" + ah.led_achd + "' AND AC_NO='" + xacno + "' ORDER BY VCH_DATE,VCH_NO,VCH_SRL";
                        }                        
                        else if (ah.ledger_tab == "SHARE_LEDGER" || ah.ledger_tab == "GF_LEDGER" || ah.ledger_tab == "TF_LEDGER" || ah.ledger_tab == "DIVIDEND_LEDGER" || ah.ledger_tab == "RTB_LEDGER")
                        {
                            sql = "SELECT * FROM " + ah.ledger_tab + " WHERE branch_id='" + branchid + "' AND MEMBER_ID='" + xacno + "' ORDER BY VCH_DATE,VCH_NO,VCH_SRL";
                        }                       
                        else if (ah.ledger_tab == "LOAN_LEDGER")
                        {
                            sql = "SELECT * FROM " + ah.ledger_tab + " WHERE branch_id='" + branchid + "' AND AC_HD='" + ah.led_achd + "' AND EMPLOYEE_ID='" + xacno + "' ORDER BY VCH_DATE,VCH_NO,VCH_SRL";
                        }                                                                       
                    }
                }
            }
            else
            {
                ld.query = "";
            }
            ld.query = sql;
            return ld;
        }
        public void Delete_LEDGER(string XACHD, string xacno, string vch_date, int vch_srl, string query, string table, string txtvch_No, string branchid)
        {
            string voucher_date = vch_date.Replace("-", "/");
            Ledger ld = new Ledger();
            config.singleResult(query);
            if (config.dt.Rows.Count > 0)
            {
                if (table == "DEPOSIT_LEDGER")
                {
                    string sql = "Delete  FROM " + table + " WHERE branch_id = '"+ branchid + "' AND  AC_NO = '" + xacno + "' AND convert(datetime, VCH_DATE, 103) = convert(datetime, '" + voucher_date + "', 103) AND VCH_NO='" + txtvch_No + "' AND VCH_SRL=" + vch_srl + "";
                    config.Execute_Query(sql);
                }
                if (table == "GF_LEDGER" || table == "TF_LEDGER" || table == "SHARE_LEDGER" || table == "RTB_LEDGER" || table == "DIVIDEND_LEDGER")
                {
                    string sql = "Delete from" + table + " WHERE branch_id='" + branchid + "' AND MEMBER_ID='" + xacno + "' AND convert(datetime, VCH_DATE, 103) = convert(datetime, '" + voucher_date + "', 103) AND VCH_NO='" + txtvch_No + "' AND VCH_SRL= " + vch_srl + "";
                    config.Execute_Query(sql);
                }
                if (table == "LOAN_LEDGER")
                {
                    string sql = "Delete FROM " + table + " WHERE branch_id='" + branchid + "' AND EMPLOYEE_ID='" + xacno + "' AND convert(datetime, VCH_DATE, 103) = convert(datetime, '" + voucher_date + "', 103) AND VCH_NO='" + txtvch_No + "' AND VCH_SRL= " + vch_srl + "";
                    config.Execute_Query(sql);
                }
            }
        }
        public void AddLedger(Vch_Details vd)
        {
            string voucher_date = vd.vch_date.Replace("-", "/");
            string sql = "select * from ACC_HEAD WHERE AC_HD='" + vd.ac_hd + "'";
            ACC_HEAD ah = new ACC_HEAD();
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                DataRow dr = (DataRow)config.dt.Rows[0];
                ah.ledger_tab = Convert.ToString(dr["ledger_tab"]);
                ah.led_achd = Convert.ToString(dr["led_achd"]);
                ah.ifcharge = Convert.ToString(dr["ifcharge"]);
                ah.ledger_col = Convert.ToString(dr["ledger_col"]);
                if (ah.ledger_col == "")
                {
                    ah.ledger_col = "P";
                }
                if (ah.ledger_tab == "DEPOSIT_LEDGER")
                {
                    decimal lbal_prin = 0;
                    decimal lbal_int = 0;
                    Deposit_Ledger dlsb = new Deposit_Ledger();
                    string sql1 = "SELECT * FROM " + ah.ledger_tab + " WHERE branch_id='"+ vd.branch_id +"' AND AC_HD='" + ah.led_achd + "' AND AC_NO='" + vd.vch_pacno + "' AND convert(datetime, VCH_DATE, 103) = convert(datetime, '" + voucher_date + "', 103) ORDER BY VCH_DATE,VCH_NO,VCH_SRL";
                    config.singleResult(sql1);
                    if (config.dt.Rows.Count > 0)
                    {
                        DataRow dr1 = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                        lbal_prin = !Convert.IsDBNull(dr1["prin_bal"]) ? Convert.ToDecimal(dr1["prin_bal"]) : Convert.ToDecimal(00);
                        lbal_int = !Convert.IsDBNull(dr1["int_bal"]) ? Convert.ToDecimal(dr1["int_bal"]) : Convert.ToDecimal(00);
                    }
                    dlsb.branch_id = vd.branch_id;
                    dlsb.ac_hd = ah.led_achd;
                    dlsb.ac_no = vd.vch_pacno;
                    dlsb.vch_date = Convert.ToDateTime(voucher_date);
                    dlsb.vch_no = vd.vch_no;
                    dlsb.vch_srl = Convert.ToInt32(vd.vch_srl);
                    dlsb.vch_type = vd.vch_type;
                    dlsb.vch_achd = vd.ac_hd;
                    dlsb.dr_cr = vd.vch_drcr;
                    dlsb.ref_ac_hd = vd.ref_ac_hd;
                    dlsb.ref_pacno = vd.ref_pacno;
                    dlsb.ref_oth = vd.ref_oth;
                    dlsb.insert_mode = "D";
                    if (ah.ledger_col == "P")
                    {
                        dlsb.prin_amount = vd.vch_amt;
                        dlsb.int_amount = 0;
                        if (vd.vch_drcr == "D")
                        {
                            dlsb.prin_bal = lbal_prin - vd.vch_amt;
                        }
                        else
                        {
                            dlsb.prin_bal = lbal_prin + vd.vch_amt;
                        }
                        dlsb.int_bal = lbal_int;
                    }
                    if (ah.ledger_col == "I")
                    {
                        dlsb.int_amount = vd.vch_amt;
                        dlsb.prin_bal = lbal_prin;
                        if (vd.vch_drcr == "D")
                        {
                            dlsb.int_bal = lbal_int + vd.vch_amt;
                        }
                        else
                        {
                            dlsb.int_bal = lbal_int - vd.vch_amt;
                        }
                    }
                    config.Insert(ah.ledger_tab, new Dictionary<String, object>()
                    {
                        {"branch_id",   dlsb.branch_id },
                        {"ac_hd",   dlsb.ac_hd },
                        {"AC_NO",   dlsb.ac_no },
                        {"vch_date",    dlsb.vch_date },
                        {"vch_no",  dlsb.vch_no },
                        {"vch_srl", dlsb.vch_srl },
                        {"vch_type",    dlsb.vch_type },
                        {"vch_achd", dlsb.vch_achd},
                        {"dr_cr", dlsb.dr_cr},
                        {"ref_ac_hd", dlsb.ref_ac_hd},
                        {"ref_pacno", dlsb.ref_pacno},
                        {"ref_oth", dlsb.ref_oth},
                        {"insert_mode", dlsb.insert_mode},
                        {"prin_amount", dlsb.prin_amount},
                        {"prin_bal", dlsb.prin_bal},
                        {"INT_AMOUNT", dlsb.int_amount},
                        {"int_bal", dlsb.int_bal},
                    });
                    ResetPrinBalIntBalForDepositLedger(ah.ledger_tab, ah.led_achd, vd.vch_pacno, voucher_date);
                }
                if (ah.ledger_tab == "GF_LEDGER" || ah.ledger_tab == "TF_LEDGER" || ah.ledger_tab == "RTB_LEDGER")
                {
                    decimal lbal_prin = 0;
                    decimal lbal_int = 0;
                    Ledger dlsb = new Ledger();
                    string sql1 = "SELECT * FROM " + ah.ledger_tab + " WHERE branch_id='" + vd.branch_id + "' AND ac_hd='" + ah.led_achd + "' and Member_id='" + vd.vch_pacno + "' AND convert(datetime, VCH_DATE, 103) <= convert(datetime, '" + voucher_date + "', 103) ORDER BY VCH_DATE,VCH_NO,VCH_SRL";
                    config.singleResult(sql1);
                    if (config.dt.Rows.Count > 0)
                    {
                        DataRow dr1 = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                        lbal_prin = !Convert.IsDBNull(dr1["prin_bal"]) ? Convert.ToDecimal(dr1["prin_bal"]) : Convert.ToDecimal(00);
                        lbal_int = !Convert.IsDBNull(dr1["int_bal"]) ? Convert.ToDecimal(dr1["int_bal"]) : Convert.ToDecimal(00);
                    }
                    dlsb.branch_id = vd.branch_id;
                    dlsb.ac_hd = ah.led_achd;
                    dlsb.member_id = vd.vch_pacno;
                    dlsb.vch_date = Convert.ToDateTime(vd.vch_date);
                    dlsb.vch_no = vd.vch_no;
                    dlsb.vch_srl = Convert.ToInt32(vd.vch_srl);
                    dlsb.vch_type = Convert.ToString(vd.vch_type);
                    dlsb.vch_achd = vd.ac_hd;
                    dlsb.dr_cr = vd.vch_drcr;
                    dlsb.ref_ac_hd = vd.ref_ac_hd;
                    dlsb.ref_pacno = vd.ref_pacno;
                    dlsb.ref_oth = vd.ref_oth;
                    dlsb.insert_mode = "D";
                    if (ah.ledger_col == "P")
                    {
                        dlsb.prin_amount = vd.vch_amt;
                        dlsb.int_amount = 0;
                        if (vd.vch_drcr == "D")
                        {
                            dlsb.prin_bal = lbal_prin - vd.vch_amt;
                        }
                        else
                        {
                            dlsb.prin_bal = lbal_prin + vd.vch_amt;
                        }
                        dlsb.int_bal = lbal_int;
                    }
                    if (ah.ledger_col == "I")
                    {
                        dlsb.int_amount = vd.vch_amt;
                        dlsb.prin_bal = lbal_prin;
                        dlsb.prin_amount = 0;
                        if (vd.vch_drcr == "D")
                        {
                            dlsb.int_bal = lbal_int + vd.vch_amt;
                        }
                        else
                        {
                            dlsb.int_bal = lbal_int - vd.vch_amt;
                        }
                    }
                    config.Insert(ah.ledger_tab, new Dictionary<String, object>()
                    {
                        {"branch_id",   dlsb.branch_id },
                        //{"ac_hd",       dlsb.ac_hd },
                        {"MEMBER_ID",   dlsb.member_id },
                        {"vch_date",    dlsb.vch_date },
                        {"vch_no",      dlsb.vch_no },
                        {"vch_srl",     dlsb.vch_srl },
                        {"vch_type",    dlsb.vch_type },
                        {"vch_achd",    dlsb.vch_achd},
                        {"dr_cr",       dlsb.dr_cr},
                        {"ref_ac_hd",   dlsb.ref_ac_hd},
                        {"ref_pacno",   dlsb.ref_pacno},
                        {"ref_oth",     dlsb.ref_oth},
                        {"insert_mode", dlsb.insert_mode},
                        {"prin_amount", dlsb.prin_amount},
                        {"prin_bal",    dlsb.prin_bal},
                        {"int_bal",     dlsb.int_bal},
                    });
                    ResetPrinBalIntBalForGF_TF_RTBLedger(ah.ledger_tab, ah.led_achd, vd.vch_pacno, Convert.ToDateTime(vd.vch_date), vd.vch_no);
                }              
                if (ah.ledger_tab == "SHARE_LEDGER" || ah.ledger_tab == "DIVIDEND_LEDGER")
                {
                    decimal lbal_prin = 0;
                    decimal lbal_int = 0;
                    Ledger dlsb = new Ledger();
                    string sql1 = "SELECT * FROM " + ah.ledger_tab + " WHERE branch_id='" + vd.branch_id + "' AND Member_id='" + vd.vch_pacno + "' AND convert(datetime, VCH_DATE, 103) <= convert(datetime, '" + voucher_date + "', 103) ORDER BY VCH_DATE,VCH_NO,VCH_SRL";
                    config.singleResult(sql1);
                    if (config.dt.Rows.Count > 0)
                    {
                        DataRow dr1 = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                        lbal_prin = Convert.ToDecimal(dr1["BAL_AMOUNT"]);
                    }
                    dlsb.branch_id = vd.branch_id;
                    dlsb.member_id = vd.vch_pacno;
                    dlsb.vch_date = Convert.ToDateTime(vd.vch_date);
                    dlsb.vch_no = vd.vch_no;
                    dlsb.vch_srl = Convert.ToInt32(vd.vch_srl);
                    dlsb.vch_type = Convert.ToString(vd.vch_type);
                    dlsb.vch_achd = vd.ac_hd;
                    dlsb.dr_cr = vd.vch_drcr;
                    dlsb.ref_ac_hd = vd.ref_ac_hd;
                    dlsb.ref_pacno = vd.ref_pacno;
                    dlsb.ref_oth = vd.ref_oth;
                    dlsb.insert_mode = "D";
                    dlsb.tran_amount = vd.vch_amt;
                    if (vd.vch_drcr == "D")
                    {
                        dlsb.bal_amount = lbal_prin - vd.vch_amt;
                    }
                    else
                    {
                        dlsb.bal_amount = lbal_prin + vd.vch_amt;
                    }
                    if (ah.ledger_tab == "SHARE_LEDGER")
                    {
                        decimal unit = 0;
                        string QRY = "SELECT * FROM ACC_HEAD WHERE AC_HD='SH' and IS_MISCDEP='Y'";
                        config.singleResult(QRY);
                        if (config.dt.Rows.Count > 0)
                        {
                            DataRow dr2 = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                            dlsb.face_val = Convert.ToDecimal(dr2["miscdep_baseamt"]);
                            unit = (dlsb.tran_amount / dlsb.face_val);
                        }
                        config.Insert("SHARE_LEDGER", new Dictionary<String, object>()
                        {
                            {"branch_id",   dlsb.branch_id },
                            {"member_id",   dlsb.member_id },
                            {"vch_date",    dlsb.vch_date },
                            {"vch_no",      dlsb.vch_no },
                            {"vch_srl",     dlsb.vch_srl },
                            {"vch_type",    dlsb.vch_type },
                            {"vch_achd",    dlsb.vch_achd},
                            {"dr_cr",       dlsb.dr_cr},
                            {"ref_ac_hd",   dlsb.ref_ac_hd},
                            {"ref_pacno",   dlsb.ref_pacno},
                            {"ref_oth",     dlsb.ref_oth},
                            {"insert_mode", dlsb.insert_mode},
                            {"TR_AMOUNT",   dlsb.tran_amount},
                            {"BAL_AMOUNT",  dlsb.bal_amount},
                            {"FACE_VAL",    dlsb.face_val},
                            {"UNITS",       unit}
                        });
                    }
                    else
                    {
                        config.Insert("DIVIDEND_LEDGER", new Dictionary<String, object>()
                        {
                            {"branch_id",   dlsb.branch_id },
                            {"member_id",   dlsb.member_id },
                            {"vch_date",    dlsb.vch_date },
                            {"vch_no",      dlsb.vch_no },
                            {"vch_srl",     dlsb.vch_srl },
                            {"vch_type",    dlsb.vch_type },
                            {"vch_achd",    dlsb.vch_achd},
                            {"dr_cr",       dlsb.dr_cr},
                            {"ref_ac_hd",   dlsb.ref_ac_hd},
                            {"ref_pacno",   dlsb.ref_pacno},
                            {"ref_oth",     dlsb.ref_oth},
                            {"insert_mode", dlsb.insert_mode},
                            {"TR_AMOUNT",   dlsb.tran_amount},
                            {"BAL_AMOUNT", dlsb.bal_amount}
                        });
                    }
                    ResetPrinBalIntBalForShare_DividendLedger(ah.ledger_tab, vd.vch_pacno, Convert.ToDateTime(vd.vch_date), vd.vch_no);
                }
                if (ah.ledger_tab == "LOAN_LEDGER")
                {
                    decimal lbal_prin = 0;
                    decimal lbal_int = 0;
                    decimal lbal_aint = 0;
                    decimal lbal_ch = 0;
                    Ledger dlsb = new Ledger();
                    string sql1 = "SELECT * FROM " + ah.ledger_tab + " WHERE branch_id='" + vd.branch_id + "' AND AC_HD='" + ah.led_achd + "' AND EMPLOYEE_ID='" + vd.vch_pacno + "' AND convert(datetime, VCH_DATE, 103) <= convert(datetime, '" + voucher_date + "', 103) ORDER BY VCH_DATE,VCH_NO,VCH_SRL";
                    config.singleResult(sql1);
                    if (config.dt.Rows.Count > 0)
                    {
                        DataRow dr1 = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                        lbal_prin = Convert.ToDecimal(dr1["prin_bal"]);
                        lbal_int = Convert.ToDecimal(dr1["int_DUE"]);
                        lbal_aint = !Convert.IsDBNull(dr1["AINT_DUE"]) ? Convert.ToDecimal(dr1["AINT_DUE"]) : Convert.ToDecimal(00);
                        lbal_ch = !Convert.IsDBNull(dr1["ichrg_due"]) ? Convert.ToDecimal(dr1["ichrg_due"]) : Convert.ToDecimal(00);
                    }
                    dlsb.branch_id = vd.branch_id;
                    dlsb.ac_hd = ah.led_achd;
                    dlsb.employee_id = vd.vch_pacno;
                    dlsb.vch_date = Convert.ToDateTime(vd.vch_date);
                    dlsb.vch_no = vd.vch_no;
                    dlsb.vch_srl = Convert.ToInt32(vd.vch_srl);
                    dlsb.vch_type = vd.vch_type;
                    dlsb.vch_achd = vd.ac_hd;
                    dlsb.dr_cr = vd.vch_drcr;
                    dlsb.ref_ac_hd = vd.ref_ac_hd;
                    dlsb.ref_pacno = vd.ref_pacno;
                    dlsb.ref_oth = vd.ref_oth;
                    dlsb.insert_mode = "D";
                    //dlsb.no = 0;
                    dlsb.tran_amount = vd.vch_amt;
                    if (ah.ledger_col == "P")
                    {
                        dlsb.prin_amount = dlsb.tran_amount;
                        if (dlsb.dr_cr == "D")
                        {
                            dlsb.prin_bal = lbal_prin + dlsb.tran_amount;
                        }
                        else
                        {
                            dlsb.prin_bal = lbal_prin - dlsb.tran_amount;
                        }
                        dlsb.int_due = lbal_int;
                        dlsb.aint_due = lbal_aint;
                        dlsb.ichrg_due = lbal_ch;
                        dlsb.int_amount = 0;
                    }
                    if (ah.ledger_col == "I")
                    {
                        dlsb.int_amount = dlsb.tran_amount;
                        dlsb.prin_bal = lbal_prin;
                        if (dlsb.dr_cr == "D")
                        {
                            dlsb.int_due = lbal_int + dlsb.tran_amount;
                        }
                        else
                        {
                            dlsb.int_due = lbal_int - dlsb.tran_amount;
                        }
                        dlsb.aint_due = lbal_aint;
                        dlsb.ichrg_due = lbal_ch;
                        dlsb.prin_amount = 0;
                    }
                    config.Insert(ah.ledger_tab, new Dictionary<String, object>()
                    {
                        {"branch_id",   dlsb.branch_id },
                        {"ac_hd",       dlsb.ac_hd },
                        {"EMPLOYEE_ID", dlsb.employee_id },
                        {"vch_date",    dlsb.vch_date },
                        {"vch_no",      dlsb.vch_no },
                        {"vch_srl",     dlsb.vch_srl },
                        {"vch_type",    dlsb.vch_type },
                        {"vch_achd",    dlsb.vch_achd},
                        {"dr_cr",       dlsb.dr_cr},
                        {"ref_ac_hd",   dlsb.ref_ac_hd},
                        {"ref_pacno",   dlsb.ref_pacno},
                        {"ref_oth",     dlsb.ref_oth},
                        {"insert_mode", dlsb.insert_mode},
                        //{"no", 0},
                        {"prin_amount", dlsb.prin_amount},
                        {"prin_bal",    dlsb.prin_bal},
                        {"int_due",     dlsb.int_due},
                        {"int_amount",  dlsb.int_amount},
                        {"AINT_DUE",    dlsb.aint_due},
                        {"ICHRG_DUE",   dlsb.ichrg_due}
                    });
                    ResetPrinBalIntDueForLoanLedgerfor_vch_entry(ah.ledger_tab, ah.led_achd, vd.vch_pacno, Convert.ToDateTime(vd.vch_date), vd.vch_no);
                }
            }
        }
        public void ResetPrinBalIntBalForDepositLedger(string xledtab, string xled_achd, string xacno, string dt)
        {
            Ledger ld = new Ledger();
            decimal lbal_prin = 0;
            decimal lbal_int = 0;
            string sql = "SELECT * FROM " + xledtab + " WHERE AC_HD='" + xled_achd + "' AND AC_NO='" + xacno + "' and convert(datetime, VCH_DATE, 103) < convert(datetime, '" + dt + "', 103) ORDER BY VCH_DATE,VCH_NO,VCH_SRL";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                DataRow ldr = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                lbal_prin = !Convert.IsDBNull(ldr["prin_bal"]) ? Convert.ToDecimal(ldr["prin_bal"]) : Convert.ToDecimal(00);
                lbal_int = !Convert.IsDBNull(ldr["int_bal"]) ? Convert.ToDecimal(ldr["int_bal"]) : Convert.ToDecimal(00);
            }
            sql = "SELECT * FROM " + xledtab + " WHERE AC_HD='" + xled_achd + "' AND AC_NO='" + xacno + "' and convert(datetime, VCH_DATE, 103) >= convert(datetime, '" + dt + "', 103) ORDER BY VCH_DATE,VCH_NO,VCH_SRL";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    ld.dr_cr = Convert.ToString(dr["dr_cr"]);
                    if (ld.dr_cr == "D")
                    {
                        lbal_prin = lbal_prin - (!Convert.IsDBNull(dr["prin_amount"]) ? Convert.ToDecimal(dr["prin_amount"]) : Convert.ToDecimal(00));
                        lbal_int = lbal_int + (!Convert.IsDBNull(dr["INT_AMOUNT"]) ? Convert.ToDecimal(dr["INT_AMOUNT"]) : Convert.ToDecimal(00));
                    }
                    else
                    {
                        lbal_prin = lbal_prin + (!Convert.IsDBNull(dr["prin_amount"]) ? Convert.ToDecimal(dr["prin_amount"]) : Convert.ToDecimal(00));
                        lbal_int = lbal_int - (!Convert.IsDBNull(dr["INT_AMOUNT"]) ? Convert.ToDecimal(dr["INT_AMOUNT"]) : Convert.ToDecimal(00));
                    }
                    string VOUCHER_DATE = ((Convert.ToDateTime(dr["vch_date"])).ToShortDateString()).Replace("-", "/");
                    ld.vch_no = Convert.ToString(dr["vch_no"]);
                    ld.vch_srl = Convert.ToInt32(dr["vch_srl"]);
                    string qry = "Update " + xledtab + " set prin_bal=" + lbal_prin + ",int_bal=" + lbal_int + " where convert(varchar, VCH_DATE, 103) = convert(varchar, '" + VOUCHER_DATE + "', 103)  AND AC_NO='" + xacno + "' and vch_no='" + ld.vch_no + "' and vch_srl=" + ld.vch_srl + "";
                    config.Execute_Query(qry);
                }
            }
        }
        public void ResetPrinBalIntBalForGF_TF_RTBLedger(string xledtab, string xled_achd, string xacno, DateTime vch_dt, string vch_no)
        {
            Ledger ld = new Ledger();
            decimal lbal_prin = 0;
            decimal lbal_int = 0;
            string sql = "SELECT * FROM " + xledtab + " where Member_id='" + xacno + "' and convert(datetime, VCH_DATE, 103) < convert(datetime, '" + vch_dt.ToString("dd/MM/yyyy").Replace("-", "/") + "', 103) ORDER BY VCH_DATE,VCH_NO,VCH_SRL";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                DataRow ldr = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                lbal_prin = !Convert.IsDBNull(ldr["prin_bal"]) ? Convert.ToDecimal(ldr["prin_bal"]) : Convert.ToDecimal(00);
                lbal_int = !Convert.IsDBNull(ldr["int_bal"]) ? Convert.ToDecimal(ldr["int_bal"]) : Convert.ToDecimal(00);
            }
            sql = "SELECT * FROM " + xledtab + " WHERE  Member_id='" + xacno + "' and convert(datetime, VCH_DATE, 103) >= convert(datetime, '" + vch_dt.ToString("dd/MM/yyyy").Replace("-", "/") + "', 103) ORDER BY VCH_DATE,VCH_NO,VCH_SRL";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    ld.dr_cr = Convert.ToString(dr["dr_cr"]);
                    if (ld.dr_cr == "D")
                    {
                        lbal_prin = lbal_prin - (!Convert.IsDBNull(dr["prin_amount"]) ? Convert.ToDecimal(dr["prin_amount"]) : Convert.ToDecimal(00));
                        lbal_int = lbal_int + (!Convert.IsDBNull(dr["INT_AMOUNT"]) ? Convert.ToDecimal(dr["INT_AMOUNT"]) : Convert.ToDecimal(00));
                    }
                    else
                    {
                        lbal_prin = lbal_prin + (!Convert.IsDBNull(dr["prin_amount"]) ? Convert.ToDecimal(dr["prin_amount"]) : Convert.ToDecimal(00));
                        lbal_int = lbal_int - (!Convert.IsDBNull(dr["INT_AMOUNT"]) ? Convert.ToDecimal(dr["INT_AMOUNT"]) : Convert.ToDecimal(00));
                    }
                    string VOUCHER_DATE = (Convert.ToDateTime(dr["vch_date"])).ToString("dd/MM/yyyy").Replace("-", "/");
                    ld.vch_no = Convert.ToString(dr["vch_no"]);
                    ld.vch_srl = Convert.ToInt32(dr["vch_srl"]);
                    string qry = "Update " + xledtab + " set prin_bal=" + lbal_prin + ",int_bal=" + lbal_int + " where convert(varchar, VCH_DATE, 103) = '"+ VOUCHER_DATE + "' AND member_id='" + xacno + "' and vch_no='" + ld.vch_no + "' and vch_srl=" + ld.vch_srl + "";
                    config.Execute_Query(qry);
                }
            }
        }
        public void ResetPrinBalIntBalForShare_DividendLedger(string xledtab, string xacno, DateTime dt, string vch_no)
        {
            Ledger ld = new Ledger();
            decimal lbal_prin = 0;           
            string sql = "SELECT * FROM " + xledtab + " where  Member_id='" + xacno + "' and convert(datetime, VCH_DATE, 103) < convert(datetime, '" + dt.ToString("dd/MM/yyyy").Replace("-", "/") + "', 103) ORDER BY VCH_DATE,VCH_NO,VCH_SRL";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                DataRow ldr = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                lbal_prin = !Convert.IsDBNull(ldr["BAL_AMOUNT"]) ? Convert.ToDecimal(ldr["BAL_AMOUNT"]) : Convert.ToDecimal(00);
            }
            sql = "SELECT * FROM " + xledtab + " WHERE  Member_id='" + xacno + "' and convert(datetime, VCH_DATE, 103) >= convert(datetime, '" + dt.ToString("dd/MM/yyyy").Replace("-", "/") + "', 103) ORDER BY VCH_DATE,VCH_NO,VCH_SRL";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    ld.dr_cr = Convert.ToString(dr["dr_cr"]);
                    if (ld.dr_cr == "D")
                    {
                        lbal_prin = lbal_prin - (!Convert.IsDBNull(dr["tr_amount"]) ? Convert.ToDecimal(dr["tr_amount"]) : Convert.ToDecimal(00));
                    }
                    else
                    {
                        lbal_prin = lbal_prin + (!Convert.IsDBNull(dr["tr_amount"]) ? Convert.ToDecimal(dr["tr_amount"]) : Convert.ToDecimal(00));
                    }
                    string VOUCHER_DATE = ((Convert.ToDateTime(dr["vch_date"])).ToShortDateString()).Replace("-", "/");
                    ld.vch_no = Convert.ToString(dr["vch_no"]);
                    ld.vch_srl = Convert.ToInt32(dr["vch_srl"]);
                    string qry = "Update " + xledtab + " set BAL_AMOUNT=" + lbal_prin + " where convert(varchar, VCH_DATE, 103) = '" + VOUCHER_DATE + "' and Member_id='" + xacno + "' and vch_no='" + ld.vch_no + "' and vch_srl=" + ld.vch_srl + "";
                    config.Execute_Query(qry);                   
                }
            }
        }
        public void ResetPrinBalIntDueForLoanLedgerfor_vch_entry(string xledtab, string xled_achd, string xacno, DateTime dt, string vch_no)
        {
            Ledger ld = new Ledger();
            decimal lbal_prin = 0;
            decimal lbal_int = 0;
            int i = 1;
            //decimal Tr_prin = 0;
            //decimal Tr_int = 0;
            string sql = "SELECT * FROM " + xledtab + " where  AC_HD='" + xled_achd + "' AND EMPLOYEE_ID='" + xacno + "' and convert(datetime, VCH_DATE, 103) < convert(datetime, '" + dt.ToString("dd/MM/yyyy").Replace("-", "/") + "', 103) ORDER BY VCH_DATE,VCH_NO,VCH_SRL";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                DataRow ldr = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                lbal_prin = !Convert.IsDBNull(ldr["prin_bal"]) ? Convert.ToDecimal(ldr["prin_bal"]) : Convert.ToDecimal(00);
                lbal_int = !Convert.IsDBNull(ldr["int_Due"]) ? Convert.ToDecimal(ldr["int_Due"]) : Convert.ToDecimal(00);
            }
            sql = "SELECT * FROM " + xledtab + " WHERE AC_HD='" + xled_achd + "' AND EMPLOYEE_ID='" + xacno + "' and convert(datetime, VCH_DATE, 103) >= convert(datetime, '" + dt.ToString("dd/MM/yyyy").Replace("-", "/") + "', 103) ORDER BY VCH_DATE,VCH_NO,VCH_SRL";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    ld.dr_cr = Convert.ToString(dr["dr_cr"]);
                    if (ld.dr_cr == "D")
                    {
                        lbal_prin = lbal_prin + (!Convert.IsDBNull(dr["prin_amount"]) ? Convert.ToDecimal(dr["prin_amount"]) : Convert.ToDecimal(00));
                        lbal_int = lbal_int + (!Convert.IsDBNull(dr["INT_AMOUNT"]) ? Convert.ToDecimal(dr["INT_AMOUNT"]) : Convert.ToDecimal(00));
                    }
                    else
                    {
                        lbal_prin = lbal_prin - (!Convert.IsDBNull(dr["prin_amount"]) ? Convert.ToDecimal(dr["prin_amount"]) : Convert.ToDecimal(00));
                        lbal_int = lbal_int - (!Convert.IsDBNull(dr["INT_AMOUNT"]) ? Convert.ToDecimal(dr["INT_AMOUNT"]) : Convert.ToDecimal(00));
                    }
                    string VOUCHER_DATE = ((Convert.ToDateTime(dr["vch_date"])).ToShortDateString()).Replace("-", "/");
                    ld.vch_no = Convert.ToString(dr["vch_no"]);
                    ld.vch_srl = Convert.ToInt32(dr["vch_srl"]);
                    string qry = "Update " + xledtab + " set prin_bal=" + lbal_prin + ",int_Due=" + lbal_int + " where convert(varchar, VCH_DATE, 103) = '" + VOUCHER_DATE + "' AND EMPLOYEE_ID='" + xacno + "'and vch_no='" + ld.vch_no + "' and vch_srl=" + ld.vch_srl + "";
                    config.Execute_Query(qry);
                }
            }
        }
        public Ledger GET_LEDGER_QRY(string XACHD, string xacno, string XLED, string branch)
        {
            Ledger ld = new Ledger();
            string sql = string.Empty;           
            if(XLED == "LOAN_LEDGER")
            {
                sql = "select * from LOAN_ledger where branch_id='" + branch + "' and ";
                sql = sql + "AC_HD='" + XACHD + "' AND EMPLOYEE_ID='" + xacno + "'";
                sql = sql + "ORDER BY VCH_DATE,VCH_NO,VCH_SRL";
            }
            if (XLED == "SHARE_LEDGER" || XLED == "GF_LEDGER" || XLED == "TF_LEDGER" || XLED == "DIVIDEND_LEDGER")
            {
                sql = "select * from " + XLED + " where branch_id='" + branch + "' ";
                sql = sql + "AND MEMBER_ID='" + xacno + "' ";
                sql = sql + "ORDER BY VCH_DATE,VCH_NO,VCH_SRL";
            }
            ld.query = sql;
            return ld;
        }
        public Ledger GET_GEN_LEDGER_For_TVCH(string XACHD, string xacno, string vch_date, string branchid, int vch_srl)
        {
            int i = 0;
            Ledger ld = new Ledger();
            string sql = "select * from ACC_HEAD WHERE AC_HD='" + XACHD + "'";
            config.singleResult(sql);
            ACC_HEAD ah = new ACC_HEAD();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    ah.ledger_tab = Convert.ToString(dr["ledger_tab"]);
                    ah.led_achd = Convert.ToString(dr["led_achd"]);
                    ah.ifcharge = Convert.ToString(dr["ifcharge"]);
                    ld.table = ah.ledger_tab;
                    if (ah.ledger_tab == "")
                    {
                        ld.query = "";
                    }
                    if (ah.ifcharge != "")
                    {
                        sql = "SELECT * FROM " + ah.ledger_tab + " WHERE branch_id='" + branchid + "' AND AC_HD='" + ah.led_achd + "' AND convert(datetime, VCH_DATE, 103) >= convert(datetime, '" + vch_date.Replace("-", "/") + "', 103)";
                    }
                    else
                    {
                        if (xacno == "")
                        {
                            ld.query = "";
                            return ld;
                        }
                        if (ah.ledger_tab == "DEPOSIT_LEDGER")
                        {
                            sql = "SELECT * FROM " + ah.ledger_tab + " WHERE branch_id='" + branchid + "' AND AC_HD='" + ah.led_achd + "' AND AC_NO='" + xacno + "' ORDER BY VCH_DATE,VCH_NO,VCH_SRL";
                        }
                        else if (ah.ledger_tab == "SHARE_LEDGER" || ah.ledger_tab == "GF_LEDGER" || ah.ledger_tab == "TF_LEDGER" || ah.ledger_tab == "DIVIDEND_LEDGER" || ah.ledger_tab == "RTB_LEDGER")
                        {
                            sql = "SELECT * FROM " + ah.ledger_tab + " WHERE branch_id='" + branchid + "' AND MEMBER_ID='" + xacno + "' ORDER BY VCH_DATE,VCH_NO,VCH_SRL";
                        }
                        else if (ah.ledger_tab == "LOAN_LEDGER")
                        {
                            sql = "SELECT * FROM " + ah.ledger_tab + " WHERE branch_id='" + branchid + "' AND AC_HD='" + ah.led_achd + "' AND EMPLOYEE_ID='" + xacno + "' ORDER BY VCH_DATE,VCH_NO,VCH_SRL";
                        }
                    }
                }
            }
            else
            {
                ld.query = "";
            }
            ld.query = sql;
            return ld;
        }
        public void Delete_LEDGER_For_TVCH(string XACHD, string xacno, string vch_date, int vch_srl, string query, string table, string txtvch_No, string branchid)
        {
            string voucher_date = vch_date.Replace("-", "/");
            Ledger ld = new Ledger();
            config.singleResult(query);
            if (config.dt.Rows.Count > 0)
            {
                if (table == "DEPOSIT_LEDGER")
                {
                    string sql = "Delete  FROM " + table + " WHERE branch_id = '" + branchid + "' AND  AC_NO = '" + xacno + "' AND convert(datetime, VCH_DATE, 103) = convert(datetime, '" + voucher_date + "', 103) AND VCH_NO='" + txtvch_No + "' AND VCH_SRL=" + vch_srl + "";
                    config.Execute_Query(sql);
                }
                if (table == "GF_LEDGER" || table == "TF_LEDGER" || table == "SHARE_LEDGER" || table == "RTB_LEDGER" || table == "DIVIDEND_LEDGER")
                {
                    string sql = "Delete from" + table + " WHERE branch_id='" + branchid + "' AND MEMBER_ID='" + xacno + "' AND convert(datetime, VCH_DATE, 103) = convert(datetime, '" + voucher_date + "', 103) AND VCH_NO='" + txtvch_No + "' AND VCH_SRL= " + vch_srl + "";
                    config.Execute_Query(sql);
                }
                if (table == "LOAN_LEDGER")
                {
                    string sql = "Delete FROM " + table + " WHERE branch_id='" + branchid + "' AND EMPLOYEE_ID='" + xacno + "' AND convert(datetime, VCH_DATE, 103) = convert(datetime, '" + voucher_date + "', 103) AND VCH_NO='" + txtvch_No + "' AND VCH_SRL= " + vch_srl + "";
                    config.Execute_Query(sql);
                }
            }
        }

        public void AddLedger_For_TVCH(TVCH_DETAIL vd)
        {
            string voucher_date = vd.trn_date.ToString("dd/MM/yyyy").Replace("-", "/");
            string sql = "select * from ACC_HEAD WHERE AC_HD='" + vd.ac_hd + "'";
            ACC_HEAD ah = new ACC_HEAD();
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                DataRow dr = (DataRow)config.dt.Rows[0];
                ah.ledger_tab = Convert.ToString(dr["ledger_tab"]);
                ah.led_achd = Convert.ToString(dr["led_achd"]);
                ah.ifcharge = Convert.ToString(dr["ifcharge"]);
                ah.ledger_col = Convert.ToString(dr["ledger_col"]);
                if (ah.ledger_col == "")
                {
                    ah.ledger_col = "P";
                }                
                if (ah.ledger_tab == "GF_LEDGER" || ah.ledger_tab == "TF_LEDGER")
                {
                    decimal lbal_prin = 0;
                    decimal lbal_int = 0;
                    Ledger dlsb = new Ledger();
                    string sql1 = "SELECT * FROM " + ah.ledger_tab + " WHERE branch_id='" + vd.branch_id + "' AND ac_hd='" + ah.led_achd + "' and Member_id='" + vd.vch_pacno + "' AND convert(datetime, VCH_DATE, 103) <= convert(datetime, '" + voucher_date + "', 103) ORDER BY VCH_DATE,VCH_NO,VCH_SRL";
                    config.singleResult(sql1);
                    if (config.dt.Rows.Count > 0)
                    {
                        DataRow dr1 = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                        lbal_prin = !Convert.IsDBNull(dr1["prin_bal"]) ? Convert.ToDecimal(dr1["prin_bal"]) : Convert.ToDecimal(00);
                        lbal_int = !Convert.IsDBNull(dr1["int_bal"]) ? Convert.ToDecimal(dr1["int_bal"]) : Convert.ToDecimal(00);
                    }
                    dlsb.branch_id = vd.branch_id;
                    dlsb.ac_hd = ah.led_achd;
                    dlsb.member_id = vd.vch_pacno;
                    dlsb.vch_date = Convert.ToDateTime(vd.trn_date);
                    dlsb.vch_no = vd.trn_no;
                    dlsb.vch_srl = 1;
                    dlsb.vch_type = "C";
                    dlsb.vch_achd = vd.ac_hd;
                    dlsb.dr_cr = "C";                   
                    dlsb.insert_mode = "MR";
                    if (ah.ledger_col == "P")
                    {
                        dlsb.prin_amount = vd.vch_amt;
                        dlsb.int_amount = 0;
                        if (vd.vch_drcr == "D")
                        {
                            dlsb.prin_bal = lbal_prin - vd.vch_amt;
                        }
                        else
                        {
                            dlsb.prin_bal = lbal_prin + vd.vch_amt;
                        }
                        dlsb.int_bal = lbal_int;
                    }
                    if (ah.ledger_col == "I")
                    {
                        dlsb.int_amount = vd.vch_amt;
                        dlsb.prin_bal = lbal_prin;
                        dlsb.prin_amount = 0;
                        if (vd.vch_drcr == "D")
                        {
                            dlsb.int_bal = lbal_int + vd.vch_amt;
                        }
                        else
                        {
                            dlsb.int_bal = lbal_int - vd.vch_amt;
                        }
                    }
                    config.Insert(ah.ledger_tab, new Dictionary<String, object>()
                    {
                        {"branch_id",   dlsb.branch_id },                   
                        {"MEMBER_ID",   dlsb.member_id },
                        {"vch_date",    dlsb.vch_date },
                        {"vch_no",      dlsb.vch_no },
                        {"vch_srl",     dlsb.vch_srl },
                        {"vch_type",    dlsb.vch_type },
                        {"vch_achd",    dlsb.vch_achd},
                        {"dr_cr",       dlsb.dr_cr},                       
                        {"insert_mode", dlsb.insert_mode},
                        {"prin_amount", dlsb.prin_amount},
                        {"prin_bal",    dlsb.prin_bal},
                        {"int_bal",     dlsb.int_bal},
                    });
                    ResetPrinBalIntBalForGF_TF__For_TVCH(ah.ledger_tab, ah.led_achd, vd.vch_pacno, Convert.ToDateTime(vd.trn_date), vd.trn_no);
                }
                if (ah.ledger_tab == "SHARE_LEDGER" || ah.ledger_tab == "DIVIDEND_LEDGER")
                {
                    decimal lbal_prin = 0;
                    decimal lbal_int = 0;
                    Ledger dlsb = new Ledger();
                    string sql1 = "SELECT * FROM " + ah.ledger_tab + " WHERE branch_id='" + vd.branch_id + "' AND Member_id='" + vd.vch_pacno + "' AND convert(datetime, VCH_DATE, 103) <= convert(datetime, '" + voucher_date + "', 103) ORDER BY VCH_DATE,VCH_NO,VCH_SRL";
                    config.singleResult(sql1);
                    if (config.dt.Rows.Count > 0)
                    {
                        DataRow dr1 = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                        lbal_prin = Convert.ToDecimal(dr1["BAL_AMOUNT"]);
                    }
                    dlsb.branch_id = vd.branch_id;
                    dlsb.member_id = vd.vch_pacno;
                    dlsb.vch_date = Convert.ToDateTime(vd.trn_date);
                    dlsb.vch_no = vd.trn_no;
                    dlsb.vch_srl = 1;
                    dlsb.vch_type = "C";
                    dlsb.vch_achd = vd.ac_hd;
                    dlsb.dr_cr = vd.vch_drcr;                   
                    dlsb.insert_mode = "MR";
                    dlsb.tran_amount = vd.vch_amt;
                    if (vd.vch_drcr == "D")
                    {
                        dlsb.bal_amount = lbal_prin - vd.vch_amt;
                    }
                    else
                    {
                        dlsb.bal_amount = lbal_prin + vd.vch_amt;
                    }
                    if (ah.ledger_tab == "SHARE_LEDGER")
                    {
                        decimal unit = 0;
                        string QRY = "SELECT * FROM ACC_HEAD WHERE AC_HD='SH' and IS_MISCDEP='Y'";
                        config.singleResult(QRY);
                        if (config.dt.Rows.Count > 0)
                        {
                            DataRow dr2 = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                            dlsb.face_val = Convert.ToDecimal(dr2["miscdep_baseamt"]);
                            unit = (dlsb.tran_amount / dlsb.face_val);
                        }
                        config.Insert("SHARE_LEDGER", new Dictionary<String, object>()
                        {
                            {"branch_id",   dlsb.branch_id },
                            {"member_id",   dlsb.member_id },
                            {"vch_date",    dlsb.vch_date },
                            {"vch_no",      dlsb.vch_no },
                            {"vch_srl",     dlsb.vch_srl },
                            {"vch_type",    dlsb.vch_type },
                            {"vch_achd",    dlsb.vch_achd},
                            {"dr_cr",       dlsb.dr_cr},                            
                            {"insert_mode", dlsb.insert_mode},
                            {"TR_AMOUNT",   dlsb.tran_amount},
                            {"BAL_AMOUNT",  dlsb.bal_amount},
                            {"FACE_VAL",    dlsb.face_val},
                            {"UNITS",       unit}
                        });
                    }
                    else
                    {
                        config.Insert("DIVIDEND_LEDGER", new Dictionary<String, object>()
                        {
                            {"branch_id",   dlsb.branch_id },
                            {"member_id",   dlsb.member_id },
                            {"vch_date",    dlsb.vch_date },
                            {"vch_no",      dlsb.vch_no },
                            {"vch_srl",     dlsb.vch_srl },
                            {"vch_type",    dlsb.vch_type },
                            {"vch_achd",    dlsb.vch_achd},
                            {"dr_cr",       dlsb.dr_cr},                            
                            {"insert_mode", dlsb.insert_mode},
                            {"TR_AMOUNT",   dlsb.tran_amount},
                            {"BAL_AMOUNT", dlsb.bal_amount}
                        });
                    }
                    ResetPrinBalIntBalForShare_DividendLedger_For_TVCH(ah.ledger_tab, vd.vch_pacno, Convert.ToDateTime(vd.trn_date), vd.trn_no);
                }
                if (ah.ledger_tab == "LOAN_LEDGER")
                {
                    decimal lbal_prin = 0;
                    decimal lbal_int = 0;
                    decimal lbal_aint = 0;
                    decimal lbal_ch = 0;
                    Ledger dlsb = new Ledger();
                    string sql1 = "SELECT * FROM " + ah.ledger_tab + " WHERE branch_id='" + vd.branch_id + "' AND AC_HD='" + ah.led_achd + "' AND EMPLOYEE_ID='" + vd.vch_pacno + "' AND convert(datetime, VCH_DATE, 103) <= convert(datetime, '" + voucher_date + "', 103) ORDER BY VCH_DATE,VCH_NO,VCH_SRL";
                    config.singleResult(sql1);
                    if (config.dt.Rows.Count > 0)
                    {
                        DataRow dr1 = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                        //lbal_prin = Convert.ToDecimal(dr1["prin_bal"]);
                        lbal_prin = !Convert.IsDBNull(dr1["prin_bal"]) ? Convert.ToDecimal(dr1["prin_bal"]) : Convert.ToDecimal(00);
                        //lbal_int = Convert.ToDecimal(dr1["int_DUE"]);
                        lbal_int = !Convert.IsDBNull(dr1["int_DUE"]) ? Convert.ToDecimal(dr1["int_DUE"]) : Convert.ToDecimal(00);
                        lbal_aint = !Convert.IsDBNull(dr1["AINT_DUE"]) ? Convert.ToDecimal(dr1["AINT_DUE"]) : Convert.ToDecimal(00);
                        lbal_ch = !Convert.IsDBNull(dr1["ichrg_due"]) ? Convert.ToDecimal(dr1["ichrg_due"]) : Convert.ToDecimal(00);
                    }
                    dlsb.branch_id = vd.branch_id;
                    dlsb.ac_hd = ah.led_achd;
                    dlsb.employee_id = vd.vch_pacno;
                    dlsb.vch_date = Convert.ToDateTime(vd.trn_date);
                    dlsb.vch_no = vd.trn_no;
                    dlsb.vch_srl = 1;
                    dlsb.vch_type = "C";
                    dlsb.vch_achd = vd.ac_hd;
                    dlsb.dr_cr = "C";                   
                    dlsb.insert_mode = "MR";
                    //dlsb.no = 0;
                    dlsb.tran_amount = vd.vch_amt;
                    if (ah.ledger_col == "P")
                    {
                        dlsb.prin_amount = dlsb.tran_amount;
                        if (dlsb.dr_cr == "D")
                        {
                            dlsb.prin_bal = lbal_prin + dlsb.tran_amount;
                        }
                        else
                        {
                            dlsb.prin_bal = lbal_prin - dlsb.tran_amount;
                        }
                        dlsb.int_due = lbal_int;
                        dlsb.aint_due = lbal_aint;
                        dlsb.ichrg_due = lbal_ch;
                        dlsb.int_amount = 0;
                    }
                    if (ah.ledger_col == "I")
                    {
                        dlsb.int_amount = dlsb.tran_amount;
                        dlsb.prin_bal = lbal_prin;
                        if (dlsb.dr_cr == "D")
                        {
                            dlsb.int_due = lbal_int + dlsb.tran_amount;
                        }
                        else
                        {
                            dlsb.int_due = lbal_int - dlsb.tran_amount;
                        }
                        dlsb.aint_due = lbal_aint;
                        dlsb.ichrg_due = lbal_ch;
                        dlsb.prin_amount = 0;
                    }
                    config.Insert(ah.ledger_tab, new Dictionary<String, object>()
                    {
                        {"branch_id",   dlsb.branch_id },
                        {"ac_hd",       dlsb.ac_hd },
                        {"EMPLOYEE_ID", dlsb.employee_id },
                        {"vch_date",    dlsb.vch_date },
                        {"vch_no",      dlsb.vch_no },
                        {"vch_srl",     dlsb.vch_srl },
                        {"vch_type",    dlsb.vch_type },
                        {"vch_achd",    dlsb.vch_achd},
                        {"dr_cr",       dlsb.dr_cr},                        
                        {"insert_mode", dlsb.insert_mode},
                        //{"no", 0},
                        {"prin_amount", dlsb.prin_amount},
                        {"prin_bal",    dlsb.prin_bal},
                        {"int_due",     dlsb.int_due},
                        {"int_amount",  dlsb.int_amount},                       
                    });
                    ResetPrinBalIntDueForLoanLedger_For_TVCH(ah.ledger_tab, ah.led_achd, vd.vch_pacno, Convert.ToDateTime(vd.trn_date), vd.trn_no);
                }
            }
        }
        public void ResetPrinBalIntBalForGF_TF__For_TVCH(string xledtab, string xled_achd, string xacno, DateTime vch_dt, string vch_no)
        {
            Ledger ld = new Ledger();
            decimal lbal_prin = 0;
            decimal lbal_int = 0;
            string sql = "SELECT * FROM " + xledtab + " where Member_id='" + xacno + "' and convert(datetime, VCH_DATE, 103) < convert(datetime, '" + vch_dt.ToString("dd/MM/yyyy").Replace("-", "/") + "', 103) ORDER BY VCH_DATE,VCH_NO,VCH_SRL";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                DataRow ldr = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                lbal_prin = !Convert.IsDBNull(ldr["prin_bal"]) ? Convert.ToDecimal(ldr["prin_bal"]) : Convert.ToDecimal(00);
                lbal_int = !Convert.IsDBNull(ldr["int_bal"]) ? Convert.ToDecimal(ldr["int_bal"]) : Convert.ToDecimal(00);
            }
            sql = "SELECT * FROM " + xledtab + " WHERE  Member_id='" + xacno + "' and convert(datetime, VCH_DATE, 103) >= convert(datetime, '" + vch_dt.ToString("dd/MM/yyyy").Replace("-", "/") + "', 103) ORDER BY VCH_DATE,VCH_NO,VCH_SRL";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    ld.dr_cr = Convert.ToString(dr["dr_cr"]);
                    if (ld.dr_cr == "D")
                    {
                        lbal_prin = lbal_prin - (!Convert.IsDBNull(dr["prin_amount"]) ? Convert.ToDecimal(dr["prin_amount"]) : Convert.ToDecimal(00));
                        lbal_int = lbal_int + (!Convert.IsDBNull(dr["INT_AMOUNT"]) ? Convert.ToDecimal(dr["INT_AMOUNT"]) : Convert.ToDecimal(00));
                    }
                    else
                    {
                        lbal_prin = lbal_prin + (!Convert.IsDBNull(dr["prin_amount"]) ? Convert.ToDecimal(dr["prin_amount"]) : Convert.ToDecimal(00));
                        lbal_int = lbal_int - (!Convert.IsDBNull(dr["INT_AMOUNT"]) ? Convert.ToDecimal(dr["INT_AMOUNT"]) : Convert.ToDecimal(00));
                    }
                    string VOUCHER_DATE = (Convert.ToDateTime(dr["vch_date"])).ToString("dd/MM/yyyy").Replace("-", "/");
                    ld.vch_no = Convert.ToString(dr["vch_no"]);
                    ld.vch_srl = Convert.ToInt32(dr["vch_srl"]);
                    string qry = "Update " + xledtab + " set prin_bal=" + lbal_prin + ",int_bal=" + lbal_int + " where convert(varchar, VCH_DATE, 103) = '" + VOUCHER_DATE + "' AND member_id='" + xacno + "' and vch_no='" + ld.vch_no + "' and vch_srl=" + ld.vch_srl + "";
                    config.Execute_Query(qry);
                }
            }
        }
        public void ResetPrinBalIntBalForShare_DividendLedger_For_TVCH(string xledtab, string xacno, DateTime dt, string vch_no)
        {
            Ledger ld = new Ledger();
            decimal lbal_prin = 0;
            string sql = "SELECT * FROM " + xledtab + " where  Member_id='" + xacno + "' and convert(datetime, VCH_DATE, 103) < convert(datetime, '" + dt.ToString("dd/MM/yyyy").Replace("-", "/") + "', 103) ORDER BY VCH_DATE,VCH_NO,VCH_SRL";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                DataRow ldr = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                lbal_prin = !Convert.IsDBNull(ldr["BAL_AMOUNT"]) ? Convert.ToDecimal(ldr["BAL_AMOUNT"]) : Convert.ToDecimal(00);
            }
            sql = "SELECT * FROM " + xledtab + " WHERE  Member_id='" + xacno + "' and convert(datetime, VCH_DATE, 103) >= convert(datetime, '" + dt.ToString("dd/MM/yyyy").Replace("-", "/") + "', 103) ORDER BY VCH_DATE,VCH_NO,VCH_SRL";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    ld.dr_cr = Convert.ToString(dr["dr_cr"]);
                    if (ld.dr_cr == "D")
                    {
                        lbal_prin = lbal_prin - (!Convert.IsDBNull(dr["tr_amount"]) ? Convert.ToDecimal(dr["tr_amount"]) : Convert.ToDecimal(00));
                    }
                    else
                    {
                        lbal_prin = lbal_prin + (!Convert.IsDBNull(dr["tr_amount"]) ? Convert.ToDecimal(dr["tr_amount"]) : Convert.ToDecimal(00));
                    }
                    string VOUCHER_DATE = ((Convert.ToDateTime(dr["vch_date"])).ToShortDateString()).Replace("-", "/");
                    ld.vch_no = Convert.ToString(dr["vch_no"]);
                    ld.vch_srl = Convert.ToInt32(dr["vch_srl"]);
                    string qry = "Update " + xledtab + " set BAL_AMOUNT=" + lbal_prin + " where convert(varchar, VCH_DATE, 103) = '" + VOUCHER_DATE + "' and Member_id='" + xacno + "' and vch_no='" + ld.vch_no + "' and vch_srl=" + ld.vch_srl + "";
                    config.Execute_Query(qry);
                }
            }
        }
        public void ResetPrinBalIntDueForLoanLedger_For_TVCH(string xledtab, string xled_achd, string xacno, DateTime dt, string vch_no)
        {
            Ledger ld = new Ledger();
            decimal lbal_prin = 0;
            decimal lbal_int = 0;
            int i = 1;
            //decimal Tr_prin = 0;
            //decimal Tr_int = 0;
            string sql = "SELECT * FROM " + xledtab + " where  AC_HD='" + xled_achd + "' AND EMPLOYEE_ID='" + xacno + "' and convert(datetime, VCH_DATE, 103) < convert(datetime, '" + dt.ToString("dd/MM/yyyy").Replace("-", "/") + "', 103) ORDER BY VCH_DATE,VCH_NO,VCH_SRL";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                DataRow ldr = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                lbal_prin = !Convert.IsDBNull(ldr["prin_bal"]) ? Convert.ToDecimal(ldr["prin_bal"]) : Convert.ToDecimal(00);
                lbal_int = !Convert.IsDBNull(ldr["int_Due"]) ? Convert.ToDecimal(ldr["int_Due"]) : Convert.ToDecimal(00);
            }
            sql = "SELECT * FROM " + xledtab + " WHERE AC_HD='" + xled_achd + "' AND EMPLOYEE_ID='" + xacno + "' and convert(datetime, VCH_DATE, 103) >= convert(datetime, '" + dt.ToString("dd/MM/yyyy").Replace("-", "/") + "', 103) ORDER BY VCH_DATE,VCH_NO,VCH_SRL";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    ld.dr_cr = Convert.ToString(dr["dr_cr"]);
                    if (ld.dr_cr == "D")
                    {
                        lbal_prin = lbal_prin + (!Convert.IsDBNull(dr["prin_amount"]) ? Convert.ToDecimal(dr["prin_amount"]) : Convert.ToDecimal(00));
                        lbal_int = lbal_int + (!Convert.IsDBNull(dr["INT_AMOUNT"]) ? Convert.ToDecimal(dr["INT_AMOUNT"]) : Convert.ToDecimal(00));
                    }
                    else
                    {
                        lbal_prin = lbal_prin - (!Convert.IsDBNull(dr["prin_amount"]) ? Convert.ToDecimal(dr["prin_amount"]) : Convert.ToDecimal(00));
                        lbal_int = lbal_int - (!Convert.IsDBNull(dr["INT_AMOUNT"]) ? Convert.ToDecimal(dr["INT_AMOUNT"]) : Convert.ToDecimal(00));
                    }
                    string VOUCHER_DATE = ((Convert.ToDateTime(dr["vch_date"])).ToShortDateString()).Replace("-", "/");
                    ld.vch_no = Convert.ToString(dr["vch_no"]);
                    ld.vch_srl = Convert.ToInt32(dr["vch_srl"]);
                    string qry = "Update " + xledtab + " set prin_bal=" + lbal_prin + ",int_Due=" + lbal_int + " where convert(varchar, VCH_DATE, 103) = '" + VOUCHER_DATE + "' AND EMPLOYEE_ID='" + xacno + "'and vch_no='" + ld.vch_no + "' and vch_srl=" + ld.vch_srl + "";
                    config.Execute_Query(qry);
                }
            }
        }
        public List<Ledger> getledgerdetails(string BranchID, string member_id, string table, string emp_id, string ac_hd)
        {
            List<Ledger> ldl = new List<Ledger>();
            string sql = string.Empty;
            string XTR_TYPE = "";
            string xref = "";
            if (table == "GF_LEDGER")
            {               
                sql = "SELECT * FROM GF_LEDGER WHERE BRANCH_ID='" + BranchID + "' AND MEMBER_ID='" + member_id + "' ORDER BY VCH_DATE,VCH_NO,VCH_SRL";
                config.singleResult(sql);
                if(config.dt.Rows.Count > 0)
                {
                    foreach(DataRow dr in config.dt.Rows)
                    {
                        Ledger ld = new Ledger();
                        ld.dr_cr = dr["dr_cr"].ToString();
                        ld.vch_type = dr["VCH_TYPE"].ToString();                       
                        ld.vch_date = Convert.ToDateTime(dr["vch_date"]);                        
                        ld.prin_amount = !Convert.IsDBNull(dr["prin_amount"]) ? Convert.ToDecimal(dr["prin_amount"]) : Convert.ToDecimal("00");
                        ld.int_amount = !Convert.IsDBNull(dr["INT_AMOUNT"]) ? Convert.ToDecimal(dr["INT_AMOUNT"]) : Convert.ToDecimal("00");
                        ld.prin_bal = !Convert.IsDBNull(dr["prin_bal"]) ? Convert.ToDecimal(dr["prin_bal"]) : Convert.ToDecimal("00");
                        ld.int_bal = !Convert.IsDBNull(dr["int_bal"]) ? Convert.ToDecimal(dr["int_bal"]) : Convert.ToDecimal("00");
                        ld.ref_ac_hd = Convert.ToString(dr["ref_ac_hd"]);
                        ld.insert_mode = dr["insert_mode"].ToString();
                        ld.ref_pacno = Convert.ToString(dr["ref_pacno"]);
                        if (ld.dr_cr == "D")
                        {
                            XTR_TYPE = "To ";
                        }
                        else
                        {
                            XTR_TYPE = "By ";
                        }
                        if (ld.vch_type == "C")
                        {
                            XTR_TYPE = XTR_TYPE + "Cash";
                        }
                        if (ld.vch_type == "T")
                        {
                            XTR_TYPE = XTR_TYPE + "Transfer";
                        }
                        if (ld.vch_type == "B")
                        {
                            XTR_TYPE = XTR_TYPE + "Bank";
                        }
                        if (ld.vch_type == "J")
                        {
                            XTR_TYPE = XTR_TYPE + "Journal";
                        }
                        if (ld.prin_amount != 0)
                        {
                            XTR_TYPE = XTR_TYPE + " @Principal";
                        }
                        else
                        {
                            XTR_TYPE = XTR_TYPE + " @Interest";
                        }
                        if(ld.vch_type == "T")
                        {
                            if (ld.ref_ac_hd != "")
                            {
                                xref = xref + "/" + ld.ref_pacno;
                            }
                            if(xref != "")
                            {
                                XTR_TYPE = XTR_TYPE + " (" + xref + ")";
                            }
                        }
                        if(ld.insert_mode == "SD")
                        {
                            XTR_TYPE = XTR_TYPE + " (" + ld.insert_mode + ")";
                        }
                        if (ld.insert_mode == "BF")
                        {
                            XTR_TYPE = XTR_TYPE + " (Balance From Ledger)";
                        }
                        ld.trns_particular = XTR_TYPE;
                        ldl.Add(ld);
                    }
                }
            }
            else if(table == "TF_LEDGER")
            {
                sql = "SELECT * FROM TF_LEDGER WHERE BRANCH_ID='" + BranchID + "' AND MEMBER_ID='" + member_id + "' ORDER BY VCH_DATE,VCH_NO,VCH_SRL";
                config.singleResult(sql);
                if (config.dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in config.dt.Rows)
                    {
                        Ledger ld = new Ledger();
                        ld.dr_cr = dr["dr_cr"].ToString();
                        ld.vch_type = dr["VCH_TYPE"].ToString();
                        ld.vch_date = Convert.ToDateTime(dr["vch_date"]);
                        ld.prin_amount = !Convert.IsDBNull(dr["prin_amount"]) ? Convert.ToDecimal(dr["prin_amount"]) : Convert.ToDecimal("00");
                        ld.int_amount = !Convert.IsDBNull(dr["INT_AMOUNT"]) ? Convert.ToDecimal(dr["INT_AMOUNT"]) : Convert.ToDecimal("00");
                        ld.prin_bal = !Convert.IsDBNull(dr["prin_bal"]) ? Convert.ToDecimal(dr["prin_bal"]) : Convert.ToDecimal("00");
                        ld.int_bal = !Convert.IsDBNull(dr["int_bal"]) ? Convert.ToDecimal(dr["int_bal"]) : Convert.ToDecimal("00");
                        ld.ref_ac_hd = Convert.ToString(dr["ref_ac_hd"]);
                        ld.insert_mode = dr["insert_mode"].ToString();
                        ld.ref_pacno = Convert.ToString(dr["ref_pacno"]);
                        if (ld.dr_cr == "D")
                        {
                            XTR_TYPE = "To ";
                        }
                        else
                        {
                            XTR_TYPE = "By ";
                        }
                        if (ld.vch_type == "C")
                        {
                            XTR_TYPE = XTR_TYPE + "Cash";
                        }
                        if (ld.vch_type == "T")
                        {
                            XTR_TYPE = XTR_TYPE + "Transfer";
                        }
                        if (ld.vch_type == "B")
                        {
                            XTR_TYPE = XTR_TYPE + "Bank";
                        }
                        if (ld.vch_type == "J")
                        {
                            XTR_TYPE = XTR_TYPE + "Journal";
                        }
                        if (ld.prin_amount != 0)
                        {
                            XTR_TYPE = XTR_TYPE + " @Principal";
                        }
                        else
                        {
                            XTR_TYPE = XTR_TYPE + " @Interest";
                        }
                        if (ld.vch_type == "T")
                        {
                            if (ld.ref_ac_hd != "")
                            {
                                xref = xref + "/" + ld.ref_pacno;
                            }
                            if (xref != "")
                            {
                                XTR_TYPE = XTR_TYPE + " (" + xref + ")";
                            }
                        }
                        if (ld.insert_mode == "SD")
                        {
                            XTR_TYPE = XTR_TYPE + " (" + ld.insert_mode + ")";
                        }
                        if (ld.insert_mode == "BF")
                        {
                            XTR_TYPE = XTR_TYPE + " (Balance From Ledger)";
                        }
                        ld.trns_particular = XTR_TYPE;
                        ldl.Add(ld);
                    }
                }
            }
            else if(table == "DIVIDEND_LEDGER")
            {
                sql = "SELECT * FROM DIVIDEND_LEDGER WHERE BRANCH_ID='" + BranchID + "' AND MEMBER_ID='" + member_id + "' ORDER BY VCH_DATE,VCH_NO,VCH_SRL";
                config.singleResult(sql);
                if (config.dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in config.dt.Rows)
                    {
                        Ledger ld = new Ledger();
                        ld.dr_cr = dr["dr_cr"].ToString();
                        ld.vch_type = dr["VCH_TYPE"].ToString();
                        ld.vch_date = Convert.ToDateTime(dr["vch_date"]);
                        ld.div_post_year_to = !Convert.IsDBNull(dr["DIV_POST_YEAR_TO"]) ? Convert.ToInt32(dr["DIV_POST_YEAR_TO"]) : Convert.ToInt32("0.00");
                        ld.prin_amount = !Convert.IsDBNull(dr["TR_AMOUNT"]) ? Convert.ToDecimal(dr["TR_AMOUNT"]) : Convert.ToDecimal("00");
                        ld.prin_bal = !Convert.IsDBNull(dr["BAL_AMOUNT"]) ? Convert.ToDecimal(dr["BAL_AMOUNT"]) : Convert.ToDecimal("00");
                        ld.ref_ac_hd = Convert.ToString(dr["ref_ac_hd"]);
                        ld.insert_mode = dr["insert_mode"].ToString();
                        ld.ref_pacno = Convert.ToString(dr["ref_pacno"]);
                        ld.ref_oth = Convert.ToString(dr["REF_OTH"]);
                        if (ld.dr_cr == "C")
                        {
                            XTR_TYPE = "By Dividend Posting ";
                            if(ld.div_post_year_to != 0)
                            {
                                XTR_TYPE = XTR_TYPE + "(" + (ld.div_post_year_to - 1) + " - " + ld.div_post_year_to + ")";
                            }
                            else
                            {
                                XTR_TYPE = "To ";
                            }
                        }
                        if (ld.vch_type == "C")
                        {
                            XTR_TYPE = XTR_TYPE + "Cash";
                        }
                        if (ld.vch_type == "T")
                        {
                            if(ld.dr_cr == "D")
                            {
                                XTR_TYPE = XTR_TYPE + "Transfer";
                            }                            
                        }
                        if (ld.vch_type == "B")
                        {
                            XTR_TYPE = XTR_TYPE + "Bank";
                        }
                        if (ld.vch_type == "J")
                        {
                            XTR_TYPE = XTR_TYPE + "Journal";
                        }                        
                        if(ld.ref_ac_hd != "")
                        {                            
                            if (ld.ref_pacno != "")
                            {
                                XTR_TYPE = XTR_TYPE + "(" + ld.ref_ac_hd + "/" + ld.ref_pacno;
                                if (ld.ref_oth != "")
                                {
                                    XTR_TYPE = XTR_TYPE + "/" + ld.ref_oth + ")";
                                }
                                else
                                {
                                    XTR_TYPE = XTR_TYPE + ")";
                                }
                            }
                           
                            XTR_TYPE = XTR_TYPE + ")";
                        }
                        ld.trns_particular = XTR_TYPE;
                        ldl.Add(ld);
                    }
                }
            }
            else if(ac_hd == "SHARE CAPITAL")
            {
                sql = "SELECT * FROM share_ledger WHERE BRANCH_ID='" + BranchID + "' AND MEMBER_ID='" + member_id + "' ORDER BY VCH_DATE,VCH_NO,VCH_SRL";
                config.singleResult(sql);
                if (config.dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in config.dt.Rows)
                    {
                        Ledger ld = new Ledger();
                        ld.vch_type = Convert.ToString(dr["VCH_TYPE"]);
                        ld.dr_cr = Convert.ToString(dr["DR_CR"]);
                        ld.ref_ac_hd = !Convert.IsDBNull(dr["REF_AC_HD"]) ? Convert.ToString(dr["REF_AC_HD"]) : Convert.ToString("");
                        ld.ref_pacno = !Convert.IsDBNull(dr["ref_pacno"]) ? Convert.ToString(dr["ref_pacno"]) : Convert.ToString("");
                        ld.ref_oth = !Convert.IsDBNull(dr["ref_oth"]) ? Convert.ToString(dr["ref_oth"]) : Convert.ToString("");
                        ld.insert_mode = !Convert.IsDBNull(dr["INSERT_MODE"]) ? Convert.ToString(dr["INSERT_MODE"]) : Convert.ToString("");
                        ld.units = !Convert.IsDBNull(dr["UNITS"]) ? Convert.ToDecimal(dr["UNITS"]) : Convert.ToDecimal(00);
                        ld.vch_date = !Convert.IsDBNull(dr["vch_date"]) ? Convert.ToDateTime(dr["vch_date"]) : Convert.ToDateTime(null);
                        ld.face_val = !Convert.IsDBNull(dr["face_val"]) ? Convert.ToDecimal(dr["face_val"]) : Convert.ToDecimal(00);
                        ld.bal_amount = !Convert.IsDBNull(dr["BAL_AMOUNT"]) ? Convert.ToDecimal(dr["BAL_AMOUNT"]) : Convert.ToDecimal(00);
                        ld.prin_amount = !Convert.IsDBNull(dr["TR_AMOUNT"]) ? Convert.ToDecimal(dr["TR_AMOUNT"]) : Convert.ToDecimal(00);
                        ld.certificate_no = !Convert.IsDBNull(dr["CERTIFICATE_NO"]) ? Convert.ToString(dr["CERTIFICATE_NO"]) : Convert.ToString("");
                        ld.certificate_date = !Convert.IsDBNull(dr["CERTIFICATE_DATE"]) ? Convert.ToDateTime(dr["CERTIFICATE_DATE"]) : Convert.ToDateTime(null);
                        ld.dist_no_from = !Convert.IsDBNull(dr["dist_no_from"]) ? Convert.ToDecimal(dr["dist_no_from"]) : Convert.ToDecimal(00);
                        ld.dist_no_to = !Convert.IsDBNull(dr["dist_no_to"]) ? Convert.ToDecimal(dr["dist_no_to"]) : Convert.ToDecimal(00);
                        ld.dist_range = ld.dist_no_from.ToString() + "-" + ld.dist_no_to.ToString();
                        if (ld.dr_cr == "D")
                        {
                            XTR_TYPE = "To ";
                        }
                        else
                        {
                            XTR_TYPE = "By ";
                        }
                        if (ld.vch_type == "C")
                        {
                            XTR_TYPE = XTR_TYPE + "Cash";
                        }
                        if (ld.vch_type == "B")
                        {
                            XTR_TYPE = XTR_TYPE + "Bank";
                        }
                        if (ld.vch_type == "T")
                        {
                            XTR_TYPE = XTR_TYPE + "Transfer";
                        }
                        if (ld.vch_type == "J")
                        {
                            XTR_TYPE = XTR_TYPE + "Journal";
                        }
                        if (ld.ref_ac_hd != "")
                        {
                            if (ld.ref_pacno != "")
                            {
                                XTR_TYPE = XTR_TYPE + "(" + ld.ref_ac_hd + "/" + ld.ref_pacno;
                                if (ld.ref_oth != "")
                                {
                                    XTR_TYPE = XTR_TYPE + "/" + ld.ref_oth + ")";
                                }
                                else
                                {
                                    XTR_TYPE = XTR_TYPE + ")";
                                }
                            }
                        }
                        ld.trns_particular = XTR_TYPE;
                        ldl.Add(ld);
                    }
                }
            }
            else if(table == "LOAN_LEDGER")
            {
                sql = "SELECT * FROM LOAN_LEDGER WHERE BRANCH_ID='" + BranchID + "' AND AC_HD='" + ac_hd + "' AND employee_ID='" + emp_id + "' order by vch_date,vch_no,vch_srl";                
                decimal xdramt = 0;
                decimal xcramt = 0;
                config.singleResult(sql);               
                if (config.dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in config.dt.Rows)
                    {
                        xdramt = 0;
                        xcramt = 0;
                        Ledger ld = new Ledger();
                        ld.dr_cr = Convert.ToString(dr["DR_CR"]);
                        ld.vch_type = Convert.ToString(dr["VCH_TYPE"]);
                        ld.insert_mode = Convert.ToString(dr["INSERT_MODE"]);
                        ld.chq_no = Convert.ToString(dr["CHQ_NO"]);
                        ld.prin_amount = !Convert.IsDBNull(dr["PRIN_AMOUNT"]) ? Convert.ToDecimal(dr["PRIN_AMOUNT"]) : Convert.ToDecimal("0.00");
                        ld.int_amount = !Convert.IsDBNull(dr["INT_AMOUNT"]) ? Convert.ToDecimal(dr["INT_AMOUNT"]) : Convert.ToDecimal("0.00");
                        ld.aint_amt = !Convert.IsDBNull(dr["AINT_AMOUNT"]) ? Convert.ToDecimal(dr["AINT_AMOUNT"]) : Convert.ToDecimal("0.00");
                        ld.ichrg_amt = !Convert.IsDBNull(dr["ICHRG_AMOUNT"]) ? Convert.ToDecimal(dr["ICHRG_AMOUNT"]) : Convert.ToDecimal("0.00");
                        ld.ichrg_due = !Convert.IsDBNull(dr["ICHRG_DUE"]) ? Convert.ToDecimal(dr["ICHRG_DUE"]) : Convert.ToDecimal("0.00");
                        ld.prin_bal = !Convert.IsDBNull(dr["PRIN_BAL"]) ? Convert.ToDecimal(dr["PRIN_BAL"]) : Convert.ToDecimal("0.00");
                        ld.int_due = !Convert.IsDBNull(dr["INT_DUE"]) ? Convert.ToDecimal(dr["INT_DUE"]) : Convert.ToDecimal("0.00");
                        ld.aint_due = !Convert.IsDBNull(dr["AINT_DUE"]) ? Convert.ToDecimal(dr["AINT_DUE"]) : Convert.ToDecimal("0.00");
                        ld.ref_ac_hd = !Convert.IsDBNull(dr["REF_AC_HD"]) ? Convert.ToString(dr["REF_AC_HD"]) : Convert.ToString("");
                        ld.ref_pacno = !Convert.IsDBNull(dr["REF_PACNO"]) ? Convert.ToString(dr["REF_PACNO"]) : Convert.ToString("");
                        ld.vch_date = !Convert.IsDBNull(dr["VCH_DATE"]) ? Convert.ToDateTime(dr["VCH_DATE"]) : Convert.ToDateTime("");
                        if (ld.dr_cr == "C")
                        {
                            XTR_TYPE = "By";
                            if (ld.prin_amount > 0)
                            {
                                xcramt = ld.prin_amount;
                            }
                            else if (ld.int_amount > 0)
                            {
                                xcramt = ld.int_amount;
                            }
                            else if (ld.aint_amt > 0)
                            {
                                xcramt = ld.aint_amt;
                            }
                            else if (ld.ichrg_amt > 0)
                            {
                                xcramt = ld.ichrg_amt;
                            }
                        }
                        if (ld.dr_cr == "D")
                        {
                            XTR_TYPE = "To ";
                            if (ld.prin_amount > 0)
                            {
                                xdramt = ld.prin_amount;
                            }
                            else if (ld.int_amount > 0)
                            {
                                xdramt = ld.int_amount;
                            }
                            else if (ld.aint_amt > 0)
                            {
                                xdramt = ld.aint_amt;
                            }
                            else if (ld.ichrg_amt > 0)
                            {
                                xdramt = ld.ichrg_amt;
                            }
                        }
                        if (ld.vch_type == "C")
                        {
                            XTR_TYPE = XTR_TYPE + "Cash";
                        }
                        if (ld.vch_type == "T")
                        {
                            XTR_TYPE = XTR_TYPE + "Transfer";
                        }
                        if (ld.vch_type == "B")
                        {
                            XTR_TYPE = XTR_TYPE + "Bank";
                        }
                        if (ld.vch_type == "J")
                        {
                            XTR_TYPE = XTR_TYPE + "Journal";
                        }
                        if (ld.insert_mode == "BF")
                        {
                            XTR_TYPE = XTR_TYPE + " (Balance From Ledger)";
                        }
                        if (ld.insert_mode == "BF")
                        {
                            XTR_TYPE = XTR_TYPE + " -Balance B/F";
                        }
                        if (ld.insert_mode == "LR")
                        {
                            XTR_TYPE = XTR_TYPE + "-Receipt";
                        }
                        if (ld.insert_mode == "II")
                        {
                            XTR_TYPE = "By Calculation";
                        }
                        if (ld.insert_mode == "LC")
                        {
                            XTR_TYPE = XTR_TYPE + "-Closure";
                        }
                        if (ld.insert_mode == "LI")
                        {
                            XTR_TYPE = XTR_TYPE + "-Schedule";
                        }
                        if (ld.insert_mode == "QI")
                        {
                            XTR_TYPE = XTR_TYPE + "-Qtrly Schedule";
                        }
                        if (ld.insert_mode == "SD")
                        {
                            XTR_TYPE = XTR_TYPE + " " + Convert.ToString(dr["REF_OTH"]);
                        }
                        if (ld.prin_amount > 0)
                        {
                            XTR_TYPE = XTR_TYPE + " @Principal";
                        }
                        if (ld.int_amount > 0)
                        {
                            XTR_TYPE = XTR_TYPE + " @Interest";
                        }
                        if (ld.aint_amt > 0)
                        {
                            XTR_TYPE = XTR_TYPE + " @Addl.Interest";
                        }
                        if (ld.ichrg_amt > 0)
                        {
                            XTR_TYPE = XTR_TYPE + " @Misc.Charges";
                        }
                        if (ld.ref_ac_hd != "")
                        {
                            XTR_TYPE = XTR_TYPE + "(" + ld.ref_ac_hd;
                            if (ld.ref_pacno != "")
                            {
                                XTR_TYPE = XTR_TYPE + "/" + ld.ref_pacno;
                            }
                            XTR_TYPE = XTR_TYPE + ")";
                        }
                        ld.trns_particular = XTR_TYPE;
                        ld.cr_amt = xcramt;
                        ld.dr_amt = xdramt;
                        ldl.Add(ld);
                    }
                }
            }            
            return ldl;
        }

       
    }
}
