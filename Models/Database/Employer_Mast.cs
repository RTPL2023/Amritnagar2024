using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Amritnagar.Includes;

namespace Amritnagar.Models.Database
{
    public class Employer_Mast
    {
        SQLConfig config = new SQLConfig();
        public string emp_cd { get; set; }
        public string emp_name { get; set; }

        public List<Employer_Mast> getEmployerMast()
        {
            string sql = "Select * from  EMPLOYER_MAST";
            config.singleResult(sql);
            List<Employer_Mast> eml = new List<Employer_Mast>();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    Employer_Mast em = new Employer_Mast();
                    em.emp_cd = Convert.ToString(dr["EMPLOYER_CD"]);
                    em.emp_name = Convert.ToString(dr["EMPLOYER_NAME"]);
                    eml.Add(em);
                }
            }
            return eml;
        }

    }
}