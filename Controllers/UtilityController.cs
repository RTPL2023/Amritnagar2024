using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Amritnagar.Models.ViewModel;
using System.Web.Mvc;
using Amritnagar.Models.Database;
using System.Drawing.Printing;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data;
//using OfficeOpenXml;
using System.IO;

namespace Amritnagar.Controllers
{
    [SessionTimeout]
    public class UtilityController : Controller
    {         
        //****************For Branch Drop Down
        public IEnumerable<SelectListItem> getBranchMastDetails()
        {
            LoginViewModel cr = new LoginViewModel();

            MasterBranch mb = new MasterBranch();
            cr.BranchDesc = mb.getBranchMast().ToList().Select(x => new SelectListItem
            {
                Value = x.id.ToString(),
                Text = x.BranchName.ToString()
            }); ;

            return cr.BranchDesc;

        }

        //****************For Gl Achd Type Drop Down only  ac_hd='GF'  ac_hd='INSU'  ac_hd='RTB' AC_HD='TF'
        public IEnumerable<SelectListItem> getGlAcchd()
        {
            LoanMasterEntryViewModel lmevm = new LoanMasterEntryViewModel();
            ACC_HEAD acc = new ACC_HEAD();
            lmevm.achddesc = acc.getglac_hd().ToList().Select(x => new SelectListItem
            {
                Value = x.ac_hd.ToString(),
                Text = x.ac_desc.ToString()
            }); ;
            return lmevm.achddesc;
        }

        //****************For Gl Achd Type Drop Down only  ac_hd='ISH'  ac_hd='SH' 
        public IEnumerable<SelectListItem> getGlAcc_hd()
        {
            ShareCapitalDetailListViewModel lmevm = new ShareCapitalDetailListViewModel();
            ACC_HEAD acc = new ACC_HEAD();
            lmevm.achddesc = acc.getglachd().ToList().Select(x => new SelectListItem
            {
                Value = x.ac_hd.ToString(),
                Text = x.ac_desc.ToString()
            }); ;
            return lmevm.achddesc;
        }

        //****************For Employer Name Drop Down
        public IEnumerable<SelectListItem> getEmployerMastDetails()
        {
            EmployerUnitMasterViewModel emvm = new EmployerUnitMasterViewModel();
            Employer_Mast em = new Employer_Mast();
            emvm.EmpDesc = em.getEmployerMast().ToList().Select(x => new SelectListItem
            {
                Value = x.emp_cd.ToString(),
                Text = x.emp_name.ToString()
            }); ;
            return emvm.EmpDesc;
        }

        //****************For Employer Branch Name Drop Down
        public IEnumerable<SelectListItem> getEmployerBranchMastDetails()
        {
            MemberMasterViewModel emvm = new MemberMasterViewModel();
            Employer_Branch_Mast ebm = new Employer_Branch_Mast();
            emvm.EmpBranchDesc = ebm.getEmployerBranchMast().ToList().Select(x => new SelectListItem
            {
                Value = x.emp_branch.ToString(),
                Text = x.emp_branch_name.ToString()
            }); ;
            return emvm.EmpBranchDesc;
        }


        //****************For Caste Drop Down
        public IEnumerable<SelectListItem> getCastMastDetails()
        {
            MemberMasterViewModel emvm = new MemberMasterViewModel();
            Caste_Mast cm = new Caste_Mast();
            emvm.CastDesc = cm.getCastMast().ToList().Select(x => new SelectListItem
            {
                Value = x.caste_id.ToString(),
                Text = x.caste_name.ToString()
            }); ;
            return emvm.CastDesc;
        }

        //****************For Religion Drop Down
        public IEnumerable<SelectListItem> getReligionMastDetails()
        {
            MemberMasterViewModel emvm = new MemberMasterViewModel();
            Religion_Mast rm = new Religion_Mast();
            emvm.ReligionDesc = rm.getReligionMast().ToList().Select(x => new SelectListItem
            {
                Value = x.relgn_id.ToString(),
                Text = x.relgn_name.ToString()
            }); ;
            return emvm.ReligionDesc;
        }

        //****************For Occupation Drop Down
        public IEnumerable<SelectListItem> getOccupationMastDetails()
        {
            MemberMasterViewModel emvm = new MemberMasterViewModel();
            Occupation_Mast om = new Occupation_Mast();
            emvm.OccupationDesc = om.getOccupationMast().ToList().Select(x => new SelectListItem
            {
                Value = x.occup_id.ToString(),
                Text = x.occup_name.ToString()
            }); ;
            return emvm.OccupationDesc;
        }

        //****************For Relationship Drop Down
        public IEnumerable<SelectListItem> getRelationMastDetails()
        {
            MemberMasterViewModel emvm = new MemberMasterViewModel();
            Relation_Mast rm = new Relation_Mast();
            emvm.RelationDesc = rm.getRelationMast().ToList().Select(x => new SelectListItem
            {
                Value = x.reln_id.ToString(),
                Text = x.reln_name.ToString()
            }); ;
            return emvm.RelationDesc;
        }

        //****************For Designation Drop Down
        public IEnumerable<SelectListItem> getDesignationMastDetails()
        {
            MemberMasterViewModel emvm = new MemberMasterViewModel();
            Designation_Mast dm = new Designation_Mast();
            emvm.DesignationDesc = dm.getDesignationMast().ToList().Select(x => new SelectListItem
            {
                Value = x.desig_cd.ToString(),
                Text = x.desig_desc.ToString()
            }); ;
            return emvm.DesignationDesc;
        }

        //****************For ervice Status Drop Down
        public IEnumerable<SelectListItem> getServiceStatusMastDetails()
        {
            MemberMasterViewModel emvm = new MemberMasterViewModel();
            Service_Status_Mast ssm = new Service_Status_Mast();
            emvm.ServiceDesc = ssm.getServiceStatusMast().ToList().Select(x => new SelectListItem
            {
                Value = x.serv_status.ToString(),
                Text = x.serv_status_desc.ToString()
            }); ;
            return emvm.ServiceDesc;
        }

        //****************For Service Status Drop Down
        public IEnumerable<SelectListItem> getDepartmentMastDetails()
        {
            MemberMasterViewModel emvm = new MemberMasterViewModel();
            Department_Mast ssm = new Department_Mast();
            emvm.DepartmentDesc = ssm.getDepartmentMast().ToList().Select(x => new SelectListItem
            {
                Value = x.dept_cd.ToString(),
                Text = x.dept_desc.ToString()
            }); ;
            return emvm.DepartmentDesc;
        }

        //****************For Category Drop Down
        public IEnumerable<SelectListItem> getCategoryMastDetails()
        {
            MemberMasterViewModel emvm = new MemberMasterViewModel();
            MemCategory_Mast ssm = new MemCategory_Mast();
            emvm.CategoryDesc = ssm.getCategoryMast().ToList().Select(x => new SelectListItem
            {
                Value = x.mem_category.ToString(),
                Text = x.category_desc.ToString()
            }); ;
            return emvm.CategoryDesc;
        }

        //****************For Type Drop Down
        public IEnumerable<SelectListItem> getTypeMastDetails()
        {
            MemberMasterViewModel emvm = new MemberMasterViewModel();
            MemberType_Mast mtm = new MemberType_Mast();
            emvm.TypeDesc = mtm.getTypeMast().ToList().Select(x => new SelectListItem
            {
                Value = x.mem_type.ToString(),
                Text = x.type_desc.ToString()
            }); ;
            return emvm.TypeDesc;
        }

        //****************For Loan Purpose Drop Down
        public IEnumerable<SelectListItem> getLoanPurposeMastDetails()
        {
            LoanMasterEntryViewModel lmevm = new LoanMasterEntryViewModel();
            LnPurpose_Mast mtm = new LnPurpose_Mast();
            lmevm.lnpurposedesc = mtm.getAllLoanPurposeList().ToList().Select(x => new SelectListItem
            {
                Value = x.ln_purpose.ToString(),
                Text = x.purpose_desc.ToString()
            }); ;
            return lmevm.lnpurposedesc;
        }

        //****************For Loan Status Drop Down
        public IEnumerable<SelectListItem> getLoanStatusMastDetails()
        {
            LoanMasterEntryViewModel lmevm = new LoanMasterEntryViewModel();
            LnStatus_Mast lsm = new LnStatus_Mast();
            lmevm.lnstatusdesc = lsm.getLoanStatusMast().ToList().Select(x => new SelectListItem
            {
                Value = x.status_cd.ToString(),
                Text = x.status_desc.ToString()
            }); ;
            return lmevm.lnstatusdesc;
        }

        //****************For Loan Type Drop Down
        public IEnumerable<SelectListItem> getLoanTypeMastDetails()
        {
            LoanMasterEntryViewModel lmevm = new LoanMasterEntryViewModel();
            LnType_Mast lpm = new LnType_Mast();
            lmevm.lntypedesc = lpm.getLoanTypeMast().ToList().Select(x => new SelectListItem
            {
                Value = x.ac_hd.ToString(),
                Text = x.lntype_desc.ToString()
            }); ;
            return lmevm.lntypedesc;
        }

        public MemoryStream DownloadTextFiles(string filename, string uploadPath)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), uploadPath, filename);
            var memory = new MemoryStream();
            if (System.IO.File.Exists(path))
            {
                var net = new System.Net.WebClient();
                var data = net.DownloadData(path);
                var content = new System.IO.MemoryStream(data);
                memory = content;
            }
            memory.Position = 0;
            return memory;
        }

    }
}