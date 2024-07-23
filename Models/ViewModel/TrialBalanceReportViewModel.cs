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
        public string gl_date { get; set; }
        public string tableele { get; set; }
        public string label1 { get; set; }
        public string label2 { get; set; }
        public IEnumerable<SelectListItem> BranchDesc { get; set; }
    }
}