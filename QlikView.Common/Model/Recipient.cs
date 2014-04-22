using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QlikView.Common
{
    public class Recipient : ReportItemBase
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

        private string _email;
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                this.OnPropertyChanged(() => this.Email);
            }
        }

        private string _emailCC;
        public string EmailCC
        {
            get
            {
                return _emailCC;
            }
            set
            {
                _emailCC = value;
                this.OnPropertyChanged(() => this.EmailCC);
            }
        }
    }
}
