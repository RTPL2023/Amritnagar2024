using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Amritnagar.Includes;

namespace Amritnagar.Models.Database
{
    public class Employer_Branch_Mast
    {
        SQLConfig config = new SQLConfig();
        public string emp_cd { get; set; }
        public string emp_branch { get; set; }
        public string emp_branch_name { get; set; }
        public string address { get; set; }
        public string address_2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string dist { get; set; }
        public string pin { get; set; }
        public string phn_no { get; set; }
        public string telex_no { get; set; }


        public string CheckAndSaveEmployerUnitMaster(Employer_Branch_Mast ebm)
        {
            string sql = "Select * from EMPLOYER_BRANCH_MAST where EMPLOYER_CD='" + ebm.emp_cd + "' and EMPLOYER_BRANCH = '"+ ebm.emp_branch +"'";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                config.Update("EMPLOYER_BRANCH_MAST", new Dictionary<String, object>()
                    {
                    { "EMPLOYER_BR_NAME",   ebm.emp_branch_name },
                    { "BRANCH_ADD1",   ebm.address },
                    { "BRANCH_ADD2",   ebm.address_2 },
                    { "BRANCH_CITY",   ebm.city },
                    { "BRANCH_DIST",   ebm.dist },
                    { "BRANCH_STATE",   ebm.state },
                    { "BRANCH_PIN",   ebm.pin },
                    { "BRANCH_PHONE",   ebm.phn_no },
                    { "BRANCH_TELEX",   ebm.telex_no },
                }, new Dictionary<string, object>()
                {
                    { "EMPLOYER_CD",         ebm.emp_cd },
                    { "EMPLOYER_BRANCH",     ebm.emp_branch },
                });
            }
            else
            {
                config.Insert("EMPLOYER_BRANCH_MAST", new Dictionary<string, object>()
                {
                    { "EMPLOYER_CD",         ebm.emp_cd },
                    { "EMPLOYER_BRANCH",     ebm.emp_branch },
                    { "EMPLOYER_BR_NAME",   ebm.emp_branch_name },
                    { "BRANCH_ADD1",   ebm.address },
                    { "BRANCH_ADD2",   ebm.address_2 },
                    { "BRANCH_CITY",   ebm.city },
                    { "BRANCH_DIST",   ebm.dist },
                    { "BRANCH_STATE",   ebm.state },
                    { "BRANCH_PIN",   ebm.pin },
                    { "BRANCH_PHONE",   ebm.phn_no },
                    { "BRANCH_TELEX",   ebm.telex_no },
                });
            }
            string msg = "Saved Successfully";
            return (msg);
        }
        public List<Employer_Branch_Mast> getAllEmployerUnitList()
        {
            string sql = "Select * from EMPLOYER_BRANCH_MAST order by EMPLOYER_BR_NAME";
            config.singleResult(sql);
            List<Employer_Branch_Mast> ebml = new List<Employer_Branch_Mast>();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    Employer_Branch_Mast em = new Employer_Branch_Mast();
                    em.emp_cd = Convert.ToString(dr["EMPLOYER_CD"]);
                    em.emp_branch = Convert.ToString(dr["EMPLOYER_BRANCH"]);
                    em.emp_branch_name = Convert.ToString(dr["EMPLOYER_BR_NAME"]);
                    em.phn_no = !Convert.IsDBNull(dr["BRANCH_PHONE"]) ? Convert.ToString(dr["BRANCH_PHONE"]) : Convert.ToString("");
                    em.telex_no = !Convert.IsDBNull(dr["BRANCH_TELEX"]) ? Convert.ToString(dr["BRANCH_TELEX"]) : Convert.ToString("");
                    ebml.Add(em);
                }
            }
            return ebml;
        }
        public void DeleteEmployerUnit(string emp_branch)
        {
            Employer_Branch_Mast ebm = new Employer_Branch_Mast();
            string sql = "Delete from EMPLOYER_BRANCH_MAST where EMPLOYER_BRANCH= '" + emp_branch + "'";
            config.Execute_Query(sql);
        }
        public List<Employer_Branch_Mast> getEmployerBranchMast()
        {
            string sql = "Select * from  EMPLOYER_BRANCH_MAST";
            config.singleResult(sql);
            List<Employer_Branch_Mast> ebml = new List<Employer_Branch_Mast>();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    Employer_Branch_Mast ebm = new Employer_Branch_Mast();
                    ebm.emp_branch = Convert.ToString(dr["EMPLOYER_BRANCH"]);
                    ebm.emp_branch_name = Convert.ToString(dr["EMPLOYER_BR_NAME"]);
                    ebml.Add(ebm);
                }
            }
            return ebml;
        }
        public string getunitnamebycode(string emp_br_code)
        {
            string sql = "Select * from EMPLOYER_BRANCH_MAST where EMPLOYER_BRANCH= '" + emp_br_code + "'";
            config.singleResult(sql);
            Employer_Branch_Mast ebm = new Employer_Branch_Mast();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {                                       
                    ebm.emp_branch_name = Convert.ToString(dr["EMPLOYER_BR_NAME"]);                    
                }
            }
            return ebm.emp_branch_name;
        }
    }
}