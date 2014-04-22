using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace QlikView.Common
{
    public class ExcelUtilies
    {
        public static void FreezePanes(Excel.Worksheet worksheet, int splitColumn, int splitRow)
        {
            worksheet.Activate();
            worksheet.Application.ActiveWindow.SplitColumn = splitColumn;
            worksheet.Application.ActiveWindow.SplitRow = splitRow;
            worksheet.Application.ActiveWindow.FreezePanes = true;
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
            //string range = "";
            //if (Index < 1) return range;
            //for (int i = 1; Index + i > 0; i = 0)
            //{
            //    range = ((char)(64 + Index % 26)).ToString() + range;
            //    Index /= 26;
            //}
            //return range;

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

        /// <summary>
        /// Delete columns
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="beginColumn"></param>
        /// <param name="endColumn"></param>
        public static void DeleteColumns(Excel.Worksheet sheet, string beginColumn, string endColumn)
        {
            sheet.Range[beginColumn + "1", endColumn + "1"].EntireColumn.Delete();
        }

        /// <summary>
        /// Delete one row
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="cellRange"></param>
        public static void DeleteRow(Excel.Worksheet sheet, string cellRange)
        {
            Excel.Range rangeDelete = sheet.Range[cellRange];
            rangeDelete.EntireRow.Delete(Excel.XlDeleteShiftDirection.xlShiftUp);
            ReleaseComObject(rangeDelete);
        }

        /// <summary>
        /// The sourcesheet and the destsheet need to be in the same workbook
        /// </summary>
        /// <param name="sourceSheet"></param>
        /// <param name="cellBegin"></param>
        /// <param name="cellEnd"></param>
        /// <param name="destSheet"></param>
        /// <param name="positionInsert"></param>
        public static void CopyRange(Excel.Worksheet sourceSheet, string cellBegin, string cellEnd, Excel.Worksheet destSheet, string positionInsert)
        {
            Excel.Range destRange = destSheet.Range[positionInsert];

            Excel.Range sourceRange = sourceSheet.Range[cellBegin, cellEnd];
            sourceRange.Select();
            sourceRange.Copy(destRange);

            ReleaseComObject(destRange);
            ReleaseComObject(sourceRange);
        }

        /// <summary>
        /// The sourcesheet and the destsheet need to be in the same workbook
        /// </summary>
        /// <param name="sourceSheet"></param>
        /// <param name="cellBegin"></param>
        /// <param name="cellEnd"></param>
        /// <param name="destSheet"></param>
        /// <param name="positionInsert"></param>
        public static void InsertRange(Excel.Worksheet sourceSheet, string cellBegin, string cellEnd, Excel.Worksheet destSheet, string positionInsert)
        {
            Excel.Range destRange = destSheet.Range[positionInsert];

            Excel.Range sourceRange = sourceSheet.Range[cellBegin, cellEnd];
            sourceRange.EntireColumn.Select();
            destRange.EntireColumn.Insert(Excel.XlInsertShiftDirection.xlShiftToRight);

            Excel.Range destRange1 = destSheet.Range[positionInsert];
            sourceRange.Copy(destRange1);

            ReleaseComObject(destRange);
            ReleaseComObject(sourceRange);
            ReleaseComObject(destRange1);
        }

        /// <summary>
        /// Release the COM Object
        /// </summary>
        /// <param name="o"></param>
        public static void ReleaseComObject(object o)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(o);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                o = null;
            }
        }

        public static void InsertRow(Excel.Worksheet sheet, string cell)
        {
            Excel.Range range = sheet.Range[cell];
            range.EntireRow.Insert(Excel.XlInsertShiftDirection.xlShiftDown);
            ReleaseComObject(range);
        }

        private void KillProcess(string processName)
        {
            //获得进程对象，以用来操作
            System.Diagnostics.Process myproc = new System.Diagnostics.Process();
            //得到所有打开的进程 
            try
            {
                //获得需要杀死的进程名
                foreach (Process thisproc in Process.GetProcessesByName(processName))
                {
                    //立即杀死进程
                    thisproc.Kill();
                }
            }
            catch (Exception Exc)
            {
                throw new Exception("", Exc);
            }
        }

        
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowThreadProcessId(IntPtr hwnd, out int ID);
        public static void Kill(Microsoft.Office.Interop.Excel.Application excel)
        {
            IntPtr t = new IntPtr(excel.Hwnd);   
            int k = 0;
            GetWindowThreadProcessId(t, out k);   
            System.Diagnostics.Process p = System.Diagnostics.Process.GetProcessById(k);  
            p.Kill();    
        }

    }
}
