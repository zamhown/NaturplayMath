using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NaturplayMath.Algebra;
using NaturplayMath.Algebra.Scalar;
using NaturplayMath.Algebra.Scalar.NumberString;

namespace NaturplayMathTest.Algebra
{
    [TestClass]
    public class RationalNumTest
    {
        /// <summary>
        /// 加法测试
        /// </summary>
        [TestMethod]
        public void PlusTest()
        {
            var a1 = new RationalNum(1, 2) + new RationalNum(-2, -3);
            Assert.AreEqual(a1.ToString(), "7 / 6");
            a1 = new RationalNum(1, 4) + new RationalNum(-1, 2);
            Assert.AreEqual(a1.ToString(), "-1 / 4");
        }
        /// <summary>
        /// 乘法测试
        /// </summary>
        [TestMethod]
        public void MultiplyTest()
        {
            var a1 = new RationalNum(1, 2) * new RationalNum(2, 3);
            Assert.AreEqual(a1.ToString(), "1 / 3");
            a1 = new RationalNum(1, 4) * new RationalNum(-4);
            Assert.AreEqual(a1.ToString(), "-1");
        }
        /// <summary>
        /// 除法测试
        /// </summary>
        [TestMethod]
        public void DivideTest()
        {
            var a1 = new RationalNum(0.5) / new RationalNum(1, 3);
            Assert.AreEqual(a1.ToString(), "3 / 2");
            a1 = new RationalNum(2) / new RationalNum(-4);
            Assert.AreEqual(a1.ToString(), "-1 / 2");
        }
        /// <summary>
        /// 取余测试
        /// </summary>
        [TestMethod]
        public void ModTest()
        {
            var a1 = new RationalNum(3) % new RationalNum(5, 2);
            Assert.AreEqual(a1.ToString(), "1 / 2");
            a1 = new RationalNum(7, 3) % new RationalNum(1, 4);
            Assert.AreEqual(a1.ToString(), "1 / 12");
        }
    }
}
