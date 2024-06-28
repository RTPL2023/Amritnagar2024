using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Amritnagar.Models.ViewModel
{
    public class LoginViewModel
    {
        
        public string UserName { get; set; }
        public string userid { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }
        public string Branch { get; set; }
        public string BranchId { get; set; }
        public string BranchName { get; set; }
        public IEnumerable<SelectListItem> BranchDesc { get; set; }
        public string msg { get; set; }
    }
}