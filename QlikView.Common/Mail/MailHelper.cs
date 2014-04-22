using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QlikView.Common
{
    public class MailHelper
    {
        public static void ExceptionNotify(string subject, string errorMessage, SmtpServer smtpServer)
        {
            Message message = new Message();
            message.From = string.IsNullOrEmpty(smtpServer.ExceptionFrom) ? "QlikViewException@ef.com" : smtpServer.ExceptionFrom;
            message.Subject = subject;
            message.Body = errorMessage;
            SmtpClientAdaptor smtpClient = new SmtpClientAdaptor(smtpServer);
            smtpClient.SendEmail(message, smtpServer.ExceptionTo.Split(';').ToList());
        }

        public static void SmallPic(string strOldPic, string strNewPic, int intWidth)
        {
            System.Drawing.Bitmap objPic, objNewPic;
            try
            {
                objPic = new System.Drawing.Bitmap(strOldPic);
                int intHeight = (intWidth / objPic.Width) * objPic.Height;
                objNewPic = new System.Drawing.Bitmap(objPic, intWidth, intHeight);
                objNewPic.Save(strNewPic);
            }
            catch (Exception exp) { throw exp; }
            finally
            {
                objPic = null;
                objNewPic = null;
            }
        }
    }
}
