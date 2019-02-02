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
        /// 开平方运算
        /// </summary>
        /// <param name="a">被开方数</param>
        /// <param name="maxDecimalPlaces">答案保留小数位数</param>
        /// <returns>幂</returns>
        public static Digitable Sqrt (Digitable a, int? maxDecimalPlaces = null)
        {
            if (a == null)
                throw new ProgramInterruptException(ProgramInterruptExceptionType.IllegalValue);
            var maxDP = maxDecimalPlaces ?? a.Space.DefaultMaxDecimalPlaces;
            var ans = a.Sqrt(maxDP);
            return ans;
        }

        /// <summary>
        /// 开平方运算
        /// </summary>
        /// <param name="maxDecimalPlaces">答案保留小数位数</param>
        /// <returns></returns>
        protected virtual Digitable Sqrt(int maxDecimalPlaces)
        {
            throw new NotImplementedException();
        }
    }
}
