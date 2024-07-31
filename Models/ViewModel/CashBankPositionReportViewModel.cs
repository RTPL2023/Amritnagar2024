using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Amritnagar.Models.ViewModel
{
    public class CashBankPositionReportViewModel
    {
        public string branch { get; set; }
        public string as_on_dt { get; set; }
        public string td_opbal { get; set; }
        public string td_credit { get; set; }
        public string td_debit { get; set; }
        public string td_clbal { get; set; }
        public string tl_opbal { get; set; }
        public string tl_credit { get; set; }
        public string tl_debit { get; set; }
        public string tl_clbal { get; set; }
        public string table1 { get; set; }
        public string table2 { get; set; }
        public string table3 { get; set; }

        public IEnumerable<SelectListItem> BranchDesc { get; set; }
    }
}