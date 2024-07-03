using Amritnagar.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Amritnagar.Models.ViewModel
{
    public class DividendCalcAndPostViewModel
    {
        public string member_id { get; set; }
        public string forsave { get; set; }
        public string colliery_code { get; set; }
        public string ex_gen { get; set; }
        public string branch { get; set; }
        public string book_no { get; set; }
        public string fr_dt { get; set; }
        public string to_dt { get; set; }
        public string post_dt { get; set; }
        public string inst { get; set; }
        public string opsh_cap { get; set; }
        public string clsh_cap { get; set; }
        public string div_pay { get; set; }
        public string tableelement { get; set; }
        public string tableelement1 { get; set; }
        public IEnumerable<SelectListItem> BranchDesc { get; set; }
        public IEnumerable<SelectListItem> EmpBranchDesc { get; set; }
        public List<Member_Mast> mdmmlst = new List<Member_Mast>();
        public dynamic[,] int_array = new dynamic[3, 13];
        public String[] month_array = new string[12];
        public Int32 xmonths { get; set; }

    }
}