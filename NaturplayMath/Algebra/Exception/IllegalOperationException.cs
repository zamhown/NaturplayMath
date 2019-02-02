using System;
using System.Collections.Generic;
using System.Text;

namespace NaturplayMath.Algebra.Exception
{
    /// <summary>
    /// 非法运算错误类型枚举
    /// </summary>
    public enum IllegalOperationExceptionType
    {
        /// <summary>
        /// 无
        /// </summary>
        None,
        /// <summary>
        /// 除以0
        /// </summary>
        DivideByZero,
        /// <summary>
        /// 0的0次方
        /// </summary>
        ZeroSquareOfZero,
        /// <summary>
        /// 真数不为正
        /// </summary>
        AntilogIsNotPositive
    }

    /// <summary>
    /// 非法运算异常类
    /// </summary>
    public class IllegalOperationException : AlgebraException
    {
        /// <summary>
        /// 非法运算详细类型
        /// </summary>
        public IllegalOperationExceptionType SecondType { get; protected set; } = IllegalOperationExceptionType.None; 

        public IllegalOperationException()
        {
            Type = AlgebraExceptionType.IllegalOperation;
        }
        public IllegalOperationException(IllegalOperationExceptionType stype)
            :this()
        {
            SecondType = stype;
        }
    }
}
