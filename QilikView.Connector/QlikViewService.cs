using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QMSClient.ServiceSupport;
using QlikView.Connector.QMSAPI;

namespace QlikView.Connector
{
    public class QlikViewService:IService
    {
        QMSAPI.QMSBackendClient _client;
        ServiceInfo _serviceInfo;

        public QlikViewService(string server, string port)
        {
            _client = new QMSAPI.QMSBackendClient();
            _client.Endpoint.Address = new System.ServiceModel.EndpointAddress(string.Format("http://{0}:{1}/QMS/Service", server,port));
            ServiceKeyClientMessageInspector.ServiceKey = _client.GetTimeLimitedServiceKey();
            List<ServiceInfo> MyQVSInfo = _client.GetServices(ServiceTypes.QlikViewServer);
            this._serviceInfo = MyQVSInfo.First();
        }

        public QlikViewService()
        {
            _client = new QMSAPI.QMSBackendClient();
            ServiceKeyClientMessageInspector.ServiceKey = _client.GetTimeLimitedServiceKey();
            List<ServiceInfo> MyQVSInfo = _client.GetServices(ServiceTypes.QlikViewServer);
            this._serviceInfo = MyQVSInfo.First();
        }

        #region IService Members

        public List<string> GetDocumentList()
        {
            List<string> docList = new List<string>();
            List<DocumentNode> list = _client.GetUserDocuments(this._serviceInfo.ID);

            foreach (var item in list)
            {
                docList.Add(string.Format("{0}/{1}", item.RelativePath, item.Name));
            }

            return docList;
        }

        #endregion

        private bool PingService()
        {
            try
            {
                _client.Ping();
            }
            catch (Exception ex)
            {
                if (ex.Message.ToString() == "Service key has expired")
                    ServiceKeyClientMessageInspector.ServiceKey = _client.GetTimeLimitedServiceKey();
                else
                    return false;
            }

            return true;
        }
    }
}
