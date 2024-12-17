using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Amritnagar.Models.ViewModel
{
    public class LoanLedgerCorrectionViewModel
    {
        public string branch_id { get; set; }
        public string ac_hd { get; set; }
        public string old_date { get; set; }
        public string emp_id { get; set; }
        public string date { get; set; }
        public string vch_no { get; set; }
        public string prnt_amt { get; set; }
        public string prnt_bal { get; set; }
        public string chq_no { get; set; }
        public string vch_type { get; set; }
        public string vch_srl { get; set; }
        public string drcr { get; set; }
        public string int_amt { get; set; }
        public string int_bal { get; set; }
        public string chq_dt { get; set; }
        public string bank_cd { get; set; }
        public string tableelement { get; set; }
        public string msg { get; set; }
        public IEnumerable<SelectListItem> BranchDesc { get; set; }
        public IEnumerable<SelectListItem> lntypedesc { get; set; }
    }
}