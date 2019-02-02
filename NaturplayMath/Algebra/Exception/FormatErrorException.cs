using System;
using System.Collections.Generic;
using System.Text;

namespace NaturplayMath.Algebra.Exception
{
    /// <summary>
    /// 格式错误异常类
    /// </summary>
    public class FormatErrorException : AlgebraException
    {
        public FormatErrorException()
        {
            Type = AlgebraExceptionType.FormatError;
        }
    }
}
