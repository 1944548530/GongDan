using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace IMMSOQAMaintain_Api.common
{
    public class ExcelHelper
    {
        public static byte[] GetExcel(DataTable dt, string sheetName, string region, string modular)
        {
            using (ExcelPackage pck = new ExcelPackage())
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add(sheetName);
                ws.Cells["A1"].LoadFromDataTable(dt, true);
                #region 设置样式，可以不要
                
                if (modular == "全检")
                {
                    ws.Column(1).Width = 22;
                    ws.Column(2).Width = 15;
                    ws.Column(3).Width = 15;
                    ws.Column(4).Width = 15;
                    ws.Column(5).Width = 15;
                    ws.Column(6).Width = 18;
                    ws.Column(7).Width = 15;
                    ws.Column(8).Width = 15;
                    ws.Column(9).Width = 15;
                    ws.Column(10).Width = 15;
                    ws.Column(11).Width = 15;
                    ws.Column(12).Width = 15;
                    ws.Column(13).Width = 15;
                    ws.Column(14).Width = 15;
                    ws.Column(15).Width = 15;
                    ws.Column(16).Width = 15;
                }
                else {
                    ws.Column(1).Width = 22;
                    ws.Column(2).Width = 15;
                    ws.Column(3).Width = 15;
                    ws.Column(4).Width = 15;
                    ws.Column(5).Width = 15;
                    ws.Column(6).Width = 15;
                    ws.Column(7).Width = 18;
                    ws.Column(8).Width = 15;
                    ws.Column(9).Width = 15;
                    ws.Column(10).Width = 15;
                    ws.Column(11).Width = 15;
                    ws.Column(12).Width = 15;
                    ws.Column(13).Width = 15;
                    ws.Column(14).Width = 15;
                    ws.Column(15).Width = 15;
                    ws.Column(16).Width = 15;
                    ws.Column(17).Width = 15;
                    ws.Column(18).Width = 15;
                    ws.Column(19).Width = 15;
                    ws.Column(20).Width = 15;
                }
                using (ExcelRange rng = ws.Cells[region])
                {
                    rng.Style.Font.Bold = true;
                    rng.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    rng.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    rng.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    rng.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    rng.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    rng.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(79, 129, 189));
                    rng.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    rng.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    rng.Style.Font.Color.SetColor(System.Drawing.Color.White);
                    rng.Style.WrapText = true;
                }
                using (ExcelRange col = ws.Cells[2, 1, 2 + dt.Rows.Count - 1, dt.Columns.Count])
                {
                    col.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    col.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    col.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    col.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    col.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    col.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    col.Style.WrapText = true;

                }
                #endregion
                return pck.GetAsByteArray();
            }
        }
    }
}