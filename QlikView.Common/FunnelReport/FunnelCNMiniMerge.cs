using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.IO;
using Microsoft.Office.Interop.Excel;
using System.Drawing;
using QlikView.Common.Zip;

namespace QlikView.Common
{
    public class FunnelCNMiniMerge : IExcelMerge
    {
        public ILog Logger
        {
            get;
            set;
        }

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

        private Color ColumnColor
        {
            get
            {
                return Color.FromArgb(153, 153, 255);
            }
        }


        Excel.Application excel;

        Excel.Workbook centerNormalWeeklyBook = null;
        Excel.Workbook TSNormalWeeklyBook = null;
        Excel.Workbook summaryBook = null;
        Excel.Workbook summaryRevenueByPac = null;

        public bool MergeFiles(Dictionary<string, ReportContext> MergedFiles, string outputFile, out string mergedFile)
        {
            if (this.Logger == null)
                this.Logger = new QVConfigLog();

            this.Logger.Message("Beigin to merge funnel reports.");
            excel = new Excel.Application();

            Dictionary<string, string> centerNormalWeeklyReports = FunnelReportHelper.GetMergedFilesByCategory(MergedFiles, "DetailNormalWeekly");
            Dictionary<string, string> centerSpecialWeeklyReports = FunnelReportHelper.GetMergedFilesByCategory(MergedFiles, "DetailSpecialWeekly");
            Dictionary<string, string> centerDailyReports = FunnelReportHelper.GetMergedFilesByCategory(MergedFiles, "DetailDaily");
            Dictionary<string, string> TSNormalWeeklyReports = FunnelReportHelper.GetMergedFilesByCategory(MergedFiles, "TSNormalWeekly");
            Dictionary<string, string> TSDailyReports = FunnelReportHelper.GetMergedFilesByCategory(MergedFiles, "TSDaily");

            string SummaryMonthlyReport = MergedFiles["FunnelCNMini_Summary_Monthly"].OutputFullName;
            string SummaryDailyReport = MergedFiles["FunnelCNMini_Summary_Daily"].OutputFullName;
            string SummaryTargetReport = MergedFiles["FunnelCNMini_Summary_Target"].OutputFullName;
            string SummaryRevenueByPACReport = MergedFiles["FunnelCNMini_Summary_RevenueByPAC"].OutputFullName;
            string SummaryTargetB2BReport = MergedFiles["FunnelCNMini_Summary_TargetB2B"].OutputFullName;

            Excel.Workbook bookDest = null;
            Excel.Workbook tempWorkbook = null;


            try
            {
                tempWorkbook = excel.Workbooks.Add(Missing.Value);

                //create a new work sheet
                Excel.Worksheet tempWorkSheet = tempWorkbook.Worksheets[1] as Excel.Worksheet;

                this.Logger.Message("Copy to tempwork.");

                #region Summary
                //Summary
                summaryBook = excel.Workbooks.Open(SummaryDailyReport);
                Excel.Worksheet summaryMonthlySheet = summaryBook.Worksheets[1];
                summaryMonthlySheet.Name = "Summary";

                //copy DetailNormalWeekly report to dest
                //Column A - EG
                summaryMonthlySheet.Copy(Missing.Value, tempWorkSheet);
                Excel.Worksheet summaryDestSheet = tempWorkbook.Worksheets["Summary"];
                int rowCount = this.GetSummaryRowCount(summaryDestSheet);
                //Append summary monthly
                this.AppendReport(tempWorkbook, summaryDestSheet, "Summary", "A2", "S25", "B" + rowCount, SummaryMonthlyReport, 1);

                //Append target report
                int targetStartRow = (rowCount + 2)*2 + 4;
                //set label
                summaryDestSheet.Range["B" + (targetStartRow - 1)].Value = "B2C";
                summaryDestSheet.Range["B" + (targetStartRow - 1)].Font.Bold = true;
                this.AppendReport(tempWorkbook, summaryDestSheet, "Summary", "A1", "K" + (FunnelCNMiniMergeParameters.TotalCity + 4), "B" + targetStartRow, SummaryTargetReport, 2);

                //Append target B2B
                int targetB2BStartRow = targetStartRow + FunnelCNMiniMergeParameters.TotalCity + 2 + 4;
                summaryDestSheet.Range["B" + (targetB2BStartRow - 1)].Value = "B2B";
                summaryDestSheet.Range["B" + (targetB2BStartRow - 1)].Font.Bold = true;
                this.AppendReport(tempWorkbook, summaryDestSheet, "Summary", "A1", "K" + (FunnelCNMiniMergeParameters.TotalCity + 4), "B" + targetB2BStartRow, SummaryTargetB2BReport, 3);


                this.FormatSummary(summaryDestSheet);
                #endregion

                #region Summary Revenue by PAC

                summaryRevenueByPac = excel.Workbooks.Open(SummaryRevenueByPACReport);
                Excel.Worksheet revenueByPacSheet = summaryRevenueByPac.Worksheets[1];
                revenueByPacSheet.Name = "Mini Center Rev By PAC";

                revenueByPacSheet.Copy(Missing.Value, tempWorkSheet);
                Excel.Worksheet revenueByPacDestSheet = tempWorkbook.Worksheets["Mini Center Rev By PAC"];
                this.FormatRevenueByPac(revenueByPacDestSheet);

                #endregion

                //Detail
                foreach (var item in centerNormalWeeklyReports.Keys)
                {
                    string header = item + " Details";

                    centerNormalWeeklyBook = excel.Workbooks.Open(centerNormalWeeklyReports[item]);
                    Excel.Worksheet centerNormalWeeklySheet = centerNormalWeeklyBook.Worksheets[1];
                    centerNormalWeeklySheet.Name = header;

                    //copy DetailNormalWeekly report to dest
                    //Column A - EG
                    centerNormalWeeklySheet.Copy(Missing.Value, tempWorkSheet);
                    Excel.Worksheet copyDestSheet = tempWorkbook.Worksheets[header];
                    ExcelUtilies.DeleteColumns(copyDestSheet, "A", "A");

                    //Append DetailSpecialWeekly
                    int normalWeeklyColumnCount = this.GetColumnHasValueEndIndex(copyDestSheet,2, 1);
                    string specialWeeklyColumnStart = ExcelUtilies.ExcelColumnIndexToName(normalWeeklyColumnCount + 1);

                    this.AppendReport(tempWorkbook, copyDestSheet, header, "B1", "AR" + this.GetWeeks(CurrentDate), specialWeeklyColumnStart + "1", centerSpecialWeeklyReports[item], 1);

                    //if (item == "NJ2")
                    //    this.AppendReport(tempWorkbook, copyDestSheet, header, "B1", "AR" + this.GetWeeks(CurrentDate), "CJ1", centerSpecialWeeklyReports[item], 1);
                    //else
                    //    this.AppendReport(tempWorkbook, copyDestSheet, header, "B1", "AR" + this.GetWeeks(CurrentDate), "DR1", centerSpecialWeeklyReports[item], 1);

                    //Append Daily report
                    this.AppendReport(tempWorkbook, copyDestSheet, header, "A1", "EG" + this.GetDays(CurrentDate), "A" + (this.GetWeeks(CurrentDate) + 3), centerDailyReports[item], 2);

                    this.FormatDetail(copyDestSheet, item);
                }

                //Mini TS
                foreach (var item in TSNormalWeeklyReports.Keys)
                {
                    string header = "Mini TS " + item;

                    TSNormalWeeklyBook = excel.Workbooks.Open(TSNormalWeeklyReports[item]);
                    Excel.Worksheet TSNormalWeeklySheet = TSNormalWeeklyBook.Worksheets[1];
                    TSNormalWeeklySheet.Name = header;

                    //copy TSNormalWeekly report to dest
                    //Column A - EG
                    TSNormalWeeklySheet.Copy(Missing.Value, tempWorkSheet);
                    Excel.Worksheet copyDestSheet = tempWorkbook.Worksheets[header];
                    ExcelUtilies.DeleteColumns(copyDestSheet, "A", "A");

                    //Append Daily report
                    this.AppendReport(tempWorkbook, copyDestSheet, header, "A1", "EG" + this.GetDays(CurrentDate), "A" + (this.GetWeeks(CurrentDate) + 3), TSDailyReports[item], 2);

                    this.FormatMiniTS(copyDestSheet,item);
                }

                this.Logger.Message("Create dest book.");
                bookDest = excel.Workbooks.Add(Missing.Value);


                //create a new work sheet
                //copy to dest from temp workbook
                //=======================================================================================
                //Why here copy all the sheets to another temp workbook?
                //I want to delete the sheeets like sheet1, sheet2, US1 etc. But it doesn't work if deleting
                // from tempWorkbook directly. So I copy to a new workbook which can fix the issue.
                // I don't know why but it does work.
                //=======================================================================================
                this.Logger.Message("create a new work sheet from dest book.");
                Excel.Worksheet sheetDest = bookDest.Worksheets[3] as Excel.Worksheet;
                foreach (var item in tempWorkbook.Worksheets)
                {
                    Excel.Worksheet sheet = item as Excel.Worksheet;

                    if (centerNormalWeeklyReports.Keys.Contains(sheet.Name.Replace(" Details", "")))
                    {
                        sheet.Copy(Missing.Value, sheetDest);
                    }

                    if (TSNormalWeeklyReports.Keys.Contains(sheet.Name.Replace("Mini TS ","")))
                    {
                        sheet.Copy(Missing.Value, sheetDest);
                    }

                    if (sheet.Name == "Mini Center Rev By PAC")
                        sheet.Copy(Missing.Value, sheetDest);

                    if (sheet.Name == "Summary")
                        sheet.Copy(Missing.Value, sheetDest);
                }

                //clear the temp workbook
                this.Logger.Message("clear the temp workbook");
                FunnelReportHelper.SaveTempWorkbook(tempWorkbook);

                //delete sheet1 sheet2 sheet3
                this.Logger.Message("delete sheet1 sheet2 sheet3");
                bookDest.Sheets["sheet1"].Delete();
                bookDest.Sheets["sheet2"].Delete();
                bookDest.Sheets["sheet3"].Delete();

                //save the dest
                this.Logger.Message("save the dest.");
                mergedFile = outputFile.Replace(".xls", "_" + DateTime.Now.ToString("yyyyMMdd") + ".xls");
                if (File.Exists(mergedFile))
                    File.Delete(mergedFile);

                bookDest.SaveAs(mergedFile);
                bookDest.Close();

                //zip the mergefile
                ZipHelper.ZipFile(mergedFile,mergedFile.Replace(".xls", ".zip"),1);
                mergedFile = mergedFile.Replace(".xls", ".zip");
            }
            catch (Exception ex)
            {
                this.Logger.Error(ex.Message + "\n" + ex.StackTrace);
                FunnelReportHelper.SaveTempWorkbook(tempWorkbook);
                FunnelReportHelper.SaveTempWorkbook(bookDest);

                throw;
            }
            finally
            {
                FunnelReportHelper.CloseWorkingWorkbook(centerNormalWeeklyBook);
                FunnelReportHelper.CloseWorkingWorkbook(TSNormalWeeklyBook);
                FunnelReportHelper.CloseWorkingWorkbook(summaryBook);
                FunnelReportHelper.CloseWorkingWorkbook(summaryRevenueByPac);
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="copyDestSheet"></param>
        /// <param name="row"></param>
        /// <param name="calculateBeginColum"></param>
        /// <returns></returns>
        private int GetColumnHasValueEndIndex(Worksheet copyDestSheet, int row, int calculateBeginColum)
        {
            int index = calculateBeginColum;
            string str = copyDestSheet.Cells[row, index].Text;            

            while (!string.IsNullOrWhiteSpace(str))
            {
                index++;
                str = copyDestSheet.Cells[row, index].Text; 
            }

            return index--;
        }

        private void FormatRevenueByPac(Worksheet revenueByPacDestSheet)
        {
            if (revenueByPacDestSheet.Cells[1, 4].Text == "DataSource")
                ExcelUtilies.DeleteColumns(revenueByPacDestSheet, "D", "D");

            if(revenueByPacDestSheet.Cells[1, 1].Text == "YearMonth")
                ExcelUtilies.DeleteColumns(revenueByPacDestSheet, "A", "A");

            if (revenueByPacDestSheet.Cells[1, 2].Text == "CityName")
                revenueByPacDestSheet.Range["B1"].Value = "";

            if (revenueByPacDestSheet.Cells[1, 1].Text == "ProductCategory")
            {
                revenueByPacDestSheet.Range["A1"].Value = "MTD";

                int endRow = 2;

                string label = revenueByPacDestSheet.Cells[endRow, 1].Text;

                while (!string.IsNullOrWhiteSpace(label))
                {
                    endRow++;
                    label = revenueByPacDestSheet.Cells[endRow, 1].Text;
                }

                endRow--;

                Range range = revenueByPacDestSheet.Range["A2", "A" + endRow];
                this.MergeCells(range, "Mini Center");
            }

            revenueByPacDestSheet.Range["A1", "H12"].Interior.ColorIndex = 0;
            //revenueByPacDestSheet.Range["A1", "F1"].Interior.Color = this.ColumnColor;
            revenueByPacDestSheet.Range["A1", "F1"].Font.Bold = true;

            ExcelUtilies.InsertRow(revenueByPacDestSheet, "A1");
            ExcelUtilies.InsertRow(revenueByPacDestSheet, "A1");
            ExcelUtilies.InsertRow(revenueByPacDestSheet, "A1");

            revenueByPacDestSheet.Range["A1"].Value = "Currency";
            revenueByPacDestSheet.Range["A2"].Value = "RMB";
            revenueByPacDestSheet.Range["A2"].Font.Bold = true;
            revenueByPacDestSheet.Range["A2"].Interior.Color = this.ColumnColor;

            this.MergeCells(revenueByPacDestSheet.Range["A3", "C3"], "Mini Center Rev not include Mini TS and B2B");
        }

        private void FormatSummary(Excel.Worksheet summaryDestSheet)
        {
            summaryDestSheet.Range["A1", "T100"].Interior.ColorIndex = 0;
            this.MergeCellsForSummary(summaryDestSheet);
            ExcelUtilies.InsertRow(summaryDestSheet, "A1");
            ExcelUtilies.InsertRow(summaryDestSheet, "A1");
            ExcelUtilies.InsertRow(summaryDestSheet, "A1");

            summaryDestSheet.Range["A4", "T4"].Font.Bold = true;
            summaryDestSheet.Range["A3", "T3"].Merge();
            summaryDestSheet.Range["A3", "T3"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            summaryDestSheet.Range["A3", "T3"].VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            summaryDestSheet.Range["A3", "T3"].Value = "Funnel Performance";
            summaryDestSheet.Range["A3", "T3"].Font.Color = Color.White;
            summaryDestSheet.Range["A3", "T3"].Font.Bold = true;
            summaryDestSheet.Range["A3", "T3"].Interior.Color = Color.DarkBlue;

            summaryDestSheet.Range["A1", "C1"].Merge();
            summaryDestSheet.Range["A1", "C1"].Value = "Mini Center Month To Date Summary as";
            summaryDestSheet.Range["A1", "H1"].Font.Bold = true;

            summaryDestSheet.Range["F1", "G1"].Merge();
            summaryDestSheet.Range["F1", "G1"].Value = "Total days of this month";

            summaryDestSheet.Range["D1"].Value = summaryDestSheet.Cells[5, 1].Text;
            summaryDestSheet.Range["D1"].Font.Color = Color.Red;

            DateTime date;
            if (DateTime.TryParse(summaryDestSheet.Cells[1,4].Text, out date))
            {
                summaryDestSheet.Range["H1"].Value = DateTime.DaysInMonth(date.Year, date.Month);
            }
            else
            {
                summaryDestSheet.Range["H1"].Value = DateTime.DaysInMonth(DateTime.Now.AddDays(-1).Year, DateTime.Now.AddDays(-1).Month);
            }
            summaryDestSheet.Range["H1"].Font.Color = Color.Red;

            ExcelUtilies.FreezePanes(summaryDestSheet, 3, 4);
        }

        private void MergeCellsForSummary(Excel.Worksheet summaryDestSheet)
        {
            int row = 2;
            int totalRow = 0;
            int startRow = row;
            int endRow = row;
            string label = summaryDestSheet.Cells[row, 2].Text;
            string str = string.Empty;
            do
            {
                str = summaryDestSheet.Cells[row, 2].Text;
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
                    Range range = summaryDestSheet.Range["B" + startRow, "B" + endRow];
                    this.MergeCells(range, label);

                    label = str;
                    startRow = row;
                    row++;
                }
            } while ((row < 40 && string.IsNullOrWhiteSpace(str)) || !string.IsNullOrWhiteSpace(str));

            string date = summaryDestSheet.Cells[2, 1].Text;
            Range range1 = summaryDestSheet.Range["A2", "A" + (totalRow - 1)];
            this.MergeCells(range1, date);

            if (endRow >= totalRow)
            {
                string MTDTotal = summaryDestSheet.Range["B" + endRow].Value;

                if (!string.IsNullOrWhiteSpace(MTDTotal) && MTDTotal == "MTD Total")
                {
                    range1 = summaryDestSheet.Range["A" + (totalRow + 1), "A" + (endRow - 1)];
                    this.MergeCells(range1, "MTD");

                    summaryDestSheet.Range["A" + endRow].Value = summaryDestSheet.Range["B" + endRow].Value;
                    summaryDestSheet.Range["B" + endRow].Clear();
                    summaryDestSheet.Range["A" + endRow].Font.Bold = true;
                }
                else
                {
                    range1 = summaryDestSheet.Range["A" + (totalRow + 1), "A" + endRow];
                    this.MergeCells(range1, "MTD");
                }
            }
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

        private void FormatDetailHeaders(Excel.Worksheet copyDestSheet, string header)
        {
            int normalWeeklyStartColumn = 2;
            int normalWeeklyEndColumn = this.GetColumnHasValueEndIndex(copyDestSheet,1, 2);

            this.MergeHeader(copyDestSheet, 1, normalWeeklyStartColumn, normalWeeklyEndColumn - 1, header);

            int specialWeeklyEndColumn = this.GetColumnHasValueEndIndex(copyDestSheet,1, normalWeeklyEndColumn + 2);
            this.MergeHeader(copyDestSheet, 1, normalWeeklyEndColumn + 2, specialWeeklyEndColumn - 1, header);

            var range = copyDestSheet.Range["A1", copyDestSheet.Cells[2, specialWeeklyEndColumn - 1]];
            range.Borders.Weight = 2;
            range.Borders.Color = Color.Black;

        }

        private void FormatDetail(Excel.Worksheet copyDestSheet, string header)
        {
            copyDestSheet.Range["A1","GI500"].Interior.ColorIndex = 0;

            this.FormatDetailHeaders(copyDestSheet, header);
            //Range range = copyDestSheet.Range[FunnelCNMiniMergeParameters.Online_BeginCell, FunnelCNMiniMergeParameters.Online_EndCell];
            //this.MergeCells(range, header + " Online");

            //range = copyDestSheet.Range[FunnelCNMiniMergeParameters.IB_BeginCell, FunnelCNMiniMergeParameters.IB_EndCell];
            //this.MergeCells(range, header + " IB");

            //range = copyDestSheet.Range[FunnelCNMiniMergeParameters.WI_BeginCell, FunnelCNMiniMergeParameters.WI_EndCell];
            //this.MergeCells(range, header + " WI");

            //range = copyDestSheet.Range[FunnelCNMiniMergeParameters.Offline_BeginCell, FunnelCNMiniMergeParameters.Offline_EndCell];
            //this.MergeCells(range, header + " Offline");

            //if (copyDestSheet.Name == "NJ2 Details")
            //{
            //    //No B2B and Others
            //    //range = copyDestSheet.Range[FunnelCNMiniMergeParameters.B2B_BeginCell, FunnelCNMiniMergeParameters.B2B_EndCell];
            //    //this.MergeCells(range, header + " B2B");

            //    //range = copyDestSheet.Range[FunnelCNMiniMergeParameters.Others_BeginCell, FunnelCNMiniMergeParameters.Others_EndCell];
            //    //this.MergeCells(range, header + " Others");


            //    range = copyDestSheet.Range[FunnelCNMiniMergeParameters.B2B_BeginCell, FunnelCNMiniMergeParameters.B2B_EndCell];
            //    this.MergeCells(range, header + " Total");

            //    range = copyDestSheet.Range[FunnelCNMiniMergeParameters.Online_BeginCell, FunnelCNMiniMergeParameters.B2B_EndCell.Replace("1", "2")];
            //    range.Interior.Color = this.HeaderColor;

            //    range = copyDestSheet.Range[FunnelCNMiniMergeParameters.B2B_BeginCell, FunnelCNMiniMergeParameters.B2B_EndCell.Replace("1", "2")];
            //    range.Interior.Color = Color.Gray;

            //    //merge cells for special weekly
            //    int startColumn = ExcelUtilies.ExcelColumnNameToIndex("CK");
            //    int endColum = this.MergeHeader(copyDestSheet, 1, startColumn, header);

            //    range = copyDestSheet.Range["A1", copyDestSheet.Cells[2, endColum - 2]];
            //    range.Borders.Weight = 2;
            //    range.Borders.Color = Color.Black;
            //}
            //else
            //{
            //    range = copyDestSheet.Range[FunnelCNMiniMergeParameters.B2B_BeginCell, FunnelCNMiniMergeParameters.B2B_EndCell];
            //    this.MergeCells(range, header + " B2B");

            //    range = copyDestSheet.Range[FunnelCNMiniMergeParameters.Others_BeginCell, FunnelCNMiniMergeParameters.Others_EndCell];
            //    this.MergeCells(range, header + " Others");

            //    range = copyDestSheet.Range[FunnelCNMiniMergeParameters.Online_BeginCell, FunnelCNMiniMergeParameters.Total_EndCell.Replace("1", "2")];
            //    range.Interior.Color = this.HeaderColor;

            //    range = copyDestSheet.Range[FunnelCNMiniMergeParameters.Total_BeginCell, FunnelCNMiniMergeParameters.Total_EndCell];
            //    this.MergeCells(range, header + " Total");

            //    range = copyDestSheet.Range[FunnelCNMiniMergeParameters.Total_BeginCell, FunnelCNMiniMergeParameters.Total_EndCell.Replace("1", "2")];
            //    range.Interior.Color = Color.Gray;

            //    //merge cells for special weekly
            //    int startColumn = ExcelUtilies.ExcelColumnNameToIndex("DS");
            //    int endColum = this.MergeHeader(copyDestSheet, 1, startColumn, header);

            //    range = copyDestSheet.Range["A1", copyDestSheet.Cells[2, endColum - 2]];
            //    range.Borders.Weight = 2;
            //    range.Borders.Color = Color.Black;
            //}

            //color column A
            copyDestSheet.Range["A1"].EntireColumn.Interior.ColorIndex = 0;

            int begin = this.GetWeeks(CurrentDate) + 3;
            int end = begin + this.GetDays(CurrentDate);
            //delete daily headers
            ExcelUtilies.DeleteRow(copyDestSheet, "A" + begin);
            ExcelUtilies.DeleteRow(copyDestSheet, "A" + begin);
            copyDestSheet.Range["A" + begin,"A" + end].Interior.Color = this.ColumnColor;

            this.DeleteEmptyRows(copyDestSheet);
            
            //Set currency label
            ExcelUtilies.InsertRow(copyDestSheet, "A1");
            ExcelUtilies.InsertRow(copyDestSheet, "A1");
            ExcelUtilies.InsertRow(copyDestSheet, "A1");

            copyDestSheet.Range["A1"].Value = "Currency";
            copyDestSheet.Range["A2"].Value = "RMB";
            copyDestSheet.Range["A2"].Font.Bold = true;
            copyDestSheet.Range["A2"].Interior.Color = this.ColumnColor;

            ExcelUtilies.FreezePanes(copyDestSheet, 1, 5);
        }

        private void DeleteEmptyRows(Worksheet copyDestSheet)
        {
            int row = 1;
            string str = copyDestSheet.Cells[row, 1].Text;

            while (!string.IsNullOrWhiteSpace(str))
            {
                row++;
                str = copyDestSheet.Cells[row, 1].Text;
            }

            copyDestSheet.Range["A" + row].Value = "Total";

            row = row + 4;

            str = copyDestSheet.Cells[row, 1].Text;

            while (string.IsNullOrWhiteSpace(str))
            {
                ExcelUtilies.DeleteRow(copyDestSheet, "A" + row);
                str = copyDestSheet.Cells[row, 1].Text;
            }
        }

        private void FormatMiniTS(Excel.Worksheet copyDestSheet, string header)
        {
            copyDestSheet.Range["A1", "GI500"].Interior.ColorIndex = 0;

            int endColum = this.GetColumnHasValueEndIndex(copyDestSheet,1, 2);
            this.MergeHeader(copyDestSheet, 1, 2, endColum - 1 , "Mini TS " + header);
            Range range = copyDestSheet.Range["A1", copyDestSheet.Cells[2, endColum - 1]];
            range.Borders.Weight = 2;
            range.Borders.Color = Color.Black;

            //color column A
            copyDestSheet.Range["A1"].EntireColumn.Interior.ColorIndex = 0;

            int begin = this.GetWeeks(CurrentDate) + 3;
            int end = begin + this.GetDays(CurrentDate);
            ExcelUtilies.DeleteRow(copyDestSheet, "A" + begin);
            ExcelUtilies.DeleteRow(copyDestSheet, "A" + begin);
            copyDestSheet.Range["A" + begin, "A" + end].Interior.Color = this.ColumnColor;

            this.DeleteEmptyRows(copyDestSheet);

            //Set currency label
            ExcelUtilies.InsertRow(copyDestSheet, "A1");
            ExcelUtilies.InsertRow(copyDestSheet, "A1");
            ExcelUtilies.InsertRow(copyDestSheet, "A1");

            copyDestSheet.Range["A1"].Value = "Currency";
            copyDestSheet.Range["A2"].Value = "RMB";
            copyDestSheet.Range["A2"].Font.Bold = true;
            copyDestSheet.Range["A2"].Interior.Color = this.ColumnColor;

            ExcelUtilies.FreezePanes(copyDestSheet, 1, 5);
        }

        private void MergeHeader(Excel.Worksheet sheet, int rowNumber, int startColumn, int endColumn, string prefix)
        {
            int start = startColumn;
            int end = startColumn;
            string label = sheet.Cells[rowNumber, start].Text; 
            
            while (end <= endColumn)
            {
                string str = sheet.Cells[rowNumber, end].Text;

                //the same, move on
                if (str == label && end < endColumn)
                {
                    end++;
                }
                else
                {
                    if (end < endColumn)
                        end = end - 1;

                    Range range = sheet.Range[sheet.Cells[rowNumber, start], sheet.Cells[rowNumber, end]];
                    this.MergeCells(range, prefix + " " + label);

                    if (label != "Total")
                    {
                        sheet.Range[sheet.Cells[rowNumber, start], sheet.Cells[rowNumber + 1, end]].Interior.Color = this.HeaderColor;
                    }
                    else
                    {
                        sheet.Range[sheet.Cells[rowNumber, start], sheet.Cells[rowNumber + 1, end]].Interior.Color = Color.Gray;
                    }

                    label = str;

                    end++;
                    start = end;                    
                }
            }

        }

        private int GetSummaryRowCount(Excel.Worksheet summaryDestSheet)
        {
            int row = 2;

            do
            {
                row++;
            }
            while (!string.IsNullOrWhiteSpace(summaryDestSheet.Cells[row,1].Text));

            return row;
        }

        private void AppendReport(Excel.Workbook destWorkbook, Excel.Worksheet copyDestSheet, string sheetName, string sourceBeginCell, string sourceEndCell, string destBeginCell, string reportFile, int sequence)
        {
            if (File.Exists(reportFile))
            {
                Excel.Workbook sourceBook = null;

                try
                {
                    sourceBook = excel.Workbooks.Open(reportFile);
                    Excel.Worksheet workingSheet = sourceBook.Worksheets[1];
                    workingSheet.Name = sheetName + sequence;

                    Excel.Worksheet tempWorkSheet = destWorkbook.Sheets[1] as Excel.Worksheet;
                    workingSheet.Copy(Missing.Value, tempWorkSheet);
                    Excel.Worksheet copySourceSheet = destWorkbook.Worksheets[sheetName + sequence];

                    ExcelUtilies.CopyRange(copySourceSheet, sourceBeginCell, sourceEndCell, copyDestSheet, destBeginCell);
                }
                catch (Exception ex)
                {
                    this.Logger.Error(string.Format("Append {0} report failed! \n {1}", sheetName, ex.StackTrace));
                    throw;
                }
                finally
                {
                    FunnelReportHelper.CloseWorkingWorkbook(sourceBook);
                }
            }
        }

        private int GetWeeks(DateTime date)
        {
            int months = (date.Month - 10 + 12) % 12;

            int weeks = 0;
            if (date.Day >= 29)
            {
                weeks = months * 6 + 6 + 3;
            }
            else if (date.Day >= 22)
            {
                weeks = months * 6 + 5 + 3;
            }
            else if (date.Day >= 15)
            {
                weeks = months * 6 + 4 + 3;
            }
            else if (date.Day >= 8)
            {
                weeks = months * 6 + 3 + 3;
            }
            else
            {
                weeks = months * 6 + 2 + 3;
            }

            //Two year weeks
            return weeks + 6*5;
        }

        private int GetDays(DateTime date)
        {
            if (date.Month >= 10)
            {
                return (int)(date - new DateTime(date.Year, 10, 1)).TotalDays + 4 + 6*30;
            }
            else
            {
                return (int)(date - new DateTime((date.Year - 1), 10, 1)).TotalDays + 4 + 6*30;
            }
        }
    }
}
