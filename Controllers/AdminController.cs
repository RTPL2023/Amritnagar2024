using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Amritnagar.Models.Database;
using Amritnagar.Models.ViewModel;

namespace Amritnagar.Controllers
{
    public class AdminController : Controller
    {
        /********************************************Department Master Start*******************************************/
        [HttpGet]
        public ActionResult DepartmentMaster(DepartmentMasterViewModel model)
        {
            return View(model);
        }
        public JsonResult SaveDepartmentMaster(DepartmentMasterViewModel model)
        {
            if (model.dept_cd == null)
            {
                model.msg = "Department Code Cannot Be Blank";
            }
            else if (model.dept_desc == null)
            {
                model.msg = "Description Cannot Be Blank";
            }
            else
            {
                Department_Mast dm = new Department_Mast();
                dm.employer_cd = "ANC";
                dm.dept_cd = model.dept_cd.ToUpper();
                dm.dept_desc = model.dept_desc.ToUpper();
                model.msg = dm.CheckAndSaveDepartment(dm);
            }
            return Json(model.msg);
        }
        public JsonResult GetDepartmentDetailsTable(string dept_cd)
        {
            Department_Mast dm = new Department_Mast();
            List<Department_Mast> dml = new List<Department_Mast>();
            dml = dm.getAllDepartmentList();
            string fd = string.Empty;
            int i = 1;
            if (dml.Count > 0)
            {
                foreach (var a in dml)
                {
                    if (i == 1)
                    {
                        fd = "<tr><th>SR No</th><th>Dept. Code</th><th>Description</th></tr>";
                        fd = fd + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.dept_cd + "</td><td>" + a.dept_desc + "</td></tr>";
                    }
                    else
                    {
                        fd = fd + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.dept_cd + "</td><td>" + a.dept_desc + "</td></tr>";
                    }
                    i = i + 1;
                }
            }
            else
            {
                fd = null;
            }
            return Json(fd);
        }
        public JsonResult DeleteDepartment(string dept_cd)
        {
            Department_Mast dm = new Department_Mast();
            dm.DeleteDepartment(dept_cd);
            return Json("Confirm Deletion");
        }
        /********************************************Department Master End***********************************************/

        /********************************************Designation Master Start*******************************************/
        [HttpGet]
        public ActionResult DesignationMaster(DesignationMasterViewModel model)
        {
            return View(model);
        }
        public JsonResult SaveDesignationMaster(DesignationMasterViewModel model)
        {
            if (model.desig_cd == null)
            {
                model.msg = "Designation Code Cannot Be Blank";
            }
            else if (model.desig_desc == null)
            {
                model.msg = "Description Cannot Be Blank";
            }
            else
            {
                Designation_Mast dm = new Designation_Mast();
                dm.desig_cd = Convert.ToInt32(model.desig_cd);
                dm.desig_desc = model.desig_desc.ToUpper();
                model.msg = dm.CheckAndSaveDesignation(dm);
            }
            return Json(model.msg);
        }
        public JsonResult GetDesignationDetailsTable(string desig_cd)
        {
            Designation_Mast dm = new Designation_Mast();
            List<Designation_Mast> dml = new List<Designation_Mast>();
            dml = dm.getAllDesignationList();
            string fd = string.Empty;
            int i = 1;
            if (dml.Count > 0)
            {
                foreach (var a in dml)
                {
                    if (i == 1)
                    {
                        fd = "<tr><th>SR No</th><th>Code</th><th>Description</th></tr>";
                        fd = fd + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.desig_cd + "</td><td>" + a.desig_desc + "</td></tr>";
                    }
                    else
                    {
                        fd = fd + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.desig_cd + "</td><td>" + a.desig_desc + "</td></tr>";
                    }
                    i = i + 1;
                }
            }
            else
            {
                fd = null;
            }
            return Json(fd);
        }
        public JsonResult DeleteDesignation(string desig_cd)
        {
            Designation_Mast dm = new Designation_Mast();
            dm.Deletedesignation(desig_cd);
            return Json("Confirm Deletion");
        }
        /********************************************Designation Master End**********************************************/

        /********************************************Relationship Master Start*******************************************/
        [HttpGet]
        public ActionResult RelationshipMaster(RelationshipMasterViewModel model)
        {
            return View(model);
        }
        public JsonResult SaveRelationshipMaster(RelationshipMasterViewModel model)
        {
            if (model.reln_id == null)
            {
                model.msg = "Designation Code Cannot Be Blank";
            }
            else if (model.reln_name == null)
            {
                model.msg = "Description Cannot Be Blank";
            }
            else
            {
                Relation_Mast rm = new Relation_Mast();
                rm.reln_id = model.reln_id.ToUpper();
                rm.reln_name = model.reln_name.ToUpper();
                model.msg = rm.CheckAndSaveRelationship(rm);
            }
            return Json(model.msg);
        }
        public JsonResult GetRelationshipDetailsTable(string reln_id)
        {
            Relation_Mast rm = new Relation_Mast();
            List<Relation_Mast> rml = new List<Relation_Mast>();
            rml = rm.getAllRelationshipList();
            string fd = string.Empty;
            int i = 1;
            if (rml.Count > 0)
            {
                foreach (var a in rml)
                {
                    if (i == 1)
                    {
                        fd = "<tr><th>SR No</th><th>Relation Code</th><th>Description</th></tr>";
                        fd = fd + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.reln_id + "</td><td>" + a.reln_name + "</td></tr>";
                    }
                    else
                    {
                        fd = fd + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.reln_id + "</td><td>" + a.reln_name + "</td></tr>";
                    }
                    i = i + 1;
                }
            }
            else
            {
                fd = null;
            }
            return Json(fd);
        }
        public JsonResult DeleteRelationship(string reln_id)
        {
            Relation_Mast rm = new Relation_Mast();
            rm.Deleterelationship(reln_id);
            return Json("Confirm Deletion");
        }
        /********************************************Relationship Master End*******************************************/

        /********************************************Occupation Master Start*******************************************/
        [HttpGet]
        public ActionResult OccupationMaster(OccupationMasterViewModel model)
        {
            return View(model);
        }
        public JsonResult SaveOccupationMaster(OccupationMasterViewModel model)
        {
            if (model.occup_id == null)
            {
                model.msg = "Occupation Code Cannot Be Blank";
            }
            else if (model.occup_name == null)
            {
                model.msg = "Description Cannot Be Blank";
            }
            else
            {
                Occupation_Mast om = new Occupation_Mast();
                om.occup_id = model.occup_id;
                om.occup_name = model.occup_name.ToUpper();
                model.msg = om.CheckAndSaveOccupation(om);
            }
            return Json(model.msg);
        }
        public JsonResult GetOccupationDetailsTable(string occup_id)
        {
            Occupation_Mast om = new Occupation_Mast();
            List<Occupation_Mast> oml = new List<Occupation_Mast>();
            oml = om.getAllOccupationList();
            string fd = string.Empty;
            int i = 1;
            if (oml.Count > 0)
            {
                foreach (var a in oml)
                {
                    if (i == 1)
                    {
                        fd = "<tr><th>SR No</th><th>Occupation Code</th><th>Occupation Description</th></tr>";
                        fd = fd + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.occup_id + "</td><td>" + a.occup_name + "</td></tr>";
                    }
                    else
                    {
                        fd = fd + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.occup_id + "</td><td>" + a.occup_name + "</td></tr>";
                    }
                    i = i + 1;
                }
            }
            else
            {
                fd = null;
            }
            return Json(fd);
        }
        public JsonResult DeleteOccupationMaster(string occup_id)
        {
            Occupation_Mast om = new Occupation_Mast();
            om.Deleteoccupation(occup_id);
            return Json("Confirm Deletion");
        }

        /********************************************Occupation Master End*******************************************/

        /********************************************Caste Master Start**********************************************/
        [HttpGet]
        public ActionResult CasteMaster(CasteMasterViewModel model)
        {
            return View(model);
        }
        public JsonResult SaveCasteMaster(CasteMasterViewModel model)
        {
            if (model.caste_id == null)
            {
                model.msg = "Caste Code Cannot Be Blank";
            }
            else if (model.caste_name == null)
            {
                model.msg = "Description Cannot Be Blank";
            }
            else
            {
                Caste_Mast cm = new Caste_Mast();
                cm.caste_id = model.caste_id.ToUpper();
                cm.caste_name = model.caste_name.ToUpper();
                model.msg = cm.CheckAndSaveCaste(cm);
            }
            return Json(model.msg);
        }
        public JsonResult GetCasteDetailsTable(string caste_id)
        {
            Caste_Mast cm = new Caste_Mast();
            List<Caste_Mast> cml = new List<Caste_Mast>();
            cml = cm.getAllcasteList();
            string fd = string.Empty;
            int i = 1;
            if (cml.Count > 0)
            {
                foreach (var a in cml)
                {
                    if (i == 1)
                    {
                        fd = "<tr><th>SR No</th><th>Caste Code</th><th>Caste Description</th></tr>";
                        fd = fd + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.caste_id + "</td><td>" + a.caste_name + "</td></tr>";
                    }
                    else
                    {
                        fd = fd + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.caste_id + "</td><td>" + a.caste_name + "</td></tr>";
                    }
                    i = i + 1;
                }
            }
            else
            {
                fd = null;
            }
            return Json(fd);
        }
        public JsonResult DeleteCasteMaster(string caste_id)
        {
            Caste_Mast cm = new Caste_Mast();
            cm.Deletecaste(caste_id);
            return Json("Confirm Deletion");
        }
        /********************************************Caste Master End************************************************/

        /********************************************Religion Master Start**********************************************/
        [HttpGet]
        public ActionResult ReligionMaster(ReligionMasterViewModel model)
        {
            return View(model);
        }
        public JsonResult SaveReligionMaster(ReligionMasterViewModel model)
        {
            if (model.relgn_id == null)
            {
                model.msg = "Religion Code Cannot Be Blank";
            }
            else if (model.relgn_name == null)
            {
                model.msg = "Description Cannot Be Blank";
            }
            else
            {
                Religion_Mast rm = new Religion_Mast();
                rm.relgn_id = model.relgn_id.ToUpper();
                rm.relgn_name = model.relgn_name.ToUpper();
                model.msg = rm.CheckAndSaveReligion(rm);
            }
            return Json(model.msg);
        }
        public JsonResult GetReligionDetailsTable(string relgn_id)
        {
            Religion_Mast rm = new Religion_Mast();
            List<Religion_Mast> rml = new List<Religion_Mast>();
            rml = rm.getAllReligionList();
            string fd = string.Empty;
            int i = 1;
            if (rml.Count > 0)
            {
                foreach (var a in rml)
                {
                    if (i == 1)
                    {
                        fd = "<tr><th>SR No</th><th>Religion Code</th><th>Description</th></tr>";
                        fd = fd + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.relgn_id + "</td><td>" + a.relgn_name + "</td></tr>";
                    }
                    else
                    {
                        fd = fd + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.relgn_id + "</td><td>" + a.relgn_name + "</td></tr>";
                    }
                    i = i + 1;
                }
            }
            else
            {
                fd = null;
            }
            return Json(fd);
        }
        public JsonResult DeleteReligionMaster(string relgn_id)
        {
            Religion_Mast rm = new Religion_Mast();
            rm.DeleteReligion(relgn_id);
            return Json("Confirm Deletion");
        }
        /********************************************Religion Master End************************************************/

        /********************************************Member Category Master Start***************************************/
        [HttpGet]
        public ActionResult MemberCategoryMaster(MemberCategoryMasterViewModel model)
        {
            return View(model);
        }
        public JsonResult SaveMemberCategoryMaster(MemberCategoryMasterViewModel model)
        {
            if (model.mem_category == null)
            {
                model.msg = "Category Code Cannot Be Blank";
            }
            else if (model.category_desc == null)
            {
                model.msg = "Description Cannot Be Blank";
            }
            else
            {
                MemCategory_Mast mcm = new MemCategory_Mast();
                mcm.mem_category = model.mem_category.ToUpper();
                mcm.category_desc = model.category_desc.ToUpper();
                model.msg = mcm.CheckAndSaveMemberCategory(mcm);
            }
            return Json(model.msg);
        }
        public JsonResult GetMemberCategoryDetailsTable(string mem_category)
        {
            MemCategory_Mast mcm = new MemCategory_Mast();
            List<MemCategory_Mast> mcml = new List<MemCategory_Mast>();
            mcml = mcm.getAllmemberCategoryList();
            string fd = string.Empty;
            int i = 1;
            if (mcml.Count > 0)
            {
                foreach (var a in mcml)
                {
                    if (i == 1)
                    {
                        fd = "<tr><th>SR No</th><th>Category Code</th><th>Description</th></tr>";
                        fd = fd + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.mem_category + "</td><td>" + a.category_desc + "</td></tr>";
                    }
                    else
                    {
                        fd = fd + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.mem_category + "</td><td>" + a.category_desc + "</td></tr>";
                    }
                    i = i + 1;
                }
            }
            else
            {
                fd = null;
            }
            return Json(fd);
        }
        public JsonResult DeleteMembercategoryMaster(string mem_category)
        {
            MemCategory_Mast mcm = new MemCategory_Mast();
            mcm.DeleteMembercategory(mem_category);
            return Json("Confirm Deletion");
        }
        /********************************************Member Category Master End***************************************/

        /********************************************Member Type Master Start***************************************/
        [HttpGet]
        public ActionResult MemberTypeList(MemberTypeListViewModel model)
        {
            return View(model);
        }
        public JsonResult SaveMemberTypeMaster(MemberTypeListViewModel model)
        {
            if (model.mem_type == null)
            {
                model.msg = "Member Type Cannot Be Blank";
            }
            else if (model.type_desc == null)
            {
                model.msg = "Description Cannot Be Blank";
            }
            else
            {
                MemberType_Mast mtm = new MemberType_Mast();
                mtm.mem_type = model.mem_type.ToUpper();
                mtm.type_desc = model.type_desc.ToUpper();
                model.msg = mtm.CheckAndSaveMemberTypeList(mtm);
            }
            return Json(model.msg);
        }
        public JsonResult GetMemberTypeDetailsTable(string mem_type)
        {
            MemberType_Mast mtm = new MemberType_Mast();
            List<MemberType_Mast> mtml = new List<MemberType_Mast>();
            mtml = mtm.getAllmemberTypeList();
            string fd = string.Empty;
            int i = 1;
            if (mtml.Count > 0)
            {
                foreach (var a in mtml)
                {
                    if (i == 1)
                    {
                        fd = "<tr><th>SR No</th><th>Member Type</th><th>Description</th></tr>";
                        fd = fd + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.mem_type + "</td><td>" + a.type_desc + "</td></tr>";
                    }
                    else
                    {
                        fd = fd + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.mem_type + "</td><td>" + a.type_desc + "</td></tr>";
                    }
                    i = i + 1;
                }
            }
            else
            {
                fd = null;
            }
            return Json(fd);
        }
        public JsonResult DeleteMemberTypeMaster(string mem_type)
        {
            MemberType_Mast mtm = new MemberType_Mast();
            mtm.DeleteMemberType(mem_type);
            return Json("Confirm Deletion");
        }
        /********************************************Member Type Master End****************************************/

        /********************************************Loan Purpose Master Start**************************************/
        [HttpGet]
        public ActionResult LoanPurposeMaster(LoanPurposeMasterViewModel model)
        {
            return View(model);
        }
        public JsonResult GetLoanPurposeDetailsTable(string ln_purpose)
        {
            LnPurpose_Mast lpm = new LnPurpose_Mast();
            List<LnPurpose_Mast> lpml = new List<LnPurpose_Mast>();
            lpml = lpm.getAllLoanPurposeList();
            string fd = string.Empty;
            int i = 1;
            if (lpml.Count > 0)
            {
                foreach (var a in lpml)
                {
                    if (i == 1)
                    {
                        fd = "<tr><th>SR No</th><th>Purpose Code</th><th>Description</th></tr>";
                        fd = fd + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.ln_purpose + "</td><td>" + a.purpose_desc + "</td></tr>";
                    }
                    else
                    {
                        fd = fd + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.ln_purpose + "</td><td>" + a.purpose_desc + "</td></tr>";
                    }
                    i = i + 1;
                }
            }
            else
            {
                fd = null;
            }
            return Json(fd);
        }
        public JsonResult SaveLoanPurposeMaster(LoanPurposeMasterViewModel model)
        {
            if (model.ln_purpose == null)
            {
                model.msg = "Purpose Code Cannot Be Blank";
            }
            else if (model.purpose_desc == null)
            {
                model.msg = "Description Cannot Be Blank";
            }
            else
            {
                LnPurpose_Mast lpm = new LnPurpose_Mast();
                lpm.ln_purpose = model.ln_purpose.ToUpper();
                lpm.purpose_desc = model.purpose_desc.ToUpper();
                model.msg = lpm.CheckAndSaveLoanPurpose(lpm);
            }
            return Json(model.msg);
        }
        public JsonResult DeleteLoanPurposeMaster(string ln_purpose)
        {
            LnPurpose_Mast lpm = new LnPurpose_Mast();
            lpm.DeleteLoanPurpose(ln_purpose);
            return Json("Confirm Deletion");
        }
        /********************************************Loan Purpose Master End***************************************/

        /********************************************Bank Master Start*********************************************/
        [HttpGet]
        public ActionResult BankMaster(BankMasterViewModel model)
        {
            return View(model);
        }
        public JsonResult SaveBankMaster(BankMasterViewModel model)
        {
            if (model.bank_cd == null)
            {
                model.msg = "Bank Code Cannot Be Blank";
            }
            else if (model.bank_name == null)
            {
                model.msg = "Bank Name Cannot Be Blank";
            }
            else
            {
                Bank_Mast bm = new Bank_Mast();
                bm.bank_cd = model.bank_cd.ToUpper();
                bm.bank_name = model.bank_name.ToUpper();
                model.msg = bm.CheckAndSaveBankMaster(bm);
            }
            return Json(model.msg);
        }
        public JsonResult GetBankmasterDetailsTable(string bank_cd)
        {
            Bank_Mast bm = new Bank_Mast();
            List<Bank_Mast> bml = new List<Bank_Mast>();
            bml = bm.getAllBankList();
            string fd = string.Empty;
            int i = 1;
            if (bml.Count > 0)
            {
                foreach (var a in bml)
                {
                    if (i == 1)
                    {
                        fd = "<tr><th>SR No</th><th>Bank Code</th><th>Bank Name</th></tr>";
                        fd = fd + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.bank_cd + "</td><td>" + a.bank_name + "</td></tr>";
                    }
                    else
                    {
                        fd = fd + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.bank_cd + "</td><td>" + a.bank_name + "</td></tr>";
                    }
                    i = i + 1;
                }
            }
            else
            {
                fd = null;
            }
            return Json(fd);
        }
        public JsonResult DeleteBankMaster(string bank_cd)
        {
            Bank_Mast bm = new Bank_Mast();
            bm.DeleteBank(bank_cd);
            return Json("Confirm Deletion");
        }
        /********************************************Bank Master End*********************************************/

        /********************************************Employer Unit Master Start*********************************************/
        [HttpGet]
        public ActionResult EmployerUnitMaster(EmployerUnitMasterViewModel model)
        {
            UtilityController u = new UtilityController();
            model.EmpDesc = u.getEmployerMastDetails();
            return View(model);
        }
        public JsonResult SaveEmployerUnitMaster(EmployerUnitMasterViewModel model)
        {
            if (model.emp_cd == null)
            {
                model.msg = "Employer Name Cannot Be Blank";
            }
            else if (model.emp_branch_name == null)
            {
                model.msg = "Unit Name Cannot Be Blank";
            }
            else
            {
                Employer_Branch_Mast ebm = new Employer_Branch_Mast();
                ebm.emp_cd = model.emp_cd;
                ebm.emp_branch = model.emp_branch.ToUpper();
                ebm.emp_branch_name = model.emp_branch_name.ToUpper();
                if (model.address == null)
                {
                    ebm.address = "";
                }
                else
                {
                    ebm.address = model.address.ToUpper();
                }
                if (model.address_2 == null)
                {
                    ebm.address_2 = "";
                }
                else
                {
                    ebm.address_2 = model.address_2.ToUpper();
                }
                if (model.city == null)
                {
                    ebm.city = "";
                }
                else
                {
                    ebm.city = model.city.ToUpper();
                }
                if (model.dist == null)
                {
                    ebm.dist = "";
                }
                else
                {
                    ebm.dist = model.dist.ToUpper();
                }
                if (model.state == null)
                {
                    ebm.state = "";
                }
                else
                {
                    ebm.state = model.state.ToUpper();
                }
                if (model.phn_no == null)
                {
                    ebm.phn_no = "";
                }
                else
                {
                    ebm.phn_no = model.phn_no;
                }
                if (model.pin == null)
                {
                    ebm.pin = "";
                }
                else
                {
                    ebm.pin = model.pin;
                }
                model.msg = ebm.CheckAndSaveEmployerUnitMaster(ebm);
            }
            return Json(model.msg);
        }
        public JsonResult GetEmployerUnitMasterDetailsTable(string emp_cd)
        {
            Employer_Branch_Mast ebm = new Employer_Branch_Mast();
            List<Employer_Branch_Mast> ebml = new List<Employer_Branch_Mast>();
            ebml = ebm.getAllEmployerUnitList();
            string fd = string.Empty;
            int i = 1;
            if (ebml.Count > 0)
            {
                foreach (var a in ebml)
                {
                    if (i == 1)
                    {
                        fd = "<tr><th>SR No</th><th>Employer Code</th><th>Unit Code</th><th>Unit Name</th><th>Phone Number</th><th>Telex Number</th></tr>";
                        fd = fd + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.emp_cd + "</td><td>" + a.emp_branch + "</td><td>" + a.emp_branch_name + "</td><td>" + a.phn_no + "</td><td>" + a.telex_no + "</td></tr>";
                    }
                    else
                    {
                        fd = fd + "<tr><td>" + Convert.ToString(i) + "</td><td>" + a.emp_cd + "</td><td>" + a.emp_branch + "</td><td>" + a.emp_branch_name + "</td><td>" + a.phn_no + "</td><td>" + a.telex_no + "</td></tr>";
                    }
                    i = i + 1;
                }
            }
            else
            {
                fd = null;
            }
            return Json(fd);
        }
        public JsonResult GetSalaryDeductionACC_HeadDetailsTable(string emp_cd, string emp_branch)
        {
            Deduct_ACHD_Mast dam = new Deduct_ACHD_Mast();
            List<Deduct_ACHD_Mast> daml = new List<Deduct_ACHD_Mast>();
            daml = dam.getAllAchdList(emp_cd, emp_branch);
            string fd = string.Empty;
            int i = 1;
            if (daml.Count > 0)
            {
                foreach (var a in daml)
                {
                    if (i == 1)
                    {
                        fd = "<tr><th></th><th>SR No</th><th>A/C Hd</th><th>A/C Description</th></tr>";
                        fd = fd + "<tr><td><input type=\"checkbox\" name=\"checkboxName\" value=\"" + a.emp_cd + "\"></td><td>" + Convert.ToString(i) + "</td><td>" + a.ac_hd + "</td><td>" + a.ac_desc + "</td></tr>";
                    }
                    else
                    {
                        fd = fd + "<tr><td><input type=\"checkbox\" name=\"checkboxName\" value=\"" + a.emp_cd + "\"></td><td>" + Convert.ToString(i) + "</td><td>" + a.ac_hd + "</td><td>" + a.ac_desc + "</td></tr>";
                    }
                    i = i + 1;
                }
            }
            else
            {
                fd = null;
            }
            return Json(fd);
        }
        public JsonResult DeleteEmployerUnitMaster(string emp_branch)
        {
            Employer_Branch_Mast ebm = new Employer_Branch_Mast();
            ebm.DeleteEmployerUnit(emp_branch);
            return Json("Confirm Deletion");
        }
        /******************************************** Tf_Gf_Ledger modify ********************************************/

        [HttpGet]
        public ActionResult Tf_Gf_Ledger(Tf_Gf_LedgerViewModel model)
        {
            UtilityController u = new UtilityController();
            model.BranchDesc = u.getBranchMastDetails();
            model.achdDesc = u.getGlAcchd();
            return View(model);
        }
        public JsonResult getdataFoeGfAndTFLedgerModify(Tf_Gf_LedgerViewModel model)
        {
            Member_Mast mm = new Member_Mast();
            mm = mm.getdetailsbymemberid(model.branch, model.mem_no);
            model.mem_name = mm.mem_name;
            model.mem_date = mm.mem_date.ToString("dd/MM/yyyy").Replace("-", "/");
            TF_Ledger tl = new TF_Ledger();
            List<TF_Ledger> tflst = new List<TF_Ledger>();
            tflst = tl.getdataByledgerTab(model.achd, model.branch, model.mem_no);
            model.tableelement = "<tr><th>Srl No.</th>><th>Voucher Date</th><th>Vch.Tp</th><th>Voucher No.</th><th>Srl</th><th>Dr/Cr</th><th>Prin.Amount</th><th>Int.Amount</th><th>Prin.Balance</th><th>Int.Balance</th><th>Ins.Md</th><th>Vch/Achd</th></tr>";
            int i = 1;
            foreach (var a in tflst)
            {

                model.tableelement = model.tableelement + "<tr><td>" + i + "</td><td>" + a.vch_date + "</td><td>" + a.vch_type + "</td><td>" + a.vch_no + "</td>" +
                    "<td>" + a.vch_srl + "</td><td>" + a.dr_cr + "</td><td>" + a.prin_amount.ToString("0.00") + "</td><td>" + a.int_amount.ToString("0.00") + "</td><td>" + a.prin_bal.ToString("0.00") + "</td>" +
                    "<td>" + a.int_bal.ToString("0.00") + "</td><td>" + a.insert_mode + "</td><td>" + a.vch_achd + "</td></tr>";

                i++;
            }
            return Json(model);
        }
        public JsonResult UpdateTfGfLedger(Tf_Gf_LedgerViewModel model)
        {
            string msg;
            TF_Ledger tl = new TF_Ledger();
            msg = tl.UpdateGFandTFLedger(model);
            return Json(msg);
        }

        /******************************************** Tf_Gf_Ledger modify END ********************************************/

        /******************************************** Share Ledger Modify ********************************************/

        [HttpGet]
        public ActionResult ShareLedgerModify(Tf_Gf_LedgerViewModel model)
        {
            UtilityController u = new UtilityController();
            model.BranchDesc = u.getBranchMastDetails();
            model.achdDesc = u.getGlAcchd();
            return View(model);
        }
        public JsonResult getdataFoeShareLedgerModify(Tf_Gf_LedgerViewModel model)
        {
            Member_Mast mm = new Member_Mast();
            mm = mm.getdetailsbymemberid(model.branch, model.mem_no);
            model.mem_name = mm.mem_name;
            model.mem_date = mm.mem_date.ToString("dd/MM/yyyy").Replace("-", "/");
            TF_Ledger tl = new TF_Ledger();
            List<TF_Ledger> tflst = new List<TF_Ledger>();
            tflst = tl.getdataByledgerTab(model.achd, model.branch, model.mem_no);
            model.tableelement = "<tr><th>Srl No.</th>><th>Voucher Date</th><th>Vch.Tp</th><th>Voucher No.</th><th>Srl</th><th>Dr/Cr</th><th>Prin.Amount</th><th>Prin.Balance</th><th>Ins.Md</th><th>Vch/Achd</th></tr>";
            int i = 1;
            foreach (var a in tflst)
            {

                model.tableelement = model.tableelement + "<tr><td>" + i + "</td><td>" + a.vch_date + "</td><td>" + a.vch_type + "</td><td>" + a.vch_no + "</td>" +
                    "<td>" + a.vch_srl + "</td><td>" + a.dr_cr + "</td><td>" + a.prin_amount.ToString("0.00") + "</td><td>" + a.prin_bal.ToString("0.00") + "</td>" +
                    "<td>" + a.insert_mode + "</td><td>" + a.vch_achd + "</td></tr>";

                i++;
            }
            return Json(model);
        }
        public JsonResult UpdateShareLedger(Tf_Gf_LedgerViewModel model)
        {
            string msg;
            TF_Ledger tl = new TF_Ledger();
            msg = tl.UpdateGFandTFLedger(model);
            return Json(msg);
        }
        /******************************************** Share Ledger Modify End ********************************************/

    }
}