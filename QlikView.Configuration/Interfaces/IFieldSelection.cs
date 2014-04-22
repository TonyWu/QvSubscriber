using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QlikView.Common;

namespace QlikView.Configuration
{
    public interface IFieldSelection
    {
        ReportConnection Connection { get; set; }
        event Action<QVField> FieldAdded;

        void Open();
    }
}
