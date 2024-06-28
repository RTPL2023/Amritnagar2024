using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Amritnagar.Models.ViewModel
{
    public class MonthlyInterestScheduleForLoanViewModel
    {
        public string branch_id { get; set; }
        public string ln_achd { get; set; }
        public string colliery_code { get; set; }
        public string book_no { get; set; }
        public string sch_date { get; set; }
        public string fr_dt { get; set; }
        public string vch_dt { get; set; }
        public string to_dt { get; set; }
        public string prcl_bal { get; set; }
        public string inst_debt { get; set; }
        public string tableelement { get; set; }
        public IEnumerable<SelectListItem> BranchDesc { get; set; }
        public IEnumerable<SelectListItem> lntypedesc { get; set; }
        public IEnumerable<SelectListItem> EmpBranchDesc { get; set; }

    }
}