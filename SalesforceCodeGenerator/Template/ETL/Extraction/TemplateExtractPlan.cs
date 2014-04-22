using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFSchools.Englishtown.SalesForce.Common.ETL.Extraction
{
    class [Object]ExtractPlan : IExtractPlan
    {
        #region IExtractPlan Members

        public string Name
        {
            get { return "[Object]"; }
        }

        public string Query
        {
            get { return @"Select Id
                    [QueryFields]                  
                    ,SystemModStamp
                    FROM [Object] WHERE SystemModstamp >= [DueDate] ORDER BY SystemModstamp LIMIT [Limit]"; }
        }

        #endregion
    }
}
