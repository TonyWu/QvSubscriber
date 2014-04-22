using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;

namespace QlikView.ReportVerification
{
    class ReportDataReader
    {
        public string  File { get; set; }
        public int HeaderRowNumber { get; set; }

        Excel.Application excel;

        public ReportDataSet ReadDataSet(string file, int headerRowNumber)
        {
            ReportDataSet dataSet = new ReportDataSet();
            try
            {
                excel = new Excel.Application();
                Excel.Workbook tempBook = excel.Workbooks.Open(file);

                foreach (var item in tempBook.Sheets)
                {
                    Excel.Worksheet sheet = item as Excel.Worksheet;
                    ReportDataTable table = this.GetDataTable(sheet, headerRowNumber);
                    dataSet.Tables.Add(table.Name, table);
                }

                tempBook.Save();
                tempBook.Close();
                tempBook = null;
                excel.Application.Quit();
            }
            catch (Exception ex)
            {
                excel.Application.Quit();
            }

            return dataSet;
        }

        private ReportDataTable GetDataTable(Excel.Worksheet sheet, int headerRowNumber)
        {
            int columnCount = 0;
            int rowCount = 0;

            //calculate the column number
            while (true)
            {
                string columnName = ExcelColumnIndexToName(columnCount + 1);
                var range = sheet.get_Range(columnName + headerRowNumber);
                if ((range != null && !string.IsNullOrEmpty(((string)range.Value2))) || columnCount < 3)
                {
                    columnCount++;
                }
                else
                {
                    break;
                }
            }

            //calculate the row number
            while (true)
            {
                var range = sheet.get_Range("A" + (headerRowNumber + rowCount));
                if (range != null && !string.IsNullOrEmpty(((string)range.Value2)))
                {
                    rowCount++;
                }
                else
                {
                    break;
                }
            }

            string[,] data = new string[rowCount, columnCount];

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    string columnName = ExcelColumnIndexToName(j + 1);
                    var range = sheet.get_Range(columnName + (headerRowNumber + i));

                    if (i == 0 && columnName == "B" && (range == null || range.Value2 == null))
                    {
                        data[i, j] = "FormulaColumn";
                    }
                    else
                    {
                        data[i, j] = (range == null || range.Value2 == null) ? string.Empty : range.Value2.ToString();
                    }
                }
            }

            ReportDataTable dataset = new ReportDataTable(data,sheet.Name);

            return dataset;
        }

        public static int ExcelColumnNameToIndex(string name)
        {
            int number = 0;
            int pow = 1;
            for (int i = name.Length - 1; i >= 0; i--)
            {
                number += (name[i] - 'A' + 1) * pow;
                pow *= 26;
            }

            return number;
        }

        public static string ExcelColumnIndexToName(int columnNumber)
        {
            int dividend = columnNumber;
            string columnName = String.Empty;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (int)((dividend - modulo) / 26);
            }

            return columnName;
        }

    }
}
