using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Amritnagar.Models.ViewModel
{
    public class MemberOpeningandClosingRegViewModel
    {
        public string branch_id { get; set; }
        public string mem_type { get; set; }
        public string fr_dt { get; set; }
        public string to_dt { get; set; }
        public string emp_id { get; set; }
        public string mem_id { get; set; }
        public string mem_dt { get; set; }
        public string mem_cat { get; set; }
        public string op_shcap { get; set; }
        public string op_tfcap { get; set; } 
        public string cl_shcap { get; set; } 
        public string cl_tfcap { get; set; } 
        public string searchtype { get; set; }
        public string tableelement { get; set; }
        public string tot_xtcltf { get; set; }
        public string tot_xtclsh { get; set; }
        public string tot_xtopsh { get; set; }
        public string tot_xtoptf { get; set; }
        public string tot_xtopprin { get; set; }
        public string tot_cl_loan_amt { get; set; }
        public string tot_cl_prin_amt { get; set; }
        public string ac_hd { get; set; }
        public IEnumerable<SelectListItem> BranchDesc { get; set; }
        public IEnumerable<SelectListItem> TypeDesc { get; set; }
        public IEnumerable<SelectListItem> lntypedesc { get; set; }

    }
}