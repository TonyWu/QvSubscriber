using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AxQlikOCXLib;
using QMSClient.ServiceSupport;
using QlikView;
using System.Runtime.CompilerServices;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using QlikView.Common;

namespace QlikView.Connector
{
    public partial class QlikViewConnector : Form,IQlikViewConnector
    {
        IQlikViewConnector _qlikViewConnector;

        public QlikViewConnector()
        {
            InitializeComponent();
            _qlikViewConnector = this.qvConnectorComponent1;
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

        public void Clear()
        {
            this._qlikViewConnector.Clear();
        }

        public void ClearBackForward()
        {
            this._qlikViewConnector.ClearBackForward();
        }
     
        #endregion
        

        #region "Test Code"

        void xl_WorkbookOpen(Excel.Workbook Wb)
        {
            //if (File.Exists(this._exportFileName))
            //    File.Delete(this._exportFileName);

            //string folder = this._exportFileName.Remove(this._exportFileName.LastIndexOf(@"\"));
            //if (!Directory.Exists(folder))
            //    Directory.CreateDirectory(folder);

            //Wb.SaveAs(this._exportFileName);
            //Wb.Close();
            //xl.Quit();

            //if (this.ExcelFileExported != null)
            //    this.ExcelFileExported(this._exportFileName);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.SetConnection(new QlikView.Common.ReportConnection()
            {
                ServerName = "usb-etbiapp",
                IsLocal = false,
                User = "tony.wu",
                Password = "Help$123",
                QlikViewDocument = "b2c/etag_marketing.qvw"
            });

            this.OpenConnector();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<string> list = this.GetFieldList();

            QVField field = this.GetQVFieldByName("Visits");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //try
            //{

            //    var fields = this._doc.GetFieldDescriptions();

            //    for (int i = 0; i < fields.Count; i++)
            //    {
            //        string fName = fields[i].Name;
            //        bool fDescription = fields[i].IsNumeric;
            //    }

            //    var sheet = this._doc.Sheets("0");


            //    //objects
            //    var objects = sheet.GetSheetObjects();
            //    foreach (var item in objects)
            //    {
            //        string id = item.GetObjectId;
            //        short type = item.GetObjectType();

            //        if (type == 10)
            //        {
            //            var obj = this._doc.GetSheetObject(id);

            //            //var field = this._doc.Fields("id");
            //            //field.SelectAll();

            //            //var all = field.GetSelectedValues();
            //            //int count = all.Count;

            //            //field.Clear();

            //            //MessageBox.Show(count.ToString());
            //            //var f1 = field.GetNoValues();
            //            //f1.Add();
            //            //f1[0].Number = 1;
            //            //f1[0].IsNumeric = true;
            //            //field.SelectValues(f1);             

            //            obj.SendToExcel();

            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}

        }

        #endregion


        public void Preview(ReportConnection connection)
        {
            this.SetConnection(connection);
            this.OpenConnector();

            this.Show();
            System.Threading.Thread.Sleep(10000);
            this.Hide();
        }
    }
}
