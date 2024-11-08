using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Amritnagar.Models.Database;
using Amritnagar.Models.ViewModel;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.AspNetCore;
//using Microsoft.Owin.Security;
//using Microsoft.Owin;
//using HomeoStore.Controllers;
//using Owin;
//using Microsoft.Owin.Extensions;
//using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Amritnagar;




namespace Amritnagar.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login(LoginViewModel model)
        {
            model.BranchDesc = getBranchMastDetails();
            if (model.BranchDesc == null)
                model.msg = "Unable To Connect Database Host";
            return View(model);
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model, string a)
        {
            if (ModelState.IsValid)
            {
                AuthDbUtility authDbUtility = new AuthDbUtility();
                MasterBranch mb = new MasterBranch();
                if (model.userid.ToUpper() == "SA" && model.Password == "Rishi@2022")
                {
                    Session["Uid"] = model.userid;
                    Session["UserRole"] = "ADMIN";
                    Session["Bid"] = "HO";
                    Session["BName"] = "HEAD OFFICE";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    var i = authDbUtility.getLoggin(model.userid, model.Password, model.BranchId);
                    if (i < 0)
                        model.msg = "Unable to connect with database host.";
                  
                    if (i == 4)
                    {
                        model.msg = "Wrong Branch Selected";
                    }
                    if(i == 2)
                    {
                        model.msg = "User Blocked";
                    }
                    if (i == 1)
                    {
                        string role = authDbUtility.getRole(model.userid);
                        Session["Uid"] = model.userid;
                        Session["UserRole"] = role;
                        Users u = new Users();
                        UserDBUtility udb = new UserDBUtility();
                        u = udb.getUserDetailsByUserId(model.userid);
                        Session["Bid"] = u.Allocated_BranchId;
                        Session["BName"] = mb.getBranch(u.Allocated_BranchId);
                        return RedirectToAction("Index", "Home");
                    }
                    if (i == 3)
                        model.msg = "Invalid User Name Or Password";
                }
            }
            return RedirectToAction("Login", model);
        }

        public IEnumerable<SelectListItem> getBranchMastDetails()
        {
            UsersViewModel uvm = new UsersViewModel();
            MasterBranch mb = new MasterBranch();
            uvm.BranchDesc = mb.getBranchMast().ToList().Select(x => new SelectListItem
            {
                Value = x.id.ToString(),
                Text = x.BranchName.ToString()
            }); ;
            return uvm.BranchDesc;
        }

        public ActionResult Logout(LoginViewModel model, string a)
        {
            Session.Clear();
            return RedirectToAction("Login", model);
        }
    }
}