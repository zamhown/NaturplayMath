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
        /// 除法运算符
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static IntegerNum operator /(IntegerNum a, IntegerNum b) => (a as Digitable / b as Digitable) as IntegerNum;

        /// <summary>
        /// 按位除法运算（不考虑正负号）
        /// </summary>
        /// <param name="num">除数</param>
        /// <param name="maxDecimalPlaces">答案保留小数位数</param>
        /// <returns></returns>
        protected override Digitable Divide_unsigned(Digitable num, int maxDecimalPlaces)
        {
            IntegerNum a = this, b = num as IntegerNum;

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

            return new IntegerNum(a.numerator / b.numerator, 1, a.Space);
        }
    }
}
