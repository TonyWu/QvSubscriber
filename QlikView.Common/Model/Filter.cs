using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QlikView.Common
{
    public class Filter : ReportItemBase
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
                this.OnPropertyChanged(()=>this.Description);
            }
        }

        private ReportConnection _connection;
        public ReportConnection Connection
        {
            get
            {
                return _connection;
            }
            set
            {
                _connection = value;
                this.OnPropertyChanged(() => this.Connection);
            }
        }
        public ReportItemDictionary<QVField> Fields { get; set; }
        public ReportItemDictionary<QvVariable> Variables { get; set; }

        public Filter()
        {
            this.Fields = new ReportItemDictionary<QVField>();
            this.Variables = new ReportItemDictionary<QvVariable>();
        }
    }
}
