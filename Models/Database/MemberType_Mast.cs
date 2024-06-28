using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Amritnagar.Includes;

namespace Amritnagar.Models.Database
{
    public class MemberType_Mast
    {
        SQLConfig config = new SQLConfig();
        public string mem_type { get; set; }
        public string type_desc { get; set; }


        public string CheckAndSaveMemberTypeList(MemberType_Mast mtm)
        {
            string sql = "Select * from MEMTYPE_MAST where MEMBER_TYPE='" + mtm.mem_type + "'";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                config.Update("MEMTYPE_MAST", new Dictionary<String, object>()
                    {
                    { "TYPE_DESC",   mtm.type_desc },
                }, new Dictionary<string, object>()
                {
                    { "MEMBER_TYPE",     mtm.mem_type },
                });
            }
            else
            {
                config.Insert("MEMTYPE_MAST", new Dictionary<string, object>()
                {
                    { "TYPE_DESC",     mtm.type_desc },
                    { "MEMBER_TYPE",   mtm.mem_type },
                });
            }
            string msg = "Saved Successfully";
            return (msg);
        }
        public List<MemberType_Mast> getAllmemberTypeList()
        {
            string sql = "Select * from MEMTYPE_MAST order by MEMBER_TYPE";
            config.singleResult(sql);
            List<MemberType_Mast> mtml = new List<MemberType_Mast>();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    MemberType_Mast mtm = new MemberType_Mast();
                    mtm.mem_type = Convert.ToString(dr["MEMBER_TYPE"]);
                    mtm.type_desc = Convert.ToString(dr["TYPE_DESC"]);
                    mtml.Add(mtm);
                }
            }
            return mtml;
        }
        public void DeleteMemberType(string mem_type)
        {
            MemberType_Mast mtm = new MemberType_Mast();
            string sql = "Delete from MEMTYPE_MAST where MEMBER_TYPE= '" + mem_type + "'";
            config.Execute_Query(sql);
        }
        public List<MemberType_Mast> getTypeMast()
        {
            string sql = "Select * from  MEMTYPE_MAST";
            config.singleResult(sql);
            List<MemberType_Mast> mtml = new List<MemberType_Mast>();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    MemberType_Mast mtm = new MemberType_Mast();
                    mtm.mem_type = Convert.ToString(dr["MEMBER_TYPE"]);
                    mtm.type_desc = Convert.ToString(dr["TYPE_DESC"]);
                    mtml.Add(mtm);
                }
            }
            return mtml;
        }
    }
}