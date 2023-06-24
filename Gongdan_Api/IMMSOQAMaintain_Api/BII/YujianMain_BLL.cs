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
    public class YujianMain_BLL
    {
        YujianMain_DAL dal = new YujianMain_DAL();
        Tool tool = new Tool();
        public int YujianSave(MainModel model) {
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
                erroNum = float.Parse(model.erro1) + float.Parse(model.erro2) + float.Parse(model.erro3) + float.Parse(model.erro5) + float.Parse(model.erro6) + float.Parse(model.erro4);
                erroDetail = "刮伤: " + model.erro1 + ", 麻点: " + model.erro2 + ", 崩边: " + model.erro3 + ", 花边: " + model.erro5 + ", 脱点: " + model.erro6 + ", 其他: " + model.erro4 + "";
            }
            else {
                erroNum = float.Parse(model.erro1) + float.Parse(model.erro2) + float.Parse(model.erro3) + float.Parse(model.erro4) + float.Parse(model.erro5) + float.Parse(model.erro6) + float.Parse(model.errBlankNum);
                erroDetail = "刮伤: " + model.erro1 + ", 麻点: " + model.erro2 + ", 崩边: " + model.erro3 + ", 花边: " + model.erro5 + ", 脱点: " + model.erro6 + ", 其他: " + model.erro4 + ", " + model.errBlankName + ": " + model.errBlankNum;
            }
            
            model.erroNum = erroNum.ToString();
            model.erroDetail = erroDetail;

            DataTable dt = dal.infoExist(model);
            DataTable lastProDt = dal.LastProcInfo(model);
            int erroTotal = int.Parse(model.erroNum);
            int inputNum = int.Parse(model.opAmount) + int.Parse(model.erroNum);
            int inputTotal = inputNum;
            int lastProOkNum = 0;
            //int leftNum = int.Parse(model.totalAmount) - inputNum;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                inputTotal += int.Parse(dt.Rows[i]["inputAmount"].ToString());
                erroTotal += int.Parse(dt.Rows[i]["erroNum"].ToString());
            }
            //if (dt.Rows.Count > 0)
            //{
            //    erroTotal = int.Parse(dt.Rows[0]["erroTotal"].ToString());
            //    //leftNum = int.Parse(dt.Rows[0]["leftNum"].ToString()) - inputNum;
            //    erroTotal += int.Parse(model.erroNum);
            //}
            for(int i = 0; i < lastProDt.Rows.Count; i ++) 
            {
                lastProOkNum += int.Parse(lastProDt.Rows[i]["opAmount"].ToString());
            }
            model.lastProcOKNum = lastProOkNum.ToString();
            //model.leftNum = leftNum.ToString();
            model.leftNum = (lastProOkNum - inputTotal).ToString();
            model.erroTotal = erroTotal.ToString();
            //float erroTPerS = tool.perCal1(erroTotal, int.Parse(model.totalAmount));
            float erroTPerS = tool.perCal1(erroTotal, lastProOkNum);
            float erroPerS = tool.perCal1(int.Parse(model.erroNum), inputNum);
            model.erroTotalPer = erroTPerS + "%";
            model.erroPer = erroPerS + "%";
            int result = 0;
            if (int.Parse(model.leftNum) < 0)
            {
                result = -1;
            }
            else {
                result = dal.infoSave(model); 
            }
            
            return result;
        }

        public int ChujianSave(MainModel model)
        {
            string lmdate = DateTime.Now.ToString("yyyy-MM-dd");
            string lmtime = DateTime.Now.ToString("HH:mm:ss");
            string lmuser = Environment.UserName;
            model.date = lmdate;
            model.lmdate = lmdate;
            model.lmtime = lmtime;
            model.lmuser = lmuser;
            DataTable dt = dal.ChuinfoExist(model);
            int result = 0;
            if (dt.Rows.Count > 0)
            {
                float amountOld = float.Parse(dt.Rows[0]["opAmount"].ToString());
                float amountNew = amountOld + float.Parse(model.opAmount);
                model.opAmount = amountNew.ToString();
                result = dal.ChuinfoSaveAgain(model);
            }
            else
            {
                float erroNum = 0;
                string erroDetail = "";
                if (string.IsNullOrWhiteSpace(model.errBlankName))
                {
                    erroNum = float.Parse(model.erro1) + float.Parse(model.erro2) + float.Parse(model.erro3) + float.Parse(model.erro4);
                    erroDetail = "刮伤: " + model.erro1 + ", 麻点: " + model.erro2 + ", 崩边: " + model.erro3 + ", 其他: " + model.erro4 + "";
                }
                else {
                    erroNum = float.Parse(model.erro1) + float.Parse(model.erro2) + float.Parse(model.erro3) + float.Parse(model.erro4) + float.Parse(model.errBlankNum);
                    erroDetail = "刮伤: " + model.erro1 + ", 麻点: " + model.erro2 + ", 崩边: " + model.erro3 + ", 其他: " + model.erro4 + ", " + model.errBlankName + ": " + model.errBlankNum;
                }
                
                model.erroNum = erroNum.ToString();
                model.erroDetail = erroDetail;
                result = dal.ChuinfoSave(model);
            }
            return result;
        }

        public int FujianSave(MainModel model)
        {
            string lmdate = DateTime.Now.ToString("yyyy-MM-dd");
            string lmtime = DateTime.Now.ToString("HH:mm:ss");
            string lmuser = Environment.UserName;
            model.date = lmdate;
            model.lmdate = lmdate;
            model.lmtime = lmtime;
            model.lmuser = lmuser;
            DataTable dt = dal.FuinfoExist(model);
            int result = 0;
            if (dt.Rows.Count > 0)
            {
                float amountOld = float.Parse(dt.Rows[0]["opAmount"].ToString());
                float amountNew = amountOld + float.Parse(model.opAmount);
                model.opAmount = amountNew.ToString();
                result = dal.FuinfoSaveAgain(model);
            }
            else
            {
                float erroNum = 0;
                string erroDetail = "";
                if (string.IsNullOrWhiteSpace(model.errBlankName))
                {
                    erroNum = float.Parse(model.erro1) + float.Parse(model.erro2) + float.Parse(model.erro3) + float.Parse(model.erro4);
                    erroDetail = "刮伤: " + model.erro1 + ", 麻点: " + model.erro2 + ", 崩边: " + model.erro3 + ", 其他: " + model.erro4 + "";
                }
                else {
                    erroNum = float.Parse(model.erro1) + float.Parse(model.erro2) + float.Parse(model.erro3) + float.Parse(model.erro4) + float.Parse(model.errBlankNum);
                    erroDetail = "刮伤: " + model.erro1 + ", 麻点: " + model.erro2 + ", 崩边: " + model.erro3 + ", 其他: " + model.erro4 + ", " + model.errBlankName + ": " + model.errBlankNum;
                }
                
                model.erroNum = erroNum.ToString();
                model.erroDetail = erroDetail;
                result = dal.FuinfoSave(model);
            }
            return result;
        }
    }
}