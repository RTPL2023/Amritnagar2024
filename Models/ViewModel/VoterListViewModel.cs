using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Amritnagar.Models.ViewModel
{
    public class VoterListViewModel
    {
        public string book_no { get; set; }
        public string unit { get; set; }
        public string tableelement { get; set; }
        public bool dist_list { get; set; }
        public IEnumerable<SelectListItem> EmpBranchDesc { get; set; }
    }
}