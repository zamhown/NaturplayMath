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
        /// 右移运算符（会改变自身的值，而非产生新的实例）
        /// </summary>
        /// <param name="a">操作数</param>
        /// <param name="offset">移动位数</param>
        /// <returns></returns>
        public static Digitable operator >>(Digitable a, int offset)
        {
            a.RightShift(offset);
            return a;
        }

        /// <summary>
        /// 小数点右移运算（会改变自身的值，而非产生新的实例）
        /// </summary>
        /// <param name="offset">移动位数</param>
        /// <returns></returns>
        public virtual void RightShift(int offset)
        {
            throw new NotImplementedException();
        }
    }
}
