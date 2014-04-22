using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QlikView.Common
{
    public class QVLogItem
    {
        public DateTime LogTime { get; set; }
        public LogType Type { get; set; }
        public string Message { get; set; }
    }
}
