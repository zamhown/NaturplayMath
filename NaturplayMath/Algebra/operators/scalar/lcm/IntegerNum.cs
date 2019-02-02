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
        /// 求两个数的最小公倍数。如果其中有个数为0，则返回0
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static IntegerNum LeastCommonMultiple(IntegerNum a, IntegerNum b)
        {
            return NaturalNumStr.LeastCommonMultiple(a.numerator, b.numerator);
        }
    }
}
