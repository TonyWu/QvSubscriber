using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace QlikView.Common
{
    class FunnelMonthlyOutputHelper
    {
        Excel.Workbook thisMonthBook = null;
        //Excel.Workbook outputmonthBook = null;
        //Excel.Workbook monthDiffBook = null;
        //Excel.Workbook actualsBook = null;
        //Excel.Workbook KFR1Book = null;
        //Excel.Workbook diffBook = null;
        //Excel.Workbook priorYearBook = null;
        //Excel.Workbook priorYearDiffBook = null;

        public ILog Logger { get; set; }

        private const string OutputJPKR = "Output JP+KR";
        private const string OutputTotal = "Output Total";
        private const string OutputBrazil = "Output Brazil";
        private const string OutputEurope = "Output Europe";
        private const string OutputMXUS = "Output MX+US+ROLA";
        private const string OutputOthers = "Output Others";

        private List<string> outputList = new List<string>()
        {
            OutputTotal,
            OutputBrazil,
            OutputEurope,
            OutputMXUS,
            OutputJPKR,
            OutputOthers
        };

        /// <summary>
        /// Just For Unit Test
        /// </summary>
        /// <param name="MergedFiles"></param>
        /// <param name="outputFile"></param>
        public void MergedOutputFilesUnitTest(Dictionary<string, ReportContext> MergedFiles, string outputFile)
        {
            if (this.Logger == null)
                this.Logger = new QVConfigLog();

            Excel.Application excel = new Excel.Application();
            Excel.Workbook workbook = this.MergedOutputFiles(excel, MergedFiles);

            workbook.SaveAs(outputFile);
            workbook.Close();
            excel.Application.Quit();
        }

        public void MergedOutputFiles(Excel.Worksheet sheetDest, Dictionary<string, ReportContext> MergedFiles)
        {
            Excel.Workbook outputTempBook = null;

            this.Logger.Message("Begin to append output files.");
            try
            {
                outputTempBook = this.MergedOutputFiles(sheetDest.Application, MergedFiles);

                if (outputTempBook != null)
                {
                    foreach (var item in outputTempBook.Worksheets)
                    {
                        Excel.Worksheet sheet = item as Excel.Worksheet;
                        if (outputList.Contains(sheet.Name))
                        {
                            sheet.Copy(Missing.Value, sheetDest);
                        }
                    }
                }
                else
                {
                    this.Logger.Error("output temp work is null, append reports failed.");
                }
            }
            catch (Exception ex)
            {
                this.Logger.Error("Append Output reports failed. \n " + ex.StackTrace);
                throw;
            }
            finally
            {
                FunnelReportHelper.SaveTempWorkbook(outputTempBook);
            }

            this.Logger.Message("Complete to append output files.");
        }

        private Excel.Workbook MergedOutputFiles(Excel.Application excel, Dictionary<string, ReportContext> MergedFiles)
        {
            Dictionary<string, string> thisMonthReports = FunnelReportHelper.GetOutputMergedFilesByCategory(MergedFiles, "ThisMonth");
            Dictionary<string, string> monthReports = FunnelReportHelper.GetOutputMergedFilesByCategory(MergedFiles, "1011Month");
            Dictionary<string, string> monthDiffReports = FunnelReportHelper.GetOutputMergedFilesByCategory(MergedFiles, "MonthDiff");

            Dictionary<string, string> actualReports = FunnelReportHelper.GetOutputMergedFilesByCategory(MergedFiles, "Actuals");
            Dictionary<string, string> KFRxReports = FunnelReportHelper.GetOutputMergedFilesByCategory(MergedFiles, "KFRx");
            Dictionary<string, string> DiffReports = FunnelReportHelper.GetOutputMergedFilesByCategory(MergedFiles, "Diff");
            Dictionary<string, string> PriorYearReports = FunnelReportHelper.GetOutputMergedFilesByCategory(MergedFiles, "PriorYear");
            Dictionary<string, string> PriorYearDiffReport = FunnelReportHelper.GetOutputMergedFilesByCategory(MergedFiles, "PriorYearDiff");

            if (thisMonthReports.Count == 0)
                return null;
            Excel.Workbook tempWorkbook = null;

            try
            {
                this.Logger.Message("Create tempWorkbook.");
                tempWorkbook = excel.Workbooks.Add(Missing.Value);
                //this.AddCategoryOutputSheet(tempWorkbook);

                this.Logger.Message("Create Output Sheets.");
                Excel.Worksheet outputTotalSheet = tempWorkbook.Sheets.Add();
                outputTotalSheet.Name = OutputTotal;
                Excel.Worksheet outputJapanSheet = tempWorkbook.Sheets.Add();
                outputJapanSheet.Name = OutputJPKR;
                Excel.Worksheet outputBrazilSheet = tempWorkbook.Sheets.Add();
                outputBrazilSheet.Name = OutputBrazil;
                Excel.Worksheet outputEuropeSheet = tempWorkbook.Sheets.Add();
                outputEuropeSheet.Name = OutputEurope;
                Excel.Worksheet outputMXUSSheet = tempWorkbook.Sheets.Add();
                outputMXUSSheet.Name = OutputMXUS;
                Excel.Worksheet outputOthersSheet = tempWorkbook.Sheets.Add();
                outputOthersSheet.Name = OutputOthers;

                //create a new work sheet
                Excel.Worksheet tempWorkSheet = tempWorkbook.Worksheets[1] as Excel.Worksheet;


                this.Logger.Message("Copy to tempwork.");

                foreach (var item in actualReports.Keys)
                {
                    thisMonthBook = excel.Workbooks.Open(thisMonthReports[item]);
                    Excel.Worksheet thisMonthSheet = thisMonthBook.Worksheets[1];
                    thisMonthSheet.Name = item;

                    //copy month report to dest
                    //Column B - M
                    thisMonthSheet.Copy(Missing.Value, tempWorkSheet);
                    Excel.Worksheet copyDestSheet = tempWorkbook.Worksheets[item];

                    if (string.IsNullOrWhiteSpace(copyDestSheet.Cells[1, 1].Text))
                        ExcelUtilies.DeleteRow(copyDestSheet, "A1");

                    //month
                    if (monthReports.ContainsKey(item))
                    {
                        this.AppendReport(tempWorkbook, copyDestSheet, item, FunnelMonthlyReportParameters.OutputMonth, monthReports[item], 1);
                    }
                    //monthDiff
                    if (monthDiffReports.ContainsKey(item))
                    {
                        this.AppendReport(tempWorkbook, copyDestSheet, item, FunnelMonthlyReportParameters.OutputMonthDiff, monthDiffReports[item], 2);
                    }
                    //Actuals
                    if (actualReports.ContainsKey(item))
                    {
                        this.AppendReport(tempWorkbook, copyDestSheet, item, FunnelMonthlyReportParameters.OutputActuals, actualReports[item], 3);
                    }
                    //KFR1
                    if (KFRxReports.ContainsKey(item))
                    {
                        this.AppendReport(tempWorkbook, copyDestSheet, item, FunnelMonthlyReportParameters.OutputKFR1, KFRxReports[item], 4);
                    }
                    //Diff
                    if (DiffReports.ContainsKey(item))
                    {
                        this.AppendReport(tempWorkbook, copyDestSheet, item, FunnelMonthlyReportParameters.OutputDiff, DiffReports[item], 5);
                    }
                    //PriorYear
                    if (PriorYearReports.ContainsKey(item))
                    {
                        this.AppendReport(tempWorkbook, copyDestSheet, item, FunnelMonthlyReportParameters.OutputPriorYear, PriorYearReports[item], 6);
                    }
                    //PriorYearDiff
                    if (PriorYearDiffReport.ContainsKey(item))
                    {
                        this.AppendReport(tempWorkbook, copyDestSheet, item, FunnelMonthlyReportParameters.OutputPriorYearDiff, PriorYearDiffReport[item], 7);
                    }

                    if (!string.IsNullOrWhiteSpace(copyDestSheet.Cells[1, 1].Text))
                    {
                        ExcelUtilies.InsertRow(copyDestSheet, "A1");
                    }

                    this.SetHeaderTitle(copyDestSheet);

                    this.CloseOutputSourceReportsExcel();

                    #region Category output
                    Logger.Message("Category output " + item);
                    this.FormatOutputCells(copyDestSheet);
                    copyDestSheet.Activate();
                    if (item == "Japan")
                    {
                        ExcelUtilies.CopyRange(copyDestSheet, "A1", "I13", outputJapanSheet, "A3");
                        outputJapanSheet.Cells[2, 1] = item;
                    }
                    else if (item == "Korea")
                    {
                        ExcelUtilies.CopyRange(copyDestSheet, "A1", "I13", outputJapanSheet, "A21");
                        outputJapanSheet.Cells[20, 1] = item;
                    }
                    else if (item == "Total")
                    {
                        ExcelUtilies.CopyRange(copyDestSheet, "A1", "I13", outputTotalSheet, "A3");
                        outputTotalSheet.Cells[2, 1] = item;
                    }
                    else if (item == "Brazil")
                    {
                        ExcelUtilies.CopyRange(copyDestSheet, "A1", "I13", outputBrazilSheet, "A3");
                        outputBrazilSheet.Cells[2, 1] = item;
                    }
                    else if (item == "Europe")
                    {
                        ExcelUtilies.CopyRange(copyDestSheet, "A1", "I13", outputEuropeSheet, "A3");
                        outputEuropeSheet.Cells[2, 1] = item;
                    }
                    else if (item == "Spain")
                    {
                        ExcelUtilies.CopyRange(copyDestSheet, "A1", "I13", outputEuropeSheet, "A19");
                        outputEuropeSheet.Cells[18, 1] = item;
                    }
                    else if (item == "MEAST")
                    {
                        ExcelUtilies.CopyRange(copyDestSheet, "A1", "I13", outputEuropeSheet, "A37");
                        outputEuropeSheet.Cells[36, 1] = item;
                    }
                    else if (item == "ROE")
                    {
                        ExcelUtilies.CopyRange(copyDestSheet, "A1", "I13", outputEuropeSheet, "A55");
                        outputEuropeSheet.Cells[54, 1] = item;
                    }
                    else if (item == "Italy")
                    {
                        ExcelUtilies.CopyRange(copyDestSheet, "A1", "I13", outputEuropeSheet, "A73");
                        outputEuropeSheet.Cells[72, 1] = item;
                    }
                    else if (item == "Germany")
                    {
                        ExcelUtilies.CopyRange(copyDestSheet, "A1", "I13", outputEuropeSheet, "A91");
                        outputEuropeSheet.Cells[90, 1] = item;
                    }
                    else if (item == "France")
                    {
                        ExcelUtilies.CopyRange(copyDestSheet, "A1", "I13", outputEuropeSheet, "A109");
                        outputEuropeSheet.Cells[108, 1] = item;
                    }
                    else if (item == "Mexico+US+ROLA")
                    {
                        ExcelUtilies.CopyRange(copyDestSheet, "A1", "I13", outputMXUSSheet, "A3");
                        outputMXUSSheet.Cells[2, 1] = item;
                    }
                    else if (item == "US")
                    {
                        ExcelUtilies.CopyRange(copyDestSheet, "A1", "I13", outputMXUSSheet, "A19");
                        outputMXUSSheet.Cells[18, 1] = item;
                    }
                    else if (item == "Mexico")
                    {
                        ExcelUtilies.CopyRange(copyDestSheet, "A1", "I13", outputMXUSSheet, "A37");
                        outputMXUSSheet.Cells[36, 1] = item;
                    }
                    else if (item == "ROLA")
                    {
                        ExcelUtilies.CopyRange(copyDestSheet, "A1", "I13", outputMXUSSheet, "A55");
                        outputMXUSSheet.Cells[54, 1] = item;
                    }
                    else if (item == "ROA")
                    {
                        ExcelUtilies.CopyRange(copyDestSheet, "A1", "I13", outputOthersSheet, "A3");
                        outputOthersSheet.Cells[2, 1] = item;
                    }                  
                    else if (item == "ROW")
                    {
                        ExcelUtilies.CopyRange(copyDestSheet, "A1", "I13", outputOthersSheet, "A19");
                        outputOthersSheet.Cells[18, 1] = item;
                    }
                    //else if (item == "Thailand")
                    //{
                    //    ExcelUtilies.CopyRange(copyDestSheet, "A1", "I13", outputOthersSheet, "A37");
                    //    outputOthersSheet.Cells[36, 1] = item;
                    //}

                    #endregion

                }

                this.FormatOutputColumnWidth(outputJapanSheet);
                this.FormatOutputColumnWidth(outputTotalSheet);
                this.FormatOutputColumnWidth(outputMXUSSheet);
                this.FormatOutputColumnWidth(outputOthersSheet);
                this.FormatOutputColumnWidth(outputBrazilSheet);
                this.FormatOutputColumnWidth(outputEuropeSheet);
            }
            catch (Exception ex)
            {
                this.CloseOutputSourceReportsExcel();
                throw;
            }

            return tempWorkbook;
        }

        private void AppendReport(Excel.Workbook destWorkbook, Excel.Worksheet copyDestSheetBefore, string sheetName, string column, string reportFile, int sequence)
        {
            if (File.Exists(reportFile))
            {
                Excel.Workbook sourceBook = null;

                try
                {
                    sourceBook = destWorkbook.Application.Workbooks.Open(reportFile);
                    Excel.Worksheet workingSheet = sourceBook.Worksheets[1];
                    workingSheet.Name = sheetName + sequence;

                    Excel.Worksheet tempWorkSheet = destWorkbook.Sheets[1] as Excel.Worksheet;
                    workingSheet.Copy(Missing.Value, tempWorkSheet);
                    Excel.Worksheet copySourceSheet = destWorkbook.Worksheets[sheetName + sequence];

                    //copy week report to dest
                    if (string.IsNullOrWhiteSpace(copySourceSheet.Cells[1, 1].Text))
                        ExcelUtilies.CopyRange(copySourceSheet, "B2", "B" + FunnelMonthlyReportParameters.OutputRowCount, copyDestSheetBefore, column + "1");
                    else
                        ExcelUtilies.CopyRange(copySourceSheet, "B1", "B" + FunnelMonthlyReportParameters.OutputRowCount, copyDestSheetBefore, column + "1");
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

        private void SetHeaderTitle(Excel.Worksheet copyDestSheet)
        {
            copyDestSheet.Cells[1, FunnelMonthlyReportParameters.OutputThisMonth] = FunnelMonthlyReportParameters.Header_OutputThisMonth;// "This Month";
            copyDestSheet.Cells[1, FunnelMonthlyReportParameters.OutputThisMonth].Font.Bold = true;
            copyDestSheet.Cells[1, FunnelMonthlyReportParameters.OutputThisMonth].Font.Size = 10;
            copyDestSheet.Columns[FunnelMonthlyReportParameters.OutputThisMonth].ColumnWidth = 15;

            copyDestSheet.Cells[1, FunnelMonthlyReportParameters.OutputMonth] = FunnelMonthlyReportParameters.Header_OutputMonth;// "10/11 Month";
            copyDestSheet.Cells[1, FunnelMonthlyReportParameters.OutputMonth].Font.Bold = true;
            copyDestSheet.Cells[1, FunnelMonthlyReportParameters.OutputMonth].Font.Size = 10;
            copyDestSheet.Columns[FunnelMonthlyReportParameters.OutputMonth].ColumnWidth = 15;

            copyDestSheet.Cells[1, FunnelMonthlyReportParameters.OutputMonthDiff] = FunnelMonthlyReportParameters.Header_OutputMonthDiff;// "Month Diff";
            copyDestSheet.Cells[1, FunnelMonthlyReportParameters.OutputMonthDiff].Font.Bold = true;
            copyDestSheet.Cells[1, FunnelMonthlyReportParameters.OutputMonthDiff].Font.Size = 10;
            copyDestSheet.Columns[FunnelMonthlyReportParameters.OutputMonthDiff].ColumnWidth = 15;

            copyDestSheet.Cells[1, FunnelMonthlyReportParameters.OutputActuals] = FunnelMonthlyReportParameters.Header_OutputActuals;// "Actuals";
            copyDestSheet.Cells[1, FunnelMonthlyReportParameters.OutputActuals].Font.Bold = true;
            copyDestSheet.Cells[1, FunnelMonthlyReportParameters.OutputActuals].Font.Size = 10;
            copyDestSheet.Columns[FunnelMonthlyReportParameters.OutputActuals].ColumnWidth = 15;

            copyDestSheet.Cells[1, FunnelMonthlyReportParameters.OutputKFR1] = FunnelMonthlyReportParameters.Header_OutputKFR;// "KFR2";
            copyDestSheet.Cells[1, FunnelMonthlyReportParameters.OutputKFR1].Font.Bold = true;
            copyDestSheet.Cells[1, FunnelMonthlyReportParameters.OutputKFR1].Font.Size = 10;
            copyDestSheet.Columns[FunnelMonthlyReportParameters.OutputKFR1].ColumnWidth = 15;

            copyDestSheet.Cells[1, FunnelMonthlyReportParameters.OutputDiff] = FunnelMonthlyReportParameters.Header_OutputDiff;// "Diff";
            copyDestSheet.Cells[1, FunnelMonthlyReportParameters.OutputDiff].Font.Bold = true;
            copyDestSheet.Cells[1, FunnelMonthlyReportParameters.OutputDiff].Font.Size = 10;
            copyDestSheet.Columns[FunnelMonthlyReportParameters.OutputDiff].ColumnWidth = 15;

            copyDestSheet.Cells[1, FunnelMonthlyReportParameters.OutputPriorYear] = FunnelMonthlyReportParameters.Header_OutputPriorYear;// "Prior Year";
            copyDestSheet.Cells[1, FunnelMonthlyReportParameters.OutputPriorYear].Font.Bold = true;
            copyDestSheet.Cells[1, FunnelMonthlyReportParameters.OutputPriorYear].Font.Size = 10;
            copyDestSheet.Columns[FunnelMonthlyReportParameters.OutputPriorYear].ColumnWidth = 15;

            copyDestSheet.Cells[1, FunnelMonthlyReportParameters.OutputPriorYearDiff] = FunnelMonthlyReportParameters.Header_OutputPriorYearDiff;// "Diff";
            copyDestSheet.Cells[1, FunnelMonthlyReportParameters.OutputPriorYearDiff].Font.Bold = true;
            copyDestSheet.Cells[1, FunnelMonthlyReportParameters.OutputPriorYearDiff].Font.Size = 10;
            copyDestSheet.Columns[FunnelMonthlyReportParameters.OutputPriorYearDiff].ColumnWidth = 15;
        }

        private void FormatOutputColumnWidth(Excel.Worksheet sheet)
        {
            sheet.Range["A1"].EntireColumn.ColumnWidth = 32;
            sheet.Range["B1"].EntireColumn.ColumnWidth = 15;
            sheet.Range["C1"].EntireColumn.ColumnWidth = 15;
            sheet.Range["D1"].EntireColumn.ColumnWidth = 15;
            sheet.Range["E1"].EntireColumn.ColumnWidth = 15;
            sheet.Range["F1"].EntireColumn.ColumnWidth = 15;
            sheet.Range["G1"].EntireColumn.ColumnWidth = 15;
            sheet.Range["H1"].EntireColumn.ColumnWidth = 15;
            sheet.Range["I1"].EntireColumn.ColumnWidth = 15;
        }

        private void FormatOutputCells(Excel.Worksheet sheet)
        {
            //TODO:
            sheet.Range["A1", "I13"].Interior.ColorIndex = -1;
        }

        private void CloseOutputSourceReportsExcel()
        {
            FunnelReportHelper.CloseWorkingWorkbook(thisMonthBook);
        }
    }
}
