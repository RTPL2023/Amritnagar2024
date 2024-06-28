using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Amritnagar.Includes;

namespace Amritnagar.Models.Database
{
    public class MemCategory_Mast
    {
        SQLConfig config = new SQLConfig();
        public string mem_category { get; set; }
        public string category_desc { get; set; }


        public string CheckAndSaveMemberCategory(MemCategory_Mast mcm)
        {
            string sql = "Select * from MEMCATEGORY_MAST where MEM_CATEGORY='" + mcm.mem_category + "'";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                config.Update("MEMCATEGORY_MAST", new Dictionary<String, object>()
                    {
                    { "CATEGORY_DESC",   mcm.category_desc },
                }, new Dictionary<string, object>()
                {
                    { "MEM_CATEGORY",     mcm.mem_category },
                });
            }
            else
            {
                config.Insert("MEMCATEGORY_MAST", new Dictionary<string, object>()
                {
                    { "CATEGORY_DESC",     mcm.category_desc },
                    { "MEM_CATEGORY",      mcm.mem_category },
                });
            }
            string msg = "Saved Successfully";
            return (msg);
        }
        public List<MemCategory_Mast> getAllmemberCategoryList()
        {
            string sql = "Select * from MEMCATEGORY_MAST order by MEM_CATEGORY";
            config.singleResult(sql);
            List<MemCategory_Mast> mcml = new List<MemCategory_Mast>();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    MemCategory_Mast mcm = new MemCategory_Mast();
                    mcm.mem_category = Convert.ToString(dr["Mem_Category"]);
                    mcm.category_desc = Convert.ToString(dr["Category_Desc"]);
                    mcml.Add(mcm);
                }
            }
            return mcml;
        }
        public void DeleteMembercategory(string mem_category)
        {
            MemCategory_Mast cmm = new MemCategory_Mast();
            string sql = "Delete from MEMCATEGORY_MAST where MEM_CATEGORY= '" + mem_category + "'";
            config.Execute_Query(sql);
        }
        public List<MemCategory_Mast> getCategoryMast()
        {
            string sql = "Select * from  MEMCATEGORY_MAST";
            config.singleResult(sql);
            List<MemCategory_Mast> cml = new List<MemCategory_Mast>();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    MemCategory_Mast cm = new MemCategory_Mast();
                    cm.mem_category = Convert.ToString(dr["MEM_CATEGORY"]);
                    cm.category_desc = Convert.ToString(dr["CATEGORY_DESC"]);
                    cml.Add(cm);
                }
            }
            return cml;
        }

    }
}