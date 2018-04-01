using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Common;

namespace InvoicingSystemWeb.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string s=MD5HelpClass.CreateMD5Hash("1234");
        }
    }
}
