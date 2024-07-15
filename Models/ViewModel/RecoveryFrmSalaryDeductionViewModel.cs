using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Amritnagar.Models.ViewModel
{
    public class RecoveryFrmSalaryDeductionViewModel
    {
        public string branch { get; set; }
        public string sch_dt { get; set; }
        public string rec_dt { get; set; }
        public string emplyer_name { get; set; }
        public string emp_unit { get; set; }
        public string ded_achd { get; set; }
        public string rec_achd { get; set; }
        public string book_no { get; set; }
        public string prnt_bal { get; set; }
        public string int_bal { get; set; }
        public string tot_bal { get; set; }
        public string grid1 { get; set; }
        public string grid2 { get; set; }
        public IEnumerable<SelectListItem> BranchDesc { get; set; }
        public IEnumerable<SelectListItem> EmpBranchDesc { get; set; }
        public IEnumerable<SelectListItem> EmpDesc { get; set; }
    }
}