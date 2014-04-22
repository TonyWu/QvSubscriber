using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Linq.Expressions;
using QlikView.Common;
using Microsoft.Practices.Prism.Commands;

namespace QlikView.Configuration.WpfClient.ViewModels
{
    public abstract class ViewModelBase : IViewModel, INotifyPropertyChanged
    {
        private bool _isNew;
        public bool IsNew
        {
            get
            {
                return this._isNew;
            }
            set
            {
                this._isNew = value;
                this.OnPropertyChanged(() => this.IsNew);
            }
        }

        private IReportItem _reportItem;
        public Common.IReportItem ReportItem
        {
            get
            {
                return this._reportItem;
            }
            set
            {
                this._reportItem = value;
                this.OnPropertyChanged(() => this.ReportItem);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public event Action<IReportItem> QvItemAdded;
        public event EventHandler QvItemAddCanceled;

        public DelegateCommand<IReportItem> QvItemAddCommand { get; protected set; }
        public DelegateCommand QvItemAddCancelCommand { get; protected set; }
        protected abstract bool OnQvItemAdd(IReportItem qvItem);

        public ViewModelBase()
        {
            this.QvItemAddCommand = new DelegateCommand<IReportItem>(this.QvItemAdd);
            this.QvItemAddCancelCommand = new DelegateCommand(this.QvItemAddCancel);
        }

        public virtual void Initialize()
        {

        }

        private void QvItemAddCancel()
        {
            if (this.QvItemAddCanceled != null)
                this.QvItemAddCanceled(this, new EventArgs());
        }

        private void QvItemAdd(IReportItem qvItem)
        {
            bool ok = this.OnQvItemAdd(qvItem);

            if (ok && this.QvItemAdded != null)
                this.QvItemAdded(qvItem);
        }

        protected void OnPropertyChanged(Expression<Func<object>> property)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(BindingHelper.Name(property)));
            }
        }
    }
}
