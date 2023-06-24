using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMMSOQAMaintain_Api.Models
{
    public class QuanjUpModel
    {
        public string dateUp { get; set; }
        public string dateOld { get; set; }
        public string pinMing { get; set; }
        public string item { get; set; }
        public string size { get; set; }
        public string luhao { get; set; }
        public string luhaoOld { get; set; }
        public string testNum { get; set; }
        public string badNum { get; set; }
        public string erroNum { get; set; }
        public string erroPer { get; set; }
        public string opAmount { get; set; }
        public string opAmountOld { get; set; }
        public string QJhkNum { get; set; }
        public string QJqtNum { get; set; }
        public string QJkbName { get; set; }
        public string QJkbNum { get; set; }
        public string erroDetail { get; set; }
        public string erroDetailOld { get; set; }
    }
}