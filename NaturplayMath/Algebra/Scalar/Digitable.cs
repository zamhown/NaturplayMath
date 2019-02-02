using NaturplayMath.Algebra.Scalar.NumberString;
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
    public abstract partial class Digitable : IOperationSpaceElement, IEquatable<Digitable>
    {
        /// <summary>
        /// 是否是正数
        /// </summary>
        public bool IsPositive
        {
            get
            {
                return PositiveOrNegative > 0;
            }
        }
        /// <summary>
        /// 是否是负数
        /// </summary>
        public bool IsNegative
        {
            get
            {
                return PositiveOrNegative < 0;
            }
        }
        /// <summary>
        /// 是否是0
        /// </summary>
        public bool IsZero
        {
            get
            {
                return PositiveOrNegative == 0;
            }
        }
        /// <summary>
        /// 是否是1
        /// </summary>
        public abstract bool IsOne { get; }
        /// <summary>
        /// 是否是整数
        /// </summary>
        public abstract bool IsInteger { get; }
        /// <summary>
        /// 是否是偶数
        /// </summary>
        public abstract bool IsEven { get; }
        /// <summary>
        /// 是否是奇数
        /// </summary>
        public abstract bool IsOdd { get; }
        /// <summary>
        /// 进制
        /// </summary>
        public uint NumberBase
        {
            get
            {
                return Space.NumberBase;
            }
        }

        /// <summary>
        /// 数的正负性，正数为1，负数为-1，零为0
        /// </summary>
        public int PositiveOrNegative { get; protected set; } = 0;
        /// <summary>
        /// 最大保留小数位数
        /// </summary>
        public int MaxDecimalPlaces { get; protected set; } = OperationSpace.DefaultSpace.DefaultMaxDecimalPlaces;
        /// <summary>
        /// 计算空间
        /// </summary>
        public OperationSpace Space { get; protected set; } = OperationSpace.DefaultSpace;

        /// <summary>
        /// 初始化
        /// </summary>
        public Digitable() { }
        /// <summary>
        /// 拷贝初始化
        /// </summary>
        /// <param name="num">原始对象</param>
        /// <param name="maxDecimalPlaces">最大保留小数位数</param>
        public Digitable(Digitable num, int? maxDecimalPlaces = null)
        {
            PositiveOrNegative = num.PositiveOrNegative;
            Space = num.Space;
            MaxDecimalPlaces = maxDecimalPlaces.HasValue ? Math.Min(maxDecimalPlaces.Value, this.Space.DefaultMaxDecimalPlaces) : num.MaxDecimalPlaces;
        }

        public abstract IOperationSpaceElement ChangeOperationSpace(OperationSpace space = null, bool? newInstance = null, int? maxDecimalPlaces = null);

        public abstract string GetTeXCode();

        public abstract string GetNaturplayMathExp();

        /// <summary>
        /// 用拷贝初始化复制一个新的实例
        /// </summary>
        /// <returns></returns>
        public abstract Digitable Copy();

        /// <summary>
        /// 获得该实例包含的数串
        /// </summary>
        public abstract NumStr GetNumberString();

        public override int GetHashCode()
        {
            return 0;
        }

        public override string ToString()
        {
            return GetNaturplayMathExp();
        }
    }
}
