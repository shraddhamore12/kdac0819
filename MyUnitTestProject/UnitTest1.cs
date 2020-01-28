using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySecurityLib;

namespace MyUnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string result=Security.GetOTP("1", 5);
            Assert.AreNotEqual(String.Empty,result);
        }

        [TestMethod]
        public void TestMethod2()
        {
            string pwd = "sunbeam";
            string encData = null;
            bool result = Security.Encrypt(pwd,out encData);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void TestMethod3()
        {
            string pwd = "tF747AD6IYo=";
            string decData = null;
            bool result = Security.Decrypt(pwd, out decData);
            Assert.AreEqual(true, result);
        }
    }
}
