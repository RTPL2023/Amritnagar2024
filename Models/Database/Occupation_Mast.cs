using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Amritnagar.Includes;


namespace Amritnagar.Models.Database
{
    public class Occupation_Mast
    {
        SQLConfig config = new SQLConfig();
        public string occup_id { get; set; }
        public string occup_name { get; set; }

        public string CheckAndSaveOccupation(Occupation_Mast om)
        {
            string sql = "Select * from OCCUP_MAST where Occup_Id='" + om.occup_id + "'";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                config.Update("OCCUP_MAST", new Dictionary<String, object>()
                    {
                    { "Occup_Name",   om.occup_name },
                }, new Dictionary<string, object>()
                {
                    { "Occup_Id",     om.occup_id },
                });
            }
            else
            {
                config.Insert("OCCUP_MAST", new Dictionary<string, object>()
                {
                    { "Occup_Id",     om.occup_id },
                    { "Occup_Name",   om.occup_name },
                });
            }
            string msg = "Saved Successfully";
            return (msg);
        }
        public List<Occupation_Mast> getAllOccupationList()
        {
            string sql = "Select * from OCCUP_MAST order by OCCUP_ID";
            config.singleResult(sql);
            List<Occupation_Mast> oml = new List<Occupation_Mast>();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    Occupation_Mast om = new Occupation_Mast();
                    om.occup_id = Convert.ToString(dr["Occup_Id"]);
                    om.occup_name = Convert.ToString(dr["Occup_Name"]);
                    oml.Add(om);
                }
            }
            return oml;
        }
        public void Deleteoccupation(string occup_id)
        {
            Relation_Mast rm = new Relation_Mast();
            string sql = "Delete from OCCUP_MAST where OCCUP_ID= '" + occup_id + "'";
            config.Execute_Query(sql);
        }
        public List<Occupation_Mast> getOccupationMast()
        {
            string sql = "Select * from  OCCUP_MAST";
            config.singleResult(sql);
            List<Occupation_Mast> oml = new List<Occupation_Mast>();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    Occupation_Mast om = new Occupation_Mast();
                    om.occup_id = Convert.ToString(dr["OCCUP_ID"]);
                    om.occup_name = Convert.ToString(dr["OCCUP_NAME"]);
                    oml.Add(om);
                }
            }
            return oml;
        }
        public String getoccupationnamebyid(String occupationid)
        {
            string BrName = string.Empty;
            string sql = "select * from OCCUP_MAST where Occup_Id = '"+ occupationid + "'";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    occup_name = Convert.ToString(dr["Occup_Name"]);
                }
            }
            return occup_name;
        }

    }
}