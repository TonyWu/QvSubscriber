using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QlikView.Common
{
    public class QvVariable : ReportItemBase
    {
        private string _value;
        public string Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                this.OnPropertyChanged(() => this.Value);
            }
        }

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
    }
}
