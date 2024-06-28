using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Amritnagar.Includes;


namespace Amritnagar.Models.Database
{
    public class Relation_Mast
    {
        SQLConfig config = new SQLConfig();
        public string reln_id { get; set; }
        public string reln_name { get; set; }

        public string CheckAndSaveRelationship(Relation_Mast rm)
        {
            string sql = "Select * from RELN_MAST where Reln_Id='" + rm.reln_id + "'";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                config.Update("RELN_MAST", new Dictionary<String, object>()
                    {
                    { "Reln_Name",   rm.reln_name },
                }, new Dictionary<string, object>()
                {
                    { "Reln_Id",     rm.reln_id },
                });
            }
            else
            {
                config.Insert("RELN_MAST", new Dictionary<string, object>()
                {
                    { "Reln_Id",     rm.reln_id },
                    { "Reln_Name",   rm.reln_name },
                });
            }
            string msg = "Saved Successfully";
            return (msg);
        }
        public List<Relation_Mast> getAllRelationshipList()
        {
            string sql = "Select * from RELN_MAST order by Reln_id";
            config.singleResult(sql);
            List<Relation_Mast> rml = new List<Relation_Mast>();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    Relation_Mast rm = new Relation_Mast();
                    rm.reln_id = Convert.ToString(dr["Reln_Id"]);
                    rm.reln_name = Convert.ToString(dr["Reln_Name"]);
                    rml.Add(rm);
                }
            }
            return rml;
        }
        public void Deleterelationship(string reln_id)
        {
            Relation_Mast rm = new Relation_Mast();
            string sql = "Delete from RELN_MAST where Reln_Id= '" + reln_id + "'";
            config.Execute_Query(sql);
        }
        public List<Relation_Mast> getRelationMast()
        {
            string sql = "Select * from  RELN_MAST";
            config.singleResult(sql);
            List<Relation_Mast> oml = new List<Relation_Mast>();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    Relation_Mast rm = new Relation_Mast();
                    rm.reln_id = Convert.ToString(dr["RELN_ID"]);
                    rm.reln_name = Convert.ToString(dr["RELN_NAME"]);
                    oml.Add(rm);
                }
            }
            return oml;
        }
        public String getrelationnamebyid(String relationid)
        {
            string BrName = string.Empty;
            string sql = "Select * from RELN_MAST where Reln_Id = '" + relationid + "'";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    reln_name = Convert.ToString(dr["RELN_NAME"]);
                }
            }
            return reln_name;
        }
    }
}