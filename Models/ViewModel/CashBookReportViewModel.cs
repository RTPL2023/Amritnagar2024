using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Amritnagar.Models.ViewModel
{
    public class CashBookReportViewModel
    {
        public string branch { get; set; }
        public string fr_dt { get; set; }
        public string to_dt { get; set; }
        public string  book_type { get; set; }
        public IEnumerable<SelectListItem> BranchDesc { get; set; }
        public string ac_hddesc { get; set; }
        public string ac_hd { get; set; }
        public string tablee { get; set; }
    }
}