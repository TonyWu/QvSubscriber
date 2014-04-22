using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EFSchools.Englishtown.SalesForce.MetaData;
using System.Data;

namespace EFSchools.Englishtown.SalesForce.Common.ETL.Transformation
{
    class [Object]Transform : ITransformPlan<[Object]>
    {
        #region ITransformPlan<[Object]> Members

        public System.Data.DataTable Transform(IEnumerable<[Object]> result)
        {
            DataTable dtResult = new DataTable();

            #region init table schema

            //init table schema
            dtResult.Columns.Add("Id", typeof(string));
            [ColumnsPlaceHolder]
            dtResult.Columns.Add("SystemModStamp", typeof(DateTime));

            #endregion

            foreach (var item in result)
            {
                DataRow dr = dtResult.NewRow();

                TransformUtil.SetValue(dr, "Id", item.Id);
                [SetValuePlaceHolder]
                TransformUtil.SetValue(dr, "SystemModStamp", item.SystemModstamp);

                dtResult.Rows.Add(dr);
            }

            return dtResult;
        }

        #endregion

        #region ITransformPlan Members

        public System.Data.DataTable Transform(System.Collections.IEnumerable result)
        {
            var objectResult = result.OfType<[Object]>();

            return ((ITransformPlan<[Object]>)this).Transform(objectResult);
        }

        #endregion
    }
}
