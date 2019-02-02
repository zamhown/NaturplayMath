using NaturplayMath.Algebra.Exception;
using NaturplayMath.Algebra.Scalar.NumberString;
using System;
using System.Collections.Generic;
using System.Text;

namespace NaturplayMath.Algebra.Scalar
{
    /// <summary>
    /// 有理数类
    /// </summary>
    public partial class RationalNum
    {
        /// <summary>
        /// 取模运算符
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static RationalNum operator %(RationalNum a, RationalNum b) => ((a as Digitable) % (b as Digitable)) as RationalNum;

        /// <summary>
        /// 取模运算
        /// </summary>
        /// <param name="a">被模数</param>
        /// <param name="b">模数</param>
        /// <param name="maxDecimalPlaces">答案保留小数位数</param>
        /// <returns>包含余数和商的元组</returns>
        public static (RationalNum, IntegerNum) Mod(RationalNum a, RationalNum b, int? maxDecimalPlaces = null)
        {
            var ans = Digitable.Mod(a, b, maxDecimalPlaces);
            return (ans.Item1 as RationalNum, ans.Item2 as IntegerNum);
        }

        /// <summary>
        /// 按位取模运算（不考虑正负号）
        /// </summary>
        /// <param name="num">模数</param>
        /// <param name="maxDecimalPlaces">答案保留小数位数</param>
        /// <returns>包含余数和商的元组</returns>
        protected override (Digitable, Digitable) Mod_unsigned(Digitable num, int maxDecimalPlaces)
        {
            RationalNum a = this, b = num as RationalNum;

            if (a == null || b == null)
                throw new ProgramInterruptException(ProgramInterruptExceptionType.IllegalValue);
            //统一计算空间
            if (a.Space != b.Space)
            {
                if (a.Space == OperationSpace.DefaultSpace)
                    a = (RationalNum)a.ChangeOperationSpace(b.Space);
                else if (b.Space == OperationSpace.DefaultSpace)
                    b = (RationalNum)b.ChangeOperationSpace(a.Space);
                else
                    throw new ProgramInterruptException(ProgramInterruptExceptionType.NotSameOperationSpace);
            }

            var ans = new RationalNum(a, b, 1, a.Space);
            ans.GetMixedNumber(out var i, out var n);
            return (new RationalNum(n * b.numerator, ans.denominator * b.denominator, 1, a.Space, maxDecimalPlaces),
                i);
        }
    }
}
