using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.IO;
using System.Drawing;

namespace QlikView.Common
{
    public class FunnelMonthlyMerge:IExcelMerge
    {

        public ILog Logger { get; set; }

        Excel.Application excel;

        Excel.Workbook monthBook = null;

        public FunnelMonthlyMerge()
        {
        }

        #region IExcelOperation Members
        
        public bool MergeFiles(Dictionary<string, ReportContext> MergedFiles, string outputFile, out string mergedFile)
        {
            if (this.Logger == null)
                this.Logger = new QVConfigLog();

            this.Logger.Message("Beigin to merge funnel reports.");
            excel = new Excel.Application();

            Dictionary<string, string> monthlyReports = FunnelReportHelper.GetMergedFilesByCategory(MergedFiles, "ByMonth");
            Dictionary<string, string> YTDFunnelReports = FunnelReportHelper.GetMergedFilesByCategory(MergedFiles, "YTDFunnel");
            Dictionary<string, string> YTDKFROReports = FunnelReportHelper.GetMergedFilesByCategory(MergedFiles, "YTDKFRx");
            Dictionary<string, string> YTDACTVsKFROReports = FunnelReportHelper.GetMergedFilesByCategory(MergedFiles, "YTDACTVsKFRx");
            Dictionary<string, string> vs1011YTDReport = FunnelReportHelper.GetMergedFilesByCategory(MergedFiles, "VS1011YTD");
            Dictionary<string, string> YTD1011Report = FunnelReportHelper.GetMergedFilesByCategory(MergedFiles, "1011YTD");

            Excel.Workbook bookDest = null;
            Excel.Workbook tempWorkbook = null;

            try
            {
                tempWorkbook = excel.Workbooks.Add(Missing.Value);

                //create a new work sheet
                Excel.Worksheet tempWorkSheet = tempWorkbook.Worksheets[1] as Excel.Worksheet;

                this.Logger.Message("Copy to tempwork.");

                foreach (var item in monthlyReports.Keys)
                {
                    monthBook = excel.Workbooks.Open(monthlyReports[item]);
                    Excel.Worksheet monthSheet = monthBook.Worksheets[1];
                    monthSheet.Name = item;

                    //copy month report to dest
                    //Column B - M
                    monthSheet.Copy(Missing.Value, tempWorkSheet);
                    Excel.Worksheet copyDestSheet = tempWorkbook.Worksheets[item];

                    #region Append Reports                   

                    //append YTDFunnel report
                    //Column BO
                    if (YTDFunnelReports.ContainsKey(item))
                    {
                        this.AppendReport(tempWorkbook, copyDestSheet, item, FunnelMonthlyReportParameters.YTDFunnel, FunnelMonthlyReportParameters.Header_YTDACT_CurrentFisicalYear, YTDFunnelReports[item], 1);
                    }

                    //append YTDKFRO
                    //Column BR
                    if (YTDKFROReports.ContainsKey(item))
                    {
                        this.AppendReport(tempWorkbook, copyDestSheet, item, FunnelMonthlyReportParameters.YTDKFR0, FunnelMonthlyReportParameters.Header_YTDKFR_CurrentFisicalYear, YTDKFROReports[item], 2);
                    }

                    //append 1011YTD
                    //Column BP
                    if (YTD1011Report.ContainsKey(item))
                    {
                        this.AppendReport(tempWorkbook, copyDestSheet, item, FunnelMonthlyReportParameters.YTD1011, FunnelMonthlyReportParameters.Header_YTDACT_BeforeFisicalYear, YTD1011Report[item], 3);
                    }

                    //append YTDACTVsKFRO report
                    //Column BS
                    if (YTDACTVsKFROReports.ContainsKey(item))
                    {
                        this.AppendReport(tempWorkbook, copyDestSheet, item, FunnelMonthlyReportParameters.YTDACTVsKFR0, FunnelMonthlyReportParameters.Header_YTDACTVsKFR, YTDACTVsKFROReports[item], 4);
                    }


                    //append VS1011YTD
                    //Column BQ
                    if (vs1011YTDReport.ContainsKey(item))
                    {
                        this.AppendReport(tempWorkbook, copyDestSheet, item, FunnelMonthlyReportParameters.VS1011YTD, FunnelMonthlyReportParameters.Header_YTDACTVs_BeforeFisicalYear, vs1011YTDReport[item], 5);
                    }
                    #endregion                    

                    this.FormatFinalReportSheet(copyDestSheet);

                    this.CloseSourceReportsExcel();
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

                    if (monthlyReports.Keys.Contains(sheet.Name))
                    {
                        sheet.Copy(Missing.Value, sheetDest);
                    }
                }

                //Append output files
                FunnelMonthlyOutputHelper outputHelper = new FunnelMonthlyOutputHelper()
                {
                    Logger = this.Logger
                };
                outputHelper.MergedOutputFiles(sheetDest, MergedFiles);

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
                mergedFile = outputFile.Replace(".xls", "_" + DateTime.Now.Year.ToString() + "Month" + DateTime.Now.Month + ".xls");
                if (File.Exists(mergedFile))
                    File.Delete(mergedFile);

                bookDest.SaveAs(mergedFile);
                bookDest.Close();

            }
            catch (Exception ex)
            {
                this.Logger.Error(ex.Message + "\n" + ex.StackTrace);

                this.CloseSourceReportsExcel();

                FunnelReportHelper.SaveTempWorkbook(tempWorkbook);
                FunnelReportHelper.SaveTempWorkbook(bookDest);
                
                throw;
            }
            finally
            {
                excel.Application.Quit();
            }

            return true;
        }

        #endregion

        private void FormatFinalReportSheet(Excel.Worksheet sheet)
        {
            //Insert a row to set the title
            ExcelUtilies.InsertRow(sheet, "A1");
            sheet.Cells[1, 1] = sheet.Name;

            //insert two rows, total excel start with Row 3
            ExcelUtilies.InsertRow(sheet, "A1");

            this.SetBackgroundColor(sheet);

            this.CalculateRententionRate(sheet);

            string endColumn = ExcelUtilies.ExcelColumnIndexToName(ExcelUtilies.ExcelColumnNameToIndex(FunnelMonthlyReportParameters.MonthEnd));

            string startColumn = this.GetCurrentMonthExcelColumn();
            if (ExcelUtilies.ExcelColumnNameToIndex(startColumn) <= ExcelUtilies.ExcelColumnNameToIndex(endColumn))
                ExcelUtilies.DeleteColumns(sheet, startColumn, endColumn);

            //for Europe remove the values of Spot Rate and KFR Rate
            if (sheet.Name == "Europe" || sheet.Name == "Mexico+US")
            {
                int endMonthColumn = ExcelUtilies.ExcelColumnNameToIndex(this.GetCurrentMonthExcelColumn());
                for (int i = 2; i < endMonthColumn; i++)
                {
                    //Spot Rate
                    sheet.Cells[4, i] = string.Empty;
                    //KFR Rate
                    sheet.Cells[5, i] = string.Empty;
                }
            }

            this.FormatCells(sheet);
            this.FreezePanes(sheet);
        }

        private void FreezePanes(Excel.Worksheet worksheet)
        {
            worksheet.Activate();
            worksheet.Application.ActiveWindow.SplitColumn = 3;
            worksheet.Application.ActiveWindow.FreezePanes = true;

        }

        private void SetBackgroundColor(Excel.Worksheet sheet)
        {
            string rowBegin = "3";
            string rowEnd = (FunnelMonthlyReportParameters.RowCount + 2).ToString();

            //set title to gray
            sheet.Range["A" + rowBegin, "A" + rowEnd].Interior.Color = Color.LightGray;

            Color color = Color.FromArgb(253, 253, 217);
            //set month 
            sheet.Range[FunnelMonthlyReportParameters.MonthBegin + rowBegin, FunnelMonthlyReportParameters.MonthEnd + rowEnd].Interior.ColorIndex = -1;// Color.CornflowerBlue;// Color.LightSteelBlue;
            sheet.Range[FunnelMonthlyReportParameters.MonthBegin + rowBegin, FunnelMonthlyReportParameters.MonthEnd + rowEnd].Interior.Color = color;// Color.CornflowerBlue;// Color.LightSteelBlue;

            //set september
            color = Color.FromArgb(220, 230, 241);
            sheet.Range[FunnelMonthlyReportParameters.PreviousMonthBegin + rowBegin, FunnelMonthlyReportParameters.PreviousMonthEnd + rowEnd].Interior.ColorIndex = -1;// Color.LightSkyBlue;
            sheet.Range[FunnelMonthlyReportParameters.PreviousMonthBegin + rowBegin, FunnelMonthlyReportParameters.PreviousMonthEnd + rowEnd].Interior.Color = color;// Color.LightSkyBlue;

            //set YTD to white
            sheet.Range[FunnelMonthlyReportParameters.YTDFunnel + rowBegin, FunnelMonthlyReportParameters.VS1011YTD + rowEnd].Interior.Color = Color.LightGray;

        }

        private void FormatCells(Excel.Worksheet sheet)
        {
            //Line name - Example
            //Visits(in '000)
            sheet.Range["A7"].EntireRow.NumberFormat = "#,";

            string rateFormat = "#,##0%";
            //Pay rate % - 40%
            sheet.Range["A11"].EntireRow.NumberFormat = rateFormat;
            //Appt Rate% - 45%
            sheet.Range["A18"].EntireRow.NumberFormat = rateFormat;
            //ShowUp% - 80%
            sheet.Range["A20"].EntireRow.NumberFormat = rateFormat;
            //Retention Tate% - 40%
            sheet.Range["A15"].EntireRow.NumberFormat = rateFormat;
            //Close rate% - 40%
            sheet.Range["A24"].EntireRow.NumberFormat = rateFormat;
            //Direct Costs% - 15%
            sheet.Range["A41"].EntireRow.NumberFormat = rateFormat;
            //Total Marketin Costs% - 14%
            sheet.Range["A43"].EntireRow.NumberFormat = rateFormat;
            //Sales Costs/Net Telesales revenue% - 24%
            sheet.Range["A46"].EntireRow.NumberFormat = rateFormat;
            //Sales Costs/Total Net revenue% - 21%
            sheet.Range["A47"].EntireRow.NumberFormat = rateFormat;
            //Cancellation Provision% - 14.0%
            sheet.Range["A54"].EntireRow.NumberFormat = rateFormat;
            //Bad debt Provision% - 7.3%
            sheet.Range["A56"].EntireRow.NumberFormat = rateFormat;

            //insert two columns before the first column
            sheet.Range["A1"].EntireColumn.Insert();
            sheet.Range["A1"].EntireColumn.Interior.ColorIndex = -1;
            sheet.Range["A1"].EntireColumn.ColumnWidth = 20;
            sheet.Range["A1"].EntireColumn.Insert();
            sheet.Range["A1"].EntireColumn.Interior.ColorIndex = -1;
            sheet.Range["A1"].EntireColumn.ColumnWidth = 15;

            //# of CR - Other Costs
            this.InsertTitleRow(sheet, "Other Costs", 48);                

            //# of Sales Staff - Salse Costs
            this.InsertTitleRow(sheet, "Sales Costs", 44);            

            //Total Marketing Costs - Martketing Costs
            this.InsertTitleRow(sheet, "Martketing Costs", 42);                

            this.InsertBlankRow(sheet, "A41");

            //Revenue(KFR rate)
            this.InsertTitleRow(sheet, "Revenue(KFR rate)", 38);

            //Revenue(Spot rate)
            this.InsertTitleRow(sheet, "Revenue(Spot rate)", 25);

            //Funnel KPI
            ExcelUtilies.DeleteRow(sheet, "A6");
            this.InsertTitleRow(sheet, "Funnel KPI", 6);

            //Set sheet title
            sheet.Range["A7"].Value = sheet.Name;
            sheet.Range["A7"].Font.Bold = true;            

            //Copy header above on Funnuel KPI
            sheet.Range["A3"].EntireRow.Cut();
            sheet.Range["A7"].Insert(Excel.XlInsertShiftDirection.xlShiftDown);

            //Delete the title line added before
            ExcelUtilies.DeleteRow(sheet, "A2");

            sheet.Range["A2"].EntireRow.Interior.ColorIndex = -1;
            sheet.Range["A3"].EntireRow.Interior.ColorIndex = -1;

            sheet.Range["A4"].EntireRow.RowHeight = sheet.Range["A5"].EntireRow.RowHeight * 2;
            sheet.Range["A5"].EntireRow.RowHeight = sheet.Range["A5"].EntireRow.RowHeight * 2;
            sheet.Range["A5"].Value = "Country";
            sheet.Range["A5"].Font.Bold = true;

            sheet.Range["C5"].Value = "Funnel performance";
            sheet.Range["C5"].Font.Bold = true;
            sheet.Range["C5"].Interior.ColorIndex = -1;
            sheet.Range["C5"].EntireColumn.ColumnWidth = 25;

            //Add Border line
            sheet.Range["B5"].EntireRow.Borders.get_Item(Excel.XlBordersIndex.xlEdgeTop).Weight = 2;
            sheet.Range["B5"].EntireRow.Borders.get_Item(Excel.XlBordersIndex.xlEdgeTop).LineStyle = Excel.XlLineStyle.xlContinuous;

            sheet.Range["B5"].EntireColumn.Borders.get_Item(Excel.XlBordersIndex.xlEdgeRight).Weight = 2;
            sheet.Range["B5"].EntireColumn.Borders.get_Item(Excel.XlBordersIndex.xlEdgeRight).LineStyle = Excel.XlLineStyle.xlContinuous;

        }

        private void InsertTitleRow(Excel.Worksheet sheet, string title, int rowNumber)
        {
            sheet.Range["B" + rowNumber].Value = title;
            sheet.Range["B" + rowNumber].Font.Bold = true;
            this.InsertBlankRow(sheet, "A" + rowNumber);    
        }

        private void InsertBlankRow(Excel.Worksheet sheet, string cell)
        {
            ExcelUtilies.InsertRow(sheet, cell);
            sheet.Range[cell].EntireRow.Interior.ColorIndex = -1;
            sheet.Range[cell].EntireRow.RowHeight = sheet.Range[cell].EntireRow.RowHeight / 2;
        }

        private string GetCurrentMonthExcelColumn()
        {
            return FunnelReportHelper.GetFunnelMonthReportMonthColumnMapping()[DateTime.Now.Month];
        }

        private void CloseSourceReportsExcel()
        {
            FunnelReportHelper.CloseWorkingWorkbook(monthBook);
        }

        private void CalculateRententionRate(Excel.Worksheet sheet)
        {
            sheet.Cells[FunnelMonthlyReportParameters.RententionRateRow, 2].NumberFormat = "##,##0.00%";
            sheet.Cells[FunnelMonthlyReportParameters.FTPPrice, 2] = string.Format("={0}{1}/{0}{2}",
                    "B", FunnelMonthlyReportParameters.OnlineFTPRevenues, FunnelMonthlyReportParameters.ChargedFTPRow);

            for (int i = 14; i <= 25; i++)
            {
                
                string column = ExcelUtilies.ExcelColumnIndexToName(i);
                string columnBefore = ExcelUtilies.ExcelColumnIndexToName(i - 1);

                //Retention Rate
                sheet.Cells[FunnelMonthlyReportParameters.RententionRateRow, i] = string.Format("={0}{1}/({2}{1} + {2}{3})",
                    column, FunnelMonthlyReportParameters.RenewalRow, columnBefore, FunnelMonthlyReportParameters.ChargedFTPRow);
                sheet.Cells[FunnelMonthlyReportParameters.RententionRateRow, i].NumberFormat = "##,##0.00%";

                //FTP Price
                sheet.Cells[FunnelMonthlyReportParameters.FTPPrice, i] = string.Format("={0}{1}/{0}{2}",
                    column, FunnelMonthlyReportParameters.OnlineFTPRevenues, FunnelMonthlyReportParameters.ChargedFTPRow);
            }
        }
       
        private void AppendReport(Excel.Workbook destWorkbook, Excel.Worksheet copyDestSheet, string sheetName, string column, string columnHeader,  string reportFile, int sequence)
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

                    //copy week report to dest
                    if (copySourceSheet.Cells[1, 1].Text == "Spot Rate")
                        ExcelUtilies.CopyRange(copySourceSheet, "B1", "B" + FunnelMonthlyReportParameters.RowCount, copyDestSheet, column + "2");
                    else
                        ExcelUtilies.CopyRange(copySourceSheet, "B1", "B" + FunnelMonthlyReportParameters.RowCount, copyDestSheet, column + "1");
                }
                catch (Exception ex)
                {
                    this.Logger.Error(string.Format("Append {0} report failed! \n {1}",sheetName,ex.StackTrace));
                    throw;
                }
                finally
                {
                    FunnelReportHelper.CloseWorkingWorkbook(sourceBook);
                }
            }

            copyDestSheet.Cells[1, column] = columnHeader;
            copyDestSheet.Cells[1, column].Font.Bold = true;
            copyDestSheet.Cells[1, column].Font.Size = 10;
            copyDestSheet.Columns[column].ColumnWidth = 15;
        }
    }
}
