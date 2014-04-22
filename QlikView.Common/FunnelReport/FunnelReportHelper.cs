using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;

namespace QlikView.Common
{
    public class FunnelReportHelper
    {
        public static Dictionary<string, string> GetMergedFilesByCategory(Dictionary<string, ReportContext> MergedFiles, string p)
        {
            Dictionary<string, string> files = new Dictionary<string, string>();

            //<Report Name="FunnelMonthly_1011YTD_Thailand" Description="Thailand">
            foreach (var item in MergedFiles.Keys)
            {
                if (!item.Contains("Output"))
                {
                    string[] array = item.Split('_');
                    if (array[1] == p)
                    {
                        files.Add(MergedFiles[item].Description, MergedFiles[item].OutputFullName);
                    }
                }
            }

            return files;
        }

        public static Dictionary<string, string> GetOutputMergedFilesByCategory(Dictionary<string, ReportContext> MergedFiles, string p)
        {
            Dictionary<string, string> files = new Dictionary<string, string>();

            foreach (var item in MergedFiles.Keys)
            {
                //<Report Name="FunnelMonthly_Output_PriorYear_Japan" Description="Japan">
                if (item.Contains("Output"))
                {
                    string[] array = item.Split('_');
                    if (array[2] == p)
                    {
                        files.Add(MergedFiles[item].Description, MergedFiles[item].OutputFullName);
                    }
                }
            }

            return files;
        }


        public static void CloseWorkingWorkbook(Excel.Workbook workbook)
        {
            if (workbook != null)
            {
                if (!workbook.Saved)
                    workbook.Save();
               
                workbook.Close();
                ExcelUtilies.ReleaseComObject(workbook);
            }
        }

        public static void SaveTempWorkbook(Excel.Workbook tempWorkbook)
        {
            if (tempWorkbook != null)
            {
                //string folder = System.Reflection.Assembly.GetExecutingAssembly().CodeBase.Remove(System.Reflection.Assembly.GetExecutingAssembly().CodeBase.LastIndexOf("/"));
                //folder = folder.Replace(@"file:///", "");
                //if (!Directory.Exists(folder + "/temp"))
                //    Directory.CreateDirectory(folder + "/temp");

                //tempWorkbook.SaveAs(folder + "/temp/book1.xls");
                tempWorkbook.SaveAs("book1.xls");
                string fullname = tempWorkbook.FullName;
                tempWorkbook.Close();
                if (File.Exists(fullname))
                    File.Delete(fullname);
                ExcelUtilies.ReleaseComObject(tempWorkbook);
            }
        }

        public static Dictionary<int, string> GetFunnelMonthReportMonthColumnMapping()
        {
            Dictionary<int, string> mapping = new Dictionary<int, string>();
            //mapping.Add(1, "F");
            //mapping.Add(2, "G");
            //mapping.Add(3, "H");
            //mapping.Add(4, "I");
            //mapping.Add(5, "J");
            //mapping.Add(6, "K");
            //mapping.Add(7, "L");
            //mapping.Add(8, "M");
            //mapping.Add(9, "N");
            //mapping.Add(10, "C");
            //mapping.Add(11, "D");
            //mapping.Add(12, "E");

            mapping.Add(1, "Q");
            mapping.Add(2, "R");
            mapping.Add(3, "S");
            mapping.Add(4, "T");
            mapping.Add(5, "U");
            mapping.Add(6, "V");
            mapping.Add(7, "W");
            mapping.Add(8, "X");
            mapping.Add(9, "Y");
            mapping.Add(10, "Z");
            mapping.Add(11, "O");
            mapping.Add(12, "P");

            return mapping;
        }
    }
}
