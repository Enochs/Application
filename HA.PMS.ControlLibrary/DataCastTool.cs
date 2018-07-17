using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Collections;

namespace HA.PMS.ToolsLibrary
{
    public class DataCastTool
    {
        public System.Data.DataTable ToDataTable(global::NPOI.SS.UserModel.ISheet sheet)
        {
            global::NPOI.SS.UserModel.IRow headerRow = sheet.GetRow(0);
            System.Data.DataTable datatable = new System.Data.DataTable(sheet.SheetName);
            if (sheet.PhysicalNumberOfRows > 0)
            {
                for (int i = headerRow.FirstCellNum; i < headerRow.LastCellNum; i++)
                {
                    datatable.Columns.Add(headerRow.GetCell(i).StringCellValue);
                }
                for (int i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
                {
                    System.Data.DataRow dataRow = datatable.NewRow();
                    for (int j = sheet.GetRow(i).FirstCellNum; j < headerRow.LastCellNum; j++)
                    {
                        NPOI.SS.UserModel.ICell cell = sheet.GetRow(i).GetCell(j);
                        if (!object.ReferenceEquals(cell, null))
                        {
                            dataRow[j] = GetCellValue(cell);
                        }
                    }
                    datatable.Rows.Add(dataRow);
                }
            }
            return datatable;
        }

        public object GetCellValue(NPOI.SS.UserModel.ICell cell)
        {
            switch (cell.CellType)
            {
                case NPOI.SS.UserModel.CellType.Unknown: return string.Empty;
                case NPOI.SS.UserModel.CellType.Numeric:
                    if (NPOI.SS.UserModel.DateUtil.IsCellDateFormatted(cell))
                    {
                        return cell.DateCellValue;
                    }
                    else
                    {
                        return cell.ToString();
                    }
                case NPOI.SS.UserModel.CellType.String: return cell.StringCellValue;
                case NPOI.SS.UserModel.CellType.Formula: return cell.StringCellValue;
                case NPOI.SS.UserModel.CellType.Blank: return string.Empty;
                case NPOI.SS.UserModel.CellType.Boolean: return cell.BooleanCellValue;
                case NPOI.SS.UserModel.CellType.Error: return string.Empty;
                default: return string.Empty;
            }
        }
    }
}
