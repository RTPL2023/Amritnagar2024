using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Amritnagar.Models.ViewModel
{
    public class VoucherEntryViewModel
    {
        public string branch { get; set; }
        public string vch_date { get; set; }
        public string vch_no { get; set; }
        public string vch_type { get; set; }
        public string debt_amt { get; set; }
        public string crdt_amt { get; set; }
        public string net_amt { get; set; }
        public string tot_chq_amt { get; set; }
        public IEnumerable<SelectListItem> BranchDesc { get; set; }
    }
}