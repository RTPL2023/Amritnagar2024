using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Amritnagar.Models.ViewModel
{
    public class UsersViewModel
    {
        
        public string UserID { get; set; }

        
        public string UserName { get; set; }

        
        public string Password { get; set; }

        
        public string ConfirmPassword { get; set; }
        public int Blocked { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
       
        public string Role { get; set; }
        public string msg { get; set; }

        public string MsgBox { get; set; }
        public string Disc { get; set; }
        public string BranchId { get; set; }
        public string BranchName { get; set; }
        public IEnumerable<SelectListItem> BranchDesc { get; set; }

    }
}