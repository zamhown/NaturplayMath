using NaturplayMath.Algebra.Exception;
using NaturplayMath.Algebra.Scalar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturplayMath.Algebra
{
    /// <summary>
    /// 计算空间类，包含适用于一系列运算的基本的、统一的设置。一般而言不支持跨空间的运算
    /// </summary>
    public partial class OperationSpace
    {
        /// <summary>
        /// 默认保留小数位数，也是该空间内保留小数位数的最大值
        /// </summary>
        public int DefaultMaxDecimalPlaces { get; protected set; }
        /// <summary>
        /// 进制
        /// </summary>
        public uint NumberBase { get; protected set; }

        /// <summary>
        /// 默认计算空间，参与运算时如果操作数的计算空间不同，属于默认计算空间的数会转化到其他空间
        /// </summary>
        public static readonly OperationSpace DefaultSpace = new OperationSpace();

        public OperationSpace(
            int maxDecimalPlaces = DefaultSettings.DefaultDecimalPlaces,
            uint numberBase = DefaultSettings.DefaultNumberBase)
        {
            //进制不能低于2进制，不能高于2^16进制，最大位数需要不小于0
            if (numberBase < 2 || numberBase > DefaultSettings.MaxNumberBase || maxDecimalPlaces < 0)
                throw new FormatErrorException();
            DefaultMaxDecimalPlaces = maxDecimalPlaces;
            NumberBase = numberBase;
        }
    }
}
