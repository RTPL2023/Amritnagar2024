﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Amritnagar.Models.ViewModel
{
    public class MemDepositeFundDetailListViewModel
    {
        public string branch { get; set; }
        public string gl_achd { get; set; }
        public string on_dt { get; set; }
        public string prnt_bal { get; set; }
        public string int_bal { get; set; }
        public string tot_bal { get; set; }
        public string tableelement { get; set; }
        public string brn_txtbox { get; set; }
        public string mem_no { get; set; }
        public string mem_name { get; set; }
        public string mem_name_txt { get; set; }
        public string mem_id { get; set; }
        public string table { get; set; }
        public IEnumerable<SelectListItem> BranchDesc { get; set; }
        public IEnumerable<SelectListItem> achddesc { get; set; }
    }
}