using IMMSOQAMaintain_Api.common;
using IMMSOQAMaintain_Api.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace IMMSOQAMaintain_Api.DAL
{
    public class XingNengMain_DAL
    {
        public DataTable infoExist(MainModel model)
        {
            string sqlCmd = @"select totalAmount, leftNum, erroTotal, erroNum, (convert(int, opAmount) + convert(int, erroNum)) inputAmount from snMain where modular = '裸片性能' and status = 'Y' and snNum = '" + model.snNum + @"' order by lmdate desc, lmtime desc";
            DataTable dt = SqlHelper<MainModel>.sqlTable(sqlCmd);
            return dt;
        }

        public DataTable LastProcInfo(MainModel model)
        {
            string sqlCmd = "";
            sqlCmd = @"select lastProcOKNum,snNum,opAmount from snMain where modular = '预检' and status = 'Y' and snNum = '" + model.snNum + @"' order by lmdate desc, lmtime desc";
            DataTable dt = SqlHelper<MainModel>.sqlTable(sqlCmd);
            if (dt.Rows.Count == 0)
            {
                sqlCmd = @"select lastProcOKNum,snNum,opAmount from snMain where modular = '清洗' and status = 'Y' and snNum = '" + model.snNum + @"' order by lmdate desc, lmtime desc";
                dt = SqlHelper<MainModel>.sqlTable(sqlCmd);
            }
           
            return dt;
        }

        public DataTable TQLastProcInfo(MainModel model)
        {
            string sqlCmd = "";
            sqlCmd = @"select lastProcOKNum,snNum,opAmount from snMain where modular = '初检' and status = 'Y' and snNum = '" + model.snNum + @"' order by lmdate desc, lmtime desc";
            DataTable dt = SqlHelper<MainModel>.sqlTable(sqlCmd);

            return dt;
        }

        public DataTable FJXinfoExist(MainModel model)
        {
            string sqlCmd = @"select totalAmount, leftNum, erroTotal,erroNum, (convert(int, opAmount) + convert(int, erroNum)) inputAmount from snMain where modular = '套圈性能' and status = 'Y' and snNum = '" + model.snNum + @"' order by lmdate desc, lmtime desc";
            DataTable dt = SqlHelper<MainModel>.sqlTable(sqlCmd);
            return dt;
        }

        //public int infoSaveAgain(MainModel model)
        //{
        //    string sqlCmd = @"update snMain set opAmount = '" + model.opAmount + @"'
        //                     where modular = '裸片性能' and snNum = '" + model.snNum + @"' and status = 'Y'";
        //    int result = SqlHelper<MainModel>.Execute(sqlCmd);
        //    return result;
        //}

        //public int FJXinfoSaveAgain(MainModel model)
        //{
        //    string sqlCmd = @"update snMain set opAmount = '" + model.opAmount + @"'
        //                     where modular = '套圈性能' and snNum = '" + model.snNum + @"' and status = 'Y'";
        //    int result = SqlHelper<MainModel>.Execute(sqlCmd);
        //    return result;
        //}

        public int infoSave(MainModel model)
        {
            string sqlCmd = @"insert into snMain values
                              (
                                '裸片性能', '" + model.date + @"', '" + model.prodName + @"', '" + model.item + @"', '" + model.size + @"', '" + model.potNum + @"',
                                '" + model.snNum + @"', '" + model.totalAmount + @"', '" + model.leftNum + @"', '" + model.erroTotal + @"', '" + model.erroTotalPer + @"', '', 
                                '" + model.opAmount + @"', '" + model.erroNum + @"', '" + model.erroPer + @"', '" + model.erroDetail + @"',
                                'Y', '" + model.lmdate + @"', '" + model.lmtime + @"', '" + model.lmuser + @"', '" + model.hongkao + @"', '" + model.lastProcOKNum + @"'   
                              )";
            int result = SqlHelper<MainModel>.Execute(sqlCmd);
            return result;
        }

        public int FJXinfoSave(MainModel model)
        {
            string sqlCmd = @"insert into snMain values
                              (
                                '套圈性能', '" + model.date + @"', '" + model.prodName + @"', '" + model.item + @"', '" + model.size + @"', '" + model.potNum + @"',
                                '" + model.snNum + @"', '" + model.totalAmount + @"', '" + model.leftNum + @"', '" + model.erroTotal + @"', '" + model.erroTotalPer + @"', '', 
                                '" + model.opAmount + @"', '" + model.erroNum + @"', '" + model.erroPer + @"', '" + model.erroDetail + @"',
                                'Y', '" + model.lmdate + @"', '" + model.lmtime + @"', '" + model.lmuser + @"', '" + model.hongkao + @"', '" + model.lastProcOKNum + @"' 
                              )";
            int result = SqlHelper<MainModel>.Execute(sqlCmd);
            return result;
        }
    }
}