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
    public class QingxiMainController : Controller
    {
        QingxiMain_BLL bll = new QingxiMain_BLL();
        // GET: QingxiMain
        public JsonResult GetInfoBySn(string sn) {
            var response = new Response();
            try
            {
                IEnumerable<MainModel> infoLi = bll.GetInfoBySn(sn);
                if (infoLi.Count() == 0)
                {
                    response.SetFailed("查询不到此工单信息");
                }
                else
                {
                    response.SetSuccess("查询成功");
                    MainModel info = infoLi.First();
                    response.SetData(info);
                }
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e) {
                LoggerHelper.WriteLog(e);
                response.SetFailed("服务器内部错误");
                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult QingxiSave(MainModel mainData)
        {
            var response = new Response();
            try
            {
                int result = bll.QingxiSave(mainData);
                if (result == 1)
                {
                    response.SetSuccess("保存成功");
                }
                else if (result == -1) {
                    response.SetFailed("工单剩余数量<0,请核实工单号，若正确请与助理联系！");
                }
                else
                {
                    response.SetFailed("保存失败");
                }
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                LoggerHelper.WriteLog(e);
                response.SetFailed("服务器内部错误");
                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }
    }
}