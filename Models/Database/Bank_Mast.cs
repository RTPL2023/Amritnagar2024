using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Amritnagar.Includes;

namespace Amritnagar.Models.Database
{
    public class Bank_Mast
    {
        SQLConfig config = new SQLConfig();
        public string bank_cd { get; set; }
        public string bank_name { get; set; }


        public string CheckAndSaveBankMaster(Bank_Mast bm)
        {
            string sql = "Select * from BANK_MASTER where BANKCD='" + bm.bank_cd + "'";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                config.Update("BANK_MASTER", new Dictionary<String, object>()
                    {
                    { "BANKNAME",   bm.bank_name },
                }, new Dictionary<string, object>()
                {
                    { "BANKCD",     bm.bank_cd },
                });
            }
            else
            {
                config.Insert("BANK_MASTER", new Dictionary<string, object>()
                {
                    { "BANKNAME", bm.bank_name },
                    { "BANKCD",   bm.bank_cd },
                });
            }
            string msg = "Saved Successfully";
            return (msg);
        }
        public List<Bank_Mast> getAllBankList()
        {
            string sql = "Select * from BANK_MASTER order by BANKCD";
            config.singleResult(sql);
            List<Bank_Mast> lpml = new List<Bank_Mast>();
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataRow dr in config.dt.Rows)
                {
                    Bank_Mast bm = new Bank_Mast();
                    bm.bank_cd = Convert.ToString(dr["BANKCD"]);
                    bm.bank_name = Convert.ToString(dr["BANKNAME"]);
                    lpml.Add(bm);
                }
            }
            return lpml;
        }
        public void DeleteBank(string bank_cd)
        {
            Bank_Mast bm = new Bank_Mast();
            string sql = "Delete from BANK_MASTER where BANKCD= '" + bank_cd + "'";
            config.Execute_Query(sql);
        }
    }
}