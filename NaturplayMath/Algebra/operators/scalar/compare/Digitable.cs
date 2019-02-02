using NaturplayMath.Algebra.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturplayMath.Algebra.Scalar
{
    /// <summary>
    /// 可数位化的数字（抽象类）
    /// </summary>
    public abstract partial class Digitable
    {
        /// <summary>
        /// 大于运算符
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator >(Digitable a, Digitable b)
        {
            if (a == null || b == null)
                throw new ProgramInterruptException(ProgramInterruptExceptionType.IllegalValue);
            else if (a.PositiveOrNegative > 0 && b.PositiveOrNegative <= 0)
                return true;
            else if (a.PositiveOrNegative <= 0 && b.PositiveOrNegative >= 0)
                return false;
            if (a.PositiveOrNegative > 0 || b.PositiveOrNegative > 0)
                return a.CompareAbsolute(b) > 0;
            else
                return a.CompareAbsolute(b) < 0;
        }
        /// <summary>
        /// 小于运算符
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator <(Digitable a, Digitable b)
        {
            if (a == null || b == null)
                throw new ProgramInterruptException(ProgramInterruptExceptionType.IllegalValue);
            else if (a.PositiveOrNegative < 0 && b.PositiveOrNegative >= 0)
                return true;
            else if (a.PositiveOrNegative >= 0 && b.PositiveOrNegative <= 0)
                return false;
            if (a.PositiveOrNegative > 0 || b.PositiveOrNegative > 0)
                return a.CompareAbsolute(b) < 0;
            else
                return a.CompareAbsolute(b) > 0;
        }
        /// <summary>
        /// 大于等于运算符
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator >=(Digitable a, Digitable b)
        {
            if (a == null || b == null)
                throw new ProgramInterruptException(ProgramInterruptExceptionType.IllegalValue);
            else if (a.PositiveOrNegative >= 0 && b.PositiveOrNegative <= 0)
                return true;
            else if (a.PositiveOrNegative < 0 && b.PositiveOrNegative >= 0)
                return false;
            if (a.PositiveOrNegative > 0 || b.PositiveOrNegative > 0)
                return a.CompareAbsolute(b) >= 0;
            else
                return a.CompareAbsolute(b) <= 0;
        }
        /// <summary>
        /// 小于等于运算符
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator <=(Digitable a, Digitable b)
        {
            if (a == null || b == null)
                throw new ProgramInterruptException(ProgramInterruptExceptionType.IllegalValue);
            else if (a.PositiveOrNegative <= 0 && b.PositiveOrNegative >= 0)
                return true;
            else if (a.PositiveOrNegative > 0 && b.PositiveOrNegative <= 0)
                return false;
            if (a.PositiveOrNegative > 0 || b.PositiveOrNegative > 0)
                return a.CompareAbsolute(b) <= 0;
            else
                return a.CompareAbsolute(b) >= 0;
        }
        /// <summary>
        /// 等于运算符
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(Digitable a, Digitable b)
        {
            if (!(a is null) && b is null
                || a is null && !(b is null))
                return false;
            else if (ReferenceEquals(a, b))
                return true;
            else if (!(a is null) && !(b is null))
            {
                if (a.PositiveOrNegative != b.PositiveOrNegative)
                    return false;
            }
            return a.CompareAbsolute(b) == 0;
        }
        /// <summary>
        /// 不等于运算符
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(Digitable a, Digitable b)
        {
            if (!(a is null) && b is null
                || a is null && !(b is null))
                return true;
            else if (ReferenceEquals(a, b))
                return false;
            else if (!(a is null) && !(b is null))
            {
                if (a.PositiveOrNegative != b.PositiveOrNegative)
                    return true;
            }
            return a.CompareAbsolute(b) != 0;
        }

        /// <summary>
        /// 将本数和另一个数取绝对值进行比较，大于输出1，小于输出-1，等于输出0
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public virtual int CompareAbsolute(Digitable num) => throw new NotImplementedException();
        /// <summary>
        /// 本数是否为空（值为0）
        /// </summary>
        /// <returns></returns>
        public virtual bool IsEmpty() => throw new NotImplementedException();

        public override bool Equals(object obj)
        {
            return Equals(obj as Digitable);
        }

        public bool Equals(Digitable other)
        {
            return this == other;
        }
    }
}
