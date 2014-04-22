using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace QlikView.Common
{
    public class ReportTask : ReportItemBase
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

        private string _incldue;
        public string Include
        {
            get
            {
                return _incldue;
            }
            set
            {
                _incldue = value;
                this.OnPropertyChanged(() => this.Include);
            }
        }

        private string _outputFolder;
        public string OutputFolder
        {
            get
            {
                return _outputFolder;
            }
            set
            {
                _outputFolder = value;
                this.OnPropertyChanged(() => this.OutputFolder);
            }
        }
        public ReportItemDictionary<QlikViewReport> Reports { get; set; }
        public ReportItemDictionary<Recipient> Recipients { get; set; }

        private RecipientGroup _group;
        public RecipientGroup Group
        {
            get
            {
                return _group;
            }
            set
            {
                _group = value;
                this.OnPropertyChanged(() => this.Group);
            }
        }

        private Message _messageDefinition;
        public Message MessageDefinition
        {
            get
            {
                return _messageDefinition;
            }
            set
            {
                _messageDefinition = value;
                this.OnPropertyChanged(() => this.MessageDefinition);
            }
        }

        private bool _isSendMailInSingleMail;
        public bool IsSendMailInSingleMail
        {
            get
            {
                return _isSendMailInSingleMail;
            }
            set
            {
                _isSendMailInSingleMail = value;
                this.OnPropertyChanged(() => this.IsSendMailInSingleMail);
            }
        }

        private bool _isMergeInSingleExcel;
        public bool IsMergeInSingleExcel
        {
            get
            {
                return _isMergeInSingleExcel;
            }
            set
            {
                _isMergeInSingleExcel = value;
                this.OnPropertyChanged(() => this.IsMergeInSingleExcel);
            }
        }

        public ReportTask()
        {
            this.Reports = new ReportItemDictionary<QlikViewReport>();
            this.Reports.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(Reports_CollectionChanged);
            this.Recipients = new ReportItemDictionary<Recipient>();

            this.MessageDefinition = new Message();
        }

        void Reports_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                if (e.NewItems.Count > 0)
                {
                    KeyValuePair<string, QlikViewReport> report = (KeyValuePair<string, QlikViewReport>)e.NewItems[0];
                    if (report.Value != null && !report.Value.Parents.ContainsKey(this.Name))
                    {
                        report.Value.Parents.Add(this.Name,this);
                    }
                }
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                if (e.OldItems.Count > 0)
                {
                    KeyValuePair<string, QlikViewReport> report = (KeyValuePair<string, QlikViewReport>)e.OldItems[0];
                    if (report.Value != null && report.Value.Parents.ContainsKey(this.Name))
                    {
                        report.Value.Parents.Remove(this.Name);
                    }
                }
            }
        }
    }
}
