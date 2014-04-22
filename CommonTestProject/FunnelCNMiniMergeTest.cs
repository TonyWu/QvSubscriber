using QlikView.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace CommonTestProject
{
    
    
    /// <summary>
    ///This is a test class for FunnelCNMiniMergeTest and is intended
    ///to contain all FunnelCNMiniMergeTest Unit Tests
    ///</summary>
    [TestClass()]
    public class FunnelCNMiniMergeTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for MergeFiles
        ///</summary>
        [TestMethod()]
        public void MergeFilesTest()
        {
            FunnelCNMiniMerge target = new FunnelCNMiniMerge(); // TODO: Initialize to an appropriate value
            Dictionary<string, ReportContext> MergedFiles = new Dictionary<string, ReportContext>(); // TODO: Initialize to an appropriate value

            MergedFiles.Add("FunnelCNMini_DetailNormalWeekly_CQ", new ReportContext()
            {
                Name = "FunnelCNMini_DetailNormalWeekly_CQ",
                Description = "CQ",
                ReportType = QlikView.Common.ReportType.Excel,
                HtmlFormat = string.Empty,
                OutputFullName = @"F:\ReportExport\UnitTest\ChinaMini\MiniCenterNormalWeekly_CQ.xls"
            });

            MergedFiles.Add("FunnelCNMini_DetailSpecialWeekly_CQ", new ReportContext()
            {
                Name = "FunnelCNMini_DetailSpecialWeekly_CQ",
                Description = "CQ",
                ReportType = QlikView.Common.ReportType.Excel,
                HtmlFormat = string.Empty,
                OutputFullName = @"F:\ReportExport\UnitTest\ChinaMini\MiniCenterSPWeekly_CQ.xls"
            });

            MergedFiles.Add("FunnelCNMini_DetailDaily_CQ", new ReportContext()
            {
                Name = "FunnelCNMini_DetailDaily_CQ",
                Description = "CQ",
                ReportType = QlikView.Common.ReportType.Excel,
                HtmlFormat = string.Empty,
                OutputFullName = @"F:\ReportExport\UnitTest\ChinaMini\MiniCenterDaily_CQ.xls"
            });

            MergedFiles.Add("FunnelCNMini_TSNormalWeekly_CQ", new ReportContext()
            {
                Name = "FunnelCNMini_TSNormalWeekly_CQ",
                Description = "CQ",
                ReportType = QlikView.Common.ReportType.Excel,
                HtmlFormat = string.Empty,
                OutputFullName = @"F:\ReportExport\UnitTest\ChinaMini\MiniTSNormalWeekly_CQ.xls"
            });

            MergedFiles.Add("FunnelCNMini_TSDaily_CQ", new ReportContext()
            {
                Name = "FunnelCNMini_TSDaily_CQ",
                Description = "CQ",
                ReportType = QlikView.Common.ReportType.Excel,
                HtmlFormat = string.Empty,
                OutputFullName = @"F:\ReportExport\UnitTest\ChinaMini\MiniTSDaily_CQ.xls"
            });

            MergedFiles.Add("FunnelCNMini_Summary_Monthly", new ReportContext()
            {
                Name = "FunnelCNMini_Summary_Monthly",
                Description = "Summary",
                ReportType = QlikView.Common.ReportType.Excel,
                HtmlFormat = string.Empty,
                OutputFullName = @"F:\ReportExport\UnitTest\ChinaMini\Summy_Monthly.xls"
            });

            MergedFiles.Add("FunnelCNMini_Summary_Daily", new ReportContext()
            {
                Name = "FunnelCNMini_Summary_Daily",
                Description = "Summary",
                ReportType = QlikView.Common.ReportType.Excel,
                HtmlFormat = string.Empty,
                OutputFullName = @"F:\ReportExport\UnitTest\ChinaMini\Summy_Daily.xls"
            });

            MergedFiles.Add("FunnelCNMini_Summary_Target", new ReportContext()
            {
                Name = "FunnelCNMini_Summary_Target",
                Description = "Summary",
                ReportType = QlikView.Common.ReportType.Excel,
                HtmlFormat = string.Empty,
                OutputFullName = @"F:\ReportExport\UnitTest\ChinaMini\Summary_Target.xls"
            });

            MergedFiles.Add("FunnelCNMini_Summary_RevenueByPAC", new ReportContext()
            {
                Name = "FunnelCNMini_Summary_RevenueByPAC",
                Description = "Summary",
                ReportType = QlikView.Common.ReportType.Excel,
                HtmlFormat = string.Empty,
                OutputFullName = @"F:\ReportExport\UnitTest\ChinaMini\Summary_RevenueByPAC.xls"
            });
            

            string outputFile = @"F:\ReportExport\UnitTest\ChinaMini\CN_Mini_FunnelReport" + "_" + DateTime.Now.Second + ".xls"; // TODO: Initialize to an appropriate value
            string mergedFile = string.Empty; // TODO: Initialize to an appropriate value
            string mergedFileExpected = string.Empty; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.MergeFiles(MergedFiles, outputFile, out mergedFile);
        }
    }
}
