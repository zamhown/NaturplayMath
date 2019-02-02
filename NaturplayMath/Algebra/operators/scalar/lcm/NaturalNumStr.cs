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
        /// 求两个数的最小公倍数。如果其中有个数为0，则返回0
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static NaturalNumStr LeastCommonMultiple(NaturalNumStr a, NaturalNumStr b)
        {
            if (a == null || b == null)
                throw new ProgramInterruptException(ProgramInterruptExceptionType.IllegalValue);
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
            if (a == 0 || b == 0)
                return new NaturalNumStr(0, a.Space);
            return a * b / GreatestCommonDivisor(a, b);
        }
    }
}
