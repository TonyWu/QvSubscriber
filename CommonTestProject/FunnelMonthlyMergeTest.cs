using QlikView.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace CommonTestProject
{
    
    
    /// <summary>
    ///This is a test class for FunnelMonthlyMergeTest and is intended
    ///to contain all FunnelMonthlyMergeTest Unit Tests
    ///</summary>
    [TestClass()]
    public class FunnelMonthlyMergeTest
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
            FunnelMonthlyMerge target = new FunnelMonthlyMerge(); // TODO: Initialize to an appropriate value
            Dictionary<string, ReportContext> MergedFiles = new Dictionary<string,ReportContext>(); // TODO: Initialize to an appropriate value

            //month
            MergedFiles.Add("FunnelMonthly_ByMonth_Brazil", new ReportContext()
            {
                Name = "FunnelMonthly_ByMonth_Brazil",
                Description = "Brazil",
                ReportType = QlikView.Common.ReportType.Excel,
                HtmlFormat = string.Empty,
                OutputFullName = @"D:\ReportExport\UnitTest\FunnelMonthly\FunnelMonthly_ByMonth_Brazil_11.xls"
            });
            MergedFiles.Add("FunnelMonthly_ByMonth_Mexico", new ReportContext()
            {
                Name = "FunnelMonthly_ByMonth_Mexico",
                Description = "Mexico",
                ReportType = QlikView.Common.ReportType.Excel,
                HtmlFormat = string.Empty,
                OutputFullName = @"D:\ReportExport\UnitTest\FunnelMonthly\FunnelMonthly_ByMonth_Mexico_11.xls"
            });

            //YTDACT
            MergedFiles.Add("FunnelMonthly_YTDACTVsKFRO_Brazil", new ReportContext()
            {
                Name = "FunnelMonthly_YTDACTVsKFRO_Brazil",
                Description = "Brazil",
                ReportType = QlikView.Common.ReportType.Excel,
                HtmlFormat = string.Empty,
                OutputFullName = @"D:\ReportExport\UnitTest\FunnelMonthly\FunnelMonthly_YTDACTVsKFRO_Brazil_11.xls"
            });
            MergedFiles.Add("FunnelMonthly_YTDACTVsKFRO_Mexico", new ReportContext()
            {
                Name = "FunnelMonthly_YTDACTVsKFRO_Mexico",
                Description = "Mexico",
                ReportType = QlikView.Common.ReportType.Excel,
                HtmlFormat = string.Empty,
                OutputFullName = @"D:\ReportExport\UnitTest\FunnelMonthly\FunnelMonthly_YTDACTVsKFRO_Mexico_11.xls"
            });

            //YTDFunnel
            MergedFiles.Add("FunnelMonthly_YTDFunnel_Brazil", new ReportContext()
            {
                Name = "FunnelMonthly_YTDFunnel_Brazil",
                Description = "Brazil",
                ReportType = QlikView.Common.ReportType.Excel,
                HtmlFormat = string.Empty,
                OutputFullName = @"D:\ReportExport\UnitTest\FunnelMonthly\FunnelMonthly_YTDFunnel_Brazil_11.xls"
            });
            MergedFiles.Add("FunnelMonthly_YTDFunnel_Mexico", new ReportContext()
            {
                Name = "FunnelMonthly_YTDFunnel_Mexico",
                Description = "Mexico",
                ReportType = QlikView.Common.ReportType.Excel,
                HtmlFormat = string.Empty,
                OutputFullName = @"D:\ReportExport\UnitTest\FunnelMonthly\FunnelMonthly_YTDFunnel_Mexico_11.xls"
            });

            //YTDKFR0
            MergedFiles.Add("FunnelMonthly_YTDKFRO_Brazil", new ReportContext()
            {
                Name = "FunnelMonthly_YTDKFRO_Brazil",
                Description = "Brazil",
                ReportType = QlikView.Common.ReportType.Excel,
                HtmlFormat = string.Empty,
                OutputFullName = @"D:\ReportExport\UnitTest\FunnelMonthly\FunnelMonthly_YTDKFRO_Brazil_11.xls"
            });
            MergedFiles.Add("FunnelMonthly_YTDKFRO_Mexico", new ReportContext()
            {
                Name = "FunnelMonthly_YTDKFRO_Mexico",
                Description = "Mexico",
                ReportType = QlikView.Common.ReportType.Excel,
                HtmlFormat = string.Empty,
                OutputFullName = @"D:\ReportExport\UnitTest\FunnelMonthly\FunnelMonthly_YTDKFRO_Mexico_11.xls"
            });
                    

            //VS YTD
            MergedFiles.Add("FunnelMonthly_VS1011YTD_Brazil", new ReportContext()
            {
                Name = "FunnelMonthly_VS1011YTD_Brazil",
                Description = "Brazil",
                ReportType = QlikView.Common.ReportType.Excel,
                HtmlFormat = string.Empty,
                OutputFullName = @"D:\ReportExport\UnitTest\FunnelMonthly\FunnelMonthly_VS1011YTD_Brazil_11.xls"
            });
            MergedFiles.Add("FunnelMonthly_VS1011YTD_Mexico", new ReportContext()
            {
                Name = "FunnelMonthly_VS1011YTD_Mexico",
                Description = "Mexico",
                ReportType = QlikView.Common.ReportType.Excel,
                HtmlFormat = string.Empty,
                OutputFullName = @"D:\ReportExport\UnitTest\FunnelMonthly\FunnelMonthly_VS1011YTD_Mexico_11.xls"
            });

            //1011 YTD
            MergedFiles.Add("FunnelMonthly_1011YTD_Brazil", new ReportContext()
            {
                Name = "FunnelMonthly_1011YTD_Brazil",
                Description = "Brazil",
                ReportType = QlikView.Common.ReportType.Excel,
                HtmlFormat = string.Empty,
                OutputFullName = @"D:\ReportExport\UnitTest\FunnelMonthly\FunnelMonthly_1011YTD_Brazil_11.xls"
            });
            MergedFiles.Add("FunnelMonthly_1011YTD_Mexico", new ReportContext()
            {
                Name = "FunnelMonthly_1011YTD_Mexico",
                Description = "Mexico",
                ReportType = QlikView.Common.ReportType.Excel,
                HtmlFormat = string.Empty,
                OutputFullName = @"D:\ReportExport\UnitTest\FunnelMonthly\FunnelMonthly_1011YTD_Mexico_11.xls"
            });

            this.PopulateOutputReport(MergedFiles);

            string outputFile = @"D:\ReportExport\UnitTest\FunnelMonthly" + DateTime.Now.Second + ".xls"; ; // TODO: Initialize to an appropriate value

            bool expected = true; // TODO: Initialize to an appropriate value
            bool actual;
            string mergedFile;
            actual = target.MergeFiles(MergedFiles, outputFile, out mergedFile);
            Assert.AreEqual(expected, actual);
        }


        /// <summary>
        ///A test for MergedOutputFiles
        ///</summary>
        [TestMethod()]
        public void MergedOutputFilesTest()
        {
            FunnelMonthlyOutputHelper target = new FunnelMonthlyOutputHelper(); // TODO: Initialize to an appropriate value

            Dictionary<string, ReportContext> MergedFiles = new Dictionary<string, ReportContext>(); // TODO: Initialize to an appropriate value
            this.PopulateOutputReport(MergedFiles);

            string outputFile = @"D:\ReportExport\UnitTest\FunnelMonthly_Output" + DateTime.Now.Second + ".xls"; ; // TODO: Initialize to an appropriate value

            target.MergedOutputFilesUnitTest(MergedFiles, outputFile);
            //Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        private void PopulateOutputReport(Dictionary<string, ReportContext> MergedFiles)
        {
            List<string> category = new List<string>()
            {
                "ThisMonth",
                "1011Month",
                "MonthDiff",
                "Actuals",
                "KFR1",
                "Diff",
                "PriorYear",
                "PriorYearDiff"
            };

            List<string> country = new List<string>()
            {
                "Japan",
                "Korea",
                "Total",
                "Brazil",
                "Spain",
                "MEAST",
                "ROE",
                "Italy",
                "Germany",
                "France",
                "Europe",
                "US",
                "Mexico",
                "Mexico+US",
                "ROA",
                "ROLA",
                "ROW",
                "Thailand"
            };

            for (int i = 0; i < category.Count; i++)
            {
                for (int j = 0; j < country.Count; j++)
                {
                    string name = string.Format("Funnel_Output_{0}_{1}", category[i], country[j]);
                    string file = string.Format(@"D:\ReportExport\UnitTest\FunnelMonthly\Funnel_Output_{0}_{1}.xls", category[i], country[j]);

                    MergedFiles.Add(name, new ReportContext()
                    {
                        Name = name,
                        Description = country[j],
                        ReportType = ReportType.Excel,
                        HtmlFormat = string.Empty,
                        OutputFullName = file
                    });
                }
            }
        }
    }
}
