using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QlikView.Common;

namespace QlikView.Configuration
{
    public interface ISingleFieldSelection
    {
        ReportConnection Connection { get; set; }
        event Action<string> ItemSelected;

        void Open();
    }
}
