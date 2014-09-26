using QlikView.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace CommonTestProject
{
    
    
    /// <summary>
    ///This is a test class for ChinaSalesFunnelDashTest and is intended
    ///to contain all ChinaSalesFunnelDashTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ChinaSalesFunnelDashTest
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
            ChinaSalesFunnelDash target = new ChinaSalesFunnelDash(); // TODO: Initialize to an appropriate value
            Dictionary<string, ReportContext> MergedFiles = new Dictionary<string,ReportContext>(); // TODO: Initialize to an appropriate value
            target.Logger = new QVConfigLog();

            MergedFiles.Add("ChinaSales", new ReportContext()
            {
                Name = "ChinaDash",
                OutputFullName = @"C:\Git_Labs\qvsubsrciptiontool\_output\Summy_Target_20130709_042708.xls",
                HtmlFormat = "",
                ReportType = ReportType.Excel,
                Description = ""
            });
            string mergedFile = @"C:\Git_Labs\qvsubsrciptiontool\_output\ChinaDash.xls"; // TODO: Initialize to an appropriate value
            string outputFile = string.Empty; // TODO: Initialize to an appropriate value
            string outputFileExpected = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.MergeFiles(MergedFiles, mergedFile, out outputFile);
            //Assert.AreEqual(outputFileExpected, outputFile);
            //Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
