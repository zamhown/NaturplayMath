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
        /// 取模运算符
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static IntegerNum operator %(IntegerNum a, IntegerNum b) => (a as Digitable % b as Digitable) as IntegerNum;

        /// <summary>
        /// 取模
        /// </summary>
        /// <param name="a">被模数</param>
        /// <param name="b">模数</param>
        /// <returns>包含余数和商的元组</returns>
        public static (IntegerNum, IntegerNum) Mod(IntegerNum a, IntegerNum b)
        {
            var ans = a.Mod_unsigned(b, 0);
            var new_ans = (ans.Item1 as IntegerNum, ans.Item2 as IntegerNum);
            new_ans.Item1.PositiveOrNegative = new_ans.Item1.IsEmpty() ? 0 : a.PositiveOrNegative;
            new_ans.Item2.PositiveOrNegative = new_ans.Item2.IsEmpty() ? 0 : a.PositiveOrNegative / b.PositiveOrNegative;
            return new_ans;
        }

        /// <summary>
        /// 按位取模运算（不考虑正负号）
        /// </summary>
        /// <param name="num">模数</param>
        /// <param name="maxDecimalPlaces">答案保留小数位数</param>
        /// <returns>包含余数和商的元组</returns>
        protected override (Digitable, Digitable) Mod_unsigned(Digitable num, int maxDecimalPlaces)
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

            var ans = NaturalNumStr.Mod(a.numerator, b.numerator);
            return (new IntegerNum(ans.Item1, null, a.Space),
                new IntegerNum(ans.Item2, null, a.Space));
        }
    }
}
