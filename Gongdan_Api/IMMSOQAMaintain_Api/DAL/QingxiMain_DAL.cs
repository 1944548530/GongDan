using IMMSOQAMaintain_Api.common;
using IMMSOQAMaintain_Api.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace IMMSOQAMaintain_Api.DAL
{
    public class QingxiMain_DAL
    {
        public IEnumerable<MainModel> GetInfoBySn(string sn) {
            string sqlCmd = @"select prodName, item, size, potNum, totalAmount, hongkao
                             from snMain where modular = '滚圆' and status = 'Y' and snNum = '" + sn + @"'";
            IEnumerable<MainModel> infoLi = SqlHelper<MainModel>.Query(sqlCmd);
            return infoLi;
        }
        public int QingxiSave(MainModel model) {
            string sqlCmd = @"insert into snMain values
                              (
                                '清洗', '" + model.date + @"', '" + model.prodName + @"', '" + model.item + @"', '" + model.size + @"', '" + model.potNum + @"',
                                '" + model.snNum + @"', '" + model.totalAmount + @"', '" + model.leftNum + @"', '" + model.erroTotal + @"', '" + model.erroTotalPer + @"', '', 
                                '" + model.opAmount + @"', '" + model.erroNum + @"', '" + model.erroPer + @"', '" + model.erroDetail + @"',
                                'Y', '" + model.lmdate + @"', '" + model.lmtime + @"', '" + model.lmuser + @"' , '" + model.hongkao + @"', '" + model.lastProcOKNum + @"' 
                              )";
            int result = SqlHelper<MainModel>.Execute(sqlCmd);
            return result;
        }

        public DataTable infoExist(MainModel model) {
            string sqlCmd = @"select totalAmount, leftNum, erroTotal, erroNum, (convert(int, opAmount) + convert(int, erroNum)) inputAmount from snMain where modular = '清洗' and status = 'Y' and snNum = '" + model.snNum + @"' order by lmdate desc, lmtime desc";
            DataTable dt = SqlHelper<MainModel>.sqlTable(sqlCmd);
            return dt;
        }

        public DataTable LastProcInfo(MainModel model) {
            string sqlCmd = @"select lastProcOKNum,snNum,opAmount from snMain where modular = '滚圆' and status = 'Y' and snNum = '" + model.snNum + @"' order by lmdate desc, lmtime desc";
            DataTable dt = SqlHelper<MainModel>.sqlTable(sqlCmd);
            return dt;
        }
    }
}