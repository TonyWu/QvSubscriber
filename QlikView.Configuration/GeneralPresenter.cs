using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QlikView.Common;

namespace QlikView.Configuration
{
    public class GeneralPresenter<T>:IBasePresenter<T> where T : IReportItem
    {
        #region IBasePresenter<T> Members

        public IMainView MainView
        {
            get;
            set;
        }

        public IBaseView<T> View
        {
            get;
            set;
        }

        public Common.ReportConfigManagerBase<T> ConfigManager
        {
            get;
            set;
        }

        public void Run()
        {
            View.ItemAdded += new Action<T>(View_ItemAdded);
            View.ItemUpdated += new Action<T>(View_ItemUpdated);
            View.Open();
        }

        void View_ItemUpdated(T obj)
        {
            this.ConfigManager.Save();
            this.MainView.RefreshUI();
        }

        void View_ItemAdded(T obj)
        {
            this.ConfigManager.AddItem(obj);
            this.MainView.RefreshUI();
        }

        #endregion
    }
}
