using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Amritnagar.Includes;

namespace Amritnagar.Models.Database
{
    public class Member_Introducer
    {
        SQLConfig config = new SQLConfig();
        public string branch_id { get; set; }
        public string mem_id { get; set; }
        public int intr_srl { get; set; }
        public string intr_member_id { get; set; }
        public string intr_name { get; set; }

        public void SaveMemberIntroducer(Member_Introducer mi)
        {
            try
            {
                string sql = string.Empty;
                sql = "Select * from Member_Introducer where BRANCH_ID='" + mi.branch_id + "' and MEMBER_ID = '" + mi.mem_id + "'";
                config.singleResult(sql);
                if (config.dt.Rows.Count > 0)
                {
                    sql = "Delete from Member_Introducer where BRANCH_ID='" + mi.branch_id + "' and MEMBER_ID = '" + mi.mem_id + "'";
                    config.Execute_Query(sql);
                    config.Insert("Member_Introducer", new Dictionary<String, object>()
                    {
                    { "BRANCH_ID",  mi.branch_id },
                    { "Member_Id",  mi.mem_id},
                    { "intr_srl",   mi.intr_srl},
                    { "intr_member_id",mi.intr_member_id },
                    { "intr_name", mi.intr_name},
                    });
                }
                else
                {
                    config.Insert("Member_Introducer", new Dictionary<String, object>()
                    {
                    { "BRANCH_ID",  mi.branch_id },
                    { "Member_Id",  mi.mem_id},
                    { "intr_srl",   mi.intr_srl},
                    { "intr_member_id",mi.intr_member_id },
                    { "intr_name", mi.intr_name},
                    });
                }                   
            }
            catch(Exception ex)
            {

            }
           
        }

    }
}