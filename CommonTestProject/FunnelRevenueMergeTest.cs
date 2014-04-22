using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using QlikView.Common;
using System.Collections.Generic;

namespace CommonTestProject
{
    
    
    /// <summary>
    ///This is a test class for FunnelRevenueMergeTest and is intended
    ///to contain all FunnelRevenueMergeTest Unit Tests
    ///</summary>
    [TestClass()]
    public class FunnelRevenueMergeTest
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
            FunnelRevenueMerge target = new FunnelRevenueMerge(); // TODO: Initialize to an appropriate value
            Dictionary<string, ReportContext> MergedFiles = new Dictionary<string,ReportContext>(); // TODO: Initialize to an appropriate value

            MergedFiles.Add("FunnelRevenue_Total_USD", new ReportContext()
            {
                Name = "FunnelRevenue_Total_USD",
                Description = "USD",
                ReportType = QlikView.Common.ReportType.Excel,
                HtmlFormat = string.Empty,
                OutputFullName = @"D:\ReportExport\UnitTest\FunnelRevenue\FunnelRevenue_USD_Total_20111214_020144.xls"
            });

            MergedFiles.Add("FunnelRevenue_TS_USD", new ReportContext()
            {
                Name = "FunnelRevenue_TS_USD",
                Description = "USD",
                ReportType = QlikView.Common.ReportType.Excel,
                HtmlFormat = string.Empty,
                OutputFullName = @"D:\ReportExport\UnitTest\FunnelRevenue\FunnelRevenue_USD_TS_20111214_020144.xls"
            });

            MergedFiles.Add("FunnelRevenue_Online_USD", new ReportContext()
            {
                Name = "FunnelRevenue_Online_USD",
                Description = "USD",
                ReportType = QlikView.Common.ReportType.Excel,
                HtmlFormat = string.Empty,
                OutputFullName = @"D:\ReportExport\UnitTest\FunnelRevenue\FunnelRevenue_USD_Online_20111214_020144.xls"
            });

            MergedFiles.Add("FunnelRevenue_Total_Local", new ReportContext()
            {
                Name = "FunnelRevenue_Total_Local",
                Description = "Local",
                ReportType = QlikView.Common.ReportType.Excel,
                HtmlFormat = string.Empty,
                OutputFullName = @"D:\ReportExport\UnitTest\FunnelRevenue\FunnelRevenue_Local_Total_20111214_020144.xls"
            });

            MergedFiles.Add("FunnelRevenue_TS_Local", new ReportContext()
            {
                Name = "FunnelRevenue_TS_Local",
                Description = "Local",
                ReportType = QlikView.Common.ReportType.Excel,
                HtmlFormat = string.Empty,
                OutputFullName = @"D:\ReportExport\UnitTest\FunnelRevenue\FunnelRevenue_Local_TS_20111214_020144.xls"
            });

            MergedFiles.Add("FunnelRevenue_Online_Local", new ReportContext()
            {
                Name = "FunnelRevenue_Online_Local",
                Description = "Local",
                ReportType = QlikView.Common.ReportType.Excel,
                HtmlFormat = string.Empty,
                OutputFullName = @"D:\ReportExport\UnitTest\FunnelRevenue\FunnelRevenue_Local_Online_20111214_020144.xls"
            });

            MergedFiles.Add("FunnelRevenue_KFRRevenue_NotSet", new ReportContext()
            {
                Name = "FunnelRevenue_KFRRevenue_NotSet",
                Description = "KFR Revenue",
                ReportType = QlikView.Common.ReportType.Excel,
                HtmlFormat = string.Empty,
                OutputFullName = @"D:\ReportExport\UnitTest\FunnelRevenue\FunnelRevenue_KFRRevenue_NotSet_20120325_232134.xls"
            });

            MergedFiles.Add("FunnelRevenue_KFRRevenue_CH157", new ReportContext()
            {
                Name = "FunnelRevenue_KFRRevenue_CH157",
                Description = "KFRRevenueCH157",
                ReportType = QlikView.Common.ReportType.Excel,
                HtmlFormat = string.Empty,
                OutputFullName = @"D:\ReportExport\UnitTest\FunnelRevenue\FunnelRevenue_KFRRevenue_CH157_20120328_232341.xls"
            });

            MergedFiles.Add("FunnelRevenue_KFRRevenue_CH159", new ReportContext()
            {
                Name = "FunnelRevenue_KFRRevenue_CH159",
                Description = "KFRRevenueCH159",
                ReportType = QlikView.Common.ReportType.Excel,
                HtmlFormat = string.Empty,
                OutputFullName = @"D:\ReportExport\UnitTest\FunnelRevenue\FunnelRevenue_KFRRevenue_CH159_20120328_232342.xls"
            });

            string outputFile = @"D:\ReportExport\UnitTest\FunnelRevenewReport" + DateTime.Now.Second + ".xls"; ; // TODO: Initialize to an appropriate value

            bool expected = true; // TODO: Initialize to an appropriate value
            bool actual;
            string mergedFile;
            string mergedFileExpected = string.Empty; // TODO: Initialize to an appropriate value

            actual = target.MergeFiles(MergedFiles, outputFile, out mergedFile);
            Assert.AreEqual(expected, actual);
        }
    }
}
