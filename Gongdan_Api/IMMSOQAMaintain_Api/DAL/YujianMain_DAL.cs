using IMMSOQAMaintain_Api.common;
using IMMSOQAMaintain_Api.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace IMMSOQAMaintain_Api.DAL
{
    public class YujianMain_DAL
    {
        public DataTable infoExist(MainModel model)
        {
            string sqlCmd = @"select totalAmount, leftNum, erroTotal, erroNum, (convert(int, opAmount) + convert(int, erroNum)) inputAmount from snMain where modular = '" + model.modular + @"' and status = 'Y' and snNum = '" + model.snNum + @"' order by lmdate desc, lmtime desc";
            DataTable dt = SqlHelper<MainModel>.sqlTable(sqlCmd);
            return dt;
        }
        public DataTable LastProcInfo(MainModel model)
        {
            string sqlCmd = "";
            DataTable dt = new DataTable();
            if (model.modular == "初检")
            {
                sqlCmd = @"select lastProcOKNum,snNum,opAmount from snMain where modular = '裸片性能' and status = 'Y' and snNum = '" + model.snNum + @"' order by lmdate desc, lmtime desc";
                dt = SqlHelper<MainModel>.sqlTable(sqlCmd);
                if (dt.Rows.Count == 0) {
                    sqlCmd = @"select lastProcOKNum,snNum,opAmount from snMain where modular = '清洗' and status = 'Y' and snNum = '" + model.snNum + @"' order by lmdate desc, lmtime desc";
                    dt = SqlHelper<MainModel>.sqlTable(sqlCmd);
                }
            }
            else {
                if (model.modular == "预检")
                {
                    sqlCmd = @"select lastProcOKNum,snNum,opAmount from snMain where modular = '清洗' and status = 'Y' and snNum = '" + model.snNum + @"' order by lmdate desc, lmtime desc";
                }
                else {
                    sqlCmd = @"select lastProcOKNum,snNum,opAmount from snMain where modular = '套圈性能' and status = 'Y' and snNum = '" + model.snNum + @"' order by lmdate desc, lmtime desc";
                }
                dt = SqlHelper<MainModel>.sqlTable(sqlCmd);
            }
            return dt;
        }

        public DataTable ChuinfoExist(MainModel model)
        {
            string sqlCmd = @"select opAmount from snMain where modular = '初检' and status = 'Y' and snNum = '" + model.snNum + @"'";
            DataTable dt = SqlHelper<MainModel>.sqlTable(sqlCmd);
            return dt;
        }

        public DataTable FuinfoExist(MainModel model)
        {
            string sqlCmd = @"select opAmount from snMain where modular = '复检' and status = 'Y' and snNum = '" + model.snNum + @"'";
            DataTable dt = SqlHelper<MainModel>.sqlTable(sqlCmd);
            return dt;
        }

        public int infoSave(MainModel model) {
            string sqlCmd = @"insert into snMain values
                              (
                                '" + model.modular + @"', '" + model.date + @"', '" + model.prodName + @"', '" + model.item + @"', '" + model.size + @"', '" + model.potNum + @"',
                                '" + model.snNum + @"', '" + model.totalAmount + @"', '" + model.leftNum + @"', '" + model.erroTotal + @"', '" + model.erroTotalPer + @"', '', 
                                '" + model.opAmount + @"', '" + model.erroNum + @"', '" + model.erroPer + @"', '" + model.erroDetail + @"',
                                'Y', '" + model.lmdate + @"', '" + model.lmtime + @"', '" + model.lmuser + @"', '" + model.hongkao + @"', '" + model.lastProcOKNum + @"' 
                              )";
             int result = SqlHelper<MainModel>.Execute(sqlCmd);
            return result;
        }

        public int ChuinfoSave(MainModel model)
        {
            string sqlCmd = @"insert into snMain values
                              (
                                '初检', '" + model.date + @"', '" + model.prodName + @"', '" + model.item + @"', '" + model.size + @"', '" + model.potNum + @"',
                                '" + model.snNum + @"', '" + model.totalAmount + @"', '" + model.opAmount + @"', '" + model.erroNum + @"', '" + model.erroDetail + @"',
                                'Y', '" + model.lmdate + @"', '" + model.lmtime + @"', '" + model.lmuser + @"'  
                              )";
            int result = SqlHelper<MainModel>.Execute(sqlCmd);
            return result;
        }

        public int FuinfoSave(MainModel model)
        {
            string sqlCmd = @"insert into snMain values
                              (
                                '复检', '" + model.date + @"', '" + model.prodName + @"', '" + model.item + @"', '" + model.size + @"', '" + model.potNum + @"',
                                '" + model.snNum + @"', '" + model.totalAmount + @"', '" + model.opAmount + @"', '" + model.erroNum + @"', '" + model.erroDetail + @"',
                                'Y', '" + model.lmdate + @"', '" + model.lmtime + @"', '" + model.lmuser + @"'  
                              )";
            int result = SqlHelper<MainModel>.Execute(sqlCmd);
            return result;
        }

        public int infoSaveAgain(MainModel model) {
            string sqlCmd = @"update snMain set opAmount = '" + model.opAmount + @"'
                             where modular = '预检' and snNum = '" + model.snNum + @"' and status = 'Y'";
            int result = SqlHelper<MainModel>.Execute(sqlCmd);
            return result;
        }

        public int ChuinfoSaveAgain(MainModel model)
        {
            string sqlCmd = @"update snMain set opAmount = '" + model.opAmount + @"'
                             where modular = '初检' and snNum = '" + model.snNum + @"' and status = 'Y'";
            int result = SqlHelper<MainModel>.Execute(sqlCmd);
            return result;
        }

        public int FuinfoSaveAgain(MainModel model)
        {
            string sqlCmd = @"update snMain set opAmount = '" + model.opAmount + @"'
                             where modular = '复检' and snNum = '" + model.snNum + @"' and status = 'Y'";
            int result = SqlHelper<MainModel>.Execute(sqlCmd);
            return result;
        }
    }
}