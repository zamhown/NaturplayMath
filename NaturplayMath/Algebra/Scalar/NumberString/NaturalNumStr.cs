using NaturplayMath.Algebra.Exception;
using System;
using System.Collections.Generic;
using System.Text;

namespace NaturplayMath.Algebra.Scalar.NumberString
{
    /// <summary>
    /// 自然数串类，按位保存自然数
    /// </summary>
    public partial class NaturalNumStr : NumStr
    {
        /// <summary>
        /// 默认构造函数，初始化为0
        /// </summary>
        public NaturalNumStr() { }
        /// <summary>
        /// 用整数初始化，如果为负数则保存绝对值
        /// </summary>
        /// <param name="num">整数</param>
        /// <param name="space">计算空间</param>
        public NaturalNumStr(Int64 num, OperationSpace space = null)
            : base(num, space, 0)
        {
            if (PositiveOrNegative < 0)
                PositiveOrNegative = 1;
        }
        /// <summary>
        /// 用整数初始化，如果为负数则保存绝对值
        /// </summary>
        /// <param name="num">整数</param>
        /// <param name="space">计算空间</param>
        public NaturalNumStr(UInt64 num, OperationSpace space = null)
            : base(num, space, 0)
        { }
        /// <summary>
        /// 用字符串表示的数字初始化，如果为负数则保存绝对值
        /// </summary>
        /// <param name="num">整数</param>
        /// <param name="space">计算空间</param>
        public NaturalNumStr(string num, OperationSpace space = null)
            : base(num, space, 0)
        {
            if (PositiveOrNegative < 0)
                PositiveOrNegative = 1;
        }
        /// <summary>
        /// 用纯数字初始化，转换为指定的计算空间
        /// </summary>
        /// <param name="num">数字</param>
        /// <param name="space">计算空间</param>
        public NaturalNumStr(NumStr num, OperationSpace space)
            : base(num, space, 0)
        {
            if (PositiveOrNegative < 0)
                PositiveOrNegative = 1;
        }
        /// <summary>
        /// 拷贝初始化
        /// </summary>
        /// <param name="num">原始对象</param>
        /// <param name="deep">是否为深拷贝</param>
        public NaturalNumStr(NumStr num, bool deep = true)
            : base(num, 0, deep)
        {
            if (PositiveOrNegative < 0)
                PositiveOrNegative = 1;
        }

        /// <summary>
        /// 改变元素的计算空间
        /// </summary>
        /// <param name="space">新的计算空间，为null即为默认计算空间</param>
        /// <param name="newInstance">是否返回新的实例（如果为null，即为不确定）</param>
        /// <param name="maxDecimalPlaces">最大保留小数位数</param>
        /// <returns></returns>
        public override IOperationSpaceElement ChangeOperationSpace(OperationSpace space = null, bool? newInstance = null, int? maxDecimalPlaces = null)
        {
            return new NaturalNumStr(ChangeOS(space, newInstance, 0), false);
        }

        /// <summary>
        /// 从UInt64隐式转换
        /// </summary>
        /// <param name="num"></param>
        public static implicit operator NaturalNumStr(UInt64 num)
        {
            return new NaturalNumStr(num);
        }
    }
}
