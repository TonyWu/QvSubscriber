using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QlikView.Common;
using QlikView.Configuration.WpfClient.ViewModels;

namespace QlikView.Configuration.WpfClient.Views
{
    public interface IView
    {
        IViewModel ViewModel { get; set; }
        void Bind();
    }
}
