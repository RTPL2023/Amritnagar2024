using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Amritnagar.Models.ViewModel
{
    public class MemberMasterViewModel
    {
        public string f_name { get; set; }
        public string branch_id { get; set; }
        public string emp_branch { get; set; }
        public string intr_mem_no { get; set; }
        public string intr_mem_name { get; set; }
        public string srl { get; set; }
        public string nominee_member_rel { get; set; }
        public string tf_buffer { get; set; }
        public string book_no { get; set; }
        public string emp_id { get; set; }
        public string emp_cd { get; set; }
        public string mem_id { get; set; }
        public string mem_date { get; set; }
        public string member_type { get; set; }
        public string member_category { get; set; }        
        public string l_name { get; set; }      
        public string mem_name { get; set; }      
        public string birth_date { get; set; }
        public string sex { get; set; }
        public string guardian_name { get; set; }
        public string member_rel { get; set; }
        public string caste { get; set; }
        public string occupation { get; set; }
        public string relgn { get; set; }
        public bool ltl_app { get; set; }
        public bool married { get; set; }
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
        public string th_f_amt { get; set; }
        public string share { get; set; }
        public string dept { get; set; }
        public string desig { get; set; }
        public string phn { get; set; }
        public string serv_sts { get; set; }
        public string join_dt { get; set; }
        public string retmnt_dt { get; set; }
        public string nominee_name { get; set; }
        public string nominee_add1 { get; set; }
        public string nominee_add2 { get; set; }
        public string nominee_rel { get; set; }
        public string nominee_city { get; set; }
        public string nominee_dist { get; set; }
        public string nominee_state { get; set; }
        public string nominee_pin { get; set; }
        public string nominee_dob { get; set; }
        public string nominee_hno { get; set; }
        public bool trans { get; set; }
        public bool ret { get; set; }
        public bool exp { get; set; }
        public string exp_dt { get; set; }
        public bool tf_double { get; set; }
        public bool mem_closed { get; set; }
        public string close_dt { get; set; }
        public string remarks { get; set; }
        public string bank_code { get; set; }
        public string accc_no { get; set; }
        public string pan { get; set; }
        public string nominee_birth_date { get; set; }
        public string msg { get; set; }
        public IEnumerable<SelectListItem> EmpBranchDesc { get; set; }
        public IEnumerable<SelectListItem> CastDesc { get; set; }
        public IEnumerable<SelectListItem> ReligionDesc { get; set; }
        public IEnumerable<SelectListItem> OccupationDesc { get; set; }
        public IEnumerable<SelectListItem> RelationDesc { get; set; }
        public IEnumerable<SelectListItem> DesignationDesc { get; set; }
        public IEnumerable<SelectListItem> ServiceDesc { get; set; }
        public IEnumerable<SelectListItem> DepartmentDesc { get; set; }
        public IEnumerable<SelectListItem> CategoryDesc { get; set; }
        public IEnumerable<SelectListItem> TypeDesc { get; set; }
        public IEnumerable<SelectListItem> BranchDesc { get; set; }
        public IEnumerable<SelectListItem> EmpDesc { get; set; }
    }
}