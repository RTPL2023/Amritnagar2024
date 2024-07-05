using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Amritnagar.Models.ViewModel
{
    public class MemberDetailListViewModel
    {
        public string book_no { get; set; }
        public string unit { get; set; }
        public string on_date { get; set; }
        public string total_mem { get; set; }
        public string bank_code { get; set; }
        public string bank_name { get; set; }
        public string post_date { get; set; }
        public string text1 { get; set; }
        public string text2 { get; set; }
        public string shtot { get; set; }
        public string to_gf { get; set; }
        public string tf { get; set; }
        public string to_itf { get; set; }
        public string to_igf { get; set; }
        public string ln_prncl_16_5 { get; set; }
        public string ln_prncl_14_5 { get; set; }
        public string ln_prncl_13 { get; set; }
        public string ln_prncl_10 { get; set; }
        public string ln_int_16_5 { get; set; }
        public string ln_int_14_5 { get; set; }
        public string ln_int_13 { get; set; }
        public string ln_int_10 { get; set; }
        public string ln_prncl_dll { get; set; }
        public string ln_prncl_14 { get; set; }
        public string ln_prncl_12_5 { get; set; }
        public string ln_prncl_12_75 { get; set; }
        public string ln_int_dll { get; set; }
        public string ln_int_14 { get; set; }
        public string ln_int_12_5 { get; set; }
        public string ln_int_12_75 { get; set; }
        public string ln_prncl_11_5 { get; set; }
        public string ln_prncl_9 { get; set; }
        public string ln_prncl_13_5 { get; set; }
        public string ln_prncl_13_75 { get; set; }
        public string int_11_5 { get; set; }
        public string int_9 { get; set; }
        public string int_13_5 { get; set; }
        public string int_13_75 { get; set; }
        public string tableelement { get; set; }
        public IEnumerable<SelectListItem> EmpBranchDesc { get; set; }
    }
}