using IMMSOQAMaintain_Api.common;
using IMMSOQAMaintain_Api.DAL;
using IMMSOQAMaintain_Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMMSOQAMaintain_Api.BII
{
    public class ChaXun_BLL
    {
        ChaXun_DAL dal = new ChaXun_DAL();
        Tool tool = new Tool();
        public List<MainModel> InfoBySn(string gongdan, string procedure, string liaohao, string luhao, int pageNum, int pagesize) {
            int indexBegin = (pageNum - 1) * pagesize + 1;
            int indexEnd = pageNum * pagesize;
            string gongdanApp = tool.sqlAppend2("snNum", gongdan);
            string procedureApp = tool.sqlAppend2("modular", procedure);
            string liaohaoApp = tool.sqlAppend2("item", liaohao);
            string luhaoApp = tool.sqlAppend2("potNum", luhao);
            return dal.InfoBySn(gongdanApp, procedureApp, liaohaoApp, luhaoApp, indexBegin, indexEnd);
        }

        public int InfoBySnCount(string gongdan) {
            return dal.InfoBySnCount(gongdan);
        }
    }
}