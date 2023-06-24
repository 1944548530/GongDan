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
    public class GunyMainController : Controller
    {
        GunyMain_BLL bll = new GunyMain_BLL();
        // GET: GunyMain
        public JsonResult GunySave(MainModel mainModel)
        {
            var response = new Response();
            try
            {
                int result = bll.GunySave(mainModel);
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
            catch (Exception e) {
                LoggerHelper.WriteLog(e);
                response.SetFailed("服务器内部错误");
                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetOtherInfo(string modular, string snNum, string date, string dateEnd, int pageNum, int pagesize) {
            var response = new Response();
            try
            {
                List<MainModel> infoLi = bll.GetOtherInfo(modular, snNum, date, dateEnd, pageNum, pagesize);
                int count = bll.OtherInfoCount(modular, snNum, date);
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

        public JsonResult OtherInfoDel(string date, string snNum, string modular, string opAmount, string erroDetail) {
            var response = new Response();
            try
            {
                int result = bll.OtherInfoDel(date, snNum, modular, opAmount, erroDetail);
                if (result == 1)
                {
                    response.SetSuccess("删除成功");
                }
                else
                {
                    response.SetFailed("删除失败");
                }
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e) {
                LoggerHelper.WriteLog(e);
                response.SetFailed("删除失败，服务器内部错误");
                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult OtherInfoUp(OtherUpModel data) {
            var response = new Response();
            try
            {
                int result = bll.OtherInfoUp(data);
                if (result == 1)
                {
                    response.SetSuccess("更新成功");
                }
                else if (result == -1) {
                    response.SetFailed("工单剩余数量<0,请核实工单号，若正确请与助理联系！");
                }
                else
                {
                    response.SetFailed("更新失败");
                }
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                LoggerHelper.WriteLog(e);
                response.SetFailed("更新失败，服务器内部错误");
                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }
         
        public ActionResult ExportExcel(string modular, string date, string dateEnd, string snNum)
        {
            try
            {
                byte[] excelArr = bll.ExportExcel(modular, date, dateEnd, snNum);
                return File(excelArr, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }
            catch (Exception e)
            {
                LoggerHelper.WriteLog(e);
                return File(new byte[10], "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }
        }
    }
}