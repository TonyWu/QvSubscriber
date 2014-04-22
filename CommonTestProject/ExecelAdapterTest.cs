using QlikView.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace CommonTestProject
{
    
    
    /// <summary>
    ///This is a test class for ExecelAdapterTest and is intended
    ///to contain all ExecelAdapterTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ExecelAdapterTest
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
        ///A test for ExecelAdapter Constructor
        ///</summary>
        public void ExecelAdapterConstructorTest()
        {
            ExcelMerge target = new ExcelMerge();
            //Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for MergeFiles
        ///</summary>
        [TestMethod()]
        public void MergeFilesTest()
        {
            //DateTime date =  DateTime.Now.Date.AddDays(0 - (int)DateTime.Now.DayOfWeek);

            int weekOfYear = weekofyear(DateTime.Now);

            weekOfYear = weekofyear(DateTime.Parse("2009/12/31"));
            weekOfYear = weekofyear(DateTime.Parse("2010/01/01"));
            weekOfYear = weekofyear(DateTime.Parse("2010/12/31"));

            int m = ExcelUtilies.ExcelColumnNameToIndex("AN");
            string c = ExcelUtilies.ExcelColumnIndexToName(m);

            //ExecelAdapter target = new ExecelAdapter(); // TODO: Initialize to an appropriate value
            //Dictionary<string, string> MergedFiles = new Dictionary<string,string>(); // TODO: Initialize to an appropriate value
            //MergedFiles.Add("China", @"C:\ReportExport\Etag_20110922_160228.xls");
            //MergedFiles.Add("Jan", @"C:\ReportExport\Etag_20110927_142140.xls");
            //MergedFiles.Add("USA", @"C:\ReportExport\Etag_20111009_163730.xls");
            //MergedFiles.Add("French", @"C:\ReportExport\Etag_20111011_140943.xls");

            //string outputFile = @"C:\ReportExport\MergedReport" + DateTime.Now.Second + ".xls"; // TODO: Initialize to an appropriate value
            //bool expected = true; // TODO: Initialize to an appropriate value
            //bool actual;
            //actual = target.MergeFiles(MergedFiles, outputFile);
            //Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        public static int weekofyear(DateTime dtime)
        {
            int weeknum = 0;

            DateTime beginDate = new DateTime(dtime.Year, 1, 1);
            DateTime endDate = new DateTime(dtime.Year, 12, 31);

            if (endDate.DayOfWeek() < 7 && (endDate.DayOfYear - dtime.DayOfYear) < 6)
                weeknum = 1;
            else
                weeknum = (dtime.DayOfYear - dtime.DayOfWeek() - (7 - beginDate.DayOfWeek() + 1)) / 7 + 2;

            return weeknum;
        } 
    }
}
