using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QlikView.Common;
using System.IO;
using System.Threading.Tasks;

namespace QlikView.Connector
{
    public class ExportEngine
    {
        private IQlikViewConnector _qlikViewConnector;

        public ILog Logger { get; set; }

        public ExportEngine(IQlikViewConnector connector)
        {
            this._qlikViewConnector = connector;
        }

        public IError RunTask(ReportTask task, SmtpServer smtpServer)
        {
            IError taskError = new QvError();
            try
            {

                this.Logger.Message("Begin to Run task [" + task.Name + "]------------------------------------------------");

                this.Logger.Message("Preview the connection. ");

                this.ConnectionPreview(task.Reports.Values.First());

                this.Logger.Message(string.Format("Task [{0}] have reports [{1}]", task.Name, string.Join(",", task.Reports.Keys.ToArray())));

                List<ReportContext> reports;
                List<string> missingExportReports;

                IError error = this.PopulateReportContext(task, smtpServer, out reports, out missingExportReports);

                if (error.HasError)
                {
                    taskError.HasError = true;
                    taskError.ErrorMessage.AppendLine(error.ErrorMessage.ToString());
                }
                else
                {
                    #region Send Mail

                    if (smtpServer.Validate() && reports.Count > 0)
                    {
                        SmtpClientAdaptor smtpClient = new SmtpClientAdaptor(smtpServer);

                        //Get the recipients
                        List<string> recipients = task.Recipients.Values.Select(x => x.Email).ToList();
                        if (task.Group != null)
                        {
                            foreach (var r in task.Group.RecipientList.Values)
                            {
                                if (!recipients.Contains(r.Email))
                                    recipients.Add(r.Email);
                            }
                        }

                        if (task.IsSendMailInSingleMail)
                        {
                            smtpClient.SendEmail(task.MessageDefinition, reports, recipients);
                        }
                        else
                        {
                            foreach (var item in reports)
                            {
                                List<ReportContext> list = new List<ReportContext>();
                                list.Add(item);
                                smtpClient.SendEmail(task.MessageDefinition, list, recipients);
                            }
                        }
                    }
                    else
                    {
                        this.Logger.Error("SMTP SERVER Validate failed or there is no report exported, can not send email.");
                        this.Logger.Info("Exported reports count " + reports.Count);
                    }

                    #endregion

                    if (missingExportReports.Count > 0)
                    {
                        string subject = "Some reports can not be exported";
                        string message = string.Join("\n\r", missingExportReports);
                        MailHelper.ExceptionNotify(subject, message, smtpServer);
                        taskError.HasError = true;
                        taskError.ErrorMessage.AppendLine(message);
                    }
                }
            }
            catch (Exception ex)
            {
                this.Logger.Error("Failed to run the task [" + task.Name + "] Error:" + ex.Message + "\n"+ ex.StackTrace);

                try
                {
                    if (!string.IsNullOrEmpty(smtpServer.ExceptionTo))
                    {
                        string subject = "QlikView Report Export&MailSend system Exception";
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine("Task name: " + task.Name);
                        sb.AppendLine(string.Empty);
                        sb.AppendLine("Exception message: " + ex.Message);
                        sb.AppendLine("Stack Trace---------------");
                        sb.AppendLine(ex.StackTrace);

                        MailHelper.ExceptionNotify(subject, sb.ToString(), smtpServer);
                        taskError.HasError = true;
                        taskError.ErrorMessage.AppendLine(sb.ToString());
                    }
                }
                catch (Exception ex1)
                {
                    this.Logger.Error("Send exception mail failed. " + ex.Message + "\n" + ex1.StackTrace);
                }
            }

            this.Logger.Message("Complete running the task----------------------------------------------------------");

            return taskError;
        }

        public bool RunReport(QlikViewReport report, SmtpServer smtpServer)
        {
            string outputFile;
            var error = this.ExportReport(report, "ReportRunTest", out outputFile);

            if (error.HasError)
                return false;

            return true;
        }

        private IError PopulateReportContext(ReportTask task, SmtpServer smtpServer, out List<ReportContext> exportedReports, out List<string> missingReports)
        {
            exportedReports = new List<ReportContext>();
            missingReports = new List<string>();

            List<ReportContext> excelReports = new List<ReportContext>();

            string folder = string.Empty;
            if (task.OutputFolder.EndsWith(@"\"))
            {
                folder = task.OutputFolder.Remove(task.OutputFolder.Length - 1);
            }
            else
            {
                folder = task.OutputFolder;
            }
            IError error = new QvError();

            #region Export report
            foreach (var item in task.Reports.Values)
            {
                this.Logger.Message("Running report [" + item.Name + "]");
                this.Logger.Message("Setting connection [" + item.Connection.Name + "]");

                string fileName;

                error = this.ExportReport(item, folder, out fileName);
                
                if (error.HasError == false)
                {
                    if (item.ReportType != Common.ReportType.Excel)
                        exportedReports.Add(new ReportContext()
                        {
                            Name = item.Name,
                            Description = item.Description,
                            OutputFullName = fileName,
                            ReportType = item.ReportType,
                            HtmlFormat = this.GetHtml(item, fileName)
                        });
                    else
                        excelReports.Add(new ReportContext()
                        {
                            Name = item.Name,
                            Description = item.Description,
                            OutputFullName = fileName,
                            ReportType = item.ReportType,
                            HtmlFormat = this.GetHtml(item, fileName)
                        });
                    this.Logger.Message("Export report [" + item.Name + "] complete");
                }
                else
                {
                    missingReports.Add(fileName);
                    this.Logger.Error(string.Format("The report [{0}] cannot be exported.", fileName));
                }
            }

            if (task.IsMergeInSingleExcel)
            {
                this.Logger.Message("Begin Merge in single excel");
                ReportContext mergedReport;
                string mergedFileName = folder + @"\" + task.Name + ".xls";

                error = this.MergeExcelReports(excelReports, mergedFileName, task.Name, out mergedReport);

                if (error.HasError == false)
                {
                    exportedReports.Add(mergedReport);
                    this.Logger.Message("Succed to merge in single excel");
                }
                else
                {
                    this.Logger.Message("Failed to merge, retain multiple files \n" + error.ErrorMessage);
                    MailHelper.ExceptionNotify("Failed to merge files for task " + task.Name, error.ErrorMessage.ToString(), smtpServer);
                }
            }
            else
            {
                exportedReports.AddRange(excelReports);
            }
            #endregion

            return error;
        }

        private IError MergeExcelReports(List<ReportContext> excelReports, string mergedFileName, string taskName, out ReportContext mergedReport)
        {
            IError error = new QvError();
            mergedReport = new ReportContext();
            try
            {
                Dictionary<string, ReportContext> mergedFiles = excelReports.ToDictionary(x => x.Name, x => x);

                //Create a thread to execute merge file
                string outputFile = string.Empty;
                Func<bool> function = () =>
                {
                    IExcelMerge excel = ExcelMergeFactory.Create(taskName); 
                    excel.Logger = this.Logger;
                    return excel.MergeFiles(mergedFiles, mergedFileName, out outputFile);
                };

                Task<bool> task = new Task<bool>(function);
                task.Start();
                task.Wait();

                mergedReport.Name = "mergedReport";
                mergedReport.OutputFullName = outputFile;
                mergedReport.ReportType = Common.ReportType.Excel;
                mergedReport.HtmlFormat = string.Empty;
                
                if (task.Result)
                    error.HasError = false;
                else
                    error.HasError = true;

                
            }
            catch (Exception ex)
            {
                error.HasError = true;
                error.ErrorMessage = new StringBuilder(ex.Message + "\n" + ex.StackTrace);
                this.Logger.Error(ex.Message);
                this.Logger.Error(ex.StackTrace);
            }

            return error;
        }

        private IError ExportReport(QlikViewReport report, string outputFolder, out string outputFile)
        {
            IError error = new QvError();
            outputFile = string.Empty;

            this._qlikViewConnector.SetConnection(report.Connection);

            this.Logger.Message("Verify the connection....");
            bool verified = this._qlikViewConnector.VerifyConnection();

            if (verified)
            {
                if (report.Filter != null)
                    this.Logger.Message("Setting Filter: [" + report.Filter.Name + "] Fields:[" + string.Join(",", report.Filter.Fields.Keys.ToArray()) + "]");
                this._qlikViewConnector.SetFilter(report.Filter);

                if (report.EnableDynamicNaming)
                {
                    outputFile = outputFolder + @"\" + report.OutputFielName.Replace(".", "_" + DateTime.Now.ToString("yyyMMdd_HHmmss") + ".");
                }
                else
                {
                    outputFile = outputFolder + @"\" + report.OutputFielName;
                }

                this.Logger.Message("Export filename: " + outputFile);

                //export file
                int returnCode = -1;
                if (report.ReportType == Common.ReportType.Excel)
                    returnCode = this._qlikViewConnector.ExportExcel(report.QlikViewExportObjectId, outputFile);
                else if (report.ReportType == Common.ReportType.Html)
                    returnCode = this._qlikViewConnector.ExportHtml(report.QlikViewExportObjectId, outputFile);
                else if (report.ReportType == Common.ReportType.JPG)
                    returnCode = this._qlikViewConnector.ExportJPG(report.QlikViewExportObjectId, outputFile);

                this.Logger.Message(string.Format("Export return code [{0}]", returnCode));

                //Check the file if exported
                if (!File.Exists(outputFile))
                {
                    //export again
                    this.Logger.Error(string.Format("The file {0} is not exported at the first time.", outputFile));
                }

                if (returnCode > -1)
                    error.HasError = false;
                else
                    error.HasError = true;
            }
            else
            {
                this.Logger.Error(string.Format("Veriy the connection {0} failed. Can not open the document. The report can not be exported.", report.Name));
                error.HasError = true;
            }

            return error;
        }

        private string GetHtml(QlikViewReport report, string fileName)
        {
            string html = string.Empty;

            if (report.ReportType == Common.ReportType.Excel && report.IsEmbeddedInMail)
            {
                string fileName1 = fileName.Replace(".xls", ".html").Replace(".jpg", ".html");
                this._qlikViewConnector.ExportHtml(report.QlikViewExportObjectId, fileName1);
                using (StreamReader sr = new StreamReader(fileName1))
                {
                    html = sr.ReadToEnd();
                }
            }
            else if(report.ReportType == Common.ReportType.JPG)
            {
                html = string.Format("<img id=\"{0}\" src=\"cid:{1}\">","Picture_" + report.Name + "_" + DateTime.Today.ToString("yyyMMdd"), report.Name +"_" + DateTime.Today.ToString("yyyMMdd"));
            }

            return html;
        }       

        private void ConnectionPreview(QlikViewReport report)
        {
            if (report != null)
                this._qlikViewConnector.Preview(report.Connection);
        }
    }
}
