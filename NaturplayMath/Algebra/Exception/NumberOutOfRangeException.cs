using System;
using System.Collections.Generic;
using System.Text;

namespace NaturplayMath.Algebra.Exception
{
    /// <summary>
    /// 数字超出范围异常类
    /// </summary>
    public class NumberOutOfRangeException : AlgebraException
    {
        public NumberOutOfRangeException()
        {
            Type = AlgebraExceptionType.NumberOutOfRange;
        }
    }
}
