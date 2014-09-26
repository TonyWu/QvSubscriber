using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.IO;
using QlikView.Common.Zip;

namespace QlikView.Common
{
    public class EFECMKTDash : IExcelMerge, IDisposable
    {
        Excel.Application excel = new Excel.Application();

        #region IExcelOperation Members
        public ILog Logger { get; set; }

        Excel.Workbook sourceBook = null;

        public bool MergeFiles(Dictionary<string, ReportContext> MergedFiles, string mergedFile, out string outputFile)
        {
            try
            {
                Excel.Workbook bookDest = excel.Workbooks.Add(Missing.Value);

                //create a new work sheet
                Excel.Worksheet sheetDest = bookDest.Worksheets[1] as Excel.Worksheet;

                foreach (var item in MergedFiles.Keys)
                {
                    if (!File.Exists(MergedFiles[item].OutputFullName))
                    {
                        continue;
                    }

                    this.Logger.Message("Merge " + item);
                    sourceBook = excel.Workbooks.Open(MergedFiles[item].OutputFullName);
                    Excel.Worksheet sheet = sourceBook.Worksheets[1];
                    sheet.Name = MergedFiles[item].Description;

                    sheet.Copy(Missing.Value, sheetDest);

                    Excel.Worksheet copysheet = bookDest.Worksheets[MergedFiles[item].Description];

                   
                    FunnelReportHelper.SaveTempWorkbook(sourceBook);
                }

                foreach (var item in bookDest.Worksheets)
                {
                    Excel.Worksheet sheet = item as Excel.Worksheet;
                    if (!MergedFiles.Values.Select(x=>x.Description).Contains(sheet.Name))
                        sheet.Delete();            
                }

                outputFile = mergedFile.Replace(".xls", "_" + DateTime.Now.ToString("yyyMMdd_HHmmss") + ".xls");

                bookDest.SaveAs(outputFile);
                bookDest.Close();

                ZipHelper.ZipFile(outputFile, outputFile.Replace(".xls", ".zip"), 1);
                outputFile = outputFile.Replace(".xls", ".zip");
            }
            catch (Exception ex)
            {
                this.Logger.Error(ex.Message + ex.StackTrace);
                throw;
            }

            return true;
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            this.excel.Quit();
        }

        #endregion

     
    }
}
