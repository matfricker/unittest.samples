using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyClasses;
using System;
using System.Configuration;
using System.IO;

namespace MyClassesTest
{
    [TestClass]
    public class FileProcessTest
    {
        private const string BAD_FILE_NAME = @"C:\BadFileName.bad";
        private string _GoodFileName;

        #region Class Initalize and Cleanup

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            testContext.WriteLine("In the Class Initalize");
            // TODO: Create resources needed for your tests.
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            // TODO: Cleanup any resources used by your tests.
        }

        #endregion

        #region Test Initialization and Cleanup

        [TestInitialize]
        public void TestInitialize()
        {
            if (TestContext.TestName == "FileNameDoesExist")
            {
                SetGoodFileName();
                if (!string.IsNullOrEmpty(_GoodFileName))
                {
                    TestContext.WriteLine("Creating File: " + _GoodFileName);
                    File.AppendAllText(_GoodFileName, "Some text");
                }
            }
        }


        [TestCleanup]
        public void TestCleanup()
        {
            if (TestContext.TestName == "FileNameDoesExist")
            {
                if (!string.IsNullOrEmpty(_GoodFileName))
                {
                    TestContext.WriteLine("Deleting File: " + _GoodFileName);
                    File.Delete(_GoodFileName);
                }
            }
        }

        #endregion

        public TestContext TestContext { get; set; }

        [TestMethod]
        [Priority(0)]
        [TestCategory("NoException")]
        [Owner("Matt Fricker")]
        public void FileNameDoesExist()
        {
            // ARRANGE
            FileProcess fp = new FileProcess();
            bool fromCall;

            // ACT
            TestContext.WriteLine("Checking File: ", _GoodFileName);
            fromCall = fp.FileExists(_GoodFileName);

            // ASSERT
            Assert.IsTrue(fromCall);
        }

        [TestMethod]
        [Priority(0)]
        [TestCategory("NoException")]
        [Owner("Matt Fricker")]
        public void FileNameDoesNotExist()
        {
            FileProcess fp = new FileProcess();
            bool fromCall;

            fromCall = fp.FileExists(BAD_FILE_NAME);

            Assert.IsFalse(fromCall);
        }

        private const string FILE_NAME = @"FileToDeploy.txt";

        [TestMethod]
        [Owner("Matt Fricker")]
        [DeploymentItem(FILE_NAME)]
        public void FileNameDoesExistUsingDeploymentItem()
        {
            FileProcess fp = new FileProcess();
            string fileName;
            bool fromCall;

            fileName = TestContext.DeploymentDirectory + @"\" + FILE_NAME;
            TestContext.WriteLine("Checking File: " + fileName);

            fromCall = fp.FileExists(fileName);

            Assert.IsTrue(fromCall);
        }

        [TestMethod]
        [Priority(1)]
        [TestCategory("Exception")]
        [Owner("Matt Fricker")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FileNameNullOrEmpty_ThrowsArgumentNullException()
        {
            FileProcess fp = new FileProcess();
            fp.FileExists("");
        }

        [TestMethod]
        [Priority(1)]
        [TestCategory("Exception")]
        [Owner("Matt Fricker")]
        public void FileNameNullOrEmpty_ThrowsArgumentNullException_UsingTryCatch()
        {
            FileProcess fp = new FileProcess();

            try
            {
                fp.FileExists("");
            }
            catch (Exception)
            {
                // The test was a sucess
                return;
            }

            Assert.Fail("Call to FileExists did not throw an ArgumentNullException.");
        }

        public void SetGoodFileName()
        {
            _GoodFileName = ConfigurationManager.AppSettings["GoodFileName"];
            if (_GoodFileName.Contains("[AppPath]"))
            {
                _GoodFileName = _GoodFileName.Replace("[AppPath]",
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            }
        }
    }
}
