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
        /// 乘法运算符
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Digitable operator *(Digitable a, Digitable b)
        {
            return Multiply(a, b);
        }

        /// <summary>
        /// 乘法运算
        /// </summary>
        /// <param name="a">乘数</param>
        /// <param name="b">乘数</param>
        /// <param name="maxDecimalPlaces">答案保留小数位数</param>
        /// <returns>积</returns>
        public static Digitable Multiply (Digitable a, Digitable b, int? maxDecimalPlaces = null)
        {
            if (a == null || b == null)
                throw new ProgramInterruptException(ProgramInterruptExceptionType.IllegalValue);
            var maxDP = maxDecimalPlaces ?? (a.Space == OperationSpace.DefaultSpace ? b.Space.DefaultMaxDecimalPlaces : a.Space.DefaultMaxDecimalPlaces);
            var ans = a.Multiply_unsigned(b, maxDP);
            ans.PositiveOrNegative = a.PositiveOrNegative * b.PositiveOrNegative;
            return ans;
        }

        /// <summary>
        /// 按位乘法运算（不考虑正负号）
        /// </summary>
        /// <param name="num">乘数</param>
        /// <param name="maxDecimalPlaces">答案保留小数位数</param>
        /// <returns></returns>
        protected virtual Digitable Multiply_unsigned(Digitable num, int maxDecimalPlaces)
        {
            throw new NotImplementedException();
        }
    }
}
