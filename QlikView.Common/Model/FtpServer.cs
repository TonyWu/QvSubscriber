using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QlikView.Common
{
    public class FtpServer : ReportItemBase
    {
        private string _host;
        public string Host
        {
            get
            {
                return _host;
            }
            set
            {
                _host = value;
                this.OnPropertyChanged(() => this.Host);
            }
        }

        private string _username;
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
                this.OnPropertyChanged(() => this.Username);
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

        private string _folder;
        public string Folder
        {
            get
            {
                return _folder;
            }
            set
            {
                _folder = value;
                this.OnPropertyChanged(() => this.Folder);
            }
        }

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

        private string _port;
        public string Port
        {
            get
            {
                return _port;
            }
            set
            {
                _port = value;
                this.OnPropertyChanged(() => this.Port);
            }
        }
    }
}
