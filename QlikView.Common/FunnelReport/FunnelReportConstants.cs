using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QlikView.Common
{
    public static class GeneralParameters
    {
        public static readonly bool KFR0 = ParameterConfig.GetBoolValue("General", "KFR0");
        public static readonly bool KFR1 = ParameterConfig.GetBoolValue("General", "KFR1");
        public static readonly bool KFR2 = ParameterConfig.GetBoolValue("General", "KFR2");
    }

    public static class FunnelWeeklyReportParameters
    {
        //General
        public static readonly string MonthBegin = ParameterConfig.GetStringValue("FunnelWeeklyReport", "MonthBegin") ?? "B";
        public static readonly string MonthEnd = ExcelUtilies.ExcelColumnIndexToName(ExcelUtilies.ExcelColumnNameToIndex(MonthBegin) + ParameterConfig.GetIntValue("FunnelWeeklyReport", "MonthColumns") - 1);//ParameterConfig.GetStringValue("FunnelWeeklyReport", "MonthEnd") ?? "M";
        public static readonly string WeekBegin = ParameterConfig.GetStringValue("FunnelWeeklyReport", "WeekBegin") ?? "B";
        public static readonly string WeekEnd = ExcelUtilies.ExcelColumnIndexToName(ExcelUtilies.ExcelColumnNameToIndex(WeekBegin) + 106 - 1);//ParameterConfig.GetStringValue("FunnelWeeklyReport", "WeekEnd") ?? "BN";
        public static readonly string YTDFunnel = ParameterConfig.GetStringValue("FunnelWeeklyReport", "YTDFunnel") ?? "BO";
        public static readonly string YTD1011 = ParameterConfig.GetStringValue("FunnelWeeklyReport", "YTD1011") ?? "BP";
        public static readonly string VS1011YTD = ParameterConfig.GetStringValue("FunnelWeeklyReport", "VS1011YTD") ?? "BQ";
        public static readonly string YTDKFR2 = ParameterConfig.GetStringValue("FunnelWeeklyReport", "YTDKFR2") ?? "BR";
        public static readonly string YTDACTVsKFR2 = ParameterConfig.GetStringValue("FunnelWeeklyReport", "YTDACTVsKFR2") ?? "BS";
        public static readonly string YTDKFR1 = ParameterConfig.GetStringValue("FunnelWeeklyReport", "YTDKFR1") ?? "BT";
        public static readonly string YTDACTVsKFR1 = ParameterConfig.GetStringValue("FunnelWeeklyReport", "YTDACTVsKFR1") ?? "BU";
        public static readonly string YTDKFR0 = ParameterConfig.GetStringValue("FunnelWeeklyReport", "YTDKFR0") ?? "BV";
        public static readonly string YTDACTVsKFR0 = ParameterConfig.GetStringValue("FunnelWeeklyReport", "YTDACTVsKFR0") ?? "BW";

        public static readonly int FTORow = ParameterConfig.GetIntValue("FunnelWeeklyReport", "FTORow"); //7
        public static readonly int ConvertedToFTPRow = ParameterConfig.GetIntValue("FunnelWeeklyReport", "ConvertedToFTPRow");// 8;
        public static readonly int PaidRateRow = ParameterConfig.GetIntValue("FunnelWeeklyReport", "PaidRateRow");//10;

        //Weekly
        public static readonly string SpotRate = ParameterConfig.GetStringValue("FunnelWeeklyReport", "SpotRate") ?? "A3";
        public static readonly string KFRRate = ParameterConfig.GetStringValue("FunnelWeeklyReport", "KFRRate") ?? "A4";
        public static readonly int OnlineRow = ParameterConfig.GetIntValue("FunnelWeeklyReport", "OnlineRow");//5;
        public static readonly string OnlineLocalCurrency = ParameterConfig.GetStringValue("FunnelWeeklyReport", "OnlineLocalCurrency") ?? "A22";
        public static readonly int OnlineLocalRow = ParameterConfig.GetIntValue("FunnelWeeklyReport", "OnlineLocalRow");// 22;
        public static readonly int TeleSalesRow = ParameterConfig.GetIntValue("FunnelWeeklyReport", "TeleSalesRow");//24;
        public static readonly string TSLocalCurrency = ParameterConfig.GetStringValue("FunnelWeeklyReport", "TSLocalCurrency") ?? "A38";
        public static readonly int TSLocalRow = ParameterConfig.GetIntValue("FunnelWeeklyReport", "TSLocalRow");// 38;
        public static readonly string BudgetRate = ParameterConfig.GetStringValue("FunnelWeeklyReport", "BudgetRate") ?? "A43";
        public static readonly string TotalLocalCurrency = ParameterConfig.GetStringValue("FunnelWeeklyReport", "TotalLocalCurrency") ?? "A41";
        public static readonly int TotalLocalRow = ParameterConfig.GetIntValue("FunnelWeeklyReport", "TotalLocalRow");// 41;
        public static readonly int SourceRowCount = ParameterConfig.GetIntValue("FunnelWeeklyReport", "SourceRowCount");//41;
        public static readonly int MergedRowCount = SourceRowCount + 1;

        //Header
        public static readonly string Header_YTDACT_CurrentFisicalYear = ParameterConfig.GetStringValue("FunnelWeeklyReport", "Header_YTDACT_CurrentFisicalYear") ?? "YTD ACT 11/12";
        public static readonly string Header_YTDACT_BeforeFisicalYear = ParameterConfig.GetStringValue("FunnelWeeklyReport", "Header_YTDACT_BeforeFisicalYear") ?? "YTD ACT 10/11";
        public static readonly string Header_YTDVS = ParameterConfig.GetStringValue("FunnelWeeklyReport", "Header_YTDVS") ?? "VS 10/11 YTD";
        public static readonly string Header_YTDKRF3 = ParameterConfig.GetStringValue("FunnelWeeklyReport", "Header_YTDKRF3") ?? "YTD KFR3 11/12";
        public static readonly string Header_YTDACTVsKRF3 = ParameterConfig.GetStringValue("FunnelWeeklyReport", "Header_YTDACTVsKRF3") ?? "YTD ACT vs KFR3";
        public static readonly string Header_YTDKRF2 = ParameterConfig.GetStringValue("FunnelWeeklyReport", "Header_YTDKRF2") ?? "YTD KFR2 11/12";
        public static readonly string Header_YTDACTVsKRF2 = ParameterConfig.GetStringValue("FunnelWeeklyReport", "Header_YTDACTVsKRF2") ?? "YTD ACT vs KFR2";
        public static readonly string Header_YTDKRF1 = ParameterConfig.GetStringValue("FunnelWeeklyReport", "Header_YTDKRF1") ?? "YTD KFR1 11/12";
        public static readonly string Header_YTDACTVsKRF1 = ParameterConfig.GetStringValue("FunnelWeeklyReport", "Header_YTDACTVsKRF1") ?? "YTD ACT vs KFR1";
        public static readonly string Header_YTDKRF0 = ParameterConfig.GetStringValue("FunnelWeeklyReport", "Header_YTDKRF0") ?? "YTD KFR0 11/12";
        public static readonly string Header_YTDACTVsKRF0 = ParameterConfig.GetStringValue("FunnelWeeklyReport", "Header_YTDACTVsKRF0") ?? "YTD ACT vs KFR0";
        public static readonly string Header_Week_BeforeFisicalYear = ParameterConfig.GetStringValue("FunnelWeeklyReport", "Header_Week_BeforeFisicalYear") ?? "11/12 Week";
        public static readonly string Header_WeekVS_BeforeFisicalYear = ParameterConfig.GetStringValue("FunnelWeeklyReport", "Header_WeekVS_BeforeFisicalYear") ?? "VS 11/12 Week";
        public static readonly string Header_FunnelKPI = ParameterConfig.GetStringValue("FunnelWeeklyReport", "Header_FunnelKPI") ?? "Funnel KPI";
    }

    public static class FunnelWeeklyReportChinaParameters
    {
        public static readonly int OnlineRow = ParameterConfig.GetIntValue("FunnelWeeklyReportChina", "OnlineRow");//5;
        public static readonly int ChinaYearMonthRowCount = ParameterConfig.GetIntValue("FunnelWeeklyReportChina", "ChinaYearMonthRowCount");//19;
        public static readonly string OnlineLocalCurrency = ParameterConfig.GetStringValue("FunnelWeeklyReportChina", "OnlineLocalCurrency") ?? "A21";
        public static readonly int OnlineLocalRow = ParameterConfig.GetIntValue("FunnelWeeklyReportChina", "OnlineLocalRow");//21;
        public static readonly int TeleSalesRow = ParameterConfig.GetIntValue("FunnelWeeklyReportChina", "TeleSalesRow");//23;
        public static readonly string TSLocalCurrency = ParameterConfig.GetStringValue("FunnelWeeklyReportChina", "TSLocalCurrency") ?? "A36";
        public static readonly int TSLocalRow = ParameterConfig.GetIntValue("FunnelWeeklyReportChina", "TSLocalRow");//36;
        public static readonly string TotalLocalCurrency = ParameterConfig.GetStringValue("FunnelWeeklyReportChina", "TotalLocalCurrency") ?? "A39";
        public static readonly int TotalLocalRow = ParameterConfig.GetIntValue("FunnelWeeklyReportChina", "TotalLocalRow");//39;
        public static readonly int SourceRowCount = ParameterConfig.GetIntValue("FunnelWeeklyReportChina", "SourceRowCount");//40;
        public static readonly int RowCount = ParameterConfig.GetIntValue("FunnelWeeklyReportChina", "RowCount");//40;
        public static readonly int MergedRowCount = SourceRowCount + 1;
        public static readonly string BudgetRate = ParameterConfig.GetStringValue("FunnelWeeklyReportChina", "BudgetRate") ?? "A41";
    }

    public class FunnelMonthlyReportParameters
    {
        public static readonly string YTDFunnel = ParameterConfig.GetStringValue("FunnelMonthlyReport", "YTDFunnel") ?? "Z";
        public static readonly string YTDKFR0 = ParameterConfig.GetStringValue("FunnelMonthlyReport", "YTDKFR0") ?? "AA";
        public static readonly string YTD1011 = ParameterConfig.GetStringValue("FunnelMonthlyReport", "YTD1011") ?? "AB";
        public static readonly string YTDACTVsKFR0 = ParameterConfig.GetStringValue("FunnelMonthlyReport", "YTDACTVsKFR0") ?? "AC";
        public static readonly string VS1011YTD = ParameterConfig.GetStringValue("FunnelMonthlyReport", "VS1011YTD") ?? "AD";
        public static readonly int RowCount = ParameterConfig.GetIntValue("FunnelMonthlyReport", "RowCount"); //53;

        public static readonly string PreviousMonthBegin = ParameterConfig.GetStringValue("FunnelMonthlyReport", "PreviousMonthBegin") ?? "B";
        public static readonly string PreviousMonthEnd = ParameterConfig.GetStringValue("FunnelMonthlyReport", "PreviousMonthEnd") ?? "M";
        public static readonly string MonthBegin = ParameterConfig.GetStringValue("FunnelMonthlyReport", "MonthBegin") ?? "N";
        public static readonly string MonthEnd = ParameterConfig.GetStringValue("FunnelMonthlyReport", "MonthEnd") ?? "Y";

        public static readonly int RententionRateRow = ParameterConfig.GetIntValue("FunnelMonthlyReport", "RententionRateRow");//15;
        public static readonly int RenewalRow = ParameterConfig.GetIntValue("FunnelMonthlyReport", "RenewalRow");//14;
        public static readonly int ChargedFTPRow = ParameterConfig.GetIntValue("FunnelMonthlyReport", "ChargedFTPRow");//12;
        public static readonly int FTPPrice = ParameterConfig.GetIntValue("FunnelMonthlyReport", "FTPPrice");//13;
        public static readonly int OnlineFTPRevenues = ParameterConfig.GetIntValue("FunnelMonthlyReport", "OnlineFTPRevenues");//26;

        //Header
        public static readonly string Header_YTDACT_CurrentFisicalYear = ParameterConfig.GetStringValue("FunnelMonthlyReport", "Header_YTDACT_CurrentFisicalYear") ?? "ACT 12/13";
        public static readonly string Header_YTDKFR_CurrentFisicalYear = ParameterConfig.GetStringValue("FunnelMonthlyReport", "Header_YTDKFR_CurrentFisicalYear") ?? "KFR0 12/13";
        public static readonly string Header_YTDACT_BeforeFisicalYear = ParameterConfig.GetStringValue("FunnelMonthlyReport", "Header_YTDACT_BeforeFisicalYear") ?? "ACT 11/12";
        public static readonly string Header_YTDACTVsKFR = ParameterConfig.GetStringValue("FunnelMonthlyReport", "Header_YTDACTVsKFR") ?? "ACT vs KFR0";
        public static readonly string Header_YTDACTVs_BeforeFisicalYear = ParameterConfig.GetStringValue("FunnelMonthlyReport", "Header_YTDACTVs_BeforeFisicalYear") ?? "ACT vs 11/12";

        //output parameters
        public static readonly int OutputRowCount = ParameterConfig.GetIntValue("FunnelMonthlyReport", "OutputRowCount");//13;

        public static readonly string OutputThisMonth = ParameterConfig.GetStringValue("FunnelMonthlyReport", "OutputThisMonth") ?? "B";
        public static readonly string OutputMonth = ParameterConfig.GetStringValue("FunnelMonthlyReport", "OutputMonth") ?? "C";
        public static readonly string OutputMonthDiff = ParameterConfig.GetStringValue("FunnelMonthlyReport", "OutputMonthDiff") ?? "D";
        public static readonly string OutputActuals = ParameterConfig.GetStringValue("FunnelMonthlyReport", "OutputActuals") ?? "E";
        public static readonly string OutputKFR1 = ParameterConfig.GetStringValue("FunnelMonthlyReport", "OutputKFR1") ?? "F";
        public static readonly string OutputDiff = ParameterConfig.GetStringValue("FunnelMonthlyReport", "OutputDiff") ?? "G";
        public static readonly string OutputPriorYear = ParameterConfig.GetStringValue("FunnelMonthlyReport", "OutputPriorYear") ?? "H";
        public static readonly string OutputPriorYearDiff = ParameterConfig.GetStringValue("FunnelMonthlyReport", "OutputPriorYearDiff") ?? "I";

        //output Header
        public static readonly string Header_OutputThisMonth = ParameterConfig.GetStringValue("FunnelMonthlyReport", "Header_OutputThisMonth") ?? "This Month";
        public static readonly string Header_OutputMonth = ParameterConfig.GetStringValue("FunnelMonthlyReport", "Header_OutputMonth") ?? "11/12 Month";
        public static readonly string Header_OutputMonthDiff = ParameterConfig.GetStringValue("FunnelMonthlyReport", "Header_OutputMonthDiff") ?? "Month Diff";
        public static readonly string Header_OutputActuals = ParameterConfig.GetStringValue("FunnelMonthlyReport", "Header_OutputActuals") ?? "Actuals";
        public static readonly string Header_OutputKFR = ParameterConfig.GetStringValue("FunnelMonthlyReport", "Header_OutputKFR") ?? "KFR0";
        public static readonly string Header_OutputDiff = ParameterConfig.GetStringValue("FunnelMonthlyReport", "Header_OutputDiff") ?? "Diff";
        public static readonly string Header_OutputPriorYear = ParameterConfig.GetStringValue("FunnelMonthlyReport", "Header_OutputPriorYear") ?? "Prior Year";
        public static readonly string Header_OutputPriorYearDiff = ParameterConfig.GetStringValue("FunnelMonthlyReport", "Header_OutputPriorYearDiff") ?? "Diff";
    }

    public class WeeklyStatisticReportParameters
    {
        public static readonly string TotalColumn = ParameterConfig.GetStringValue("WeeklyStatisticReport", "TotalColumn") ?? "X";
        public static readonly string TSColumn = ParameterConfig.GetStringValue("WeeklyStatisticReport", "TSColumn") ?? "X";
        public static readonly string OnlineColumn = ParameterConfig.GetStringValue("WeeklyStatisticReport", "OnlineColumn") ?? "X";

        public static readonly int TotalRowCount = ParameterConfig.GetIntValue("WeeklyStatisticReport", "TotalRowCount");//17;
        public static readonly int TSRowCount = ParameterConfig.GetIntValue("WeeklyStatisticReport", "TSRowCount");//17;
        public static readonly int OnlineRowCount = ParameterConfig.GetIntValue("WeeklyStatisticReport", "OnlineRowCount");//17;

        public static readonly string KFRRevenueColumn = ParameterConfig.GetStringValue("WeeklyStatisticReport", "KFRRevenueColumn") ?? "V";
        public static readonly int KFRRevenueRowCount = ParameterConfig.GetIntValue("WeeklyStatisticReport", "KFRRevenueRowCount");//17;

    }

    public class FunnelCNMiniMergeParameters
    {
        public static readonly string Online_BeginCell = ParameterConfig.GetStringValue("FunnelCNMiniMerge", "Online_BeginCell") ?? "B1";
        public static readonly string Online_EndCell = ParameterConfig.GetStringValue("FunnelCNMiniMerge", "Online_EndCell") ?? "R1";

        public static readonly string IB_BeginCell = ParameterConfig.GetStringValue("FunnelCNMiniMerge", "IB_BeginCell") ?? "S1";
        public static readonly string IB_EndCell = ParameterConfig.GetStringValue("FunnelCNMiniMerge", "IB_EndCell") ?? "AI1";

        public static readonly string WI_BeginCell = ParameterConfig.GetStringValue("FunnelCNMiniMerge", "WI_BeginCell") ?? "AJ1";
        public static readonly string WI_EndCell = ParameterConfig.GetStringValue("FunnelCNMiniMerge", "WI_EndCell") ?? "AZ1";

        public static readonly string Offline_BeginCell = ParameterConfig.GetStringValue("FunnelCNMiniMerge", "Offline_BeginCell") ?? "BA1";
        public static readonly string Offline_EndCell = ParameterConfig.GetStringValue("FunnelCNMiniMerge", "Offline_EndCell") ?? "BQ1";

        public static readonly string B2B_BeginCell = ParameterConfig.GetStringValue("FunnelCNMiniMerge", "B2B_BeginCell") ?? "BR1";
        public static readonly string B2B_EndCell = ParameterConfig.GetStringValue("FunnelCNMiniMerge", "B2B_EndCell") ?? "CH1";

        public static readonly string Others_BeginCell = ParameterConfig.GetStringValue("FunnelCNMiniMerge", "Others_BeginCell") ?? "CI1";
        public static readonly string Others_EndCell = ParameterConfig.GetStringValue("FunnelCNMiniMerge", "Others_EndCell") ?? "CY1";

        public static readonly string Referral_BeginCell = ParameterConfig.GetStringValue("FunnelCNMiniMerge", "Referral_BeginCell") ?? "DS1";
        public static readonly string Referral_EndCell = ParameterConfig.GetStringValue("FunnelCNMiniMerge", "Referral_EndCell") ?? "EF1";

        public static readonly string Upgrade_BeginCell = ParameterConfig.GetStringValue("FunnelCNMiniMerge", "Upgrade_BeginCell") ?? "EG1";
        public static readonly string Upgrade_EndCell = ParameterConfig.GetStringValue("FunnelCNMiniMerge", "Upgrade_EndCell") ?? "ET1";

        public static readonly string Renewal_BeginCell = ParameterConfig.GetStringValue("FunnelCNMiniMerge", "Renewal_BeginCell") ?? "EU1";
        public static readonly string Renewal_EndCell = ParameterConfig.GetStringValue("FunnelCNMiniMerge", "Renewal_EndCell") ?? "FH1";

        public static readonly string Total_BeginCell = ParameterConfig.GetStringValue("FunnelCNMiniMerge", "Total_BeginCell") ?? "CZ1";
        public static readonly string Total_EndCell = ParameterConfig.GetStringValue("FunnelCNMiniMerge", "Total_EndCell") ?? "DP1";

        public static readonly string MiniTSTotal_BeginCell = ParameterConfig.GetStringValue("FunnelCNMiniMerge", "MiniTSTotal_BeginCell") ?? "BR1";
        public static readonly string MiniTSTotal_EndCell = ParameterConfig.GetStringValue("FunnelCNMiniMerge", "MiniTSTotal_EndCell") ?? "CH1";

        public static readonly int TotalCity = ParameterConfig.GetIntValue("FunnelCNMiniMerge", "TotalCity");
    }
}
