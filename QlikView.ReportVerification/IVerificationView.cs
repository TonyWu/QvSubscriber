using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QlikView.ReportVerification
{
    public interface IVerificationView
    {
        ReportVerificationModel Model { get; set; }

        void RefreshUI();
    }
}
