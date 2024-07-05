using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Amritnagar.Models.ViewModel
{
    public class Tf_Gf_LedgerViewModel
    {
        public string branch { get; set; }
        public string achd { get; set; }
        public string mem_no { get; set; }
        public string mem_name { get; set; }
        public string mem_date { get; set; }
        public string vch_date { get; set; }
        public string vch_type { get; set; }
        public string vch_no { get; set; }
        public string srl { get; set; }
        public string drcr { get; set; }
        public string prin_amt { get; set; }
        public string int_amt { get; set; }
        public string ins_md { get; set; }
        public string vch_achd { get; set; }
        public string tableelement { get; set; }
        public IEnumerable<SelectListItem> BranchDesc { get; set; }
        public IEnumerable<SelectListItem> achdDesc { get; set; }
        public string con_vch_date { get; set; }
        public string con_vch_type { get; set; }
        public string con_vch_no { get; set; }
        public string con_srl { get; set; }
        public string con_drcr { get; set; }
    }
}