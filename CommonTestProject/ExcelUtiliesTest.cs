using QlikView.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CommonTestProject
{
    
    
    /// <summary>
    ///This is a test class for ExcelUtiliesTest and is intended
    ///to contain all ExcelUtiliesTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ExcelUtiliesTest
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
        ///A test for ExcelColumnIndexToName
        ///</summary>
        [TestMethod()]
        public void ExcelColumnIndexToNameTest()
        {
            DateTime date = DateTime.Now.Date.AddDays(0 - DateTime.Now.Day);
            int Index = 28; // TODO: Initialize to an appropriate value
            string expected = "AB"; // TODO: Initialize to an appropriate value
            string actual;
            actual = ExcelUtilies.ExcelColumnIndexToName(Index);
            Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ExcelColumnNameToIndex
        ///</summary>
        [TestMethod()]
        public void ExcelColumnNameToIndexTest()
        {
            string name = "AB"; // TODO: Initialize to an appropriate value
            int expected = 28; // TODO: Initialize to an appropriate value
            int actual;
            actual = ExcelUtilies.ExcelColumnNameToIndex(name);
            Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for LoadMergeProvider
        ///</summary>
        [TestMethod()]
        public void LoadMergeProviderTest()
        {
            string className = "QlikView.Common.FunnelReportMerge"; // TODO: Initialize to an appropriate value
            //IExcelMerge expected = null; // TODO: Initialize to an appropriate value
            IExcelMerge actual;
            //actual = ExcelUtilies.LoadMergeProvider(className);
            //Assert.IsNotNull(actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for LoadMergeProvider
        ///</summary>
        [TestMethod()]
        public void LoadMergeProviderTest1()
        {
            string className = string.Empty; // TODO: Initialize to an appropriate value           
            IExcelMerge actual;
            //actual = ExcelUtilies.LoadMergeProvider("FunnelRevenue");
            //Assert.IsNotNull(actual);

            string str = DateTime.Now.ToString("yyyyYear_MM") + "Month";

        }
    }
}
