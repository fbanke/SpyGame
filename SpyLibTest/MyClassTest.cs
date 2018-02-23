using System;
using SpyLib;
using NUnit.Framework;

namespace SpyLibTest
{
    [TestFixture]
    public class MyClassTest
    {
        public MyClassTest()
        {
        }
        [Test]
        public void DivideTest()
        {
            MyClass mc = new MyClass();

            int result = mc.Divide(12, 6);

            Assert.AreEqual(2, result);
            // 
            // Dividing by zero should result in -1 since the function handles this case.
            result = mc.Divide(0, 22);
            Assert.AreEqual(-1, result);
        }
    }
}
