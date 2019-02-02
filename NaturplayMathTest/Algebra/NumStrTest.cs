using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NaturplayMath.Algebra;
using NaturplayMath.Algebra.Scalar;
using NaturplayMath.Algebra.Scalar.NumberString;

namespace NaturplayMathTest.Algebra
{
    [TestClass]
    public class NumStrUnitTest
    {
        /// <summary>
        /// 计算空间转换测试
        /// </summary>
        [TestMethod]
        public void SpaceConversionTest()
        {
            var a1 = new NumStr("76.25", new OperationSpace(2, 10));
            Assert.AreEqual(a1.ChangeOperationSpace(new OperationSpace(2, 2)).ToString(), "1001100.01");
            Assert.AreEqual(a1.ChangeOperationSpace(new OperationSpace(2, 8)).ToString(), "114.2");
            Assert.AreEqual(a1.ChangeOperationSpace(new OperationSpace(2, 10)).ToString(), "76.25");
            Assert.AreEqual(a1.ChangeOperationSpace(new OperationSpace(2, 16)).ToString(), "4C.4");
            a1 = new NumStr("1001100.01", new OperationSpace(2, 2));
            Assert.AreEqual(a1.ChangeOperationSpace(new OperationSpace(2, 2)).ToString(), "1001100.01");
            Assert.AreEqual(a1.ChangeOperationSpace(new OperationSpace(2, 8)).ToString(), "114.2");
            Assert.AreEqual(a1.ChangeOperationSpace(new OperationSpace(2, 10)).ToString(), "76.25");
            Assert.AreEqual(a1.ChangeOperationSpace(new OperationSpace(2, 16)).ToString(), "4C.4");
            a1 = new NumStr("114.2", new OperationSpace(2, 8));
            Assert.AreEqual(a1.ChangeOperationSpace(new OperationSpace(2, 2)).ToString(), "1001100");
            Assert.AreEqual(a1.ChangeOperationSpace(new OperationSpace(2, 8)).ToString(), "114.2");
            Assert.AreEqual(a1.ChangeOperationSpace(new OperationSpace(2, 10)).ToString(), "76.24");
            Assert.AreEqual(a1.ChangeOperationSpace(new OperationSpace(2, 16)).ToString(), "4C.3D");
            a1 = new NumStr("4C.4", new OperationSpace(2, 16));
            Assert.AreEqual(a1.ChangeOperationSpace(new OperationSpace(2, 2)).ToString(), "1001100");
            Assert.AreEqual(a1.ChangeOperationSpace(new OperationSpace(2, 8)).ToString(), "114.17");
            Assert.AreEqual(a1.ChangeOperationSpace(new OperationSpace(2, 10)).ToString(), "76.24");
            Assert.AreEqual(a1.ChangeOperationSpace(new OperationSpace(2, 16)).ToString(), "4C.4");
        }
        /// <summary>
        /// 加法测试
        /// </summary>
        [TestMethod]
        public void PlusTest()
        {
            var a1 = new NumStr("21000") + new NumStr("3.277");
            Assert.AreEqual(a1.ToString(), "21003.277");
            a1 = new NumStr("98.023") + new NumStr("3.977");
            Assert.AreEqual(a1.ToString(), "102");
            a1 = new NumStr("-98.023") + new NumStr("3.977");
            Assert.AreEqual(a1.ToString(), "-94.046");
            a1 = new NumStr("98.023") + new NumStr("-3.977");
            Assert.AreEqual(a1.ToString(), "94.046");
            a1 = new NumStr("-98.023") + new NumStr("-3.977");
            Assert.AreEqual(a1.ToString(), "-102");

            a1 = new NumStr("-98.023");
            a1 = a1 + a1;
            Assert.AreEqual(a1.ToString(), "-196.046");

            var s = new OperationSpace(10, 16);
            a1 = new NumStr("7B", s) + new NumStr("3C", s);
            Assert.AreEqual(a1.ToString(), "B7");
        }
        /// <summary>
        /// 减法测试
        /// </summary>
        [TestMethod]
        public void MinusTest()
        {
            var a1 = new NumStr("10000") - new NumStr("0.023");
            Assert.AreEqual(a1.ToString(), "9999.977");
            a1 = new NumStr("18.023") - new NumStr("9.023");
            Assert.AreEqual(a1.ToString(), "9");
            a1 = new NumStr("18.023") - new NumStr("9.344");
            Assert.AreEqual(a1.ToString(), "8.679");
            a1 = new NumStr("18.023") - new NumStr("18.023");
            Assert.AreEqual(a1.ToString(), "0");
            a1 = new NumStr("-18.023") - new NumStr("9.344");
            Assert.AreEqual(a1.ToString(), "-27.367");
            a1 = new NumStr("18.023") - new NumStr("-9.344");
            Assert.AreEqual(a1.ToString(), "27.367");
            a1 = new NumStr("-18.023") - new NumStr("-9.344");
            Assert.AreEqual(a1.ToString(), "-8.679");

            a1 = new NumStr("-98.023");
            a1 = a1 - a1;
            Assert.AreEqual(a1.ToString(), "0");

            var s = new OperationSpace(10, 16);
            a1 = new NumStr("7B", s) - new NumStr("3C", s);
            Assert.AreEqual(a1.ToString(), "3F");
        }
        /// <summary>
        /// 乘法测试
        /// </summary>
        [TestMethod]
        public void MultiplyTest()
        {
            var a1 = new NumStr("60") * new NumStr("1.23");
            Assert.AreEqual(a1.ToString(), "73.8");
            a1 = new NumStr("96.2014") * new NumStr("6.3254");
            Assert.AreEqual(a1.ToString(), "608.51233556");
            a1 = new NumStr("0.25") * new NumStr("0.25");
            Assert.AreEqual(a1.ToString(), "0.0625");
            a1 = new NumStr("0.5") * new NumStr("60");
            Assert.AreEqual(a1.ToString(), "30");
            a1 = new NumStr("-0.5") * new NumStr("60");
            Assert.AreEqual(a1.ToString(), "-30");

            a1 = new NumStr("-98.023");
            a1 = a1 * a1;
            Assert.AreEqual(a1.ToString(), "9608.508529");

            var s = new OperationSpace(10, 16);
            a1 = new NumStr("7B", s) * new NumStr("3C", s);
            Assert.AreEqual(a1.ToString(), "1CD4");

            s = new OperationSpace(4, 10);
            a1 = new NumStr("96.2014", s) * new NumStr("6.3254", s);
            Assert.AreEqual(a1.ToString(), "608.5123");
        }
        /// <summary>
        /// 除法测试
        /// </summary>
        [TestMethod]
        public void DivideTest()
        {
            var a1 = new NumStr("6.3") / new NumStr("2.3");
            Assert.AreEqual(a1.ToString(), "2.7391304347");
            a1 = new NumStr("6.3") / new NumStr("2.4");
            Assert.AreEqual(a1.ToString(), "2.625");
            a1 = new NumStr("63") / new NumStr("0.0023");
            Assert.AreEqual(a1.ToString(), "27391.3043478260");
            a1 = new NumStr("0.1") / new NumStr("991");
            Assert.AreEqual(a1.ToString(), "0.0001009081");

            a1 = new NumStr("-98.023");
            a1 = a1 / a1;
            Assert.AreEqual(a1.ToString(), "1");
        }
        /// <summary>
        /// 取余测试
        /// </summary>
        [TestMethod]
        public void ModTest()
        {
            var a1 = new NumStr("6.3") % new NumStr("2.3");
            Assert.AreEqual(a1.ToString(), "1.7");
            a1 = new NumStr("789") % new NumStr("6");
            Assert.AreEqual(a1.ToString(), "3");
            a1 = new NumStr("-6") % new NumStr("3");
            Assert.AreEqual(a1.IsZero, true);
            a1 = new NumStr("-6") % new NumStr("-9");
            Assert.AreEqual(a1.ToString(), "-6");

            a1 = new NumStr("-98.023");
            a1 = a1 % a1;
            Assert.AreEqual(a1.ToString(), "0");
        }
        /// <summary>
        /// 乘方测试
        /// </summary>
        [TestMethod]
        public void PowerTest()
        {
            var a1 = new NumStr("-2") ^ new NumStr("29");
            Assert.AreEqual(a1.ToString(), "-536870912");
            a1 = new NumStr("0.6", new OperationSpace(12)) ^ new NumStr("23");
            Assert.AreEqual(a1.ToString(), "0.000007897302");
        }
        /// <summary>
        /// 开平方测试
        /// </summary>
        [TestMethod]
        public void SqrtTest()
        {
            var a1 = Digitable.Sqrt(new NumStr("3"));
            Assert.AreEqual(a1.ToString(), "1.7320508075");
            a1 = Digitable.Sqrt(new NumStr("0.0001"));
            Assert.AreEqual(a1.ToString(), "0.01");
            a1 = Digitable.Sqrt(new NumStr("8569755963"));
            Assert.AreEqual(a1.ToString(), "92572.9764186071");
        }
        /// <summary>
        /// 小数点左移测试
        /// </summary>
        [TestMethod]
        public void LeftShiftTest()
        {
            var a1 = new NumStr("3034.5") << 3;
            Assert.AreEqual(a1.ToString(), "3.0345");
            a1 = new NumStr("3") << 3;
            Assert.AreEqual(a1.ToString(), "0.003");
            a1 = new NumStr("1111.1234", null, 4) << 8;
            Assert.AreEqual(a1.ToString(), "0");
            a1 = new NumStr("1111.1234", null, 4) << 7;
            Assert.AreEqual(a1.ToString(), "0.0001");
        }
        /// <summary>
        /// 小数点右移测试
        /// </summary>
        [TestMethod]
        public void RightShiftTest()
        {
            var a1 = new NumStr("3.0345") >> 3;
            Assert.AreEqual(a1.ToString(), "3034.5");
            a1 = new NumStr("3") >> 3;
            Assert.AreEqual(a1.ToString(), "3000");
            a1 = new NumStr("1111.1234", null, 4) >> 5;
            Assert.AreEqual(a1.ToString(), "111112340");
        }
    }
}
