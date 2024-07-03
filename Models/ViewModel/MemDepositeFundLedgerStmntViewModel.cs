using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Amritnagar.Models.ViewModel
{
    public class MemDepositeFundLedgerStmntViewModel
    {
        public string branch { get; set; }
        public string gl_achd { get; set; }
        public string mem_no { get; set; }
        public string mem_type { get; set; }
        public string mem_name { get; set; }
        public string mem_dt { get; set; }
        public string mem_cat { get; set; }
        public string caste { get; set; }
        public string sex { get; set; }
        public string relgn { get; set; }
        public string dept { get; set; }
        public string serv_sts { get; set; }
        public string orgn { get; set; }
        public string unit { get; set; }
        public string mailAdd_hno { get; set; }
        public string mailAdd_add1 { get; set; }
        public string mailAdd_add2 { get; set; }
        public string mailAdd_city { get; set; }
        public string mailAdd_dist { get; set; }
        public string mailAdd_state { get; set; }
        public string mailAdd_pin { get; set; }
        public string pay_clrd_upto { get; set; }
        public string buff_txt { get; set; }
        public string due_amt { get; set; }
        public string statbar { get; set; }
        public string tableelement { get; set; }
        public IEnumerable<SelectListItem> BranchDesc { get; set; }
        public IEnumerable<SelectListItem> achddesc { get; set; }
        public IEnumerable<SelectListItem> CategoryDesc { get; set; }
        public IEnumerable<SelectListItem> EmpBranchDesc { get; set; }
        public IEnumerable<SelectListItem> CastDesc { get; set; }
        public IEnumerable<SelectListItem> ReligionDesc { get; set; }
        public IEnumerable<SelectListItem> ServiceDesc { get; set; }
        public IEnumerable<SelectListItem> DepartmentDesc { get; set; }
        public IEnumerable<SelectListItem> EmpDesc { get; set; }
        public IEnumerable<SelectListItem> TypeDesc { get; set; }
    }
}