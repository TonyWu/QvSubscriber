using QlikView.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace CommonTestProject
{
    
    
    /// <summary>
    ///This is a test class for DateFunctionsTest and is intended
    ///to contain all DateFunctionsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DateFunctionsTest
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
        ///A test for CalculateExpression
        ///</summary>
        [TestMethod()]
        public void CalculateExpressionTest()
        {
            string expression = "LastMonthendDate"; // TODO: Initialize to an appropriate value
            List<ExpressionCondition> expected = null; // TODO: Initialize to an appropriate value
            List<ExpressionCondition> actual;
            actual = DateFunctions.CalculateExpression(expression);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetValueFromExpression
        ///</summary>
        [TestMethod()]
        public void GetValueFromExpressionTest()
        {
            string p = "LastWeekendDate"; // TODO: Initialize to an appropriate value
            string expected = "2012/03/11"; // TODO: Initialize to an appropriate value
            string actual;
            actual = DateFunctions.GetValueFromExpression(p).ToString("yyyy/MM/dd");
            Assert.AreEqual(expected, actual);
            actual = DateFunctions.GetValueFromExpression(p).ToString("yyyy/MM/dd 00:00:00");
            Assert.AreEqual("2012/03/11 00:00:00", actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
