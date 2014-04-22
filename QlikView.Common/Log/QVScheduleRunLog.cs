using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace QlikView.Common
{
    public class QVScheduleRunLog:QVLogBase
    {
        public QVScheduleRunLog()
        {
            string folder = System.Reflection.Assembly.GetExecutingAssembly().CodeBase.Remove(System.Reflection.Assembly.GetExecutingAssembly().CodeBase.LastIndexOf("/"));
            folder = folder.Replace("file:///", "");
            if (!Directory.Exists(folder + "/log"))
                Directory.CreateDirectory(folder + "/log");
            
            this._logFile = folder + string.Format("/log/SchedulerRunning_{0}.txt", DateTime.Now.ToString("yyyyMMddHHmmss"));

            this.InitializeLogFile();
        }
    }
}
