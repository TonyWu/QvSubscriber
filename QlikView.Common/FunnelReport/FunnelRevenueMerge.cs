using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Reflection;
using System.Drawing;

namespace QlikView.Common
{
    class FunnelRevenueMerge : IExcelMerge
    {
        public ILog Logger { get; set; }

        Excel.Application excel;

        Excel.Workbook totalBook = null;
        Excel.Workbook TSBook = null;
        Excel.Workbook onlineBook = null;

        public bool MergeFiles(Dictionary<string, ReportContext> MergedFiles, string outputFile, out string mergedFile)
        {
            mergedFile = string.Empty;

            if (this.Logger == null)
                this.Logger = new QVConfigLog();

            this.Logger.Message("Beigin to merge funnel revenue reports.");
            excel = new Excel.Application();

            Dictionary<string, string> totalReports = FunnelReportHelper.GetMergedFilesByCategory(MergedFiles, "Total");
            Dictionary<string, string> tsReports = FunnelReportHelper.GetMergedFilesByCategory(MergedFiles, "TS");
            Dictionary<string, string> onlineReports = FunnelReportHelper.GetMergedFilesByCategory(MergedFiles, "Online");

            Excel.Workbook tempWorkbook = null;
            Excel.Workbook bookDest = null;

            try
            {
                tempWorkbook = excel.Workbooks.Add(Missing.Value);

                //create a new work sheet
                Excel.Worksheet tempWorkSheet = tempWorkbook.Worksheets[1] as Excel.Worksheet;

                this.Logger.Message("Copy to tempwork.");

                foreach (var item in totalReports.Keys)
                {
                    totalBook = excel.Workbooks.Open(totalReports[item]);
                    Excel.Worksheet totalSheet = totalBook.Worksheets[1];
                    totalSheet.Name = item;

                    //copy month report to dest
                    //Column
                    totalSheet.Copy(Missing.Value, tempWorkSheet);
                    Excel.Worksheet copyDestSheet = tempWorkbook.Worksheets[item];
                    
                    //append TS reports
                    //Column
                    if (tsReports.ContainsKey(item))
                    {
                        TSBook = excel.Workbooks.Open(tsReports[item]);
                        Excel.Worksheet weekSheet = TSBook.Worksheets[1];
                        weekSheet.Name = item + "1";

                        weekSheet.Copy(Missing.Value, tempWorkSheet);
                        Excel.Worksheet copySourceSheet = tempWorkbook.Worksheets[item + "1"];

                        //copy week report to dest
                        ExcelUtilies.CopyRange(copySourceSheet, "A1", WeeklyStatisticReportParameters.TSColumn + WeeklyStatisticReportParameters.TSRowCount, copyDestSheet, "A22");
                        //ExcelUtilies.ReleaseComObject(weekSheet);                  
                    }

                    //append Online report
                    //Column 
                    if (onlineReports.ContainsKey(item))
                    {
                        onlineBook = excel.Workbooks.Open(onlineReports[item]);
                        Excel.Worksheet weekSheet = onlineBook.Worksheets[1];
                        weekSheet.Name = item + "2";

                        weekSheet.Copy(Missing.Value, tempWorkSheet);
                        Excel.Worksheet copySourceSheet = tempWorkbook.Worksheets[item + "2"];

                        //copy week report to dest
                        ExcelUtilies.CopyRange(copySourceSheet, "A1", WeeklyStatisticReportParameters.OnlineColumn + WeeklyStatisticReportParameters.OnlineRowCount, copyDestSheet, "A40");
                        //ExcelUtilies.ReleaseComObject(weekSheet);                  
                    }
                    
                    copyDestSheet.Range["A1", WeeklyStatisticReportParameters.TotalColumn + "100"].Interior.ColorIndex = 0;

                    Excel.Range range = copyDestSheet.Range["A1"];
                    range.EntireRow.Insert(Excel.XlInsertShiftDirection.xlShiftDown);
                    //ExcelUtilies.ReleaseComObject(range);

                    this.SetTitle(copyDestSheet, 1, 1, "Total by product");
                    this.SetTitle(copyDestSheet, 22, 1, "Telesales");
                    this.SetTitle(copyDestSheet, 40, 1, "Online");

                    if (GeneralParameters.KFR2 == false && GeneralParameters.KFR1 == false)
                    {
                        ExcelUtilies.DeleteColumns(copyDestSheet, "U", "AC");
                        ExcelUtilies.DeleteColumns(copyDestSheet, "I", "N");
                    }
                    else if (GeneralParameters.KFR1 == true && GeneralParameters.KFR2 == false)
                    {
                        ExcelUtilies.DeleteColumns(copyDestSheet, "X", "AC");
                        ExcelUtilies.DeleteColumns(copyDestSheet, "K", "N");
                    }

                    //ExcelUtilies.ReleaseComObject(copyDestSheet);
                    //close working workbook
                    this.CloseSourceReportsExcel();
                }

                //Copy KFR revenue to tempWorkSheet
                this.CopyKFRRevenue2SheetDest(tempWorkbook, MergedFiles);

                this.Logger.Message("Create dest book.");
                bookDest = excel.Workbooks.Add(Missing.Value);
                //get the first sheet of dest book
                this.Logger.Message("get the first sheet of dest book");
                Excel.Worksheet sheetDest = bookDest.Worksheets[1] as Excel.Worksheet;

                //copy to dest from temp workbook
                this.Logger.Message("copy to dest from temp workbook.");
                foreach (var item in tempWorkbook.Worksheets)
                {
                    Excel.Worksheet sheet = item as Excel.Worksheet;
                    if (totalReports.Keys.Contains(sheet.Name))
                    {
                        sheet.Copy(Missing.Value, sheetDest);
                    }
                    else if (sheet.Name == "KFR Revenue")
                    {
                        sheet.Copy(sheetDest, Missing.Value);
                    }
                }

                //delete sheet1 sheet2 sheet3
                excel.DisplayAlerts = false;
                this.Logger.Message("delete sheet1 sheet2 sheet3");
                foreach (var item in bookDest.Worksheets)
                {
                    Excel.Worksheet sheet = item as Excel.Worksheet;
                    if (!totalReports.Keys.Contains(sheet.Name) && sheet.Name != "KFR Revenue")
                        sheet.Delete();
                }

                //clear the temp workbook
                this.Logger.Message("clear the temp workbook");
                FunnelReportHelper.SaveTempWorkbook(tempWorkbook);

                //save the dest
                this.Logger.Message("save the dest.");
                mergedFile = outputFile.Replace(".xls", "_" + DateTime.Now.LastWeekendDate().WeekOfYearString() + ".xls");
                //mergedFile = outputFile.Replace(".xls", "_" + DateTime.Now.LatestTwoWeeksEndDate().WeekOfYearString() + ".xls");
                if (File.Exists(mergedFile))
                {
                    File.Delete(mergedFile);
                }
                bookDest.SaveAs(mergedFile);
                bookDest.Close();
                excel.DisplayAlerts = true;
            }
            catch (Exception ex)
            {
                this.Logger.Error(ex.Message + "\n" + ex.StackTrace);

                excel.DisplayAlerts = false;

                this.CloseSourceReportsExcel();
                FunnelReportHelper.SaveTempWorkbook(tempWorkbook);
                

                excel.DisplayAlerts = true;
                throw;
            }
            finally
            {
                excel.Application.Quit();
                //ExcelUtilies.ReleaseComObject(excel);
                //excel = null;
                //System.GC.Collect();

                ExcelUtilies.Kill(excel);
            }
            
            return true;
        }

        private void CopyKFRRevenue2SheetDest(Excel.Workbook tempWorkbook, Dictionary<string, ReportContext> MergedFiles)
        {
            Dictionary<string, string> KFRRevenueReports = FunnelReportHelper.GetMergedFilesByCategory(MergedFiles, "KFRRevenue");

            if (MergedFiles.ContainsKey("FunnelRevenue_KFRRevenue_NotSet"))
            {
                ReportContext report = MergedFiles["FunnelRevenue_KFRRevenue_NotSet"];

                Excel.Worksheet tempWorkSheet = tempWorkbook.Worksheets[1] as Excel.Worksheet;

                totalBook = excel.Workbooks.Open(report.OutputFullName);
                Excel.Worksheet totalSheet = totalBook.Worksheets[1];
                totalSheet.Name = "KFR Revenue";

                //copy month report to dest
                //Column
                totalSheet.Copy(Missing.Value, tempWorkSheet);
                Excel.Worksheet copyDestSheet = tempWorkbook.Worksheets["KFR Revenue"];

                //append TS reports
                //Column
                if (MergedFiles.ContainsKey("FunnelRevenue_KFRRevenue_CH157"))
                {
                    report = MergedFiles["FunnelRevenue_KFRRevenue_CH157"];
                    TSBook = excel.Workbooks.Open(report.OutputFullName);
                    Excel.Worksheet weekSheet = TSBook.Worksheets[1];
                    weekSheet.Name = report.Description;

                    weekSheet.Copy(Missing.Value, tempWorkSheet);
                    Excel.Worksheet copySourceSheet = tempWorkbook.Worksheets[report.Description];

                    //copy week report to dest
                    ExcelUtilies.CopyRange(copySourceSheet, "A1", WeeklyStatisticReportParameters.KFRRevenueColumn + WeeklyStatisticReportParameters.KFRRevenueRowCount, copyDestSheet, "A21");
                    //ExcelUtilies.ReleaseComObject(weekSheet);
                }

                //append Online report
                //Column 
                if (MergedFiles.ContainsKey("FunnelRevenue_KFRRevenue_CH157"))
                {
                    report = MergedFiles["FunnelRevenue_KFRRevenue_CH159"];
                    onlineBook = excel.Workbooks.Open(report.OutputFullName);
                    Excel.Worksheet weekSheet = onlineBook.Worksheets[1];
                    weekSheet.Name = report.Description;

                    weekSheet.Copy(Missing.Value, tempWorkSheet);
                    Excel.Worksheet copySourceSheet = tempWorkbook.Worksheets[report.Description];

                    //copy week report to dest
                    ExcelUtilies.CopyRange(copySourceSheet, "A1", WeeklyStatisticReportParameters.KFRRevenueColumn + WeeklyStatisticReportParameters.KFRRevenueRowCount, copyDestSheet, "A38");
                    //ExcelUtilies.ReleaseComObject(weekSheet);
                }

                copyDestSheet.Range["A1", WeeklyStatisticReportParameters.TotalColumn + "56"].Interior.ColorIndex = 0;

                if (GeneralParameters.KFR2 == false && GeneralParameters.KFR1 == false)
                {
                    ExcelUtilies.DeleteColumns(copyDestSheet, "S", "AA");
                    ExcelUtilies.DeleteColumns(copyDestSheet, "G", "L");
                }
                else if (GeneralParameters.KFR1 == true && GeneralParameters.KFR2 == false)
                {
                    ExcelUtilies.DeleteColumns(copyDestSheet, "V", "AA");
                    ExcelUtilies.DeleteColumns(copyDestSheet, "I", "L");
                }

                //ExcelUtilies.ReleaseComObject(copyDestSheet);
                this.CloseSourceReportsExcel();
            }           
        }

        private void CloseSourceReportsExcel()
        {
            FunnelReportHelper.CloseWorkingWorkbook(totalBook);
            FunnelReportHelper.CloseWorkingWorkbook(TSBook);
            FunnelReportHelper.CloseWorkingWorkbook(onlineBook);
        }

        private void SetTitle(Excel.Worksheet sheet, int row, int column, string title)
        {
            sheet.Cells[row, column] = title;
            sheet.Cells[row, column].Font.Size = 11;
            sheet.Cells[row, column].Font.Bold = true;
            //sheet.Cells[row, column].Font.Color = Color.White;
        }
    }
}
                    