using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QlikView.Common
{
    public interface IError
    {
        bool HasError { get; set; }
        StringBuilder ErrorMessage { get; set; }
    }
}
