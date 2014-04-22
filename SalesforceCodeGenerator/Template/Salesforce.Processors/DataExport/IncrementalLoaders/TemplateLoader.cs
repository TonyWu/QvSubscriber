using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EF.Frameworks.Common.SecurityEF.PrincipalEF;
using EF.Frameworks.Common.FactoryEF;
using EFSchools.Englishtown.SalesForce.Common.ETL.ExecutionPlan;

namespace EFSchools.Englishtown.SalesForce.Processors.DataExport.IncrementalLoaders
{
    class [Object]Loader:IncrementalLoaderBase
    {
        public [Object]Loader(string serviceName, IEFPrincipal principal)
            : base(serviceName, principal, Factory<[Object]ExecutionPlan>.Create())
        { }

        public override string BusinessTypeName
        {
            get { return "[Object]"; }
        }
    }
}
