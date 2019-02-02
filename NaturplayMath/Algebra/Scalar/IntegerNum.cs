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
    public partial class IntegerNum : RationalNum
    {
        /// <summary>
        /// 默认构造函数，初始化为0
        /// </summary>
        public IntegerNum()
            :base()
        { }
        /// <summary>
        /// 用纯数字初始化（小数部分会被截断）
        /// </summary>
        /// <param name="num">数字</param>
        /// <param name="space">计算空间</param>
        public IntegerNum(NumStr num, OperationSpace space = null)
            : base(new NaturalNumStr(num, false), space, 0)
        {
            PositiveOrNegative = num.PositiveOrNegative;
        }
        /// <summary>
        /// 用纯数字初始化
        /// </summary>
        /// <param name="num">数字</param>
        /// <param name="positiveOrNegative">正负性</param>
        /// <param name="space">计算空间</param>
        public IntegerNum(NaturalNumStr num, int? positiveOrNegative = null, OperationSpace space = null)
            : base(num, space, 0)
        {
            PositiveOrNegative = positiveOrNegative ?? num.PositiveOrNegative;
        }
        /// <summary>
        /// 拷贝初始化
        /// </summary>
        /// <param name="num">原始对象</param>
        /// <param name="deep">是否为深拷贝</param>
        public IntegerNum(RationalNum num, bool deep = true)
            : base(num, 0, deep)
        { }

        /// <summary>
        /// 从NaturalNumStr隐式转换
        /// </summary>
        /// <param name="num"></param>
        public static implicit operator IntegerNum(NaturalNumStr num)
        {
            return new IntegerNum(num);
        }
        /// <summary>
        /// 从Int64隐式转换
        /// </summary>
        /// <param name="num"></param>
        public static implicit operator IntegerNum(Int64 num)
        {
            return new IntegerNum(num);
        }
    }
}
