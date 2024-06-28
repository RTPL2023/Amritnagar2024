using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Amritnagar.Includes;

namespace Amritnagar.Models.Database
{
    public class Loan_Surity
    {
        SQLConfig config = new SQLConfig();
        public string branch_id { get; set; }
        public string ac_hd { get; set; }
        public string employee_id { get; set; }
        public string smember_id { get; set; }
        public string surity_name { get; set; }
        public string semployee_id { get; set; }
    }
}