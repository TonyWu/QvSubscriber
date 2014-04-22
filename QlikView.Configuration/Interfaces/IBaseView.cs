using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QlikView.Common;

namespace QlikView.Configuration
{
    public interface IBaseView<T>
    {
        T Item { get; set; }
        void Open();
        event Action<T> ItemAdded;
        event Action<T> ItemUpdated;
    }
}
