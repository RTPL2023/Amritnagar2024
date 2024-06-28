using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Amritnagar.Models.ViewModel
{
    public class ShareCapitalDetailListViewModel
    {
        public string branch_id { get; set; }
        public string on_dt { get; set; }
        public string gl_hd { get; set; }
        public string gr_total { get; set; }
        public string tableelement { get; set; }
        public IEnumerable<SelectListItem> BranchDesc { get; set; }
        public IEnumerable<SelectListItem> achddesc { get; set; }
    }
}