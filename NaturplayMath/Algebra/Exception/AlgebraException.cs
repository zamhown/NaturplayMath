using System;
using System.Collections.Generic;
using System.Text;

namespace NaturplayMath.Algebra.Exception
{
    /// <summary>
    /// 错误类型枚举
    /// </summary>
    public enum AlgebraExceptionType
    {
        /// <summary>
        /// 无
        /// </summary>
        None,
        /// <summary>
        /// 数字超出范围
        /// </summary>
        NumberOutOfRange,
        /// <summary>
        /// 非法运算
        /// </summary>
        IllegalOperation,
        /// <summary>
        /// 格式错误
        /// </summary>
        FormatError,
        /// <summary>
        /// 程序中断错误
        /// </summary>
        ProgramInterrupt
    }

    /// <summary>
    /// 计算时触发的异常类
    /// </summary>
    public abstract class AlgebraException : System.Exception
    {
        public AlgebraExceptionType Type { get; protected set; } = AlgebraExceptionType.None;
    }
}
