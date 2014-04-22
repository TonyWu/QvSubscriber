using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Drawing;
using Microsoft.Office.Interop.Excel;

namespace QlikView.Common
{
    class ChinaSalesFunnelDash: IExcelMerge
    {
        Excel.Application excel = new Excel.Application();

        public ILog Logger { get; set; }

        Excel.Workbook sourceBook = null;

        private DateTime CurrentDate
        {
            get
            {
                return DateTime.Today;
            }
        }

        private Color HeaderColor
        {
            get
            {
                return Color.FromArgb(204, 255, 255);
            }
        }

        public bool MergeFiles(Dictionary<string, ReportContext> MergedFiles, string mergedFile, out string outputFile)
        {
            Excel.Workbook bookDest = null;
            try
            {
                bookDest = excel.Workbooks.Add(Missing.Value);

                //create a new work sheet
                Excel.Worksheet sheetDest = bookDest.Worksheets[1] as Excel.Worksheet;

                if (MergedFiles.Count > 0)
                {
                    this.Logger.Message("Merge " + MergedFiles.First().Key);
                    sourceBook = excel.Workbooks.Open(MergedFiles.First().Value.OutputFullName);
                    Excel.Worksheet sheet = sourceBook.Worksheets[1];
                    sheet.Name = "Summary";

                    sheet.Copy(Missing.Value, sheetDest);

                    Excel.Worksheet copysheet = bookDest.Worksheets["Summary"];

                    this.FormatSummary(copysheet);

                    FunnelReportHelper.CloseWorkingWorkbook(sourceBook);
                }

                foreach (var item in bookDest.Worksheets)
                {
                    Excel.Worksheet sheet = item as Excel.Worksheet;
                    if (sheet.Name != "Summary")
                        sheet.Delete();
                }

                //outputFile = "ChinaDash_" + DateTime.Now.ToString("yyyMMdd") + ".xls";
                outputFile = mergedFile.Replace(".xls", "_" + DateTime.Now.ToString("yyyMMdd") + ".xls");

                if (File.Exists(outputFile))
                {
                    File.Delete(outputFile);
                }

                bookDest.SaveAs(outputFile);
                bookDest.Close();
            }
            catch (Exception ex)
            {
                throw;
            }

            return true;
        }

        private void FormatSummary(Excel.Worksheet summaryDestSheet)
        {
            summaryDestSheet.Range["A1", "AZ100"].Interior.ColorIndex = 0;
            this.MergeCellsForSummary(summaryDestSheet);
            ExcelUtilies.InsertRow(summaryDestSheet, "A1");
            ExcelUtilies.InsertRow(summaryDestSheet, "A1");
            ExcelUtilies.InsertRow(summaryDestSheet, "A1");

            summaryDestSheet.Range["A4", "AX4"].Font.Bold = true;
            summaryDestSheet.Range["A3", "AX3"].Merge();
            summaryDestSheet.Range["A3", "AX3"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            summaryDestSheet.Range["A3", "AX3"].VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            summaryDestSheet.Range["A3", "AX3"].Value = "Funnel Performance";
            summaryDestSheet.Range["A3", "AX3"].Font.Color = Color.White;
            summaryDestSheet.Range["A3", "AX3"].Font.Bold = true;
            summaryDestSheet.Range["A3", "AX3"].Interior.Color = Color.DarkBlue;

            summaryDestSheet.Range["A1", "C1"].Merge();
            summaryDestSheet.Range["A1", "C1"].Value = "Center Month To Date Summary as";
            summaryDestSheet.Range["A1", "K1"].Font.Bold = true;

            summaryDestSheet.Range["F1", "H1"].Merge();
            summaryDestSheet.Range["F1", "H1"].Value = "Total days of this month";

            summaryDestSheet.Range["D1"].Value = DateTime.Today.ToString("yyyy/MM/dd");
            summaryDestSheet.Range["D1"].Font.Color = Color.Red;
            summaryDestSheet.Range["D1"].ColumnWidth = 10;

            DateTime date = DateTime.Today;
           
            summaryDestSheet.Range["I1"].Value = DateTime.DaysInMonth(date.Year, date.Month);
            summaryDestSheet.Range["I1"].Font.Color = Color.Red;

            summaryDestSheet.Range["K1", "L1"].Merge();
            summaryDestSheet.Range["K1","L1"].Value = "Time Ratio";

            summaryDestSheet.Range["M1", "N1"].Merge();
            summaryDestSheet.Range["M1","N1"].Value = string.Format("{0:N2}%", ((float)date.Day / (float)DateTime.DaysInMonth(date.Year, date.Month))*100);
            summaryDestSheet.Range["M1","N1"].Font.Color = Color.Red;

            ExcelUtilies.FreezePanes(summaryDestSheet, 2, 4);
        }

        private void MergeCellsForSummary(Excel.Worksheet summaryDestSheet)
        {
            int row = 2;
            int totalRow = 0;
            int startRow = row;
            int endRow = row;
            string label = summaryDestSheet.Cells[row, 1].Text;
            string str = string.Empty;
            do
            {
                str = summaryDestSheet.Cells[row, 1].Text;
                if (string.IsNullOrWhiteSpace(str) && totalRow == 0)
                {
                    totalRow = row;
                }

                if (str == label)
                {
                    row++;
                }
                else
                {
                    endRow = row - 1;
                    Range range = summaryDestSheet.Range["A" + startRow, "A" + endRow];
                    this.MergeCells(range, label);

                    label = str;
                    startRow = row;
                    row++;
                }
            } while ((row < 100 && string.IsNullOrWhiteSpace(str)) || !string.IsNullOrWhiteSpace(str));

            
        }

        private void MergeCells(Range range, string label)
        {
            range.Clear();
            range.Merge();
            range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            range.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            range.Font.Bold = true;
            range.Value = label;
        }
    }
}
