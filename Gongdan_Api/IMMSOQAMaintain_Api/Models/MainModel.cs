using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMMSOQAMaintain_Api.Models
{
    public class MainModel
    {
        public string date { get; set; }
        public string modular { get; set; }
        public string prodName { get; set; }
        public string item { get; set; }
        public string size { get; set; }
        public string potNum { get; set; }
        public string snNum { get; set; }
        public string totalAmount { get; set; }
        public string leftNum { get; set; }
        public string erroTotal { get; set; }
        public string erroTotalPer { get; set; }
        public string badNum { get; set; }
        public string opAmount { get; set; }
        public int inputAmount { get; set; }
        public string erroNum { get; set; }
        public string erroPer { get; set; }
        public string erroDetail { get; set; }
        public string erro1 { get; set; }
        public string erro2 { get; set; }
        public string erro3 { get; set; }
        public string erro4 { get; set; }
        public string erro5 { get; set; }
        public string erro6 { get; set; }
        public string errBlankName { get; set; }
        public string errBlankNum { get; set; }
        public string lmdate { get; set; }
        public string lmtime { get; set; }
        public string lmuser { get; set; }
        public string hongkao { get; set; }
        public string lastProcOKNum { get; set; }
    }
}