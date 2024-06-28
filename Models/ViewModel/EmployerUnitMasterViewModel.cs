using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Amritnagar.Models.ViewModel
{
    public class EmployerUnitMasterViewModel
    {
        public string emp_cd { get; set; }
        public string emp_branch { get; set; }
        public string emp_branch_name { get; set; }
        public string address { get; set; }
        public string address_2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string dist { get; set; }
        public string pin { get; set; }
        public string phn_no { get; set; }
        public string telex_no { get; set; }
        public bool sal_ded { get; set; }
        public string msg { get; set; }

        public IEnumerable<SelectListItem> EmpDesc { get; set; }
        

    }
}