using IMMSOQAMaintain_Api.BII;
using IMMSOQAMaintain_Api.common;
using IMMSOQAMaintain_Api.Models;
using RepositoryBuild_Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMMSOQAMaintain_Api.Controllers.Maintain
{
    public class ChaXunController : Controller
    {
        ChaXun_BLL bll = new ChaXun_BLL();
        // GET: ChaXun
        public JsonResult InfoBySn(string gongdan, string procedure, string liaohao, string luhao, int pageNum, int pagesize)
        {
            var response = new Response();
            try
            {
                List<MainModel> infoLi = bll.InfoBySn(gongdan, procedure, liaohao, luhao, pageNum, pagesize);
                int count = bll.InfoBySnCount(gongdan);
                if (infoLi.Count() > 0)
                {
                    var obj = new
                    {
                        total = count,
                        rows = (from i in infoLi
                                select new MainModel()
                                {
                                    date = i.date,
                                    modular = i.modular,
                                    prodName = i.prodName,
                                    item = i.item,
                                    size = i.size,
                                    potNum = i.potNum,
                                    snNum = i.snNum,
                                    totalAmount = i.totalAmount,
                                    leftNum = i.leftNum,
                                    erroTotal = i.erroTotal,
                                    erroTotalPer = i.erroTotalPer,
                                    erroPer = i.erroPer,
                                    opAmount = i.opAmount,
                                    inputAmount = i.inputAmount,
                                    erroNum = i.erroNum,
                                    erroDetail = i.erroDetail,
                                    hongkao = i.hongkao,
                                    lastProcOKNum = i.lastProcOKNum
                                }
                               ).ToArray()
                    };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { total = count, rows = new List<string>() }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                LoggerHelper.WriteLog(e);
                return Json(new { total = 0, rows = new List<string>() }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}