using IMMSOQAMaintain_Api.common;
using IMMSOQAMaintain_Api.DAL;
using IMMSOQAMaintain_Api.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace IMMSOQAMaintain_Api.BII
{
    
    public class QuanjMain_BLL
    {
        Tool tool = new Tool();
        QuanjMain_DAL dal = new QuanjMain_DAL();
        public int QuanjSave(MainModel model) {
            string lmdate = DateTime.Now.ToString("yyyy-MM-dd");
            string lmtime = DateTime.Now.ToString("HH:mm:ss");
            string lmuser = Environment.UserName;
            int erroNum = 0;
            string erroDetail = "";
            if (string.IsNullOrWhiteSpace(model.errBlankName))
            {
                erroNum = int.Parse(model.erro1) + int.Parse(model.erro2);
                erroDetail = "短需烘烤: " + model.erro1 + ", 其他: " + model.erro2 + "";
            }
            else {
                erroNum = int.Parse(model.erro1) + int.Parse(model.erro2) + int.Parse(model.errBlankNum);
                erroDetail = "短需烘烤: " + model.erro1 + ", 其他: " + model.erro2 + ", " + model.errBlankName + ": " + model.errBlankNum;
            }
            int inputNum = int.Parse(model.opAmount) + erroNum;
            float erroPer = tool.perCal1(erroNum, inputNum);
            model.erroPer = erroPer + "%";
            model.erroNum = erroNum.ToString();
            model.erroDetail = erroDetail;
            model.date = lmdate;
            model.lmdate = lmdate;
            model.lmtime = lmtime;
            model.lmuser = lmuser;
            return dal.QuanjSave(model);
        }

        public IEnumerable<MainModel> InfoByLuhao(string luhao) {
            IEnumerable<MainModel> infoLi = dal.InfoByLuhao(luhao);
            return infoLi;
        }

        public List<MainModel> GetQuanjInfo(string potNum, string date, string dateEnd, int pageNum, int pageSize) {
            if (string.IsNullOrEmpty(date))
            {
                date = "";
            }
            int indexBegin = (pageNum - 1) * pageSize + 1;
            int indexEnd = pageNum * pageSize;
            string potSqlApp = tool.sqlAppend2("potNum", potNum);
            string dateSqlApp = tool.sqlAppend(date, dateEnd, "date");
            return dal.GetQuanjInfo(indexBegin, indexEnd, potSqlApp, dateSqlApp);
        }

        public int getInfoCount(string potNum, string date) {
            if (string.IsNullOrEmpty(date))
            {
                date = "";
            }
            string potSqlApp = tool.sqlAppend2("potNum", potNum);
            string dateSqlApp = tool.sqlAppend2("date", date);
            return dal.getInfoCount(potSqlApp, dateSqlApp);
        }

        public int QuanjDel(string date, string item, string potNum, string opAmount, string erroDetail) {
            date = date.Substring(0, 10);
            int result = dal.QuanjDel(date, item, potNum, opAmount, erroDetail);
            return result;
        }

        public int QuanjUp(QuanjUpModel model) {
            int erroNum = 0;
            string erroDetail = "";
            if (string.IsNullOrWhiteSpace(model.QJkbName))
            {
                erroNum = int.Parse(model.QJhkNum) + int.Parse(model.QJqtNum);
                erroDetail = "短需烘烤: " + model.QJhkNum + ", 其他: " + model.QJqtNum + "";
            }
            else
            {
                erroNum = int.Parse(model.QJhkNum) + int.Parse(model.QJqtNum) + int.Parse(model.QJkbNum);
                erroDetail = "短需烘烤: " + model.QJhkNum + ", 其他: " + model.QJqtNum + ", " + model.QJkbName + ": " + model.QJkbNum;
            }
            float erroPer = tool.perCal1(erroNum, int.Parse(model.opAmount));
            model.erroPer = erroPer + "%";
            model.erroNum = erroNum.ToString();
            model.erroDetail = erroDetail;
            model.dateOld = model.dateOld.Substring(0, 10);
            model.dateUp = model.dateUp.Substring(0, 10);
            int result = dal.QuanjUp(model);
            return result;
        }

        public byte[] ExportExcel(string date, string dateEnd, string potNum)
        {
            if (string.IsNullOrEmpty(date))
            {
                date = "";
            }
            string snNumSqlApp = tool.sqlAppend2("potNum", potNum);
            string dateSqlApp = tool.sqlAppend(date, dateEnd, "date");
            byte[] arr = dal.ExportExcel(snNumSqlApp, dateSqlApp);
            return arr;
        }

        //public IEnumerable<RejDataModel> RejDataSearch(int pageNum, int pagesize, string dateBegin, string dateEnd, string segment, string shift) {
        //    if (string.IsNullOrEmpty(dateBegin)) {
        //        dateBegin = "";
        //    }
        //    if (string.IsNullOrEmpty(dateEnd)) {
        //        dateEnd = "";
        //    }
        //    string sqlApp = tool.sqlAppend(dateBegin, dateEnd, "date");
        //    int indexBegin = (pageNum - 1) * pagesize + 1;
        //    int indexEnd = pageNum * pagesize;
        //    IEnumerable<RejDataModel> modelLi = dal.RejDataSearch(indexBegin, indexEnd, sqlApp, segment, shift);
        //    return modelLi;
        //}

        //public int getInfoCount(string dateBegin, string dateEnd, string segment, string shift) {
        //    if (string.IsNullOrEmpty(dateBegin))
        //    {
        //        dateBegin = "";
        //    }
        //    if (string.IsNullOrEmpty(dateEnd))
        //    {
        //        dateEnd = "";
        //    }
        //    string sqlApp = tool.sqlAppend(dateBegin, dateEnd, "date");
        //    int count = dal.getInfoCount(sqlApp, segment, shift);
        //    return count;
        //}

        //public int RejDataDel(RejDataModel rejData) {
        //    int result = dal.RejDataDel(rejData);
        //    return result;
        //}

        //public int RejDataUp(RejDataUpModel model) {
        //    string week = tool.getWeekid(model.dateUp);
        //    string month = tool.getMonthid(model.dateUp);
        //    model.weekid = week;
        //    model.monthid = month;
        //    int result = dal.RejDataUp(model);
        //    return result;
        //}

        //public byte[] ExportExcel(string dateBegin, string dateEnd, string modular) {
        //    if (string.IsNullOrEmpty(dateBegin))
        //    {
        //        dateBegin = "";
        //    }
        //    if (string.IsNullOrEmpty(dateEnd))
        //    {
        //        dateEnd = "";
        //    }
        //    string sqlApp = tool.sqlAppend(dateBegin, dateEnd, "date");
        //    byte[] arr = dal.ExportExcel(sqlApp, modular);
        //    return arr;
        //}
    }
}