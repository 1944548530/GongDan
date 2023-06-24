using IMMSOQAMaintain_Api.BII;
using IMMSOQAMaintain_Api.common;
using IMMSOQAMaintain_Api.Models;
using RepositoryBuild_Api.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMMSOQAMaintain_Api.Controllers.Maintain
{
    public class QuanjMainController : Controller
    {
        // GET: RejDataMain
        QuanjMain_BLL bll = new QuanjMain_BLL();
        public JsonResult QuanjSave(MainModel mainModel)
        {
            var response = new Response();
            try
            {
                int result = bll.QuanjSave(mainModel);
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

        public JsonResult InfoByLuhao(string luhao) {
            var response = new Response();
            try
            {
                IEnumerable<MainModel> infoLi = bll.InfoByLuhao(luhao);
                if (infoLi.Count() == 0)
                {
                    response.SetFailed("查询不到此炉号信息");
                }
                else
                {
                    response.SetSuccess("查询成功");
                    MainModel info = infoLi.First();
                    response.SetData(info);
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

        public JsonResult GetQuanjInfo(string potNum, string date, string dateEnd, int pageNum, int pageSize) {
            var response = new Response();
            try {
                List<MainModel> infoLi = bll.GetQuanjInfo(potNum, date, dateEnd, pageNum, pageSize);
                int count = bll.getInfoCount(potNum, date);
                if (infoLi.Count() > 0)
                {
                    var obj = new
                    {
                        total = count,
                        rows = (from i in infoLi
                                select new MainModel()
                                {
                                    date = i.date,
                                    prodName = i.prodName,
                                    item = i.item,
                                    size = i.size,
                                    potNum = i.potNum,
                                    totalAmount = i.totalAmount,
                                    opAmount = i.opAmount,
                                    inputAmount = i.inputAmount,
                                    erroNum = i.erroNum,
                                    erroPer = i.erroPer,
                                    erroDetail = i.erroDetail,
                                    badNum = i.badNum
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
            catch (Exception e) {
                LoggerHelper.WriteLog(e);
                return Json(new { total = 0, rows = new List<string>() }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult QuanjDel(string date, string item, string potNum, string opAmount, string erroDetail) {
            var response = new Response();
            try
            {
                int result = bll.QuanjDel(date, item, potNum, opAmount, erroDetail);
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
            catch (Exception e)
            {
                LoggerHelper.WriteLog(e);
                response.SetFailed("删除失败，服务器内部错误");
                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ExportExcel(string date, string dateEnd, string potNum)
        {
            try
            {
                byte[] excelArr = bll.ExportExcel(date, dateEnd, potNum);
                return File(excelArr, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }
            catch (Exception e)
            {
                LoggerHelper.WriteLog(e);
                return File(new byte[10], "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }
        }

        public JsonResult QuanjUp(QuanjUpModel data) {
            var response = new Response();
            try
            {
                int result = bll.QuanjUp(data);
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

        //public JsonResult RejDataSearch(int pageNum, int pagesize, string dateBegin, string dateEnd, string segment,string shift)
        //{
        //    var response = new Response();
        //    try
        //    {
        //        IEnumerable<RejDataModel> infoLi = bll.RejDataSearch(pageNum, pagesize, dateBegin, dateEnd, segment, shift);
        //        int count = bll.getInfoCount(dateBegin, dateEnd, segment, shift);
        //        if (infoLi.Count() > 0)
        //        {
        //            var obj = new
        //            {
        //                total = count,
        //                rows = (from i in infoLi
        //                        select new RejDataModel()
        //                        {
        //                            date = i.date,
        //                            shift = i.shift,
        //                            line = i.line,
        //                            model = i.model,
        //                            item = i.item,
        //                            rejNum = i.rejNum,
        //                            holdNum = i.holdNum,
        //                            respDep = i.respDep,
        //                            coporation = i.coporation,
        //                            rejReason = i.rejReason,
        //                            segment = i.segment
        //                        }
        //                       ).ToArray()
        //            };
        //            return Json(obj, JsonRequestBehavior.AllowGet);
        //        }
        //        else {
        //            return Json(new { total = count, rows = new List<string>()}, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    catch (Exception e) {
        //        LoggerHelper.WriteLog(e);
        //        return Json(new { total = 0, rows = new List<string>() }, JsonRequestBehavior.AllowGet);
        //    }
        //}

        //public JsonResult RejDataDel(RejDataModel RejData) {
        //    var response = new Response();
        //    try {
        //        int result = bll.RejDataDel(RejData);
        //        if (result > 0)
        //        {
        //            response.SetSuccess("删除成功");
        //        }
        //        else {
        //            response.SetFailed("删除失败");
        //        }
        //        return Json(response, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception e) {
        //        LoggerHelper.WriteLog(e);
        //        response.SetFailed("服务器内部错误");
        //        return Json(response, JsonRequestBehavior.AllowGet);
        //    }
        //}

        //public JsonResult RejDataUp(RejDataUpModel RejDataUp) {
        //    var response = new Response();
        //    try
        //    {
        //        int result = bll.RejDataUp(RejDataUp);
        //        if (result == 1) {
        //            response.SetSuccess("修改成功");
        //        }
        //        else {
        //            response.SetSuccess("修改失败");
        //        }
        //        return Json(response, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception e) {
        //        LoggerHelper.WriteLog(e);
        //        response.SetFailed("服务器内部错误");
        //        return Json(response, JsonRequestBehavior.AllowGet);
        //    }
        //}

        //public ActionResult ExportExcel(string dateBegin, string dateEnd, string modular) {
        //    try
        //    {
        //        byte[] excelArr = bll.ExportExcel(dateBegin, dateEnd, modular);
        //        return File(excelArr, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        //    }
        //    catch (Exception e) {
        //        LoggerHelper.WriteLog(e);
        //        return File(new byte[10], "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        //    }
        //}

        //public string FAISave() {
        //    string sqlCmd = "update dncuser set Password = '321' where LoginName = 'fangjun'";
        //    int result = MysqlHelper.Execute(sqlCmd);
        //    string json = "";
        //    if (result == 1)
        //    {
        //        json = "{\"result\":\"修改成功\"}";
        //    }
        //    else {
        //        json = "{\"result\":\"修改失败\"}";
        //    }
        //    return json;

        //}

        //public string FAISearch() {
        //    string sqlCmd = "select LoginName, Password from dncuser";
        //    DataTable dt = MysqlHelper.SqlDataTable(sqlCmd);
        //    return "{\"result\":\"查询成功\"}";
        //}

    }
}