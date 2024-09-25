using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Amritnagar.Models.ViewModel
{
    public class GeneralDedscheduleViewModel
    {
        public string branch { get; set; }
        public string sending_dt { get; set; }
        public string unit { get; set; }
        public string year { get; set; }
        public string month_code { get; set; }
        public string code { get; set; }
        public string mem_cat { get; set; }
        public string tableelement { get; set; }
        public string tableelement1 { get; set; }
        public IEnumerable<SelectListItem> CategoryDesc { get; set; }
        public IEnumerable<SelectListItem> EmpBranchDesc { get; set; }
        public IEnumerable<SelectListItem> BranchDesc { get; set; }
    }
}