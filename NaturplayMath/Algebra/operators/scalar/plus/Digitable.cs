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
        /// 加法运算符
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Digitable operator +(Digitable a, Digitable b)
        {
            return Plus(a, b);
        }

        /// <summary>
        /// 加法运算
        /// </summary>
        /// <param name="a">加数</param>
        /// <param name="b">加数</param>
        /// <param name="maxDecimalPlaces">答案保留小数位数</param>
        /// <returns>和</returns>
        public static Digitable Plus(Digitable a, Digitable b, int? maxDecimalPlaces = null)
        {
            if (a == null || b == null)
                throw new ProgramInterruptException(ProgramInterruptExceptionType.IllegalValue);
            var maxDP = maxDecimalPlaces ?? (a.Space == OperationSpace.DefaultSpace ? b.Space.DefaultMaxDecimalPlaces : a.Space.DefaultMaxDecimalPlaces);
            //根据正负性来重定向
            if (a.IsPositive && b.IsNegative)
            {
                var ans = a.Minus_unsigned(b, maxDP, out bool hasSwaped);
                ans.PositiveOrNegative = hasSwaped ? -1 : (ans.IsEmpty() ? 0 : 1);
                return ans;
            }
            else if (a.IsNegative && b.IsPositive)
            {
                var ans = b.Minus_unsigned(a, maxDP, out bool hasSwaped);
                ans.PositiveOrNegative = hasSwaped ? -1 : (ans.IsEmpty() ? 0 : 1);
                return ans;
            }
            else if (a.IsZero)
            {
                var ans = b.Copy();
                ans.MaxDecimalPlaces = maxDP;
                return ans;
            }
            else if (b.IsZero)
            {
                var ans = a.Copy();
                ans.MaxDecimalPlaces = maxDP;
                return ans;
            }
            else
            {
                var ans = a.Plus_unsigned(b, maxDP);
                ans.PositiveOrNegative = a.PositiveOrNegative;
                return ans;
            }
        }

        /// <summary>
        /// 按位加法运算（不考虑正负号）
        /// </summary>
        /// <param name="num">另一个加数</param>
        /// <param name="maxDecimalPlaces">答案保留小数位数</param>
        /// <returns></returns>
        protected virtual Digitable Plus_unsigned(Digitable num, int maxDecimalPlaces)
        {
            throw new NotImplementedException();
        }
    }
}
