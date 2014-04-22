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
    public class FunnelWeeklyMerge : IExcelMerge
    {
        public ILog Logger { get; set; }

        Dictionary<string, string> CountryCurrencies = new Dictionary<string, string>();
        Dictionary<int, string> matrixDefinations = null;
        Dictionary<int, string> chinaMatrixDefinations = null;

        public Dictionary<int, string> MatrixDefinations
        {
            get
            {
                return this.GetMatrixDefinations();
            }
        }

        public Dictionary<int, string> ChinaMatrixDefinations
        {
            get
            {
                return this.GetChinaMatrixDefinations();
            }
        }

        Excel.Application excel;

        Excel.Workbook monthBook = null;
        Excel.Workbook weekBook = null;
        Excel.Workbook weekBook1 = null;
        Excel.Workbook YTDFunnelBook = null;

        Excel.Workbook vs1011YTDBook = null;
        Excel.Workbook vs1011WeekBook = null;
        Excel.Workbook YTD1011Book = null;
        Excel.Workbook Week1011Book = null;

        Excel.Workbook YTDKFR3Book = null;
        Excel.Workbook YTDACTVsKFR3Book = null;
        Excel.Workbook YTDKFR2Book = null;
        Excel.Workbook YTDACTVsKFR2Book = null;
        Excel.Workbook YTDKFR1Book = null;
        Excel.Workbook YTDACTVsKFR1Book = null;
        Excel.Workbook YTDKFR0Book = null;
        Excel.Workbook YTDACTVsKFR0Book = null;

        public FunnelWeeklyMerge()
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
            Dictionary<string, string> weeklyReports = FunnelReportHelper.GetMergedFilesByCategory(MergedFiles, "ByWeek");
            Dictionary<string, string> weeklyReports1 = FunnelReportHelper.GetMergedFilesByCategory(MergedFiles, "ByWeekEx");
            Dictionary<string, string> YTDFunnelReports = FunnelReportHelper.GetMergedFilesByCategory(MergedFiles, "YTDFunnel");

            Dictionary<string, string> vs1011WeekReport = FunnelReportHelper.GetMergedFilesByCategory(MergedFiles, "VS1011Week");
            Dictionary<string, string> vs1011YTDReport = FunnelReportHelper.GetMergedFilesByCategory(MergedFiles, "VS1011YTD");
            Dictionary<string, string> Week1011Report = FunnelReportHelper.GetMergedFilesByCategory(MergedFiles, "1011Week");
            Dictionary<string, string> YTD1011Report = FunnelReportHelper.GetMergedFilesByCategory(MergedFiles, "1011YTD");

            Dictionary<string, string> YTDKFR3Reports = FunnelReportHelper.GetMergedFilesByCategory(MergedFiles, "YTDKFR3");
            Dictionary<string, string> YTDACTVsKFR3Reports = FunnelReportHelper.GetMergedFilesByCategory(MergedFiles, "YTDACTVsKFR3");
            Dictionary<string, string> YTDKFR2Reports = FunnelReportHelper.GetMergedFilesByCategory(MergedFiles, "YTDKFR2");
            Dictionary<string, string> YTDACTVsKFR2Reports = FunnelReportHelper.GetMergedFilesByCategory(MergedFiles, "YTDACTVsKFR2");
            Dictionary<string, string> YTDKFR1Reports = FunnelReportHelper.GetMergedFilesByCategory(MergedFiles, "YTDKFR1");
            Dictionary<string, string> YTDACTVsKFR1Reports = FunnelReportHelper.GetMergedFilesByCategory(MergedFiles, "YTDACTVsKFR1");
            Dictionary<string, string> YTDKFR0Reports = FunnelReportHelper.GetMergedFilesByCategory(MergedFiles, "YTDKFR0");
            Dictionary<string, string> YTDACTVsKFR0Reports = FunnelReportHelper.GetMergedFilesByCategory(MergedFiles, "YTDACTVsKFR0");

            string CNTSMiniReport = string.Empty;
            if (MergedFiles.ContainsKey("Funnel_CNTSMini_OnlineFTPRevenues"))
            {
                CNTSMiniReport = MergedFiles["Funnel_CNTSMini_OnlineFTPRevenues"].OutputFullName;
            }

            Excel.Workbook bookDest = null;
            Excel.Workbook tempWorkbook = null;

            try
            {
                tempWorkbook = excel.Workbooks.Add(Missing.Value);

                //create a new work sheet
                Excel.Worksheet tempWorkSheet = tempWorkbook.Worksheets[1] as Excel.Worksheet;

                this.Logger.Message("Copy to tempwork.");

                foreach (var item in weeklyReports.Keys)
                {
                    monthBook = excel.Workbooks.Open(monthlyReports[item]);
                    Excel.Worksheet monthSheet = monthBook.Worksheets[1];
                    monthSheet.Name = item;

                    //copy month report to dest
                    //Column B - M
                    this.Logger.Message("copy month report to dest");
                    monthSheet.Copy(Missing.Value, tempWorkSheet);
                    Excel.Worksheet copyDestSheet = tempWorkbook.Worksheets[item];

                    //weekBook = excel.Workbooks.Open(weeklyReports[item]);
                    //Excel.Worksheet weeklySheet = weekBook.Worksheets[1];
                    //weeklySheet.Name = item;

                    ////copy Week report to dest
                    ////Column B - M
                    //this.Logger.Message("copy month report to dest");
                    //weeklySheet.Copy(Missing.Value, tempWorkSheet);
                    //Excel.Worksheet copyDestSheet = tempWorkbook.Worksheets[item];

                    int sourceRowCount = FunnelWeeklyReportParameters.SourceRowCount;
                    int columnCount = 0;

                    #region Append Reports
                    //append weekly reports
                    //Column N - BN
                    this.Logger.Message("append weekly reports");
                    if (weeklyReports.ContainsKey(item))
                    {
                        weekBook = excel.Workbooks.Open(weeklyReports[item]);
                        Excel.Worksheet weekSheet = weekBook.Worksheets[1];
                        weekSheet.Name = item + columnCount;

                        weekSheet.Copy(Missing.Value, tempWorkSheet);
                        Excel.Worksheet copySourceSheet = tempWorkbook.Worksheets[item + columnCount];

                        string endColumn = ExcelUtilies.ExcelColumnIndexToName(ExcelUtilies.ExcelColumnNameToIndex("B") + ParameterConfig.GetIntValue("FunnelWeeklyReport", "WeekColumns") - 1);

                        this.Logger.Message(endColumn + sourceRowCount);
                        //copy week report to dest
                        ExcelUtilies.CopyRange(copySourceSheet, "B1", endColumn + sourceRowCount, copyDestSheet, FunnelWeeklyReportParameters.WeekBegin + "1");
                        this.Logger.Message("Week 1");
                    }

                    if (weeklyReports1.ContainsKey(item))
                    {
                        columnCount++;
                        weekBook1 = excel.Workbooks.Open(weeklyReports1[item]);
                        Excel.Worksheet weekSheet = weekBook1.Worksheets[1];
                        weekSheet.Name = item + columnCount;

                        weekSheet.Copy(Missing.Value, tempWorkSheet);
                        Excel.Worksheet copySourceSheet = tempWorkbook.Worksheets[item + columnCount];

                        string endColumn = ExcelUtilies.ExcelColumnIndexToName(ExcelUtilies.ExcelColumnNameToIndex("B") + ParameterConfig.GetIntValue("FunnelWeeklyReport", "WeekColumns") - 1);

                        this.Logger.Message(endColumn + sourceRowCount);
                        //copy week report to dest
                        ExcelUtilies.CopyRange(copySourceSheet, "B1", endColumn + sourceRowCount, copyDestSheet, "BO" + "1");
                        this.Logger.Message("Week 1");
                    }

                    int offset = 0;

                    #region 11/12 Week

                    if (Week1011Report.ContainsKey(item))
                    {
                        columnCount++;
                        offset++;

                        string columnName = ExcelUtilies.ExcelColumnIndexToName(ExcelUtilies.ExcelColumnNameToIndex(FunnelWeeklyReportParameters.WeekEnd) + offset);
                        
                        Week1011Book = null;
                        if (File.Exists(Week1011Report[item]))
                        {
                            Week1011Book = excel.Workbooks.Open(Week1011Report[item]);
                            Excel.Worksheet week1011Sheet = Week1011Book.Worksheets[1];
                            week1011Sheet.Name = item + columnCount;

                            week1011Sheet.Copy(Missing.Value, tempWorkSheet);
                            Excel.Worksheet copySourceSheet = tempWorkbook.Worksheets[item + columnCount];

                            //copy week report to dest
                            if (copySourceSheet.Cells[1, 1].Text == "Spot Rate")
                                ExcelUtilies.CopyRange(copySourceSheet, "B1", "B" + sourceRowCount, copyDestSheet, columnName + "2");
                            else
                                ExcelUtilies.CopyRange(copySourceSheet, "B1", "B" + sourceRowCount, copyDestSheet, columnName + "1");
                        }

                        copyDestSheet.Cells[1, columnName] = FunnelWeeklyReportParameters.Header_Week_BeforeFisicalYear;
                        copyDestSheet.Cells[1, columnName].Font.Size = 10;
                        copyDestSheet.Cells[1, columnName].Font.Bold = true;
                        copyDestSheet.Columns[columnName].ColumnWidth = 15;
                    }

                    #endregion

                    #region VS 11/12 Week

                    if (vs1011WeekReport.ContainsKey(item))
                    {
                        columnCount++;
                        offset++;

                        string columnName = ExcelUtilies.ExcelColumnIndexToName(ExcelUtilies.ExcelColumnNameToIndex(FunnelWeeklyReportParameters.WeekEnd) + offset);
                        vs1011WeekBook = null;
                        if (File.Exists(vs1011WeekReport[item]))
                        {
                            vs1011WeekBook = excel.Workbooks.Open(vs1011WeekReport[item]);
                            Excel.Worksheet weekSheet = vs1011WeekBook.Worksheets[1];
                            weekSheet.Name = item + columnCount;

                            weekSheet.Copy(Missing.Value, tempWorkSheet);
                            Excel.Worksheet copySourceSheet = tempWorkbook.Worksheets[item + columnCount];

                            //copy week report to dest
                            if (copySourceSheet.Cells[1, 1].Text == "Spot Rate")
                                ExcelUtilies.CopyRange(copySourceSheet, "B1", "B" + sourceRowCount, copyDestSheet, columnName + "2");
                            else
                                ExcelUtilies.CopyRange(copySourceSheet, "B1", "B" + sourceRowCount, copyDestSheet, columnName + "1");
                        }

                        copyDestSheet.Cells[1, columnName] = FunnelWeeklyReportParameters.Header_WeekVS_BeforeFisicalYear;// "VS 11/12 Week";
                        copyDestSheet.Cells[1, columnName].Font.Size = 10;
                        copyDestSheet.Cells[1, columnName].Font.Bold = true;
                        copyDestSheet.Columns[columnName].ColumnWidth = 15;
                    }

                    #endregion

                    #region append YTDFunnel report YTD ACT 12/13
                    //Column BO
                    this.Logger.Message("append YTDFunnel reports");
                    if (YTDFunnelReports.ContainsKey(item))
                    {
                        columnCount++;
                        offset++;
                        string columnName = ExcelUtilies.ExcelColumnIndexToName(ExcelUtilies.ExcelColumnNameToIndex(FunnelWeeklyReportParameters.WeekEnd) + offset);

                        YTDFunnelBook = null;

                        if (File.Exists(YTDFunnelReports[item]))
                        {
                            YTDFunnelBook = excel.Workbooks.Open(YTDFunnelReports[item]);
                            Excel.Worksheet weekSheet = YTDFunnelBook.Worksheets[1];
                            weekSheet.Name = item + columnCount;

                            weekSheet.Copy(Missing.Value, tempWorkSheet);
                            Excel.Worksheet copySourceSheet = tempWorkbook.Worksheets[item + columnCount];

                            //copy week report to dest
                            if (copySourceSheet.Cells[1, 1].Text == "Spot Rate")
                                ExcelUtilies.CopyRange(copySourceSheet, "B1", "B" + sourceRowCount, copyDestSheet, columnName + "2");
                            else
                                ExcelUtilies.CopyRange(copySourceSheet, "B1", "B" + sourceRowCount, copyDestSheet, columnName + "1");
                        }

                        copyDestSheet.Cells[1, columnName] = FunnelWeeklyReportParameters.Header_YTDACT_CurrentFisicalYear;// "YTD ACT 12/13";
                        copyDestSheet.Cells[1, columnName].Font.Bold = true;
                        copyDestSheet.Cells[1, columnName].Font.Size = 10;
                        copyDestSheet.Columns[columnName].ColumnWidth = 15;
                    }
                    #endregion

                    #region append YTD ACT 11/12
                    //Column BP
                    this.Logger.Message("append 1011YTD reports");
                    if (YTD1011Report.ContainsKey(item))
                    {
                        columnCount++;
                        offset++;
                        string columnName = ExcelUtilies.ExcelColumnIndexToName(ExcelUtilies.ExcelColumnNameToIndex(FunnelWeeklyReportParameters.WeekEnd) + offset);
                       
                        YTD1011Book = null;

                        if (File.Exists(YTD1011Report[item]))
                        {
                            YTD1011Book = excel.Workbooks.Open(YTD1011Report[item]);
                            Excel.Worksheet weekSheet = YTD1011Book.Worksheets[1];
                            weekSheet.Name = item + columnCount;

                            weekSheet.Copy(Missing.Value, tempWorkSheet);
                            Excel.Worksheet copySourceSheet = tempWorkbook.Worksheets[item + columnCount];

                            //copy week report to dest
                            if (copySourceSheet.Cells[1, 1].Text == "Spot Rate")
                                ExcelUtilies.CopyRange(copySourceSheet, "B1", "B" + sourceRowCount, copyDestSheet, columnName + "2");
                            else
                                ExcelUtilies.CopyRange(copySourceSheet, "B1", "B" + sourceRowCount, copyDestSheet, columnName + "1");
                        }

                        copyDestSheet.Cells[1, columnName] = FunnelWeeklyReportParameters.Header_YTDACT_BeforeFisicalYear;// "YTD ACT 11/12";
                        copyDestSheet.Cells[1, columnName].Font.Bold = true;
                        copyDestSheet.Cells[1, columnName].Font.Size = 10;
                        copyDestSheet.Columns[columnName].ColumnWidth = 15;
                    }

                    #endregion

                    #region append VS 10/11 YTD
                    //Column BQ
                    this.Logger.Message("append VS1011YTD reports");
                    if (vs1011YTDReport.ContainsKey(item))
                    {
                        columnCount++;
                        offset++;
                        string columnName = ExcelUtilies.ExcelColumnIndexToName(ExcelUtilies.ExcelColumnNameToIndex(FunnelWeeklyReportParameters.WeekEnd) + offset);

                        vs1011YTDBook = null;

                        if (File.Exists(vs1011YTDReport[item]))
                        {
                            vs1011YTDBook = excel.Workbooks.Open(vs1011YTDReport[item]);
                            Excel.Worksheet weekSheet = vs1011YTDBook.Worksheets[1];
                            weekSheet.Name = item + columnCount;

                            weekSheet.Copy(Missing.Value, tempWorkSheet);
                            Excel.Worksheet copySourceSheet = tempWorkbook.Worksheets[item + columnCount];

                            //copy week report to dest
                            if (copySourceSheet.Cells[1, 1].Text == "Spot Rate")
                                ExcelUtilies.CopyRange(copySourceSheet, "B1", "B" + sourceRowCount, copyDestSheet, columnName + "2");
                            else
                                ExcelUtilies.CopyRange(copySourceSheet, "B1", "B" + sourceRowCount, copyDestSheet, columnName + "1");

                        }

                        copyDestSheet.Cells[1, columnName] = FunnelWeeklyReportParameters.Header_YTDVS;// "VS 11/12 YTD";
                        copyDestSheet.Cells[1, columnName].Font.Bold = true;
                        copyDestSheet.Cells[1, columnName].Font.Size = 10;
                        copyDestSheet.Columns[columnName].ColumnWidth = 15;
                    }

                    #endregion

                    #region YTDKFR3
                    //append YTDKFR2
                    //Column BR
                    this.Logger.Message("append YTDKFR3 reports");
                    if (YTDKFR3Reports.ContainsKey(item))
                    {
                        columnCount++;
                        offset++;
                        string columnName = ExcelUtilies.ExcelColumnIndexToName(ExcelUtilies.ExcelColumnNameToIndex(FunnelWeeklyReportParameters.WeekEnd) + offset);

                        YTDKFR3Book = null;
                        //For the thailand, there will be empty. So no file is exported.
                        if (File.Exists(YTDKFR3Reports[item]))
                        {
                            YTDKFR3Book = excel.Workbooks.Open(YTDKFR3Reports[item]);
                            Excel.Worksheet weekSheet = YTDKFR3Book.Worksheets[1];
                            weekSheet.Name = item + columnCount;

                            weekSheet.Copy(Missing.Value, tempWorkSheet);
                            Excel.Worksheet copySourceSheet = tempWorkbook.Worksheets[item + columnCount];

                            //copy week report to dest
                            if (copySourceSheet.Cells[1, 1].Text == "Spot Rate")
                                ExcelUtilies.CopyRange(copySourceSheet, "B1", "B" + sourceRowCount, copyDestSheet, columnName + "2");
                            else
                                ExcelUtilies.CopyRange(copySourceSheet, "B1", "B" + sourceRowCount, copyDestSheet, columnName + "1");
                        }

                        copyDestSheet.Cells[1, columnName] = FunnelWeeklyReportParameters.Header_YTDKRF3;// "YTD KFR2 12/13";
                        copyDestSheet.Cells[1, columnName].Font.Bold = true;
                        copyDestSheet.Cells[1, columnName].Font.Size = 10;
                        copyDestSheet.Columns[columnName].ColumnWidth = 15;
                    }

                    //append YTDACTVsKFR1 report
                    //Column BS
                    this.Logger.Message("append YTDACTVsKFR3 reports");
                    if (YTDACTVsKFR3Reports.ContainsKey(item))
                    {
                        columnCount++;
                        offset++;
                        string columnName = ExcelUtilies.ExcelColumnIndexToName(ExcelUtilies.ExcelColumnNameToIndex(FunnelWeeklyReportParameters.WeekEnd) + offset);

                        YTDACTVsKFR3Book = null;
                        //For the thailand, there will be empty. So no file is exported.
                        if (File.Exists(YTDACTVsKFR3Reports[item]))
                        {
                            YTDACTVsKFR3Book = excel.Workbooks.Open(YTDACTVsKFR3Reports[item]);
                            Excel.Worksheet weekSheet = YTDACTVsKFR3Book.Worksheets[1];
                            weekSheet.Name = item + columnCount;

                            weekSheet.Copy(Missing.Value, tempWorkSheet);
                            Excel.Worksheet copySourceSheet = tempWorkbook.Worksheets[item + columnCount];

                            //copy week report to dest
                            if (copySourceSheet.Cells[1, 1].Text == "Spot Rate")
                                ExcelUtilies.CopyRange(copySourceSheet, "B1", "B" + sourceRowCount, copyDestSheet, columnName + "2");
                            else
                                ExcelUtilies.CopyRange(copySourceSheet, "B1", "B" + sourceRowCount, copyDestSheet, columnName + "1");
                        }

                        copyDestSheet.Cells[1, columnName] = FunnelWeeklyReportParameters.Header_YTDACTVsKRF3;// "YTD ACT vs KFR3";
                        copyDestSheet.Cells[1, columnName].Font.Bold = true;
                        copyDestSheet.Cells[1, columnName].Font.Size = 10;
                        copyDestSheet.Columns[columnName].ColumnWidth = 16;
                    }
                    #endregion

                    #region YTDKFR2
                    //append YTDKFR2
                    //Column BR
                    this.Logger.Message("append YTDKFR2 reports");
                    if (YTDKFR2Reports.ContainsKey(item))
                    {
                        columnCount++;
                        offset++;
                        string columnName = ExcelUtilies.ExcelColumnIndexToName(ExcelUtilies.ExcelColumnNameToIndex(FunnelWeeklyReportParameters.WeekEnd) + offset);
                       
                        YTDKFR2Book = null;
                        //For the thailand, there will be empty. So no file is exported.
                        if (File.Exists(YTDKFR2Reports[item]))
                        {
                            YTDKFR2Book = excel.Workbooks.Open(YTDKFR2Reports[item]);
                            Excel.Worksheet weekSheet = YTDKFR2Book.Worksheets[1];
                            weekSheet.Name = item + columnCount;

                            weekSheet.Copy(Missing.Value, tempWorkSheet);
                            Excel.Worksheet copySourceSheet = tempWorkbook.Worksheets[item + columnCount];

                            //copy week report to dest
                            if (copySourceSheet.Cells[1, 1].Text == "Spot Rate")
                                ExcelUtilies.CopyRange(copySourceSheet, "B1", "B" + sourceRowCount, copyDestSheet, columnName + "2");
                            else
                                ExcelUtilies.CopyRange(copySourceSheet, "B1", "B" + sourceRowCount, copyDestSheet, columnName + "1");
                        }

                        copyDestSheet.Cells[1, columnName] = FunnelWeeklyReportParameters.Header_YTDKRF2;// "YTD KFR2 12/13";
                        copyDestSheet.Cells[1, columnName].Font.Bold = true;
                        copyDestSheet.Cells[1, columnName].Font.Size = 10;
                        copyDestSheet.Columns[columnName].ColumnWidth = 15;
                    }

                    //append YTDACTVsKFR1 report
                    //Column BS
                    this.Logger.Message("append YTDACTVsKFR2 reports");
                    if (YTDACTVsKFR2Reports.ContainsKey(item))
                    {
                        columnCount++;
                        offset++;
                        string columnName = ExcelUtilies.ExcelColumnIndexToName(ExcelUtilies.ExcelColumnNameToIndex(FunnelWeeklyReportParameters.WeekEnd) + offset);
                       
                        YTDACTVsKFR2Book = null;
                        //For the thailand, there will be empty. So no file is exported.
                        if (File.Exists(YTDACTVsKFR2Reports[item]))
                        {
                            YTDACTVsKFR2Book = excel.Workbooks.Open(YTDACTVsKFR2Reports[item]);
                            Excel.Worksheet weekSheet = YTDACTVsKFR2Book.Worksheets[1];
                            weekSheet.Name = item + columnCount;

                            weekSheet.Copy(Missing.Value, tempWorkSheet);
                            Excel.Worksheet copySourceSheet = tempWorkbook.Worksheets[item + columnCount];

                            //copy week report to dest
                            if (copySourceSheet.Cells[1, 1].Text == "Spot Rate")
                                ExcelUtilies.CopyRange(copySourceSheet, "B1", "B" + sourceRowCount, copyDestSheet, columnName + "2");
                            else
                                ExcelUtilies.CopyRange(copySourceSheet, "B1", "B" + sourceRowCount, copyDestSheet, columnName + "1");
                        }

                        copyDestSheet.Cells[1, columnName] = FunnelWeeklyReportParameters.Header_YTDACTVsKRF2;// "YTD ACT vs KFR2";
                        copyDestSheet.Cells[1, columnName].Font.Bold = true;
                        copyDestSheet.Cells[1, columnName].Font.Size = 10;
                        copyDestSheet.Columns[columnName].ColumnWidth = 16;
                    }
                    #endregion

                    #region YTDKFR1
                    //append YTDKFR1
                    //Column BR
                    this.Logger.Message("append YTDKFR1 reports");
                    if (YTDKFR1Reports.ContainsKey(item))
                    {
                        columnCount++;
                        offset++;
                        string columnName = ExcelUtilies.ExcelColumnIndexToName(ExcelUtilies.ExcelColumnNameToIndex(FunnelWeeklyReportParameters.WeekEnd) + offset);
                       
                        YTDKFR1Book = null;
                        //For the thailand, there will be empty. So no file is exported.
                        if (File.Exists(YTDKFR1Reports[item]))
                        {
                            YTDKFR1Book = excel.Workbooks.Open(YTDKFR1Reports[item]);
                            Excel.Worksheet weekSheet = YTDKFR1Book.Worksheets[1];
                            weekSheet.Name = item + columnCount;

                            weekSheet.Copy(Missing.Value, tempWorkSheet);
                            Excel.Worksheet copySourceSheet = tempWorkbook.Worksheets[item + columnCount];

                            //copy week report to dest
                            if (copySourceSheet.Cells[1, 1].Text == "Spot Rate")
                                ExcelUtilies.CopyRange(copySourceSheet, "B1", "B" + sourceRowCount, copyDestSheet, columnName + "2");
                            else
                                ExcelUtilies.CopyRange(copySourceSheet, "B1", "B" + sourceRowCount, copyDestSheet, columnName + "1");
                        }

                        copyDestSheet.Cells[1, columnName] = FunnelWeeklyReportParameters.Header_YTDKRF1;// "YTD KFR1 12/13";
                        copyDestSheet.Cells[1, columnName].Font.Bold = true;
                        copyDestSheet.Cells[1, columnName].Font.Size = 10;
                        copyDestSheet.Columns[columnName].ColumnWidth = 15;
                    }

                    //append YTDACTVsKFR1 report
                    //Column BS
                    this.Logger.Message("append YTDACTVsKFR1 reports");
                    if (YTDACTVsKFR1Reports.ContainsKey(item))
                    {
                        columnCount++;
                        offset++;
                        string columnName = ExcelUtilies.ExcelColumnIndexToName(ExcelUtilies.ExcelColumnNameToIndex(FunnelWeeklyReportParameters.WeekEnd) + offset);
                       
                        YTDACTVsKFR1Book = null;
                        //For the thailand, there will be empty. So no file is exported.
                        if (File.Exists(YTDACTVsKFR1Reports[item]))
                        {
                            YTDACTVsKFR1Book = excel.Workbooks.Open(YTDACTVsKFR1Reports[item]);
                            Excel.Worksheet weekSheet = YTDACTVsKFR1Book.Worksheets[1];
                            weekSheet.Name = item + columnCount;

                            weekSheet.Copy(Missing.Value, tempWorkSheet);
                            Excel.Worksheet copySourceSheet = tempWorkbook.Worksheets[item + columnCount];

                            //copy week report to dest
                            if (copySourceSheet.Cells[1, 1].Text == "Spot Rate")
                                ExcelUtilies.CopyRange(copySourceSheet, "B1", "B" + sourceRowCount, copyDestSheet, columnName + "2");
                            else
                                ExcelUtilies.CopyRange(copySourceSheet, "B1", "B" + sourceRowCount, copyDestSheet, columnName + "1");
                        }

                        copyDestSheet.Cells[1, columnName] = FunnelWeeklyReportParameters.Header_YTDACTVsKRF1;// "YTD ACT vs KFR1";
                        copyDestSheet.Cells[1, columnName].Font.Bold = true;
                        copyDestSheet.Cells[1, columnName].Font.Size = 10;
                        copyDestSheet.Columns[columnName].ColumnWidth = 16;
                    }
                    #endregion

                    #region YTDKFR0

                    //append YTDKFR0
                    //Column BT
                    this.Logger.Message("append YTDKFR0 reports");
                    if (YTDKFR0Reports.ContainsKey(item))
                    {
                        columnCount++;
                        offset++;
                        string columnName = ExcelUtilies.ExcelColumnIndexToName(ExcelUtilies.ExcelColumnNameToIndex(FunnelWeeklyReportParameters.WeekEnd) + offset);
                       
                        YTDKFR0Book = null;
                        //For the thailand, there will be empty. So no file is exported.
                        if (File.Exists(YTDKFR0Reports[item]))
                        {
                            YTDKFR0Book = excel.Workbooks.Open(YTDKFR0Reports[item]);
                            Excel.Worksheet weekSheet = YTDKFR0Book.Worksheets[1];
                            weekSheet.Name = item + columnCount;

                            weekSheet.Copy(Missing.Value, tempWorkSheet);
                            Excel.Worksheet copySourceSheet = tempWorkbook.Worksheets[item + columnCount];

                            //copy week report to dest
                            if (copySourceSheet.Cells[1, 1].Text == "Spot Rate")
                                ExcelUtilies.CopyRange(copySourceSheet, "B1", "B" + sourceRowCount, copyDestSheet, columnName + "2");
                            else
                                ExcelUtilies.CopyRange(copySourceSheet, "B1", "B" + sourceRowCount, copyDestSheet, columnName + "1");
                        }

                        copyDestSheet.Cells[1, columnName] = FunnelWeeklyReportParameters.Header_YTDKRF0;// "YTD KFR0 12/13";
                        copyDestSheet.Cells[1, columnName].Font.Bold = true;
                        copyDestSheet.Cells[1, columnName].Font.Size = 10;
                        copyDestSheet.Columns[columnName].ColumnWidth = 15;
                    }

                    //append YTDACTVsKFRO report
                    //Column BS
                    this.Logger.Message("append YTDACTVsKFRO reports");
                    if (YTDACTVsKFR0Reports.ContainsKey(item))
                    {
                        columnCount++;
                        offset++;
                        string columnName = ExcelUtilies.ExcelColumnIndexToName(ExcelUtilies.ExcelColumnNameToIndex(FunnelWeeklyReportParameters.WeekEnd) + offset);
                       
                        YTDACTVsKFR0Book = null;
                        //For the thailand, there will be empty. So no file is exported.
                        if (File.Exists(YTDACTVsKFR0Reports[item]))
                        {
                            YTDACTVsKFR0Book = excel.Workbooks.Open(YTDACTVsKFR0Reports[item]);
                            Excel.Worksheet weekSheet = YTDACTVsKFR0Book.Worksheets[1];
                            weekSheet.Name = item + columnCount;

                            weekSheet.Copy(Missing.Value, tempWorkSheet);
                            Excel.Worksheet copySourceSheet = tempWorkbook.Worksheets[item + columnCount];

                            //copy week report to dest
                            if (copySourceSheet.Cells[1, 1].Text == "Spot Rate")
                                ExcelUtilies.CopyRange(copySourceSheet, "B1", "B" + sourceRowCount, copyDestSheet, columnName + "2");
                            else
                                ExcelUtilies.CopyRange(copySourceSheet, "B1", "B" + sourceRowCount, copyDestSheet, columnName + "1");
                        }

                        copyDestSheet.Cells[1, columnName] = FunnelWeeklyReportParameters.Header_YTDACTVsKRF0;// "YTD ACT vs KFR0";
                        copyDestSheet.Cells[1, columnName].Font.Bold = true;
                        copyDestSheet.Cells[1, columnName].Font.Size = 10;
                        copyDestSheet.Columns[columnName].ColumnWidth = 16;
                    }

                    #endregion

                    #endregion

                    //Set Header Title
                    copyDestSheet.Cells[1, 1] = FunnelWeeklyReportParameters.Header_FunnelKPI;// "Funnel KPI";

                    //Insert a row to set the title
                    Excel.Range range = copyDestSheet.Range["A1"];
                    range.EntireRow.Insert(Excel.XlInsertShiftDirection.xlShiftDown);
                    copyDestSheet.Cells[1, 1] = item;

                    //calculate current week paid rate
                    this.CalculatePaidRate(copyDestSheet);

                    this.CloseSourceReportsExcel();
                }

                this.Logger.Message("Create dest book.");
                bookDest = excel.Workbooks.Add(Missing.Value);

                //create a new work sheet
                this.Logger.Message("create a new work sheet from dest book.");
                Excel.Worksheet sheetDest = bookDest.Worksheets[1] as Excel.Worksheet;

                //copy to dest from temp workbook
                this.Logger.Message("copy to dest from temp workbook.");
                foreach (var item in tempWorkbook.Worksheets)
                {
                    Excel.Worksheet sheet = item as Excel.Worksheet;
                    if (weeklyReports.Keys.Contains(sheet.Name))
                    {

                        if (sheet.Name == "China" || sheet.Name == "CNMini")
                        {
                            this.AjustChinaLayout(sheet);
                        }
                        else
                        {
                            this.AjustGeneralLayout(sheet);
                        }

                        sheet.Copy(Missing.Value, sheetDest);
                    }
                }

                //delete sheet1 sheet2 sheet3
                this.Logger.Message("delete sheet1 sheet2 sheet3");
                foreach (var item in bookDest.Worksheets)
                {
                    Excel.Worksheet sheet = item as Excel.Worksheet;
                    if (!weeklyReports.Keys.Contains(sheet.Name))
                        sheet.Delete();
                }

                //clear the temp workbook
                FunnelReportHelper.SaveTempWorkbook(tempWorkbook);

                //save the dest
                this.Logger.Message("save the dest.");
                mergedFile = outputFile.Replace(".xls", "_" + DateTime.Now.LastWeekendDate().WeekOfYearString() + ".xls");
                //mergedFile = outputFile.Replace(".xls", "_" + DateTime.Now.LatestTwoWeeksEndDate().WeekOfYearString() + ".xls");
                if (File.Exists(mergedFile))
                    File.Delete(mergedFile);

                bookDest.SaveAs(mergedFile);
                bookDest.Close();

            }
            catch (Exception ex)
            {
                this.Logger.Error(ex.Message + "\n" + ex.StackTrace);

                excel.DisplayAlerts = false;

                this.CloseSourceReportsExcel();

                FunnelReportHelper.SaveTempWorkbook(tempWorkbook);
                FunnelReportHelper.SaveTempWorkbook(bookDest);
                
                excel.DisplayAlerts = true;
                throw;
            }
            finally
            {
                excel.Application.Quit();
            }

            return true;
        }

        private void AjustGeneralLayout(Excel.Worksheet sheet)
        {
            //set title to gray
            sheet.Range["A2", "A" + FunnelWeeklyReportParameters.MergedRowCount.ToString()].Interior.Color = Color.LightGray;

            //set month to yellow
            sheet.Range[FunnelWeeklyReportParameters.MonthBegin + "2", FunnelWeeklyReportParameters.MonthEnd + FunnelWeeklyReportParameters.MergedRowCount.ToString()].Interior.Color = Color.LightYellow;

            //set week to grey
            //string weekColumnEnd = ExcelUtilies.ExcelColumnIndexToName(ExcelUtilies.ExcelColumnNameToIndex(FunnelWeeklyReportParameters.WeekEnd) + 2);
            sheet.Range[FunnelWeeklyReportParameters.WeekBegin + "2", FunnelWeeklyReportParameters.WeekEnd + FunnelWeeklyReportParameters.MergedRowCount.ToString()].Interior.Color = Color.LightGray;

            //set the last year week to yellow
            sheet.Range[FunnelWeeklyReportParameters.WeekBegin + "2", "BN" + FunnelWeeklyReportParameters.MergedRowCount.ToString()].Interior.Color = Color.FromArgb(252,213,180);

            //set others to white
            string columnName = ExcelUtilies.ExcelColumnIndexToName(ExcelUtilies.ExcelColumnNameToIndex(FunnelWeeklyReportParameters.WeekEnd) + 1);
            string columnName1 = ExcelUtilies.ExcelColumnIndexToName(ExcelUtilies.ExcelColumnNameToIndex(FunnelWeeklyReportParameters.WeekEnd) + 15);
            sheet.Range[columnName + "2", columnName1 + FunnelWeeklyReportParameters.MergedRowCount.ToString()].Interior.PatternColorIndex = 0;

            ////set YTD to white
            ////YTD Funnel
            //columnName = ExcelUtilies.ExcelColumnIndexToName(ExcelUtilies.ExcelColumnNameToIndex(FunnelWeeklyReportParameters.WeekEnd) + 3);
            ////YTDACTVsKR0
            //columnName1 = ExcelUtilies.ExcelColumnIndexToName(ExcelUtilies.ExcelColumnNameToIndex(FunnelWeeklyReportParameters.YTDACTVsKFR0) + 2);
            //sheet.Range[columnName + "2", columnName1 + FunnelWeeklyReportParameters.MergedRowCount.ToString()].Interior.PatternColorIndex = 0;

            this.SetRowBackgroundColor(sheet);
            this.SetCurrencyLabel(sheet, sheet.Name);
            
            //Delete all the weeks after current week
            //There are two columns VS1011Week and 1011Week inserted,so need plus 2
            string startColumn = ExcelUtilies.ExcelColumnIndexToName(this.GetCurrentWeekExcelIndex() + 1);
            string endColumn = FunnelWeeklyReportParameters.WeekEnd;

            if (ExcelUtilies.ExcelColumnNameToIndex(startColumn) <= ExcelUtilies.ExcelColumnNameToIndex(endColumn))
                ExcelUtilies.DeleteColumns(sheet, startColumn, endColumn);
            

            //Inser the matrix defination
            this.InsertMatrixDefinations(sheet);

            //delete some rows
            if (sheet.Name == "Mexico+US+ROLA" || sheet.Name == "Europe" || sheet.Name == "ROLA" || sheet.Name == "ROA")
            {
                //ExcelUtilies.DeleteRow(copyDestSheet, FunnelWeeklyReportParameters.BudgetRate); //delete Budget Rate
                ExcelUtilies.DeleteRow(sheet, FunnelWeeklyReportParameters.TotalLocalCurrency); //Total Local Currency
                ExcelUtilies.DeleteRow(sheet, FunnelWeeklyReportParameters.TSLocalCurrency); //TS Local Currency
                ExcelUtilies.DeleteRow(sheet, FunnelWeeklyReportParameters.OnlineLocalCurrency); //Online Local Currency
                ExcelUtilies.DeleteRow(sheet, FunnelWeeklyReportParameters.KFRRate); //KFRRate
                ExcelUtilies.DeleteRow(sheet, FunnelWeeklyReportParameters.SpotRate); //Spot Rate                            
            }    
        }

        private void AjustChinaLayout(Excel.Worksheet sheet)
        {
            //set title to gray
            sheet.Range["A2", "A" + FunnelWeeklyReportChinaParameters.MergedRowCount.ToString()].Interior.Color = Color.LightGray;

            //set month to yellow 
            //Remove Month from weekly funnel report
            sheet.Range[FunnelWeeklyReportParameters.MonthBegin + "2", FunnelWeeklyReportParameters.MonthEnd + FunnelWeeklyReportChinaParameters.MergedRowCount.ToString()].Interior.Color = Color.LightYellow;

            //set week to grey
            //string weekColumnEnd = ExcelUtilies.ExcelColumnIndexToName(ExcelUtilies.ExcelColumnNameToIndex(FunnelWeeklyReportParameters.WeekEnd) + 2);
            sheet.Range[FunnelWeeklyReportParameters.WeekBegin + "2", FunnelWeeklyReportParameters.WeekEnd + FunnelWeeklyReportChinaParameters.MergedRowCount.ToString()].Interior.Color = Color.LightGray;

            //set the last year week to yellow
            sheet.Range[FunnelWeeklyReportParameters.WeekBegin + "2", "BN" + FunnelWeeklyReportParameters.MergedRowCount.ToString()].Interior.Color = Color.FromArgb(252, 213, 180);

            //set others to white
            string columnName = ExcelUtilies.ExcelColumnIndexToName(ExcelUtilies.ExcelColumnNameToIndex(FunnelWeeklyReportParameters.WeekEnd) + 1);
            string columnName1 = ExcelUtilies.ExcelColumnIndexToName(ExcelUtilies.ExcelColumnNameToIndex(FunnelWeeklyReportParameters.WeekEnd) + 15);
            sheet.Range[columnName + "2", columnName1 + FunnelWeeklyReportParameters.MergedRowCount.ToString()].Interior.PatternColorIndex = 0;

            this.SetRowBackgroundColor(sheet);
            this.SetCurrencyLabel(sheet, sheet.Name);
           
            string startColumn = ExcelUtilies.ExcelColumnIndexToName(this.GetCurrentWeekExcelIndex() + 1);
            string endColumn = FunnelWeeklyReportParameters.WeekEnd;
            if (ExcelUtilies.ExcelColumnNameToIndex(startColumn) <= ExcelUtilies.ExcelColumnNameToIndex(endColumn))
                ExcelUtilies.DeleteColumns(sheet, startColumn, endColumn);

            //Inser the matrix defination
            this.InsertMatrixDefinations(sheet);
                  
        }

        private void InsertMatrixDefinations(Excel.Worksheet sheet)
        {
            Excel.Range range = sheet.Range["B1"];

            range.EntireColumn.Insert(Excel.XlInsertShiftDirection.xlShiftToRight);

            if (sheet.Name == "China" || sheet.Name == "CNMini")
            {
                for (int i = 1; i < FunnelWeeklyReportChinaParameters.MergedRowCount; i++)
                {
                    sheet.Cells[i, 2] = this.ChinaMatrixDefinations[i];
                }
            }
            else
            {
                for (int i = 1; i <= FunnelWeeklyReportParameters.MergedRowCount; i++)
                {
                    if (i == FunnelWeeklyReportParameters.TSLocalRow + 1)
                    {
                        if (sheet.Name == "Mexico+US+ROLA")
                        {
                            sheet.Cells[i, 2] = "Total MX+US+ROLA Budget Revenue";
                        }
                        else if (sheet.Name == "Europe")
                        {
                            sheet.Cells[i, 2] = "Total Europe Budget Revenue";
                        }
                        else
                        {
                            sheet.Cells[i, 2] = this.MatrixDefinations[i];
                        }
                    }
                    else
                    {
                        sheet.Cells[i, 2] = this.MatrixDefinations[i];
                    }
                }
            }

            sheet.Range["B1"].EntireColumn.Font.Bold = true;
            sheet.Range["B1"].EntireColumn.Font.Color = Color.Red;
            sheet.Range["B1"].EntireColumn.Interior.ColorIndex = 0;
        }

        private void SetRowBackgroundColor(Excel.Worksheet sheet)
        {
            if (sheet.Name == "China")
            {
                sheet.Range["A" + FunnelWeeklyReportChinaParameters.OnlineRow].EntireRow.Interior.Color = Color.LightSkyBlue;
                sheet.Range["A" + FunnelWeeklyReportChinaParameters.TeleSalesRow].EntireRow.Interior.Color = Color.LightSkyBlue;
            }
            else
            {
                sheet.Range["A" + FunnelWeeklyReportParameters.OnlineRow].EntireRow.Interior.Color = Color.LightSkyBlue;
                sheet.Range["A" + FunnelWeeklyReportParameters.TeleSalesRow].EntireRow.Interior.Color = Color.LightSkyBlue;
            }
        }

        private void CloseSourceReportsExcel()
        {
            FunnelReportHelper.CloseWorkingWorkbook(monthBook);
            FunnelReportHelper.CloseWorkingWorkbook(weekBook);
            FunnelReportHelper.CloseWorkingWorkbook(weekBook1);
            FunnelReportHelper.CloseWorkingWorkbook(YTDFunnelBook);
            FunnelReportHelper.CloseWorkingWorkbook(YTDKFR1Book);
            FunnelReportHelper.CloseWorkingWorkbook(YTDACTVsKFR1Book);
            FunnelReportHelper.CloseWorkingWorkbook(vs1011WeekBook);
            FunnelReportHelper.CloseWorkingWorkbook(vs1011YTDBook);
            FunnelReportHelper.CloseWorkingWorkbook(Week1011Book);
            FunnelReportHelper.CloseWorkingWorkbook(YTD1011Book);


            FunnelReportHelper.CloseWorkingWorkbook(YTDKFR3Book);
            FunnelReportHelper.CloseWorkingWorkbook(YTDACTVsKFR3Book);
            FunnelReportHelper.CloseWorkingWorkbook(YTDKFR2Book);
            FunnelReportHelper.CloseWorkingWorkbook(YTDACTVsKFR2Book);
            FunnelReportHelper.CloseWorkingWorkbook(YTDKFR0Book);
            FunnelReportHelper.CloseWorkingWorkbook(YTDACTVsKFR0Book);
        }
        
        private void SetCurrencyLabel(Excel.Worksheet copyDestSheet, string country)
        {
            string currency = this.GetCurrencyByCountry(country);
            
            int begin = ExcelUtilies.ExcelColumnNameToIndex(FunnelWeeklyReportParameters.MonthBegin);
            int end = ExcelUtilies.ExcelColumnNameToIndex(FunnelWeeklyReportParameters.WeekEnd) + 3; //To YTD ACT 12/13 column

            int TotalLocalRow;
            int TSLocalRow;
            int OnlineLocalRow;

            if (country == "China" || country == "CNMini")
            {
                TotalLocalRow = FunnelWeeklyReportChinaParameters.TotalLocalRow;
                TSLocalRow = FunnelWeeklyReportChinaParameters.TSLocalRow;
                OnlineLocalRow = FunnelWeeklyReportChinaParameters.OnlineLocalRow;
            }
            else
            {
                TotalLocalRow = FunnelWeeklyReportParameters.TotalLocalRow;
                TSLocalRow = FunnelWeeklyReportParameters.TSLocalRow;
                OnlineLocalRow = FunnelWeeklyReportParameters.OnlineLocalRow;
            }

            for (int i = begin; i < end + 1; i++)
            {
                string number = copyDestSheet.Cells[TSLocalRow, i].Text;

                if (!string.IsNullOrWhiteSpace(number) && !number.Contains("%"))
                {
                    if (currency == "€")
                    {
                        copyDestSheet.Cells[TotalLocalRow, i].NumberFormat = "€#,##0;-€#,##0";
                        copyDestSheet.Cells[TSLocalRow, i].NumberFormat = "€#,##0;-€#,##0";
                        copyDestSheet.Cells[OnlineLocalRow, i].NumberFormat = "€#,##0;-€#,##0";
                    }
                    else if (currency == "$")
                    {
                        copyDestSheet.Cells[TotalLocalRow, i].NumberFormat = "$#,##0;-$#,##0";
                        copyDestSheet.Cells[TSLocalRow, i].NumberFormat = "$#,##0;-$#,##0";
                        copyDestSheet.Cells[OnlineLocalRow, i].NumberFormat = "$#,##0;-$#,##0";
                    }
                    else
                    {
                        string formatString = string.Format("[${0}]#,##0;-[${0}]#,##0", currency);
                        copyDestSheet.Cells[TotalLocalRow, i].NumberFormat = formatString;
                        copyDestSheet.Cells[TSLocalRow, i].NumberFormat = formatString;
                        copyDestSheet.Cells[OnlineLocalRow, i].NumberFormat = formatString;
                    }
                }
            }
        }

        #endregion

        private string GetCurrencyByCountry(string country)
        {
            CountryCurrencies.Clear();
            CountryCurrencies.Add("Spain", "€");
            CountryCurrencies.Add("MEAST", "$");
            CountryCurrencies.Add("ROE", "$");
            CountryCurrencies.Add("Italy", "€");
            CountryCurrencies.Add("Germany", "€");
            CountryCurrencies.Add("France", "€");
            CountryCurrencies.Add("US", "$");
            CountryCurrencies.Add("Mexico", "MXN");
            CountryCurrencies.Add("Brazil", "BRL");
            CountryCurrencies.Add("Taiwan", "NTD");
            CountryCurrencies.Add("Hong Kong", "HKD");
            CountryCurrencies.Add("China", "CNY");
            CountryCurrencies.Add("CNMini", "CNY");
            CountryCurrencies.Add("Korea", "KRW");
            CountryCurrencies.Add("Japan", "JPY");
            CountryCurrencies.Add("ROA", "$");
            CountryCurrencies.Add("ROLA", "$");
            CountryCurrencies.Add("ROW", "$");
            CountryCurrencies.Add("Thailand", "THB");
            CountryCurrencies.Add("Russia", "RUB");

            if (CountryCurrencies.ContainsKey(country))
                return CountryCurrencies[country];
            else 
                return string.Empty;
        }

        private void CalculatePaidRate(Excel.Worksheet sheet)
        {
            int currentIndex = this.GetCurrentWeekExcelIndex();
            if (currentIndex == 0)
                return;

            if (sheet.Name == "Brazil" || sheet.Name == "Japan")
            {
                string column1 = ExcelUtilies.ExcelColumnIndexToName(currentIndex - 4);
                string column2 = ExcelUtilies.ExcelColumnIndexToName(currentIndex - 3);
                string column3 = ExcelUtilies.ExcelColumnIndexToName(currentIndex - 2);
                string column4 = ExcelUtilies.ExcelColumnIndexToName(currentIndex - 1);
                string column5 = ExcelUtilies.ExcelColumnIndexToName(currentIndex);

                //calculate last week
                //Converted to FTP
                sheet.Cells[FunnelWeeklyReportParameters.ConvertedToFTPRow, currentIndex - 1] = string.Format("={0}*AVERAGE({1}:{2})", column4 + FunnelWeeklyReportParameters.FTORow, column1 + FunnelWeeklyReportParameters.PaidRateRow, column3 + FunnelWeeklyReportParameters.PaidRateRow);
                //Paid Rate
                sheet.Cells[FunnelWeeklyReportParameters.PaidRateRow, currentIndex - 1] = string.Format("={0}/{1}", column4 + FunnelWeeklyReportParameters.ConvertedToFTPRow, column4 + FunnelWeeklyReportParameters.FTORow);

                //calculete current week
                //Converted to FTP
                sheet.Cells[FunnelWeeklyReportParameters.ConvertedToFTPRow, currentIndex] = string.Format("={0}*AVERAGE({1}:{2})", column5 + FunnelWeeklyReportParameters.FTORow, column2 + FunnelWeeklyReportParameters.PaidRateRow, column4 + FunnelWeeklyReportParameters.PaidRateRow);
                //Paid Rate
                sheet.Cells[FunnelWeeklyReportParameters.PaidRateRow, currentIndex] = string.Format("={0}/{1}", column5 + FunnelWeeklyReportParameters.ConvertedToFTPRow, column5 + FunnelWeeklyReportParameters.FTORow);
            }
            else
            {
                string column1 = ExcelUtilies.ExcelColumnIndexToName(currentIndex - 2);
                string column2 = ExcelUtilies.ExcelColumnIndexToName(currentIndex - 1);
                string column3 = ExcelUtilies.ExcelColumnIndexToName(currentIndex);

                //Converted to FTP
                sheet.Cells[FunnelWeeklyReportParameters.ConvertedToFTPRow, currentIndex] = string.Format("={0}*AVERAGE({1}:{2})", column3 + FunnelWeeklyReportParameters.FTORow, column1 + FunnelWeeklyReportParameters.PaidRateRow, column2 + FunnelWeeklyReportParameters.PaidRateRow);
                //Paid Rate
                sheet.Cells[FunnelWeeklyReportParameters.PaidRateRow, currentIndex] = string.Format("={0}/{1}", column3 + FunnelWeeklyReportParameters.ConvertedToFTPRow, column3 + FunnelWeeklyReportParameters.FTORow);
            }
        }

        private int GetCurrentWeekExcelIndex()
        {
            int currentIndex = 0;

            DateTime fiscalStartDate;

            if (DateTime.Today.LastWeekendDate().Month < 10)
                fiscalStartDate = new DateTime(DateTime.Today.LastWeekendDate().Year - 1, 10, 1);
            else
                fiscalStartDate = new DateTime(DateTime.Today.LastWeekendDate().Year, 10, 1);

            int count = 0;

            if (fiscalStartDate.DayOfWeek == DayOfWeek.Monday)
                count = ((DateTime.Today.LastWeekendDate() - fiscalStartDate).Days + 1) / 7;
            else
                count = ((DateTime.Today.LastWeekendDate() - fiscalStartDate).Days + 1) / 7 + 1;

            //BB column is the 2012Wk40(2012/09/24-2012/09/30)
            currentIndex = ExcelUtilies.ExcelColumnNameToIndex("BN") + count;

            return currentIndex;
        }
       
        private Dictionary<int, string> GetMatrixDefinations()
        {
            if (this.matrixDefinations == null)
            {
                matrixDefinations = new Dictionary<int, string>();
                matrixDefinations.Add(1, string.Empty);
                matrixDefinations.Add(2, string.Empty);
                matrixDefinations.Add(3, string.Empty);
                matrixDefinations.Add(4, string.Empty);
                matrixDefinations.Add(5, string.Empty);
                matrixDefinations.Add(6, "A");
                matrixDefinations.Add(7, "B");
                matrixDefinations.Add(8, "C");
                matrixDefinations.Add(9, "D=B/A");
                matrixDefinations.Add(10, "E=C/B");
                matrixDefinations.Add(11, "F");
                matrixDefinations.Add(12, "G=I/F");
                matrixDefinations.Add(13, "H");
                matrixDefinations.Add(14, "I");
                matrixDefinations.Add(15, "J");
                matrixDefinations.Add(16, "K");
                matrixDefinations.Add(17, "L");
                matrixDefinations.Add(18, "R");
                matrixDefinations.Add(19, "M=I+J+K+L+R");
                matrixDefinations.Add(20, "N");
                matrixDefinations.Add(21, "O=M+N");
                matrixDefinations.Add(22, "P=O*Spot rate");
                matrixDefinations.Add(23, "Q=P/KFR rate");
                matrixDefinations.Add(24, string.Empty);
                matrixDefinations.Add(25, "AA");
                matrixDefinations.Add(26, "AB");
                matrixDefinations.Add(27, "AC=AB/AA");
                matrixDefinations.Add(28, "AD");
                matrixDefinations.Add(29, "AE=AD/AB");
                matrixDefinations.Add(30, "AF=AJ/AH");
                matrixDefinations.Add(31, "AG=AM/AB");
                matrixDefinations.Add(32, "AH");
                matrixDefinations.Add(33, "AI=AH/AB");
                matrixDefinations.Add(34, "AJ");
                matrixDefinations.Add(35, "AK");
                matrixDefinations.Add(36, "AL");
                matrixDefinations.Add(37, "AR");
                matrixDefinations.Add(38, "AQ");
                matrixDefinations.Add(39, "AM=AJ+AK+AL+AR+AQ");
                matrixDefinations.Add(40, "AN=AM*Spot rate");
                matrixDefinations.Add(41, "AO=AN/KFR rate");
                matrixDefinations.Add(42, "'=O+AM");
                matrixDefinations.Add(43, "'=P+AN");
                matrixDefinations.Add(44, "'=Q+AO");
            }
            
            return this.matrixDefinations;
        }

        private Dictionary<int, string> GetChinaMatrixDefinations()
        {
            if (this.chinaMatrixDefinations == null)
            {
                chinaMatrixDefinations = new Dictionary<int, string>();
                chinaMatrixDefinations.Add(1, string.Empty);
                chinaMatrixDefinations.Add(2, string.Empty);
                chinaMatrixDefinations.Add(3, string.Empty);
                chinaMatrixDefinations.Add(4, string.Empty);
                chinaMatrixDefinations.Add(5, string.Empty);
                chinaMatrixDefinations.Add(6, "A");
                chinaMatrixDefinations.Add(7, "B");
                chinaMatrixDefinations.Add(8, "C");
                chinaMatrixDefinations.Add(9, "D=B/A");
                chinaMatrixDefinations.Add(10, "E=C/B");
                chinaMatrixDefinations.Add(11, "F");
                chinaMatrixDefinations.Add(12, "G=I/F");
                chinaMatrixDefinations.Add(13, "H");
                chinaMatrixDefinations.Add(14, "I");
                chinaMatrixDefinations.Add(15, "J");
                chinaMatrixDefinations.Add(16, "K");
                chinaMatrixDefinations.Add(17, "L");
                chinaMatrixDefinations.Add(18, "R");
                chinaMatrixDefinations.Add(19, "M=I+J+K+L");
                chinaMatrixDefinations.Add(20, "N");
                chinaMatrixDefinations.Add(21, "O=M+N");
                chinaMatrixDefinations.Add(22, "P=O*Spot rate");
                chinaMatrixDefinations.Add(23, "Q=P/KFR rate");
                chinaMatrixDefinations.Add(24, string.Empty);
                chinaMatrixDefinations.Add(25, "AA");
                chinaMatrixDefinations.Add(26, "AB");
                chinaMatrixDefinations.Add(27, "AC=AB/AA");
                chinaMatrixDefinations.Add(28, "AD");
                chinaMatrixDefinations.Add(29, "AE=AD/AB");
                chinaMatrixDefinations.Add(30, "AF=AJ/AH");
                chinaMatrixDefinations.Add(31, "AG=AM/AB");
                chinaMatrixDefinations.Add(32, "AH");
                chinaMatrixDefinations.Add(33, "AI=AH/AB");
                chinaMatrixDefinations.Add(34, "AJ");
                chinaMatrixDefinations.Add(35, "AK");
                chinaMatrixDefinations.Add(36, "AM=AJ+AK");
                chinaMatrixDefinations.Add(37, "AN=AM*Spot rate");
                chinaMatrixDefinations.Add(38, "AO=AN/KFR rate");
                chinaMatrixDefinations.Add(39, "'=O+AM");
                chinaMatrixDefinations.Add(40, "'=P+AN");
                chinaMatrixDefinations.Add(41, "'=Q+AO");
            }

            return this.chinaMatrixDefinations;
        }      
    }          
}              
