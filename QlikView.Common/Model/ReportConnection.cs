using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QlikView.Common
{
    public class ReportConnection : ReportItemBase
    {
        private string _description;
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                this.OnPropertyChanged(() => this.Description);
            }
        }

        private bool _isLocal;
        public bool IsLocal
        {
            get
            {
                return _isLocal;
            }
            set
            {
                _isLocal = value;
                _isServer = !value;
                this.OnPropertyChanged(() => this.IsLocal);
                this.OnPropertyChanged(() => this.IsServer);
            }
        }

        private bool _isServer;
        public bool IsServer
        {
            get
            {
                return _isServer;
            }
        }

        private string _serverName;
        public string ServerName
        {
            get
            {
                return _serverName;
            }
            set
            {
                _serverName = value;
                this.OnPropertyChanged(() => this.ServerName);
            }
        }

        private string _serivceHost;
        public string ServiceHost
        {
            get
            {
                return _serivceHost;
            }
            set
            {
                _serivceHost = value;
                this.OnPropertyChanged(() => this.ServiceHost);
            }
        }

        private string _servicePort;
        public string ServicePort
        {
            get
            {
                return _servicePort;
            }
            set
            {
                _servicePort = value;
                this.OnPropertyChanged(() => this.ServicePort);
            }
        }

        private string _qlikViewDocument;
        public string QlikViewDocument
        {
            get
            {
                return _qlikViewDocument;
            }
            set
            {
                _qlikViewDocument = value;
                this.OnPropertyChanged(() => this.QlikViewDocument);
            }
        }

        private string _user;
        public string User
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
                this.OnPropertyChanged(() => this.User);
            }
        }

        private string _password;
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                this.OnPropertyChanged(() => this.Password);
            }
        }
    }
}
