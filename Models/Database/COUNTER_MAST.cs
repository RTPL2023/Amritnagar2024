using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Amritnagar.Includes;

namespace Amritnagar.Models.Database
{
    public class COUNTER_MAST
    {
        SQLConfig config = new SQLConfig();
        public int counter_no  { get; set; }
        public string counter_desc  { get; set; }
        public string scroll_flag  { get; set; }
        public string counter_prefix { get; set; }


        public List<COUNTER_MAST> getCounterMast()
        {
            string sql = "Select * from  COUNTER_MAST";
            config.singleResult(sql);
            List<COUNTER_MAST> cml = new List<COUNTER_MAST>();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    COUNTER_MAST cm = new COUNTER_MAST();
                    cm.counter_no = Convert.ToInt32(dr["COUNTER_NO"]);
                    cm.counter_desc = Convert.ToString(dr["COUNTER_DESC"]);
                    cml.Add(cm);
                }
            }
            return cml;
        }
    }
}