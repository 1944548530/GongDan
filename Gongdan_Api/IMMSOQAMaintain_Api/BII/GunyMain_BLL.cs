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
    public class GunyMain_BLL
    {
        GunyMain_DAL dal = new GunyMain_DAL();
        Tool tool = new Tool();
        public int GunySave(MainModel model) {
            string lmdate = DateTime.Now.ToString("yyyy-MM-dd");
            string lmtime = DateTime.Now.ToString("HH:mm:ss");
            string lmuser = Environment.UserName;
            model.date = lmdate;
            model.lmdate = lmdate;
            model.lmtime = lmtime;
            model.lmuser = lmuser;
            float erroNum = 0;
            string erroDetail = "";
            if (string.IsNullOrWhiteSpace(model.errBlankName))
            {
                erroNum = float.Parse(model.erro1) + float.Parse(model.erro2);
                erroDetail = "滚坏: " + model.erro1 + ", 其他: " + model.erro2 + "";
            }
            else
            {
                erroNum = float.Parse(model.erro1) + float.Parse(model.erro2) + float.Parse(model.errBlankNum);
                erroDetail = "滚坏: " + model.erro1 + ", 其他: " + model.erro2 + ", " + model.errBlankName + ": " + model.errBlankNum;
            }
            model.erroNum = erroNum.ToString();
            model.erroDetail = erroDetail;
            DataTable dt = dal.infoExist(model);
            int erroTotal = int.Parse(model.erroNum);
            int inputNum = int.Parse(model.opAmount) + int.Parse(model.erroNum);
            int leftNum = int.Parse(model.totalAmount) - inputNum;
            if (dt.Rows.Count > 0)
            {
                //erroTotal = int.Parse(dt.Rows[0]["erroTotal"].ToString());
                leftNum = int.Parse(dt.Rows[0]["leftNum"].ToString()) - inputNum;
                //erroTotal += int.Parse(model.erroNum);
            }
            int lastProcOKNum = int.Parse(model.opAmount);
            for (int i = 0; i < dt.Rows.Count; i++) {
                lastProcOKNum += int.Parse(dt.Rows[i]["opAmount"].ToString());
                erroTotal += int.Parse(dt.Rows[i]["erroNum"].ToString());
            }
            model.lastProcOKNum = lastProcOKNum.ToString();
            model.erroTotal = erroTotal.ToString();
            float erroTPerS = tool.perCal1(erroTotal, int.Parse(model.totalAmount));
            float erroPerS = tool.perCal1(int.Parse(model.erroNum), inputNum);
            model.erroTotalPer = erroTPerS + "%";
            model.erroPer = erroPerS + "%";
            model.leftNum = leftNum.ToString();
            int result = 0;
            if (int.Parse(model.leftNum) < 0) {
                result = -1;
            }
            else {
                result = dal.GunySave(model);
            }
             
            return result;
        }

        public List<MainModel> GetOtherInfo(string modular, string snNum, string date, string dateEnd, int pageNum, int pagesize) {
            if (string.IsNullOrEmpty(date))
            {
                date = "";
            }
            int indexBegin = (pageNum - 1) * pagesize + 1;
            int indexEnd = pageNum * pagesize;
            string snSqlApp = tool.sqlAppend2("snNum", snNum);
            string dateSqlApp = tool.sqlAppend(date, dateEnd, "date");
            return dal.GetOtherInfo(modular, indexBegin, indexEnd, snSqlApp, dateSqlApp);
        }

        public int OtherInfoCount(string modular, string snNum, string date) {
            if (string.IsNullOrEmpty(date))
            {
                date = "";
            }
            string potSqlApp = tool.sqlAppend2("snNum", snNum);
            string dateSqlApp = tool.sqlAppend2("date", date);
            return dal.OtherInfoCount(modular, potSqlApp, dateSqlApp);
        }

        public int OtherInfoDel(string date, string snNum, string modular, string opAmount, string erroDetail) {
            date = date.Substring(0, 10);
            int result = dal.OtherInfoDel(date, snNum, modular, opAmount, erroDetail);
            return result;
        }

        public int OtherInfoUp(OtherUpModel model) {
            float erroNum = 0;
            string erroDetail = "";
            if (model.modular == "滚圆")
            {
                if (string.IsNullOrWhiteSpace(model.GYkbName))
                {
                    erroNum = float.Parse(model.ghNum) + float.Parse(model.qtNum);
                    erroDetail = "滚坏: " + model.ghNum + ", 其他: " + model.qtNum + "";
                }
                else
                {
                    erroNum = float.Parse(model.ghNum) + float.Parse(model.qtNum) + float.Parse(model.GYkbNum);
                    erroDetail = "滚坏: " + model.ghNum + ", 其他: " + model.qtNum + ", " + model.GYkbName + ": " + model.GYkbNum;
                }
            }
            else if (model.modular == "预检" || model.modular == "初检" || model.modular == "复检")
            {
                if (string.IsNullOrWhiteSpace(model.GYkbName))
                {
                    erroNum = float.Parse(model.gsNum) + float.Parse(model.mdNum) + float.Parse(model.bbNum) + float.Parse(model.huabNum) + float.Parse(model.tuodNum) + float.Parse(model.qtNum);
                    erroDetail = "刮伤: " + model.gsNum + ", 麻点: " + model.mdNum + ", 崩边: " + model.bbNum + ", 花边: " + model.huabNum + ", 脱点: " + model.tuodNum + ", 其他: " + model.qtNum + "";
                }
                else
                {
                    erroNum = float.Parse(model.gsNum) + float.Parse(model.mdNum) + float.Parse(model.bbNum) + float.Parse(model.huabNum) + float.Parse(model.tuodNum) + float.Parse(model.qtNum) + float.Parse(model.GYkbNum);
                    erroDetail = "刮伤: " + model.gsNum + ", 麻点: " + model.mdNum + ", 崩边: " + model.bbNum + ", 花边: " + model.huabNum + ", 脱点: " + model.tuodNum + ", 其他: " + model.qtNum + ", " + model.GYkbName + ": " + model.GYkbNum;
                }
            }
            else if (model.modular == "裸片性能" || model.modular == "套圈性能")
            {
                if (string.IsNullOrWhiteSpace(model.GYkbName))
                {
                    erroNum = float.Parse(model.hkNum) + float.Parse(model.qtNum);
                    erroDetail = "短需烘烤: " + model.hkNum + ", 其他: " + model.qtNum + "";
                }
                else
                {
                    erroNum = float.Parse(model.hkNum) + float.Parse(model.qtNum) + float.Parse(model.GYkbNum);
                    erroDetail = "短需烘烤: " + model.hkNum + ", 其他: " + model.qtNum + ", " + model.GYkbName + ": " + model.GYkbNum;
                }

            }
            else {
                if (string.IsNullOrWhiteSpace(model.GYkbName))
                {
                    erroNum = float.Parse(model.gsNum) + float.Parse(model.dpNum) + float.Parse(model.qtNum);
                    erroDetail = "刮伤: " + model.gsNum + ", 丢片: " + model.dpNum + ", 其他: " + model.qtNum + "";
                }
                else
                {
                    erroNum = float.Parse(model.gsNum) + float.Parse(model.dpNum) + float.Parse(model.qtNum) + float.Parse(model.GYkbNum);
                    erroDetail = "刮伤: " + model.gsNum + ", 丢片: " + model.dpNum + ", 其他: " + model.qtNum + ", " + model.GYkbName + ": " + model.GYkbNum;
                }
            }
            model.erroNum = erroNum.ToString();
            model.erroDetail = erroDetail;
            DataTable dt = dal.infoExistUp(model);
            int erroTotal = int.Parse(model.erroNum);
            int inputNum = int.Parse(model.outputNum) + int.Parse(model.erroNum);
            int leftNum = int.Parse(model.gdAmount) - inputNum;
            if (dt.Rows.Count > 1)
            {
                leftNum = int.Parse(dt.Rows[1]["leftNum"].ToString()) - inputNum;
                erroTotal += int.Parse(model.erroNum);
            }
            model.erroTotal = erroTotal.ToString();
            float erroTPerS = tool.perCal1(erroTotal, int.Parse(model.gdAmount));
            float erroPerS = tool.perCal1(int.Parse(model.erroNum), inputNum);
            model.erroTotalPer = erroTPerS + "%";
            model.erroPer = erroPerS + "%";
            model.dateOld = model.dateOld.Substring(0, 10);
            model.dateUp = model.dateUp.Substring(0, 10);
            model.leftNum = leftNum.ToString();
            int result = 0;
            if (int.Parse(model.leftNum) < 0)
            {
                result = -1;
            }
            else {
                result = dal.OtherInfoUp(model);
                if (model.gdAmount != model.gdAmountOld) {
                    dal.totalAmountUp(model);
                }
                
            }
            return result;
        }

        public byte[] ExportExcel(string modular, string date, string dateEnd, string snNum)
        {
            if (string.IsNullOrEmpty(date))
            {
                date = "";
            }
            string snNumSqlApp = tool.sqlAppend2("snNum", snNum);
            string dateSqlApp = tool.sqlAppend(date, dateEnd, "date");
            byte[] arr = dal.ExportExcel(modular, snNumSqlApp, dateSqlApp);
            return arr;
        }
    }
}