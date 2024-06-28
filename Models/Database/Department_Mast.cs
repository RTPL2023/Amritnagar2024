using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Amritnagar.Includes;

namespace Amritnagar.Models.Database
{
    public class Department_Mast
    {
        SQLConfig config = new SQLConfig();
        public string employer_cd { get; set; }
        public string dept_cd { get; set; }
        public string dept_desc { get; set; }

        public string CheckAndSaveDepartment(Department_Mast dm)
        {
            string sql = "Select * from DEPT_MAST where Dept_CD='" + dm.dept_cd + "'";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                config.Update("DEPT_MAST", new Dictionary<String, object>()
                    {
                    { "Employer_CD", dm.employer_cd },
                    { "Dept_Desc",   dm.dept_desc },                                       
                }, new Dictionary<string, object>()
                {
                    { "Dept_CD",     dm.dept_cd },
                });
            }
            else
            {
                config.Insert("DEPT_MAST", new Dictionary<string, object>()
                {
                    { "Employer_CD", dm.employer_cd },
                    { "Dept_CD",     dm.dept_cd },
                    { "Dept_Desc",   dm.dept_desc },
                });
            }              
            string msg = "Saved Successfully";
            return (msg);
        }
        public List<Department_Mast> getAllDepartmentList()
        {
            string sql = "Select * from DEPT_MAST order by Dept_Cd";
            config.singleResult(sql);
            List<Department_Mast> dml = new List<Department_Mast>();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    Department_Mast dm = new Department_Mast();                  
                    dm.dept_cd = Convert.ToString(dr["Dept_Cd"]);
                    dm.dept_desc = Convert.ToString(dr["Dept_Desc"]);                  
                    dml.Add(dm);
                }
            }
            return dml;
        }
        public void DeleteDepartment(string dept_cd)
        {
            Department_Mast dm = new Department_Mast();
            string sql = "Delete from DEPT_MAST where Dept_Cd= '"+ dept_cd + "'";
            config.Execute_Query(sql);
        }
        public List<Department_Mast> getDepartmentMast()
        {
            string sql = "Select * from  DEPT_MAST";
            config.singleResult(sql);
            List<Department_Mast> dml = new List<Department_Mast>();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    Department_Mast dm = new Department_Mast();
                    dm.dept_cd = Convert.ToString(dr["DEPT_CD"]);
                    dm.dept_desc = Convert.ToString(dr["DEPT_DESC"]);
                    dml.Add(dm);
                }
            }
            return dml;
        }
    }
}