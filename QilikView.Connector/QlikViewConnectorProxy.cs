using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QlikView.Common;

namespace QlikView.Connector
{
    public class QlikViewConnectorProxy:IQlikViewConnector
    {
        private static QlikViewConnectorProxy instance = null;
        static readonly object padlock = new object();
        IQlikViewConnector _qlikViewConnector;

        QlikViewConnectorProxy()
        {
            this._qlikViewConnector = new QlikViewConnector();
        }

        public static QlikViewConnectorProxy Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new QlikViewConnectorProxy();
                        }
                    }
                }
                return instance;
            }
        }

        #region IQlikViewConnector Members

        public void SetConnection(Common.ReportConnection connection)
        {
            this._qlikViewConnector.SetConnection(connection);
        }

        public bool VerifyConnection()
        {
            return this._qlikViewConnector.VerifyConnection();
        }

        public void OpenConnector()
        {
            this._qlikViewConnector.OpenConnector();
        }

        public void SetFilter(Common.Filter filter)
        {
            this._qlikViewConnector.SetFilter(filter);
        }

        public List<string> GetFieldList()
        {
            return this._qlikViewConnector.GetFieldList();
        }

        public Common.QVField GetQVFieldByName(string name)
        {
            return this._qlikViewConnector.GetQVFieldByName(name);
        }

        public Common.ReportItemDictionary<QVField> GetCurrentSelection()
        {
            return this._qlikViewConnector.GetCurrentSelection();
        }

        public void SetSelections(Common.ReportItemDictionary<QVField> qvFieldCollection)
        {
            this._qlikViewConnector.SetSelections(qvFieldCollection);
        }

        public void Clear()
        {
            this._qlikViewConnector.Clear();
        }

        public void ClearBackForward()
        {
            this._qlikViewConnector.ClearBackForward();
        }

        public int ExportExcel(string objectId, string fileName)
        {
            return this._qlikViewConnector.ExportExcel(objectId, fileName);
        }

        public int ExportHtml(string objectId, string fileName)
        {
            return this._qlikViewConnector.ExportHtml(objectId, fileName);
        }

        public int ExportJPG(string objectId, string fileName)
        {
            return this._qlikViewConnector.ExportJPG(objectId, fileName);
        }

        public List<string> GetExportDocumentIdList()
        {
            return this._qlikViewConnector.GetExportDocumentIdList();
        }

        #endregion



        public void Preview(ReportConnection connection)
        {
            this._qlikViewConnector.Preview(connection);
        }


        public int ExportCSV(string objectId, string fileName)
        {
            return this._qlikViewConnector.ExportCSV(objectId, fileName);
        }
    }
}
