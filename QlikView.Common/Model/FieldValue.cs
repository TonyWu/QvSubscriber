using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QlikView.Common
{
    public class FieldValue
    {
        public string Value { get; set; }
        public double Number { get; set; }
        public bool IsNumeric { get; set; }
    }
}
