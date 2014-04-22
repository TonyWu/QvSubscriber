using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QlikView.Common
{
    public class Message:ReportItemBase
    {
        private string _from;
        public string From
        {
            get
            {
                return _from;
            }
            set
            {
                _from = value;
                this.OnPropertyChanged(() => this.From);
            }
        }

        private string _cc;
        public string CC
        {
            get
            {
                return _cc;
            }
            set
            {
                _cc = value;
                this.OnPropertyChanged(() => this.CC);
            }
        }

        private string _bcc;
        public string BCC
        {
            get
            {
                return _bcc;
            }
            set
            {
                _bcc = value;
                this.OnPropertyChanged(() => this.BCC);
            }
        }

        private string _subject;
        public string Subject
        {
            get
            {
                return _subject;
            }
            set
            {
                _subject = value;
                this.OnPropertyChanged(() => this.Subject);
            }
        }

        private string _body;
        public string Body
        {
            get
            {
                return _body;
            }
            set
            {
                _body = value;
                this.OnPropertyChanged(() => this.Body);
            }
        }

        public override string ToString()
        {
            return this.Subject;
        }
    }
}
