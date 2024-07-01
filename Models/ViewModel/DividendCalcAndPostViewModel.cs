using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Amritnagar.Models.ViewModel
{
    public class DividendCalcAndPostViewModel
    {
        public string colliery_code { get; set; }
        public string cat { get; set; }
        public string branch { get; set; }
        public string book_no { get; set; }
        public string fr_dt { get; set; }
        public string to_dt { get; set; }
        public string post_dt { get; set; }
        public string inst { get; set; }
        public string opsh_cap { get; set; }
        public string clsh_cap { get; set; }
        public string div_pay { get; set; }
        public IEnumerable<SelectListItem> BranchDesc { get; set; }
    }
}