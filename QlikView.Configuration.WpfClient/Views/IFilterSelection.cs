using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QlikView.Common;

namespace QlikView.Configuration.WpfClient.Views
{
    public interface IFilterSelection
    {
        ReportConnection Connection { get; set; }
        ReportItemDictionary<QVField> Fields { get; set; }

        void Open();
    }
}
