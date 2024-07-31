using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Amritnagar.Models.ViewModel
{
    public class GeneralLedgerReportViewModel
    {
        public string branch { get; set; }
        public string fr_dt { get; set; }
        public string to_dt { get; set; } 
        public string ac_hd { get; set; } 
        public string ac_desc { get; set; }
        public string tableelement { get; set; }
        public IEnumerable<SelectListItem> BranchDesc { get; set; }
    }
}