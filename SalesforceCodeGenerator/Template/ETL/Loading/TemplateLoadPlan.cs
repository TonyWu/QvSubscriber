using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace EFSchools.Englishtown.SalesForce.Common.ETL.Loading
{
    class [Object]LoadPlan : ILoadPlan
    {
        #region ILoadPlan Members

        public System.Data.SqlClient.SqlBulkCopyOptions BulkCopyOption
        {
            get { return SqlBulkCopyOptions.Default; }
        }

        public IEnumerable<System.Data.SqlClient.SqlBulkCopyColumnMapping> BulkCopyColumnMapping
        {
            get
            {
                List<SqlBulkCopyColumnMapping> mapping = new List<SqlBulkCopyColumnMapping>()
                {
                    new SqlBulkCopyColumnMapping("Id", "Id"),
                    [ColumnMappingPlaceHolder]
                    new SqlBulkCopyColumnMapping("SystemModStamp", "SystemModStamp")
                };

                return mapping;
            }
        }

        #endregion
    }
}
