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
            var position = new WindowsFormsApp1.Form1().position;
            Assert.AreEqual(position, 100);
            Console.WriteLine(position);
        }
    }
}