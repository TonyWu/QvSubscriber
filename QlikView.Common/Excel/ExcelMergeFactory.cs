using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QlikView.Common
{
    public class ExcelMergeFactory
    {
        private static Dictionary<string, Type> ExcelMergeProviders;

        static ExcelMergeFactory()
        {
            ExcelMergeProviders = new Dictionary<string, Type>();
            ExcelMergeProviders.Add("General", Type.GetType("QlikView.Common.ExcelMerge"));
            ExcelMergeProviders.Add("FunnelMonthly", Type.GetType("QlikView.Common.FunnelMonthlyMerge"));
            ExcelMergeProviders.Add("FunnelWeekly", Type.GetType("QlikView.Common.FunnelWeeklyMerge"));
            ExcelMergeProviders.Add("WeeklyStatisticReport", Type.GetType("QlikView.Common.FunnelRevenueMerge"));
            ExcelMergeProviders.Add("SpotRateMonthly", Type.GetType("QlikView.Common.SpotRateMonthlyMerge"));
            ExcelMergeProviders.Add("FunnelCNMini", Type.GetType("QlikView.Common.FunnelCNMiniMerge"));
            ExcelMergeProviders.Add("ChinaSalesFunnelDash", Type.GetType("QlikView.Common.ChinaSalesFunnelDash"));
            ExcelMergeProviders.Add("ShanghaiSalesFunnelDash", Type.GetType("QlikView.Common.ShanghaiSalesFunnelDash"));
            ExcelMergeProviders.Add("EFECMKTDash", Type.GetType("QlikView.Common.EFECMKTDash"));

        }

        public static IExcelMerge Create(string taskName = "")
        {
            Type mergeType = null;

            if (taskName.StartsWith("FunnelByWeek"))
            {
                mergeType = ExcelMergeProviders["FunnelWeekly"];
            }
            else if (taskName.StartsWith("WeeklyStatisticReport"))
            {
                mergeType = ExcelMergeProviders["WeeklyStatisticReport"];
            }
            else if (taskName.StartsWith("FunnelMonthly"))
            {
                mergeType = ExcelMergeProviders["FunnelMonthly"];
            }
            else if (taskName.StartsWith("SpotRateMonthly"))
            {
                mergeType = ExcelMergeProviders["SpotRateMonthly"];
            }
            else if (taskName.StartsWith("FunnelCNMini"))
            {
                mergeType = ExcelMergeProviders["FunnelCNMini"];
            }
            else if (taskName.StartsWith("ChinaSalesFunnelDash"))
            {
                mergeType = ExcelMergeProviders["ChinaSalesFunnelDash"];
            }
            else if (taskName.StartsWith("SH_SalesFunnelDash"))
            {
                mergeType = ExcelMergeProviders["ShanghaiSalesFunnelDash"];
            }
            else if (taskName.StartsWith("EFECMKTDash"))
            {
                mergeType = ExcelMergeProviders["EFECMKTDash"];
            }
            else
            {
                mergeType = ExcelMergeProviders["General"];
            }

            return Create(mergeType);
        }

        public static IExcelMerge Create(Type providerClass)
        {
            return Activator.CreateInstance(providerClass) as IExcelMerge;
        }
    }
}
