using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Amritnagar.Models.ViewModel
{
    public class PrepOfDeductionScheduleViewModel
    {
        public string branch { get; set; }
        public string sch_dt { get; set; }
        public string emp_name { get; set; }
        public string unit { get; set; }
        public string mem_type { get; set; }
        public string mem_cat { get; set; }
        public string book_no { get; set; }
        public string prnt_bal { get; set; }
        public string int_bal { get; set; }
        public string tot_bal { get; set; }
        public IEnumerable<SelectListItem> BranchDesc { get; set; }
        public IEnumerable<SelectListItem> TypeDesc { get; set; }
        public IEnumerable<SelectListItem> CategoryDesc { get; set; }
        public IEnumerable<SelectListItem> EmpBranchDesc { get; set; }
        public IEnumerable<SelectListItem> EmpDesc { get; set; }
    }
}