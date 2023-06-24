using IMMSOQAMaintain_Api.common;
using IMMSOQAMaintain_Api.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace IMMSOQAMaintain_Api.DAL
{
    public class QuanjMain_DAL
    {
        Tool tool = new Tool();
        public int QuanjSave(MainModel model) {
            string sqlCmd = @"insert into snMain values
                              (
                                '半成品全检', '" + model.date + @"', '" + model.prodName + @"', '" + model.item + @"', '" + model.size + @"', '" + model.potNum + @"',
                                '', '" + model.totalAmount + @"', '', '', '', '" + model.badNum + @"', '" + model.opAmount + @"', '" + model.erroNum + @"', '" + model.erroPer + @"', '" + model.erroDetail + @"',
                                'Y', '" + model.lmdate + @"', '" + model.lmtime + @"', '" + model.lmuser + @"', '', ''  
                              )";
            int result = SqlHelper<MainModel>.Execute(sqlCmd);
            return result;
        }

        public IEnumerable<MainModel> InfoByLuhao(string luhao) {
            string sqlCmd = @"select prodName, item, size, totalAmount, badNum
                             from snMain where modular = '半成品全检' and status = 'Y' and potNum = '" + luhao + @"'";
            IEnumerable<MainModel> infoLi = SqlHelper<MainModel>.Query(sqlCmd);
            return infoLi;
        }

        public List<MainModel> GetQuanjInfo(int indexBegin, int indexEnd, string potSqlApp, string dateSqlApp) {
            //(convert(int, b.opAmount) + convert(int, b.erroNum)) inputAmount
            //(CONVERT(opAmount, SIGNED) + CONVERT(erroNum, SIGNED)) inputAmount
            string sqlCmd = @"select (b.date + ' ' + b.lmtime)date, b.prodName, b.item, b.size, b.potNum, b.totalAmount, b.opAmount, b.erroNum, b.erroDetail, 
                              (convert(int, b.opAmount) + convert(int, b.erroNum)) inputAmount, b.erroPer, b.badNum from
                             (
                                select ROW_NUMBER() over(order by lmdate desc, lmtime desc) num,
                                date, prodName, item, size, potNum, totalAmount, opAmount, erroNum, erroPer, erroDetail, badNum, lmtime  
                                from(select * from snMain where modular = '半成品全检' and status = 'Y' " + potSqlApp + @" " + dateSqlApp + @")a
                             )b where b.num between '" + indexBegin + @"' and '" + indexEnd + @"'";
            List<MainModel> infoLi = SqlHelper<MainModel>.Query(sqlCmd).ToList();
            return infoLi;
        }

        public int getInfoCount(string potSqlApp, string dateSqlApp) {
            string sqlCmd = @"select date from snMain where modular = '半成品全检' and status = 'Y' " + potSqlApp + @" " + dateSqlApp + @"";
            DataTable dt = SqlHelper<MainModel>.sqlTable(sqlCmd);
            return dt.Rows.Count;
        }

        public int QuanjDel(string date, string item, string potNum, string opAmount, string erroDetail) {
            string sqlCmd = @"update snMain set status = 'N'
                             where date = '" + date + @"' and modular = '半成品全检' and potNum = '" + potNum + @"' and item = '" + item + @"'
                             and opAmount = '" + opAmount + @"' and erroDetail = '" + erroDetail + @"'";
            int result = SqlHelper<MainModel>.Execute(sqlCmd);
            return result;
        }

        public int QuanjUp(QuanjUpModel model) {
            string sqlCmd = @"update snMain set 
                              date = '" + model.dateUp + @"', prodName = '" + model.pinMing + @"', item = '" + model.item + @"', size = '" + model.size + @"',erroDetail = '" + model.erroDetail + @"',
                              potNum = '" + model.luhao + @"', totalAmount= '" + model.testNum + @"', opAmount = '" + model.opAmount + @"', erroNum = '" + model.erroNum + @"', erroPer = '" + model.erroPer + @"', badNum = '" + model.badNum + @"'
                              where modular = '半成品全检' and date = '" + model.dateOld + @"' and potNum = '" + model.luhaoOld + @"' and opAmount = '" + model.opAmountOld + @"' and erroDetail = '" + model.erroDetailOld + @"' and status = 'Y'";
            int result = SqlHelper<MainModel>.Execute(sqlCmd);
            return result;
        }

        public byte[] ExportExcel(string snNumSqlApp, string dateSqlApp)
        {
            string sqlCmd = @"select (date + ' ' + lmtime) 日期, modular 工序, prodName 品名, item 成品料号, size 尺寸, potNum 炉号, totalAmount 测试数量, badNum 不良数量,
                              (CONVERT(int, opAmount) + CONVERT(int,erroNum)) 投入数量, opAmount 良品数量, erroNum 不良现象, erroPer 不良率, erroDetail 不良现象明细
                              from snMain
                              where status = 'Y' and modular = '半成品全检' " + snNumSqlApp + @" " + dateSqlApp + @"";
            DataTable dt = SqlHelper<MainModel>.sqlTable(sqlCmd);
            DataColumn col1 = new DataColumn();
            col1.DataType = System.Type.GetType("System.String");
            col1.ColumnName = "短需烘烤";
            col1.DefaultValue = "";

            DataColumn col2 = new DataColumn();
            col2.DataType = System.Type.GetType("System.String");
            col2.ColumnName = "其他";
            col2.DefaultValue = "";

            DataColumn col3 = new DataColumn();
            col3.DataType = System.Type.GetType("System.String");
            col3.ColumnName = "空白";
            col3.DefaultValue = "";
            dt.Columns.Add(col1);
            dt.Columns.Add(col2);
            dt.Columns.Add(col3);
            for (int i = 0; i < dt.Rows.Count; i++) {
                string erroDetailL = dt.Rows[i]["不良现象明细"].ToString();
                string erroDetailS = erroDetailL.Replace("短需烘烤:", "").Replace("其他:", ""); 
                List<string> erroLi = erroDetailS.Split(',').ToList();
                string col1Str = erroLi[0].Trim();
                string col2Str = erroLi[1].Trim();
                string col3Str = "";
                if (erroLi.Count > 2) {
                    col3Str = erroLi[2].Trim();
                }
                dt.Rows[i]["短需烘烤"] = col1Str;
                dt.Rows[i]["其他"] = col2Str;
                dt.Rows[i]["空白"] = col3Str;
            }
            dt.Columns.Remove("不良现象明细");
            byte[] ary = ExcelHelper.GetExcel(dt, "sheet1", "A1:O1", "全检");
            return ary;
        }
        //public DataTable RejDataExist(RejDataModel model) {
        //    string sqlCmd = @"select date from IMMSOQARejData 
        //                    where modular = 'RejData' and date = '" + model.date + @"' and status = 'Y' and 
        //                    shift = '" + model.shift + @"' and line = '" + model.line + @"' and rejNum = " + model.rejNum + @" and holdNum = " + model.holdNum + @" and rejReason = '" + model.rejReason + @"'";
        //    DataTable dt = SqlHelper<RejDataModel>.sqlTable(sqlCmd);
        //    return dt;
        //}

        //public IEnumerable<RejDataModel> RejDataSearch(int indexBegin, int indexEnd, string sqlApp, string segment, string shift) {
        //    string segApp = tool.sqlAppend2("segment", segment);
        //    string shiftApp = tool.sqlAppend2("shift", shift);
        //    string sqlCmd = @"select b.date, b.shift, b.line, b.model, b.item, b.rejReason, b.rejNum, b.holdNum, b.coporation, b.respDep, b.segment from 
        //                  (  select ROW_NUMBER() over(order by lmdate desc, lmtime desc) num, * from 
        //                    (
        //                        select * from IMMSOQARejData where modular = 'RejData' and status = 'Y' " + sqlApp + @" " + segApp + @" " + shiftApp + @"
        //                    )a
        //                  )b where b.num between '" + indexBegin + @"' and '" + indexEnd + @"'";

        //    IEnumerable<RejDataModel> modelLi = SqlHelper<RejDataModel>.Query(sqlCmd);
        //    return modelLi;
        //}

        //public int getInfoCount(string sqlApp, string segment, string shift) {
        //    string segApp = tool.sqlAppend2("segment", segment);
        //    string shiftApp = tool.sqlAppend2("shift", shift);
        //    string sqlCmd = @"select date from IMMSOQARejData where modular = 'RejData' and status = 'Y' " + sqlApp + @" " + segApp + @" " + shiftApp + @"";
        //    DataTable dt = SqlHelper<RejDataModel>.sqlTable(sqlCmd);
        //    return dt.Rows.Count;
        //}

        //public int RejDataDel(RejDataModel model) {
        //    string sqlCmd = @"update IMMSOQARejData set status = 'N' where date = '" + model.date + @"' and shift = '" + model.shift + @"' 
        //                    and line = '" + model.line + @"' and model = '" + model.model + @"' and item = '" + model.item + @"' and rejReason = '" + model.rejReason + @"'";
        //    int result = SqlHelper<RejDataModel>.Execute(sqlCmd);
        //    return result;
        //}

        //public int RejDataUp(RejDataUpModel rejDataUp) {
        //    string sqlCmd = @"update IMMSOQARejData set date = '" + rejDataUp.dateUp + @"', shift = '" + rejDataUp.shiftUp + @"', line = '" + rejDataUp.lineUp + @"', model = '" + rejDataUp.modelUp + @"', item = '" + rejDataUp.itemUp + @"', segment = '" + rejDataUp.segmentUp + @"',
        //                    rejReason = '" + rejDataUp.feedReasonUp + @"', rejNum = " + rejDataUp.rejNumUp + @", holdNum = " + rejDataUp.holdNumUp + @", respDep = '" + rejDataUp.respDepUp + @"', coporation = '" + rejDataUp.corpUp + @"', weekid='" + rejDataUp.weekid + @"', monthid='" + rejDataUp.monthid + @"'
        //                    where date = '" + rejDataUp.dateB + @"' and shift = '" + rejDataUp.shiftB + @"' and line = '" + rejDataUp.lineB + @"' and model = '" + rejDataUp.modelB + @"' and item = '" + rejDataUp.itemB +  @"'";
        //    int result = SqlHelper<RejDataModel>.Execute(sqlCmd);
        //    return result;
        //}

        //public byte[] ExportExcel(string sqlApp, string modular) {
        //    string sqlCmd = @"select date[日期], shift[班别], line[线别], model[机种], item[料号], rejNum[REJ数量], holdNum[Hold数量], respDep[责任部门], coporation[厂商], rejReason[判退原因] from IMMSOQARejData
        //                    where modular = '" + modular + @"' and status = 'Y' " + sqlApp + @"";
        //    DataTable dt = SqlHelper<RejDataModel>.sqlTable(sqlCmd);
        //    byte[] ary = ExcelHelper.GetExcel(dt, "sheet1", "A1:J1");
        //    return ary;
        //}
    }
}