using NaturplayMath.Algebra.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturplayMath.Algebra.Scalar
{
    /// <summary>
    /// 可数位化的数字（抽象类）
    /// </summary>
    public abstract partial class Digitable
    {
        /// <summary>
        /// 取模运算符
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Digitable operator %(Digitable a, Digitable b)
        {
            return Mod(a, b).Item1;
        }

        /// <summary>
        /// 取模运算
        /// </summary>
        /// <param name="a">被模数</param>
        /// <param name="b">模数</param>
        /// <param name="maxDecimalPlaces">答案保留小数位数</param>
        /// <returns>包含余数和商的元组</returns>
        public static (Digitable, Digitable) Mod (Digitable a, Digitable b, int? maxDecimalPlaces = null)
        {
            if (a == null || b == null)
                throw new ProgramInterruptException(ProgramInterruptExceptionType.IllegalValue);
            var maxDP = maxDecimalPlaces ?? (a.Space == OperationSpace.DefaultSpace ? b.Space.DefaultMaxDecimalPlaces : a.Space.DefaultMaxDecimalPlaces);
            var ans = a.Mod_unsigned(b, maxDP);
            ans.Item1.PositiveOrNegative = ans.Item1.IsEmpty() ? 0 : a.PositiveOrNegative;
            ans.Item2.PositiveOrNegative = ans.Item2.IsEmpty() ? 0 : a.PositiveOrNegative / b.PositiveOrNegative;
            return ans;
        }

        /// <summary>
        /// 按位取模运算（不考虑正负号）
        /// </summary>
        /// <param name="num">模数</param>
        /// <param name="maxDecimalPlaces">答案保留小数位数</param>
        /// <returns>包含余数和商的元组</returns>
        protected virtual (Digitable, Digitable) Mod_unsigned(Digitable num, int maxDecimalPlaces)
        {
            throw new NotImplementedException();
        }
    }
}
