using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QlikView.Common;
using Microsoft.Practices.Prism.Commands;
using System.Windows;

namespace QlikView.Configuration.WpfClient.ViewModels
{
    public class FilterViewModel:ViewModelBase
    {
        //public IEnumerable<Recipient> MyProperty { get; set; }
        public IEnumerable<ReportConnection> ConnectionList { get; set; }

        public DelegateCommand<QvVariable> VariableAddCommand { get; private set; }
        public DelegateCommand<QvVariable> VariableDeleteCommand { get; private set; }
        public DelegateCommand<QVField> FieldDeleteCommand { get; private set; }
        public DelegateCommand FieldAddCommand { get; private set; }

        private QvVariable _newVariable;
        public QvVariable NewVariable
        {
            get
            {
                return this._newVariable;
            }
            set
            {
                this._newVariable = value;
                this.OnPropertyChanged(() => NewVariable);
            }
        }

        public FilterViewModel()
        {
            this.ConnectionList = ReportConfig.ConnectionManager.ItemCollection.Values.ToList();
            this.VariableAddCommand = new DelegateCommand<QvVariable>(this.VariableAdd);
            this.VariableDeleteCommand = new DelegateCommand<QvVariable>(this.VariableDelete);
            this.FieldDeleteCommand = new DelegateCommand<QVField>(this.FieldDelete);
            this.FieldAddCommand = new DelegateCommand(this.FieldAdd);

            this.NewVariable = new QvVariable();
        }

        protected override bool OnQvItemAdd(IReportItem qvItem)
        {
            try
            {
                Filter filter = qvItem as Filter;

                if (string.IsNullOrWhiteSpace(filter.Name) || filter.Connection == null)
                {
                    MessageBox.Show("Name and connection are required.");
                    return false;
                }


                ReportConfig.FilterManager.AddItem(qvItem);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }

        protected void VariableAdd(QvVariable variable)
        {
            if(string.IsNullOrWhiteSpace(variable.Name))
            {
                 MessageBox.Show("The name can not be empty.");
                 return;
            }

            if (string.IsNullOrWhiteSpace(variable.Value) && string.IsNullOrWhiteSpace(variable.Expression))
            {
                MessageBox.Show("Value or Expression can not be empty.");
                return;
            }

            Filter filter = this.ReportItem as Filter;

            if (filter.Variables.ContainsKey(variable.Name))
            {
                MessageBox.Show("The same name variable exists, cannot add.");
                return;
            }

            filter.Variables.Add(variable.Name, variable);

            this.NewVariable = new QvVariable();
        }

        protected void VariableDelete(QvVariable variable)
        {
            if (variable == null)
            {
                MessageBox.Show("Please select an variable.");
                return;
            }
            if (MessageBox.Show("Do you want to delete the variable " + variable.Name, "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Filter filter = this.ReportItem as Filter;
                filter.Variables.Remove(variable.Name);
            }
        }

        protected void FieldDelete(QVField field)
        {
            if (field == null)
            {
                MessageBox.Show("Please select an field.");
                return;
            }
            if (MessageBox.Show("Do you want to delete the field " + field.Name, "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Filter filter = this.ReportItem as Filter;
                filter.Fields.Remove(field.Name);
            }
        }

        protected void FieldAdd()
        {
            IFieldSelection selection = new FieldSelectionForm();
            selection.Connection = (this.ReportItem as Filter).Connection;

            selection.FieldAdded += new Action<QVField>(selection_FieldAdded);

            selection.Open();
        }

        void selection_FieldAdded(QVField obj)
        {
            Filter filter = this.ReportItem as Filter;

            if (filter.Fields.ContainsKey(obj.Name))
            {
                MessageBox.Show("The same name field exists, cannot add.");
                return;
            }

            filter.Fields.Add(obj.Name, obj);
        }
    }
}
