using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace QlikView.Common
{
    public class QVConfigLog:QVLogBase
    {
        public QVConfigLog()
        {
            if (!Directory.Exists("log"))
                Directory.CreateDirectory("log");
            this._logFile = string.Format(@"log\Config_{0}.txt",DateTime.Now.ToString("yyyyMMdd"));

            this.InitializeLogFile();
        }
       
    }
}
