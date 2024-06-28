using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Amritnagar.Models.ViewModel
{
    public class LoanDetailListViewModel
    {
        public string branch_id { get; set; }
        public string ac_hd { get; set; }
        public string on_date { get; set; }
        public string prcl_bal { get; set; }
        public string int_rec { get; set; }
        public string add_int_rec { get; set; }
        public string tableelement { get; set; }
        public IEnumerable<SelectListItem> BranchDesc { get; set; }
        public IEnumerable<SelectListItem> lntypedesc { get; set; }
    }
}