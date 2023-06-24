using IMMSOQAMaintain_Api.common;
using IMMSOQAMaintain_Api.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace IMMSOQAMaintain_Api.DAL
{
    public class GunyMain_DAL
    {
        Tool tool = new Tool();
        public int GunySave(MainModel model) {
            string sqlCmd = @"insert into snMain values
                              (
                                '滚圆', '" + model.date + @"', '" + model.prodName + @"', '" + model.item + @"', '" + model.size + @"', '" + model.potNum + @"',
                                '" + model.snNum + @"', '" + model.totalAmount + @"', '" + model.leftNum + @"', '" + model.erroTotal + @"', '" + model.erroTotalPer + @"', '', 
                                '" + model.opAmount + @"', '" + model.erroNum + @"', '" + model.erroPer + @"', '" + model.erroDetail + @"',
                                'Y', '" + model.lmdate + @"', '" + model.lmtime + @"', '" + model.lmuser + @"', '" + model.hongkao + @"', '" + model.lastProcOKNum + @"'  
                              )";
            int result = SqlHelper<MainModel>.Execute(sqlCmd);
            return result;
        }

        public int GunySaveAgain(MainModel model) {
            string sqlCmd = @"update snMain set opAmount = '" + model.opAmount + @"'
                             where modular = '滚圆' and snNum = '" + model.snNum + @"' and status = 'Y'";
            int result = SqlHelper<MainModel>.Execute(sqlCmd);
            return result;
        }

        public DataTable infoExist(MainModel model) {
            string sqlCmd = @"select totalAmount, leftNum, erroNum, erroTotal,(convert(int, opAmount) + convert(int, erroNum)) inputAmount  from snMain where modular = '滚圆' and status = 'Y' and snNum = '" + model.snNum + @"' order by lmdate desc, lmtime desc";
            DataTable dt = SqlHelper<MainModel>.sqlTable(sqlCmd);
            return dt;
        }

        public DataTable infoExistUp(OtherUpModel model) {
            string sqlCmd = @"select totalAmount, leftNum, erroTotal from snMain where modular = '" + model.modular + @"' and status = 'Y' and snNum = '" + model.gongdan + @"' order by lmdate desc, lmtime desc";
            DataTable dt = SqlHelper<MainModel>.sqlTable(sqlCmd);
            return dt;
        }

        public List<MainModel> GetOtherInfo(string modular, int indexBegin, int indexEnd, string snSqlApp, string dateSqlApp) {
            string sqlCmd = @"select (b.date + ' ' + b.lmtime)date, b.modular, b.prodName, b.item, b.size, b.potNum, b.snNum, b.totalAmount, b.leftNum, b.erroTotal, b.erroTotalPer,  b.opAmount, b.erroNum, 
                              (convert(int, b.opAmount) + convert(int, b.erroNum)) inputAmount, b.erroDetail, b.erroPer, b.hongkao,b.lastProcOKNum from
                             (
                                select ROW_NUMBER() over(order by date desc, lmtime desc) num,
                                date, modular, prodName, item, size, potNum, snNum, totalAmount, leftNum, erroTotal, erroTotalPer, opAmount, erroNum, erroPer, erroDetail, lmtime, hongkao, lastProcOKNum 
                                from(select * from snMain where status = 'Y' and modular = '" + modular + @"' " + snSqlApp + @" " + dateSqlApp + @")a
                             )b where b.num between '" + indexBegin + @"' and '" + indexEnd + @"'";
            List<MainModel> infoLi = SqlHelper<MainModel>.Query(sqlCmd).ToList();
            return infoLi;
        }

        public int OtherInfoCount(string modular, string snNum, string date) {
            string sqlCmd = @"select date from snMain where modular = '" + modular + @"' " + snNum + @" " + date + @" and status = 'Y'";
            DataTable dt = SqlHelper<MainModel>.sqlTable(sqlCmd);
            return dt.Rows.Count;
        }

        public int OtherInfoDel(string date, string snNum, string modular, string opAmount, string erroDetail)
        {
            string sqlCmd = @"update snMain set status = 'N'
                             where date = '" + date + @"' and modular = '" + modular + @"' and snNum = '" + snNum + @"' and opAmount = '" + opAmount + @"' and erroDetail = '" + erroDetail + @"'";
            int result = SqlHelper<MainModel>.Execute(sqlCmd);
            return result; 
        }

        public int OtherInfoUp(OtherUpModel model) {
            string sqlCmd = @"update snMain set 
                              date = '" + model.dateUp + @"', prodName = '" + model.pinMing + @"', item = '" + model.item + @"', size = '" + model.size + @"',erroDetail = '" + model.erroDetail + @"',
                              potNum = '" + model.luhao + @"', totalAmount= '" + model.gdAmount + @"', leftNum = '" + model.leftNum + @"', erroTotal = '" + model.erroTotal + @"', erroTotalPer = '" + model.erroTotalPer + @"', 
                              erroPer = '" + model.erroPer + @"', opAmount = '" + model.outputNum + @"', erroNum = '" + model.erroNum + @"'
                              where modular = '" + model.modular + @"' and date = '" + model.dateOld + @"' and snNum = '" + model.gongdan + @"' and status = 'Y' and opAmount = '" + model.opAmountOld + @"' and erroDetail = '" + model.erroDetailOld + @"'";
            int result = SqlHelper<MainModel>.Execute(sqlCmd);
            return result;
        }

        public void totalAmountUp(OtherUpModel model) {
            List<string> moduleLi = new List<string>() { "滚圆", "清洗", "预检", "裸片性能", "初检", "套圈性能", "复检" };
            string infoBySnSql = @"select modular, snNum, leftNum, opAmount, erroNum, erroTotal, erroPer, erroTotalPer, lmtime from snmain 
                                   where snNum = '" + model.gongdan + @"' and status = 'Y' order by modular, lmdate , lmtime";
            List<TotalNumModel> info = SqlHelper<TotalNumModel>.Query(infoBySnSql).ToList();
            for (int i = 0; i < moduleLi.Count; i++) {
                List<TotalNumModel> infoBymodule = info.Where(v => v.modular == moduleLi[i]).ToList();
                for (int j = 0; j < infoBymodule.Count; j++) {
                    string lmtime = infoBymodule[j].lmtime;
                    int leftNum = 0;
                    float erroTotalPer = 0;
                    if (j > 0)
                    {
                        int leftB = int.Parse(infoBymodule[j - 1].leftNum);
                        leftNum = leftB - int.Parse(infoBymodule[j].opAmount) - int.Parse(infoBymodule[j].erroNum);
                        //float erroPer = tool.perCal1(int.Parse(infoBymodule[j].erroNum), (int.Parse(infoBymodule[j].erroNum) + int.Parse(infoBymodule[j].opAmount)));
                        erroTotalPer = tool.perCal1(int.Parse(infoBymodule[j].erroTotal), int.Parse(model.gdAmount));
                    }
                    else {
                        leftNum = int.Parse(model.gdAmount) - int.Parse(infoBymodule[j].opAmount) - int.Parse(infoBymodule[j].erroNum);
                        //float erroPer = tool.perCal1(int.Parse(infoBymodule[j].erroNum), (int.Parse(infoBymodule[j].erroNum) + int.Parse(infoBymodule[j].opAmount)));
                        erroTotalPer = tool.perCal1(int.Parse(infoBymodule[j].erroTotal), int.Parse(model.gdAmount));
                    }
                    string erroTotalPerStr = erroTotalPer + "%";
                    string sqlCmd = @"update snmain
                                      set totalAmount = '" + model.gdAmount + @"', leftNum = '" + leftNum + @"', erroTotalPer = '" + erroTotalPerStr + @"'
                                      where modular = '" + moduleLi[i] + @"'
                                      and snNum = '" + model.gongdan + @"' and status = 'Y' and lmtime = '" + lmtime + @"'";
                    int result = SqlHelper<MainModel>.Execute(sqlCmd);
                }
            }
            
        }

        public byte[] ExportExcel(string modular, string snNumSqlApp, string dateSqlApp)
        {
            string sqlCmd = @"select (date + ' ' + lmtime) 日期, snNum 工单号, modular 工序, prodName 品名, item 成品料号, size 尺寸, potNum 炉号, hongkao 烘烤, totalAmount 工单总量, leftNum 剩余数量, erroTotal 不良总量, lastProcOKNum 前工序良品总量, erroTotalPer 总不良率, (convert(int, opAmount) + convert(int, erroNum))  投入数量, 
                              opAmount 产出数量, erroNum 不良现象, erroPer 不良率, erroDetail 不良现象明细 from snMain
                              where status = 'Y' and modular = '" + modular + @"' " + snNumSqlApp + @" " + dateSqlApp + @"";
            DataTable dt = SqlHelper<MainModel>.sqlTable(sqlCmd);
            string titleRegion = "";
            if (modular == "滚圆")
            {
                DataColumn col1 = new DataColumn();
                col1.DataType = System.Type.GetType("System.String");
                col1.ColumnName = "滚坏";
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
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string erroDetailL = dt.Rows[i]["不良现象明细"].ToString();
                    string erroDetailS = erroDetailL.Replace("滚坏:", "").Replace("其他:", "");
                    List<string> erroLi = erroDetailS.Split(',').ToList();
                    string col1Str = erroLi[0].Trim();
                    string col2Str = erroLi[1].Trim();
                    string col3Str = "";
                    if (erroLi.Count > 2)
                    {
                        col3Str = erroLi[2].Trim();
                    }
                    dt.Rows[i]["滚坏"] = col1Str;
                    dt.Rows[i]["其他"] = col2Str;
                    dt.Rows[i]["空白"] = col3Str;
                }
                titleRegion = "A1:T1";
            }
            else if (modular == "清洗")
            {
                DataColumn col1 = new DataColumn();
                col1.DataType = System.Type.GetType("System.String");
                col1.ColumnName = "刮伤";
                col1.DefaultValue = "";

                DataColumn col2 = new DataColumn();
                col2.DataType = System.Type.GetType("System.String");
                col2.ColumnName = "丢片";
                col2.DefaultValue = "";

                DataColumn col3 = new DataColumn();
                col3.DataType = System.Type.GetType("System.String");
                col3.ColumnName = "其他";
                col3.DefaultValue = "";

                DataColumn col4 = new DataColumn();
                col4.DataType = System.Type.GetType("System.String");
                col4.ColumnName = "空白";
                col4.DefaultValue = "";
                dt.Columns.Add(col1);
                dt.Columns.Add(col2);
                dt.Columns.Add(col3);
                dt.Columns.Add(col4);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string erroDetailL = dt.Rows[i]["不良现象明细"].ToString();
                    string erroDetailS = erroDetailL.Replace("刮伤:", "").Replace("丢片:", "").Replace("其他:", "");
                    List<string> erroLi = erroDetailS.Split(',').ToList();
                    string col1Str = erroLi[0].Trim();
                    string col2Str = erroLi[1].Trim();
                    string col3Str = erroLi[2].Trim();
                    string col4Str = "";
                    if (erroLi.Count > 3)
                    {
                        col4Str = erroLi[3].Trim();
                    }
                    dt.Rows[i]["刮伤"] = col1Str;
                    dt.Rows[i]["丢片"] = col2Str;
                    dt.Rows[i]["其他"] = col3Str;
                    dt.Rows[i]["空白"] = col4Str;
                }
                titleRegion = "A1:U1";
            }
            else if (modular == "预检" || modular == "初检" || modular == "复检")
            {
                DataColumn col1 = new DataColumn();
                col1.DataType = System.Type.GetType("System.String");
                col1.ColumnName = "刮伤";
                col1.DefaultValue = "";

                DataColumn col2 = new DataColumn();
                col2.DataType = System.Type.GetType("System.String");
                col2.ColumnName = "麻点";
                col2.DefaultValue = "";

                DataColumn col3 = new DataColumn();
                col3.DataType = System.Type.GetType("System.String");
                col3.ColumnName = "崩边";
                col3.DefaultValue = "";

                DataColumn col4 = new DataColumn();
                col4.DataType = System.Type.GetType("System.String");
                col4.ColumnName = "花边";
                col4.DefaultValue = "";

                DataColumn col5 = new DataColumn();
                col5.DataType = System.Type.GetType("System.String");
                col5.ColumnName = "脱点";
                col5.DefaultValue = "";

                DataColumn col6 = new DataColumn();
                col6.DataType = System.Type.GetType("System.String");
                col6.ColumnName = "其他";
                col6.DefaultValue = "";

                DataColumn col7 = new DataColumn();
                col7.DataType = System.Type.GetType("System.String");
                col7.ColumnName = "空白";
                col7.DefaultValue = "";

                dt.Columns.Add(col1);
                dt.Columns.Add(col2);
                dt.Columns.Add(col3);
                dt.Columns.Add(col4);
                dt.Columns.Add(col5);
                dt.Columns.Add(col6);
                dt.Columns.Add(col7);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string erroDetailL = dt.Rows[i]["不良现象明细"].ToString();
                    string erroDetailS = erroDetailL.Replace("刮伤:", "").Replace("麻点:", "").Replace("崩边:", "").Replace("花边:", "").Replace("脱点:", "").Replace("其他:", "");
                    List<string> erroLi = erroDetailS.Split(',').ToList();
                    string col1Str = erroLi[0].Trim();
                    string col2Str = erroLi[1].Trim();
                    string col3Str = erroLi[2].Trim();
                    string col4Str = erroLi[3].Trim();
                    string col5Str = "";
                    string col6Str = "";
                    string col7Str = "";
                    if (erroLi.Count > 5) {
                        col5Str = erroLi[4].Trim();
                        col6Str = erroLi[5].Trim();
                    }
                    
                    if (erroLi.Count > 6)
                    {
                        col7Str = erroLi[6].Trim();
                    }
                    dt.Rows[i]["刮伤"] = col1Str;
                    dt.Rows[i]["麻点"] = col2Str;
                    dt.Rows[i]["崩边"] = col3Str;
                    dt.Rows[i]["花边"] = col4Str;
                    dt.Rows[i]["脱点"] = col5Str;
                    dt.Rows[i]["其他"] = col6Str;
                    dt.Rows[i]["空白"] = col7Str;
                }
                titleRegion = "A1:X1";
            }
            else {
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
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string erroDetailL = dt.Rows[i]["不良现象明细"].ToString();
                    string erroDetailS = erroDetailL.Replace("短需烘烤:", "").Replace("其他:", "");
                    List<string> erroLi = erroDetailS.Split(',').ToList();
                    string col1Str = erroLi[0].Trim();
                    string col2Str = erroLi[1].Trim();
                    string col3Str = "";
                    if (erroLi.Count > 2)
                    {
                        col3Str = erroLi[2].Trim();
                    }
                    dt.Rows[i]["短需烘烤"] = col1Str;
                    dt.Rows[i]["其他"] = col2Str;
                    dt.Rows[i]["空白"] = col3Str;
                }
                titleRegion = "A1:T1";
            }
            dt.Columns.Remove("不良现象明细");
            byte[] ary = ExcelHelper.GetExcel(dt, "sheet1", titleRegion, "滚圆");
            return ary;
        }
    }
}