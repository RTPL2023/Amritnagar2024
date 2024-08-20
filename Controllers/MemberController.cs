using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Amritnagar.Models.Database;
using Amritnagar.Models.ViewModel;
using System.IO;
using System.Drawing.Printing;
using System.Drawing;
//using ESC_POS_USB_NET.Printer;


namespace Amritnagar.Controllers
{
    public class MemberController : Controller
    {
        /********************************************Member Master Start*******************************************/
        [HttpGet]
        public ActionResult MemberMaster(MemberMasterViewModel model)
        {
            Member_Mast mm = new Member_Mast();
            UtilityController u = new UtilityController();
            model.BranchDesc = u.getBranchMastDetails();
            model.TypeDesc = u.getTypeMastDetails();
            model.CastDesc = u.getCastMastDetails();
            model.RelationDesc = u.getRelationMastDetails();
            model.ReligionDesc = u.getReligionMastDetails();
            model.DepartmentDesc = u.getDepartmentMastDetails();
            model.DesignationDesc = u.getDesignationMastDetails();
            model.OccupationDesc = u.getOccupationMastDetails();
            model.ServiceDesc = u.getServiceStatusMastDetails();
            model.EmpDesc = u.getEmployerMastDetails();
            model.EmpBranchDesc = u.getEmployerBranchMastDetails();
            model.CategoryDesc = u.getCategoryMastDetails();
            model.mem_date = DateTime.Now.ToString("dd-MM-yyyy").Replace("-", "/");
            int mem_id = Convert.ToInt32(mm.getmemid()) + 1;
            int j = (7 - mem_id.ToString().Length);
            string mem_no = "";
            for (int i = 1; i <= j; i++)
            {
                mem_no = mem_no + "0";
            }
            model.mem_id = mem_no + mem_id;
            return View(model);
        }
        public JsonResult SaveRec(MemberMasterViewModel model)
        {
            Boolean i;
            Member_Mast mm = new Member_Mast();
            i = mm.CheckDetailsByMemberId(model.branch_id, model.mem_id);
            if (i == false)
            {
                i = mm.CheckDetailsByControlNo(model.branch_id, model.emp_id);
                if (i == false)
                {
                    mm.branch_id = model.branch_id.ToUpper();
                    mm.mem_id = model.mem_id;
                    mm.mem_date = Convert.ToDateTime(model.mem_date);
                    mm.member_type = model.member_type.ToUpper();
                    mm.member_category = model.member_category.ToUpper();
                    mm.emp_cd = model.emp_cd;
                    mm.emp_branch = model.emp_branch;
                    mm.emp_id = model.emp_id;
                    mm.book_no = model.book_no;
                    mm.f_name = model.f_name.ToUpper();
                    mm.l_name = model.l_name.ToUpper();
                    mm.mem_name = (mm.f_name + " " + mm.l_name).ToUpper();
                    mm.birth_date = Convert.ToDateTime(model.birth_date);
                    if (model.sex == "MALE")
                    {
                        mm.sex = "M";
                    }
                    else if (model.sex == "FEMALE")
                    {
                        mm.sex = "F";
                    }
                    else
                    {
                        mm.sex = "";
                    }
                    mm.guardian_name = model.guardian_name.ToUpper();
                    mm.member_rel = model.member_rel.ToUpper();
                    mm.caste = model.caste.ToUpper();
                    mm.relgn = model.relgn.ToUpper();
                    mm.occupation = model.occupation;
                    if (model.mailAdd_house == null)
                    {
                        mm.mailAdd_house = "";
                    }
                    else
                    {
                        mm.mailAdd_house = model.mailAdd_house;
                    }
                    if (model.mailAdd_add1 == null)
                    {
                        mm.mailAdd_add1 = "";
                    }
                    else
                    {
                        mm.mailAdd_add1 = model.mailAdd_add1.ToUpper();
                    }
                    if (model.mailAdd_add2 == null)
                    {
                        mm.mailAdd_add2 = "";
                    }
                    else
                    {
                        mm.mailAdd_add2 = model.mailAdd_add2.ToUpper();
                    }
                    if (model.mailAdd_city == null)
                    {
                        mm.mailAdd_city = "";
                    }
                    else
                    {
                        mm.mailAdd_city = model.mailAdd_city.ToUpper();
                    }
                    if (model.mailAdd_dist == null)
                    {
                        mm.mailAdd_dist = "";
                    }
                    else
                    {
                        mm.mailAdd_dist = model.mailAdd_dist.ToUpper();
                    }
                    if (model.mailAdd_state == null)
                    {
                        mm.mailAdd_state = "";
                    }
                    else
                    {
                        mm.mailAdd_state = model.mailAdd_state.Substring(0, 2).ToUpper();
                    }
                    mm.mailAdd_pin = model.mailAdd_pin;
                    if (model.prmntAdd_house == null)
                    {
                        mm.prmntAdd_house = "";
                    }
                    else
                    {
                        mm.prmntAdd_house = model.prmntAdd_house;
                    }
                    if (model.prmntAdd_add1 == null)
                    {
                        mm.prmntAdd_add1 = "";
                    }
                    else
                    {
                        mm.prmntAdd_add1 = model.prmntAdd_add1.ToUpper();
                    }
                    if (model.prmntAdd_add2 == null)
                    {
                        mm.prmntAdd_add2 = "";
                    }
                    else
                    {
                        mm.prmntAdd_add2 = model.prmntAdd_add2.ToUpper();
                    }
                    if (model.prmntAdd_city == null)
                    {
                        mm.prmntAdd_city = "";
                    }
                    else
                    {
                        mm.prmntAdd_city = model.prmntAdd_city.ToUpper();
                    }
                    if (model.prmntAdd_dist == null)
                    {
                        mm.prmntAdd_dist = "";
                    }
                    else
                    {
                        mm.prmntAdd_dist = model.prmntAdd_dist.ToUpper();
                    }
                    if (model.prmntAdd_state == null)
                    {
                        mm.prmntAdd_state = "";
                    }
                    else
                    {
                        mm.prmntAdd_state = model.prmntAdd_state.Substring(0, 2).ToUpper();
                    }
                    mm.prmntAdd_pin = model.prmntAdd_pin;
                    mm.ident_mark = model.ident_mark;
                    mm.pan = model.pan;
                    mm.th_f_amt = Convert.ToDecimal(model.th_f_amt);
                    mm.phn = model.phn;
                    if (model.tf_buffer != null)
                    {
                        mm.tf_buffer = Convert.ToDecimal(model.tf_buffer);
                    }
                    else
                    {
                        mm.tf_buffer = Convert.ToDecimal("00");
                    }
                    mm.desig = model.desig;
                    if (model.married == true)
                    {
                        mm.married = 1;
                    }
                    else
                    {
                        mm.married = 0;
                    }
                    if (model.ltl_app == true)
                    {
                        mm.ltl_app = 1;
                    }
                    else
                    {
                        mm.ltl_app = 0;
                    }
                    mm.serv_sts = model.serv_sts;
                    mm.dept = model.dept;
                    mm.join_dt = Convert.ToDateTime(Convert.ToDateTime(model.join_dt).ToString("dd-MM-yyyy").Replace("-", "/"));
                    mm.retmnt_dt = Convert.ToDateTime(Convert.ToDateTime(model.retmnt_dt).ToString("dd-MM-yyyy").Replace("-", "/"));
                    if (model.share == null)
                    {
                        mm.share = 0;
                    }
                    else
                    {
                        mm.share = Convert.ToDecimal(model.share);
                    }
                    if (model.remarks == null)
                    {
                        mm.remarks = "";
                    }
                    else
                    {
                        mm.remarks = model.remarks;
                    }
                    if (model.bank_code == null)
                    {
                        mm.bank_code = "";
                    }
                    else
                    {
                        mm.bank_code = model.bank_code;
                    }
                    if (model.accc_no == null)
                    {
                        mm.accc_no = "";
                    }
                    else
                    {
                        mm.accc_no = model.accc_no;
                    }
                    //mm.blood_group = model.BloodGroup;
                    if (model.exp == true)
                    {
                        mm.exp = "D";
                        mm.exp_dt = Convert.ToDateTime(Convert.ToDateTime(model.exp_dt).ToString("dd-MM-yyyy").Replace("-", "/"));
                    }
                    else
                    {
                        mm.exp = "A";
                        mm.exp_dt = null;
                    }
                    if (model.mem_closed == true)
                    {
                        mm.mem_closed = "C";
                        mm.close_dt = Convert.ToDateTime(Convert.ToDateTime(model.close_dt).ToString("dd-MM-yyyy").Replace("-", "/"));
                    }
                    else
                    {
                        mm.mem_closed = "";
                        mm.close_dt = null;
                    }

                    if (model.ret == true)
                    {
                        mm.ret = "Y";
                    }
                    else
                    {
                        mm.ret = "";
                    }
                    if (model.trans == true)
                    {
                        mm.trans = "Y";
                    }
                    else
                    {
                        mm.trans = "";
                    }
                    if (model.tf_double == true)
                    {
                        mm.tf_double = "D";
                    }
                    else
                    {
                        mm.tf_double = "S";
                    }
                    model.msg = mm.SaveMemberMaster(mm);
                    if (model.msg == "Saved Successfully")
                    {
                        Member_Nominee mn = new Member_Nominee();
                        mn.branch_id = model.branch_id.ToUpper();
                        mn.mem_id = model.mem_id;
                        mn.nom_hno = model.nominee_hno;
                        mn.nom_add1 = model.nominee_add1.ToUpper();
                        if (model.nominee_add2 == null)
                        {
                            mn.nom_add2 = "";
                        }
                        else
                        {
                            mn.nom_add2 = model.nominee_add2.ToUpper();
                        }
                        if (model.nominee_city == null)
                        {
                            mn.nom_city = "";
                        }
                        else
                        {
                            mn.nom_city = model.nominee_city.ToUpper();
                        }
                        if (model.nominee_dist == null)
                        {
                            mn.nom_dist = "";
                        }
                        else
                        {
                            mn.nom_dist = model.nominee_dist.ToUpper();
                        }
                        mn.nom_name = model.nominee_name.ToUpper();
                        if (model.nominee_state == null)
                        {
                            mn.nom_state = "";
                        }
                        else
                        {
                            mn.nom_state = model.nominee_state.Substring(0, 2).ToUpper();
                        }
                        mn.nom_pin = model.nominee_pin;
                        if (model.nominee_birth_date == null)
                        {
                            mn.nom_birthdt = "";
                        }
                        else
                        {
                            mn.nom_birthdt = Convert.ToDateTime(model.nominee_birth_date).ToString("dd-MM-yyyy").Replace("-", "/");
                        }
                        mn.nom_srl = "1";
                        if (model.nominee_rel == null)
                        {
                            mn.nom_rltn_id = "";
                        }
                        else
                        {
                            mn.nom_rltn_id = model.nominee_rel.ToUpper();
                        }
                        mn.SaveMemberNominee(mn);
                        Member_Introducer mi = new Member_Introducer();
                        if (model.intr_mem_no != null && model.intr_mem_no.Length > 0)
                        {
                            mi.branch_id = model.branch_id;
                            mi.mem_id = model.mem_id;
                            mi.intr_srl = Convert.ToInt32(model.srl);
                            mi.intr_member_id = model.intr_mem_no;
                            mi.intr_name = model.intr_mem_name;
                            mi.SaveMemberIntroducer(mi);
                        }
                        Member_Mast m = new Member_Mast();
                        m = m.getbooknoandemployeeidbymemberidandupdate(model.mem_id);
                    }
                    else
                    {
                        model.msg = "Unable To Save";
                    }
                }
                else
                {
                    model.msg = "Employee Id Already Exist";
                }
            }
            else
            {
                model.msg = "Member Id Already Exist";
            }
            return Json(model.msg);
        }
        public JsonResult GetIntroducerName(string intr_mem_no)
        {
            Member_Mast mm = new Member_Mast();
            string membername = mm.getMemberNameByMemberId(intr_mem_no);
            return Json(membername);
        }
        public JsonResult GetRetirementDate(string BrthDt)
        {

            string retirementdt = Convert.ToDateTime(Convert.ToDateTime(BrthDt).ToString("dd/MM/yyyy")).AddDays(60 * 365 + 15).ToString("dd/MM/yyyy").Replace("-", "/");
            int year = Convert.ToDateTime(retirementdt).Year;
            int month = Convert.ToDateTime(retirementdt).Month;
            int day = DateTime.DaysInMonth(year, month);
            string mm = "";
            if (Convert.ToString(month).Length < 2)
            {
                mm = "0" + Convert.ToString(month);
            }
            else
            {
                mm = Convert.ToString(month);
            }
            retirementdt = Convert.ToString(day) + "/" + mm + "/" + Convert.ToString(year);
            return Json(retirementdt);
        }
        public JsonResult getMemberdetailsbyMemberId(string branch_id, string mem_id)
        {
            Member_Mast mm = new Member_Mast();
            MemberMasterViewModel model = new MemberMasterViewModel();
            mm = mm.getdetails(branch_id, mem_id);
            if (mm.sex == "M")
            {
                mm.sex = "MALE";
            }
            else
            {
                mm.sex = "FEMALE";
            }
            model.emp_cd = mm.emp_cd;
            model.caste = mm.caste;
            model.member_category = mm.member_category;
            model.book_no = mm.book_no;
            model.emp_branch = mm.emp_branch;
            model.emp_id = mm.emp_id;
            model.mem_date = mm.mem_date.ToString("dd-MM-yyyy").Replace("-", "/");
            model.member_type = mm.member_type;
            model.f_name = mm.f_name;
            model.l_name = mm.l_name;
            model.guardian_name = mm.guardian_name;
            model.member_rel = mm.member_rel;
            model.mailAdd_house = mm.mailAdd_house;
            model.mailAdd_add1 = mm.mailAdd_add1;
            model.mailAdd_add2 = mm.mailAdd_add2;
            model.mailAdd_city = mm.mailAdd_city;
            model.mailAdd_dist = mm.mailAdd_dist;
            model.mailAdd_state = mm.mailAdd_state;
            model.mailAdd_pin = mm.mailAdd_pin;
            model.prmntAdd_house = mm.prmntAdd_house;
            model.prmntAdd_add1 = mm.prmntAdd_add1;
            model.prmntAdd_add2 = mm.prmntAdd_add2;
            model.prmntAdd_city = mm.prmntAdd_city;
            model.prmntAdd_dist = mm.prmntAdd_dist;
            model.prmntAdd_state = mm.prmntAdd_state;
            model.prmntAdd_pin = mm.prmntAdd_pin;
            model.phn = mm.phn;
            model.th_f_amt = Convert.ToString(mm.th_f_amt);
            model.birth_date = Convert.ToDateTime(mm.birth_date).ToString("dd-MM-yyyy").Replace("-", "/");
            model.join_dt = Convert.ToDateTime(mm.join_dt).ToString("dd-MM-yyyy").Replace("-", "/");
            model.retmnt_dt = Convert.ToDateTime(mm.retmnt_dt).ToString("dd-MM-yyyy").Replace("-", "/");
            if (Convert.ToDateTime(mm.exp_dt).ToString("dd-MM-yyyy").Replace("-", "/") == "01/01/0001")
            {
                model.exp_dt = "";
            }
            else
            {
                model.exp_dt = Convert.ToDateTime(mm.exp_dt).ToString("dd-MM-yyyy").Replace("-", "/");
            }
            if (Convert.ToDateTime(mm.close_dt).ToString("dd-MM-yyyy").Replace("-", "/") == "01/01/0001")
            {
                model.close_dt = "";
            }
            else
            {
                model.close_dt = Convert.ToDateTime(mm.close_dt).ToString("dd-MM-yyyy").Replace("-", "/");
            }
            //model.exp_dt = Convert.ToDateTime(mm.exp_dt).ToString("dd-MM-yyyy");
            //model.close_dt = Convert.ToDateTime(mm.close_dt).ToString("dd-MM-yyyy");
            model.tf_buffer = Convert.ToString(mm.tf_buffer);
            model.sex = mm.sex;
            model.relgn = mm.relgn;
            model.bank_code = mm.bank_code;
            model.accc_no = mm.accc_no;
            model.share = Convert.ToString(mm.share);
            model.remarks = mm.remarks;
            model.occupation = mm.occupation;
            model.serv_sts = mm.serv_sts;
            model.dept = mm.dept;
            model.desig = mm.desig;
            model.pan = mm.pan;
            model.nominee_add1 = mm.nominee_add1;
            model.nominee_add2 = mm.nominee_add2;
            model.nominee_city = mm.nominee_city;
            model.nominee_dist = mm.nominee_dist;
            model.nominee_state = mm.nom_state;
            model.nominee_pin = mm.nominee_pin;
            model.nominee_birth_date = mm.nom_birthdt;
            model.nominee_name = mm.nominee_name;
            model.nominee_rel = mm.nominee_rel;
            model.intr_mem_name = mm.intr_name;
            model.srl = Convert.ToString(mm.intr_srl);
            model.intr_mem_no = mm.intr_member_id;
            model.ident_mark = mm.ident_mark;
            if (mm.trans == "Y")
            {
                model.trans = true;
            }
            else
            {
                model.trans = false;
            }
            if (mm.ret == "Y")
            {
                model.ret = true;
            }
            else
            {
                model.ret = false;
            }
            if (mm.married == 1)
            {
                model.married = true;
            }
            else
            {
                model.married = false;
            }
            if (mm.ltl_app == 1)
            {
                model.ltl_app = true;
            }
            else
            {
                model.ltl_app = false;
            }
            if (mm.exp == "D")
            {
                model.exp = true;
            }
            else
            {
                model.exp = false;
            }
            if (mm.mem_closed == "C")
            {
                model.mem_closed = true;
            }
            else
            {
                model.mem_closed = false;
            }
            if (mm.tf_double == "D")
            {
                model.tf_double = true;
            }
            else
            {
                model.tf_double = false;
            }
            model.msg = mm.msg;
            return Json(model);
            //return Json(mm);
        }
        public JsonResult getMemberdetailsbyEmployeeId(string branch_id, string emp_id)
        {
            Member_Mast mm = new Member_Mast();
            MemberMasterViewModel model = new MemberMasterViewModel();
            mm = mm.getdetailsbyempid(branch_id, emp_id);
            if (mm.sex == "M")
            {
                mm.sex = "MALE";
            }
            else
            {
                mm.sex = "FEMALE";
            }
            model.emp_cd = mm.emp_cd;
            model.caste = mm.caste;
            model.member_category = mm.member_category;
            model.book_no = mm.book_no;
            model.emp_branch = mm.emp_branch;
            model.mem_id = mm.mem_id;
            model.mem_date = mm.mem_date.ToString("dd-MM-yyyy").Replace("-", "/");
            model.member_type = mm.member_type;
            model.f_name = mm.f_name;
            model.l_name = mm.l_name;
            model.guardian_name = mm.guardian_name;
            model.member_rel = mm.member_rel;
            model.mailAdd_house = mm.mailAdd_house;
            model.mailAdd_add1 = mm.mailAdd_add1;
            model.mailAdd_add2 = mm.mailAdd_add2;
            model.mailAdd_city = mm.mailAdd_city;
            model.mailAdd_dist = mm.mailAdd_dist;
            model.mailAdd_state = mm.mailAdd_state;
            model.mailAdd_pin = mm.mailAdd_pin;
            model.prmntAdd_house = mm.prmntAdd_house;
            model.prmntAdd_add1 = mm.prmntAdd_add1;
            model.prmntAdd_add2 = mm.prmntAdd_add2;
            model.prmntAdd_city = mm.prmntAdd_city;
            model.prmntAdd_dist = mm.prmntAdd_dist;
            model.prmntAdd_state = mm.prmntAdd_state;
            model.prmntAdd_pin = mm.prmntAdd_pin;
            model.phn = mm.phn;
            model.th_f_amt = Convert.ToString(mm.th_f_amt);
            model.birth_date = Convert.ToDateTime(mm.birth_date).ToString("dd-MM-yyyy").Replace("-", "/");
            model.join_dt = Convert.ToDateTime(mm.join_dt).ToString("dd-MM-yyyy").Replace("-", "/");
            model.retmnt_dt = Convert.ToDateTime(mm.retmnt_dt).ToString("dd-MM-yyyy").Replace("-", "/");
            if (Convert.ToDateTime(mm.exp_dt).ToString("dd-MM-yyyy").Replace("-", "/") == "01/01/0001")
            {
                model.exp_dt = "";
            }

            else
            {
                model.exp_dt = Convert.ToDateTime(mm.exp_dt).ToString("dd-MM-yyyy").Replace("-", "/");
            }
            if (Convert.ToDateTime(mm.close_dt).ToString("dd-MM-yyyy").Replace("-", "/") == "01/01/0001")
            {
                model.close_dt = "";
            }
            else
            {
                model.close_dt = Convert.ToDateTime(mm.close_dt).ToString("dd-MM-yyyy").Replace("-", "/");
            }
            model.tf_buffer = Convert.ToString(mm.tf_buffer);
            model.sex = mm.sex;
            model.relgn = mm.relgn;
            model.bank_code = mm.bank_code;
            model.accc_no = mm.accc_no;
            model.share = Convert.ToString(mm.share);
            model.remarks = mm.remarks;
            model.occupation = mm.occupation;
            model.serv_sts = mm.serv_sts;
            model.dept = mm.dept;
            model.desig = mm.desig;
            model.pan = mm.pan;
            model.nominee_add1 = mm.nominee_add1;
            model.nominee_add2 = mm.nominee_add2;
            model.nominee_city = mm.nominee_city;
            model.nominee_dist = mm.nominee_dist;
            model.nominee_state = mm.nom_state;
            model.nominee_pin = mm.nominee_pin;
            model.nominee_birth_date = mm.nom_birthdt;
            model.nominee_name = mm.nominee_name;
            model.nominee_rel = mm.nominee_rel;
            model.intr_mem_name = mm.intr_name;
            model.srl = Convert.ToString(mm.intr_srl);
            model.intr_mem_no = mm.intr_member_id;
            model.ident_mark = mm.ident_mark;
            if (mm.trans == "Y")
            {
                model.trans = true;
            }
            else
            {
                model.trans = false;
            }
            if (mm.ret == "Y")
            {
                model.ret = true;
            }
            else
            {
                model.ret = false;
            }
            if (mm.married == 1)
            {
                model.married = true;
            }
            else
            {
                model.married = false;
            }
            if (mm.ltl_app == 1)
            {
                model.ltl_app = true;
            }
            else
            {
                model.ltl_app = false;
            }
            if (mm.exp == "D")
            {
                model.exp = true;
            }
            else
            {
                model.exp = false;
            }
            if (mm.mem_closed == "C")
            {
                model.mem_closed = true;
            }
            else
            {
                model.mem_closed = false;
            }
            if (mm.tf_double == "D")
            {
                model.tf_double = true;
            }
            else
            {
                model.tf_double = false;
            }
            model.msg = mm.msg;
            //return Json(mm);
            return Json(model);
        }
        public JsonResult UpdateRec(MemberMasterViewModel model)
        {
            Member_Mast mm = new Member_Mast();
            mm.branch_id = model.branch_id.ToUpper();
            mm.mem_id = model.mem_id;
            mm.mem_date = Convert.ToDateTime(model.mem_date);
            mm.member_type = model.member_type.ToUpper();
            mm.member_category = model.member_category.ToUpper();
            mm.emp_cd = model.emp_cd;
            mm.emp_branch = model.emp_branch;
            mm.emp_id = model.emp_id;
            mm.book_no = model.book_no;
            mm.f_name = model.f_name.ToUpper();
            mm.l_name = model.l_name.ToUpper();
            mm.mem_name = (mm.f_name + " " + mm.l_name).ToUpper();
            mm.birth_date = Convert.ToDateTime(model.birth_date);
            if (model.sex == "MALE")
            {
                mm.sex = "M";
            }
            else if (model.sex == "FEMALE")
            {
                mm.sex = "F";
            }
            else
            {
                mm.sex = "";
            }
            mm.guardian_name = model.guardian_name.ToUpper();
            mm.member_rel = model.member_rel.ToUpper();
            mm.caste = model.caste.ToUpper();
            mm.relgn = model.relgn.ToUpper();
            mm.occupation = model.occupation;
            if (model.mailAdd_house == null)
            {
                mm.mailAdd_house = "";
            }
            else
            {
                mm.mailAdd_house = model.mailAdd_house;
            }
            if (model.mailAdd_add1 == null)
            {
                mm.mailAdd_add1 = "";
            }
            else
            {
                mm.mailAdd_add1 = model.mailAdd_add1.ToUpper();
            }
            if (model.mailAdd_add2 == null)
            {
                mm.mailAdd_add2 = "";
            }
            else
            {
                mm.mailAdd_add2 = model.mailAdd_add2.ToUpper();
            }
            if (model.mailAdd_city == null)
            {
                mm.mailAdd_city = "";
            }
            else
            {
                mm.mailAdd_city = model.mailAdd_city.ToUpper();
            }
            if (model.mailAdd_dist == null)
            {
                mm.mailAdd_dist = "";
            }
            else
            {
                mm.mailAdd_dist = model.mailAdd_dist.ToUpper();
            }
            if (model.mailAdd_state == null)
            {
                mm.mailAdd_state = "";
            }
            else
            {
                mm.mailAdd_state = model.mailAdd_state.Substring(0, 2).ToUpper();
            }
            mm.mailAdd_pin = model.mailAdd_pin;
            if (model.prmntAdd_house == null)
            {
                mm.prmntAdd_house = "";
            }
            else
            {
                mm.prmntAdd_house = model.prmntAdd_house;
            }
            if (model.prmntAdd_add1 == null)
            {
                mm.prmntAdd_add1 = "";
            }
            else
            {
                mm.prmntAdd_add1 = model.prmntAdd_add1.ToUpper();
            }
            if (model.prmntAdd_add2 == null)
            {
                mm.prmntAdd_add2 = "";
            }
            else
            {
                mm.prmntAdd_add2 = model.prmntAdd_add2.ToUpper();
            }
            if (model.prmntAdd_city == null)
            {
                mm.prmntAdd_city = "";
            }
            else
            {
                mm.prmntAdd_city = model.prmntAdd_city.ToUpper();
            }
            if (model.prmntAdd_dist == null)
            {
                mm.prmntAdd_dist = "";
            }
            else
            {
                mm.prmntAdd_dist = model.prmntAdd_dist.ToUpper();
            }
            if (model.prmntAdd_state == null)
            {
                mm.prmntAdd_state = "";
            }
            else
            {
                mm.prmntAdd_state = model.prmntAdd_state.Substring(0, 2).ToUpper();
            }
            mm.prmntAdd_pin = model.prmntAdd_pin;
            mm.ident_mark = model.ident_mark;
            mm.pan = model.pan;
            mm.th_f_amt = Convert.ToDecimal(model.th_f_amt);
            mm.phn = model.phn;
            if (model.tf_buffer != null)
            {
                mm.tf_buffer = Convert.ToDecimal(model.tf_buffer);
            }
            else
            {
                mm.tf_buffer = Convert.ToDecimal("00");
            }
            mm.desig = model.desig;
            if (model.married == true)
            {
                mm.married = 1;
            }
            else
            {
                mm.married = 0;
            }
            if (model.ltl_app == true)
            {
                mm.ltl_app = 1;
            }
            else
            {
                mm.ltl_app = 0;
            }
            mm.serv_sts = model.serv_sts;
            mm.dept = model.dept;
            mm.join_dt = Convert.ToDateTime(Convert.ToDateTime((model.join_dt)).ToString("dd-MM-yyyy").Replace("-", "/"));
            mm.retmnt_dt = Convert.ToDateTime(Convert.ToDateTime(model.retmnt_dt).ToString("dd-MM-yyyy").Replace("-", "/"));
            if (model.share == null)
            {
                mm.share = 0;
            }
            else
            {
                mm.share = Convert.ToDecimal(model.share);
            }
            if (model.remarks == null)
            {
                mm.remarks = "";
            }
            else
            {
                mm.remarks = model.remarks;
            }
            if (model.bank_code == null)
            {
                mm.bank_code = "";
            }
            else
            {
                mm.bank_code = model.bank_code;
            }
            if (model.accc_no == null)
            {
                mm.accc_no = "";
            }
            else
            {
                mm.accc_no = model.accc_no;
            }
            //mm.blood_group = model.BloodGroup;
            if (model.exp == true)
            {
                mm.exp = "D";
                mm.exp_dt = Convert.ToDateTime(Convert.ToDateTime(model.exp_dt).ToString("dd-MM-yyyy").Replace("-", "/"));
            }
            else
            {
                mm.exp = "A";
                mm.exp_dt = null;
            }
            if (model.mem_closed == true)
            {
                mm.mem_closed = "C";
                mm.close_dt = Convert.ToDateTime(Convert.ToDateTime(model.close_dt).ToString("dd-MM-yyyy").Replace("-", "/"));
            }
            else
            {
                mm.mem_closed = "";
                mm.close_dt = null;
            }
            if (model.ret == true)
            {
                mm.ret = "Y";
            }
            else
            {
                mm.ret = "";
            }
            if (model.trans == true)
            {
                mm.trans = "Y";
            }
            else
            {
                mm.trans = "";
            }
            if (model.tf_double == true)
            {
                mm.tf_double = "D";
            }
            else
            {
                mm.tf_double = "S";
            }
            model.msg = mm.UpdateMemberMaster(mm);
            if (model.msg == "Updated Successfuly")
            {
                Member_Nominee mn = new Member_Nominee();
                mn.branch_id = model.branch_id.ToUpper();
                mn.mem_id = model.mem_id;
                mn.nom_hno = model.mailAdd_house;
                mn.nom_add1 = model.mailAdd_add1.ToUpper();
                if (model.nominee_add2 == null)
                {
                    mn.nom_add2 = "";
                }
                else
                {
                    mn.nom_add2 = model.nominee_add2.ToUpper();
                }
                if (model.nominee_city == null)
                {
                    mn.nom_city = "";
                }
                else
                {
                    mn.nom_city = model.nominee_city.ToUpper();
                }
                if (model.nominee_dist == null)
                {
                    mn.nom_dist = "";
                }
                else
                {
                    mn.nom_dist = model.nominee_dist.ToUpper();
                }
                mn.nom_name = model.nominee_name.ToUpper();
                if (model.nominee_state == null)
                {
                    mn.nom_state = "";
                }
                else
                {
                    mn.nom_state = model.nominee_state.Substring(0, 2).ToUpper();
                }
                mn.nom_pin = model.nominee_pin;
                if (model.nominee_birth_date == null)
                {
                    mn.nom_birthdt = "";
                }
                else
                {
                    mn.nom_birthdt = Convert.ToDateTime(model.nominee_birth_date).ToString("dd-MM-yyyy").Replace("-", "/");
                }
                mn.nom_srl = "1";
                if (model.nominee_rel == null)
                {
                    mn.nom_rltn_id = "";
                }
                else
                {
                    mn.nom_rltn_id = model.nominee_rel.ToUpper();
                }
                mn.SaveMemberNominee(mn);
                Member_Introducer mi = new Member_Introducer();
                if (model.intr_mem_no != null && model.intr_mem_no.Length > 0)
                {
                    mi.branch_id = model.branch_id;
                    mi.mem_id = model.mem_id;
                    mi.intr_srl = Convert.ToInt32(model.srl);
                    mi.intr_member_id = model.intr_mem_no;
                    mi.intr_name = model.intr_mem_name;
                    mi.SaveMemberIntroducer(mi);
                }
                Member_Mast m = new Member_Mast();
                m = m.getbooknoandemployeeidbymemberidandupdate(model.mem_id);
            }
            return Json(model.msg);
        }
        public JsonResult getallmemberdetails()
        {
            MemberMasterViewModel model = new MemberMasterViewModel();
            Member_Mast mm = new Member_Mast();
            List<Member_Mast> mml = new List<Member_Mast>();
            mml = mm.getallmemberdetails();
            int i = 1;
            string mem_date = "";
            if (mml.Count > 0)
            {
                foreach (var a in mml)
                {
                    if (Convert.ToDateTime(a.mem_date).ToString("dd-MM-yyyy").Replace("-", "/") == "01/01/0001")
                    {
                        mem_date = "";
                    }
                    else
                    {
                        mem_date = Convert.ToDateTime(a.mem_date).ToString("dd-MM-yyyy").Replace("-", "/");
                    }
                    if (i == 1)
                    {
                        model.tableelement = "<tr><th>Srl</th><th>Membership No</th><th>Membership Date</th><th>Member Type</th><th>Category</th><th>Name Of Member</th></tr>";
                        model.tableelement = model.tableelement + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.mem_id + "</td><td>" + mem_date + "</td><td>" + a.member_type + "</td><td>" + a.member_category + "</td><td>" + a.mem_name + "</td></tr>";
                    }
                    else
                    {
                        model.tableelement = model.tableelement + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.mem_id + "</td><td>" + mem_date + "</td><td>" + a.member_type + "</td><td>" + a.member_category + "</td><td>" + a.mem_name + "</td></tr>";
                    }
                    i = i + 1;
                }
            }
            else
            {
                model.tableelement = null;
            }
            return Json(model);
        }
        /********************************************Member Master End*******************************************/

        /********************************************Member OpeningClosing List Start*******************************************/
        [HttpGet]
        public ActionResult MemberOpeningandClosingReg(MemberOpeningandClosingRegViewModel model)
        {
            UtilityController u = new UtilityController();
            model.BranchDesc = u.getBranchMastDetails();
            model.TypeDesc = u.getTypeMastDetails();
            model.fr_dt = DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/");
            model.to_dt = DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/");
            return View(model);
        }
        public JsonResult GetMemberOpeningClosingDetails(string searchtype, string branch_id, string mem_type, string fr_dt, string to_dt)
        {
            MemberOpeningandClosingRegViewModel model = new MemberOpeningandClosingRegViewModel();
            Member_Mast mm = new Member_Mast();
            List<Member_Mast> mml = new List<Member_Mast>();
            mml = mm.getdetails(searchtype, branch_id, mem_type, fr_dt, to_dt);
            string fd = string.Empty;
            int i = 1;
            decimal xtopsh = 0;
            decimal xtoptf = 0;
            decimal xtclsh = 0;
            decimal xtcltf = 0;

            if (searchtype == "Opening")
            {
                if (mml.Count > 0)
                {
                    foreach (var a in mml)
                    {
                        if (i == 1)
                        {
                            if (a.member_type == "GEN")
                            {
                                model.tableelement = "<tr><th>SR No</th><th>Man No</th><th>Member No</th><th>Opening Dt.</th><th>Type</th><th>Category</th><th>Member's Name</th><th>Share Op/Amount</th><th>Tf Op/Amount</th></tr>";
                                model.tableelement = model.tableelement + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.emp_id + "</td><td>" + a.mem_id + "</td><td>" + a.mem_date.ToString("dd-MM-yyyy").Replace("-", "/") + "</td><td>" + a.member_type + "</td><td>" + a.member_category + "</td><td>" + a.mem_name + "</td><td>" + a.tramt.ToString("0.00") + "</td><td>" + "" + "</td></tr>";
                                xtopsh = (xtopsh + a.tramt);
                            }
                            else
                            {
                                model.tableelement = "<tr><th>SR No</th><th>Man No</th><th>Member No</th><th>Opening Dt.</th><th>Type</th><th>Category</th><th>Member's Name</th><th>Share Op/Amount</th><th>Tf Op/Amount</th></tr>";
                                model.tableelement = model.tableelement + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.emp_id + "</td><td>" + a.mem_id + "</td><td>" + a.mem_date.ToString("dd-MM-yyyy").Replace("-", "/") + "</td><td>" + a.member_type + "</td><td>" + a.member_category + "</td><td>" + a.mem_name + "</td><td>" + "" + "</td><td>" + a.tramt.ToString("0.00") + "</td></tr>";
                                xtoptf = (xtoptf + a.tramt);
                            }
                        }
                        else
                        {
                            if (a.member_type == "GEN")
                            {
                                //fd = "<tr><th>SR No</th><th>Man No</th><th>Member No</th><th>Opening Dt.</th><th>Type</th><th>Category</th><th>Member's Name</th><th>Share Op/Amount</th><th>Tf Op/Amount</th></tr>";
                                model.tableelement = model.tableelement + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.emp_id + "</td><td>" + a.mem_id + "</td><td>" + a.mem_date.ToString("dd-MM-yyyy").Replace("-", "/") + "</td><td>" + a.member_type + "</td><td>" + a.member_category + "</td><td>" + a.mem_name + "</td><td>" + a.tramt.ToString("0.00") + "</td><td>" + "" + "</td></tr>";
                                xtopsh = (xtopsh + a.tramt);
                            }
                            else
                            {
                                //fd = "<tr><th>SR No</th><th>Man No</th><th>Member No</th><th>Opening Dt.</th><th>Type</th><th>Category</th><th>Member's Name</th><th>Share Op/Amount</th><th>Tf Op/Amount</th></tr>";
                                model.tableelement = model.tableelement + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.emp_id + "</td><td>" + a.mem_id + "</td><td>" + a.mem_date.ToString("dd-MM-yyyy").Replace("-", "/") + "</td><td>" + a.member_type + "</td><td>" + a.member_category + "</td><td>" + a.mem_name + "</td><td>" + "" + "</td><td>" + a.tramt.ToString("0.00") + "</td></tr>";
                                xtoptf = (xtoptf + a.tramt);
                            }
                        }
                        i = i + 1;
                    }
                    model.tot_xtopsh = xtopsh.ToString("0.00");
                    model.tot_xtoptf = xtoptf.ToString("0.00");
                }
                else
                {
                    //fd = "";
                    model.tableelement = null;
                }
            }
            else
            {
                if (mml.Count > 0)
                {
                    foreach (var a in mml)
                    {
                        if (i == 1)
                        {
                            if (a.member_type == "GEN")
                            {
                                model.tableelement = "<tr><th>SR No</th><th>Man No</th><th>Member No</th><th>Opening Dt.</th><th>Closing Dt.</th><th>Type</th><th>Category</th><th>Member's Name</th><th>Share Cl/Payment</th><th>Tf Cl/Payment</th></tr>";
                                model.tableelement = model.tableelement + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.emp_id + "</td><td>" + a.mem_id + "</td><td>" + a.mem_date.ToString("dd-MM-yyyy").Replace("-", "/") + "</td><td>" + Convert.ToDateTime(a.close_dt).ToString("dd-MM-yyyy").Replace("-", "/") + "</td><td>" + a.member_type + "</td><td>" + a.member_category + "</td><td>" + a.mem_name + "</td><td>" + a.tramt.ToString("0.00") + "</td><td>" + "" + "</td></tr>";
                                xtclsh = (xtclsh + a.tramt);
                            }
                            else
                            {
                                model.tableelement = "<tr><th>SR No</th><th>Man No</th><th>Member No</th><th>Opening Dt.</th><th>Closing Dt.</th><th>Type</th><th>Category</th><th>Member's Name</th><th>Share Cl/Payment</th><th>Tf Cl/Payment</th></tr>";
                                model.tableelement = model.tableelement + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.emp_id + "</td><td>" + a.mem_id + "</td><td>" + a.mem_date.ToString("dd-MM-yyyy").Replace("-", "/") + "</td><td>" + Convert.ToDateTime(a.close_dt).ToString("dd-MM-yyyy").Replace("-", "/") + "</td><td>" + a.member_type + "</td><td>" + a.member_category + "</td><td>" + a.mem_name + "</td><td>" + "" + "</td><td>" + a.tramt.ToString("0.00") + "</td></tr>";
                                xtcltf = (xtcltf + a.tramt);
                            }
                        }
                        else
                        {
                            if (a.member_type == "GEN")
                            {
                                //fd = "<tr><th>SR No</th><th>Man No</th><th>Member No</th><th>Opening Dt.</th><th>Type</th><th>Category</th><th>Member's Name</th><th>Share Op/Amount</th><th>Tf Op/Amount</th></tr>";
                                model.tableelement = model.tableelement + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.emp_id + "</td><td>" + a.mem_id + "</td><td>" + a.mem_date.ToString("dd-MM-yyyy").Replace("-", "/") + "</td><td>" + Convert.ToDateTime(a.close_dt).ToString("dd-MM-yyyy").Replace("-", "/") + "</td><td>" + a.member_type + "</td><td>" + a.member_category + "</td><td>" + a.mem_name + "</td><td>" + a.tramt.ToString("0.00") + "</td><td>" + "" + "</td></tr>";
                                xtclsh = (xtclsh + a.tramt);
                            }
                            else
                            {
                                //fd = "<tr><th>SR No</th><th>Man No</th><th>Member No</th><th>Opening Dt.</th><th>Type</th><th>Category</th><th>Member's Name</th><th>Share Op/Amount</th><th>Tf Op/Amount</th></tr>";
                                model.tableelement = model.tableelement + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.emp_id + "</td><td>" + a.mem_id + "</td><td>" + a.mem_date.ToString("dd-MM-yyyy").Replace("-", "/") + "</td><td>" + Convert.ToDateTime(a.close_dt).ToString("dd-MM-yyyy").Replace("-", "/") + "</td><td>" + a.member_type + "</td><td>" + a.member_category + "</td><td>" + a.mem_name + "</td><td>" + "" + "</td><td>" + a.tramt.ToString("0.00") + "</td></tr>";
                                xtcltf = (xtcltf + a.tramt);
                            }
                        }
                        i = i + 1;
                    }
                    model.tot_xtcltf = xtcltf.ToString("0.00");
                    model.tot_xtclsh = xtclsh.ToString("0.00");
                }
                else
                {
                    //fd = "";
                    model.tableelement = null;
                }
            }
            return Json(model);
        }
        public ActionResult MembersOpeningClosingList(string searchtype, string branch_id, string mem_type, string fr_dt, string to_dt)
        {
            Licence lc = new Licence();
            lc = lc.getlicencedetails();
            Member_Mast mm = new Member_Mast();
            List<Member_Mast> mml = new List<Member_Mast>();
            mml = mm.getdetails(searchtype, branch_id, mem_type, fr_dt, to_dt);
            Directory.CreateDirectory(Server.MapPath("~/wwwroot\\TextFiles"));
            if (searchtype == "Opening")
            {
                using (StreamWriter sw = new StreamWriter(Server.MapPath("~/wwwroot\\TextFiles\\Member_Opening_Closing_List.txt")))
                {
                    int Pg = 1;
                    int Ln = 0;
                    int i = 1;
                    decimal tramt = 0;
                    string mem_name = "";
                    sw.WriteLine("                       " + lc.lic_name);
                    sw.WriteLine("                               " + lc.lic_add1 + "," + lc.lic_add2 + " PHONE : " + lc.lic_phone);
                    sw.WriteLine("");
                    sw.WriteLine("                        MEMBER OPENING LIST FROM " + fr_dt + " TO " + to_dt);
                    sw.WriteLine("======================================================================================");
                    sw.WriteLine("Employee ID |Member No. |Member Name                        |Opening Date|Share Amount");
                    sw.WriteLine("======================================================================================");
                    foreach (var am in mml)
                    {
                        if (am.tramt.ToString().Length > 11)
                        {
                            tramt = Convert.ToDecimal(Convert.ToString(am.tramt).Substring(0, 10));
                        }
                        else
                        {
                            tramt = am.tramt;
                        }
                        if (am.mem_name.ToString().Length > 35)
                        {
                            mem_name = (am.mem_name).Substring(0, 34);
                        }
                        else
                        {
                            mem_name = am.mem_name;
                        }
                        if (Ln > Pg * 65)
                        {
                            Pg = Pg + 1;
                            Ln = Ln + 7;
                            sw.WriteLine("");
                            sw.WriteLine("                       " + lc.lic_name);
                            sw.WriteLine("                               " + lc.lic_add1 + "," + lc.lic_add2 + " PHONE : " + lc.lic_phone);
                            sw.WriteLine("");
                            sw.WriteLine("                        MEMBER OPENING LIST FROM " + fr_dt + " TO " + to_dt);
                            sw.WriteLine("======================================================================================");
                            sw.WriteLine("Employee ID |Member No. |Member Name                        |Opening Date|Share Amount");
                            sw.WriteLine("======================================================================================");
                        }
                        if (am.member_type == "GEN")
                        {
                            sw.WriteLine("".ToString().PadLeft(12 - (am.emp_id).Length) + am.emp_id + "|"
                            + "".ToString().PadLeft(11 - (am.mem_id).ToString().Length) + am.mem_id + "|"
                            + "".ToString().PadLeft(35 - (mem_name).ToString().Length) + mem_name + "|"
                            + "".ToString().PadLeft(12 - (am.mem_date).ToString("dd-MM-yyyy").Replace("-", "/").Length) + (am.mem_date).ToString("dd-MM-yyyy").Replace("-", "/") + "|"
                            + "".ToString().PadLeft(11 - (tramt).ToString().Length) + tramt.ToString("0.00") + "|");

                            //sw.WriteLine(am.emp_id.ToString().PadLeft(6) + am.mem_id.ToString().PadLeft(14) + am.mem_name.ToString().PadLeft(34) + am.mem_date.ToString("dd-MM-yyyy").Replace("-", "/").PadLeft(18) + am.tramt.ToString("0.00"));
                        }
                        else
                        {
                            sw.WriteLine("".ToString().PadLeft(12 - (am.emp_id).Length) + am.emp_id + "|"
                            + "".ToString().PadLeft(11 - (am.mem_id).ToString().Length) + am.mem_id + "|"
                            + "".ToString().PadLeft(35 - (mem_name).ToString().Length) + mem_name + "|"
                            + "".ToString().PadLeft(12 - (am.mem_date).ToString("dd-MM-yyyy").Replace("-", "/").Length) + (am.mem_date).ToString("dd-MM-yyyy").Replace("-", "/") + "|"
                            + "".ToString().PadLeft(11 - ("").ToString().Length) + "" + "|");

                            //sw.WriteLine(am.emp_id.ToString().PadLeft(6) + am.mem_id.ToString().PadLeft(14) + am.mem_name.ToString().PadLeft(34) + am.mem_date.ToString("dd-MM-yyyy").Replace("-", "/").PadLeft(18) + "");
                        }
                        Ln = Ln + 1;
                        i = i + 1;
                    }
                }
            }
            else
            {
                using (StreamWriter sw = new StreamWriter(Server.MapPath("~/wwwroot\\TextFiles\\Member_Opening_Closing_List.txt")))
                {
                    int Pg = 1;
                    int Ln = 0;
                    int i = 1;
                    decimal tramt = 0;
                    string mem_name = "";
                    sw.WriteLine("                       " + lc.lic_name);
                    sw.WriteLine("                               " + lc.lic_add1 + "," + lc.lic_add2 + " PHONE : " + lc.lic_phone);
                    sw.WriteLine("");
                    sw.WriteLine("                        MEMBER CLOSING LIST FROM " + fr_dt + " TO " + to_dt);
                    sw.WriteLine("======================================================================================");
                    sw.WriteLine("Employee ID |Member No. |Member Name                        |Closing Date|Share Amount");
                    sw.WriteLine("======================================================================================");
                    foreach (var am in mml)
                    {
                        if (am.tramt.ToString().Length > 11)
                        {
                            tramt = Convert.ToDecimal(Convert.ToString(am.tramt).Substring(0, 10));
                        }
                        else
                        {
                            tramt = am.tramt;
                        }
                        if (am.mem_name.ToString().Length > 35)
                        {
                            mem_name = (am.mem_name).Substring(0, 34);
                        }
                        else
                        {
                            mem_name = am.mem_name;
                        }
                        if (Ln > Pg * 65)
                        {
                            Pg = Pg + 1;
                            Ln = Ln + 7;
                            sw.WriteLine("");
                            sw.WriteLine("                       " + lc.lic_name);
                            sw.WriteLine("                               " + lc.lic_add1 + "," + lc.lic_add2 + " PHONE : " + lc.lic_phone);
                            sw.WriteLine("");
                            sw.WriteLine("                        MEMBER CLOSING LIST FROM " + fr_dt + " TO " + to_dt);
                            sw.WriteLine("======================================================================================");
                            sw.WriteLine("Employee ID |Member No. |Member Name                        |Closing Date|Share Amount");
                            sw.WriteLine("======================================================================================");
                        }
                        if (am.member_type == "GEN")
                        {
                            sw.WriteLine("".ToString().PadLeft(12 - (am.emp_id).Length) + am.emp_id + "|"
                            + "".ToString().PadLeft(11 - (am.mem_id).ToString().Length) + am.mem_id + "|"
                            + "".ToString().PadLeft(35 - (mem_name).ToString().Length) + mem_name + "|"
                            + "".ToString().PadLeft(12 - Convert.ToDateTime(am.close_dt).ToString("dd-MM-yyyy").Replace("-", "/").Length) + Convert.ToDateTime(am.close_dt).ToString("dd-MM-yyyy").Replace("-", "/") + "|"
                            + "".ToString().PadLeft(11 - (tramt).ToString().Length) + tramt.ToString("0.00") + "|");

                            //sw.WriteLine(am.emp_id.ToString().PadLeft(6) + am.mem_id.ToString().PadLeft(14) + am.mem_name.ToString().PadLeft(34) + Convert.ToDateTime(am.close_dt).ToString("dd-MM-yyyy").Replace("-", "/").PadLeft(18) + am.tramt.ToString("0.00"));
                        }
                        else
                        {
                            sw.WriteLine("".ToString().PadLeft(12 - (am.emp_id).Length) + am.emp_id + "|"
                            + "".ToString().PadLeft(11 - (am.mem_id).ToString().Length) + am.mem_id + "|"
                            + "".ToString().PadLeft(35 - (mem_name).ToString().Length) + mem_name + "|"
                            + "".ToString().PadLeft(12 - Convert.ToDateTime(am.close_dt).ToString("dd-MM-yyyy").Replace("-", "/").Length) + Convert.ToDateTime(am.close_dt).ToString("dd-MM-yyyy").Replace("-", "/") + "|"
                            + "".ToString().PadLeft(11 - ("").ToString().Length) + "" + "|");

                            //sw.WriteLine(am.emp_id.ToString().PadLeft(6) + am.mem_id.ToString().PadLeft(14) + am.mem_name.ToString().PadLeft(34) + Convert.ToDateTime(am.close_dt).ToString("dd-MM-yyyy").Replace("-", "/").PadLeft(18) + "");
                        }
                        Ln = Ln + 1;
                        i = i + 1;
                    }
                }
            }
            UtilityController u = new UtilityController();
            var memory = u.DownloadTextFiles("Member_Opening_Closing_List.txt", Server.MapPath("~/wwwroot\\TextFiles"));
            if (System.IO.File.Exists(Server.MapPath("~/wwwroot\\TextFiles\\Member_Opening_Closing_List.txt")))
            {
                System.IO.File.Delete(Server.MapPath("~/wwwroot\\TextFiles\\Member_Opening_Closing_List.txt"));
            }
            return File(memory.ToArray(), "text/plain", "Member_" + searchtype + "_List_" + DateTime.Now.ToShortDateString().Replace("/", "_") + ".txt");
        }

        /********************************************Member OpeningClosing List End*******************************************/

        /********************************************Member Status Start*******************************************/
        [HttpGet]
        public ActionResult MemberStatus(MemberStatusViewModel model)
        {
            UtilityController u = new UtilityController();
            model.TypeDesc = u.getTypeMastDetails();
            model.CategoryDesc = u.getCategoryMastDetails();
            model.BranchDesc = u.getBranchMastDetails();
            return View(model);
        }
        public JsonResult GetMemberRecbymemberId(MemberStatusViewModel model)
        {
            Member_Mast mm = new Member_Mast();
            List<Member_Mast> mml = new List<Member_Mast>();
            mml = mm.getmemberdetailsbymemid(model.BranchID, model.member_no);
            int i = 1;
            string sig = "";
            string lti = "";
            if (mml.Count > 0)
            {
                foreach (var a in mml)
                {
                    if (a.ltl_app == 0)
                    {
                        sig = "YES";
                        lti = "NO";
                    }
                    else
                    {
                        sig = "NO";
                        lti = "YES";
                    }
                    if (i == 1)
                    {
                        model.tableelement = "<tr><th>Srl</th><th>Mem Id</th><th>Member Name</th><th>Caste</th><th>Sex</th><th>Occup</th><th>Sig</th><th>LTI</th><th>PAN No</th></tr>";
                        model.tableelement = model.tableelement + "<tr><td>" + Convert.ToString(i) + "</td><td>" + model.member_no + "</td><td>" + a.mem_name + "</td><td>" + a.caste + "</td><td>" + a.sex + "</td><td>" + a.occupation + "</td><td>" + sig + "</td><td>" + lti + "</td><td>" + a.pan + "</td></tr>";
                    }
                    else
                    {
                        model.tableelement = model.tableelement + "<tr><td>" + Convert.ToString(i) + "</td><td>" + model.member_no + "</td><td>" + a.mem_name + "</td><td>" + a.caste + "</td><td>" + a.sex + "</td><td>" + a.occupation + "</td><td>" + sig + "</td><td>" + lti + "</td><td>" + a.pan + "</td></tr>";
                    }
                    i = i + 1;
                    model.mail_hno = a.mailAdd_house;
                    model.mail_add1 = a.mailAdd_add1;
                    model.mail_add2 = a.mailAdd_add2;
                    model.mail_city = a.mailAdd_city;
                    model.mail_state = a.mailAdd_state;
                    model.mail_dist = a.mailAdd_dist;
                    model.mail_pin = a.mailAdd_pin;
                    model.member_date = a.mem_date.ToString("dd-MM-yyyy").Replace("-", "/");
                    model.mem_category = a.member_category;
                    model.mem_type = a.member_type;
                    model.name = a.mem_name;
                }
            }
            else
            {
                model.tableelement = null;
            }
            return Json(model);
        }
        public JsonResult GetNomineedetailsBymemberId(MemberStatusViewModel model)
        {
            Member_Nominee mn = new Member_Nominee();
            List<Member_Nominee> mnl = new List<Member_Nominee>();
            mnl = mn.getnomineedetails(model.BranchID, model.member_no);
            int i = 1;
            if (mnl.Count > 0)
            {
                foreach (var a in mnl)
                {
                    if (i == 1)
                    {
                        model.tableelement = "<tr><th>Name Of Nominee</th><th>Relation</th></tr>";
                        model.tableelement = model.tableelement + "<tr><td>" + a.nom_name + "</td><td>" + a.nom_rltn_id + "</td></tr>";
                    }
                    else
                    {
                        model.tableelement = model.tableelement + "<tr><td>" + a.nom_name + "</td><td>" + a.nom_rltn_id + "</td></tr>";
                    }
                    i = i + 1;
                }
            }
            else
            {
                model.tableelement = null;
            }
            return Json(model);
        }
        public JsonResult GetLoanSurityInfoBymemberId(MemberStatusViewModel model)
        {
            Loan_Master lm = new Loan_Master();
            List<Loan_Master> lml = new List<Loan_Master>();
            lml = lm.getloansuritydetails(model.BranchID, model.member_no);
            int i = 1;
            if (lml.Count > 0)
            {
                foreach (var a in lml)
                {
                    if (i == 1)
                    {
                        model.tableelement = "<tr><th>Srl</th><th>Loan Type</th><th>Loan Id</th><th>Loan Date</th><th>Name of Lonee</th><th>Loan Amount</th></tr>";
                        model.tableelement = model.tableelement + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.ac_hd + "</td><td>" + a.emp_id + "</td><td>" + a.loan_dt.ToString("dd-MM-yyyy").Replace("-", "/") + "</td><td>" + a.loanee_name + "</td><td>" + a.loan_amt.ToString("0.00") + "</td></tr>";
                    }
                    else
                    {
                        model.tableelement = model.tableelement + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.ac_hd + "</td><td>" + a.emp_id + "</td><td>" + a.loan_dt.ToString("dd-MM-yyyy").Replace("-", "/") + "</td><td>" + a.loanee_name + "</td><td>" + a.loan_amt.ToString("0.00") + "</td></tr>";
                    }
                    i = i + 1;
                }
            }
            else
            {
                model.tableelement = null;
            }
            return Json(model);
        }
        public JsonResult GetShareCapital(MemberStatusViewModel model)
        {
            decimal shcap_tot = 0;
            SHARE_LEDGER sl = new SHARE_LEDGER();
            List<SHARE_LEDGER> sll = new List<SHARE_LEDGER>();
            sll = sl.getShareLedgerDetail(model.BranchID, model.member_no);
            int i = 1;
            if (sll.Count > 0)
            {
                foreach (var a in sll)
                {
                    if (i == 1)
                    {
                        model.tableelement = "<tr><th>Account Head</th><th>Existing Capital</th></tr>";
                        model.tableelement = model.tableelement + "<tr><td>" + a.acc_desc + "</td><td>" + a.bal_amount.ToString("0.00") + "</td></tr>";
                    }
                    else
                    {
                        model.tableelement = model.tableelement + "<tr><td>" + a.acc_desc + "</td><td>" + a.bal_amount.ToString("0.00") + "</td></tr>";
                    }
                    i = i + 1;
                    shcap_tot = shcap_tot + a.bal_amount;
                }
                model.share_cap_tot = shcap_tot.ToString("0.00");
            }
            else
            {
                model.tableelement = null;
            }
            return Json(model);
        }
        public JsonResult GetMemberFund(MemberStatusViewModel model)
        {
            //decimal int_payble = 0;
            FUNDDEP_MAST fdm = new FUNDDEP_MAST();            
            List<FUNDDEP_MAST> fdml = new List<FUNDDEP_MAST>();
            fdml = fdm.getFundDepositInfo(model.BranchID, model.member_no);
            int i = 1;
            decimal fnd_dep = 0;
            decimal dep = 0;
            decimal tot_int = 0;
            if (fdml.Count > 0)
            {
                foreach (var a in fdml)
                {
                    if(a.is_deposit != null)
                    {
                        tot_int = get_fdep_int_payble(a.ac_hd, a.int_rate);
                    }                   
                    if (a.ledger_tab == "DIVIDEND_LEDGER")
                    {
                        dep = a.bal_amount + tot_int;
                        if (i == 1)
                        {
                            model.tableelement = "<tr><th>Account Head</th><th>Principal Bal</th><th>Interest Bal</th><th>Int Payable</th><th>Net Payable</th><th>Account Head</th><th>Table</th></tr>";
                            model.tableelement = model.tableelement + "<tr><td>" + a.fund_desc + "</td><td>" + a.bal_amount.ToString("0.00") + "</td><td>" + "" + "</td><td>" + tot_int.ToString("0.00") + "</td><td>" + dep.ToString("0.00") + "</td><td>" + a.ac_hd + "</td><td>" + a.ledger_tab + "</td></tr>";                          
                        }
                        else
                        {
                            model.tableelement = model.tableelement + "<tr><td>" + a.fund_desc + "</td><td>" + a.bal_amount.ToString("0.00") + "</td><td>" + "" + "</td><td>" + tot_int.ToString("0.00") + "</td><td>" + dep.ToString("0.00") + "</td><td>" + a.ac_hd + "</td><td>" + a.ledger_tab + "</td></tr>";
                        }
                    }
                    else
                    {
                        dep = a.prin_bal + a.int_bal + tot_int;
                        if (i == 1)
                        {
                            model.tableelement = "<tr><th>Account Head</th><th>Principal Bal</th><th>Interest Bal</th><th>Int Payable</th><th>Net Payable</th><th>Account Head</th><th>Table</th></tr>";
                            model.tableelement = model.tableelement + "<tr><td>" + a.fund_desc + "</td><td>" + a.prin_bal.ToString("0.00") + "</td><td>" + a.int_bal.ToString("0.00") + "</td><td>" + tot_int.ToString("0.00") + "</td><td>" + dep.ToString("0.00") + "</td><td>" + a.ac_hd + "</td><td>" + a.ledger_tab + "</td></tr>";                            
                        }
                        else
                        {
                            model.tableelement = model.tableelement + "<tr><td>" + a.fund_desc + "</td><td>" + a.prin_bal.ToString("0.00") + "</td><td>" + a.int_bal.ToString("0.00") + "</td><td>" + tot_int.ToString("0.00") + "</td><td>" + dep.ToString("0.00") + "</td><td>" + a.ac_hd + "</td><td>" + a.ledger_tab + "</td></tr>";                            
                        }
                    }
                    i = i + 1;
                    fnd_dep = fnd_dep + dep;
                }
                model.tot_fund = fnd_dep.ToString("0.00");
                model.tot_assets = Convert.ToDecimal(model.share_cap_tot + model.tot_fund).ToString("0.00");
            }
            else
            {
                model.tableelement = null;
            }
            return Json(model);
        }
        public JsonResult GETMEMLOANINFO(MemberStatusViewModel model)
        {
            decimal xtotpayb = 0;
            Loan_Master lm = new Loan_Master();
            List<Loan_Master> lml = new List<Loan_Master>();
            lml = lm.getmemberloandetails(model.BranchID, model.member_no);
            int i = 1;            
            if (lml.Count > 0)
            {
                foreach (var a in lml)
                {                  
                    lm = lm.getloanledger(a.ledger_tab, a.ac_hd, a.branch_id, a.emp_id, a.loan_amt, a.inst_no, a.ln_spcl, a.inst_rate, a.loan_dt);
                    //lm = lm.get_loan_current_due(a.ac_hd, a.loan_amt, a.inst_no, a.ln_spcl, a.inst_rate, a.loan_dt, a.branch_id, a.mem_id);
                    if (i == 1)
                    {
                        model.tableelement = "<tr><th>Account Head</th><th>Emp. Id</th><th>Loan date</th><th>Loan Amount</th><th>Instl</th><th>Sec</th><th>Principal Due</th><th>O/D Principal</th><th>Int.Recv</th><th>Aint.Recv</th><th>Total Recievable</th><th>Account Head</th><th>Table</th></tr>";
                        model.tableelement = model.tableelement + "<tr><td>" + a.ac_desc + "</td><td>" + a.emp_id + "</td><td>" + a.loan_dt.ToString("dd-MM-yyyy").Replace("-", "/") + "</td><td>" + a.loan_amt.ToString("0.00") + "</td><td>" + a.inst_no + "</td><td>" + "" + "</td><td>" + lm.prin_bal.ToString("0.00") + "</td><td>" + lm.xodprin.ToString("0.00") + "</td><td>" + lm.int_bal.ToString("0.00") + "</td><td>" + lm.lbal_aint.ToString("0.00") + "</td><td>" + (lm.prin_bal + lm.int_bal + lm.lbal_aint).ToString("0.00") + "</td><td>" + a.ac_hd + "</td><td>" + a.ledger_tab + "</td></tr>";
                    }
                    else
                    {
                        model.tableelement = model.tableelement + "<tr><td>" + a.ac_desc + "</td><td>" + a.emp_id + "</td><td>" + a.loan_dt.ToString("dd-MM-yyyy").Replace("-", "/") + "</td><td>" + a.loan_amt.ToString("0.00") + "</td><td>" + a.inst_no + "</td><td>" + "" + "</td><td>" + lm.prin_bal.ToString("0.00") + "</td><td>" + lm.xodprin.ToString("0.00") + "</td><td>" + lm.int_bal.ToString("0.00") + "</td><td>" + lm.lbal_aint.ToString("0.00") + "</td><td>" + (lm.prin_bal + lm.int_bal + lm.lbal_aint).ToString("0.00") + "</td><td>" + a.ac_hd + "</td><td>" + a.ledger_tab + "</td></tr>";
                    }
                    i = i + 1;
                    xtotpayb = xtotpayb + lm.prin_bal + lm.int_bal + lm.lbal_aint;
                }
                model.tot_Loan = xtotpayb.ToString("0.00");
                model.tot_liabilities = Convert.ToDecimal(model.tot_Oth_Loan + model.tot_Loan).ToString("0.00");                
            }
            else
            {
                model.tableelement = null;
            }
            return Json(model);
        }
        public JsonResult GETOTHLOANINFO(MemberStatusViewModel model)
        {
            decimal xtotpayb = 0;
            Loan_Master lm = new Loan_Master();
            List<Loan_Master> lml = new List<Loan_Master>();
            lml = lm.getmemberotherloandetails(model.BranchID, model.member_no);
            int i = 1;
            if (lml.Count > 0)
            {
                foreach (var a in lml)
                {
                    if (i == 1)
                    {
                        //lm = lm.get_loan_current_due(a.ac_hd, a.loan_amt, a.inst_no, a.ln_spcl, a.inst_rate, a.loan_dt, a.branch_id, a.mem_id);
                        lm = lm.getloanledger(a.ledger_tab, a.ac_hd, a.branch_id, a.emp_id, a.loan_amt, a.inst_no, a.ln_spcl, a.inst_rate, a.loan_dt);
                        model.tableelement = "<tr><th>Account Head</th><th>Loan Id</th><th>Loan date</th><th>Loan Amount</th><th>Instl</th><th>Sec</th><th>Principal Due</th><th>O/D Principal</th><th>Int.Recv</th><th>Aint.Recv</th><th>Total Recievable</th><th>Account Head</th><th>Table</th></tr>";
                        model.tableelement = model.tableelement + "<tr><td>" + a.ac_desc + "</td><td>" + a.emp_id + "</td><td>" + a.loan_dt.ToString("dd-MM-yyyy").Replace("-", "/") + "</td><td>" + a.loan_amt.ToString("0.00") + "</td><td>" + a.inst_no + "</td><td>" + "" + "</td><td>" + lm.prin_bal.ToString("0.00") + "</td><td>" + lm.xodprin.ToString("0.00") + "</td><td>" + lm.int_bal.ToString("0.00") + "</td><td>" + lm.lbal_aint.ToString("0.00") + "</td><td>" + (lm.prin_bal + lm.int_bal + lm.lbal_aint).ToString("0.00") + "</td><td>" + a.ac_hd + "</td><td>" + a.ledger_tab + "</td></tr>";
                    }
                    else
                    {
                        model.tableelement = model.tableelement + "<tr><td>" + a.ac_desc + "</td><td>" + a.emp_id + "</td><td>" + a.loan_dt.ToString("dd-MM-yyyy").Replace("-", "/") + "</td><td>" + a.loan_amt.ToString("0.00") + "</td><td>" + a.inst_no + "</td><td>" + "" + "</td><td>" + lm.prin_bal.ToString("0.00") + "</td><td>" + lm.xodprin.ToString("0.00") + "</td><td>" + lm.int_bal.ToString("0.00") + "</td><td>" + lm.lbal_aint.ToString("0.00") + "</td><td>" + (lm.prin_bal + lm.int_bal + lm.lbal_aint).ToString("0.00") + "</td><td>" + a.ac_hd + "</td><td>" + a.ledger_tab + "</td></tr>";
                    }
                    i = i + 1;
                    xtotpayb = xtotpayb + lm.prin_bal + lm.int_bal + lm.lbal_aint;
                }
                model.tot_Oth_Loan = xtotpayb.ToString("0.00");
                
            }
            else
            {
                model.tableelement = null;
            }
            return Json(model);
        }             
        public decimal get_fdep_int_payble(string ac_hd, decimal int_rate)
        {
            decimal xtot_int = 0;
            string XLINTDATE = "";
            string xintto = "";
            string xintfr = "";
            string xstdt = "";
            decimal xfmnth = 0;
            MemberStatusViewModel model = new MemberStatusViewModel();
            List<FUNDDEP_MAST> fdml = new List<FUNDDEP_MAST>();
           
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            int year1 = (DateTime.Now.Year) - 1;
            if (month > 3)
            {
                XLINTDATE = "31/03/" + year;
            }
            else
            {
                XLINTDATE = "31/03/" + year1;
            }
            xintfr = Convert.ToDateTime(XLINTDATE).AddDays(1).ToString();
            xfmnth = Convert.ToDecimal(Convert.ToDateTime(XLINTDATE).Subtract(DateTime.Now).Days / (365.25 / 12));
            if(xfmnth > 0)
            {
                xfmnth = Convert.ToDecimal(Convert.ToDateTime(xintfr).Subtract(DateTime.Now).Days / (365.25 / 12));
                xintto = Convert.ToDateTime(xintfr).AddMonths(Convert.ToInt32(xfmnth)).ToString();
                xintto = "01/" + Convert.ToDateTime(xintto).Month + Convert.ToDateTime(xintto).Year;
                xintto = Convert.ToDateTime(xintto).AddDays(-1).ToString();
                xfmnth = Convert.ToDecimal(Convert.ToDateTime(xintfr).Subtract(Convert.ToDateTime(xintto)).Days / (365.25 / 12)) + 1;
                string[] arrdt = new string[Convert.ToInt32(xfmnth) + 1];
                if(xfmnth > 0)
                {
                    for(int i=1; i <=xfmnth; i++)
                    {

                    }
                }
                xtot_int = Convert.ToDecimal(CAL_GFTF_INT(Convert.ToDateTime(xintfr), Convert.ToDateTime(xintto), fdml, Convert.ToInt32(xfmnth), int_rate, Convert.ToDecimal(0), model));
            }
            return xtot_int;
        }
        public double CAL_GFTF_INT(DateTime xfrdt, DateTime XTODT, List<FUNDDEP_MAST> fdml, int xformonths, decimal XINT_RATE, decimal XMAX_MINBAL, MemberStatusViewModel model)
        {
            double open_bal = 0; double clos_bal = 0; double xtot = 0;
            double xr_bal = 0; double xbal = 0; int xyear = 0; int xmonth = 0;
            int xm = 0;  //decimal K=0;
            //int mm = 0;
            int tt = 0;

            for (int K = 1; K <= 12; K++)
            {
                model.int_array[2, K] = 0;
            }
            double CAL_GFTF_INT = 0;
            open_bal = 0;
            clos_bal = 0;
            xtot = 0;
            xr_bal = 0;
            xbal = 0;
            xm = 1;
            if (fdml.Count == 0)
            {
                return xbal;
            }
            else
            {
                var result = fdml.FindLast(delegate (FUNDDEP_MAST sbl)
                {
                    return sbl.vch_date < xfrdt;
                });
                if (result == null)
                {
                    open_bal = 0;
                }
                else
                {
                    open_bal = Convert.ToDouble(result.prin_bal);
                }
                xr_bal = open_bal;
                int XMONTH = Convert.ToDateTime(model.int_array[1, xm]).Month; // int.Parse(int_array[1, xm].ToString("MM"));
                int XYEAR = Convert.ToDateTime(model.int_array[1, xm]).Year; //int.Parse(int_array[1, xm].ToString("yyyy"));
                model.int_array[2, xm] = Convert.ToInt32(xr_bal);
                foreach (FUNDDEP_MAST tf in fdml)
                {
                    if (tf.vch_date > XTODT)
                        break;
                    if (tf.vch_date < xfrdt)
                    {
                        //do nothing
                    }
                    else
                    {
                        if (tf.vch_date.Month > XMONTH || tf.vch_date.Year > XYEAR)
                        {
                            if (tf.vch_date.Month == XMONTH && tf.vch_date.Year == XYEAR)
                            {

                            }
                            else
                            {
                                xm = xm + 1;
                                XMONTH = Convert.ToDateTime(model.int_array[1, xm]).Month;  //int.Parse(int_array[1, xm].ToString("MM"));
                                XYEAR = Convert.ToDateTime(model.int_array[1, xm]).Year; // int.Parse(int_array[1, xm].ToString("yyyy"));
                                model.int_array[2, xm] = Convert.ToInt32(xr_bal);
                            }
                        }
                        xbal = Convert.ToDouble(tf.prin_bal);
                        //String XDRCR = tf.dr_cr;
                        if(xbal < 0)
                        {
                            xbal = 0;
                        }
                        if (tf.vch_date.Day <= 10)
                            model.int_array[2, xm] = Convert.ToInt32(xbal);
                        else
                        {
                            if (xbal < model.int_array[2, xm])
                            {
                                model.int_array[2, xm] = Convert.ToInt32(xbal);
                            }
                        }
                        xr_bal = xbal;
                    }
                }
                if (xm < xformonths)
                {
                    for (int mm = xm + 1; mm <= xformonths; mm++)
                    {
                        model.int_array[2, mm] = xr_bal;
                    }
                }
            }
            if (model.int_array[2, xformonths] == 0)
            {
                xtot = 0;
            }
            else
            {
                for (int PP = 1; PP <= xformonths; PP++)
                {
                    xtot = xtot + model.int_array[2, PP];
                }
            }
            CAL_GFTF_INT = Convert.ToDouble(((xtot * Convert.ToDouble(XINT_RATE) / 1200) + 0.00000002));
            //  CAL_GFTF_INT = IIf(CAL_GFTF_INT < 1, 0, CAL_GFTF_INT);
            CAL_GFTF_INT = CAL_GFTF_INT < 1 ? 0 : CAL_GFTF_INT;
            CAL_GFTF_INT = Math.Round(CAL_GFTF_INT, 0);
            return CAL_GFTF_INT;
        }       
        public JsonResult getledgerbymemid(string BranchID, string member_no, string table, string emp_id, string ac_hd, string name)
        {
            Ledger ld = new Ledger();
            List<Ledger> ldl = new List<Ledger>();
            MemberStatusViewModel model = new MemberStatusViewModel();           
            ldl = ld.getledgerdetails(BranchID, member_no, table, emp_id, ac_hd);
            int i = 1;
            string certificate_date = "";           
            if (ldl.Count > 0)
            {
                foreach (var a in ldl)
                {
                    if(table == "GF_LEDGER" || table == "TF_LEDGER")
                    {                      
                        if(a.dr_cr== "C")
                        {
                            if(a.prin_amount > 0)
                            {
                                if (i == 1)
                                {
                                    model.tableelement = "<tr><th>Date</th><th>Particulars</th><th>Debit Amount</th><th>Credit Amount</th><th>Principal Balance</th><th>Interest Balance</th></tr>";
                                    model.tableelement = model.tableelement + "<tr><td>" + a.vch_date.ToString("dd-MM-yyyy").Replace("-", "/") + "</td><td>" + a.trns_particular + "</td><td>" + "" + "</td><td>" + a.prin_amount.ToString("0.00") + "</td><td>" + a.prin_bal.ToString("0.00") + "</td><td>" + a.int_bal.ToString("0.00") + "</td></tr>";
                                }
                                else
                                {
                                    model.tableelement = model.tableelement + "<tr><td>" + a.vch_date.ToString("dd-MM-yyyy").Replace("-", "/") + "</td><td>" + a.trns_particular + "</td><td>" + "" + "</td><td>" + a.prin_amount.ToString("0.00") + "</td><td>" + a.prin_bal.ToString("0.00") + "</td><td>" + a.int_bal.ToString("0.00") + "</td></tr>";
                                }
                                i = i + 1;
                            }
                            else
                            {
                                if (i == 1)
                                {
                                    model.tableelement = "<tr><th>Date</th><th>Particulars</th><th>Debit Amount</th><th>Credit Amount</th><th>Principal Balance</th><th>Interest Balance</th></tr>";
                                    model.tableelement = model.tableelement + "<tr><td>" + a.vch_date.ToString("dd-MM-yyyy").Replace("-", "/") + "</td><td>" + a.trns_particular + "</td><td>" + "" + "</td><td>" + a.int_amount.ToString("0.00") + "</td><td>" + a.prin_bal.ToString("0.00") + "</td><td>" + a.int_bal.ToString("0.00") + "</td></tr>";
                                }
                                else
                                {
                                    model.tableelement = model.tableelement + "<tr><td>" + a.vch_date.ToString("dd-MM-yyyy").Replace("-", "/") + "</td><td>" + a.trns_particular + "</td><td>" + "" + "</td><td>" + a.int_amount.ToString("0.00") + "</td><td>" + a.prin_bal.ToString("0.00") + "</td><td>" + a.int_bal.ToString("0.00") + "</td></tr>";
                                }
                                i = i + 1;
                            }
                        }
                        else
                        {
                            if (a.prin_amount > 0)
                            {
                                if (i == 1)
                                {
                                    model.tableelement = "<tr><th>Date</th><th>Particulars</th><th>Debit Amount</th><th>Credit Amount</th><th>Principal Balance</th><th>Interest Balance</th></tr>";
                                    model.tableelement = model.tableelement + "<tr><td>" + a.vch_date.ToString("dd-MM-yyyy").Replace("-", "/") + "</td><td>" + a.trns_particular + "</td><td>" + a.prin_amount.ToString("0.00") + "</td><td>" + "" + "</td><td>" + a.prin_bal.ToString("0.00") + "</td><td>" + a.int_bal.ToString("0.00") + "</td></tr>";
                                }
                                else
                                {
                                    model.tableelement = model.tableelement + "<tr><td>" + a.vch_date.ToString("dd-MM-yyyy").Replace("-", "/") + "</td><td>" + a.trns_particular + "</td><td>" + a.prin_amount.ToString("0.00") + "</td><td>" + "" + "</td><td>" + a.prin_bal.ToString("0.00") + "</td><td>" + a.int_bal.ToString("0.00") + "</td></tr>";
                                }
                                i = i + 1;
                            }
                            else
                            {
                                if (i == 1)
                                {
                                    model.tableelement = "<tr><th>Date</th><th>Particulars</th><th>Debit Amount</th><th>Credit Amount</th><th>Principal Balance</th><th>Interest Balance</th></tr>";
                                    model.tableelement = model.tableelement + "<tr><td>" + a.vch_date.ToString("dd-MM-yyyy").Replace("-", "/") + "</td><td>" + a.trns_particular + "</td><td>" + a.int_amount.ToString("0.00") + "</td><td>" + "" + "</td><td>" + a.prin_bal.ToString("0.00") + "</td><td>" + a.int_bal.ToString("0.00") + "</td></tr>";
                                }
                                else
                                {
                                    model.tableelement = model.tableelement + "<tr><td>" + a.vch_date.ToString("dd-MM-yyyy").Replace("-", "/") + "</td><td>" + a.trns_particular + "</td><td>" + a.int_amount.ToString("0.00") + "</td><td>" + "" + "</td><td>" + a.prin_bal.ToString("0.00") + "</td><td>" + a.int_bal.ToString("0.00") + "</td></tr>";
                                }
                                i = i + 1;
                            }
                        }                        
                    } 
                    else if(table == "DIVIDEND_LEDGER")
                    {
                        if(a.dr_cr == "C")
                        {
                            if (i == 1)
                            {
                                model.tableelement = "<tr><th>Date</th><th>Particulars</th><th>Debit Amount</th><th>Credit Amount</th><th>Balance</th></tr>";
                                model.tableelement = model.tableelement + "<tr><td>" + a.vch_date.ToString("dd-MM-yyyy").Replace("-", "/") + "</td><td>" + a.trns_particular + "</td><td>" + "" + "</td><td>" + a.prin_amount.ToString("0.00") + "</td><td>" + a.prin_bal.ToString("0.00") + "</td></tr>";
                            }
                            else
                            {
                                model.tableelement = model.tableelement + "<tr><td>" + a.vch_date.ToString("dd-MM-yyyy").Replace("-", "/") + "</td><td>" + a.trns_particular + "</td><td>" + "" + "</td><td>" + a.prin_amount.ToString("0.00") + "</td><td>" + a.prin_bal.ToString("0.00") + "</td></tr>";
                            }
                        }
                        else
                        {
                            if (i == 1)
                            {
                                model.tableelement = "<tr><th>Date</th><th>Particulars</th><th>Debit Amount</th><th>Credit Amount</th><th>Balance</th></tr>";
                                model.tableelement = model.tableelement + "<tr><td>" + a.vch_date.ToString("dd-MM-yyyy").Replace("-", "/") + "</td><td>" + a.trns_particular + "</td><td>" + a.prin_amount.ToString("0.00") + "</td><td>" + "" + "</td><td>" + a.prin_bal.ToString("0.00") + "</td></tr>";
                            }
                            else
                            {
                                model.tableelement = model.tableelement + "<tr><td>" + a.vch_date.ToString("dd-MM-yyyy").Replace("-", "/") + "</td><td>" + a.trns_particular + "</td><td>" + a.prin_amount.ToString("0.00") + "</td><td>" + "" + "</td><td>" + a.prin_bal.ToString("0.00") + "</td></tr>";
                            }
                        }                       
                        i = i + 1;                       
                    }
                    else if(ac_hd == "SHARE CAPITAL")
                    {
                        if (Convert.ToDateTime(a.certificate_date).ToString("dd-MM-yyyy").Replace("-", "/") == "01/01/0001")
                        {
                            certificate_date = "";
                        }
                        else
                        {
                            certificate_date = Convert.ToDateTime(a.certificate_date).ToString("dd-MM-yyyy").Replace("-", "/");
                        }
                        if (a.dr_cr == "C")
                        {
                            if (i == 1)
                            {
                                model.tableelement = "<tr><th>Date</th><th>Particulars</th><th>F/Value</th><th>Units</th><th>Debit Amount</th><th>Credit Amount</th><th>Balance Capital</th><th>Certificate No</th><th>Certificate Dt</th><th>Distinct No(From-To)</th></tr>";
                                model.tableelement = model.tableelement + "<tr><td>" + a.vch_date.ToString("dd/MM/yyyy").Replace("-", "/") + "</td><td>" + a.trns_particular + "</td><td>" + a.face_val.ToString("0.00") + "</td><td>" + a.units.ToString("0.00") + "</td><td>" + "" + "</td><td>" + a.prin_amount.ToString("0.00") + "</td><td>" + a.bal_amount.ToString("0.00") + "</td><td>" + a.certificate_no + "</td><td>" + certificate_date + "</td><td>" + a.dist_range + "</td></tr>";
                            }
                            else
                            {
                                model.tableelement = model.tableelement + "<tr><td>" + a.vch_date.ToString("dd/MM/yyyy").Replace("-", "/") + "</td><td>" + a.trns_particular + "</td><td>" + a.face_val.ToString("0.00") + "</td><td>" + a.units.ToString("0.00") + "</td><td>" + "" + "</td><td>" + a.prin_amount.ToString("0.00") + "</td><td>" + a.bal_amount.ToString("0.00") + "</td><td>" + a.certificate_no + "</td><td>" + certificate_date + "</td><td>" + a.dist_range + "</td></tr>";
                            }
                        }
                        else
                        {
                            if (i == 1)
                            {
                                model.tableelement = "<tr><th>Date</th><th>Particulars</th><th>F/Value</th><th>Units</th><th>Debit Amount</th><th>Credit Amount</th><th>Balance Capital</th><th>Certificate Dt</th><th>Dist Range</th></tr>";
                                model.tableelement = model.tableelement + "<tr><td>" + a.vch_date.ToString("dd/MM/yyyy").Replace("-", "/") + "</td><td>" + a.trns_particular + "</td><td>" + a.face_val.ToString("0.00") + "</td><td>" + a.units.ToString("0.00") + "</td><td>" + a.prin_amount.ToString("0.00") + "</td><td>" + "" + "</td><td>" + a.bal_amount.ToString("0.00") + "</td><td>" + a.certificate_no + "</td><td>" + certificate_date + "</td><td>" + a.dist_range + "</td></tr>";
                            }
                            else
                            {
                                model.tableelement = model.tableelement + "<tr><td>" + a.vch_date.ToString("dd/MM/yyyy").Replace("-", "/") + "</td><td>" + a.trns_particular + "</td><td>" + a.face_val.ToString("0.00") + "</td><td>" + a.units.ToString("0.00") + "</td><td>" + a.prin_amount.ToString("0.00") + "</td><td>" + "" + "</td><td>" + a.bal_amount.ToString("0.00") + "</td><td>" + a.certificate_no + "</td><td>" + certificate_date + "</td><td>" + a.dist_range + "</td></tr>";
                            }
                        }
                        i = i + 1;
                    }
                    else if(table == "LOAN_LEDGER")
                    {
                        if (a.dr_amt > 0)
                        {
                            if (i == 1)
                            {
                                model.tableelement = "<tr><th>Date</th><th>Transaction Particulars</th><th>Cheque No.</th><th>Dr Amount</th><th>Cr Amount</th><th>Prin/Balance</th><th>Int/Balance</th><th>Aint/Bal</th><th>Oth/Chrgs</th></tr>";
                                model.tableelement = model.tableelement + "<tr><td>" + a.vch_date.ToString("dd/MM/yyyy").Replace("-", "/") + "</td><td>" + a.trns_particular + "</td><td>" + a.chq_no + "</td><td>" + a.dr_amt.ToString("0.00") + "</td><td></td><td>" + a.prin_bal.ToString("0.00") + "</td><td>" + a.int_due.ToString("0.00") + "</td><td>" + a.aint_due.ToString("0.00") + "</td><td>" + a.ichrg_due.ToString("0.00") + "</td></tr>";
                            }
                            else
                            {
                                model.tableelement = model.tableelement + "<tr><td>" + a.vch_date.ToString("dd/MM/yyyy").Replace("-", "/") + "</td><td>" + a.trns_particular + "</td><td>" + a.chq_no + "</td><td>" + a.dr_amt.ToString("0.00") + "</td><td></td><td>" + a.prin_bal.ToString("0.00") + "</td><td>" + a.int_due.ToString("0.00") + "</td><td>" + a.aint_due.ToString("0.00") + "</td><td>" + a.ichrg_due.ToString("0.00") + "</td></tr>";
                            }
                        }
                        if (a.cr_amt > 0)
                        {
                            if (i == 1)
                            {
                                model.tableelement = "<tr><th>Date</th><th>Transaction Particulars</th><th>Cheque No.</th><th>Dr Amount</th><th>Cr Amount</th><th>Prin/Balance</th><th>Int/Balance</th><th>Aint/Bal</th><th>Oth/Chrgs</th></tr>";
                                model.tableelement = model.tableelement + "<tr><td>" + a.vch_date.ToString("dd/MM/yyyy").Replace("-", "/") + "</td><td>" + a.trns_particular + "</td><td>" + a.chq_no + "</td><td></td><td>" + a.cr_amt.ToString("0.00") + "</td><td>" + a.prin_bal.ToString("0.00") + "</td><td>" + a.int_due.ToString("0.00") + "</td><td>" + a.aint_due.ToString("0.00") + "</td><td>" + a.ichrg_due.ToString("0.00") + "</td></tr>";
                            }
                            else
                            {
                                model.tableelement = model.tableelement + "<tr><td>" + a.vch_date.ToString("dd/MM/yyyy").Replace("-", "/") + "</td><td>" + a.trns_particular + "</td><td>" + a.chq_no + "</td><td></td><td>" + a.cr_amt.ToString("0.00") + "</td><td>" + a.prin_bal.ToString("0.00") + "</td><td>" + a.int_due.ToString("0.00") + "</td><td>" + a.aint_due.ToString("0.00") + "</td><td>" + a.ichrg_due.ToString("0.00") + "</td></tr>";
                            }
                        }
                        i = i + 1;
                    }
                }
                model.Branch = BranchID;
                model.mem_no = member_no;
                model.mem_name = name;
                model.ln_type = ac_hd;
                model.loanee_emp_id = emp_id;
            }
            else
            {
                model.tableelement = null;
            }
            return Json(model);
        }

        //public JsonResult GetMemberLoanInfoByMemberId(MemberStatusViewModel model)
        //{
        //    Loan_Master lm = new Loan_Master();
        //    List<Loan_Master> lml = new List<Loan_Master>();
        //    lml = lm.GetLoanInfo(model.BranchID, model.member_no);
        //    return Json(model);
        //}

        /********************************************Member Status End*******************************************/

        /********************************************Share Ledger Statement Start*******************************************/
        [HttpGet]
        public ActionResult ShareLedgerStatement(ShareLedgerStatementViewModel model)
        {
            UtilityController u = new UtilityController();
            model.BranchDesc = u.getBranchMastDetails();
            model.TypeDesc = u.getTypeMastDetails();
            model.CastDesc = u.getCastMastDetails();
            model.ReligionDesc = u.getReligionMastDetails();
            model.DepartmentDesc = u.getDepartmentMastDetails();
            model.ServiceDesc = u.getServiceStatusMastDetails();
            model.EmpDesc = u.getEmployerMastDetails();
            model.EmpBranchDesc = u.getEmployerBranchMastDetails();
            model.CategoryDesc = u.getCategoryMastDetails();
            return View(model);
        }
        public JsonResult GetMemberDetailsbyBranchAndMemberId(ShareLedgerStatementViewModel model)
        {
            Member_Mast mm = new Member_Mast();
            List<Member_Mast> mml = new List<Member_Mast>();
            mml = mm.getmemberdetailsbymemid(model.branch_id, model.mem_no);
            int i = 1;
            string sig = "";
            string lti = "";
            if (mml.Count > 0)
            {
                foreach (var a in mml)
                {
                    Designation_Mast dm = new Designation_Mast();
                    string desig = dm.getdesignationname(a.desig);
                    if (a.ltl_app == 0)
                    {
                        sig = "YES";
                        lti = "NO";
                    }
                    else
                    {
                        sig = "NO";
                        lti = "YES";
                    }
                    if (i == 1)
                    {
                        model.tableelement = "<tr><th>Srl</th><th>Employee Id</th><th>Name Of A/C Holder</th><th>Designation</th><th>Sign</th><th>LTI</th><th>Pan No</th></tr>";
                        model.tableelement = model.tableelement + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.emp_id + "</td><td>" + a.mem_name + "</td><td>" + desig + "</td><td>" + sig + "</td><td>" + lti + "</td><td>" + a.pan + "</td></tr>";
                    }
                    else
                    {
                        model.tableelement = model.tableelement + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.emp_id + "</td><td>" + a.mem_name + "</td><td>" + desig + "</td><td>" + sig + "</td><td>" + lti + "</td><td>" + a.pan + "</td></tr>";
                    }
                    i = i + 1;
                    model.hold_no = a.mailAdd_house;
                    model.mailAdd_add1 = a.mailAdd_add1;
                    model.mailAdd_add2 = a.mailAdd_add2;
                    model.mailAdd_city = a.mailAdd_city;
                    model.mailAdd_state = a.mailAdd_state;
                    model.mailAdd_dist = a.mailAdd_dist;
                    model.mailAdd_pin = a.mailAdd_pin;
                    model.unit = a.emp_branch;
                    if (a.sex == "M")
                    {
                        model.sex = "MALE";
                    }
                    else
                    {
                        model.sex = "FEMALE";
                    }
                    model.mem_dt = a.mem_date.ToString("dd-MM-yyyy").Replace("-", "/");
                    model.cat = a.member_category;
                    model.caste = a.caste;
                    model.orgn = a.emp_cd;
                    model.dept = a.dept;
                    model.serv_sts = a.serv_sts;
                    model.mem_type = a.member_type;
                    model.mem_name = a.mem_name;
                    model.relgn = a.relgn;
                }
            }
            else
            {
                model.tableelement = null;
            }
            return Json(model);
        }
        public JsonResult getshareledgerdetailsbymemberid(ShareLedgerStatementViewModel model)
        {
            SHARE_LEDGER sl = new SHARE_LEDGER();
            List<SHARE_LEDGER> shl = new List<SHARE_LEDGER>();
            shl = sl.getdetailsbymemid(model.branch_id, model.mem_no);
            int i = 1;
            string sig = "";
            string lti = "";
            string certificate_date = "";
            if (shl.Count > 0)
            {
                foreach (var a in shl)
                {
                    if (Convert.ToDateTime(a.certificate_date).ToString("dd-MM-yyyy").Replace("-", "/") == "01/01/0001")
                    {
                        certificate_date = "";
                    }
                    else
                    {
                        certificate_date = Convert.ToDateTime(a.certificate_date).ToString("dd-MM-yyyy").Replace("-", "/");
                    }
                    if (a.dr_cr == "C")
                    {
                        if (i == 1)
                        {
                            model.tableelement = "<tr><th>Date</th><th>Particulars</th><th>F/Value</th><th>Units</th><th>Debit Amount</th><th>Credit Amount</th><th>Balance Capital</th><th>Certificate No</th><th>Certificate Dt</th><th>Distinct No(From-To)</th></tr>";
                            model.tableelement = model.tableelement + "<tr><td>" + a.vch_date.ToString("dd/MM/yyyy").Replace("-", "/") + "</td><td>" + a.particular + "</td><td>" + a.face_val.ToString("0.00") + "</td><td>" + a.units.ToString("0.00") + "</td><td>" + "" + "</td><td>" + a.tr_amount.ToString("0.00") + "</td><td>" + a.bal_amount.ToString("0.00") + "</td><td>" + a.certificate_no + "</td><td>" + certificate_date + "</td><td>" + a.dist_range + "</td></tr>";
                        }
                        else
                        {
                            model.tableelement = model.tableelement + "<tr><td>" + a.vch_date.ToString("dd/MM/yyyy").Replace("-", "/") + "</td><td>" + a.particular + "</td><td>" + a.face_val.ToString("0.00") + "</td><td>" + a.units.ToString("0.00") + "</td><td>" + "" + "</td><td>" + a.tr_amount.ToString("0.00") + "</td><td>" + a.bal_amount.ToString("0.00") + "</td><td>" + a.certificate_no + "</td><td>" + certificate_date + "</td><td>" + a.dist_range + "</td></tr>";
                        }
                    }
                    else
                    {
                        if (i == 1)
                        {
                            model.tableelement = "<tr><th>Date</th><th>Particulars</th><th>F/Value</th><th>Units</th><th>Debit Amount</th><th>Credit Amount</th><th>Balance Capital</th><th>Certificate Dt</th><th>Dist Range</th></tr>";
                            model.tableelement = model.tableelement + "<tr><td>" + a.vch_date.ToString("dd/MM/yyyy").Replace("-", "/") + "</td><td>" + a.particular + "</td><td>" + a.face_val.ToString("0.00") + "</td><td>" + a.units.ToString("0.00") + "</td><td>" + a.tr_amount.ToString("0.00") + "</td><td>" + "" + "</td><td>" + a.bal_amount.ToString("0.00") + "</td><td>" + a.certificate_no + "</td><td>" + certificate_date + "</td><td>" + a.dist_range + "</td></tr>";
                        }
                        else
                        {
                            model.tableelement = model.tableelement + "<tr><td>" + a.vch_date.ToString("dd/MM/yyyy").Replace("-", "/") + "</td><td>" + a.particular + "</td><td>" + a.face_val.ToString("0.00") + "</td><td>" + a.units.ToString("0.00") + "</td><td>" + a.tr_amount.ToString("0.00") + "</td><td>" + "" + "</td><td>" + a.bal_amount.ToString("0.00") + "</td><td>" + a.certificate_no + "</td><td>" + certificate_date + "</td><td>" + a.dist_range + "</td></tr>";
                        }
                    }
                    i = i + 1;
                }
            }
            else
            {
                model.tableelement = null;
            }
            return Json(model);
        }
        public JsonResult getshareledgerlistsbymemberid(ShareLedgerStatementViewModel model)
        {
            SHARE_LEDGER sl = new SHARE_LEDGER();
            List<SHARE_LEDGER> shl = new List<SHARE_LEDGER>();
            shl = sl.getledgerlistsbymemid(model.branch_id, model.mem_no);
            int i = 1;
            decimal xtot = 0;
            if (shl.Count > 0)
            {
                foreach (var a in shl)
                {
                    if (i == 1)
                    {
                        model.tableelement = "<tr><th>Share Acount Head</th><th>F/value</th><th>Units(Debit)</th><th>Debit Amount</th><th>Units(Credit)</th><th>Credit Amount</th><th>Balance Unit</th><th>Share Capital</th></tr>";
                        model.tableelement = model.tableelement + "<tr><td>" + a.acc_desc + "</td><td>" + a.MISCDEP_BASEAMT.ToString("0.00") + "</td><td>" + a.TOT_UNITDR.ToString("0.00") + "</td><td>" + a.TOT_AMTDR.ToString("0.00") + "</td><td>" + a.TOT_UNITCR.ToString("0.00") + "</td><td>" + a.TOT_AMTCR.ToString("0.00") + "</td><td>" + a.balance_unit.ToString("0.00") + "</td><td>" + a.share_capital.ToString("0.00") + "</td></tr>";
                        xtot = xtot + a.TOT_AMTCR - a.TOT_AMTDR;
                    }
                    else
                    {
                        model.tableelement = model.tableelement + "<tr><td>" + a.acc_desc + "</td><td>" + a.MISCDEP_BASEAMT.ToString("0.00") + "</td><td>" + a.TOT_UNITDR.ToString("0.00") + "</td><td>" + a.TOT_AMTDR.ToString("0.00") + "</td><td>" + a.TOT_UNITCR.ToString("0.00") + "</td><td>" + a.TOT_AMTCR.ToString("0.00") + "</td><td>" + a.balance_unit.ToString("0.00") + "</td><td>" + a.share_capital.ToString("0.00") + "</td></tr>";
                        xtot = xtot + a.TOT_AMTCR - a.TOT_AMTDR;
                    }
                    i = i + 1;
                }
                model.ex_total_sh_cap = xtot.ToString("0.00");
            }
            else
            {
                model.tableelement = null;
            }
            return Json(model);
        }
        public ActionResult ShareLedgerDetailsPrintfile(ShareLedgerStatementViewModel model)
        {
            decimal face_val = 0;
            decimal units = 0;
            decimal dr_amt = 0;
            decimal cr_amt = 0;
            decimal balance_amt = 0;
            string certificate_date = "";
            Licence lc = new Licence();
            lc = lc.getlicencedetails();
            Member_Mast mm = new Member_Mast();
            mm = mm.getdetails(model.branch_id, model.mem_no);
            SHARE_LEDGER sl = new SHARE_LEDGER();
            List<SHARE_LEDGER> shl = new List<SHARE_LEDGER>();
            shl = sl.getdetailsbymemid(model.branch_id, model.mem_no);
            Directory.CreateDirectory(Server.MapPath("~/wwwroot\\TextFiles"));
            using (StreamWriter sw = new StreamWriter(Server.MapPath("~/wwwroot\\TextFiles\\Share_Ledger_Details_List.txt")))
            {
                int Pg = 1;
                int Ln = 0;
                int i = 1;
                sw.WriteLine("                                                                Run Date: " + DateTime.Now.ToString("dd-MM-yyyy").Replace("-", "/") + "   Page# " + Pg);
                sw.WriteLine("                       " + lc.lic_name);
                sw.WriteLine("                               " + lc.lic_add1 + "," + lc.lic_add2 + " PHONE : " + lc.lic_phone);
                sw.WriteLine("");
                sw.WriteLine("Share Ledger Statement ");
                sw.WriteLine("----------------------------------------------------------------+---------------------------------------");
                sw.WriteLine("Member Id. : " + model.mem_no + " | Member Date     : " + model.mem_dt);
                sw.WriteLine("Name       : " + model.mem_name + " | Member Categoty : " + model.cat);
                sw.WriteLine("Address    : " + model.mailAdd_add1 + " | Member Type     : " + model.mem_type);
                sw.WriteLine("             " + model.mailAdd_add2 + " " + model.mailAdd_city + " " + model.mailAdd_dist + " " + model.mailAdd_state + " " + model.mailAdd_pin);
                sw.WriteLine("Employee Id: " + mm.emp_id + "   Organization : " + model.orgn + "  Unit : " + model.unit);
                sw.WriteLine("-------+---------------------------+----------+------+----------+----------+---------+---------+---------");
                sw.WriteLine("Date   |TransactionParticulars     |Face Value|Units |Dr. Amount|Cr. Amount| Balance |Cert. No |Issued On");
                sw.WriteLine("-------+---------------------------+----------+------+----------+----------+---------+---------+---------");
                foreach (var am in shl)
                {
                    if (Convert.ToDateTime(am.certificate_date).ToString("dd-MM-yyyy").Replace("-", "/") == "01/01/0001")
                    {
                        certificate_date = "";
                    }
                    else
                    {
                        certificate_date = Convert.ToDateTime(am.certificate_date).ToString("dd-MM-yyyy").Replace("-", "/");
                    }
                    if (am.face_val.ToString().Length > 10)
                    {
                        face_val = Convert.ToDecimal(Convert.ToString(am.face_val).Substring(0, 9));
                    }
                    else
                    {
                        face_val = am.face_val;
                    }
                    if (am.units.ToString().Length > 6)
                    {
                        units = Convert.ToDecimal(Convert.ToString(am.units).Substring(0, 5));
                    }
                    else
                    {
                        units = am.units;
                    }
                    if (am.tr_amount.ToString().Length > 10)
                    {
                        dr_amt = Convert.ToDecimal(Convert.ToString(am.tr_amount).Substring(0, 9));
                    }
                    else
                    {
                        dr_amt = am.tr_amount;
                    }
                    if (am.tr_amount.ToString().Length > 10)
                    {
                        cr_amt = Convert.ToDecimal(Convert.ToString(am.tr_amount).Substring(0, 9));
                    }
                    else
                    {
                        cr_amt = am.tr_amount;
                    }
                    if (am.bal_amount.ToString().Length > 10)
                    {
                        balance_amt = Convert.ToDecimal(Convert.ToString(am.bal_amount).Substring(0, 9));
                    }
                    else
                    {
                        balance_amt = am.bal_amount;
                    }
                    if (Ln > Pg * 65)
                    {
                        Pg = Pg + 1;
                        Ln = Ln + 7;
                        sw.WriteLine("                                                                Run Date: " + DateTime.Now.ToString("dd-MM-yyyy").Replace("-", "/") + "   Page# " + Pg);
                        sw.WriteLine("                       " + lc.lic_name);
                        sw.WriteLine("                               " + lc.lic_add1 + "," + lc.lic_add2 + " PHONE : " + lc.lic_phone);
                        sw.WriteLine("");
                        sw.WriteLine("Share Ledger Statement ");
                        sw.WriteLine("----------------------------------------------------------------+---------------------------------------");
                        sw.WriteLine("Member Id. : " + model.mem_no + " | Member Date     : " + model.mem_dt);
                        sw.WriteLine("Name       : " + model.mem_name + " | Member Categoty : " + model.cat);
                        sw.WriteLine("Address    : " + model.mailAdd_add1 + " | Member Type     : " + model.mem_type);
                        sw.WriteLine("             " + model.mailAdd_add2 + " " + model.mailAdd_city + " " + model.mailAdd_dist + " " + model.mailAdd_state + " " + model.mailAdd_pin);
                        sw.WriteLine("Employee Id: " + mm.emp_id + "   Organization : " + model.orgn + "  Unit : " + model.unit);
                        sw.WriteLine("-------+---------------------------+----------+------+----------+----------+---------+---------+---------");
                        sw.WriteLine("Date   |TransactionParticulars     |Face Value|Units |Dr. Amount|Cr. Amount| Balance |Cert. No |Issued On");
                        sw.WriteLine("-------+---------------------------+----------+------+----------+----------+---------+---------+---------");
                    }
                    if (am.dr_cr == "C")
                    {
                        sw.WriteLine("".ToString().PadLeft(10 - (am.vch_date).ToString("dd-MM-yyyy").Replace("-", "/").Length) + am.vch_date.ToString("dd-MM-yyyy").Replace("-", "/") + "|"
                          + "".ToString().PadLeft(27 - (am.particular).ToString().Length) + am.particular + "|"
                          + "".ToString().PadLeft(10 - (face_val).ToString().Length) + face_val.ToString("0.00") + "|"
                           + "".ToString().PadLeft(6 - (units).ToString().Length) + units.ToString("0.00") + "|"
                            + "".ToString().PadLeft(10 - ("").ToString().Length) + "".ToString() + "|"
                             + "".ToString().PadLeft(10 - (cr_amt).ToString().Length) + cr_amt.ToString("0.00") + "|"
                              + "".ToString().PadLeft(10 - (balance_amt).ToString().Length) + balance_amt.ToString("0.00") + "|"
                              + "".ToString().PadLeft(9 - (am.certificate_no).ToString().Length) + am.certificate_no.ToString() + "|"
                               + "".ToString().PadLeft(10 - (certificate_date).ToString().Length) + certificate_date + "|");
                    }
                    else
                    {
                        sw.WriteLine("".ToString().PadLeft(10 - (am.vch_date).ToString("dd-MM-yyyy").Replace("-", "/").Length) + am.vch_date.ToString("dd-MM-yyyy").Replace("-", "/") + "|"
                          + "".ToString().PadLeft(27 - (am.particular).ToString().Length) + am.particular + "|"
                          + "".ToString().PadLeft(10 - (face_val).ToString().Length) + face_val.ToString("0.00") + "|"
                           + "".ToString().PadLeft(6 - (units).ToString().Length) + units.ToString("0.00") + "|"
                            + "".ToString().PadLeft(10 - (dr_amt).ToString().Length) + dr_amt.ToString("0.00") + "|"
                             + "".ToString().PadLeft(10 - ("").ToString().Length) + "".ToString() + "|"
                              + "".ToString().PadLeft(10 - (balance_amt).ToString().Length) + balance_amt.ToString("0.00") + "|"
                              + "".ToString().PadLeft(9 - (am.certificate_no).ToString().Length) + am.certificate_no.ToString() + "|"
                              + "".ToString().PadLeft(10 - (certificate_date).ToString().Length) + certificate_date + "|");
                    }
                    Ln = Ln + 1;
                    i = i + 1;
                }
            }
            UtilityController u = new UtilityController();
            var memory = u.DownloadTextFiles("Share_Ledger_Details_List.txt", Server.MapPath("~/wwwroot\\TextFiles"));
            if (System.IO.File.Exists(Server.MapPath("~/wwwroot\\TextFiles\\Share_Ledger_Details_List.txt")))
            {
                System.IO.File.Delete(Server.MapPath("~/wwwroot\\TextFiles\\Share_Ledger_Details_List.txt"));
            }
            return File(memory.ToArray(), "text/plain", "Share_Ledger_Details_List_" + DateTime.Now.ToShortDateString().Replace("/", "_") + ".txt");
        }

        //public void getshareledgerdetailsprintfile(ShareLedgerStatementViewModel model)
        //{
        //    decimal face_val = 0;
        //    decimal units = 0;
        //    decimal dr_amt = 0;
        //    decimal cr_amt = 0;
        //    decimal balance_amt = 0;
        //    string certificate_date = "";
        //    Licence lc = new Licence();
        //    lc = lc.getlicencedetails();
        //    Member_Mast mm = new Member_Mast();
        //    mm = mm.getdetails(model.branch_id, model.mem_no);
        //    SHARE_LEDGER sl = new SHARE_LEDGER();
        //    List<SHARE_LEDGER> shl = new List<SHARE_LEDGER>();
        //    shl = sl.getdetailsbymemid(model.branch_id, model.mem_no);
        //    PrintDocument pd = new PrintDocument();
        //    Font font = new Font("Courier New", 15);
        //    Printer printer = new Printer("EPSON PLQ-35 ESC/P2", "UTF-8");
        //    printer.Append(" ");
        //    printer.AlignCenter();
        //    printer.Append("          " + "Branch       : MAIN(CHITTARANJAN)");
        //    printer.AlignLeft();
        //    printer.Append("  ");
        //    printer.Append("          " + "A/C No.      : " + mm.cmtdno.ToString());
        //    printer.Append("  ");
        //    printer.Append("          " + "A/C Name.    : " + mm.member_name.ToString().PadRight(30));
        //    printer.Append("  ");
        //    printer.Append("          " + "SR No.       : " + mm.member_id.ToString().PadRight(30));
        //    printer.Append("  ");
        //    printer.Append("          " + "Oprn.Mode.   : ");
        //    printer.Append("          " + "Address      : " + mm.mail_add1.ToString().PadRight(55));
        //    printer.Append("          " + "             : " + mm.mail_add2.ToString().PadRight(55));
        //    printer.Append("          " + "             : " + wadd_oth.ToString().PadRight(55));
        //    printer.Append(" ");
        //    printer.Append(" ");
        //    printer.Append(" ");
        //    printer.PrintDocument();
        //    printer.Clear();

        //}

        /********************************************Share Ledger Statement End*******************************************/

        /********************************************Member's deposit Fund Interest Payable Schedule Start********************/
        [HttpGet]
        public ActionResult MemDepositeFundIntPaySch(MemDepositeFundIntPaySchViewModel model)
        {
            UtilityController u = new UtilityController();
            model.BranchDesc = u.getBranchMastDetails();
            model.achddesc = u.getGlAcchd();
            model.EmpBranchDesc = u.getEmployerBranchMastDetails();
            model.post_dt = DateTime.Now.ToString("dd/MM/yyyy").Replace("-", "/");
            model.fr_dt = "01/04/" + Convert.ToString(DateTime.Now.Year - 1);
            model.to_dt = "31/03/" + Convert.ToString(DateTime.Now.Year);
            return View(model);
        }
        public JsonResult getintrateByachd(MemDepositeFundIntPaySchViewModel model)
        {
            FUNDDEP_MAST fm = new FUNDDEP_MAST();
            fm = fm.getIntrateByachd(model.ac_hd);

            model.int_rate = (fm.int_rate).ToString("0.00");
            if (model.ac_hd == "TF")
            {
                model.heading = "P A R T I C U L A R S  O F  T H R I F T  F U N D  I N T E R E S T  P A Y A B L E";
            }
            if (model.ac_hd == "GF")
            {
                model.heading = "P A R T I C U L A R S  O F  G U A R A N T E E  F U N D  I N T E R E S T  P A Y A B L E";
            }
            return Json(model);
        }
        public JsonResult getLIstForintrestpost(MemDepositeFundIntPaySchViewModel model)
        {
            Member_Mast mm = new Member_Mast();
            initGrid(model);
            PopulateGrid(model);
            return Json(model);
        }
        public void initGrid(MemDepositeFundIntPaySchViewModel model)
        {
            int xmonths, cols;
            String xf_str, xstr;
            DateTime xl_dt;
            DateTime xfrm_dt = Convert.ToDateTime(model.fr_dt);
            //model.fromDate = Convert.ToDateTime(model.FrmDate);
            Member_Mast mm = new Member_Mast();
            List<Member_Mast> mmlst = new List<Member_Mast>();
            mmlst = mm.GetmemMastForDemandInterestCalculation(model);
            if (mmlst == null)
            {
                //alert message
                //return
            }
            else
            {
                model.mdmmlst = mmlst;
            }
            xmonths = Convert.ToInt32(Convert.ToDateTime(model.to_dt).Subtract(Convert.ToDateTime(model.fr_dt)).Days / (365.25 / 12));
            var result = Convert.ToDateTime(model.fr_dt).CompareTo(Convert.ToDateTime(model.to_dt));
            model.te = "<tr><th>Srl.</th><th>Member No</th><th>Mem.Date</th><th>Name</th><th>Op / Principal</th><th>Op / Interest</th>";
            for (int i = 1; i <= xmonths; i++)
            {
                model.int_array[1, i] = xfrm_dt;
                xstr = xfrm_dt.ToString("MMM, yyyy");
                model.month_array[i - 1] = xstr;
                model.te = model.te + "<th>" + xstr + "</th>";
                xfrm_dt = xfrm_dt.AddMonths(1);
            }
            model.te = model.te + " <th>Cl/Principal</th><th>Cl/Interest</th><th>Int.Payable</th><th>Cl / Int(Incl.Payb.)</th></tr>";
            model.xmonths = xmonths;
        }
        public void PopulateGrid(MemDepositeFundIntPaySchViewModel model)
        {
            // utility utl = new utility();
            decimal open_prin = 0;
            Double xtot_int = 0;
            decimal open_int;
            decimal clos_prin = 0;
            decimal clos_int = 0;
            decimal xtot_opn_prin = 0;
            decimal xtot_opn_int = 0;
            decimal xtot_cls_prin = 0;
            decimal xtot_cls_int = 0;
            decimal xtot_int_amt = 0;
            int i = 1;
            foreach (Member_Mast mm in model.mdmmlst)
            {
                string xac = mm.mem_id;
                string xacnm = mm.mem_name;
                open_prin = 0;
                open_int = 0;
                clos_prin = 0;
                clos_int = 0;
                TF_Ledger tf = new TF_Ledger();
                List<TF_Ledger> tflst = new List<TF_Ledger>();
                tflst = tf.getgetGFandTFDetail(model.ac_hd, model.branch, model.fr_dt, xac);
                open_prin = 0;
                open_int = 0;
                if (tflst.Count > 0)
                {
                    //var result = tflst.LastOrDefault().vch_date;
                    //if (result < Convert.ToDateTime(model.fr_dt))
                    //{

                    //    if (Convert.ToDecimal(tflst.LastOrDefault().prin_bal) + Convert.ToDecimal(tflst.LastOrDefault().int_bal) > 0)
                    //    {
                    //        open_prin = Convert.ToDecimal(tflst.LastOrDefault().prin_bal);
                    //    }
                    //}
                    var result = tflst.FindLast(delegate (TF_Ledger sbl)
                    {
                        return sbl.vch_date < Convert.ToDateTime(model.fr_dt);
                    });
                    if (result == null)
                    {
                        open_prin = 0;
                    }
                    else
                    {

                        open_prin = Convert.ToDecimal(result.prin_bal);
                    }
                    xtot_opn_prin = xtot_opn_prin + open_prin;
                    xtot_opn_int = xtot_opn_int + open_int;
                    clos_prin = 0;
                    clos_int = 0;
                    //var result2 = tflst.LastOrDefault().vch_date;

                    //if (Convert.ToDateTime(result2)<= Convert.ToDateTime(model.to_dt))
                    //{
                    //    if (Convert.ToDecimal(tflst.LastOrDefault().prin_bal) + Convert.ToDecimal(tflst.LastOrDefault().int_bal) > 0)
                    //    {
                    //        clos_prin = Convert.ToDecimal(tflst.LastOrDefault().prin_bal);
                    //        clos_int = Convert.ToDecimal(tflst.LastOrDefault().int_bal);
                    //        clos_int = 0;

                    //    }
                    //}
                    var result2 = tflst.FindLast(delegate (TF_Ledger sbl)
                    {
                        return sbl.vch_date <= Convert.ToDateTime(model.to_dt);
                    });
                    if (result2 == null)
                    {
                        clos_prin = 0;
                    }
                    else
                    {
                        if (Convert.ToDecimal(result2.prin_bal) + Convert.ToDecimal(result2.int_bal) > 0)
                        {
                            clos_prin = Convert.ToDecimal(result2.prin_bal);
                            clos_int = Convert.ToDecimal(result2.int_bal);
                            clos_int = 0;
                        }
                    }
                    xtot_cls_prin = xtot_cls_prin + clos_prin;
                    xtot_cls_int = xtot_cls_int + clos_int;
                }
                xtot_int = CAL_GFTF_INT(Convert.ToDateTime(model.fr_dt), Convert.ToDateTime(model.to_dt), tflst, model.xmonths, Convert.ToDecimal(model.int_rate), Convert.ToDecimal(0), model);
                xtot_int = Math.Truncate(xtot_int);
                model.te = model.te + "<tr><td>" + i + "</td><td>" + xac + "</td><td>" + mm.mem_date + "</td><td>" + xacnm + "</td><td>" + open_prin + "</td><td>" + open_int + "</td>";
                for (int c = 1; c <= model.xmonths; c++)
                {
                    model.te = model.te + "<td>" + model.int_array[2, c] + "</td>";
                }
                model.te = model.te + "<td>" + clos_prin + "</td><td>" + clos_int + "</td><td>" + xtot_int + "</td><td>" + (clos_int + Convert.ToDecimal(xtot_int)) + "</td></tr>";
                xtot_int_amt = xtot_int_amt + Convert.ToDecimal(xtot_int);
                model.op_prn_bal = xtot_opn_prin.ToString("0.00");
                model.op_int_pay = xtot_opn_int.ToString("0.00");
                model.op_tot_bal = (xtot_opn_prin + xtot_opn_int).ToString("0.00");
                model.close_prn_bal = xtot_cls_prin.ToString("0.00");
                model.close_int_pay = xtot_cls_int.ToString("0.00");
                model.close_tot_bal = (xtot_cls_prin + xtot_cls_int).ToString("0.00");
                model.add_int_pay = xtot_int_amt.ToString("0.00");
                model.net_int_pay = (xtot_cls_int + xtot_int_amt).ToString("0.00");
                model.net_tot_bal = (xtot_cls_prin + xtot_cls_int + xtot_int_amt).ToString("0.00");

                if (model.forsave == "Savedata")
                {
                    tf.SaveInLedger(mm, model, clos_prin, xtot_int);
                }
                i++;
            }
        }
        public double CAL_GFTF_INT(DateTime xfrdt, DateTime XTODT, List<TF_Ledger> tflst, int xformonths, decimal XINT_RATE, decimal XMAX_MINBAL, MemDepositeFundIntPaySchViewModel model)
        {
            double open_bal = 0; double clos_bal = 0; double xtot = 0;
            double xr_bal = 0; double xbal = 0; int xyear = 0; int xmonth = 0;
            int xm = 0;  //decimal K=0;
            //int mm = 0;
            int tt = 0;

            for (int K = 1; K <= 12; K++)
            {
                model.int_array[2, K] = 0;
            }
            double CAL_GFTF_INT = 0;
            open_bal = 0;
            clos_bal = 0;
            xtot = 0;
            xr_bal = 0;
            xbal = 0;
            xm = 1;
            if (tflst.Count == 0)
            {
                return xbal;
            }
            else
            {
                var result = tflst.FindLast(delegate (TF_Ledger sbl)
                {
                    return sbl.vch_date < xfrdt;
                });
                if (result == null)
                {
                    open_bal = 0;
                }
                else
                {
                    open_bal = Convert.ToDouble(result.prin_bal);
                }
                xr_bal = open_bal;
                int XMONTH = Convert.ToDateTime(model.int_array[1, xm]).Month; // int.Parse(int_array[1, xm].ToString("MM"));
                int XYEAR = Convert.ToDateTime(model.int_array[1, xm]).Year; //int.Parse(int_array[1, xm].ToString("yyyy"));
                model.int_array[2, xm] = Convert.ToInt32(xr_bal);
                foreach (TF_Ledger tf in tflst)
                {
                    if (tf.vch_date > XTODT)
                        break;
                    if (tf.vch_date < xfrdt)
                    {
                        //do nothing
                    }
                    else
                    {
                        if (tf.vch_date.Month > XMONTH || tf.vch_date.Year > XYEAR)
                        {
                            if (tf.vch_date.Month == XMONTH && tf.vch_date.Year == XYEAR)
                            {

                            }
                            else
                            {
                                xm = xm + 1;
                                XMONTH = Convert.ToDateTime(model.int_array[1, xm]).Month;  //int.Parse(int_array[1, xm].ToString("MM"));
                                XYEAR = Convert.ToDateTime(model.int_array[1, xm]).Year; // int.Parse(int_array[1, xm].ToString("yyyy"));
                                model.int_array[2, xm] = Convert.ToInt32(xr_bal);
                            }
                        }
                        xbal = Convert.ToDouble(tf.prin_bal);
                        String XDRCR = tf.dr_cr;
                        if (tf.vch_date.Day <= 10)
                            model.int_array[2, xm] = Convert.ToInt32(xbal);
                        else
                        {
                            if (xbal < model.int_array[2, xm])
                            {
                                model.int_array[2, xm] = Convert.ToInt32(xbal);
                            }
                        }
                        xr_bal = xbal;
                    }
                }
                if (xm < xformonths)
                {
                    for (int mm = xm + 1; mm <= xformonths; mm++)
                    {
                        model.int_array[2, mm] = xr_bal;
                    }
                }
            }
            if (model.int_array[2, xformonths] == 0)
            {
                xtot = 0;
            }
            else
            {
                for (int PP = 1; PP <= xformonths; PP++)
                {
                    xtot = xtot + model.int_array[2, PP];
                }
            }
            CAL_GFTF_INT = Convert.ToDouble(((xtot * Convert.ToDouble(XINT_RATE) / 1200) + 0.00000002));
            //  CAL_GFTF_INT = IIf(CAL_GFTF_INT < 1, 0, CAL_GFTF_INT);
            CAL_GFTF_INT = CAL_GFTF_INT < 1 ? 0 : CAL_GFTF_INT;
            CAL_GFTF_INT = Math.Round(CAL_GFTF_INT, 0);
            return CAL_GFTF_INT;
        }
        /********************************************Member's deposit Fund Interest Payable Schedule End********************/

        /********************************************Share Capital Details List Start*******************************************/
        [HttpGet]
        public ActionResult ShareCapitalDetailList(ShareCapitalDetailListViewModel model)
        {
            UtilityController u = new UtilityController();
            model.BranchDesc = u.getBranchMastDetails();
            model.achddesc = u.getGlAcc_hd();
            return View(model);
        }
        public JsonResult getsharecapitaldetaillist(ShareCapitalDetailListViewModel model)
        {
            SHARE_LEDGER sl = new SHARE_LEDGER();
            List<SHARE_LEDGER> shl = new List<SHARE_LEDGER>();
            shl = sl.getdetails(model.branch_id, model.on_dt, model.gl_hd);
            int i = 1;
            decimal xtot = 0;
            if (shl.Count > 0)
            {
                foreach (var a in shl)
                {
                    if (i == 1)
                    {
                        model.tableelement = "<tr><th>srl</th><th>Membership No</th><th>Member Date</th><th>Member's Name</th><th>Units</th><th>Share Capital</th></tr>";
                        model.tableelement = model.tableelement + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.member_id + "</td><td>" + a.mem_date.ToString("dd/MM/yyyy").Replace("-", "/") + "</td><td>" + a.mem_name + "</td><td>" + a.balance_unit.ToString("0.00") + "</td><td>" + a.bal_amount.ToString("0.00") + "</td></tr>";
                    }
                    else
                    {
                        model.tableelement = model.tableelement + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.member_id + "</td><td>" + a.mem_date.ToString("dd/MM/yyyy").Replace("-", "/") + "</td><td>" + a.mem_name + "</td><td>" + a.balance_unit.ToString("0.00") + "</td><td>" + a.bal_amount.ToString("0.00") + "</td></tr>";
                    }
                    i = i + 1;
                }
            }
            else
            {
                model.tableelement = null;
            }
            return Json(model);
        }
        public JsonResult getdetaillistsummary(ShareCapitalDetailListViewModel model)
        {
            SHARE_LEDGER sl = new SHARE_LEDGER();
            List<SHARE_LEDGER> shl = new List<SHARE_LEDGER>();
            shl = sl.getdetails(model.branch_id, model.on_dt, model.gl_hd);
            int i = 1;
            decimal xtot = 0;
            if (shl.Count > 0)
            {
                foreach (var a in shl)
                {
                    if (i == 1)
                    {
                        model.tableelement = "<tr><th>Share Account Head</th><th>Face Value</th><th>Total Units</th><th>Total Share Capital</th></tr>";
                        model.tableelement = model.tableelement + "<tr><td>" + a.acc_desc + "</td><td>" + a.MISCDEP_BASEAMT.ToString("0.00") + "</td><td>" + a.balance_unit.ToString("0.00") + a.bal_amount.ToString("0.00") + "</td></tr>";
                        xtot = xtot + a.bal_amount;
                    }
                    else
                    {
                        model.tableelement = model.tableelement + "<tr><td>" + a.acc_desc + "</td><td>" + a.MISCDEP_BASEAMT.ToString("0.00") + "</td><td>" + a.balance_unit.ToString("0.00") + a.bal_amount.ToString("0.00") + "</td></tr>";
                        xtot = xtot + a.bal_amount;
                    }
                    i = i + 1;
                }
                model.gr_total = xtot.ToString("0.00");
            }
            else
            {
                model.tableelement = null;
            }
            return Json(model);
        }

        /********************************************Share Capital Details List End*******************************************/

        /********************************************Member Deposit Fund Ledger Statement Start *******************************************/
        [HttpGet]
        public ActionResult MemDepositeFundLedgerStmnt(MemDepositeFundLedgerStmntViewModel model)
        {
            UtilityController u = new UtilityController();

            model.BranchDesc = u.getBranchMastDetails();
            model.TypeDesc = u.getTypeMastDetails();
            model.achddesc = u.getGlAcchd();
            model.CastDesc = u.getCastMastDetails();
            model.ReligionDesc = u.getReligionMastDetails();
            model.DepartmentDesc = u.getDepartmentMastDetails();
            model.ServiceDesc = u.getServiceStatusMastDetails();
            model.EmpDesc = u.getEmployerMastDetails();
            model.EmpBranchDesc = u.getEmployerBranchMastDetails();
            model.CategoryDesc = u.getCategoryMastDetails();

            return View(model);
        }
        public JsonResult getdatabyMemberIdForfundLedgerStatement(MemDepositeFundLedgerStmntViewModel model)
        {
            DateTime XPAIDUPTO;
            DateTime XFPAIDUPTO;
            DateTime XPAIDUPTO1;
            DateTime xfrmm;
            DateTime xtomm;
            decimal XBUFF = 0;
            decimal XRESTADV = 0;
            Member_Mast mm = new Member_Mast();
            mm = mm.getdetailsbymemberid(model.branch, model.mem_no);
            model.mem_dt = Convert.ToDateTime(mm.mem_date).ToString("dd/MM/yyyy").Replace("-", "/");
            model.mem_type = mm.member_type;
            model.mem_cat = mm.member_category;
            model.mem_name = mm.mem_name;
            model.orgn = mm.emp_cd;
            model.unit = mm.emp_branch;
            model.dept = mm.dept;
            model.serv_sts = mm.serv_sts;
            model.caste = mm.caste;
            if (mm.sex == "M")
            {
                model.sex = "MALE";
            }
            else
            {
                model.sex = "FEMALE";
            }
            model.relgn = mm.relgn;
            model.mailAdd_hno = mm.mailAdd_house;
            model.mailAdd_add1 = mm.mailAdd_add1;
            model.mailAdd_add2 = mm.mailAdd_add2;
            model.mailAdd_city = mm.mailAdd_city;
            model.mailAdd_dist = mm.mailAdd_dist;
            model.mailAdd_state = mm.mailAdd_state;
            model.mailAdd_pin = mm.mailAdd_pin;


            if (model.gl_achd == "TF")
            {
                XBUFF = mm.tf_buffer;
                if (mm.tf_buffer > 0)
                {
                    model.buff_txt = "Transferred Rs. " + mm.tf_buffer.ToString("0.00");
                }

            }
            XPAIDUPTO = mm.mem_date;
            XPAIDUPTO = Convert.ToDateTime("01/" + XPAIDUPTO.Month +"/"+ XPAIDUPTO.Year);
            XPAIDUPTO = XPAIDUPTO.AddDays(-1);
            XFPAIDUPTO = XPAIDUPTO;
            if (mm.mem_closed != null && mm.mem_closed != "")
            {
                if (mm.close_dt != null & Convert.ToString(mm.close_dt) != "01/01/0001")
                {
                    model.statbar = "Membership Closed on " + mm.close_dt + ".";
                }
                else
                {
                    model.statbar = "Membership is Closed.";

                }

            }
            if (model.gl_achd == "TF")
            {

                if (XBUFF > 0)
                {
                    XPAIDUPTO = GET_INST_FOR(XBUFF + XRESTADV, XPAIDUPTO.AddDays(1));
                }
            }

            TF_Ledger tfl = new TF_Ledger();
            List<TF_Ledger> tfllst = new List<TF_Ledger>();
            tfllst = tfl.getdataByledgerTab(model.gl_achd,model.branch,model.mem_no);
            int i = 1;
            decimal xtot = 0;
            string XTR_TYPE = "";
            string xref = "";
            model.tableelement = "<tr><th>Date</th><th>Particulars of Transaction</th><th>Debit Amount</th><th>Credit Amount</th><th>Principal Bal.</th><th>Interest Bal.</th></tr>";

            if (tfllst.Count > 0)
            {
                foreach (var a in tfllst)
                {
                    XTR_TYPE = a.dr_cr == "D" ? "To " : "By ";
                    switch (a.vch_type)
                    {
                        case "C":
                            XTR_TYPE = XTR_TYPE + "Cash";
                            break;
                        case "B":
                            XTR_TYPE = XTR_TYPE + "Bank";
                            break;
                        case "T":
                            XTR_TYPE = XTR_TYPE + "Transfer";
                            break;
                        case "J":
                            XTR_TYPE = XTR_TYPE + "Journal";
                            break;
                    }
                    string stt = a.prin_amount > 0 ? " @Principal" : " @Interest";
                    XTR_TYPE = XTR_TYPE + stt;
                    if (a.vch_type == "T")
                    {
                        xref = a.ref_ac_hd;
                        if (xref != null && xref != "")
                        {
                            xref = xref + "/" + a.ref_pacno;
                            XTR_TYPE = XTR_TYPE + " (" + xref + ")";
                        }
                    }
                    switch (a.insert_mode)
                    {
                        case "SD":
                            XTR_TYPE = XTR_TYPE + " (" + a.insert_mode + ")";
                            break;
                        case "BF":
                            XTR_TYPE = XTR_TYPE + " (Balance From Ledger)";
                            break;

                    }
                    if (model.gl_achd == "TF")
                    {
                        if (a.dr_cr == "C" && a.prin_amount > 0)
                        {
                            XPAIDUPTO1 = GET_INST_FOR(a.prin_amount + XRESTADV, XPAIDUPTO.AddDays(1));
                            if (XPAIDUPTO1 > XPAIDUPTO)
                            {
                                xfrmm = XPAIDUPTO.AddDays(1);
                                xtomm = XPAIDUPTO1;
                            }
                            else
                            {
                                xfrmm = XPAIDUPTO.AddDays(1);
                                xtomm = xfrmm;
                            }
                            if (i == 1 && XBUFF > 0)
                            {
                                XTR_TYPE = XTR_TYPE + "[B/F Cleared Upto " + xtomm.ToString("dd/MM/yyyy") + "] ";
                            }
                            else
                            {
                                if (xfrmm != xtomm)
                                {
                                    XTR_TYPE = XTR_TYPE + " [" + xfrmm.ToString("dd/MM/yyyy") + " - " + xtomm.ToString("dd/MM/yyyy") + "] ";
                                }
                                else
                                {
                                    XTR_TYPE = XTR_TYPE + " [" + xfrmm.ToString("dd/MM/yyyy") + "] ";
                                }
                            }
                            if (XRESTADV > 0)
                            {
                                XTR_TYPE = XTR_TYPE + " + Adv.-Rs." + XRESTADV;
                            }
                        }
                    }
                    model.tableelement = model.tableelement + "<tr><td>" + a.vch_date + "</td><td>" + XTR_TYPE + "</td>";
                    if (a.dr_cr == "D")
                    {
                        if (a.prin_amount > 0)
                        {
                            model.tableelement = model.tableelement + "<td>" + a.prin_amount.ToString("0.00") + "</td>";
                        }
                        else
                        {
                            model.tableelement = model.tableelement + "<td>" + a.int_amount.ToString("0.00") + "</td>";
                        }
                    }
                    else
                    {
                        if (a.prin_amount > 0)
                        {
                            model.tableelement = model.tableelement + "<td>" + a.prin_amount.ToString("0.00") + "</td>";
                        }
                        else
                        {
                            model.tableelement = model.tableelement + "<td>" + a.int_amount.ToString("0.00") + "</td>";
                        }
                    }
                    model.tableelement = model.tableelement + "<td>" + a.prin_bal.ToString("0.00") + "</td><td>" + a.int_bal.ToString("0.00") + "</td></tr>";

                    i = i + 1;
                }
            }
            else
            {
                model.tableelement = null;
            }
            return Json(model);
        }
        public DateTime GET_INST_FOR(decimal XAMT, DateTime xfrdt)
        {
            DateTime GETINSTFOR = DateTime.Now;
            DateTime XTODT;
            DateTime XPDUPTO = xfrdt;
            decimal XPBAMT = 0;
            decimal XREST = XAMT;
            decimal XRATE = 0;
            decimal XRESTADV = 0;
            int XPRD = 0;
            int xinstl = 0;
            DateTime XFRDT1 = xfrdt;
            TF_Ledger tl = new TF_Ledger();
            List<TF_Ledger> tflst = new List<TF_Ledger>();
            tflst = tl.getTfRate();
           
            var result = tflst.FindLast(delegate (TF_Ledger sbl)
            {
                return sbl.eff_date <= XFRDT1;
            });
            result = tflst.FindLast(s => s.eff_date <= XFRDT1);
            if (result != null)
            {
                result = tflst.Find(delegate (TF_Ledger sbl)
                {
                    return sbl.eff_date > XFRDT1;
                });
                XFRDT1 = result.eff_date;
            }
            if (result == null)
            {
                
            }
            else
            {
                XRATE = result.tf_rate;
            }
            
            result = tflst.Find(delegate (TF_Ledger sbl)
            {
                return sbl.eff_date > XFRDT1;
            });
            if (result == null)
            {
                XTODT = Convert.ToDateTime("31/12/2099");
            }
            else
            {
                XTODT = result.eff_date.AddDays(-1);
            }
            XPRD = Convert.ToInt32(Convert.ToDateTime(XTODT).Subtract(Convert.ToDateTime(XFRDT1)).Days / (365.25 / 12));
            if (XAMT <= XPRD * XRATE)
            {
                xinstl = Convert.ToInt32(XAMT / XRATE);
                if (xinstl > 0)
                {
                    XPDUPTO = XFRDT1.AddMonths(xinstl - 1);
                    XPDUPTO = Convert.ToDateTime("01/" + Convert.ToString(XPDUPTO.Month == 12 ? 1 : XPDUPTO.Month + 1) + "/" + Convert.ToString(XPDUPTO.Month == 12 ? XPDUPTO.Year + 1 : XPDUPTO.Year));
                    XPDUPTO = XPDUPTO.AddDays(-1);
                    GETINSTFOR = XPDUPTO;
                    XREST = XREST - (xinstl * XRATE);
                    XRESTADV = XREST;
                }
                else
                {
                    GETINSTFOR = XPDUPTO;
                    XREST = XAMT;
                    XRESTADV = XREST;
                }
            }
            else
            {
                XAMT = XAMT - (XPRD * XRATE);
                XREST = XREST - (XPRD * XRATE);
                XPDUPTO = XTODT;
                XFRDT1 = XPDUPTO.AddDays(1);
            }
            return GETINSTFOR;
        }
        /********************************************Member Deposit Fund Ledger Statement End*******************************************/
        [HttpGet]
        public ActionResult MemDepositeFundDetailList(MemDepositeFundDetailListViewModel model)
        {
            UtilityController u = new UtilityController();
            model.BranchDesc = u.getBranchMastDetails();
            model.achddesc = u.getGlAcchd();
            return View(model);
        }

        /********************************************Member Details List Start*******************************************/
        [HttpGet]
        public ActionResult MemberDetailList(MemberDetailListViewModel model)
        {
            UtilityController u = new UtilityController();
            model.EmpBranchDesc = u.getEmployerBranchMastDetails();
            model.on_date = DateTime.Now.ToString("dd-MM-yyyy").Replace("-", "/");
            return View(model);
        }
        public JsonResult getmemberdetailslistbybookno(MemberDetailListViewModel model)
        {
            Member_Mast mm = new Member_Mast();
            List<Member_Mast> mml = new List<Member_Mast>();
            mml = mm.getdetailsbybookno(model.unit, model.book_no, model.on_date);
            int i = 1;
            decimal TOT_SHARE = 0;
            decimal tot_tf = 0;
            decimal XTOT_ITF = 0;
            decimal tot_gf = 0;
            decimal XTOT_IGF = 0;
            decimal XTOTSFL = 0;
            decimal XTOTISFL = 0;
            decimal XTOTSJL = 0;
            decimal XTOTISJL = 0;
            decimal XTOTSL3 = 0;
            decimal XTOTISL3 = 0;
            decimal XTOTPSL = 0;
            decimal XTOTIPSL = 0;
            decimal XTOTDLL = 0;
            decimal XTOTIDLL = 0;
            decimal XTOTsjl1 = 0;
            decimal XTOTisjl1 = 0;
            decimal XTOTM14 = 0;
            decimal XTOTIM14 = 0;
            decimal XTOTM12 = 0;
            decimal XTOTIM12 = 0;
            decimal XTOTSFL1 = 0;
            decimal XTOTISFL1 = 0;
            decimal XTOTPSL1 = 0;
            decimal XTOTIPSL1 = 0;
            decimal XTOTSL4 = 0;
            decimal XTOTISL4 = 0;
            decimal XTOTSL6 = 0;
            decimal XTOTISL6 = 0;
            decimal XTOTSL7 = 0;
            decimal XTOTISL7 = 0;
            if (mml.Count > 0)
            {
                foreach (var a in mml)
                {
                    if (i == 1)
                    {
                        model.tableelement = "<tr><th>srl</th><th>Member_Id</th><th>Name Of The Member</th><th>Share Capital</th><th>Thrift Fund</th><th>Gurantee Fund</th><th>Int.TF</th><th>Int.GF</th><th>Loan(16.5%)</th><th>Int(16.5%)</th><th>Loan(14.5%)</th><th>Int(14.5%)</th><th>Loan(13%)</th><th>Int(13%)</th><th>Loan(10%)</th><th>Int(10%)</th><th>DN Loan</th><th>Int. DLL</th><th>Loan 14</th><th>INT 14</th><th>Loan 12.5</th><th>INT 12.5</th><th>Loan 12.75</th><th>INT 12.75</th><th>Loan 11.5</th><th>INT 11.5</th><th>Loan 9</th><th>INT 9</th><th>SL 4</th><th>ISL 4</th><th>SL 6</th><th>ISL 6</th><th>SL 7</th><th>ISL 7</th></tr>";
                        model.tableelement = model.tableelement + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.mem_id + "</td><td>" + a.mem_name+ "</td><td>" + a.XSHARE.ToString("0.00") + "</td><td>" + a.XTF.ToString("0.00") + "</td><td>" + a.xgf.ToString("0.00") + "</td><td>" + a.xint_tf.ToString("0.00") + "</td><td>" + a.Xint_Gf.ToString("0.00") + "</td><td>" + a.xsfl.ToString("0.00") + "</td><td>" + a.XISFL.ToString("0.00") + "</td><td>" + a.xsjl.ToString("0.00") + "</td><td>" + a.XIsjl.ToString("0.00") + "</td><td>" + a.XSL3.ToString("0.00") + "</td><td>" + a.XISL3.ToString("0.00") + "</td><td>" + a.xpsl.ToString("0.00") + "</td><td>" + a.XIPSL.ToString("0.00") + "</td><td>" + a.xdll.ToString("0.00") + "</td><td>" + a.XIDLL.ToString("0.00") + "</td><td>" + a.xsjl1.ToString("0.00") + "</td><td>" + a.XIsjl1.ToString("0.00") + "</td><td>" + a.xM14.ToString("0.00") + "</td><td>" + a.XIM14.ToString("0.00") + "</td><td>" + a.xM12.ToString("0.00") + "</td><td>" + a.XIM12.ToString("0.00") + "</td><td>" + a.xSFL1.ToString("0.00") + "</td><td>" + a.XISFL1.ToString("0.00") + "</td><td>" + a.xPSL1.ToString("0.00") + "</td><td>" + a.XIPSL1.ToString("0.00") + "</td><td>" + a.xSL4.ToString("0.00") + "</td><td>" + a.XISL4.ToString("0.00") + "</td><td>" + a.xSL6.ToString("0.00") + "</td><td>" + a.XISL6.ToString("0.00") + "</td><td>" + a.xSL7.ToString("0.00") + "</td><td>" + a.XISL7.ToString("0.00") + "</td></tr>";
                        TOT_SHARE = TOT_SHARE + a.XSHARE;
                        tot_tf = tot_tf + a.XTF;
                        XTOT_ITF = XTOT_ITF + a.xint_tf;
                        tot_gf = tot_gf + a.xgf;
                        XTOT_IGF = XTOT_IGF + a.Xint_Gf;
                        XTOTSFL = XTOTSFL + a.xsfl;
                        XTOTISFL = XTOTISFL + a.XISFL;
                        XTOTSJL = XTOTSJL + a.xsjl;
                        XTOTISJL = XTOTISJL + a.XIsjl;
                        XTOTSL3 = XTOTSL3 + a.XSL3;
                        XTOTISL3 = XTOTISL3 + a.XISL3;
                        XTOTPSL = XTOTPSL + a.xpsl;
                        XTOTIPSL = XTOTIPSL + a.XIPSL;
                        XTOTDLL = XTOTDLL + a.xdll;
                        XTOTIDLL = XTOTIDLL + a.XIDLL;
                        XTOTsjl1 = XTOTsjl1 + a.xsjl1;
                        XTOTisjl1 = XTOTisjl1 + a.XIsjl1;
                        XTOTM14 = XTOTM14 + a.xM14;
                        XTOTIM14 = XTOTIM14 + a.XIM14;
                        XTOTM12 = XTOTM12 + a.xM12;
                        XTOTIM12 = XTOTIM12 + a.XIM12;
                        XTOTSFL1 = XTOTSFL1 + a.xSFL1;
                        XTOTISFL1 = XTOTISFL1 + a.XISFL1;
                        XTOTPSL1 = XTOTPSL1 + a.xPSL1;
                        XTOTIPSL1 = XTOTIPSL1 + a.XIPSL1;
                        XTOTSL4 = XTOTSL4 + a.xSL4;
                        XTOTISL4 = XTOTISL4 + a.XISL4;
                        XTOTSL6 = XTOTSL6 + a.xSL6;
                        XTOTISL6 = XTOTISL6 + a.XISL6;
                        XTOTSL7 = XTOTSL7 + a.xSL7;
                        XTOTISL7 = XTOTISL7 + a.XISL7;
                    }
                    else
                    {
                        model.tableelement = model.tableelement + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.mem_id + "</td><td>" + a.mem_name + "</td><td>" + a.XSHARE.ToString("0.00") + "</td><td>" + a.XTF.ToString("0.00") + "</td><td>" + a.xgf.ToString("0.00") + "</td><td>" + a.xint_tf.ToString("0.00") + "</td><td>" + a.Xint_Gf.ToString("0.00") + "</td><td>" + a.xsfl.ToString("0.00") + "</td><td>" + a.XISFL.ToString("0.00") + "</td><td>" + a.xsjl.ToString("0.00") + "</td><td>" + a.XIsjl.ToString("0.00") + "</td><td>" + a.XSL3.ToString("0.00") + "</td><td>" + a.XISL3.ToString("0.00") + "</td><td>" + a.xpsl.ToString("0.00") + "</td><td>" + a.XIPSL.ToString("0.00") + "</td><td>" + a.xdll.ToString("0.00") + "</td><td>" + a.XIDLL.ToString("0.00") + "</td><td>" + a.xsjl1.ToString("0.00") + "</td><td>" + a.XIsjl1.ToString("0.00") + "</td><td>" + a.xM14.ToString("0.00") + "</td><td>" + a.XIM14.ToString("0.00") + "</td><td>" + a.xM12.ToString("0.00") + "</td><td>" + a.XIM12.ToString("0.00") + "</td><td>" + a.xSFL1.ToString("0.00") + "</td><td>" + a.XISFL1.ToString("0.00") + "</td><td>" + a.xPSL1.ToString("0.00") + "</td><td>" + a.XIPSL1.ToString("0.00") + "</td><td>" + a.xSL4.ToString("0.00") + "</td><td>" + a.XISL4.ToString("0.00") + "</td><td>" + a.xSL6.ToString("0.00") + "</td><td>" + a.XISL6.ToString("0.00") + "</td><td>" + a.xSL7.ToString("0.00") + "</td><td>" + a.XISL7.ToString("0.00") + "</td></tr>";
                        TOT_SHARE = TOT_SHARE + mm.XSHARE;
                        TOT_SHARE = TOT_SHARE + a.XSHARE;
                        tot_tf = tot_tf + a.XTF;
                        XTOT_ITF = XTOT_ITF + a.xint_tf;
                        tot_gf = tot_gf + a.xgf;
                        XTOT_IGF = XTOT_IGF + a.Xint_Gf;
                        XTOTSFL = XTOTSFL + a.xsfl;
                        XTOTISFL = XTOTISFL + a.XISFL;
                        XTOTSJL = XTOTSJL + a.xsjl;
                        XTOTISJL = XTOTISJL + a.XIsjl;
                        XTOTSL3 = XTOTSL3 + a.XSL3;
                        XTOTISL3 = XTOTISL3 + a.XISL3;
                        XTOTPSL = XTOTPSL + a.xpsl;
                        XTOTIPSL = XTOTIPSL + a.XIPSL;
                        XTOTDLL = XTOTDLL + a.xdll;
                        XTOTIDLL = XTOTIDLL + a.XIDLL;
                        XTOTsjl1 = XTOTsjl1 + a.xsjl1;
                        XTOTisjl1 = XTOTisjl1 + a.XIsjl1;
                        XTOTM14 = XTOTM14 + a.xM14;
                        XTOTIM14 = XTOTIM14 + a.XIM14;
                        XTOTM12 = XTOTM12 + a.xM12;
                        XTOTIM12 = XTOTIM12 + a.XIM12;
                        XTOTSFL1 = XTOTSFL1 + a.xSFL1;
                        XTOTISFL1 = XTOTISFL1 + a.XISFL1;
                        XTOTPSL1 = XTOTPSL1 + a.xPSL1;
                        XTOTIPSL1 = XTOTIPSL1 + a.XIPSL1;
                        XTOTSL4 = XTOTSL4 + a.xSL4;
                        XTOTISL4 = XTOTISL4 + a.XISL4;
                        XTOTSL6 = XTOTSL6 + a.xSL6;
                        XTOTISL6 = XTOTISL6 + a.XISL6;
                        XTOTSL7 = XTOTSL7 + a.xSL7;
                        XTOTISL7 = XTOTISL7 + a.XISL7;
                    }
                    i = i + 1;
                }
                model.shtot = TOT_SHARE.ToString("0.00");
                model.to_gf = tot_gf.ToString("0.00");
                model.tf = tot_tf.ToString("0.00");
                model.to_itf = XTOT_ITF.ToString("0.00");
                model.to_igf = XTOT_IGF.ToString("0.00");
                model.ln_prncl_16_5 = XTOTSFL.ToString("0.00");
                model.ln_int_16_5 = XTOTISFL.ToString("0.00");
                model.ln_prncl_14_5 = XTOTSJL.ToString("0.00");
                model.ln_int_14_5 = XTOTISJL.ToString("0.00");
                model.ln_prncl_13 = XTOTSL3.ToString("0.00");
                model.ln_int_13 = XTOTISL3.ToString("0.00");
                model.ln_prncl_10 = XTOTPSL.ToString("0.00");
                model.ln_int_10 = XTOTIPSL.ToString("0.00");
                model.ln_prncl_dll = XTOTDLL.ToString("0.00");
                model.ln_int_dll = XTOTIDLL.ToString("0.00");
                model.ln_prncl_14 = XTOTsjl1.ToString("0.00");
                model.ln_int_14 = XTOTisjl1.ToString("0.00");
                model.ln_prncl_12_5 = XTOTM14.ToString("0.00");
                model.ln_int_12_5 = XTOTIM14.ToString("0.00");
                model.ln_prncl_12_75 = XTOTM12.ToString("0.00");
                model.ln_int_12_75 = XTOTIM12.ToString("0.00");
                model.ln_prncl_11_5 = XTOTSFL1.ToString("0.00");
                model.int_11_5 = XTOTISFL1.ToString("0.00");
                model.ln_prncl_9 = XTOTPSL1.ToString("0.00");
                model.int_9 = XTOTIPSL1.ToString("0.00");
                model.ln_prncl_13_75 = XTOTSL4.ToString("0.00");
                model.int_13_75 = XTOTISL4.ToString("0.00");
                model.ln_prncl_13_5 = XTOTSL6.ToString("0.00");
                model.int_13_5 = XTOTISL6.ToString("0.00");
                model.total_mem = Convert.ToString(i - 1);
            }
            else
            {
                model.tableelement = null;
            }
            return Json(model);
        }
        public JsonResult getmemberledgerdetailswithoutinterestbybookno(MemberDetailListViewModel model)
        {
            Member_Mast mm = new Member_Mast();
            List<Member_Mast> mml = new List<Member_Mast>();
            mml = mm.getdetailsbybookno(model.unit, model.book_no, model.on_date);
            int i = 1;
            decimal TOT_SHARE = 0;
            decimal tot_tf = 0;         
            decimal tot_gf = 0;        
            decimal XTOTSFL = 0;          
            decimal XTOTSJL = 0;          
            decimal XTOTSL3 = 0;         
            decimal XTOTPSL = 0;           
            decimal XTOTDLL = 0;          
            decimal XTOTsjl1 = 0;           
            decimal XTOTM14 = 0;           
            decimal XTOTM12 = 0;            
            decimal XTOTSFL1 = 0;
            decimal XTOTISFL1 = 0;
            decimal XTOTPSL1 = 0;
            
            if (mml.Count > 0)
            {
                foreach (var a in mml)
                {
                    if (i == 1)
                    {
                        model.tableelement = "<tr><th>srl</th><th>Member_Id</th><th>Name Of The Member</th><th>Share Capital</th><th>Thrift Fund</th><th>Gurantee Fund</th><th>Loan(16.5%)</th><th>Loan(14.5%)</th><th>Loan(13%)</th><th>Loan(10%)</th><th>DN Loan</th><th>Loan 14</th><th>Loan 12.5</th><th>Loan 12.75</th><th>Loan 11.5</th><th>Loan 9</th></tr>";
                        model.tableelement = model.tableelement + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.mem_id + "</td><td>" + a.mem_name + "</td><td>" + a.XSHARE.ToString("0.00") + "</td><td>" + a.XTF.ToString("0.00") + "</td><td>" + a.xgf.ToString("0.00") + "</td><td>" + a.xsfl.ToString("0.00") + "</td><td>" + a.xsjl.ToString("0.00") + "</td><td>" + a.XSL3.ToString("0.00") + "</td><td>" + a.xpsl.ToString("0.00") + "</td><td>" + a.xdll.ToString("0.00") + "</td><td>" + a.xsjl1.ToString("0.00") + "</td><td>" + a.xM14.ToString("0.00") + "</td><td>" + a.xM12.ToString("0.00") + "</td><td>" + a.xSFL1.ToString("0.00") + "</td><td>" + a.xPSL1.ToString("0.00") + "</td></tr>";
                        TOT_SHARE = TOT_SHARE + a.XSHARE;
                        tot_tf = tot_tf + a.XTF;                      
                        tot_gf = tot_gf + a.xgf;                       
                        XTOTSFL = XTOTSFL + a.xsfl;                      
                        XTOTSJL = XTOTSJL + a.xsjl;                       
                        XTOTSL3 = XTOTSL3 + a.XSL3;                       
                        XTOTPSL = XTOTPSL + a.xpsl;                      
                        XTOTDLL = XTOTDLL + a.xdll;                       
                        XTOTsjl1 = XTOTsjl1 + a.xsjl1;                      
                        XTOTM14 = XTOTM14 + a.xM14;                      
                        XTOTM12 = XTOTM12 + a.xM12;                    
                        XTOTSFL1 = XTOTSFL1 + a.xSFL1;
                        XTOTISFL1 = XTOTISFL1 + a.XISFL1;
                        XTOTPSL1 = XTOTPSL1 + a.xPSL1;                                                
                    }
                    else
                    {
                        model.tableelement = model.tableelement + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.mem_id + "</td><td>" + a.mem_name + "</td><td>" + a.XSHARE.ToString("0.00") + "</td><td>" + a.XTF.ToString("0.00") + "</td><td>" + a.xgf.ToString("0.00") + "</td><td>" + a.xsfl.ToString("0.00") + "</td><td>" + a.xsjl.ToString("0.00") + "</td><td>" + a.XSL3.ToString("0.00") + "</td><td>" + a.xpsl.ToString("0.00") + "</td><td>" + a.xdll.ToString("0.00") + "</td><td>" + a.xsjl1.ToString("0.00") + "</td><td>" + a.xM14.ToString("0.00") + "</td><td>" + a.xM12.ToString("0.00") + "</td><td>" + a.xSFL1.ToString("0.00") + "</td><td>" + a.xPSL1.ToString("0.00") + "</td></tr>";
                        TOT_SHARE = TOT_SHARE + a.XSHARE;
                        tot_tf = tot_tf + a.XTF;
                        tot_gf = tot_gf + a.xgf;
                        XTOTSFL = XTOTSFL + a.xsfl;
                        XTOTSJL = XTOTSJL + a.xsjl;
                        XTOTSL3 = XTOTSL3 + a.XSL3;
                        XTOTPSL = XTOTPSL + a.xpsl;
                        XTOTDLL = XTOTDLL + a.xdll;
                        XTOTsjl1 = XTOTsjl1 + a.xsjl1;
                        XTOTM14 = XTOTM14 + a.xM14;
                        XTOTM12 = XTOTM12 + a.xM12;
                        XTOTSFL1 = XTOTSFL1 + a.xSFL1;
                        XTOTISFL1 = XTOTISFL1 + a.XISFL1;
                        XTOTPSL1 = XTOTPSL1 + a.xPSL1;
                    }
                    i = i + 1;
                }
                model.shtot = TOT_SHARE.ToString("0.00");
                model.to_gf = tot_gf.ToString("0.00");
                model.tf = tot_tf.ToString("0.00");             
                model.ln_prncl_16_5 = XTOTSFL.ToString("0.00");                
                model.ln_prncl_14_5 = XTOTSJL.ToString("0.00");            
                model.ln_prncl_13 = XTOTSL3.ToString("0.00");              
                model.ln_prncl_10 = XTOTPSL.ToString("0.00");              
                model.ln_prncl_dll = XTOTDLL.ToString("0.00");              
                model.ln_prncl_14 = XTOTsjl1.ToString("0.00");               
                model.ln_prncl_12_5 = XTOTM14.ToString("0.00");               
                model.ln_prncl_12_75 = XTOTM12.ToString("0.00");              
                model.ln_prncl_11_5 = XTOTSFL1.ToString("0.00");               
                model.ln_prncl_9 = XTOTPSL1.ToString("0.00");
                model.total_mem = Convert.ToString(i - 1);
            }
            else
            {
                model.tableelement = null;
            }
            return Json(model);
        }
        public ActionResult MemberDetailsPrintfile(MemberDetailListViewModel model)
        {
            Member_Mast mm = new Member_Mast();
            List<Member_Mast> mml = new List<Member_Mast>();
            mml = mm.getdetailsbybookno(model.unit, model.book_no, model.on_date);
            Directory.CreateDirectory(Server.MapPath("~/wwwroot\\TextFiles"));
            using (StreamWriter sw = new StreamWriter(Server.MapPath("~/wwwroot\\TextFiles\\Member_Details_List.txt")))
            {
                int Pg = 1;
                int Ln = 0;
                int i = 1;
                string serial = string.Empty;
                string mem_id = "";
                string mem_name = "";
                sw.WriteLine("BOOK - NO =" + model.book_no);
                sw.WriteLine("Bansra Employees' Co-operative Credit Society Ltd.  Pg No :" + Pg);
                sw.WriteLine("Member Detaillist As On " +"  " + model.on_date);
                sw.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|-----------------------------------------------");
                sw.WriteLine("Srl |Member No.| Member Name             |SHARE      |TF         | GF        |INT TF     |INT GF     | Loan Dues | Intt. Dues|Loan14.5   |Int 14.5   |Loan13     |Int13      |Loan10     |Int10      |Dll        |Int Dll    |Loan 14    |Int 14     |Loan 12.5  |Int 12.5   |Loan 12.75 |Int 12.75  |Loan 11.5  |Int 11.5   |Loan 9     |Int 9      |SL4 13.75  |INT SL4    |SL6 13.5   |INT SL6    ");
                sw.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|-----------------------------------------------");       
                foreach (var am in mml)
                {                   
                    if (Ln > Pg * 65)
                    {
                        Pg = Pg + 1;
                        Ln = Ln + 7;
                        sw.WriteLine("BOOK - NO =" + model.book_no);
                        sw.WriteLine("Bansra Employees' Co-operative Credit Society Ltd.  Pg No :" + Pg);
                        sw.WriteLine("Member Detaillist As On " + "  " + model.on_date);
                        sw.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|-----------------------------------------------");
                        sw.WriteLine("Srl |Member No.| Member Name             |SHARE      |TF         | GF        |INT TF     |INT GF     | Loan Dues | Intt. Dues|Loan14.5   |Int 14.5   |Loan13     |Int13      |Loan10     |Int10      |Dll        |Int Dll    |Loan 14    |Int 14     |Loan 12.5  |Int 12.5   |Loan 12.75 |Int 12.75  |Loan 11.5  |Int 11.5   |Loan 9     |Int 9      |SL4 13.75  |INT SL4    |SL6 13.5   |INT SL6    ");
                        sw.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|-----------------------------------------------");
                    }
                    if (Convert.ToString(i).Length > 4)
                    {
                        serial = Convert.ToString(i).Substring(0, 3);
                    }
                    else
                    {
                        serial = Convert.ToString(i);
                    }
                    if (am.mem_id.ToString().Length > 10)
                    {
                        mem_id = (am.mem_id).Substring(0, 9);
                    }
                    else
                    {
                        mem_id = am.mem_id;
                    }                   
                    if (am.mem_name.ToString().Length > 25)
                    {
                        mem_name = (am.mem_name).Substring(0, 24);
                    }
                    else
                    {
                        mem_name = am.mem_name;
                    }
                    sw.WriteLine("".ToString().PadLeft(4 - (serial).ToString().Length) + serial + "|"
                          + "".ToString().PadLeft(10 - (mem_id).ToString().Length) + mem_id + "|"
                          + "".ToString().PadLeft(25 - (mem_name).ToString().Length) + mem_name + "|"
                           + "".ToString().PadLeft(15 - (am.XSHARE).ToString().Length) + am.XSHARE.ToString("0.00") + "|"
                            + "".ToString().PadLeft(16 - (am.XTF).ToString().Length) + am.XTF.ToString("0.00") + "|"
                             + "".ToString().PadLeft(16 - (am.xgf).ToString().Length) + am.xgf.ToString("0.00") + "|"
                              + "".ToString().PadLeft(16 - (am.xint_tf).ToString().Length) + am.xint_tf.ToString("0.00") + "|"
                              + "".ToString().PadLeft(15 - (am.Xint_Gf).ToString().Length) + am.Xint_Gf.ToString("0.00") + "|"
                               + "".ToString().PadLeft(16 - (am.xsfl).ToString().Length) + am.xsfl.ToString("0.00") + "|"
                               + "".ToString().PadLeft(15 - (am.XISFL).ToString().Length) + am.XISFL.ToString("0.00") + "|"
                               + "".ToString().PadLeft(15 - (am.xsjl).ToString().Length) + am.xsjl.ToString("0.00") + "|"
                               + "".ToString().PadLeft(15 - (am.XIsjl).ToString().Length) + am.XIsjl.ToString("0.00") + "|"
                               + "".ToString().PadLeft(15 - (am.XSL3).ToString().Length) + am.XSL3.ToString("0.00") + "|"
                               + "".ToString().PadLeft(15 - (am.XISL3).ToString().Length) + am.XISL3.ToString("0.00") + "|"
                               + "".ToString().PadLeft(15 - (am.xpsl).ToString().Length) + am.xpsl.ToString("0.00") + "|"
                               + "".ToString().PadLeft(15 - (am.XIPSL).ToString().Length) + am.XIPSL.ToString("0.00") + "|"
                               + "".ToString().PadLeft(15 - (am.xdll).ToString().Length) + am.xdll.ToString("0.00") + "|"
                               + "".ToString().PadLeft(15 - (am.XIDLL).ToString().Length) + am.XIDLL.ToString("0.00") + "|"
                               + "".ToString().PadLeft(15 - (am.xsjl1).ToString().Length) + am.xsjl1.ToString("0.00") + "|"
                               + "".ToString().PadLeft(15 - (am.XIsjl1).ToString().Length) + am.XIsjl1.ToString("0.00") + "|"
                               + "".ToString().PadLeft(15 - (am.xM14).ToString().Length) + am.xM14.ToString("0.00") + "|"
                               + "".ToString().PadLeft(15 - (am.XIM14).ToString().Length) + am.XIM14.ToString("0.00") + "|"
                               + "".ToString().PadLeft(15 - (am.xM12).ToString().Length) + am.xM12.ToString("0.00") + "|"
                               + "".ToString().PadLeft(15 - (am.XIM12).ToString().Length) + am.XIM12.ToString("0.00") + "|"
                               + "".ToString().PadLeft(15 - (am.xSFL1).ToString().Length) + am.xSFL1.ToString("0.00") + "|"
                               + "".ToString().PadLeft(15 - (am.XISFL1).ToString().Length) + am.XISFL1.ToString("0.00") + "|"
                               + "".ToString().PadLeft(15 - (am.xPSL1).ToString().Length) + am.xPSL1.ToString("0.00") + "|"
                               + "".ToString().PadLeft(15 - (am.XIPSL1).ToString().Length) + am.XIPSL1.ToString("0.00") + "|"
                               + "".ToString().PadLeft(15 - (am.xSL4).ToString().Length) + am.xSL4.ToString("0.00") + "|"
                               + "".ToString().PadLeft(15 - (am.XISL4).ToString().Length) + am.XISL4.ToString("0.00") + "|"
                               + "".ToString().PadLeft(15 - (am.xSL6).ToString().Length) + am.xSL6.ToString("0.00") + "|"
                               + "".ToString().PadLeft(15 - (am.XISL6).ToString().Length) + am.XISL6.ToString("0.00") + "|"
                               //+ "".ToString().PadLeft(10 - (am.xSL7).ToString().Length) + am.xSL7.ToString("0.00") + "|"
                               //+ "".ToString().PadLeft(10 - (am.XISL7).ToString().Length) + am.XISL7.ToString("0.00") + "|"
                               );                    
                    Ln = Ln + 1;
                    sw.WriteLine("");
                    sw.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|");
                    i = i + 1;
                    Ln = Ln + 1;
                }
                sw.WriteLine("");
                sw.WriteLine("");
                sw.WriteLine("Total :--->");
                sw.WriteLine("Share Capital : " + model.shtot + "  " + "Gurantee Fund:" + model.to_gf + " " + "Thrift Fund :" + model.tf + " " + "Int. Tf : " + model.to_itf +" " + "Int. Gf :" + model.to_igf);
                sw.WriteLine("Loan 16.5 : " + model.ln_prncl_16_5 + " " + "Int. 16.5 :" + model.ln_int_16_5 + " " + "Loan 14.5 :" + model.ln_prncl_14_5 + " " + "Int. 14.5 :" + model.ln_int_14_5);
                sw.WriteLine("Loan 13 : " + model.ln_prncl_13 + " " + "Int 13 " + model.ln_int_13);
                sw.WriteLine("Loan 10 :" + model.ln_prncl_10 + " " + "Int 10 :" + model.ln_int_10);
                sw.WriteLine("Loan DLL :" + model.ln_prncl_dll + " " + "Int DLL :" + model.ln_int_dll);
                sw.WriteLine("Loan 14 :" + model.ln_prncl_14 + " " + "Int DLL :" + model.ln_int_14);
                sw.WriteLine("Loan 12.5 :" + model.ln_prncl_12_5 + " " + "Int 12.5 :" + model.ln_int_12_5);
                sw.WriteLine("Loan 12.75 :" + model.ln_prncl_12_75 +" "+ "Int 12.75 :" + model.ln_int_12_75);
                sw.WriteLine("Loan 11.5 :" + model.ln_prncl_11_5 + " " + "Int 11.5 :" + model.int_11_5);
                sw.WriteLine("Loan 9 :" + model.ln_prncl_9 + " " + "Int 9 :" + model.int_9);
                sw.WriteLine("Loan 13.75 :" + model.ln_prncl_13_75 + " " + "Int 13.75 :" + model.int_13_75);
                sw.WriteLine("Loan 13.5 :" + model.ln_prncl_13_5 + " " + "Int 13.5 :" + model.int_13_5);
            }
            UtilityController u = new UtilityController();
            var memory = u.DownloadTextFiles("Member_Details_List.txt", Server.MapPath("~/wwwroot\\TextFiles"));
            if (System.IO.File.Exists(Server.MapPath("~/wwwroot\\TextFiles\\Member_Details_List.txt")))
            {
                System.IO.File.Delete(Server.MapPath("~/wwwroot\\TextFiles\\Member_Details_List.txt"));
            }
            return File(memory.ToArray(), "text/plain", "Member_Details_List_" + DateTime.Now.ToShortDateString().Replace("/", "_") + ".txt");
        }

        /********************************************Member Details List End*******************************************/
    }    
}
