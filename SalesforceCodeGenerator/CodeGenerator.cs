using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SalesforceCodeGenerator
{
    class CodeGenerator
    {
        private string _objectName;

        public List<Field> Fields { get; set; }

        public CodeGenerator(string objectName)
        {
            this._objectName = objectName;
        }

        /// <summary>
        /// For ExecutionPlan, Loader, ExportService
        /// </summary>
        /// <param name="templateFile"></param>
        /// <param name="outputFile"></param>
        public void GenerateCommon(string templateFile, string outputFile)
        {
            string template = this.ReadTemplate(templateFile);
            template = template.Replace("[Object]", this._objectName);
            this.SaveTemplate(template, outputFile);
        }

        private void SaveTemplate(string template, string outputFile)
        {
            using (StreamWriter sw = new StreamWriter(outputFile))
            {
                sw.Write(template);
            }
        }

        private string ReadTemplate(string templateFile)
        {
            using (StreamReader sr = new StreamReader(templateFile))
            {
                return sr.ReadToEnd();
            }
        }

        public void GenerateExtractPlan(string templateFile, string outputFile)
        {
            string template = this.ReadTemplate(templateFile);
            template = template.Replace("[Object]", this._objectName);
            StringBuilder queryFields = new StringBuilder();

            foreach (var item in this.Fields)
            {
                queryFields.AppendLine("                    ," + item.FieldName);
            }

            template = template.Replace("[QueryFields]", queryFields.ToString().Trim());
            this.SaveTemplate(template, outputFile);
        }

        public void GenerateLoadPlan(string templateFile, string outputFile)
        {
            string template = this.ReadTemplate(templateFile);
            template = template.Replace("[Object]", this._objectName);
            StringBuilder queryFields = new StringBuilder();

            foreach (var item in this.Fields)
            {
                queryFields.AppendLine(string.Format("                    new SqlBulkCopyColumnMapping(\"{0}\", \"{0}\"),",item.FieldName));
            }

            template = template.Replace("[ColumnMappingPlaceHolder]", queryFields.ToString().Trim());
            this.SaveTemplate(template, outputFile);
        }

        public void GenerateTransform(string templateFile, string outputFile)
        {
            string template = this.ReadTemplate(templateFile);
            template = template.Replace("[Object]", this._objectName);
            StringBuilder columns = new StringBuilder();
            StringBuilder setValues = new StringBuilder();
            foreach (var item in this.Fields)
            {
                columns.AppendLine(string.Format("            dtResult.Columns.Add(\"{0}\", typeof({1}));",item.FieldName,item.Type.Name));
                setValues.AppendLine(string.Format("                TransformUtil.SetValue(dr, \"{0}\", item.{0});",item.FieldName));
            }

            template = template.Replace("[ColumnsPlaceHolder]", columns.ToString().Trim());
            template = template.Replace("[SetValuePlaceHolder]", setValues.ToString().Trim());
            this.SaveTemplate(template, outputFile);
        }

        

        public void GenerateTable(string templateFile, string outputFile)
        {
            string template = this.ReadTemplate(templateFile);
            template = template.Replace("[Object]", this._objectName);
            StringBuilder columns = new StringBuilder();
            foreach (var item in this.Fields)
            {
                if (item.DbType == "datetime" || item.DbType == "bit" || item.DbType == "integer" || item.DbType == "int")
                    columns.AppendLine(string.Format("	[{0}] [{1}] NULL,", item.FieldName, item.DbType));
                else
                    columns.AppendLine(string.Format("	[{0}] [{1}]({2}) NULL,", item.FieldName, item.DbType, item.Length));
            }

            template = template.Replace("[ColumnsPlaceHolder]", columns.ToString().Trim());
            this.SaveTemplate(template, outputFile);
        }      

        public void GenerateStoredProcedure(string templateFile, string outputFile)
        {
            string template = this.ReadTemplate(templateFile);
            template = template.Replace("[Object]", this._objectName);
            StringBuilder updateColumns = new StringBuilder();
            StringBuilder insertTargets = new StringBuilder();
            StringBuilder insertSources = new StringBuilder();

            foreach (var item in this.Fields)
            {
                updateColumns.AppendLine(string.Format("		   [{0}] = w.[{0}],", item.FieldName));
                insertTargets.AppendLine(string.Format("		   [{0}],", item.FieldName));
                insertSources.AppendLine(string.Format("		   w.[{0}],", item.FieldName));
            }

            template = template.Replace("[UpdatePlaceHolder]", updateColumns.ToString().Trim());
            template = template.Replace("[InsertPlaceHolderTarget]", insertTargets.ToString().Trim());
            template = template.Replace("[InsertPlaceHolderSource]", insertSources.ToString().Trim());
            this.SaveTemplate(template, outputFile);
        }
    }
}
