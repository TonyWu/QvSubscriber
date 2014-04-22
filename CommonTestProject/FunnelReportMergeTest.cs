using QlikView.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace CommonTestProject
{
    
    
    /// <summary>
    ///This is a test class for FunnelReportMergeTest and is intended
    ///to contain all FunnelReportMergeTest Unit Tests
    ///</summary>
    [TestClass()]
    public class FunnelReportMergeTest
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
            FunnelWeeklyMerge target = new FunnelWeeklyMerge(); // TODO: Initialize to an appropriate value
            Dictionary<string, ReportContext> MergedFiles = new Dictionary<string, ReportContext>(); // TODO: Initialize to an appropriate value

            //week
            MergedFiles.Add("Funnel_ByWeek_Brazil", new ReportContext() {
                Name = "Funnel_ByWeek_Brazil",
                Description = "Brazil",
                ReportType = QlikView.Common.ReportType.Excel,
                HtmlFormat = string.Empty,
                OutputFullName = @"F:\ReportExport\UnitTest\test\FunnelByWeek_Brazil.xls"
            });
            MergedFiles.Add("Funnel_ByWeek_China", new ReportContext()
            {
                Name = "Funnel_ByWeek_China",
                Description = "China",
                ReportType = QlikView.Common.ReportType.Excel,
                HtmlFormat = string.Empty,
                OutputFullName = @"F:\ReportExport\UnitTest\test\FunnelByWeek_China.xls"
            });
          

            //month
            MergedFiles.Add("Funnel_ByMonth_Brazil", new ReportContext()
            {
                Name = "Funnel_ByMonth_Brazil",
                Description = "Brazil",
                ReportType = QlikView.Common.ReportType.Excel,
                HtmlFormat = string.Empty,
                OutputFullName = @"F:\ReportExport\UnitTest\test\FunnelByMonth_Brazil.xls"
            });
            MergedFiles.Add("Funnel_ByMonth_China", new ReportContext()
            {
                Name = "Funnel_ByMonth_China",
                Description = "China",
                ReportType = QlikView.Common.ReportType.Excel,
                HtmlFormat = string.Empty,
                OutputFullName = @"F:\ReportExport\UnitTest\test\FunnelByMonth_China.xls"
            });

            //YTDKFR0
            MergedFiles.Add("Funnel_YTDKFR0_Brazil", new ReportContext()
            {
                Name = "Funnel_YTDKFR0_Brazil",
                Description = "Brazil",
                ReportType = QlikView.Common.ReportType.Excel,
                HtmlFormat = string.Empty,
                OutputFullName = @"F:\ReportExport\UnitTest\test\FunnelYTDKFR0_Brazil.xls"
            });
            MergedFiles.Add("Funnel_YTDKFR0_China", new ReportContext()
            {
                Name = "Funnel_YTDKFR0_China",
                Description = "China",
                ReportType = QlikView.Common.ReportType.Excel,
                HtmlFormat = string.Empty,
                OutputFullName = @"F:\ReportExport\UnitTest\test\FunnelYTDKFR0_China.xls"
            });

            //YTDACT0
            MergedFiles.Add("Funnel_YTDACTVsKFR0_Brazil", new ReportContext()
            {
                Name = "Funnel_YTDACTVsKFR0_Brazil",
                Description = "Brazil",
                ReportType = QlikView.Common.ReportType.Excel,
                HtmlFormat = string.Empty,
                OutputFullName = @"F:\ReportExport\UnitTest\test\FunnelYTDACTVsKFR0_Brazil.xls"
            });
            MergedFiles.Add("Funnel_YTDACTVsKFR0_China", new ReportContext()
            {
                Name = "Funnel_YTDACTVsKFR0_China",
                Description = "China",
                ReportType = QlikView.Common.ReportType.Excel,
                HtmlFormat = string.Empty,
                OutputFullName = @"F:\ReportExport\UnitTest\test\FunnelYTDACTVsKFR0_China.xls"
            });

            ////YTDKFR1
            //MergedFiles.Add("Funnel_YTDKFR1_Brazil", new ReportContext()
            //{
            //    Name = "Funnel_YTDKFR1_Brazil",
            //    Description = "Brazil",
            //    ReportType = QlikView.Common.ReportType.Excel,
            //    HtmlFormat = string.Empty,
            //    OutputFullName = @"F:\ReportExport\UnitTest\test\FunnelYTDKFR1_Brazil.xls"
            //});
            //MergedFiles.Add("Funnel_YTDKFR1_China", new ReportContext()
            //{
            //    Name = "Funnel_YTDKFR1_China",
            //    Description = "China",
            //    ReportType = QlikView.Common.ReportType.Excel,
            //    HtmlFormat = string.Empty,
            //    OutputFullName = @"F:\ReportExport\UnitTest\test\FunnelYTDKFR1_China.xls"
            //});

            //YTDACT1
            //MergedFiles.Add("Funnel_YTDACTVsKFR1_Brazil", new ReportContext()
            //{
            //    Name = "Funnel_YTDACTVsKFR1_Brazil",
            //    Description = "Brazil",
            //    ReportType = QlikView.Common.ReportType.Excel,
            //    HtmlFormat = string.Empty,
            //    OutputFullName = @"F:\ReportExport\UnitTest\test\FunnelYTDACTVsKFR1_Brazil.xls"
            //});
            //MergedFiles.Add("Funnel_YTDACTVsKFR1_China", new ReportContext()
            //{
            //    Name = "Funnel_YTDACTVsKFR1_China",
            //    Description = "China",
            //    ReportType = QlikView.Common.ReportType.Excel,
            //    HtmlFormat = string.Empty,
            //    OutputFullName = @"F:\ReportExport\UnitTest\test\FunnelYTDACTVsKFR1_China.xls"
            //});

            //YTDKFR2
            //MergedFiles.Add("Funnel_YTDKFR2_Brazil", new ReportContext()
            //{
            //    Name = "Funnel_YTDKFR2_Brazil",
            //    Description = "Brazil",
            //    ReportType = QlikView.Common.ReportType.Excel,
            //    HtmlFormat = string.Empty,
            //    OutputFullName = @"F:\ReportExport\UnitTest\test\FunnelYTDKFR2_Brazil.xls"
            //});
            //MergedFiles.Add("Funnel_YTDKFR2_China", new ReportContext()
            //{
            //    Name = "Funnel_YTDKFR2_China",
            //    Description = "China",
            //    ReportType = QlikView.Common.ReportType.Excel,
            //    HtmlFormat = string.Empty,
            //    OutputFullName = @"F:\ReportExport\UnitTest\test\FunnelYTDKFR2_China.xls"
            //});

            //YTDACT1
            //MergedFiles.Add("Funnel_YTDACTVsKFR2_Brazil", new ReportContext()
            //{
            //    Name = "Funnel_YTDACTVsKFR2_Brazil",
            //    Description = "Brazil",
            //    ReportType = QlikView.Common.ReportType.Excel,
            //    HtmlFormat = string.Empty,
            //    OutputFullName = @"F:\ReportExport\UnitTest\test\FunnelYTDACTVsKFR2_Brazil.xls"
            //});
            //MergedFiles.Add("Funnel_YTDACTVsKFR2_China", new ReportContext()
            //{
            //    Name = "Funnel_YTDACTVsKFR2_China",
            //    Description = "China",
            //    ReportType = QlikView.Common.ReportType.Excel,
            //    HtmlFormat = string.Empty,
            //    OutputFullName = @"F:\ReportExport\UnitTest\test\FunnelYTDACTVsKFR2_China.xls"
            //});

            //YTDFunnel
            MergedFiles.Add("Funnel_YTDFunnel_Brazil", new ReportContext()
            {
                Name = "Funnel_YTDFunnel_Brazil",
                Description = "Brazil",
                ReportType = QlikView.Common.ReportType.Excel,
                HtmlFormat = string.Empty,
                OutputFullName = @"F:\ReportExport\UnitTest\test\FunnelYTDFunnel_Brazil.xls"
            });
            MergedFiles.Add("Funnel_YTDFunnel_China", new ReportContext()
            {
                Name = "Funnel_YTDFunnel_China",
                Description = "China",
                ReportType = QlikView.Common.ReportType.Excel,
                HtmlFormat = string.Empty,
                OutputFullName = @"F:\ReportExport\UnitTest\test\FunnelYTDFunnel_China.xls"
            });

           

            //VS week
            MergedFiles.Add("Funnel_VS1011Week_Brazil", new ReportContext()
            {
                Name = "Funnel_VS1011Week_Brazil",
                Description = "Brazil",
                ReportType = QlikView.Common.ReportType.Excel,
                HtmlFormat = string.Empty,
                OutputFullName = @"F:\ReportExport\UnitTest\test\FunnelVs1011Week_Brazil.xls"
            });
            MergedFiles.Add("Funnel_VS1011Week_China", new ReportContext()
            {
                Name = "Funnel_VS1011Week_China",
                Description = "China",
                ReportType = QlikView.Common.ReportType.Excel,
                HtmlFormat = string.Empty,
                OutputFullName = @"F:\ReportExport\UnitTest\test\FunnelVs1011Week_China.xls"
            });

            //VS YTD
            MergedFiles.Add("Funnel_VS1011YTD_Brazil", new ReportContext()
            {
                Name = "Funnel_VS1011YTD_Brazil",
                Description = "Brazil",
                ReportType = QlikView.Common.ReportType.Excel,
                HtmlFormat = string.Empty,
                OutputFullName = @"F:\ReportExport\UnitTest\test\FunnelVs1011YTD_Brazil.xls"
            });
            MergedFiles.Add("Funnel_VS1011YTD_China", new ReportContext()
            {
                Name = "Funnel_VS1011YTD_China",
                Description = "China",
                ReportType = QlikView.Common.ReportType.Excel,
                HtmlFormat = string.Empty,
                OutputFullName = @"F:\ReportExport\UnitTest\test\FunnelVs1011YTD_China.xls"
            });
           
            //1011 YTD
            MergedFiles.Add("Funnel_1011YTD_Brazil", new ReportContext()
            {
                Name = "Funnel_1011YTD_Brazil",
                Description = "Brazil",
                ReportType = QlikView.Common.ReportType.Excel,
                HtmlFormat = string.Empty,
                OutputFullName = @"F:\ReportExport\UnitTest\test\Funnel1011YTD_Brazil.xls"
            });
            MergedFiles.Add("Funnel_1011YTD_China", new ReportContext()
            {
                Name = "Funnel_1011YTD_China",
                Description = "China",
                ReportType = QlikView.Common.ReportType.Excel,
                HtmlFormat = string.Empty,
                OutputFullName = @"F:\ReportExport\UnitTest\test\Funnel1011YTD_China.xls"
            });

            //1011 week
            MergedFiles.Add("Funnel_1011Week_Brazil", new ReportContext()
            {
                Name = "Funnel_1011Week_Brazil",
                Description = "Brazil",
                ReportType = QlikView.Common.ReportType.Excel,
                HtmlFormat = string.Empty,
                OutputFullName = @"F:\ReportExport\UnitTest\test\Funnel1011Week_Brazil.xls"
            });
            MergedFiles.Add("Funnel_1011Week_China", new ReportContext()
            {
                Name = "Funnel_1011Week_China",
                Description = "China",
                ReportType = QlikView.Common.ReportType.Excel,
                HtmlFormat = string.Empty,
                OutputFullName = @"F:\ReportExport\UnitTest\test\Funnel1011Week_China.xls"
            });

            //Add CN TS Mini
            //MergedFiles.Add("Funnel_CNTSMini_OnlineFTPRevenues", new ReportContext()
            //{
            //    Name = "Funnel_CNTSMini_OnlineFTPRevenues",
            //    Description = "CNTSMini_OnlineFTPRevenues",
            //    ReportType = QlikView.Common.ReportType.Excel,
            //    HtmlFormat = string.Empty,
            //    OutputFullName = @"F:\ReportExport\UnitTest\test\Funnel_CNSMiniOnlineRevenues_20111214_021135.xls"
            //});

            string outputFile = @"F:\ReportExport\UnitTest\MergedReport" + DateTime.Now.Second + ".xls"; ; // TODO: Initialize to an appropriate value
            
            bool expected = true; // TODO: Initialize to an appropriate value
            bool actual;
            string mergedFile;
            actual = target.MergeFiles(MergedFiles, outputFile, out mergedFile);
            Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
