using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Amritnagar.Includes;

namespace Amritnagar.Models.Database
{
    public class Designation_Mast
    {
        SQLConfig config = new SQLConfig();
        
        public int desig_cd { get; set; }
        public string desig_desc { get; set; }
        public bool checkdata { get; set; }
      
        public string CheckAndSaveDesignation(Designation_Mast dm)
        {
            string sql = "Select * from DESIG_MAST where Desig_CD='" + dm.desig_cd + "'";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                config.Update("DESIG_MAST", new Dictionary<String, object>()
                    {                   
                    { "Desig_Desc",   dm.desig_desc },
                }, new Dictionary<string, object>()
                {
                    { "Desig_Cd",     dm.desig_cd },
                });
            }
            else
            {
                config.Insert("DESIG_MAST", new Dictionary<string, object>()
                {                   
                    { "Desig_Cd",     dm.desig_cd },
                    { "Desig_Desc",   dm.desig_desc },
                });
            }
            string msg = "Saved Successfully";
            return (msg);
        }
        public List<Designation_Mast> getAllDesignationList()
        {
            string sql = "Select * from DESIG_MAST order by Desig_Cd";
            config.singleResult(sql);
            List<Designation_Mast> dml = new List<Designation_Mast>();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    Designation_Mast dm = new Designation_Mast();
                    dm.desig_cd = Convert.ToInt32(dr["Desig_Cd"]);
                    dm.desig_desc = Convert.ToString(dr["Desig_Desc"]);
                    dml.Add(dm);
                }
            }
            return dml;
        }
        public void Deletedesignation(string desig_cd)
        {
            Department_Mast dm = new Department_Mast();
            string sql = "Delete from DESIG_MAST where Desig_Cd= '" + desig_cd + "'";
            config.Execute_Query(sql);
        }
        public List<Designation_Mast> getDesignationMast()
        {
            string sql = "Select * from  DESIG_MAST";
            config.singleResult(sql);
            List<Designation_Mast> dml = new List<Designation_Mast>();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    Designation_Mast dm = new Designation_Mast();
                    dm.desig_cd = Convert.ToInt32(dr["DESIG_CD"]);
                    dm.desig_desc = Convert.ToString(dr["DESIG_DESC"]);
                    dml.Add(dm);
                }
            }
            return dml;
        }

        public String getdesignationname(String desigid)
        {
            string BrName = string.Empty;
            string sql = "Select * from DESIG_MAST where DESIG_CD = '" + desigid + "'";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    desig_desc = Convert.ToString(dr["DESIG_DESC"]);
                }
            }
            return desig_desc;
        }
    }
}