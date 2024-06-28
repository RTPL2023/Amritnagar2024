using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Amritnagar.Includes;
namespace Amritnagar.Models.Database
{
    public class Member_Nominee
    {
        SQLConfig config = new SQLConfig();
        public string branch_id { get; set; }
        public string mem_id { get; set; }
        public string nom_srl { get; set; }
        public string nom_name { get; set; }
        public string nom_rltn_id { get; set; }
        public string nom_birthdt { get; set; }
        public string nom_hno { get; set; }
        public string nom_add1 { get; set; }
        public string nom_add2 { get; set; }
        public string nom_city { get; set; }
        public string nom_dist { get; set; }
        public string nom_state { get; set; }
        public string nom_pin { get; set; }

        public void SaveMemberNominee(Member_Nominee mn)
        {
            try
            {
                string sql = string.Empty;
                sql = "Select * from MEMBER_NOMINEE where BRANCH_ID='" + mn.branch_id + "' and MEMBER_ID = '"+ mn.mem_id + "'";
                config.singleResult(sql);
                if (config.dt.Rows.Count > 0)
                {
                    sql = "Delete from MEMBER_NOMINEE where BRANCH_ID='" + mn.branch_id + "' and MEMBER_ID = '" + mn.mem_id + "'";
                    config.Execute_Query(sql);
                    config.Insert("MEMBER_NOMINEE", new Dictionary<String, object>()
                    {
                    { "BRANCH_ID",  mn.branch_id },
                    { "MEMBER_ID",  mn.mem_id },
                    { "NOM_CITY",   mn.nom_city },
                    { "NOM_DIST",   mn.nom_dist},
                    { "NOM_PIN",    mn.nom_pin },
                    { "NOM_HNO",    mn.nom_hno },
                    { "NOM_ADD1",    mn.nom_add1},
                    { "NOM_ADD2",    mn.nom_add2},
                    { "NOM_BIRTH_DT",   mn.nom_birthdt },
                    { "NOM_STATE",   mn.nom_state },
                    { "NOM_NAME",   mn.nom_name },
                    { "NOM_RELN_ID",   mn.nom_rltn_id },
                    { "NOM_SRL",   mn.nom_srl },
                    });
                }
                else
                {
                    config.Insert("MEMBER_NOMINEE", new Dictionary<String, object>()
                    {
                    { "BRANCH_ID",  mn.branch_id },
                    { "MEMBER_ID",  mn.mem_id },
                    { "NOM_CITY",   mn.nom_city },
                    { "NOM_DIST",   mn.nom_dist},
                    { "NOM_PIN",    mn.nom_pin },
                    { "NOM_HNO",    mn.nom_hno },
                    { "NOM_ADD1",    mn.nom_add1},
                    { "NOM_ADD2",    mn.nom_add2},
                    { "NOM_BIRTH_DT",   mn.nom_birthdt },
                    { "NOM_STATE",   mn.nom_state },
                    { "NOM_NAME",   mn.nom_name },
                    { "NOM_RELN_ID",   mn.nom_rltn_id },
                    { "NOM_SRL",   mn.nom_srl },
                    });
                }                  
            }
            catch (Exception x)
            {

            }
        }

        public List<Member_Nominee> getnomineedetails(string branch_id, string member_no)
        {
            List<Member_Nominee> mnl = new List<Member_Nominee>();
            string sql = string.Empty;          
            sql = "SELECT * FROM MEMBER_NOMINEE WHERE BRANCH_ID='" + branch_id + "' AND MEMBER_ID='" + member_no + "'";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    Member_Nominee mn = new Member_Nominee();
                    mn.nom_name = !Convert.IsDBNull(dr["NOM_NAME"]) ? Convert.ToString(dr["NOM_NAME"]) : Convert.ToString(""); 
                    mn.nom_rltn_id = !Convert.IsDBNull(dr["NOM_RELN_ID"]) ? Convert.ToString(dr["NOM_RELN_ID"]) : Convert.ToString("");                    
                    mnl.Add(mn);
                }
            }                      
            return mnl;
        }
    }
}