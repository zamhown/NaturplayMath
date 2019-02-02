using NaturplayMath.Algebra.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturplayMath.Algebra.Scalar.NumberString
{
    /// <summary>
    /// 自然数串类
    /// </summary>
    public partial class NaturalNumStr
    {
        /// <summary>
        /// 整除运算符
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static NaturalNumStr operator /(NaturalNumStr a, NaturalNumStr b) => new NaturalNumStr(Digitable.Divide(a, b, 0) as NumStr, false);
    }
}
