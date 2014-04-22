using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QlikView.Common;

namespace QlikView.Configuration
{
    public interface IBasePresenter<T> where T: IReportItem
    {
        IMainView MainView { get; set; }
        IBaseView<T> View { get; set; }
        ReportConfigManagerBase<T> ConfigManager { get; set; }
        void Run();
    }
}
