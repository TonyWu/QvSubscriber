using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EF.Frameworks.Common.SecurityEF.UserManager4;
using EF.Frameworks.Common.ExceptionsEF;
using EFSchools.Englishtown.SalesForce.Processors.DataExport.IncrementalLoaders;
using EFSchools.Englishtown.SalesForce.Common;
using EF.Frameworks.Common.ServicesEF;

namespace EFSchools.Englishtown.SalesForce.Processors
{
    public class [Object]ExportService : Service
    {
         #region " Constructors "

        public [Object]ExportService(string serviceName, UserPrincipal principal)
            : base(serviceName)
        {
            try
            {
                this.Initialize(serviceName, principal);
            }
            catch (Exception e)
            {
                ExceptionManager.Publish(e);
                throw e;
            }
        }

        #endregion

        #region "  Non-Public Methods "

        private void Initialize(string name, UserPrincipal principal)
        {
            this.RegisterTask(name, principal);
        }

        private void RegisterTask(string name, UserPrincipal principal)
        {
            [Object]Loader task = new [Object]Loader(name, principal);

            SalesForceETLConfiguration etlConfiguration = ETLConfigurationSvc.LoadByName(task.BusinessTypeName);

            task.Schedule(DateTime.Now, TimeSpan.FromSeconds(etlConfiguration.ScheduleInterval));

            this.AddTask(task);
        }

        #endregion
    }
}
