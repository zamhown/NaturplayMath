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
        /// 取自然对数
        /// </summary>
        /// <param name="maxDecimalPlaces">答案保留小数位数</param>
        /// <returns></returns>
        protected override Digitable Ln(int maxDecimalPlaces)
        {
            NumStr a = this;

            if (a == null)
                throw new ProgramInterruptException(ProgramInterruptExceptionType.IllegalValue);
            //特殊情况
            if (!a.IsPositive)
                throw new IllegalOperationException(IllegalOperationExceptionType.AntilogIsNotPositive);
            else if (a.IsOne)
                return new NumStr(0, a.Space, maxDecimalPlaces);

            /* 原理：
             * 泰勒展开式：ln(1+x) = x-x^2/2+x^3/3-...+(-1)^(k-1)*(x^k)/k
             * = x(1-x(1/2-x(1/3-x(1/4-x(1/5...)))))
             */
            NumStr ans = null;
            
            return ans;
        }
    }
}