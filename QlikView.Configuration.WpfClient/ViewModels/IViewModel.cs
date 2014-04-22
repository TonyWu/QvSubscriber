using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QlikView.Common;

namespace QlikView.Configuration.WpfClient.ViewModels
{
    public interface IViewModel
    {
        IReportItem ReportItem { get; set; }
        bool IsNew { get; set; }
        event Action<IReportItem> QvItemAdded;
        event EventHandler QvItemAddCanceled;
        void Initialize();
    }
}
