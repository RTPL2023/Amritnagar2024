using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Amritnagar.Models.ViewModel
{
    public class DividendWritOfSchViewModel
    {
        public string branch { get; set; }
        public string upto_dt { get; set; }
        public string vch_dt { get; set; }
        public string prnt_bal { get; set; }
        public IEnumerable<SelectListItem> BranchDesc { get; set; }
    }
}