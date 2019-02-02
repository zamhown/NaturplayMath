using NaturplayMath.Algebra.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturplayMath.Algebra.Scalar.NumberString
{
    /// <summary>
    /// 数字串类
    /// </summary>
    public partial class NumStr
    {
        /// <summary>
        /// 乘方运算符
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static NumStr operator ^(NumStr a, NumStr b) => ((a as Digitable) ^ (b as Digitable)) as NumStr;

        /// <summary>
        /// 乘方运算（不考虑正负号）
        /// </summary>
        /// <param name="num">指数</param>
        /// <param name="maxDecimalPlaces">答案保留小数位数</param>
        /// <returns></returns>
        protected override Digitable Power_unsigned(Digitable num, int maxDecimalPlaces)
        {
            NumStr a = this, b = num as NumStr;

            if (a == null || b == null)
                throw new ProgramInterruptException(ProgramInterruptExceptionType.IllegalValue);
            //统一计算空间
            if (a.Space != b.Space)
            {
                if (a.Space == OperationSpace.DefaultSpace)
                    a = (NumStr)a.ChangeOperationSpace(b.Space);
                else if (b.Space == OperationSpace.DefaultSpace)
                    b = (NumStr)b.ChangeOperationSpace(a.Space);
                else
                    throw new ProgramInterruptException(ProgramInterruptExceptionType.NotSameOperationSpace);
            }
            //特殊情况
            if (a.IsZero)
            {
                if (b.IsZero) throw new IllegalOperationException(IllegalOperationExceptionType.ZeroSquareOfZero);
                return new NumStr(0, a.Space, maxDecimalPlaces);
            }
            else if (b.IsZero)
            {
                return new NumStr(1, a.Space, maxDecimalPlaces);
            }

            NumStr ans;
            //将指数分割出整数，先做快速幂
            var split = b.SplitByDecimalPoint();
            if (!split.Item1.IsZero)
            {
                if (split.Item1.IsOne)
                {
                    ans = split.Item2.IsZero ? new NumStr(a, maxDecimalPlaces) : a;
                }
                else
                {
                    var tmp = a;
                    ans = new NumStr(1, a.Space, maxDecimalPlaces);
                    //将指数化为2进制
                    var binary = split.Item1.ChangeOperationSpace(new OperationSpace(0, 2)) as NumStr;
                    var count = 0;
                    foreach (var i in binary.integerNumList)
                    {
                        if (i == 1)
                        {
                            ans = ans.Multiply_unsigned(tmp, maxDecimalPlaces) as NumStr;
                            ans.PositiveOrNegative = 1;  //小心符号陷阱
                        }
                        if (++count < binary.integerNumList.Count)
                        {
                            tmp = tmp.Multiply_unsigned(tmp, maxDecimalPlaces) as NumStr;
                            tmp.PositiveOrNegative = 1;  //小心符号陷阱
                        }
                    }
                }
            }
            else
                ans = new NumStr(1, a.Space, maxDecimalPlaces);

            //小数部分乘方
            
            return ans;
        }
    }
}