using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Amritnagar.Includes;

namespace Amritnagar.Models.Database
{
    public class Religion_Mast
    {
        SQLConfig config = new SQLConfig();
        public string relgn_id { get; set; }
        public string relgn_name { get; set; }

        public string CheckAndSaveReligion(Religion_Mast rm)
        {
            string sql = "Select * from RELIGION_MAST where RELGN_ID='" + rm.relgn_id + "'";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                config.Update("RELIGION_MAST", new Dictionary<String, object>()
                    {
                    { "RELGN_NAME",   rm.relgn_name },
                }, new Dictionary<string, object>()
                {
                    { "RELGN_ID",     rm.relgn_id },
                });
            }
            else
            {
                config.Insert("RELIGION_MAST", new Dictionary<string, object>()
                {
                    { "RELGN_ID",     rm.relgn_id },
                    { "RELGN_NAME",   rm.relgn_name },
                });
            }
            string msg = "Saved Successfully";
            return (msg);
        }
        public List<Religion_Mast> getAllReligionList()
        {
            string sql = "Select * from RELIGION_MAST order by RELGN_ID";
            config.singleResult(sql);
            List<Religion_Mast> rml = new List<Religion_Mast>();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    Religion_Mast rm = new Religion_Mast();
                    rm.relgn_id = Convert.ToString(dr["Relgn_Id"]);
                    rm.relgn_name = Convert.ToString(dr["Relgn_Name"]);
                    rml.Add(rm);
                }
            }
            return rml;
        }
        public void DeleteReligion(string relgn_id)
        {
            Religion_Mast rm = new Religion_Mast();
            string sql = "Delete from RELIGION_MAST where RELGN_ID= '" + relgn_id + "'";
            config.Execute_Query(sql);
        }
        public List<Religion_Mast> getReligionMast()
        {
            string sql = "Select * from  RELIGION_MAST";
            config.singleResult(sql);
            List<Religion_Mast> rml = new List<Religion_Mast>();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    Religion_Mast rm = new Religion_Mast();
                    rm.relgn_id = Convert.ToString(dr["RELGN_ID"]);
                    rm.relgn_name = Convert.ToString(dr["RELGN_NAME"]);
                    rml.Add(rm);
                }
            }
            return rml;
        }
    }
}