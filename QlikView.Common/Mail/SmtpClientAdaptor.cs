using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace QlikView.Common
{
    public class SmtpClientAdaptor
    {
        private SmtpServer _smtpServer;

        public SmtpClientAdaptor(SmtpServer smtpServer)
        {
            this._smtpServer = smtpServer;
        }

        public void SendEmail(Message message, List<ReportContext> reports, List<string> recipients)
        {
            SmtpClient client = null;
            try
            {
                client = new SmtpClient(this._smtpServer.Server);
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(this._smtpServer.User, this._smtpServer.Password);

                if (this._smtpServer.UsePickupDirectoryLocation)
                {
                    client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    client.PickupDirectoryLocation = this._smtpServer.PickupDirectoryLocation;
                }
                else
                {
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                }

                MailMessage mMessage = new MailMessage();
                mMessage.From = new MailAddress(message.From);

                foreach (var item in recipients)
                {
                    mMessage.To.Add(item);
                }

                mMessage.Subject = message.Subject.Replace("@Date",DateTime.Today.ToString("yyyy-MM-dd"));
                if (!string.IsNullOrWhiteSpace(message.CC))
                {
                    foreach (var item in message.CC.Split(";".ToArray()))
                    {
                        mMessage.CC.Add(item);
                    }
                }

                if (!string.IsNullOrWhiteSpace(message.BCC))
                {
                    foreach (var item in message.BCC.Split(";".ToArray()))
                    {
                        mMessage.Bcc.Add(item);
                    }
                }

                string Body = this.GetBodyFromTemplate(message.Body, reports);
                mMessage.IsBodyHtml = true;
                AlternateView htmlBody = AlternateView.CreateAlternateViewFromString(Body, null, "text/html");
                foreach (var item in reports)
                {
                    if (item.ReportType == ReportType.JPG)
                    {
                        LinkedResource lrImage = new LinkedResource(item.OutputFullName, "image/gif");
                        lrImage.ContentId = item.Name + "_" + DateTime.Today.ToString("yyyMMdd");
                        htmlBody.LinkedResources.Add(lrImage);
                    }
                    else
                    {
                        mMessage.Attachments.Add(new Attachment(item.OutputFullName));
                    }
                }
                mMessage.AlternateViews.Add(htmlBody);

                client.Send(mMessage);

                mMessage.Dispose();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (client != null)
                {
                    client.Dispose();
                    client = null;
                }
            }
        }

        private string GetBodyFromTemplate(string template, List<ReportContext> reports)
        {
            if (template.Contains("@Template"))
            {
                template = template.Replace("@Template", "");
                template = template.Replace("@Date", DateTime.Today.ToString("yyyy-MM-dd"));
                foreach (var item in reports)
                {
                    template = template.Replace("{" + item.Name + "}", item.HtmlFormat);
                }
            }
            else
            {
                foreach (var item in reports)
                {
                    template = template + "<br/><br/>" + item.HtmlFormat;
                }
            }

            return template;
        }
              
     
        public void SendEmail(Message message, List<string> recipients)
        {
            try
            {
                SmtpClient client = new SmtpClient(this._smtpServer.Server);
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(this._smtpServer.User, this._smtpServer.Password);
                
                if (this._smtpServer.UsePickupDirectoryLocation)
                {
                    client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    client.PickupDirectoryLocation = this._smtpServer.PickupDirectoryLocation;
                }
                else
                {
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                }

                MailMessage mMessage = new MailMessage();
                mMessage.From = new MailAddress(message.From);

                foreach (var item in recipients)
                {
                    mMessage.To.Add(item);
                }

                mMessage.Subject = message.Subject;
                if (!string.IsNullOrWhiteSpace(message.CC))
                {
                    foreach (var item in message.CC.Split(";".ToArray()))
                    {
                        mMessage.CC.Add(item);
                    }
                }

                if (!string.IsNullOrWhiteSpace(message.BCC))
                {
                    foreach (var item in message.BCC.Split(";".ToArray()))
                    {
                        mMessage.Bcc.Add(item);
                    }
                }

                mMessage.Body = message.Body ?? string.Empty;

                mMessage.IsBodyHtml = true;

                client.Send(mMessage);

                mMessage.Dispose();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void SendEmail(Message message, string jpgFile, List<string> recipients)
        {
            try
            {
                SmtpClient client = new SmtpClient(this._smtpServer.Server);
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(this._smtpServer.User, this._smtpServer.Password);

                if (this._smtpServer.UsePickupDirectoryLocation)
                {
                    client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    client.PickupDirectoryLocation = this._smtpServer.PickupDirectoryLocation;
                }
                else
                {
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                }

                MailMessage mMessage = new MailMessage();
                mMessage.From = new MailAddress(message.From);

                foreach (var item in recipients)
                {
                    mMessage.To.Add(item);
                }

                mMessage.Subject = message.Subject;
                if (!string.IsNullOrWhiteSpace(message.CC))
                {
                    foreach (var item in message.CC.Split(";".ToArray()))
                    {
                        mMessage.CC.Add(item);
                    }
                }

                if (!string.IsNullOrWhiteSpace(message.BCC))
                {
                    foreach (var item in message.BCC.Split(";".ToArray()))
                    {
                        mMessage.Bcc.Add(item);
                    }
                }

                LinkedResource lrImage = new LinkedResource(jpgFile, "image/gif");
                lrImage.ContentId = "qlikViewReport";
                string Body = message.Body + "<br/><br/><img src=\"cid:qlikViewReport\">";
                AlternateView htmlBody = AlternateView.CreateAlternateViewFromString(Body, null, "text/html");
                htmlBody.LinkedResources.Add(lrImage);
                mMessage.AlternateViews.Add(htmlBody);
                mMessage.IsBodyHtml = true;
                mMessage.Attachments.Add(new Attachment(jpgFile));

                client.Send(mMessage);

                mMessage.Dispose();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
