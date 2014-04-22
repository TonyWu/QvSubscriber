using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QlikView.Common
{
    public class QvError:IError
    {
        #region IError Members

        public bool HasError
        {
            get;
            set;
        }

        public StringBuilder ErrorMessage
        {
            get;
            set;
        }

        #endregion

        public QvError()
        {
            this.ErrorMessage = new StringBuilder();
        }
    }
}
