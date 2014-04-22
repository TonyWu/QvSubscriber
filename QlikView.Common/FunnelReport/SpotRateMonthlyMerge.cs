using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace QlikView.Common
{
    class SpotRateMonthlyMerge: IExcelMerge
    {
        public ILog Logger { get; set; }

        public bool MergeFiles(Dictionary<string, ReportContext> MergedFiles, string outputFile, out string mergedFile)
        {
            if (this.Logger == null)
                this.Logger = new QVConfigLog();

            if (MergedFiles.Values.Count < 1)
                throw new Exception("Change Spot Rate Monthly file name failed.");

            DateTime lastMonthendDate = DateTime.Now.Date.AddDays(0 - DateTime.Now.Day);
            mergedFile = outputFile.Replace(".xls", "_" + lastMonthendDate.ToString("yyyyMM") + ".xls");

            if (File.Exists(mergedFile))
                File.Delete(mergedFile);

            File.Copy(MergedFiles.Values.First().OutputFullName, mergedFile);

            return true;
        }
    }
}
