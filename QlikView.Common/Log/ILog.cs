using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QlikView.Common
{
    public interface ILog
    {
        void Info(string information);
        void Message(string message);
        void Error(string error);
    }
}
