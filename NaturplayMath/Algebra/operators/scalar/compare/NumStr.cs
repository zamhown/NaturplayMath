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
        /// 大于运算符
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator >(NumStr a, NumStr b) => (a as Digitable) > (b as Digitable);
        /// <summary>
        /// 小于运算符
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator <(NumStr a, NumStr b) => (a as Digitable) < (b as Digitable);
        /// <summary>
        /// 大于等于运算符
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator >=(NumStr a, NumStr b) => (a as Digitable) >= (b as Digitable);
        /// <summary>
        /// 小于等于运算符
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator <=(NumStr a, NumStr b) => (a as Digitable) <= (b as Digitable);
        /// <summary>
        /// 等于运算符
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(NumStr a, NumStr b) => (a as Digitable) == (b as Digitable);
        /// <summary>
        /// 不等于运算符
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(NumStr a, NumStr b) => (a as Digitable) != (b as Digitable);

        /// <summary>
        /// 将本数和另一个数取绝对值进行比较，大于输出1，小于输出-1，等于输出0
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public override int CompareAbsolute(Digitable num)
        {
            NumStr a = this, b = num as NumStr;

            if (ReferenceEquals(a, b))
                return 0;

            if (a == null || b == null)
                throw new ProgramInterruptException(ProgramInterruptExceptionType.IllegalValue);
            //统一计算空间
            if (a.Space != b.Space)
            {
                if (a.Space == OperationSpace.DefaultSpace)
                    a = (NumStr)a.ChangeOperationSpace(b.Space);
                else if (b.Space == OperationSpace.DefaultSpace)
                    b = (NumStr)b.ChangeOperationSpace(a.Space);
                else
                    throw new ProgramInterruptException(ProgramInterruptExceptionType.NotSameOperationSpace);
            }

            if (a.IntegerPlaces > b.IntegerPlaces)
                return 1;
            else if (a.IntegerPlaces < b.IntegerPlaces)
                return -1;
            else
            {
                //比较整数
                if (a.integerNumList != null)
                {
                    var ai = a.integerNumList.Last;
                    var bi = b.integerNumList.Last;
                    while (ai != null && ai.Value == bi.Value)
                    {
                        ai = ai.Previous;
                        bi = bi.Previous;
                    }
                    if (ai != null) return ai.Value > bi.Value ? 1 : -1;
                }
                //比较小数
                if (a.decimalNumList != null && b.decimalNumList == null)
                    return 1;
                else if (a.decimalNumList == null && b.decimalNumList != null)
                    return -1;
                else if (a.decimalNumList == null && b.decimalNumList == null)
                    return 0;
                else
                {
                    var ai = a.decimalNumList.First;
                    var bi = b.decimalNumList.First;
                    while (ai != null && bi != null && ai.Value == bi.Value)
                    {
                        ai = ai.Next;
                        bi = bi.Next;
                    }
                    if (ai != null && bi == null) return 1;
                    else if (ai == null && bi != null) return -1;
                    else if (ai == null && bi == null) return 0;
                    else return ai.Value > bi.Value ? 1 : -1;
                }
            }
        }

        /// <summary>
        /// 本数是否为空（值为0）
        /// </summary>
        /// <returns></returns>
        public override bool IsEmpty() => CompareAbsolute(this, 0) == 0;

        /// <summary>
        /// 将两数进行比较，大于输出1，小于输出-1，等于输出0
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int CompareAbsolute(NumStr a, NumStr b) => a.CompareAbsolute(b);
    }
}