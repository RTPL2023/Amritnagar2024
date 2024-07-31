﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Amritnagar.Models.ViewModel
{
    public class DayBookReportViewModel
    {
        public string branch { get; set; }
        public string daybook_dt { get; set; }
        public string ac_hddesc { get; set; }
        public string ac_hd { get; set; }
        public string label1 { get; set; }
        public string label2 { get; set; }
        public string label3 { get; set; }
        public string label4 { get; set; }
        public string label5{ get; set; }
        public string label6 { get; set; }
        public string tableele { get; set; }
        public IEnumerable<SelectListItem> BranchDesc { get; set; }
    }
}