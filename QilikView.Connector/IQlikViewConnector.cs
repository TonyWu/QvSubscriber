using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QlikView.Common;

namespace QlikView.Connector
{
    public interface IQlikViewConnector
    {
        void SetConnection(ReportConnection connection);
        bool VerifyConnection();
        void OpenConnector();
        void SetFilter(Filter filter);
        List<string> GetFieldList();
        QVField GetQVFieldByName(string name);
        int ExportExcel(string objectId, string fileName);
        int ExportHtml(string objectId, string fileName);
        int ExportJPG(string objectId, string fileName);
        List<string> GetExportDocumentIdList();
        ReportItemDictionary<QVField> GetCurrentSelection();
        void SetSelections(ReportItemDictionary<QVField> qvFieldCollection);

        void Clear();
        void ClearBackForward();

        void Preview(ReportConnection connection);
    }
}
