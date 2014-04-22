using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QlikView.Common
{
    public class ReportContext
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string OutputFullName { get; set; }
        public string HtmlFormat { get; set; }
        public ReportType ReportType { get; set; }
    }
}
