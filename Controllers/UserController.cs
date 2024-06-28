using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Amritnagar.Models.ViewModel;
using System.Web.Mvc;
using Amritnagar.Models.Database;
using Amritnagar.Includes;

namespace Amritnagar.Controllers
{
    [SessionTimeout]
    public class UsersController : Controller
    {

        public ActionResult AllUserList()
        {
            UserDBUtility udu = new UserDBUtility();

            List<Users> usrs = new List<Users>();
            usrs = udu.GetAllUsers();

            ViewBag.Users = usrs;

            return View();
        }

        [HttpGet]
        public ActionResult EditUsers(string id)
        {
            EditUserViewModel model = new EditUserViewModel();
            UserDBUtility udu = new UserDBUtility();
            Users u = new Users();
            model.BranchDesc = getBranchMastDetails();
            u = udu.getUserDetailsByUserId(id);
            model.UserID = u.User_ID;
            model.UserName = u.User_Name;
            model.MobileNo = u.Mobile_number;
            model.EmailId = u.Email_ID;
            model.BranchId = u.Allocated_BranchId;
            model.Role = u.User_Role;
            //model.Disc = Convert.ToString(u.Disc);
            model.Password = AmritnagarUtility.Decryptdata(u.Password);
            if (u.Blocked == 0)
                model.Blocked = false;
            else
                model.Blocked = true;



            return View(model);
        }
        [HttpPost]
        public ActionResult EditUsers(EditUserViewModel model, string btnSave)
        {
            if (btnSave != null)
            {
                UserDBUtility udu = new UserDBUtility();
                Users u = new Users();
                u.User_ID = model.UserID;
                u.User_Name = model.UserName;
                u.User_Role = model.Role;
                u.Password = AmritnagarUtility.Encryptdata(model.Password);
                u.Mobile_number = model.MobileNo;
                u.Allocated_BranchId = model.BranchId;
                u.Email_ID = model.EmailId;
                if (model.Blocked == true)
                    u.Blocked = 1;
                else
                    u.Blocked = 0;

                u.modified_by = Convert.ToString(Session["Uid"]);
                //u.Disc = Convert.ToInt32(model.Disc);
                u.modified_on = DateTime.Now.ToShortDateString();
                u.modify_time = DateTime.Now.ToShortTimeString();
                u.m_device_name = Environment.MachineName;

                udu.EditUserdetailsByUserId(u);
            }
            return RedirectToAction("AllUserList");
        }

        [HttpGet]
        public ActionResult BlockUser(string id)
        {
            EditUserViewModel model = new EditUserViewModel();
            UserDBUtility udu = new UserDBUtility();
            Users u = new Users();
            u.User_ID = id;
            u.Blocked = 1;
            u.modified_by = Convert.ToString(Session["Uid"]);
            u.modified_on = DateTime.Now.ToShortDateString();
            u.modify_time = DateTime.Now.ToShortTimeString();
            u.User_Role = /*Convert.ToString(Session["UserRole"])*/Environment.MachineName;
            udu.BlockUserByUserId(u);

            return RedirectToAction("AllUserList");
        }

        [HttpGet]
        public ActionResult Add(UsersViewModel model)
        {
            model.BranchDesc = getBranchMastDetails();
            return View(model);
        }


        [HttpPost]
        public ActionResult Add(UsersViewModel model, String btnSave)
        {
            if (btnSave != null)
            {
                UserDBUtility udu = new UserDBUtility();
                var um = new UsersViewModel();
                if (model.UserName == null)
                {
                    model.msg = "User Name cannot be blank";
                }
                else if (model.UserID == null)
                {
                    model.msg = "User Id cannot be blank";
                }
                else if (model.Password == null)
                {
                    model.msg = "Password cannot be blank";
                }
                //else if (model.Disc == null)
                //{
                //    model.msg = "Discount(%) cannot be blank";
                //}
                else if (model.MobileNo == null)
                {
                    model.msg = "Mobile No cannot be blank";
                }
                else if (model.EmailId == null)
                {
                    model.msg = "Email-Id cannot be blank";
                }

                else
                {
                    Users u = new Users();
                    int a = udu.CheckUserId(model.UserID);
                    if (a == 0)
                    {
                        if (model.MobileNo.Length == 13 || model.MobileNo.Length == 11 || model.MobileNo.Length == 10)
                        {
                            string MobileNo = string.Empty;
                            if (model.MobileNo.Length > 10)
                            {
                                int length = model.MobileNo.Length - 10;
                                MobileNo = model.MobileNo.Substring(length);
                            }
                            else
                            {
                                MobileNo = model.MobileNo;
                            }
                            u = udu.CheckMobileNo(MobileNo);
                            if (u.tagMOB == 0)
                            {
                                u = udu.CheckEmailId(model.EmailId);
                                if (u.tagEmail == 0)
                                {
                                    u.User_ID = model.UserID;
                                    u.User_Name = model.UserName;
                                    u.User_Role = model.Role;
                                    u.Allocated_BranchId = model.BranchId;
                                    u.Password = AmritnagarUtility.Encryptdata(model.Password);
                                    u.Mobile_number = model.MobileNo;
                                    u.Disc = Convert.ToInt32(model.Disc);
                                    u.Email_ID = model.EmailId;
                                    u.created_by = Convert.ToString(Session["Uid"]);
                                    u.created_on = System.DateTime.Now.ToShortDateString();
                                    u.create_time = System.DateTime.Now.ToShortTimeString();
                                    u.device_name = Environment.MachineName;
                                    udu.AddUser(u);
                                    return RedirectToAction("AllUserList");
                                }
                                else
                                {
                                    model.msg = "Email Id Already Exist Against User Id : " + u.User_ID;
                                }
                            }
                            else
                            {
                                model.msg = "Mobile No Already Exist Against User Id : " + u.User_ID;
                            }
                        }
                        else
                        {
                            model.msg = "Invalid Mobile No";
                        }
                    }
                    else
                    {
                        model.msg = "USER ID ALREADY EXIST";
                    }
                }
            }
            return RedirectToAction("Add", model);
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
    }

}