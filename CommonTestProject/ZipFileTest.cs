using QlikView.Common.Zip;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CommonTestProject
{
    
    
    /// <summary>
    ///This is a test class for ZipFileTest and is intended
    ///to contain all ZipFileTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ZipFileTest
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
        ///A test for AddFileToZip
        ///</summary>
        [TestMethod()]
        public void AddFileToZipTest()
        {
            string zipFilename = @"C:\TFS\ETTfsApp\Englishtown\Team_Systems_System\Tools\QlikViewExport\CommonTestProject\test.zip"; // TODO: Initialize to an appropriate value
            string fileToAdd = @"C:\TFS\ETTfsApp\Englishtown\Team_Systems_System\Tools\QlikViewExport\CommonTestProject\FunnelCNMini_20130328.xls"; // TODO: Initialize to an appropriate value
            //ZipFile.AddFileToZip(zipFilename, fileToAdd);

            ZipHelper.ZipFile(fileToAdd,zipFilename, 1);
            //Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}
