using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amritnagar.Includes;
using System.Data;

namespace Amritnagar.Models.Database
{
    public class UserDBUtility
    {
        SQLConfig config = new SQLConfig();

        String sql;
        public List<Users> GetAllUsers()
        {
            //     sql = "SELECT * from users WHERE user_id = '" + _UserName + "' and Password = '" + _pwd + "'";
            sql = "SELECT * from users";

            int RetValue = 0;
            List<Users> usrs = new List<Users>();
            try
            {
                config.singleResult(sql);
                if (config.dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in config.dt.Rows)
                    {
                        Users u = new Users();
                        u.User_ID = Convert.ToString(dr["User_ID"]);
                        u.User_Name = Convert.ToString(dr["User_Name"]);
                        u.User_Role = Convert.ToString(dr["User_Role"]);
                        u.Email_ID= Convert.ToString(dr["Email_ID"]);
                        u.Allocated_BranchId = Convert.ToString(dr["Allocated_BranchId"]);
                        u.Mobile_number = Convert.ToString(dr["Mobile_number"]);
                        u.Blocked = !Convert.IsDBNull(dr["Blocked"]) ? Convert.ToInt32(dr["Blocked"]) : Convert.ToInt32("0"); 
                        sql = "SELECT * from branch_mast where branch_id='" + u.Allocated_BranchId + "'";
                        config.singleResult(sql);
                        if (config.dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr1 in config.dt.Rows)
                            {
                                u.branchName = Convert.ToString(dr1["Branch_name"]);
                            }
                        }
                        usrs.Add(u);
                    }

                }

                return usrs;

            }
            catch (Exception ex)
            {
                usrs = null;
                //  objDB.LogErrorsInDB(ex.Message + ",  StackTrace - " + ex.StackTrace);

            }

            return usrs;
        }
        public void AddUser(Users user)
        {
            config.Insert("Users", new Dictionary<string, object>()
                {
                    { "User_Id", user.User_ID },
                    { "User_Name", user.User_Name },
                    { "User_Role", user.User_Role },
                    { "Password", user.Password },
                    //{ "Disc", user.Disc },
                    { "Allocated_BranchId", user.Allocated_BranchId },
                    { "Blocked", 0 },
                    { "Mobile_Number", user.Mobile_number},
                    { "Email_Id", user.Email_ID},
                    { "Created_By", user.created_by },
                    { "Created_On", user.created_on},
                    { "create_time",user.create_time },
                    { "Device_Name", user.device_name},
                    { "Modified_By", user.modified_by},
                    { "Modified_On", user.modified_on},
                    { "Modify_time", user.modify_time},
                    { "M_Device_Name", user.m_device_name},

                });
            

        }          
        public Users getUserDetailsByUserId(String id)
        {
            string sql = "SELECT * FROM Users WHERE USER_ID = '" + id + "'";
            config.singleResult(sql);

            Users u = new Users();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {

                    u.User_ID = dr["USER_ID"].ToString();
                    u.User_Name = dr["USER_NAME"].ToString();
                    u.User_Role = dr["USER_ROLE"].ToString();
                    u.Allocated_BranchId = dr["Allocated_BranchId"].ToString();
                    u.Email_ID = dr["EMAIL_ID"].ToString();
                    u.Mobile_number = dr["Mobile_number"].ToString();
                    u.Password = dr["Password"].ToString();
                    //u.Disc= !Convert.IsDBNull(dr["Disc"]) ? Convert.ToInt32(dr["Disc"]) : Convert.ToInt32("0"); ;
                    u.Blocked = !Convert.IsDBNull(dr["Blocked"]) ? Convert.ToInt32(dr["Blocked"]) : Convert.ToInt32("0"); ;

                }

            }
            return u;

        }
        public int CheckUserId(String id)
        {
            string sql = "SELECT * FROM Users WHERE USER_ID = '" + id + "'";
            config.singleResult(sql);
            int a = 0;
            Users u = new Users();
            if (config.dt.Rows.Count > 0)
            {
                a = 1;
                return a;

            }
            else
            {
                return a;
            }
        }
        public Users CheckMobileNo(String MOB)
        {
            string sql = "SELECT * FROM Users ";
            config.singleResult(sql);
            
            Users u = new Users();
            u.tagMOB = 0;
            UserDBUtility udu = new UserDBUtility();
            if (config.dt.Rows.Count > 0)
            {
                string DBMobile = string.Empty;
                foreach (DataRow dr in config.dt.Rows)
                {
                    u.Mobile_number = Convert.ToString(dr["Mobile_number"]);
                    if (u.Mobile_number.Length > 10)
                    {
                        int length = u.Mobile_number.Length - 10;
                        DBMobile = u.Mobile_number.Substring(length);
                    }
                    else
                    {
                        DBMobile = u.Mobile_number;
                    }
                    if (DBMobile == MOB)
                    {
                        u.tagMOB = 1;
                        u.User_ID = dr["USER_ID"].ToString();
                        return u;
                    }
                    else
                    {
                        u.tagMOB = 0;
                        


                    }
                }



            }
            return u;
            
        }
        public Users CheckEmailId(String Email)
        {
            string sql = "SELECT * FROM Users";
            config.singleResult(sql);

            Users u = new Users();
            u.tagEmail = 0;
            UserDBUtility udu = new UserDBUtility();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    u.Email_ID = Convert.ToString(dr["Email_ID"]);
                    
                    if (u.Email_ID == Email)
                    {
                        u.tagEmail = 1;
                        u.User_ID = dr["USER_ID"].ToString();
                        return u;
                    }
                    else
                    {
                        u.tagEmail = 0;
                        


                    }
                }



            }
            return u;

        }
        public void UpdateUser(Users users)
        {
            config.Update("Users", new Dictionary<string, object>()
            {

                { "USER_NAME", users.User_Name },
                 { "USER_ROLE", users.User_Role }


            }, new Dictionary<string, object>()
            {
              { "USER_ID", users.User_ID}

            });
        }
        public int ResetPassword(string _UserName, string _pwd)
        {

            string encpwd = AmritnagarUtility.Encryptdata(_pwd);

            //   string dcrpwd = FinProUility.Decryptdata(encpwd);

            sql = "update users set user_password='" + encpwd + "' WHERE user_id = '" + _UserName + "'";

            int RetValue = 0;

            try
            {
                config.Execute_Query(sql);

                RetValue = 1;
            }
            catch (Exception ex)
            {
                RetValue = 0;
            }

            return Convert.ToInt32(RetValue);

        }
        public void EditUserdetailsByUserId(Users user)
        {
            config.Update("Users", new Dictionary<String, object>()
                {
                     
                    { "User_Name", user.User_Name },
                    { "User_Role", user.User_Role },
                    { "Password", user.Password },
                    //{ "Disc", user.Disc },
                     {"Allocated_BranchId", user.Allocated_BranchId },
                    { "Blocked", user.Blocked },
                    { "Mobile_Number", user.Mobile_number},
                    { "Email_Id", user.Email_ID},
                    { "Modified_By", user.modified_by},
                    { "Modified_On", user.modified_on},
                    { "Modify_time", user.modify_time},
                    { "M_Device_Name", user.m_device_name},
                   
            }, new Dictionary<string, object>()
                  {
                  { "User_Id", user.User_ID },

            });
        }
        public void BlockUserByUserId(Users user)
        {
            config.Update("Users", new Dictionary<String, object>()
                {                  
                    { "Blocked", user.Blocked },
                    { "Modified_By", user.modified_by},
                    { "Modified_On", user.modified_on},
                    { "Modify_time", user.modify_time},
                    { "M_Device_Name", user.m_device_name}

            }, new Dictionary<string, object>()
                  {
                  { "User_Id", user.User_ID },

            });
        }
    }
}
