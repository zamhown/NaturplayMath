using NaturplayMath.Algebra.Exception;
using NaturplayMath.Algebra.Scalar.NumberString;
using System;
using System.Collections.Generic;
using System.Text;

namespace NaturplayMath.Algebra.Scalar
{
    /// <summary>
    /// 有理数类
    /// </summary>
    public partial class IntegerNum
    {
        /// <summary>
        /// 将本数和另一个数取绝对值进行比较，大于输出1，小于输出-1，等于输出0
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public override int CompareAbsolute(Digitable num)
        {
            IntegerNum a = this, b = num as IntegerNum;

            if (ReferenceEquals(a, b))
                return 0;

            if (a == null || b == null)
                throw new ProgramInterruptException(ProgramInterruptExceptionType.IllegalValue);
            //统一计算空间
            if (a.Space != b.Space)
            {
                if (a.Space == OperationSpace.DefaultSpace)
                    a = (IntegerNum)a.ChangeOperationSpace(b.Space);
                else if (b.Space == OperationSpace.DefaultSpace)
                    b = (IntegerNum)b.ChangeOperationSpace(a.Space);
                else
                    throw new ProgramInterruptException(ProgramInterruptExceptionType.NotSameOperationSpace);
            }

            return a.numerator.CompareAbsolute(b.numerator);
        }
        
        /// <summary>
        /// 将两数进行比较，大于输出1，小于输出-1，等于输出0
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int CompareAbsolute(IntegerNum a, IntegerNum b) => a.CompareAbsolute(b);
    }
}
