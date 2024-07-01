using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Amritnagar.Models.ViewModel
{
    public class LoanTransfCreditEntryViewModel
    {
        public string branch { get; set; }
        public string date { get; set; }
        public string shift { get; set; }
        public string counter { get; set; }
        public string achd { get; set; }
        public string vch_no { get; set; }
        public string emp_id { get; set; }
        public string ref_achd { get; set; }
        public string ref_acno { get; set; }
        public string name_ref { get; set; }
        public string amt { get; set; }
        public string achd_paid { get; set; }
        public string achd_paid1 { get; set; }
        public string achd_amt { get; set; }
        public string achd_amt1 { get; set; }
        public string achd_bal { get; set; }
        public string achd_bal1 { get; set; }
        public string name { get; set; }
        public string loanee_name { get; set; }
        public string int_rate { get; set; }
        public string loan_dt { get; set; }
        public string loan_amt { get; set; }
        public string instl { get; set; }
        public string inst_prn { get; set; }
        public string last_trdt { get; set; }
        public string loanee_cat { get; set; }
        public string od_prin { get; set; }
        public string od_inst { get; set; }
        public string od_ad_int { get; set; }

        public string rec_lastpay_prin { get; set; }
        public string rec_lastpay_int { get; set; }
        public string rec_lastpay_ad_int { get; set; }
        public string rec_lastpay_inchrg { get; set; }
        public string rec_lastpay_grtot { get; set; }

        public string rec_regdt_prin { get; set; }
        public string rec_regdt_int { get; set; }
        public string rec_regdt_ad_int { get; set; }
        public string rec_regdt_inchrg { get; set; }
        public string rec_regdt_grtot { get; set; }

        public string rec_amtcl_prin { get; set; }
        public string rec_amtcl_int { get; set; }
        public string rec_amtcl_ad_int { get; set; }
        public string rec_amtcl_inchrg { get; set; }
        public string rec_amtcl_grtot { get; set; }
        public IEnumerable<SelectListItem> BranchDesc { get; set; }
        public IEnumerable<SelectListItem> TypeDesc { get; set; }
    }
}