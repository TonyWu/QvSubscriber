using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QlikView.Common
{
    public enum SheetObjectType
    {
        ListBox = 1,
        MultiBox,
        StaticticsBox,
        TableBox,
        Button,
        TextBox,
        CurrentSelection,
        InputBox,
        PivotTable = 10,
        StraigtTable = 11,
        Graph = 13,
        Container = 36
    }
}
