using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Amritnagar.Includes;

namespace Amritnagar.Models.Database
{
    public class Caste_Mast
    {
        SQLConfig config = new SQLConfig();
        public string caste_id { get; set; }
        public string caste_name { get; set; }

        public string CheckAndSaveCaste(Caste_Mast cm)
        {
            string sql = "Select * from CASTE_MAST where CASTE_ID='" + cm.caste_id + "'";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                config.Update("CASTE_MAST", new Dictionary<String, object>()
                    {
                    { "Caste_Name",   cm.caste_name },
                }, new Dictionary<string, object>()
                {
                    { "Caste_Id",     cm.caste_id },
                });
            }
            else
            {
                config.Insert("CASTE_MAST", new Dictionary<string, object>()
                {
                    { "Caste_Id",     cm.caste_id },
                    { "Caste_Name",   cm.caste_name },
                });
            }
            string msg = "Saved Successfully";
            return (msg);
        }
        public List<Caste_Mast> getAllcasteList()
        {
            string sql = "Select * from CASTE_MAST order by CASTE_ID";
            config.singleResult(sql);
            List<Caste_Mast> cml = new List<Caste_Mast>();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    Caste_Mast cm = new Caste_Mast();
                    cm.caste_id = Convert.ToString(dr["Caste_Id"]);
                    cm.caste_name = Convert.ToString(dr["Caste_Name"]);
                    cml.Add(cm);
                }
            }
            return cml;
        }
        public void Deletecaste(string caste_id)
        {
            Caste_Mast cm = new Caste_Mast();
            string sql = "Delete from CASTE_MAST where CASTE_ID= '" + caste_id + "'";
            config.Execute_Query(sql);
        }
        public List<Caste_Mast> getCastMast()
        {
            string sql = "Select * from  CASTE_MAST";
            config.singleResult(sql);
            List<Caste_Mast> cml = new List<Caste_Mast>();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    Caste_Mast cm = new Caste_Mast();
                    cm.caste_id = Convert.ToString(dr["CASTE_ID"]);
                    cm.caste_name = Convert.ToString(dr["CASTE_NAME"]);
                    cml.Add(cm);
                }
            }
            return cml;
        }
        public String getCastenamebyid(String casteid)
        {
            string BrName = string.Empty;
            string sql = "Select * from CASTE_MAST where CASTE_ID = '" + casteid + "'";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    caste_name = Convert.ToString(dr["CASTE_NAME"]);
                }
            }
            return caste_name;
        }
    }
}