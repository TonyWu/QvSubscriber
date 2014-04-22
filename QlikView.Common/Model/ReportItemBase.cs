using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

namespace QlikView.Common
{
    public class ReportItemBase:IReportItem,INotifyPropertyChanged
    {
        private string _name;
        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
                this.OnPropertyChanged(()=>this.Name);
            }
        }

        //private IReportItem _parent;
        public ReportItemDictionary<IReportItem> Parents
        {
            get;
            set;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(Expression<Func<object>> property)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this,new PropertyChangedEventArgs(BindingHelper.Name(property)));
            }
        }

        public ReportItemBase()
        {
            this.Parents = new ReportItemDictionary<IReportItem>();
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
