using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Amritnagar.Includes;
using Amritnagar.Models.ViewModel;

namespace Amritnagar.Models.Database
{
    public class Member_Mast
    {
        SQLConfig config = new SQLConfig();
        public string branch_id { get; set; }
        public string mem_id { get; set; }
        public string emp_branch { get; set; }
        public string book_no { get; set; }
        public string emp_id { get; set; }
        public string emp_cd { get; set; }
        public DateTime mem_date { get; set; }
        public string member_type { get; set; }
        public string f_name { get; set; }        
        public string mem_name { get; set; }        
        public string member_category { get; set; }
        public string l_name { get; set; }
        public Nullable<DateTime> birth_date { get; set; }
        public string sex { get; set; }
        public string guardian_name { get; set; }
        public string member_rel { get; set; }
        public string caste { get; set; }
        public string occupation { get; set; }
        public string relgn { get; set; }
        public int ltl_app { get; set; }
        public int married { get; set; }
        public string mailAdd_house { get; set; }
        public string mailAdd_add1 { get; set; }
        public string mailAdd_add2 { get; set; }
        public string mailAdd_city { get; set; }
        public string mailAdd_dist { get; set; }
        public string mailAdd_state { get; set; }
        public string mailAdd_pin { get; set; }
        public string prmntAdd_house { get; set; }
        public string prmntAdd_city { get; set; }
        public string prmntAdd_dist { get; set; }
        public string prmntAdd_state { get; set; }
        public string prmntAdd_pin { get; set; }
        public string prmntAdd_add1 { get; set; }
        public string prmntAdd_add2 { get; set; }
        public string ident_mark { get; set; }
        public decimal th_f_amt { get; set; }
        public decimal tf_buffer { get; set; }
        public decimal share { get; set; }
        public string dept { get; set; }
        public string desig { get; set; }
        public string phn { get; set; }
        public string serv_sts { get; set; }
        public Nullable<DateTime> join_dt { get; set; }
        public Nullable<DateTime> retmnt_dt { get; set; }
        public string nominee_name { get; set; }
        public string nominee_add1 { get; set; }
        public string nominee_add2 { get; set; }
        public string nominee_rel { get; set; }
        public string nominee_city { get; set; }
        public string nominee_dist { get; set; }
        public string nominee_state { get; set; }
        public string nominee_pin { get; set; }
        public string nominee_dob { get; set; }
        public string trans { get; set; }
        public string ret { get; set; }
        public string exp { get; set; }
        public Nullable<DateTime> exp_dt { get; set; }
        public string tf_double { get; set; }
        public string mem_closed { get; set; }
        public Nullable<DateTime> close_dt { get; set; }
        public string remarks { get; set; }
        public string bank_code { get; set; }
        public string accc_no { get; set; }
        public string pan { get; set; }
        public string nom_srl { get; set; }
        public string nom_name { get; set; }
        public string nom_rltn_id { get; set; }
        public string nom_birthdt { get; set; }
        public string nom_hno { get; set; }
        public string nom_add1 { get; set; }
        public string nom_add2 { get; set; }
        public string nom_city { get; set; }
        public string nom_dist { get; set; }
        public string nom_state { get; set; }
        public string msg { get; set; }
        public string nom_pin { get; set; }
        public int intr_srl { get; set; }
        public string intr_member_id { get; set; }
        public string intr_name { get; set; }
        public decimal tramt { get; set; }


        public Boolean CheckDetailsByMemberId(string branch_id, string mem_id)
        {
            string sql;
            Boolean i;
            sql = "select * from member_mast where branch_id='"+ branch_id + "' AND MEMBER_ID='" + mem_id + "'";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                i = true;
            }
            else
            {
                i = false;
            }
            return i;
        }
        public Boolean CheckDetailsByControlNo(string branch_id, string emp_id)
        {
            string sql;
            Boolean i;
            sql = "select * from member_mast where branch_id='" + branch_id + "' AND EMPLOYEE_ID='" + emp_id + "' ";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                i = true;
            }
            else
            {
                i = false;
            }
            return i;
        }
        public string getMemberNameByMemberId(string intr_mem_no)
        {
            string qry;
            qry = "SELECT member_name from member_mast where member_id='" + intr_mem_no + "'";
            config.singleResult(qry);
            Member_Mast mm = new Member_Mast();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    mm.mem_name = dr["member_name"].ToString();
                }
            }
            return mm.mem_name;
        }
        public string SaveMemberMaster(Member_Mast mm)
        {
            try
            {
                //string sql = "";
                //sql = "Insert into Member_Mast(BRANCH_ID,MEMBER_ID,EMPLOYEE_ID,EMPLOYER_CD,EMPLOYER_BRANCH,MEMBER_DATE,BOOK_NO,MEMBER_TYPE,MEM_CATEGORY,MEMBER_FIRST_NM,MEMBER_FIRST_NM" +
                //    "MEMBER_LAST_NM,MEMBER_NAME,GRDN_NAME,RELN_ID,mail_hno,mail_add1,mail_add2,mail_city,mail_dist,mail_state,mail_pin,perm_hno" +
                //    "perm_add1,perm_add2,perm_city,perm_dist,perm_state,perm_pin,phone_no,blood_group,birth_date,caste_id,sex,relgn_id,occup_id" +
                //    "married,if_lti,serv_status,dept_cd,desig_cd,date_of_joining,date_of_retirement,pan_no,id_mark,remarks,is_dead,member_closed" +
                //    "tf_buffer,TF_FLAG,member_retired,member_transfered,share,bank_code,ACCOUNT_NO";

                //if(mm.exp == "D")
                //{
                //    sql = sql + ",expiry_date";
                //}
                //if(mm.mem_closed == "C")
                //{
                //    sql = sql + ",member_closdt";
                //}

                config.Insert("Member_Mast", new Dictionary<String, object>()
                {
                        { "BRANCH_ID",  mm.branch_id },
                        { "MEMBER_ID",  mm.mem_id },
                        { "EMPLOYEE_ID",mm.emp_id },
                        { "EMPLOYER_CD", mm.emp_cd},
                        { "EMPLOYER_BRANCH",mm.emp_branch },                    
                        { "MEMBER_DATE",    mm.mem_date },                                      
                        { "BOOK_NO",    mm.book_no},
                        { "MEMBER_TYPE",    mm.member_type },
                        { "MEM_CATEGORY",   mm.member_category },
                        { "MEMBER_FIRST_NM",mm.f_name },
                        { "MEMBER_LAST_NM",mm.l_name},
                        { "MEMBER_NAME",    mm.mem_name },                 
                        { "GRDN_NAME",  mm.guardian_name },
                        { "RELN_ID",    mm.member_rel},
                        { "mail_hno",   mm.mailAdd_house},
                        { "mail_add1",  mm.mailAdd_add1},
                        { "mail_add2",  mm.mailAdd_add2},
                        { "mail_city",  mm.mailAdd_city },
                        { "mail_dist",  mm.mailAdd_dist},
                        { "mail_state", mm.mailAdd_state},
                        { "mail_pin",   mm.mailAdd_pin},
                        { "perm_hno",   mm.prmntAdd_house},
                        { "perm_add1",  mm.prmntAdd_add1},
                        { "perm_add2",  mm.prmntAdd_add2},
                        { "perm_city",  mm.prmntAdd_city},
                        { "perm_dist",  mm.prmntAdd_dist },
                        { "perm_state", mm.prmntAdd_state },
                        { "perm_pin",   mm.prmntAdd_pin  },
                        { "phone_no",   mm.phn },
                        { "blood_group",mm.th_f_amt },
                        { "birth_date", mm.birth_date },
                        { "caste_id",    mm.caste  },
                        { "sex",        mm.sex },
                        { "relgn_id",   mm.relgn },
                        { "occup_id",   mm.occupation },
                        { "married",    mm.married },
                        { "if_lti",     mm.ltl_app },
                        { "serv_status",mm.serv_sts },
                        { "dept_cd",    mm.dept },
                        { "desig_cd",   mm.desig },
                        { "date_of_joining",    mm.join_dt },
                        { "date_of_retirement", mm.retmnt_dt },
                        { "pan_no", mm.pan },
                        { "id_mark",mm.ident_mark },
                        { "remarks",mm.remarks },
                        { "is_dead",mm.exp },
                        { "expiry_date",mm.exp_dt },
                        { "member_closed",mm.mem_closed },
                        { "member_closdt",mm.close_dt },
                        { "tf_buffer", mm.tf_buffer },
                        { "TF_FLAG", mm.tf_double },
                        { "member_retired",mm.ret },
                        { "member_transfered",mm.trans },
                        { "share",mm.share },                                      
                        { "bank_code",mm.bank_code },                                      
                        { "ACCOUNT_NO",mm.accc_no },                                      
                });
            }
            catch (Exception x)
            {

            }
            string msg = "Saved Successfully";
            return (msg);
        }
        public Member_Mast getbooknoandemployeeidbymemberidandupdate(string mem_id)
        {
            string qry;
            qry = "SELECT * from member_mast where member_id='" + mem_id + "'";
            config.singleResult(qry);
            Member_Mast mm = new Member_Mast();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    mm.book_no = dr["BOOK_NO"].ToString();
                    mm.emp_id = dr["EMPLOYEE_ID"].ToString();
                    qry = "select * from RECOVERY_SCHEDULE where employee_id='" + mm.emp_id + "' ORDER BY EMPLOYEE_ID";
                    config.singleResult(qry);
                    if(config.dt.Rows.Count > 0)
                    {
                        config.Update("RECOVERY_SCHEDULE", new Dictionary<String, object>()
                            {
                            { "BOOK_NO",   mm.book_no},
                        }, new Dictionary<string, object>()
                        {
                            { "EMPLOYEE_ID",     mm.emp_id },
                        });
                    }
                    qry = "select * from LOAN_MASTER where employee_id='" + mm.emp_id + "' ORDER BY EMPLOYEE_ID";
                    config.singleResult(qry);
                    if (config.dt.Rows.Count > 0)
                    {
                        config.Update("LOAN_MASTER", new Dictionary<String, object>()
                            {
                            { "BOOK_NO",   mm.book_no},
                        }, new Dictionary<string, object>()
                        {
                            { "EMPLOYEE_ID",     mm.emp_id },
                        });
                    }
                }
            }
            return mm;
        }
        public Member_Mast getdetails(string branch_id, string memid)
        {
            Member_Mast mm = new Member_Mast();
            string sql = "select * from MEMBER_MAST where branch_id= '"+ branch_id + "' AND MEMBER_ID='" + memid + "'";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    mm.emp_cd = dr["EMPLOYER_CD"].ToString();
                    mm.book_no = dr["book_no"].ToString();
                    mm.emp_branch = dr["EMPLOYER_BRANCH"].ToString();
                    //mm.birth_date = !Convert.IsDBNull(dr["birth_date"]) ? Convert.ToDateTime(dr["birth_date"]) : Convert.ToDateTime("01/01/0001");
                    mm.emp_id = dr["EMPLOYEE_ID"].ToString();
                    mm.mem_date = Convert.ToDateTime(dr["MEMBER_DATE"].ToString());
                    mm.member_type = dr["MEMBER_TYPE"].ToString();
                    mm.member_category = dr["MEM_CATEGORY"].ToString();
                    //mm.if_lti = !Convert.IsDBNull(dr["if_lti"]) ? Convert.ToInt32(dr["if_lti"]) : Convert.ToInt32("00"); /*Convert.ToInt32(dr["if_lti"]);*/
                    mm.f_name = dr["MEMBER_FIRST_NM"].ToString();
                    mm.l_name = dr["MEMBER_LAST_NM"].ToString();
                    mm.guardian_name = dr["GRDN_NAME"].ToString();
                    mm.member_rel = dr["RELN_ID"].ToString();
                    mm.mailAdd_house = dr["MAIL_HNO"].ToString();
                    mm.mailAdd_add1 = dr["MAIL_ADD1"].ToString();
                    mm.mailAdd_add2 = dr["MAIL_ADD2"].ToString();
                    mm.mailAdd_city = dr["MAIL_CITY"].ToString();
                    //mm.sbamt = !Convert.IsDBNull(dr["sbamt"]) ? Convert.ToDecimal(dr["sbamt"]) : Convert.ToDecimal("00"); /*Convert.ToDecimal(dr["sbamt"]);*/
                    mm.mailAdd_dist = dr["MAIL_DIST"].ToString();
                    mm.mailAdd_state = dr["MAIL_STATE"].ToString();
                    mm.mailAdd_pin = dr["MAIL_PIN"].ToString();
                    mm.prmntAdd_house = dr["PERM_HNO"].ToString();
                    mm.prmntAdd_add1 = dr["PERM_ADD1"].ToString();
                    mm.prmntAdd_add2 = dr["PERM_ADD2"].ToString();
                    mm.prmntAdd_city = dr["PERM_CITY"].ToString();
                    mm.prmntAdd_dist = dr["PERM_DIST"].ToString();
                    mm.prmntAdd_state = dr["PERM_STATE"].ToString();
                    mm.prmntAdd_pin = !Convert.IsDBNull(dr["PERM_PIN"]) ? Convert.ToString(dr["PERM_PIN"]) : Convert.ToString("");
                    mm.phn = dr["PHONE_NO"].ToString();
                    mm.th_f_amt = Convert.ToDecimal(dr["BLOOD_GROUP"]);
                    mm.birth_date = Convert.ToDateTime(dr["BIRTH_DATE"].ToString());
                    mm.caste = dr["CASTE_ID"].ToString();                
                    mm.sex = dr["SEX"].ToString();
                    mm.relgn = dr["RELGN_ID"].ToString();
                    mm.occupation = dr["OCCUP_ID"].ToString();
                    mm.married = Convert.ToInt32(dr["MARRIED"]);
                    mm.ltl_app = Convert.ToInt32(dr["IF_LTI"]);
                    mm.serv_sts = dr["SERV_STATUS"].ToString();
                    mm.dept = dr["DEPT_CD"].ToString();                  
                    mm.desig = dr["DESIG_CD"].ToString();
                    mm.ret = dr["MEMBER_RETIRED"].ToString();
                    mm.married = !Convert.IsDBNull(dr["married"]) ? Convert.ToInt32(dr["married"]) : Convert.ToInt32("00"); /*Convert.ToInt32(dr["married"]);*/
                    mm.trans = dr["MEMBER_TRANSFERED"].ToString();
                    mm.pan = dr["PAN_NO"].ToString();
                    mm.ident_mark = dr["ID_MARK"].ToString();
                    mm.join_dt = !Convert.IsDBNull(dr["DATE_OF_JOINING"]) ? Convert.ToDateTime(dr["DATE_OF_JOINING"]) : Convert.ToDateTime("");
                    mm.retmnt_dt = !Convert.IsDBNull(dr["DATE_OF_RETIREMENT"]) ? Convert.ToDateTime(dr["DATE_OF_RETIREMENT"]) : Convert.ToDateTime(null);
                    mm.exp_dt = !Convert.IsDBNull(dr["EXPIRY_DATE"]) ? Convert.ToDateTime(dr["EXPIRY_DATE"]) : Convert.ToDateTime(null);
                    mm.close_dt = !Convert.IsDBNull(dr["MEMBER_CLOSDT"]) ? Convert.ToDateTime(dr["MEMBER_CLOSDT"]) : Convert.ToDateTime(null);
                    mm.bank_code = !Convert.IsDBNull(dr["BANK_CODE"]) ? Convert.ToString(dr["BANK_CODE"]) : Convert.ToString("");
                    mm.accc_no = !Convert.IsDBNull(dr["ACCOUNT_NO"]) ? Convert.ToString(dr["ACCOUNT_NO"]) : Convert.ToString("");
                    mm.share = !Convert.IsDBNull(dr["share"]) ? Convert.ToDecimal(dr["share"]) : Convert.ToDecimal("0.00");
                    mm.remarks = dr["REMARKS"].ToString();
                    mm.exp = dr["IS_DEAD"].ToString();
                    mm.mem_closed = dr["MEMBER_CLOSED"].ToString();
                    mm.tf_buffer = Convert.ToDecimal(dr["TF_BUFFER"]);
                    mm.tf_double = dr["TF_FLAG"].ToString();
                    sql = "Select * from MEMBER_NOMINEE where branch_id= '" + branch_id + "' And MEMBER_ID = '" + memid + "'";
                    config.singleResult(sql);
                    if (config.dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr1 in config.dt.Rows)
                        {
                            mm.nom_srl = dr1["NOM_SRL"].ToString();
                            mm.nominee_name = dr1["NOM_NAME"].ToString();
                            mm.nominee_rel = dr1["NOM_RELN_ID"].ToString();
                            //mm.nom_birthdt = dr1["NOM_BIRTH_DT"].ToString();
                            mm.nom_birthdt = !Convert.IsDBNull(dr1["NOM_BIRTH_DT"]) ? Convert.ToString(dr1["NOM_BIRTH_DT"]) : Convert.ToString("");
                            //mm.nom_hno = dr1["NOM_HNO"].ToString();
                            mm.nom_hno = !Convert.IsDBNull(dr1["NOM_HNO"]) ? Convert.ToString(dr1["NOM_HNO"]) : Convert.ToString("");
                            //mm.nominee_add1 = dr1["NOM_ADD1"].ToString();
                            mm.nominee_add1 = !Convert.IsDBNull(dr1["NOM_ADD1"]) ? Convert.ToString(dr1["NOM_ADD1"]) : Convert.ToString("");
                            mm.nominee_add2 = !Convert.IsDBNull(dr1["NOM_ADD2"]) ? Convert.ToString(dr1["NOM_ADD2"]) : Convert.ToString("");
                            mm.nominee_city = !Convert.IsDBNull(dr1["NOM_CITY"]) ? Convert.ToString(dr1["NOM_CITY"]) : Convert.ToString("");
                            mm.nominee_dist = !Convert.IsDBNull(dr1["NOM_DIST"]) ? Convert.ToString(dr1["NOM_DIST"]) : Convert.ToString("");
                            mm.nom_state = !Convert.IsDBNull(dr1["NOM_STATE"]) ? Convert.ToString(dr1["NOM_STATE"]) : Convert.ToString("");
                            mm.nominee_pin = !Convert.IsDBNull(dr1["NOM_PIN"]) ? Convert.ToString(dr1["NOM_PIN"]) : Convert.ToString("");
                        }
                    }
                    sql = "Select * from MEMBER_INTRODUCER where branch_id= '" + branch_id + "' And MEMBER_ID = '" + memid + "'";
                    config.singleResult(sql);
                    if (config.dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr2 in config.dt.Rows)
                        {
                            mm.intr_srl = Convert.ToInt32(dr2["INTR_SRL"]);
                            mm.intr_member_id = dr2["INTR_MEMBER_ID"].ToString();
                            mm.intr_name = dr2["INTR_NAME"].ToString();
                        }
                    }
                }
            }
            else
            {
                mm.msg = "New Member";
            }
            
            return mm;
        }
        public Member_Mast getdetailsbyempid(string branch_id, string emp_id)
        {
            Member_Mast mm = new Member_Mast();
            string sql = "select * from MEMBER_MAST where branch_id= '" + branch_id + "' AND EMPLOYEE_ID='" + emp_id + "'";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    mm.emp_cd = dr["EMPLOYER_CD"].ToString();
                    mm.book_no = dr["book_no"].ToString();
                    mm.emp_branch = dr["EMPLOYER_BRANCH"].ToString();
                    //mm.birth_date = !Convert.IsDBNull(dr["birth_date"]) ? Convert.ToDateTime(dr["birth_date"]) : Convert.ToDateTime("01/01/0001");
                    mm.mem_id = dr["MEMBER_ID"].ToString();
                    mm.mem_date = Convert.ToDateTime(dr["MEMBER_DATE"].ToString());
                    mm.member_type = dr["MEMBER_TYPE"].ToString();
                    mm.member_category = dr["MEM_CATEGORY"].ToString();
                    //mm.if_lti = !Convert.IsDBNull(dr["if_lti"]) ? Convert.ToInt32(dr["if_lti"]) : Convert.ToInt32("00"); /*Convert.ToInt32(dr["if_lti"]);*/
                    mm.f_name = dr["MEMBER_FIRST_NM"].ToString();
                    mm.l_name = dr["MEMBER_LAST_NM"].ToString();
                    mm.guardian_name = dr["GRDN_NAME"].ToString();
                    mm.member_rel = dr["RELN_ID"].ToString();
                    mm.mailAdd_house = dr["MAIL_HNO"].ToString();
                    mm.mailAdd_add1 = dr["MAIL_ADD1"].ToString();
                    mm.mailAdd_add2 = dr["MAIL_ADD2"].ToString();
                    mm.mailAdd_city = dr["MAIL_CITY"].ToString();
                    //mm.sbamt = !Convert.IsDBNull(dr["sbamt"]) ? Convert.ToDecimal(dr["sbamt"]) : Convert.ToDecimal("00"); /*Convert.ToDecimal(dr["sbamt"]);*/
                    mm.mailAdd_dist = dr["MAIL_DIST"].ToString();
                    mm.mailAdd_state = dr["MAIL_STATE"].ToString();
                    mm.mailAdd_pin = dr["MAIL_PIN"].ToString();
                    mm.prmntAdd_house = dr["PERM_HNO"].ToString();
                    mm.prmntAdd_add1 = dr["PERM_ADD1"].ToString();
                    mm.prmntAdd_add2 = dr["PERM_ADD2"].ToString();
                    mm.prmntAdd_city = dr["PERM_CITY"].ToString();
                    mm.prmntAdd_dist = dr["PERM_DIST"].ToString();
                    mm.prmntAdd_state = dr["PERM_STATE"].ToString();
                    mm.prmntAdd_pin = !Convert.IsDBNull(dr["PERM_PIN"]) ? Convert.ToString(dr["PERM_PIN"]) : Convert.ToString(""); 
                    mm.phn = dr["PHONE_NO"].ToString();
                    mm.th_f_amt = Convert.ToDecimal(dr["BLOOD_GROUP"]);
                    mm.birth_date = Convert.ToDateTime(dr["BIRTH_DATE"].ToString());
                    mm.caste = dr["CASTE_ID"].ToString();
                    mm.sex = dr["SEX"].ToString();
                    mm.relgn = dr["RELGN_ID"].ToString();
                    mm.occupation = dr["OCCUP_ID"].ToString();
                    mm.married = Convert.ToInt32(dr["MARRIED"]);
                    mm.ltl_app = Convert.ToInt32(dr["IF_LTI"]);
                    mm.serv_sts = dr["SERV_STATUS"].ToString();
                    mm.dept = dr["DEPT_CD"].ToString();
                    mm.desig = dr["DESIG_CD"].ToString();
                    mm.ret = dr["MEMBER_RETIRED"].ToString();
                    //mm.married = !Convert.IsDBNull(dr["married"]) ? Convert.ToInt32(dr["married"]) : Convert.ToInt32("00"); /*Convert.ToInt32(dr["married"]);*/
                    mm.trans = dr["MEMBER_TRANSFERED"].ToString();
                    mm.pan = dr["PAN_NO"].ToString();
                    mm.ident_mark = !Convert.IsDBNull(dr["ID_MARK"]) ? Convert.ToString(dr["ID_MARK"]) : Convert.ToString("");
                    mm.join_dt = !Convert.IsDBNull(dr["DATE_OF_JOINING"]) ? Convert.ToDateTime(dr["DATE_OF_JOINING"]) : Convert.ToDateTime("");
                    mm.retmnt_dt = !Convert.IsDBNull(dr["DATE_OF_RETIREMENT"]) ? Convert.ToDateTime(dr["DATE_OF_RETIREMENT"]) : Convert.ToDateTime(null);
                    mm.exp_dt = !Convert.IsDBNull(dr["EXPIRY_DATE"]) ? Convert.ToDateTime(dr["EXPIRY_DATE"]):Convert.ToDateTime(null);
                    mm.close_dt = !Convert.IsDBNull(dr["MEMBER_CLOSDT"]) ? Convert.ToDateTime(dr["MEMBER_CLOSDT"]) : Convert.ToDateTime(null);
                    mm.bank_code = !Convert.IsDBNull(dr["BANK_CODE"]) ? Convert.ToString(dr["BANK_CODE"]) : Convert.ToString("");
                    mm.accc_no = !Convert.IsDBNull(dr["ACCOUNT_NO"]) ? Convert.ToString(dr["ACCOUNT_NO"]) : Convert.ToString("");
                    mm.share = !Convert.IsDBNull(dr["share"]) ? Convert.ToDecimal(dr["share"]) : Convert.ToDecimal("0.00");
                    mm.remarks = dr["REMARKS"].ToString();
                    mm.exp = dr["IS_DEAD"].ToString();
                    mm.mem_closed = dr["MEMBER_CLOSED"].ToString();
                    mm.tf_buffer = Convert.ToDecimal(dr["TF_BUFFER"]);
                    mm.tf_double = dr["TF_FLAG"].ToString();
                    sql = "Select * from MEMBER_NOMINEE where branch_id= '" + branch_id + "' And MEMBER_ID = '" + mm.mem_id + "'";
                    config.singleResult(sql);
                    if (config.dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr1 in config.dt.Rows)
                        {
                            mm.nom_srl = dr1["NOM_SRL"].ToString();
                            mm.nominee_name = dr1["NOM_NAME"].ToString();
                            mm.nominee_rel = dr1["NOM_RELN_ID"].ToString();
                            mm.nom_birthdt = dr1["NOM_BIRTH_DT"].ToString();
                            mm.nom_hno = dr1["NOM_HNO"].ToString();
                            mm.nominee_add1 = dr1["NOM_ADD1"].ToString();
                            mm.nominee_add2 = !Convert.IsDBNull(dr1["NOM_ADD2"]) ? Convert.ToString(dr1["NOM_ADD2"]) : Convert.ToString("");
                            mm.nominee_city = !Convert.IsDBNull(dr1["NOM_CITY"]) ? Convert.ToString(dr1["NOM_CITY"]) : Convert.ToString("");
                            mm.nominee_dist = !Convert.IsDBNull(dr1["NOM_DIST"]) ? Convert.ToString(dr1["NOM_DIST"]) : Convert.ToString("");
                            mm.nom_state = !Convert.IsDBNull(dr1["NOM_STATE"]) ? Convert.ToString(dr1["NOM_STATE"]) : Convert.ToString("");
                            mm.nominee_pin = !Convert.IsDBNull(dr1["NOM_PIN"]) ? Convert.ToString(dr1["NOM_PIN"]) : Convert.ToString("");
                        }
                    }
                    sql = "Select * from MEMBER_INTRODUCER where branch_id= '" + branch_id + "' And MEMBER_ID = '" + mm.mem_id + "'";
                    config.singleResult(sql);
                    if (config.dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr2 in config.dt.Rows)
                        {
                            mm.intr_srl = Convert.ToInt32(dr2["INTR_SRL"]);
                            mm.intr_member_id = dr2["INTR_MEMBER_ID"].ToString();
                            mm.intr_name = dr2["INTR_NAME"].ToString();
                        }
                    }
                }
            }
            else
            {
                mm.msg = "New Member";
            }
            
            return mm;
        }
        public string UpdateMemberMaster(Member_Mast mm)
        {
             try
             {
                    config.Update("MEMBER_MAST", new Dictionary<String, object>()
                    {                       
                        { "EMPLOYER_CD", mm.emp_cd},
                        { "EMPLOYER_BRANCH",mm.emp_branch },
                        { "MEMBER_DATE",    mm.mem_date },
                        { "BOOK_NO",    mm.book_no},
                        { "MEMBER_TYPE",    mm.member_type },
                        { "MEM_CATEGORY",   mm.member_category },
                        { "MEMBER_FIRST_NM",mm.f_name },
                        { "MEMBER_LAST_NM",mm.l_name},
                        { "MEMBER_NAME",    mm.mem_name },
                        { "GRDN_NAME",  mm.guardian_name },
                        { "RELN_ID",    mm.member_rel},
                        { "mail_hno",   mm.mailAdd_house},
                        { "mail_add1",  mm.mailAdd_add1},
                        { "mail_add2",  mm.mailAdd_add2},
                        { "mail_city",  mm.mailAdd_city },
                        { "mail_dist",  mm.mailAdd_dist},
                        { "mail_state", mm.mailAdd_state},
                        { "mail_pin",   mm.mailAdd_pin},
                        { "perm_hno",   mm.prmntAdd_house},
                        { "perm_add1",  mm.prmntAdd_add1},
                        { "perm_add2",  mm.prmntAdd_add2},
                        { "perm_city",  mm.prmntAdd_city},
                        { "perm_dist",  mm.prmntAdd_dist },
                        { "perm_state", mm.prmntAdd_state },
                        { "perm_pin",   mm.prmntAdd_pin  },
                        { "phone_no",   mm.phn },
                        { "blood_group",mm.th_f_amt },
                        { "birth_date", mm.birth_date },
                        { "caste_id",    mm.caste  },
                        { "sex",        mm.sex },
                        { "relgn_id",   mm.relgn },
                        { "occup_id",   mm.occupation },
                        { "married",    mm.married },
                        { "if_lti",     mm.ltl_app },
                        { "serv_status",mm.serv_sts },
                        { "dept_cd",    mm.dept },
                        { "desig_cd",   mm.desig },
                        { "date_of_joining",    mm.join_dt },
                        { "date_of_retirement", mm.retmnt_dt },
                        { "pan_no", mm.pan },
                        { "id_mark",mm.ident_mark },
                        { "remarks",mm.remarks },
                        { "is_dead",mm.exp },
                        { "expiry_date",mm.exp_dt },
                        { "member_closed",mm.mem_closed },
                        { "member_closdt",mm.close_dt },
                        { "tf_buffer", mm.tf_buffer },
                        { "TF_FLAG", mm.tf_double },
                        { "member_retired",mm.ret },
                        { "member_transfered",mm.trans },
                        { "share",mm.share },
                        { "bank_code",mm.bank_code },
                        { "ACCOUNT_NO",mm.accc_no },
                    }, new Dictionary<string, object>()
                    {
                        { "BRANCH_ID",  mm.branch_id },
                        { "MEMBER_ID",  mm.mem_id },
                        { "EMPLOYEE_ID",mm.emp_id },
                    });               
             }
             catch (Exception ex)
             {
                
             }
            string msg = "Updated Successfuly";
            return msg;
        }     
        public List<Member_Mast> getdetails(string searchtype, string branch, string mem_type, string fr_dt, string to_dt)
        {
            List<Member_Mast> mil = new List<Member_Mast>();
            string sql = string.Empty;
            if (searchtype == "Opening")
            {
                sql = "SELECT EMPLOYEE_ID,MEMBER_ID,MEMBER_DATE,MEMBER_TYPE,MEM_CATEGORY,MEMBER_NAME,MEMBER_CLOSED,MEMBER_CLOSDT " +
                    "FROM MEMBER_MAST WHERE BRANCH_ID='" + branch + "' AND convert(datetime, MEMBER_DATE, 103) >= convert(datetime, '" + fr_dt + "', 103) and convert(datetime, MEMBER_DATE, 103) <= convert(datetime, '" + to_dt + "', 103)" +
                    "AND MEMBER_TYPE='" + mem_type + "' ORDER BY MEMBER_ID,MEMBER_DATE,MEMBER_TYPE ";                
                config.singleResult(sql);
                if (config.dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in config.dt.Rows)
                    {
                        Member_Mast mm = new Member_Mast();
                        mm.emp_id = dr["EMPLOYEE_ID"].ToString();
                        mm.mem_id = dr["MEMBER_ID"].ToString();
                        //mm.mem_date = Convert.ToDateTime(dr["MEMBER_DATE"].ToString());
                        mm.mem_date = !Convert.IsDBNull(dr["MEMBER_DATE"]) ? Convert.ToDateTime(dr["MEMBER_DATE"]) : Convert.ToDateTime(null);
                        //mm.close_dt = dr["MEMBER_CLOSDT"].ToString();
                        mm.member_type = dr["MEMBER_TYPE"].ToString();
                        mm.member_category = dr["MEM_CATEGORY"].ToString();
                        mm.mem_name = dr["MEMBER_NAME"].ToString();
                        //mil.Add(mm);
                        if (mm.member_type == "GEN")
                        {
                            sql = "select * from share_ledger where BRANCH_ID='" + branch + "' AND member_id='" + mm.mem_id + "' AND convert(datetime, vch_date, 103) >= convert(datetime, '" + fr_dt + "', 103) and convert(datetime, vch_date, 103) <= convert(datetime, '" + to_dt + "', 103) order by vch_date,vch_srl";
                            config.singleResult(sql);
                            if (config.dt.Rows.Count > 0)
                            {
                                foreach (DataRow dr1 in config.dt.Rows)
                                {                                  
                                    mm.tramt = !Convert.IsDBNull(dr1["BAL_AMOUNT"]) ? Convert.ToDecimal(dr1["BAL_AMOUNT"]) : Convert.ToDecimal("00");                                   
                                }
                            }
                        }
                        else
                        {
                            sql = "select* from tf_ledger where BRANCH_ID = '" + branch + "' AND member_id = '" + mm.mem_id + "' AND convert(datetime, vch_date, 103) >= convert(datetime, '" + fr_dt + "', 103) and convert(datetime, vch_date, 103) <= convert(datetime, '" + to_dt + "', 103) order by vch_date,vch_srl";
                            config.singleResult(sql);
                            if (config.dt.Rows.Count > 0)
                            {
                                foreach (DataRow dr1 in config.dt.Rows)
                                {                                   
                                    mm.tramt = !Convert.IsDBNull(dr1["prin_bal"]) ? Convert.ToDecimal(dr1["prin_bal"]) : Convert.ToDecimal("00");                                  
                                }
                            }
                        }
                        mil.Add(mm);
                    }
                }
            }
            else if (searchtype == "Closing")
            {
                sql = "SELECT EMPLOYEE_ID,MEMBER_ID,MEMBER_DATE,MEMBER_TYPE,MEM_CATEGORY,MEMBER_NAME,MEMBER_CLOSED,MEMBER_CLOSDT  " +
                    "FROM MEMBER_MAST WHERE BRANCH_ID='" + branch + "' AND  convert(datetime, MEMBER_CLOSDT, 103) >= convert(datetime, '" + fr_dt + "', 103) and convert(datetime, MEMBER_CLOSDT, 103) <= convert(datetime, '" + to_dt + "', 103) " +
                    "AND MEMBER_CLOSED='C' ORDER BY MEMBER_ID,MEMBER_DATE,MEMBER_TYPE";
                config.singleResult(sql);
                if (config.dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in config.dt.Rows)
                    {
                        Member_Mast mm = new Member_Mast();
                        mm.emp_id = dr["EMPLOYEE_ID"].ToString();
                        mm.mem_id = dr["MEMBER_ID"].ToString();
                        //mm.mem_date = Convert.ToDateTime(dr["MEMBER_DATE"].ToString());
                        mm.mem_date = !Convert.IsDBNull(dr["MEMBER_DATE"]) ? Convert.ToDateTime(dr["MEMBER_DATE"]) : Convert.ToDateTime(null);
                        mm.close_dt = Convert.ToDateTime(dr["MEMBER_CLOSDT"].ToString());
                        mm.member_type = dr["MEMBER_TYPE"].ToString();
                        mm.member_category = dr["MEM_CATEGORY"].ToString();
                        mm.mem_name = dr["MEMBER_NAME"].ToString();
                       // mil.Add(mm);
                        if (mm.member_type == "GEN")
                        {
                            sql = "select * from share_ledger where BRANCH_ID='" + branch + "' AND member_id='" + mm.mem_id + "' AND convert(datetime, vch_date, 103) >= convert(datetime, '" + fr_dt + "', 103) and convert(datetime, vch_date, 103) <= convert(datetime, '" + to_dt + "', 103) AND DR_CR='D' order by vch_date,vch_srl";
                            config.singleResult(sql);
                            if (config.dt.Rows.Count > 0)
                            {
                                foreach (DataRow dr1 in config.dt.Rows)
                                {                                
                                    mm.tramt = !Convert.IsDBNull(dr1["TR_AMOUNT"]) ? Convert.ToDecimal(dr1["TR_AMOUNT"]) : Convert.ToDecimal("00");                                 
                                }
                            }
                        }
                        else
                        {
                            sql = "select* from tf_ledger where BRANCH_ID = '" + branch + "' AND member_id = '" + mm.mem_id + "' AND convert(datetime, vch_date, 103) >= convert(datetime, '" + fr_dt + "', 103) and convert(datetime, vch_date, 103) <= convert(datetime, '" + to_dt + "', 103) AND DR_CR='D' order by vch_date,vch_srl";
                            config.singleResult(sql);
                            if (config.dt.Rows.Count > 0)
                            {
                                foreach (DataRow dr1 in config.dt.Rows)
                                {                                  
                                    mm.tramt = !Convert.IsDBNull(dr1["PRIN_AMOUNT"]) ? Convert.ToDecimal(dr1["PRIN_AMOUNT"]) : Convert.ToDecimal("00");                                 
                                }
                            }
                        }
                        mil.Add(mm);
                    }
                }
            }                                       
            return mil;
        }
        public Member_Mast getdetailsbymemberid(string memid)
        {
            Member_Mast mm = new Member_Mast();
            string sql = "select * from MEMBER_MAST where MEMBER_ID='" + memid + "' order by MEMBER_ID";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {                   
                    mm.book_no = dr["book_no"].ToString();                  
                    mm.mem_date = Convert.ToDateTime(dr["MEMBER_DATE"].ToString());
                    mm.member_type = dr["MEMBER_TYPE"].ToString();
                    mm.member_category = dr["MEM_CATEGORY"].ToString();                                      
                    mm.guardian_name = dr["GRDN_NAME"].ToString();
                    mm.mem_name = dr["MEMBER_NAME"].ToString();                  
                }
            }
            return mm;
        }
        public Member_Mast getmemidbyempid(string emp_id)
        {
            Member_Mast mm = new Member_Mast();
            string sql = "select * from member_mast where employee_id='" + emp_id + "' order by employee_id";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    mm.mem_id = dr["MEMBER_ID"].ToString();                   
                }
            }
            else
            {
                mm.msg = "First create membership master ... next input Man number";
            }
            return mm;
        }
        public List<Member_Mast> getmemberdetailsbymemid(string branch_id, string memid)
        {
            string sql = "select * from MEMBER_MAST where branch_id= '" + branch_id + "' AND MEMBER_ID='" + memid + "'";
            config.singleResult(sql);
            List<Member_Mast> mml = new List<Member_Mast>();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    Member_Mast mm = new Member_Mast();
                    mm.emp_cd = dr["EMPLOYER_CD"].ToString();
                    mm.book_no = dr["book_no"].ToString();
                    mm.emp_branch = dr["EMPLOYER_BRANCH"].ToString();
                    //mm.birth_date = !Convert.IsDBNull(dr["birth_date"]) ? Convert.ToDateTime(dr["birth_date"]) : Convert.ToDateTime("01/01/0001");
                    mm.emp_id = dr["EMPLOYEE_ID"].ToString();
                    mm.mem_date = Convert.ToDateTime(dr["MEMBER_DATE"].ToString());
                    mm.member_type = dr["MEMBER_TYPE"].ToString();
                    mm.member_category = dr["MEM_CATEGORY"].ToString();
                    //mm.if_lti = !Convert.IsDBNull(dr["if_lti"]) ? Convert.ToInt32(dr["if_lti"]) : Convert.ToInt32("00"); /*Convert.ToInt32(dr["if_lti"]);*/
                    mm.f_name = dr["MEMBER_FIRST_NM"].ToString();
                    mm.l_name = dr["MEMBER_LAST_NM"].ToString();
                    mm.mem_name = dr["MEMBER_NAME"].ToString();
                    mm.guardian_name = dr["GRDN_NAME"].ToString();
                    mm.member_rel = dr["RELN_ID"].ToString();
                    mm.mailAdd_house = dr["MAIL_HNO"].ToString();
                    mm.mailAdd_add1 = dr["MAIL_ADD1"].ToString();
                    mm.mailAdd_add2 = dr["MAIL_ADD2"].ToString();
                    mm.mailAdd_city = dr["MAIL_CITY"].ToString();
                    //mm.sbamt = !Convert.IsDBNull(dr["sbamt"]) ? Convert.ToDecimal(dr["sbamt"]) : Convert.ToDecimal("00"); /*Convert.ToDecimal(dr["sbamt"]);*/
                    mm.mailAdd_dist = dr["MAIL_DIST"].ToString();
                    mm.mailAdd_state = dr["MAIL_STATE"].ToString();
                    mm.mailAdd_pin = dr["MAIL_PIN"].ToString();
                    mm.prmntAdd_house = dr["PERM_HNO"].ToString();
                    mm.prmntAdd_add1 = dr["PERM_ADD1"].ToString();
                    mm.prmntAdd_add2 = dr["PERM_ADD2"].ToString();
                    mm.prmntAdd_city = dr["PERM_CITY"].ToString();
                    mm.prmntAdd_dist = dr["PERM_DIST"].ToString();
                    mm.prmntAdd_state = dr["PERM_STATE"].ToString();
                    mm.prmntAdd_pin = !Convert.IsDBNull(dr["PERM_PIN"]) ? Convert.ToString(dr["PERM_PIN"]) : Convert.ToString("");
                    mm.phn = dr["PHONE_NO"].ToString();
                    //mm.th_f_amt = Convert.ToDecimal(dr["BLOOD_GROUP"]);
                    mm.th_f_amt = !Convert.IsDBNull(dr["BLOOD_GROUP"]) ? Convert.ToDecimal(dr["BLOOD_GROUP"]) : Convert.ToDecimal("00");
                    //mm.birth_date = Convert.ToDateTime(dr["BIRTH_DATE"].ToString());
                    mm.birth_date = !Convert.IsDBNull(dr["BIRTH_DATE"]) ? Convert.ToDateTime(dr["BIRTH_DATE"]) : Convert.ToDateTime(null); ;
                    mm.caste = dr["CASTE_ID"].ToString();
                    mm.sex = dr["SEX"].ToString();
                    mm.relgn = dr["RELGN_ID"].ToString();
                    mm.occupation = dr["OCCUP_ID"].ToString();
                    mm.married = Convert.ToInt32(dr["MARRIED"]);
                    mm.ltl_app = Convert.ToInt32(dr["IF_LTI"]);
                    mm.serv_sts = dr["SERV_STATUS"].ToString();
                    mm.dept = dr["DEPT_CD"].ToString();
                    mm.desig = dr["DESIG_CD"].ToString();
                    mm.ret = dr["MEMBER_RETIRED"].ToString();
                    mm.married = !Convert.IsDBNull(dr["married"]) ? Convert.ToInt32(dr["married"]) : Convert.ToInt32("00"); /*Convert.ToInt32(dr["married"]);*/
                    mm.trans = dr["MEMBER_TRANSFERED"].ToString();
                    mm.pan = dr["PAN_NO"].ToString();
                    mm.ident_mark = dr["ID_MARK"].ToString();
                    mm.join_dt = !Convert.IsDBNull(dr["DATE_OF_JOINING"]) ? Convert.ToDateTime(dr["DATE_OF_JOINING"]) : Convert.ToDateTime(null);
                    mm.retmnt_dt = !Convert.IsDBNull(dr["DATE_OF_RETIREMENT"]) ? Convert.ToDateTime(dr["DATE_OF_RETIREMENT"]) : Convert.ToDateTime(null);
                    mm.exp_dt = !Convert.IsDBNull(dr["EXPIRY_DATE"]) ? Convert.ToDateTime(dr["EXPIRY_DATE"]) : Convert.ToDateTime(null);
                    mm.close_dt = !Convert.IsDBNull(dr["MEMBER_CLOSDT"]) ? Convert.ToDateTime(dr["MEMBER_CLOSDT"]) : Convert.ToDateTime(null);
                    mm.bank_code = !Convert.IsDBNull(dr["BANK_CODE"]) ? Convert.ToString(dr["BANK_CODE"]) : Convert.ToString("");
                    mm.accc_no = !Convert.IsDBNull(dr["ACCOUNT_NO"]) ? Convert.ToString(dr["ACCOUNT_NO"]) : Convert.ToString("");
                    mm.share = !Convert.IsDBNull(dr["share"]) ? Convert.ToDecimal(dr["share"]) : Convert.ToDecimal("0.00");
                    mm.remarks = dr["REMARKS"].ToString();
                    mm.exp = dr["IS_DEAD"].ToString();
                    mm.mem_closed = dr["MEMBER_CLOSED"].ToString();
                    mm.tf_buffer = Convert.ToDecimal(dr["TF_BUFFER"]);
                    mm.tf_double = dr["TF_FLAG"].ToString();
                    mml.Add(mm);
                }
            }
            //else
            //{
            //    Member_Mast mm = new Member_Mast();
            //    mm.msg = "Invalid Membership No.";
            //}
            return mml;
        }
        public string updatetfbuffer(Member_Mast mm)
        {
            string sql = string.Empty;
            sql = "select * from member_mast where branch_id = 'MN' and member_id = '" + mm.mem_id + "'";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                try
                {
                    config.Update("member_mast", new Dictionary<String, object>()
                    {
                       { "tf_buffer", mm.tf_buffer },
                    }, new Dictionary<string, object>()
                    {
                        { "BRANCH_ID",  mm.branch_id },
                        { "MEMBER_ID",  mm.mem_id },
                    });
                }
                catch (Exception ex)
                {

                }
            }
            string msg = "Over";
            return (msg);
        }
        public string getmemid()
        {
            string qry;
            qry = "select MEMBER_ID from MEMBER_MAST";
            config.singleResult(qry);
            Member_Mast mm = new Member_Mast();
            if (config.dt.Rows.Count > 0)
            {
                DataRow dr = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                mm.mem_id = Convert.ToString(dr["MEMBER_ID"]);
            }
            return mm.mem_id;
        }
        public List<Member_Mast> GetmemMastForDemandInterestCalculation(MemDepositeFundIntPaySchViewModel model)
        {
            // DepositMast dm = new DepositMast();
            List<Member_Mast> mmlst = new List<Member_Mast>();
            string sql = "SELECT * FROM MEMBER_MAST WHERE BRANCH_ID='" + model.branch + "' and EMPLOYER_BRANCH='" + model.colliery_code + "' AND ";
            sql = sql + "BOOK_NO= '" + model.book_no + "' AND MEM_CATEGORY='" + model.ex_gen + "' AND convert(datetime, MEMBER_DATE, 103) <= convert(datetime, '" + model.to_dt + "', 103) AND ";
            sql = sql + "IIF(MEMBER_CLOSDT is NULL,convert(datetime, '31/03/2099', 103),convert(datetime, MEMBER_CLOSDT, 103)) >= convert(datetime, '" + model.to_dt + "', 103) ORDER BY MEMBER_ID";
            //sql = sql + "AND convert(datetime, MEMBER_CLOSDT, 103) >= convert(datetime, '" + model.to_dt + "', 103) ORDER BY MEMBER_ID";
            config.singleResult(sql);

            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    Member_Mast mm = new Member_Mast();
                    mm.mem_date = Convert.ToDateTime(dr["MEMBER_DATE"]);
                    mm.mem_name = Convert.ToString(dr["member_name"]);
                    mm.mem_id = Convert.ToString(dr["MEMBER_ID"]);
                    mmlst.Add(mm);
                }
            }
            else
            {
                mmlst = null;
            }
            return mmlst;


        }
        public List<Member_Mast> getallmemberdetails()
        {
            string sql = "select * from MEMBER_MAST order by MEMBER_NAME";
            config.singleResult(sql);
            List<Member_Mast> mml = new List<Member_Mast>();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    Member_Mast mm = new Member_Mast();
                    mm.mem_id = dr["MEMBER_ID"].ToString();
                    mm.mem_date = Convert.ToDateTime(dr["MEMBER_DATE"].ToString());
                    mm.member_type = dr["MEMBER_TYPE"].ToString();
                    mm.member_category = dr["MEM_CATEGORY"].ToString();
                    mm.mem_name = dr["MEMBER_NAME"].ToString();
                    mml.Add(mm);
                }
            }
            return mml;
        }



    }
}