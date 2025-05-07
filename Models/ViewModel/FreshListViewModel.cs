using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Amritnagar.Models.ViewModel
{
    //public class PrintFreshListViewModel
    //{
    //    public List<FreshListViewModel> Row { get; set; }
    //}
    public class FreshListViewModel
    {      
        public string rec_dt { get; set; }
        public string book_no { get; set; }
        public string mem_no { get; set; }
        public string msg { get; set; }
        public string emp_branch { get; set; }
        public string fr_dt { get; set; }
        public string to_dt { get; set; }
        public string edit_age { get; set; }
        public string pan_no { get; set; }
        public string dob { get; set; }
        public string premium_yr { get; set; }
        public string contc_no { get; set; }
        public string email { get; set; }
        public string tableelement { get; set; }
        public string tableelement2 { get; set; }
        public string percnt { get; set; }
        public string mem_name { get; set; }
        public int srl { get; set; }
        //public decimal gf_int_amt { get; set; }
        //public decimal gf_prin_bal { get; set; }
        //public decimal tf_prin_bal { get; set; }
        //public decimal tf_int_amt { get; set; }
        //public decimal div_tr_amt { get; set; }
        //public decimal sh_bal { get; set; }
        //public decimal tot_int_amt { get; set; }
        public IEnumerable<SelectListItem> EmpBranchDesc { get; set; }
    }
}