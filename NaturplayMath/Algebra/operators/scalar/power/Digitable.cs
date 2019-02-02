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
        /// 乘方运算符（原C#异或运算符）
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Digitable operator ^(Digitable a, Digitable b)
        {
            return Power(a, b);
        }

        /// <summary>
        /// 乘方运算
        /// </summary>
        /// <param name="a">底数</param>
        /// <param name="b">指数</param>
        /// <param name="maxDecimalPlaces">答案保留小数位数</param>
        /// <returns>幂</returns>
        public static Digitable Power (Digitable a, Digitable b, int? maxDecimalPlaces = null)
        {
            if (a == null || b == null)
                throw new ProgramInterruptException(ProgramInterruptExceptionType.IllegalValue);
            var maxDP = maxDecimalPlaces ?? (a.Space == OperationSpace.DefaultSpace ? b.Space.DefaultMaxDecimalPlaces : a.Space.DefaultMaxDecimalPlaces);
            var ans = a.Power_unsigned(b, maxDP);
            if (a.PositiveOrNegative > 0)
                ans.PositiveOrNegative = 1;
            else if (a.PositiveOrNegative < 0)
            {
                if (b.IsInteger)
                    ans.PositiveOrNegative = b.IsOdd ? -1 : 1;
                else
                    throw new IllegalOperationException();
            }
            else
                ans.PositiveOrNegative = 0;
            return ans;
        }

        /// <summary>
        /// 乘方运算（不考虑正负号）
        /// </summary>
        /// <param name="num">指数</param>
        /// <param name="maxDecimalPlaces">答案保留小数位数</param>
        /// <returns></returns>
        protected virtual Digitable Power_unsigned(Digitable num, int maxDecimalPlaces)
        {
            throw new NotImplementedException();
        }
    }
}
