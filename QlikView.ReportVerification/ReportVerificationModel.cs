using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QlikView.ReportVerification
{
    public class ReportVerificationModel
    {
        public string SourceFile { get; set; }
        public int SourceHeaderRowNumber { get; set; }
        public int DestinationHeaderRowNumber { get; set; }
        public string DestinationFile { get; set; }
        public bool IsIdentity { get; set; }

        public ReportDataSet SourceDataSet { get; set; }
        public ReportDataSet DestinationDataSet { get; set; }

        public ReportDataSet VerifiedResultSet { get; set; }
        public Dictionary<string,string> VerifiedResultMsg { get; set; }

        public ReportVerificationModel()
        {
            SourceDataSet = new ReportDataSet();
            DestinationDataSet = new ReportDataSet();
            VerifiedResultSet = new ReportDataSet();
            VerifiedResultMsg = new Dictionary<string, string>();
        }
    }
}
