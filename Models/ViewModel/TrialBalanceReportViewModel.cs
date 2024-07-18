using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Amritnagar.Models.ViewModel
{
    public class TrialBalanceReportViewModel
    {
        public string branch { get; set; }
        public string bal_as_on { get; set; }
        public string label { get; set; }
        public IEnumerable<SelectListItem> BranchDesc { get; set; }
    }
}