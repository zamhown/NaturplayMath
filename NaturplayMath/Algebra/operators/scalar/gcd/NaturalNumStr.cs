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
        /// 求两个数的最大公约数。如果其中一个数为0、另一个不为0，则返回不为0的那个数
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static NaturalNumStr GreatestCommonDivisor(NaturalNumStr a, NaturalNumStr b)
        {
            if (a == null || b == null)
                throw new ProgramInterruptException(ProgramInterruptExceptionType.IllegalValue);
            var old_a = a;
            var old_b = b;
            //统一计算空间
            if (a.Space != b.Space)
            {
                if (a.Space == OperationSpace.DefaultSpace)
                    a = (NaturalNumStr)a.ChangeOperationSpace(b.Space);
                else if (b.Space == OperationSpace.DefaultSpace)
                    b = (NaturalNumStr)b.ChangeOperationSpace(a.Space);
                else
                    throw new ProgramInterruptException(ProgramInterruptExceptionType.NotSameOperationSpace);
            }
            if (a == 0)
            {
                if (b == 0)
                    throw new IllegalOperationException();
                else
                {
                    if (ReferenceEquals(b, old_b))
                        return new NaturalNumStr(b);
                    else
                        return b;
                }
            }
            else if (b == 0)
            {
                if (ReferenceEquals(a, old_a))
                    return new NaturalNumStr(a);
                else
                    return a;
            }
            if (b > a)
            {
                var t = a;
                a = b;
                b = t;
            }
            //辗转相除法
            while (!b.IsZero)
            {
                var t = a;
                a = b;
                b = t % b;
            }
            if (ReferenceEquals(a, old_a) || ReferenceEquals(a, old_b))
                return new NaturalNumStr(a);
            else
                return a;
        }
    }
}
