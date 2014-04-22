using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EFSchools.Englishtown.SalesForce.Common.ETL.ExecutePlan;

namespace EFSchools.Englishtown.SalesForce.Common.ETL.ExecutionPlan
{
    public class [Object]ExecutionPlan : IExecutionPlan
    {
        #region Private Members

        private [Object]ExecutionPlan() { }

        #endregion

        #region IExecutionPlan Members

        public Extraction.IExtractPlan ExtractionPlan
        {
            get { return new Extraction.[Object]ExtractPlan(); }
        }

        public Transformation.ITransformPlan TransformationPlan
        {
            get { return new Transformation.[Object]Transform(); }
        }

        public Loading.ILoadPlan LoadingPlan
        {
            get { return new Loading.[Object]LoadPlan(); }
        }

        public SalesForceServiceExecutionInfo ExecutionInfo
        {
            get;
            set;
        }

        public SalesForceETLConfiguration EtlConfiguration
        {
            get;
            set;
        }

        #endregion
    }
}
