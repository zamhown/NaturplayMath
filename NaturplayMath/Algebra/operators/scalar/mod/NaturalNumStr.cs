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
        /// 取模运算符
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static NaturalNumStr operator %(NaturalNumStr a, NaturalNumStr b)
        {
            return new NaturalNumStr(a as NumStr % b as NumStr, false);
        }

        /// <summary>
        /// 取模
        /// </summary>
        /// <param name="a">被模数</param>
        /// <param name="b">模数</param>
        /// <returns>包含余数和商的元组</returns>
        public static (NaturalNumStr, NaturalNumStr) Mod(NaturalNumStr a, NaturalNumStr b)
        {
            var ans = a.Mod_unsigned(b, 0);
            var new_ans = (new NaturalNumStr(ans.Item1 as NumStr, false), new NaturalNumStr(ans.Item2 as NumStr, false));
            new_ans.Item1.PositiveOrNegative = new_ans.Item1.IsEmpty() ? 0 : 1;
            new_ans.Item2.PositiveOrNegative = new_ans.Item2.IsEmpty() ? 0 : 1;
            return new_ans;
        }
    }
}
