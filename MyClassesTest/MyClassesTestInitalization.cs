using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyClassesTest
{
    /// <summary>
    /// Assembly Initilize and Cleanup Methods
    /// </summary>
    [TestClass]
    public class MyClassesTestInitalization
    {
        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext testContext)
        {
            testContext.WriteLine("In the Assembly Initalize method.");
            // TODO: Create resources needed for your tests.
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            // TODO: Cleanup any resources used by your tests.
        }
    }
}
