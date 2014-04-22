using QlikView.ReportVerification;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace QlikView.ReportVerificationTest
{
    
    
    /// <summary>
    ///This is a test class for ReportDataReaderTest and is intended
    ///to contain all ReportDataReaderTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ReportDataReaderTest
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
        ///A test for GetDataSet
        ///</summary>
        [TestMethod()]
        public void GetDataSetTest()
        {
            ReportDataReader target = new ReportDataReader(); // TODO: Initialize to an appropriate value
            string file = @"D:\ReportExport\UnitTest\ReportVerification\FunnelMonthly_2012Month1.xls"; // TODO: Initialize to an appropriate value
            int headerRowNumber = 3; // TODO: Initialize to an appropriate value
            //ReportDataSet expected = null; // TODO: Initialize to an appropriate value
            ReportDataSet actual;
            //actual = target.GetDataSet(file, headerRowNumber);
            //Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
