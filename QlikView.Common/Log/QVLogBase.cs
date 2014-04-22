using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace QlikView.Common
{
    public class QVLogBase:ILog
    {
        protected string _logFile;
        private List<QVLogItem> _logList;
        private string _seperate = "        ||";

        public QVLogBase()
        {
            this._logList = new List<QVLogItem>();
        }

        protected void InitializeLogFile()
        {
            if (!File.Exists(this._logFile))
            {
                var stream = File.Create(this._logFile);
                StreamWriter sw = new StreamWriter(stream);
                sw.WriteLine(string.Format("Date{0}Time{0}Type{0}Message", this._seperate));

                sw.Close();
                stream.Close();
            }
        }

        #region ILog Members

        public virtual void Info(string information)
        {
            this.Log(information, LogType.Information);
        }

        public virtual void Message(string message)
        {
            this.Log(message, LogType.Message);
        }

        public virtual void Error(string error)
        {
            this.Log(error, LogType.Error);
        }

        #endregion

        private void Log(string message, LogType type)
        {
            using (var sw = new StreamWriter(this._logFile, true))
            {
                sw.WriteLine(DateTime.Now.ToString("MM/dd/yyyy")
                    + this._seperate
                    + DateTime.Now.ToString("HH:mm:ss")
                    + this._seperate
                    + type.ToString()
                    + this._seperate
                    + message);
            }
        }
    }
}
