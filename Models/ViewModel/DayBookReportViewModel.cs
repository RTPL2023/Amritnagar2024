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
        public IEnumerable<SelectListItem> BranchDesc { get; set; }
    }
}