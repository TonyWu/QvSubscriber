using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace QlikView.Common
{
    public class QVField : ReportItemBase
    {
        public ObservableCollection<FieldValue> Values { get; set; }

        private string _expression;
        public string Expression
        {
            get
            {
                return _expression;
            }
            set
            {
                _expression = value;
                this.OnPropertyChanged(() => this.Expression);
                _hasExpression = !string.IsNullOrWhiteSpace(this.Expression);
                this.OnPropertyChanged(() => this.HasExpression);
            }
        }

        private bool _hasExpression;
        public bool HasExpression
        {
            get
            {
                return _hasExpression;
            }
        }

        public override string ToString()
        {
            var qeury = from q in Values
                    select q.Value;

            if (qeury != null && qeury.Count() > 0)
                return string.Join(",", qeury.ToArray());
            else
                return string.Empty;
        }

        public QVField()
        {
            this.Values = new ObservableCollection<FieldValue>();
        }
    }
}
