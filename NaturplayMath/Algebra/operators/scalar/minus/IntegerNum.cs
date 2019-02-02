using NaturplayMath.Algebra.Exception;
using NaturplayMath.Algebra.Scalar.NumberString;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturplayMath.Algebra.Scalar
{
    /// <summary>
    /// 整数类
    /// </summary>
    public partial class IntegerNum
    {
        /// <summary>
        /// 减法运算符
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static IntegerNum operator -(IntegerNum a, IntegerNum b) => (a as Digitable - b as Digitable) as IntegerNum;

        /// <summary>
        /// 按位减法运算（不考虑正负号）
        /// </summary>
        /// <param name="num">减数</param>
        /// <param name="maxDecimalPlaces">答案保留小数位数</param>
        /// <param name="hasSwaped">是否发生参数交换</param>
        /// <returns></returns>
        protected override Digitable Minus_unsigned(Digitable num, int maxDecimalPlaces, out bool hasSwaped)
        {
            IntegerNum a = this, b = num as IntegerNum;

            if (a == null || b == null)
                throw new ProgramInterruptException(ProgramInterruptExceptionType.IllegalValue);
            //统一计算空间
            if (a.Space != b.Space)
            {
                if (a.Space == OperationSpace.DefaultSpace)
                    a = (IntegerNum)a.ChangeOperationSpace(b.Space);
                else if (b.Space == OperationSpace.DefaultSpace)
                    b = (IntegerNum)b.ChangeOperationSpace(a.Space);
                else
                    throw new ProgramInterruptException(ProgramInterruptExceptionType.NotSameOperationSpace);
            }
            //判断两参数大小，进行调整
            if (NumStr.CompareAbsolute(a.numerator, b.numerator) < 0)
            {
                var t = a;
                a = b;
                b = t;
                hasSwaped = true;
            }
            else
                hasSwaped = false;

            return new IntegerNum(a.numerator - b.numerator, a.Space);
        }
    }
}
