using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Amritnagar.Models.ViewModel
{
    public class VoucherEntryViewModel
    {
        public string branch { get; set; }
        public string vch_date { get; set; }
        public string vch_no { get; set; }
        public string vch_type { get; set; }
        public string debt_amt { get; set; }
        public string crdt_amt { get; set; }
        public string net_amt { get; set; }
        public string tot_chq_amt { get; set; }
        public string srl { get; set; }
        public string drcr { get; set; }
        public string vch_achd { get; set; }
        public string vchpacno { get; set; }
        public string particular { get; set; }
        public string amount { get; set; }
        public string ref_ac_hd { get; set; }
        public string refacno { get; set; }
        public string refParticular { get; set; }
        public string txtvch_No { get; set; }
        public string del_vch_no { get; set; }
        public string tableElement { get; set; }
        public string clos_flag { get; set; }

        public IEnumerable<SelectListItem> BranchDesc { get; set; }
    }
}