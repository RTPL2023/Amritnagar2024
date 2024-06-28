using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Amritnagar.Includes;


namespace Amritnagar.Models.Database
{
    public class Users
    {
        SQLConfig config = new SQLConfig();
        public Int32 Id { get; set; }
        public Int32 Disc { get; set; }
        public string User_ID { get; set; }
        public string User_Name { get; set; }
        public string Password { get; set; }
        public string Allocated_BranchId { get; set; }
        public string User_Role { get; set; }
        public Int32 Blocked { get; set; }
        public string created_by { get; set; }
        public string created_on { get; set; }
        public string create_time { get; set; }
        public string device_name { get; set; }
        public string modified_by { get; set; }
        public string modified_on { get; set; }
        public string modify_time { get; set; }
        public string m_device_name { get; set; }
        public string Email_ID { get; set; }
        public string NewPassword { get; set; }
        public string Mobile_number { get; set; }
        public int tagMOB { get; set; }
        public int tagEmail { get; set; }
        public int tag { get; set; }
        public string branchName { get; set; }

        public Users changepassword(Users US)
        {
            string sql = "SELECT * FROM Users WHERE USER_ID = '" + US.User_ID + "' and USER_PASSWORD='" + US.Password + "'";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                config.Update("Users", new Dictionary<string, object>()
                {
                { "USER_PASSWORD", US.NewPassword },

                }, new Dictionary<string, object>()
                {

                { "USER_ID", US.User_ID}
                });
                US.tag = 1;
            }
            else
            {
                US.tag = 0;
            }

            return US;
        }
    }
}