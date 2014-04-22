using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QlikView.Common
{
    public class RecipientGroup : ReportItemBase
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

        public ReportItemDictionary<Recipient> RecipientList { get; set; }

        public RecipientGroup()
        {
            this.RecipientList = new ReportItemDictionary<Recipient>();
        }
    }
}
