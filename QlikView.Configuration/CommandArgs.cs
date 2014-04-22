using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QlikView.Configuration
{
    public class CommandArgs<T>:EventArgs
    {
        public IMainView MainView { get; set; }
        public string TypeProfix { get; set; }
        public T Target { get; set; }
    }
}
