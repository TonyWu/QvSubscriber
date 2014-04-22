using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QlikView.Common
{
    public class ReportSchedulerDefinition : ReportItemBase
    {
        private string _description;
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                this.OnPropertyChanged(() => this.Description);
            }
        }
        //public DateTime StartDate { get; set; }
        //public DateTime ShotTime { get; set; }
        //public ScheduleType ScheduleType { get; set; }
        public ReportItemDictionary<ReportTask> Tasks { get; set; }

        public ReportSchedulerDefinition()
        {
            this.Tasks = new ReportItemDictionary<ReportTask>();
        }
    }
}
