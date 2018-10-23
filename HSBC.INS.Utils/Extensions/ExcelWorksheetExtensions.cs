using HSBC.INS.Utils.Common;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HSBC.INS.Utils.Extensions
{
    public static class ExcelWorksheetExtensions
    {
        /// <summary>
        /// 从Excel中获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sheet"></param>
        /// <param name="columnsCount">Excel中数据范围的列数</param>
        /// <param name="startRow">要获取的数据的起始行的索引，从1开始</param>
        /// <returns></returns>
        public static List<T> GetRow<T>(this ExcelWorksheet sheet, int columnsCount, int startRow) where T : ExcelRowIndex
        {
            //计算行数
            int rowsCount = sheet.Cells.Count() / columnsCount;

            List<T> list = new List<T>();
            Type t = typeof(T);
            PropertyInfo[] propertyInfos = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            bool isRowEmpty;
            for (int rowIndex = startRow; rowIndex <= rowsCount; rowIndex++)
            {
                isRowEmpty = true;

                T obj = (T)Activator.CreateInstance(typeof(T));
                foreach (PropertyInfo property in propertyInfos)
                {
                    if (property.Name == "RowIndex")
                    {
                        property.SetValue(obj, rowIndex);
                    }
                    else
                    {
                        var excelColumnAttr = property.GetCustomAttribute<ExcelColumnAttribute>();
                        string cellAddress = excelColumnAttr.Column + rowIndex;
                        object cellValue = sheet.Cells[cellAddress].Value;

                        try
                        {
                            property.SetPropertyValue(obj, cellValue);
                        }
                        catch (Exception e)
                        {

                        }

                        if (isRowEmpty && cellValue != null && !string.IsNullOrWhiteSpace(cellValue.ToString()))
                            isRowEmpty = false;
                    }
                }

                if (!isRowEmpty)
                    list.Add(obj);
            }
            return list;
        }
    }
}
