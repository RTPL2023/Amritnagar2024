using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Amritnagar.Includes;

namespace Amritnagar.Models.Database
{
    public class GF_Ledger
    {
        SQLConfig config = new SQLConfig();
        public string branch_id { get; set; }
        public string member_id { get; set; }
        public DateTime vch_date { get; set; }
        public string vch_no { get; set; }
        public decimal vch_srl { get; set; }
        public string vch_type { get; set; }
        public string vch_achd { get; set; }
        public string dr_cr { get; set; }
        public decimal prin_amount { get; set; }
        public decimal prin_bal { get; set; }
        public decimal int_amount { get; set; }
        public decimal int_bal { get; set; }
        public string ref_ac_hd { get; set; }
        public string ref_pacno { get; set; }
        public string ref_oth { get; set; }
        public string insert_mode { get; set; }
    }
}