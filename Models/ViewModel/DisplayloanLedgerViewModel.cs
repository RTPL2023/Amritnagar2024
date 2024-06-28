using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Amritnagar.Models.ViewModel
{
    public class DisplayloanLedgerViewModel
    {
        public string branch_id { get; set; }
        public string loan_type { get; set; }
        public string mem_id { get; set; }
        public string emp_id { get; set; }
        public string org_emp { get; set; }
        public string unit { get; set; }
        public string book_no { get; set; }
        public string dept { get; set; }
        public string deg { get; set; }
        public string service_sts { get; set; }
        public string join_dt { get; set; }
        public string ret_dt { get; set; }
        public string mail_h_no { get; set; }
        public string mail_add1 { get; set; }
        public string mail_add2 { get; set; }
        public string mail_city { get; set; }
        public string mail_dist { get; set; }
        public string mail_state { get; set; }
        public string mail_pin { get; set; }
        public string chq_no { get; set; }
        public string chq_dt { get; set; }
        public string chq_bank_branch_ref { get; set; }
        public string sanc_dt { get; set; }
        public string loan_amt { get; set; }
        public string inst { get; set; }
        public string int_rate { get; set; }
        public string inst_amt { get; set; }
        public string loan_purpose { get; set; }
        public string fr_dt { get; set; }
        public string to_dt { get; set; }
        public string msg { get; set; }
        public string tableelement { get; set; }
        public IEnumerable<SelectListItem> BranchDesc { get; set; }
        public IEnumerable<SelectListItem> lntypedesc { get; set; }
        public IEnumerable<SelectListItem> lnpurposedesc { get; set; }
        public IEnumerable<SelectListItem> DesignationDesc { get; set; }
        public IEnumerable<SelectListItem> ServiceDesc { get; set; }
        public IEnumerable<SelectListItem> DepartmentDesc { get; set; }
        public IEnumerable<SelectListItem> EmpBranchDesc { get; set; }
        public IEnumerable<SelectListItem> EmpDesc { get; set; }

    }
}