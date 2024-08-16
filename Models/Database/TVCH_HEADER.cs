using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Amritnagar.Includes;

namespace Amritnagar.Models.Database
{
    public class TVCH_HEADER
    {
        SQLConfig config = new SQLConfig();
        public string branch_id { get; set; }
        public DateTime trn_date { get; set; }
        public string trn_shift { get; set; }
        public decimal counter_no { get; set; }
        public string trn_no { get; set; }
        public string trn_type { get; set; }
        public string chk_flag { get; set; }
        public string insert_mode { get; set; }

        public void SaveUpdateTVCH_Header(string branch, string date, string shift, string counter, string vch_no)
        {
            string Shift_type = string.Empty;
            string sql = string.Empty;
            if(shift == "EVENING")
            {
                Shift_type = "E";
            }
            else if(shift == "GENERAL")
            {
                Shift_type = "G";
            }
            else if(shift == "MORNING")
            {
                Shift_type = "M";
            }
            sql = "SELECT * FROM TVCH_HEADER WHERE BRANCH_ID='" + branch + "' AND ";
            sql = sql + "convert(varchar, TRN_DATE, 103) = '" + date.Replace("-", "/") + "' AND ";
            sql = sql + "TRN_SHIFT='" + Shift_type + "' AND ";
            sql = sql + "INSERT_MODE='MR'";
            sql = sql + "AND COUNTER_NO='" + counter + "'";
            sql = sql + " ORDER BY TRN_DATE";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                sql = "Delete from TVCH_HEADER where convert(varchar, TRN_DATE, 103) = '" + date.Replace("-", "/") + "' and INSERT_MODE = 'MR' and trn_no='" + vch_no + "'";
                config.Execute_Query(sql);
                config.Insert("TVCH_HEADER", new Dictionary<String, object>()
                {
                    {"branch_id",   branch },
                    {"trn_no", vch_no },
                    {"trn_shift",    Shift_type},
                    {"trn_date",   Convert.ToDateTime(date)},                    
                    {"counter_no",    counter},
                    {"TRN_TYPE",    "C"},
                    {"INSERT_MODE",  "MR"}
                });
            }
            else
            {
                config.Insert("TVCH_HEADER", new Dictionary<String, object>()
                {
                    {"branch_id",   branch },
                    {"trn_no", vch_no },
                    {"trn_shift",    Shift_type},
                    {"trn_date",   Convert.ToDateTime(date)},
                    {"counter_no",    counter},
                    {"TRN_TYPE",    "C"},
                    {"INSERT_MODE",  "MR"}
                });
            }
        }
    }
}