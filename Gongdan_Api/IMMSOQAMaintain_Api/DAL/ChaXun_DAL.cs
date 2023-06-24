using IMMSOQAMaintain_Api.common;
using IMMSOQAMaintain_Api.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace IMMSOQAMaintain_Api.DAL
{
    public class ChaXun_DAL
    {
        public List<MainModel> InfoBySn(string gongdanApp, string procedureApp, string liaohaoApp, string luhaoApp, int indexBegin, int indexEnd) {
            string sqlCmd = @"select (b.date + ' ' + b.lmtime)date, b.modular, b.prodName, b.item, b.size, b.potNum, b.snNum, b.totalAmount, b.leftNum, b.erroTotal, b.erroTotalPer,  b.opAmount, b.erroNum, b.hongkao,
                              (convert(int, b.opAmount) + convert(int, b.erroNum)) inputAmount, b.erroDetail, b.erroPer,b.lastProcOKNum from
                             (
                                select ROW_NUMBER() over(order by modular, date desc, lmtime desc) num,
                                date, modular, prodName, item, size, potNum, snNum, totalAmount, leftNum, erroTotal, erroTotalPer, opAmount, erroNum, erroPer, erroDetail, lmtime, hongkao, lastProcOKNum
                                from(select * from snMain where status = 'Y' " + gongdanApp + @" " + procedureApp + @" " + liaohaoApp + @" " + luhaoApp + @")a
                             )b where b.num between '" + indexBegin + @"' and '" + indexEnd + @"'";
            List<MainModel> infoLi = SqlHelper<MainModel>.Query(sqlCmd).ToList();
            return infoLi;
        }

        public int InfoBySnCount(string gongdan) {
            string sqlCmd = @"select date from snMain where snNum = '" + gongdan + @"' and status = 'Y'";
            DataTable dt = SqlHelper<MainModel>.sqlTable(sqlCmd);
            return dt.Rows.Count;
        }
    }
}