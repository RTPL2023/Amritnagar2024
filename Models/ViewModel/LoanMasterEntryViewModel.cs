using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Amritnagar.Models.ViewModel
{
    public class LoanMasterEntryViewModel
    {
        public string branch_id { get; set; }
        public string ac_hd { get; set; }
        public string emp_id { get; set; }
        public string loan_dt { get; set; }
        public string status_cd { get; set; }
        public string mem_id { get; set; }
        public string mem_dt { get; set; }
        public string mem_type { get; set; }
        public string mem_cat { get; set; }
        public string book_no { get; set; }
        public string loanee_name { get; set; }
        public string gurdian_name { get; set; }
        public string loan_amt { get; set; }
        public string prin_amt { get; set; }
        public string inst_amt { get; set; }
        public string inst_no { get; set; }
        public string inst_rate { get; set; }
        public string ln_purpose { get; set; }
        public string lic_premium { get; set; }
        public string vch_date { get; set; }
        public string vch_no { get; set; }
        public string msg { get; set; }
        public string tableelement { get; set; }
        public IEnumerable<SelectListItem> BranchDesc { get; set; }
        public IEnumerable<SelectListItem> lnpurposedesc { get; set; }
        public IEnumerable<SelectListItem> lnstatusdesc { get; set; }
        public IEnumerable<SelectListItem> lntypedesc { get; set; }
        public IEnumerable<SelectListItem> achddesc { get; set; }

    }
}