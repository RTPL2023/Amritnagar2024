﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Amritnagar.Models.ViewModel
{
    public class SendingScheduleReportViewModel
    {
        public string sch_dt { get; set; }
        public string book_no { get; set; }
        public string emp_branch { get; set; }
        public string tableelement { get; set; }
        public IEnumerable<SelectListItem> EmpBranchDesc { get; set; }
    }
}