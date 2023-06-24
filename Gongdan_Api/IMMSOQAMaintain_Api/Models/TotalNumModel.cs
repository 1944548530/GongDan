using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMMSOQAMaintain_Api.Models
{
    public class TotalNumModel
    {
        public string modular { get; set; } 
        public string snNum { get; set; }
        public string leftNum { get; set; }
        public string opAmount { get; set; }
        public string erroNum { get; set; }
        public string erroTotal { get; set; }
        public string erroPer { get; set; }
        public string erroTotalPer { get; set; }
        public string lmtime { get; set; }
    }
}