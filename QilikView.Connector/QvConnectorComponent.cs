using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QlikView.Common;
using System.Collections.ObjectModel;

namespace QlikView.Connector
{
    public partial class QvConnectorComponent : UserControl,IQlikViewConnector
    {
        private static ReportConnection CurrentConnection;

        private Doc _doc;
        private Filter _filter;

        private string _exportFileName = string.Empty;

        public QvConnectorComponent()
        {
            InitializeComponent();
        }

        #region IQlikviewConnector Members

        public void SetConnection(QlikView.Common.ReportConnection connection)
        {
            if (CurrentConnection == null || CurrentConnection.Name != connection.Name || this._doc == null)
            {
                if (connection.IsLocal == false)
                {
                    this.axQlikOCX1.UserName = connection.User;
                    this.axQlikOCX1.Password = connection.Password;
                    string doc = string.Format("qvp://{0}/{1}", connection.ServerName, connection.QlikViewDocument);
                    this._doc = this.axQlikOCX1.OpenDocument(doc);
                }
                else
                {
                    this._doc = this.axQlikOCX1.OpenDocument(connection.QlikViewDocument);
                }

                CurrentConnection = connection;
            }
        }

        public bool VerifyConnection()
        {
            return this._doc != null;
        }

        public void OpenConnector()
        {
            this.Show();
            this._doc.Activate();
        }

        public void SetFilter(QlikView.Common.Filter filter)
        {
            if (filter == null)
            {
                this._doc.Clear();
                return;
            }

            this._filter = filter;

            this.SetSelections(filter.Fields);
            this.SetVariables(filter.Variables);       
        }

        private void SetVariables(ReportItemDictionary<QvVariable> qvVaiableCollection)
        {
            foreach (var item in qvVaiableCollection.Values)
            {
                Variable v = this._doc.Variables(item.Name);
                if (v != null)
                {
                    v.SetContent(this.GetValueFromExpression(item),false);
                }
            }
        }

        public List<string> GetFieldList()
        {
            List<string> list = new List<string>();
            var fields = this._doc.GetFieldDescriptions();
            
            for (int i = 0; i < fields.Count; i++)
            {
                list.Add(fields[i].Name);
            }

            return list;
        }

        public QlikView.Common.QVField GetQVFieldByName(string name)
        {
            QlikView.Common.QVField qvField = new QlikView.Common.QVField();
            qvField.Name = name;
            var field = this._doc.Fields(name);
            var values = field.GetPossibleValues(2000);
            for (int i = 0; i < values.Count; i++)
            {
                qvField.Values.Add(new QlikView.Common.FieldValue()
                {
                    Value = values[i].Text,
                    IsNumeric = values[i].IsNumeric,
                    Number = values[i].Number
                });
            }

            return qvField;
        }

        public ReportItemDictionary<QVField> GetCurrentSelection()
        {
            ReportItemDictionary<QVField> qc = new ReportItemDictionary<QVField>();
            var selections = this._doc.GetCurrentSelections();
            for (int i = 0; i < selections.Selections.Length; i++)
            {
                string fieldName = selections.VarId[i];
                var field = this._doc.Fields(fieldName);

                QlikView.Common.QVField qvField = new QlikView.Common.QVField();
                qvField.Name = field.Name();
                var values = field.GetSelectedValues();
                for (int j = 0; j < values.Count; j++)
                {
                    qvField.Values.Add(new QlikView.Common.FieldValue()
                    {
                        Value = values[j].Text,
                        IsNumeric = values[j].IsNumeric,
                        Number = values[j].Number
                    });
                }

                qc.Add(qvField.Name, qvField);
            }

            return qc;
        }

        public void SetSelections(ReportItemDictionary<QVField> qvFieldCollection)
        {
            this._doc.Clear();

            foreach (var item in qvFieldCollection.Values)
            {
                ObservableCollection<FieldValue> values;
                if (item.HasExpression)
                {
                    values = this.GetValuesFromExpression(item);
                }
                else
                {
                    values = item.Values;
                }

                var field = this._doc.Fields(item.Name);
                var value = field.GetNoValues();

                int i = 0;
                foreach (var valueItem in values)
                {
                    value.Add();
                    value[i].Text = valueItem.Value;
                    value[i].Number = valueItem.Number;
                    value[i].IsNumeric = valueItem.IsNumeric;

                    i++;
                }

                field.SelectValues(value);
            }
        }

        public int ExportExcel(string objectId, string fileName)
        {
            if (objectId.Contains("|"))
            {
                return this.ExecuteExportExcelFromContainer(objectId, fileName);
            }
            else
            {
                return ExecuteExportExcel(objectId, fileName);
            }
        }

        private int ExecuteExportExcelFromContainer(string idString, string fileName)
        {
            string containerId = idString.Split("|".ToArray())[0].Replace("Container:", string.Empty);
            string objectId = idString.Split("|".ToArray())[1].Replace("Object:", string.Empty);

            var obj = this.FindSheetObject(containerId);

            if (obj != null)
            {
                QlikView.Container c = obj as QlikView.Container;

                if (c != null)
                {
                    for (int j = 0; j < c.GetProperties().ContainedObjects.Count; j++)
                    {
                        var o = c.GetProperties().ContainedObjects[j];
                        if (o.Id_OBSOLETE == objectId)
                        {
                            return ExecuteExportExcel(o.Default, fileName);
                        }
                    }
                }
            }

            return -1;
        }

        private int ExecuteExportExcel(string objectId, string fileName)
        {
            this._exportFileName = fileName;

            var obj = this.FindSheetObject(objectId);

            QlikView.TableBox table = obj as QlikView.TableBox;
            QlikView.Graph graph = obj as QlikView.Graph;
            QlikView.PivotTableBox pivotBox = obj as QlikView.PivotTableBox;
            QlikView.StraightTableBox straightTable = obj as QlikView.StraightTableBox;
            QlikView.ListBox listBox = obj as QlikView.ListBox;
            
            if (obj != null)
            {
                if (table != null)
                {
                    table.ExportBiff(this._exportFileName);
                    return 0;
                }
                else if (graph != null)
                {
                    /*Export format
                        0   =   HTML
                        1   =   Text delimited
                        2   =   bitmap image
                        3   =   XML
                        4   =   QVD
                        5   =   BIFF (Excel)
                     * */
                    bool ok = graph.ExportEx(this._exportFileName, 5);
                    if (ok)
                        return 0;
                    else
                        return -1;
                }
                else if (pivotBox != null)
                {
                    pivotBox.ExportBiff(this._exportFileName);
                    return 0;
                }
                else if (straightTable != null)
                {
                    straightTable.ExportBiff(this._exportFileName);
                    return 0;
                }
                else
                {
                    obj.ExportBiff(this._exportFileName);
                    return 0;
                }
            }

            return -1;
        }

        private int ExecuteExportCSV(string objectId, string fileName)
        {
            this._exportFileName = fileName;

            var obj = this.FindSheetObject(objectId);

            QlikView.TableBox table = obj as QlikView.TableBox;
            QlikView.Graph graph = obj as QlikView.Graph;
            QlikView.PivotTableBox pivotBox = obj as QlikView.PivotTableBox;
            QlikView.StraightTableBox straightTable = obj as QlikView.StraightTableBox;
            QlikView.ListBox listBox = obj as QlikView.ListBox;

            if (obj != null)
            {
                if (table != null)
                {
                    table.Export(this._exportFileName,",");
                    return 0;
                }
                else if (graph != null)
                {
                    /*Export format
                        0   =   HTML
                        1   =   Text delimited
                        2   =   bitmap image
                        3   =   XML
                        4   =   QVD
                        5   =   BIFF (Excel)
                     * */
                    bool ok = graph.ExportEx(this._exportFileName, 1);
                    if (ok)
                        return 0;
                    else
                        return -1;
                }
                else if (pivotBox != null)
                {
                    pivotBox.Export(this._exportFileName, ",");
                    return 0;
                }
                else if (straightTable != null)
                {
                    straightTable.Export(this._exportFileName, ",");
                    return 0;
                }
                else
                {
                    obj.Export(this._exportFileName, ",");
                    return 0;
                }
            }

            return -1;
        }

        public int ExportHtml(string objectId, string fileName)
        {
            this._exportFileName = fileName;

            var obj = this.FindSheetObject(objectId);

            QlikView.TableBox table = obj as QlikView.TableBox;
            QlikView.Graph graph = obj as QlikView.Graph;
            QlikView.PivotTableBox pivotBox = obj as QlikView.PivotTableBox;
            QlikView.StraightTableBox straightTable = obj as QlikView.StraightTableBox;


            if (obj != null)
            {
                if (table != null)
                {
                    table.ExportBiff(this._exportFileName);
                    return 0;
                }

                if (graph != null)
                {
                    /*Export format
                        0   =   HTML
                        1   =   Text delimited
                        2   =   bitmap image
                        3   =   XML
                        4   =   QVD
                        5   =   BIFF (Excel)
                     * */
                    bool ok = graph.ExportEx(this._exportFileName, 0);
                    if (ok)
                        return 0;
                    else
                        return -1;
                }

                if (pivotBox != null)
                {
                    pivotBox.ExportHtml(this._exportFileName);
                    return 0;
                }

                if (straightTable != null)
                {
                    straightTable.ExportHtml(this._exportFileName);
                    return 0;
                }
            }

            return -1;
        }

        public int ExportJPG(string objectId, string fileName)
        {
            if (objectId.Contains("|"))
            {
                return this.ExportJPGFromContainer(objectId, fileName);
            }
            else
            {
                return ExecuteExportJPG(objectId, fileName);
            }
        }

        private int ExportJPGFromContainer(string idString, string filename)
        {
            string containerId = idString.Split("|".ToArray())[0].Replace("Container:", string.Empty);
            string objectId = idString.Split("|".ToArray())[1].Replace("Object:", string.Empty);

            var obj = this.FindSheetObject(containerId);

            if (obj != null)
            {
                QlikView.Container c = obj as QlikView.Container;

                if (c != null)
                {
                    for (int j = 0; j < c.GetProperties().ContainedObjects.Count; j++)
                    {
                        var o = c.GetProperties().ContainedObjects[j];
                        if (o.Id_OBSOLETE == objectId)
                        {
                            return ExecuteExportJPG(objectId, filename);
                        }
                    }
                }
            }

            return -1;
        }

        private int ExecuteExportJPG(string objectId, string fileName)
        {
            this._exportFileName = fileName;

            var obj = this.FindSheetObject(objectId);

            QlikView.TableBox table = obj as QlikView.TableBox;
            QlikView.Graph graph = obj as QlikView.Graph;
            QlikView.PivotTableBox pivotBox = obj as QlikView.PivotTableBox;
            QlikView.StraightTableBox straightTable = obj as QlikView.StraightTableBox;

            if (obj != null)
            {
                if (obj.GetSheet().IsActive() == false)
                {
                    obj.GetSheet().Activate();

                    //special case for china dash
                    if (this._filter != null && (this._filter.Name == "ChinaSalesFunnelDash_Summary" || this._filter.Name == "ShanghaiSalesFunnelDash_Summary"))
                        System.Threading.Thread.Sleep(120 * 1000);
                    else
                        System.Threading.Thread.Sleep(10 * 1000);

                    if (this._filter != null)
                        this._filter = null;

                }

                if (table != null)
                {
                    if (table.IsMinimized())
                        table.Restore();

                    table.ExportBiff(this._exportFileName);
                    return 0;
                }

                if (graph != null)
                {
                    /*Export format
                        0   =   HTML
                        1   =   Text delimited
                        2   =   bitmap image
                        3   =   XML
                        4   =   QVD
                        5   =   BIFF (Excel)
                     * */

                    if (graph.IsMinimized())
                        graph.Restore();

                    bool ok = graph.ExportEx(this._exportFileName, 2);
                    if (ok)
                        return 0;
                    else
                        return -1;
                }

                if (pivotBox != null)
                {
                    if (pivotBox.IsMinimized())
                        pivotBox.Restore();

                    pivotBox.ExportBitmapToFile(this._exportFileName);
                    return 0;
                }

                if (straightTable != null)
                {
                    if (straightTable.IsMinimized())
                        straightTable.Restore();

                    straightTable.ExportBitmapToFile(this._exportFileName);
                    return 0;
                }
            }

            return -1;
        }

        public List<string> GetExportDocumentIdList()
        {
            List<string> IdList = new List<string>();

            var count = this._doc.NoOfSheets();

            for (int i = 0; i < count; i++)
            {
                var sheet = this._doc.Sheets(i.ToString());
                var objects = sheet.GetSheetObjects();
                foreach (var item in objects)
                {
                    string id = item.GetObjectId;
                    short type = item.GetObjectType();

                    if (type == (int)SheetObjectType.Graph || type == (int)SheetObjectType.TableBox
                        || type == (int)SheetObjectType.PivotTable || type == (int)SheetObjectType.StraigtTable)
                        IdList.Add(id);

                    if (type == (int)SheetObjectType.Container)
                    {
                        QlikView.Container c = item as QlikView.Container;
                        for (int j = 0; j < c.GetProperties().ContainedObjects.Count; j++)
                        {
                            var obj = c.GetProperties().ContainedObjects[j];
                            IdList.Add(string.Format("Container:{0}|Object:{1}", c.GetObjectId(), obj.Id_OBSOLETE));
                        }
                    }
                }
            }

            return IdList;
        }

        public void Clear()
        {
            this._doc.Clear();
        }

        public void ClearBackForward()
        {
            this._doc.ClearBackForward();
        }

        #endregion

        private dynamic FindSheetObject(string objectId)
        {
            var count = this._doc.NoOfSheets();

            for (int i = 0; i < count; i++)
            {
                var sheet = this._doc.Sheets(i.ToString());

                var objects = sheet.GetSheetObjects();
                foreach (var item in objects)
                {
                    string id = item.GetObjectId;
                    if (id == objectId)
                        return item;
                }
            }

            return null;
        }

        private string GetValueFromExpression(QvVariable item)
        {
            if (item.HasExpression)
            {
                return DateFunctions.GetValueFromExpression(item.Expression).ToString("yyyy/MM/dd");
            }
            else
            {
                return item.Value;
            }
        }

        private ObservableCollection<FieldValue> GetValuesFromExpression(QVField item)
        {
            ObservableCollection<FieldValue> values = new ObservableCollection<FieldValue>();

            if (item.Name == "TheYear" || item.Name == "MonthOfYear" || item.Name == "DayOfMonth")
            {
                var valuelist = Functions.GetDateValueFromExpression(item.Expression);

                foreach (var value in valuelist)
                {
                    values.Add(new FieldValue()
                    {
                        IsNumeric = true,
                        Number = value,
                        Value = value.ToString()
                    });
                }
            }
            else if (item.Name == "YearMonth" && item.Expression.Contains("CurrentFiscalYearMonthEx"))
            {
                values = Functions.GetValuesFromCurrentFiscalYearMonthEx();
            }
            else if (item.Name == "YearMonth" && item.Expression.Contains("CurrentFiscalYearMonth"))
            {
                values = Functions.GetValuesFromCurrentFiscalYearMonth();
            }
            else if (item.Name == "YearMonth" && item.Expression.Contains("LatestTwoFiscalYearMonth"))
            {
                values = Functions.GetValuesFromLatestTwoFiscalYearMonth();
            }
            else if (item.Name == "YearMonth" && item.Expression.Contains("CurrentYearMonthNumeric"))
            {
                values = Functions.GetValuesFromCurrentYearMonthNumeric();
            }
            else if (item.Name == "YearMonth" && item.Expression.Contains("CurrentYearMonth"))
            {
                values = Functions.GetValuesFromCurrentYearMonth();
            }
            else if (item.Name == "FiscalYearName" && item.Expression == "CurrentFiscalYearName")
            {
                values = Functions.GetCurrentFiscalYearName();
            }
            else if (item.Name == "FiscalYearName" && item.Expression == "PreviousFiscalYearName")
            {
                values = Functions.GetPreviousFiscalYearName();
            }
            else if (item.Name == "UsageYearMonth" && item.Expression.Contains("LastMonthFiscalYearMonth"))
            {
                values = Functions.GetValuesFromLastMonthFiscalYearMonth();

                var field = this.GetQVFieldByName(item.Name);

                foreach (var value in values)
                {
                    value.IsNumeric = field.Values.First(x => x.Value == value.Value).IsNumeric;
                    value.Number = field.Values.First(x => x.Value == value.Value).Number;
                }
            }
            else
            {
                var field = this.GetQVFieldByName(item.Name);
                List<ExpressionCondition> conditions = DateFunctions.CalculateExpression(item.Expression);

                foreach (var value in field.Values)
                {
                    DateTime date = DateTime.Parse(value.Value).Date;

                    bool isChecked = true;
                    foreach (var condition in conditions)
                    {
                        if (!condition.IsChecked(date))
                        {
                            isChecked = false;
                            break;
                        }
                    }

                    if (isChecked)
                        values.Add(value);
                }
            }
            return values;
        }


        public void Preview(ReportConnection connection)
        {
            //Do Nothing
        }


        public int ExportCSV(string objectId, string fileName)
        {
            return ExecuteExportCSV(objectId, fileName);
        }
    }
}
