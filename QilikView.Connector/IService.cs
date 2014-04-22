using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QlikView.Connector
{
    public interface IService
    {
        List<string> GetDocumentList();
    }
}
