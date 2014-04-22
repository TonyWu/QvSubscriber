using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QlikView.Common
{
    public interface IExcelMerge
    {
        ILog Logger { get; set; }
        bool MergeFiles(Dictionary<string,ReportContext> MergedFiles, string outputFile, out string mergedFile);
    }
}
