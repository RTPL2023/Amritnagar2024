﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Amritnagar.Includes;
namespace Amritnagar.Models.Database
{
    public class Deduct_ACHD_Mast
    {
        SQLConfig config = new SQLConfig();
        public string emp_cd { get; set; }
        public string emp_branch { get; set; }
        public string ac_hd { get; set; }
        public string ac_desc { get; set; }

        public List<Deduct_ACHD_Mast> getAllAchdList(string emp_cd, string emp_branch)
        {
            string sql = "Select * from DEDUCT_ACHD_MAST WHERE EMPLOYER_CD='" + emp_cd + "' AND EMPLOYER_BRANCH='" + emp_branch + "'";
            config.singleResult(sql);
            List<Deduct_ACHD_Mast> daml = new List<Deduct_ACHD_Mast>();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    Deduct_ACHD_Mast dam = new Deduct_ACHD_Mast();
                    dam.ac_hd = Convert.ToString(dr["AC_HD"]);
                    dam.ac_desc = Convert.ToString(dr["AC_DESC"]);
                    daml.Add(dam);
                }
            }
            return daml;
        }
    }
}