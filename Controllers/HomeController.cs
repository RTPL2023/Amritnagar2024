using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Amritnagar.Models.ViewModel;
using Amritnagar.Includes;
using System.Data;

namespace Amritnagar.Controllers
{
    public class HomeController : Controller
    {
        SQLConfig config = new SQLConfig();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult HomePage()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult DashBoard(DashboardViewModel model)
        {
            return View(model);
        }

        public string getfromdate(DashboardViewModel model)
        {
            string fdate = "";

            if (model.Month == "1Month")
            {
                fdate = DateTime.Now.Date.AddMonths(-1).ToString("dd-MM-yyyy").Replace("-", "/");
                //model.Month = "1";
            }
            else if (model.Month == "3Month")
            {
                fdate = DateTime.Now.Date.AddMonths(-3).ToString("dd-MM-yyyy").Replace("-", "/");
                //  model.Month = "3";
            }
            else if (model.Month == "6Month")
            {
                fdate = DateTime.Now.Date.AddMonths(-6).ToString("dd-MM-yyyy").Replace("-", "/");
                // model.Month = "6";
            }
            else if (model.Month == "12Month")
            {
                fdate = DateTime.Now.Date.AddMonths(-12).ToString("dd-MM-yyyy").Replace("-", "/");
                // model.Month = "12";
            }
            return fdate;
        }

        public JsonResult GetNumberOfLoans(DashboardViewModel model)
        {
            List<object> iData = new List<object>();
            string sql = "";
            string fdate = "";
            fdate = getfromdate(model);
            string todate = DateTime.Now.Date.ToString("dd-MM-yyyy").Replace("-", "/");
            sql = "select ac_hd , COUNT(*)  AS TotalRows  from loan_master where  convert(datetime, loan_date, 103) >= convert(datetime, '" + fdate + "', 103) and convert(datetime, loan_date, 103) <= convert(datetime, '" + todate + "', 103) group by ac_hd";
            config.singleResult(sql);
            int I = 0;
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataColumn dc in config.dt.Columns)
                {
                    List<object> x = new List<object>();
                    x = (from DataRow drr in config.dt.Rows select drr[dc.ColumnName]).ToList();
                    iData.Add(x);
                }
            }
            return Json(iData);
        }

        public JsonResult GetNumberOfMemberOpening(DashboardViewModel model)
        {
            List<object> iData = new List<object>();
            string sql = "";
            string fdate = "";
            fdate = getfromdate(model);
            string todate = DateTime.Now.Date.ToString("dd-MM-yyyy").Replace("-", "/");
            sql = "select book_no, COUNT(*) AS TotalRows from MEMBER_MAST where convert(datetime, MEMBER_DATE, 103) >= convert(datetime, '" + fdate + "', 103) and convert(datetime, MEMBER_DATE, 103) <= convert(datetime, '" + todate + "', 103) group by book_no";
            config.singleResult(sql);
            int I = 0;
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataColumn dc in config.dt.Columns)
                {
                    List<object> x = new List<object>();
                    x = (from DataRow drr in config.dt.Rows select drr[dc.ColumnName]).ToList();
                    iData.Add(x);
                }
            }
            return Json(iData);
        }

        public JsonResult GetNumberOfMemberClosing(DashboardViewModel model)
        {
            List<object> iData = new List<object>();
            string sql = "";
            string fdate = "";
            //fdate = getfromdate(model);
            fdate = "01/09/2017";
            string todate = DateTime.Now.Date.ToString("dd-MM-yyyy").Replace("-", "/");
            sql = "select book_no, COUNT(*) AS TotalRows from MEMBER_MAST where convert(datetime, MEMBER_CLOSDT, 103) >= convert(datetime, '" + fdate + "', 103) and convert(datetime, MEMBER_CLOSDT, 103) <= convert(datetime, '" + todate + "', 103) group by book_no";
            config.singleResult(sql);
            int I = 0;
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataColumn dc in config.dt.Columns)
                {
                    List<object> x = new List<object>();
                    x = (from DataRow drr in config.dt.Rows select drr[dc.ColumnName]).ToList();
                    iData.Add(x);
                }
            }
            return Json(iData);
        }

        public JsonResult GetLoanTrasactionsDetails(DashboardViewModel model)
        {
            List<object> iData = new List<object>();
            string sql = "";                    
            sql = "select ac_hd , COUNT(*)  AS TotalRows from loan_master where convert(datetime, loan_date, 103) >= convert(datetime, '" + model.fdate + "', 103) and convert(datetime, loan_date, 103) <= convert(datetime, '" + model.tdate + "', 103) group by ac_hd";          
            config.singleResult(sql);
            int I = 0;
            if (config.dt.Rows.Count > 0)
            {
                foreach (DataColumn dc in config.dt.Columns)
                {
                    List<object> x = new List<object>();
                    x = (from DataRow drr in config.dt.Rows select drr[dc.ColumnName]).ToList();
                    iData.Add(x);
                }
            }
            return Json(iData);
        }

        public JsonResult GetlistOftrasactionAmt(DashboardViewModel model)
        {
            string sql = "";           
            int i = 1;                      
            sql = "select ac_hd , Sum(loan_amt) AS Amount from loan_master where convert(datetime, loan_date, 103) >= convert(datetime, '" + model.fdate + "', 103) and convert(datetime, loan_date, 103) <= convert(datetime, '" + model.tdate + "', 103) group by ac_hd";
            config.singleResult(sql);
            if (config.dt.Rows.Count > 0)
            {
                model.tableelement = "<tr><th>Srl</th><th>AcHd</th><th>Amount</th></tr>";
                foreach (DataRow dr in config.dt.Rows)
                {
                    model.tableelement = model.tableelement + "<tr><td>" + i + "</td><td>" + Convert.ToString(dr["ac_hd"]) + "</td><td>" + Convert.ToDecimal(dr["Amount"]).ToString("0.00") + "</td></tr>";
                    i++;
                }
            }          
           return Json(model);
        }
    }
}