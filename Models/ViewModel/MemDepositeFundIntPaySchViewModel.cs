using Amritnagar.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Amritnagar.Models.ViewModel
{
    public class MemDepositeFundIntPaySchViewModel
    {
        public string colliery_code { get; set; }
        public string ex_gen { get; set; }
        public string branch { get; set; }
        public string ac_hd { get; set; }
        public string book_no { get; set; }
        public string fr_dt { get; set; }
        public string to_dt { get; set; }
        public string post_dt { get; set; }
        public string int_rate { get; set; }
        public string op_prn_bal { get; set; }
        public string close_prn_bal { get; set; }
        public string op_int_pay { get; set; }
        public string close_int_pay { get; set; } 
        public string op_tot_bal { get; set; }
        public string close_tot_bal { get; set; }
        public string add_int_pay { get; set; }
        public string net_int_pay { get; set; }
        public string net_tot_bal { get; set; }
        public string heading { get; set; }
        public string te { get; set; }
        public IEnumerable<SelectListItem> BranchDesc { get; set; }
        public IEnumerable<SelectListItem> achddesc { get; set; }
        public IEnumerable<SelectListItem> EmpBranchDesc { get; set; }

        public List<Member_Mast> mdmmlst = new List<Member_Mast>();
        public dynamic[,] int_array = new dynamic[3, 13];
        public String[] month_array = new string[12];
        public Int32 xmonths { get; set; }

        public string forsave { get; set; }


    }
}