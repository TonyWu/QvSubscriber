using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SalesforceCodeGenerator
{
    class Constants
    {
        public static readonly string TemplateExecutionPlan = @"Template\ETL\ExecutionPlan\TemplateExecutionPlan.cs";
        public static readonly string TemplateExtractPlan = @"Template\ETL\Extraction\TemplateExtractPlan.cs";
        public static readonly string TemplateLoadPlan = @"Template\ETL\Loading\TemplateLoadPlan.cs";
        public static readonly string TemplateTransform = @"Template\ETL\Transformation\TemplateTransform.cs";
        public static readonly string TemplateExportService = @"Template\Salesforce.Processors\DataExport\TemplateExportService.cs";
        public static readonly string TemplateLoader = @"Template\Salesforce.Processors\DataExport\IncrementalLoaders\TemplateLoader.cs";
        public static readonly string Template_Save_p = @"Template\VS3\Salesforce\Stored Procedure\dbo.Template_Save_p.prc";
        public static readonly string TemplateTable = @"Template\VS3\Salesforce\Table\dbo.Template.tbl";
        public static readonly string TemplateWTable = @"Template\VS3\Salesforce\Table\dbo.wTemplate.tbl";
    }
}
