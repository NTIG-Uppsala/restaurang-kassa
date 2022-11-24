using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("TestProject")]

namespace TestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var bruh = new WindowsFormsApp1.Form1().position;
            Assert.AreEqual(bruh, 100);
            Console.WriteLine(bruh);
        }
    }
}