using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Amritnagar.Models.Database
{
    public class LoginDetails
    {
        public String user_id { get; set; }
        public int Logged_In { get; set; }
        public String Login_date { get; set; }
        public String login_time { get; set; }
        public String Logout_date { get; set; }
        public String logout_time { get; set; }
        
    }
}