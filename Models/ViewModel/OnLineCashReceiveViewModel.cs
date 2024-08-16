using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Amritnagar.Models.ViewModel
{
    public class OnLineCashReceiveViewModel
    {
        public string branch { get; set; }
        public string date { get; set; }
        public string shift { get; set; }
        public string counter { get; set; }
        public string achd { get; set; }
        public string vch_no { get; set; }
        public string acc_no { get; set; }
        public string rcv_prtclr { get; set; }
        public string amt { get; set; }
        public string name { get; set; }
        public string op_dt { get; set; }
        public string clrd_upto { get; set; }
        public string amt_due { get; set; }
        public string gt_head { get; set; }
        public string gt_amt { get; set; }
        public string tableelement { get; set; }
        public IEnumerable<SelectListItem> BranchDesc { get; set; }
        public IEnumerable<SelectListItem> TypeDesc { get; set; }
        public IEnumerable<SelectListItem> achddesc { get; set; }
        public IEnumerable<SelectListItem> CounterDesc { get; set; }
    }
}