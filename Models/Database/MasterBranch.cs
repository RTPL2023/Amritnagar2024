using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Amritnagar.Includes;

namespace Amritnagar.Models.Database
{
    public class MasterBranch
    {

        SQLConfig config = new SQLConfig();
        public String BranchId { get; set; }
        public String id { get; set; }
        public int Id { get; set; }
        public string DL_No { get; set; }
        public string PAN_No { get; set; }
        public String BranchSubId { get; set; }
        public String BranchName { get; set; }
        public String Address { get; set; }
        public String ContactNo { get; set; }
        public String EmailId { get; set; }
        public String Created_by { get; set; }
        public String Create_date { get; set; }
        public String Create_Time { get; set; }
        public String Modified_by { get; set; }
        public String Modified_Date { get; set; }
        public String Modified_Time { get; set; }
        public String Device_name { get; set; }
        public String M_Device_name { get; set; } 
        public String Country_Id { get; set; }
        public String Br_Country_Name { get; set; }
        public String State_Id { get; set; }
        public String Br_State_Name { get; set; }
        public String District_Id { get; set; }
        public String Br_District_Name { get; set; }
        public String City_Id { get; set; }
        public String Br_City_Name { get; set; }
        public String Gst_In { get; set; }
        public String Pin_code { get; set; }
        public int codenum { get; set; }
        public string msg { get; set; }
        public int acode { get; set; }
        public bool checkdata { get; set; }
        public string CityName { get; set; }
        public string DistrictName { get; set; }
        public string StateName { get; set; }
        public string CountryName { get; set; }
        public string Inv_Code { get; set; }
        public string Organisation_Name { get; set; }
        public string Office_1 { get; set; }
        public string office_2 { get; set; }
        public string Mob_1 { get; set; }
        public string Mob_2 { get; set; }
        public string Website { get; set; }


      

        public List<MasterBranch> getBranchMast()
        {
            string sql = "Select * from  Branch_Mast";
            config.singleResult(sql);

            List<MasterBranch> mbl = new List<MasterBranch>();
           
                    if (config.dt.Rows.Count > 0)
                       {
                        foreach (DataRow dr in config.dt.Rows)
                        {
                            MasterBranch mb = new MasterBranch();
                            mb.id = Convert.ToString(dr["branch_id"]);
                            mb.BranchName = Convert.ToString(dr["Branch_Name"]);
                            mbl.Add(mb);
                        }
                    }
            return mbl;
        }
        public String getBranch(String Branchid)
        {
            string BrName = string.Empty;
            string sql = "SELECT * from Branch_Mast WHERE Branch_Id = '" + Branchid + "' ";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    BrName = Convert.ToString(dr["Branch_Name"]);
                }
            }
            return BrName;  
        }

      
    }
}