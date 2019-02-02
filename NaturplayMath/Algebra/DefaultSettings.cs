using System;
using System.Collections.Generic;
using System.Text;

namespace NaturplayMath.Algebra
{
    /// <summary>
    /// 默认设置静态类
    /// </summary>
    public static class DefaultSettings
    {
        /// <summary>
        /// 最大整数位数
        /// </summary>
        public const int MaxIntegerPlaces = 10000;
        /// <summary>
        /// 默认最大保留小数位数
        /// </summary>
        public const int DefaultDecimalPlaces = 10;
        /// <summary>
        /// 最大保留小数位数
        /// </summary>
        public const int MaxDecimalPlaces = int.MaxValue;
        /// <summary>
        /// 默认进制
        /// </summary>
        public const uint DefaultNumberBase = 10;
        /// <summary>
        /// 最高支持进制
        /// </summary>
        public const uint MaxNumberBase = 65536;
    }
}
