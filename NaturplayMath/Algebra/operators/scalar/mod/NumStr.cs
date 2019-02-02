using NaturplayMath.Algebra.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturplayMath.Algebra.Scalar.NumberString
{
    /// <summary>
    /// 数字串类
    /// </summary>
    public partial class NumStr
    {
        /// <summary>
        /// 取模运算符
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static NumStr operator %(NumStr a, NumStr b) => ((a as Digitable) % (b as Digitable)) as NumStr;

        /// <summary>
        /// 取模运算
        /// </summary>
        /// <param name="a">被模数</param>
        /// <param name="b">模数</param>
        /// <param name="maxDecimalPlaces">答案保留小数位数</param>
        /// <returns>包含余数和商的元组</returns>
        public static (NumStr, NumStr) Mod(NumStr a, NumStr b, int maxDecimalPlaces)
        {
            var ans = Digitable.Mod(a, b, maxDecimalPlaces);
            return (ans.Item1 as NumStr, ans.Item2 as NumStr);
        }

        /// <summary>
        /// 按位取模运算（不考虑正负号）
        /// </summary>
        /// <param name="num">模数</param>
        /// <param name="maxDecimalPlaces">答案保留小数位数</param>
        /// <returns>包含余数和商的元组</returns>
        protected override (Digitable, Digitable) Mod_unsigned(Digitable num, int maxDecimalPlaces)
        {
            NumStr a = this, b = num as NumStr;

            if (a == null || b == null)
                throw new ProgramInterruptException(ProgramInterruptExceptionType.IllegalValue);
            //统一计算空间
            if (a.Space != b.Space)
            {
                if (a.Space == OperationSpace.DefaultSpace)
                    a = (NumStr)a.ChangeOperationSpace(b.Space);
                else if (b.Space == OperationSpace.DefaultSpace)
                    b = (NumStr)b.ChangeOperationSpace(a.Space);
                else
                    throw new ProgramInterruptException(ProgramInterruptExceptionType.NotSameOperationSpace);
            }

            if (CompareAbsolute(a, b) < 0)
                return (new NumStr(a), new NaturalNumStr(0, a.Space));
            var quotient = a.Divide_unsigned(b, 0) as NumStr;
            var ans = a.Minus_unsigned(quotient * b, maxDecimalPlaces, out bool tmp);
            return (ans, quotient);
        }
    }
}