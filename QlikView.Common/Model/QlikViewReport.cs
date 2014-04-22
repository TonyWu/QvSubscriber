using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QlikView.Common
{
    public class QlikViewReport : ReportItemBase
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

        private string _outputFileName;
        public string OutputFielName
        {
            get
            {
                return _outputFileName;
            }
            set
            {
                _outputFileName = value;
                this.OnPropertyChanged(() => this.OutputFielName);
            }
        }

        private bool _enableDynamicNaming;
        public bool EnableDynamicNaming
        {
            get
            {
                return _enableDynamicNaming;
            }
            set
            {
                _enableDynamicNaming = value;
                this.OnPropertyChanged(() => this.EnableDynamicNaming);
            }
        }

        private string _qlikViewExportObjectId;
        public string QlikViewExportObjectId
        {
            get
            {
                return _qlikViewExportObjectId;
            }
            set
            {
                _qlikViewExportObjectId = value;
                this.OnPropertyChanged(() => this.QlikViewExportObjectId);
            }
        }

        private Filter _filter;
        public Filter Filter
        {
            get
            {
                return _filter;
            }
            set
            {
                if (value == null )
                {
                    if (_filter != null)
                        _filter.Parents.Remove(this.Name);
                }
                else
                {
                    if (!value.Parents.ContainsKey(this.Name))
                        value.Parents.Add(this.Name, this);
                }

                _filter = value;
                this.OnPropertyChanged(() => this.Filter);
            }
        }

        private ReportType _reportType;
        public ReportType ReportType
        {
            get
            {
                return _reportType;
            }
            set
            {
                _reportType = value;
                this.OnPropertyChanged(() => this.ReportType);
            }
        }

        private bool _isEmbeddedInMail;
        public bool IsEmbeddedInMail
        {
            get
            {
                return _isEmbeddedInMail;
            }
            set
            {
                _isEmbeddedInMail = value;
                this.OnPropertyChanged(() => this.IsEmbeddedInMail);
            }
        }
    }
}
