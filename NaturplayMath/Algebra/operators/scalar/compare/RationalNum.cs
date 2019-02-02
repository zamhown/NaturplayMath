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
    public partial class RationalNum
    {
        /// <summary>
        /// 大于运算符
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator >(RationalNum a, RationalNum b) => (a as Digitable) > (b as Digitable);
        /// <summary>
        /// 小于运算符
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator <(RationalNum a, RationalNum b) => (a as Digitable) < (b as Digitable);
        /// <summary>
        /// 大于等于运算符
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator >=(RationalNum a, RationalNum b) => (a as Digitable) >= (b as Digitable);
        /// <summary>
        /// 小于等于运算符
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator <=(RationalNum a, RationalNum b) => (a as Digitable) <= (b as Digitable);
        /// <summary>
        /// 等于运算符
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(RationalNum a, RationalNum b) => (a as Digitable) == (b as Digitable);
        /// <summary>
        /// 不等于运算符
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(RationalNum a, RationalNum b) => (a as Digitable) != (b as Digitable);

        /// <summary>
        /// 将本数和另一个数取绝对值进行比较，大于输出1，小于输出-1，等于输出0
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public override int CompareAbsolute(Digitable num)
        {
            RationalNum a = this, b = num as RationalNum;

            if (ReferenceEquals(a, b))
                return 0;

            if (a == null || b == null)
                throw new ProgramInterruptException(ProgramInterruptExceptionType.IllegalValue);
            //统一计算空间
            if (a.Space != b.Space)
            {
                if (a.Space == OperationSpace.DefaultSpace)
                    a = (RationalNum)a.ChangeOperationSpace(b.Space);
                else if (b.Space == OperationSpace.DefaultSpace)
                    b = (RationalNum)b.ChangeOperationSpace(a.Space);
                else
                    throw new ProgramInterruptException(ProgramInterruptExceptionType.NotSameOperationSpace);
            }

            //通分
            ReductionToACommonDenominator(out var d, out var nums, a, b);
            return nums[0].CompareAbsolute(nums[1]);
        }

        /// <summary>
        /// 本数是否为空（值为0）
        /// </summary>
        /// <returns></returns>
        public override bool IsEmpty() => numerator.IsEmpty();

        /// <summary>
        /// 将两数进行比较，大于输出1，小于输出-1，等于输出0
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int CompareAbsolute(RationalNum a, RationalNum b) => a.CompareAbsolute(b);
    }
}
