using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using EFSchools.Englishtown.SalesForce.MetaData;
using System.IO;

namespace SalesforceCodeGenerator
{
    public partial class MainForm : Form
    {
        private CodeGenerator _codeGenerator;
        private Dictionary<string, Field> _fields = new Dictionary<string, Field>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Field field = new Field();
            field.FieldName = this.txtFieldName.Text;
            field.DbType = this.comboBoxType.SelectedItem.ToString();
            field.Length = this.txtLength.Text;

            if (!this._fields.ContainsKey(field.FieldName))
                this._fields.Add(field.FieldName, field);
            this.Display();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.comboBoxType.DataSource = TypeMapping.Mappings.Keys.ToList();
        }

        private void btnGenerateAll_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txtObjectName.Text))
            {
                MessageBox.Show("Please input the Object Name.");
                return;
            }

            try
            {
                this.GenerateCode();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }

        private void GenerateCode()
        {
            CodeGenerator coderGenerator = new CodeGenerator(this.txtObjectName.Text);
            coderGenerator.Fields = this._fields.Values.ToList();

            string outputFile = string.Empty;

            outputFile = Constants.TemplateExecutionPlan
                .Replace(@"Template\", @"Output\")
                .Replace("Template", this.txtObjectName.Text);
            coderGenerator.GenerateCommon(Constants.TemplateExecutionPlan, outputFile);

            outputFile = Constants.TemplateLoader
                .Replace(@"Template\", @"Output\")
                .Replace("Template", this.txtObjectName.Text);
            coderGenerator.GenerateCommon(Constants.TemplateLoader, outputFile);

            outputFile = Constants.TemplateExportService
                .Replace(@"Template\", @"Output\")
                .Replace("Template", this.txtObjectName.Text);
            coderGenerator.GenerateCommon(Constants.TemplateExportService, outputFile);

            outputFile = Constants.TemplateExtractPlan
                .Replace(@"Template\", @"Output\")
                .Replace("Template", this.txtObjectName.Text);
            coderGenerator.GenerateExtractPlan(Constants.TemplateExtractPlan, outputFile);

            outputFile = Constants.TemplateLoadPlan
                .Replace(@"Template\", @"Output\")
                .Replace("Template", this.txtObjectName.Text);
            coderGenerator.GenerateLoadPlan(Constants.TemplateLoadPlan, outputFile);

            outputFile = Constants.TemplateTransform
                .Replace(@"Template\", @"Output\")
                .Replace("Template", this.txtObjectName.Text);
            coderGenerator.GenerateTransform(Constants.TemplateTransform, outputFile);

            outputFile = Constants.TemplateTable
                .Replace(@"Template\", @"Output\")
                .Replace("Template", this.txtObjectName.Text);
            coderGenerator.GenerateTable(Constants.TemplateTable, outputFile);

            outputFile = Constants.TemplateWTable
                .Replace(@"Template\", @"Output\")
                .Replace("Template", this.txtObjectName.Text);
            coderGenerator.GenerateTable(Constants.TemplateWTable, outputFile);

            outputFile = Constants.Template_Save_p
                .Replace(@"Template\", @"Output\")
                .Replace("Template", this.txtObjectName.Text);
            coderGenerator.GenerateStoredProcedure(Constants.Template_Save_p, outputFile);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            this._fields.Remove(this.txtFieldName.Text);
            this.Display();
        }

        private void Display()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in this._fields.Values)
            {
                if (item.DbType == "datetime" || item.DbType == "bit" || item.DbType == "integer" || item.DbType == "int")
                    sb.AppendLine(string.Format("[{0}] [{1}] NULL,", item.FieldName, item.DbType));
                else
                    sb.AppendLine(string.Format("[{0}] [{1}]({2}) NULL,", item.FieldName, item.DbType, item.Length));
            }

            this.richTextBox1.Text = sb.ToString();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this._fields.Clear();
            this.Display();
        }

        private void btnLoadColumns_Click(object sender, EventArgs e)
        {
            string objectName = this.txtObjectName.Text;

            if (string.IsNullOrWhiteSpace(objectName))
            {
                MessageBox.Show("Please input the object name.");
                return;
            }

            this._fields = TemplateMgr.LoadFieldsByObject(objectName);

            if (this._fields.Count > 0)
                this.Display();
            else
                MessageBox.Show("The object schema not exist. Will create it.");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string objectName = this.txtObjectName.Text;
            if (string.IsNullOrWhiteSpace(objectName))
            {
                MessageBox.Show("Please input the object name.");
                return;
            }
            Dictionary<string, Field> fields = new Dictionary<string, Field>();

            foreach (var item in this._fields.Keys.OrderBy(x=>x).ToList())
            {
                fields.Add(item, this._fields[item]);
            }

            if (fields.Count > 0)
                TemplateMgr.Save(fields, objectName);
        }

        public static DateTime UnixTimeStampToDateTime(long timestamp)
        {
            DateTime time = new DateTime(1970, 1, 1);
            return time.AddMilliseconds(timestamp);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DateTime time = UnixTimeStampToDateTime(1403841609307);
            //string[] array = this.richTextBox1.Text.Split("\n\t".ToArray());

            //foreach (var item in array)
            //{
            //    if (string.IsNullOrWhiteSpace(item))
            //        continue;
            //    var temp = item.Trim().Split(' ');
            //    Field field = new Field();

            //    field.FieldName = temp[0].Replace("[", "").Replace("]", "");
            //    field.DbType = temp[1].Split('(')[0].Replace("[", "").Replace("]", "");
            //    field.Length = temp[1].Split('(').Length == 1?string.Empty:temp[1].Split('(')[1].Replace(")", "");

            //    this._fields.Add(field.FieldName, field);
            //}

            //string objectName = this.txtObjectName.Text;
            //TemplateMgr.Save(this._fields, objectName);
        }

        private void btnRefectorPro_Click(object sender, EventArgs e)
        {

            //string[] array = "Id, RecordTypeId, WhoId, WhatId, Subject, ActivityDate, Status, Priority, OwnerId, Description, IsDeleted, AccountId, IsClosed, CreatedDate, CreatedById, LastModifiedDate, LastModifiedById, SystemModstamp, IsArchived, CallDurationInSeconds, CallType, CallDisposition, CallObject, ReminderDateTime, IsReminderSet, ConnectionReceivedId, ConnectionSentId, EF_Office__c, Appt_Status__c, Owner__c, Task_created_by_role__c, Auto_Assignment__c, RecordTypeName__c, Enrolment_details__c, Coaching_comments__c, Comments__c, Feedback__c, Advice__c".Replace(", ","&").Split('&');// "Id, RecordTypeId, WhoId, WhatId, Subject, ActivityDate, Status, Priority, OwnerId, Description, IsDeleted, AccountId, IsClosed, CreatedDate, CreatedById, LastModifiedDate, LastModifiedById, SystemModstamp, IsArchived, CallDurationInSeconds, CallType, CallDisposition, CallObject, ReminderDateTime, IsReminderSet, ConnectionReceivedId, ConnectionSentId,EF_Office__c, Appt_Status__c, Owner__c, Task_created_by_role__c, Auto_Assignment__c, RecordTypeName__c, Enrolment_details__c, Coaching_comments__c, Comments__c, Feedback__c, Advice__c".Replace(", ", "&").Split("&".ToArray());
            string[] array = "Id,SystemModStamp".Replace(", ", "&").Split('&');
            string typeName = this.txtObjectName.Text;

            Type t = Type.GetType("EFSchools.Englishtown.SalesForce.MetaData." + typeName);
            List<PropertyInfo> list = MetadataRefector.LoadProperties(t);

            string query = @"select {0} from " + typeName;

            StringBuilder sb = new StringBuilder();

            int count = 0;
            foreach (var item in list)
            {
                if (!array.Contains(item.Name) && !item.Name.Contains("Specified"))
                {
                    sb.AppendLine(item.Name + ",");
                    count++;
                }
            }

            query = string.Format(query, sb.ToString());
            this.richTextBox1.Text = query;

            this.txtFieldName.Text = list.Count.ToString();
            this.txtLength.Text = count.ToString();
            this.txtObjectName.Text = array.Count().ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            //DateTime duedate = DateTime.Parse("1995-09-20T02:15:40.000Z");
            //string oneMonthLater = duedate.AddMonths(1).ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.000Z");

            //var str1 = new String('*', 10);

            string task = "EFLabsCost_STN";

            StringBuilder sbTaskReports = new StringBuilder();
            StringBuilder sbReports = new StringBuilder();
            StringBuilder sbFilters = new StringBuilder();

            var stn = ReportParameter.PopulateSTN();

            foreach (var item in stn)
            {
                sbTaskReports.AppendLine("        <Report>" + task + "_" + item.Name + "</Report>");

                sbReports.AppendLine(string.Format("    <Report Name=\"{0}_{1}\" Description=\"{2}\">", task, item.Name, item.Description));
                sbReports.AppendLine("      <Connection>EFLabsCost</Connection>");
                sbReports.AppendLine(string.Format("      <OutputFileName>{0}_{1}.xls</OutputFileName>",task,item.Name));
                sbReports.AppendLine("      <EnableDynamicNaming>True</EnableDynamicNaming>");
                sbReports.AppendLine("      <EmbeddedInMail>False</EmbeddedInMail>");
                sbReports.AppendLine(string.Format("      <QlikViewExportObjectId>Document\\{0}</QlikViewExportObjectId>",item.ObjectId));
                sbReports.AppendLine(string.Format("      <Filter>{0}_{1}</Filter>",task,item.Name));
                sbReports.AppendLine("      <ReportType>0</ReportType>");
                sbReports.AppendLine("    </Report>");


                StringBuilder sbValues = new StringBuilder();

                foreach (var value in item.FilterValue)
                {
                    sbValues.AppendLine("          <Value Number=\"1E+300\" IsNumeric=\"False\">" + value + "</Value>");
                }

                sbFilters.AppendLine(string.Format("    <Filter Name=\"{0}_{1}\" Description=\"{2}\">",task,item.Name,item.Description));
                sbFilters.AppendLine("      <Connection>EFLabsCost</Connection>");
                sbFilters.AppendLine("      <Fields>");
                sbFilters.AppendLine("        <Field Name=\"UsageYearMonth\" Description=\"\" Expression=\"LastMonthFiscalYearMonth\" />");
                sbFilters.AppendLine("        <Field Name=\"ProductCategory\" Description=\"\" Expression=\"\">");
                sbFilters.Append(sbValues.ToString());
                sbFilters.AppendLine("        </Field>");
                sbFilters.AppendLine("      </Fields>");
                sbFilters.AppendLine("      <Variables>");
                sbFilters.AppendLine("      </Variables>");
                sbFilters.AppendLine("    </Filter>");
            }

            StreamReader sr = new StreamReader("ReportConfig.xml");

            string str = sr.ReadToEnd();

            str = str.Replace("{TaskReport}", sbTaskReports.ToString())
                  .Replace("{Report}", sbReports.ToString())
                  .Replace("{Filter}", sbFilters.ToString());

            StreamWriter sw = new StreamWriter(task + ".xml");
            sw.Write(str);

            sr.Close();
            sw.Close();
        }


    }
}
