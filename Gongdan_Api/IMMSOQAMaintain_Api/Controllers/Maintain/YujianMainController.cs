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
    public class YujianMainController : Controller
    {
        YujianMain_BLL bll = new YujianMain_BLL();
        // GET: YujianMain
        public JsonResult YujianSave(MainModel mainData)
        {
            var response = new Response();
            try
            {
                int result = bll.YujianSave(mainData);
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

        public JsonResult ChujianSave(MainModel mainData) {
            var response = new Response();
            try
            {
                int result = bll.ChujianSave(mainData);
                if (result == 1)
                {
                    response.SetSuccess("保存成功");
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

        public JsonResult FujianSave(MainModel mainData)
        {
            var response = new Response();
            try
            {
                int result = bll.FujianSave(mainData);
                if (result == 1)
                {
                    response.SetSuccess("保存成功");
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