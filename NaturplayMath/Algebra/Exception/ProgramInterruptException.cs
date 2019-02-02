using System;
using System.Collections.Generic;
using System.Text;

namespace NaturplayMath.Algebra.Exception
{
    /// <summary>
    /// 程序中断错误类型枚举
    /// </summary>
    public enum ProgramInterruptExceptionType
    {
        /// <summary>
        /// 无
        /// </summary>
        None,
        /// <summary>
        /// 参与运算的量取了非法值（如null）
        /// </summary>
        IllegalValue,
        /// <summary>
        /// 参与运算的量不在一个计算空间内
        /// </summary>
        NotSameOperationSpace
    }

    /// <summary>
    /// 程序中断异常类
    /// </summary>
    public class ProgramInterruptException : AlgebraException
    {
        /// <summary>
        /// 程序中断详细类型
        /// </summary>
        public ProgramInterruptExceptionType SecondType { get; protected set; } = ProgramInterruptExceptionType.None;

        public ProgramInterruptException()
        {
            Type = AlgebraExceptionType.ProgramInterrupt;
        }
        public ProgramInterruptException(ProgramInterruptExceptionType stype)
            : this()
        {
            SecondType = stype;
        }
    }
}
