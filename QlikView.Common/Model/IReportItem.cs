using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QlikView.Common
{
    public interface IReportItem
    {
        string Name { get; set; }
        /// <summary>
        /// Report's Parent is Task, The Filter's Parent is also the Task
        /// </summary>
        ReportItemDictionary<IReportItem> Parents { get; set; }
    }
}
