using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Amritnagar.Includes;

namespace Amritnagar.Models.Database
{
    public class AuthDbUtility
    {
        SQLConfig config = new SQLConfig();

        String sql;
        public int getLoggin(string userid, string _pwd,string branch)
        {
            int RetValue = 0;
            try
            {
                string encpwd = AmritnagarUtility.Encryptdata(_pwd);           
                sql = "SELECT * FROM users WHERE USER_ID = '" + userid + "'  and PASSWORD = '" + encpwd + "' ";           
                LoginDetails ld = new LoginDetails();           
                config.singleResult(sql);
           
                if (config.dt.Rows.Count > 0)
                {
                    DataRow dr = (DataRow)config.dt.Rows[0];
                    if (Convert.ToInt32(dr["blocked"]) == 1)
                    {
                        RetValue = 2;
                    }
                    else
                    {
                        RetValue = 1;
                    }
                }
                else
                {
                    RetValue = 3;
                }
            }
            catch
            {
                RetValue = -1;
            }                
            return Convert.ToInt32(RetValue);
        }

        public void setlogout(string _UserName)
        {
            //sql = "SELECT * FROM login_details WHERE USER_ID = '" + _UserName + "' order by login_datetime";
            //config.singleResult(sql);
            //logindetails ld = new logindetails();
            //if (config.dt.Rows.Count > 0)
            //{
            //    DataRow dr = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
            //   ld.login_datetime = Convert.ToDateTime(dr["login_datetime"]);

            //    config.Update("login_details", new Dictionary<string, object>()
            //    {
               
            //    { "logout_datetime", DateTime.Now},
            //    { "logged_in", 0 }
            //    }, new Dictionary<string, object>()
            //      {
            //      { "USER_ID", _UserName},
            //      { "login_datetime", ld.login_datetime}

            //    });
            //}
        }

        //public int updatePassword(string _UserName, string _pwd)
        //{
        //    string encpwd = FinProUility.Encryptdata(_pwd);

        //    string dcrpwd = FinProUility.Decryptdata(encpwd);

        //    //     sql = "SELECT * from users WHERE user_id = '" + _UserName + "' and Password = '" + _pwd + "'";
        //    sql = "update user_mast set user_password='" + encpwd + "' WHERE user_id = '" + _UserName + "'";

        //    int RetValue = 0;

        //    try
        //    {
        //        config.Execute_Query(sql);
        //        //if (config.dt.Rows.Count > 0)
        //        //{
        //        //    RetValue = 1;

        //        //}



        //    }
        //    catch (Exception ex)
        //    {

        //        //  objDB.LogErrorsInDB(ex.Message + ",  StackTrace - " + ex.StackTrace);

        //    }
        //    //finally
        //    //{
        //    //    objDB.DisConnect();
        //    //}
        //    return Convert.ToInt32(RetValue);
        //}


        public String getRole(String _UserName)
        {
            String _role=String.Empty ;
            sql = "SELECT USER_ROLE from USERS WHERE user_id = '" + _UserName + "' ";        
            try
            {
                config.singleResult(sql);
                if (config.dt.Rows.Count > 0)
                {
                    _role= config.dt.Rows[0]["USER_ROLE"].ToString() ;
                    //_role = '"' + rl + '"';                   
                }
                return _role;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;               
            }
           // return _role;
        }

        public String getBranch(String UserId)
        {
            String branch = String.Empty;
            sql = "SELECT * from USERS WHERE user_id = '" + UserId + "' ";
            try
            {
                config.singleResult(sql);
                if (config.dt.Rows.Count > 0)
                {
                    DataRow dr = (DataRow)config.dt.Rows[config.dt.Rows.Count - 1];
                    branch = Convert.ToString(dr["Allocated_BranchId"]);
                }
                return branch;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
            // return _role;
        }
    }
}