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
        /// 求两个数的最大公约数。如果其中一个数为0、另一个不为0，则返回不为0的那个数
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static IntegerNum GreatestCommonDivisor(IntegerNum a, IntegerNum b)
        {
            return NaturalNumStr.GreatestCommonDivisor(a.numerator, b.numerator);
        }
    }
}
