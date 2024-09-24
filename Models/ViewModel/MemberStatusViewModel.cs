using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Amritnagar.Models.ViewModel
{
    public class MemberStatusViewModel
    {
        public String BranchID { get; set; }
        public String Branch { get; set; }
        public String mem_no { get; set; }
        public String mem_name { get; set; }
        public String AcHd { get; set; }
        public String PfNo { get; set; }
        public String FrDt { get; set; }
        public String ToDt { get; set; }
        public String srl { get; set; }
        public String CertPrefix { get; set; }
        public String CertSrl { get; set; }
        public String CertDt { get; set; }
        public String CertDistinctive { get; set; }
        public String refresh { get; set; }
        public String generate { get; set; }
        public String save { get; set; }
        public string CR { get; set; }
        public string cont_no { get; set; }
        public string member_no { get; set; }
        public string lonee_name { get; set; }
        public string loanee_emp_id { get; set; }
        public string member_date { get; set; }
        public string ac_hd { get; set; }
        public string Branch_sh { get; set; }
        public string ln_Branch { get; set; }
        public string mem_no_sh { get; set; }
        public string mem_name_sh { get; set; }
        public IEnumerable<SelectListItem> TypeDesc { get; set; }
        public string mem_type { get; set; }
        public IEnumerable<SelectListItem> CategoryDesc { get; set; }
        public IEnumerable<SelectListItem> BranchDesc { get; set; }
        public string mem_category { get; set; }
        public string tableelement { get; set; }
        public string name { get; set; }
        public string share_cap_tot { get; set; }
        public string ShAcDesc { get; set; }
        public string Shbal { get; set; }
        public string ShAcHd { get; set; }
        public string ShTab { get; set; }
        public string msg { get; set; }
        public string mailing_address { get; set; }
        public string liabilities { get; set; }
        public string cap_ass { get; set; }
        public string nominee_name { get; set; }
        public string nominee_relation { get; set; }
        public string mail_hno { get; set; }
        public string mail_add1 { get; set; }
        public string mail_add2 { get; set; }
        public string mail_state { get; set; }
        public string mail_city { get; set; }
        public string tot_fund { get; set; }
        public string mail_dist { get; set; }
        public string mail_pin { get; set; }
        public string cust_id { get; set; }
        public string cust_name { get; set; }
        public string cast_id { get; set; }
        public string sex { get; set; }
        public string occup_id { get; set; }
        public string sig { get; set; }
        public string lti { get; set; }
        public string pan_no { get; set; }
        public string ac_no { get; set; }
        public string open_date { get; set; }
        public string chq_facility { get; set; }
        public string oprn_mode { get; set; }
        public string spl_status { get; set; }
        public string net_paybal { get; set; }
        public string prin_bal { get; set; }
        public string tot_time_deposit { get; set; }
        public string tot_Demand_deposit { get; set; }
        public string tot_Loan { get; set; }
        public string tot_Oth_Loan { get; set; }
        public string tot_liabilities { get; set; }
        public string tot_assets { get; set; }
        public string emp_id { get; set; }
        public string table { get; set; }
        public string ln_type { get; set; }

        public dynamic[,] int_array = new dynamic[3, 13];
    }
}