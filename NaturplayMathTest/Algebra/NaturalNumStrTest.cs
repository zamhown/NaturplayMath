using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NaturplayMath.Algebra;
using NaturplayMath.Algebra.Scalar.NumberString;

namespace NaturplayMathTest.Algebra
{
    [TestClass]
    public class NaturalNumStrTest
    {
        /// <summary>
        /// 最大公约数测试
        /// </summary>
        [TestMethod]
        public void GCDTest()
        {
            var a1 = NaturalNumStr.GreatestCommonDivisor(16, 24);
            Assert.AreEqual(a1.ToString(), "8");
            a1 = NaturalNumStr.GreatestCommonDivisor(13, 24);
            Assert.AreEqual(a1.ToString(), "1");

            var s = new OperationSpace(10, 16);
            a1 = NaturalNumStr.GreatestCommonDivisor(new NaturalNumStr("90", s), new NaturalNumStr("60", s));
            Assert.AreEqual(a1.ToString(), "30");
        }
        /// <summary>
        /// 最小公倍数测试
        /// </summary>
        [TestMethod]
        public void LCMTest()
        {
            var a1 = NaturalNumStr.LeastCommonMultiple(16, 24);
            Assert.AreEqual(a1.ToString(), "48");

            var s = new OperationSpace(10, 16);
            a1 = NaturalNumStr.LeastCommonMultiple(new NaturalNumStr("90", s), new NaturalNumStr("60", s));
            Assert.AreEqual(a1.ToString(), "120");
        }
    }
}
