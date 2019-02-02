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
        /// 开平方运算
        /// </summary>
        /// <param name="maxDecimalPlaces">答案保留小数位数</param>
        /// <returns></returns>
        protected override Digitable Sqrt(int maxDecimalPlaces)
        {
            NumStr a = new NumStr(this);

            if (a == null)
                throw new ProgramInterruptException(ProgramInterruptExceptionType.IllegalValue);
            //特殊情况
            if (a.IsNegative)
                throw new IllegalOperationException();
            else if (a.IsZero)
                return new NumStr(0, a.Space, maxDecimalPlaces);

            //设进制为k，先将数字多次放大或缩小k^2倍，使其归一化为：1<=a<=k^2
            var one = new NaturalNumStr(1, a.Space);
            var k2 = new NaturalNumStr(a.Space.NumberBase * a.Space.NumberBase, a.Space);
            var shiftTimes = 0;
            while (a < one)
            {
                a.RightShift(2);
                shiftTimes++;
            }
            while (a > k2)
            {
                a.LeftShift(2);
                shiftTimes--;
            }

            //平方根初值x
            var x = new NumStr(0, a.Space);
            //x当前的有效数字位数
            var nx = 0;
            //x^2初值
            var x2 = new NumStr(0, a.Space);
            //(x+1)^2的值
            NumStr x12;
            do
            {
                //用完全平方公式计算(y+1)^2
                x12 = x2 + x + x + one;
                if (a < x12)
                {
                    //本位枚举完成，开始枚举下一位
                    x.RightShift(1);  //x=x*k
                    nx++;
                    x2.RightShift(2);  //x2=x2*k^2
                    a.RightShift(2);  //a=a*k^2
                }
                else
                {
                    //继续枚举
                    x = x + one;
                    x2 = x12;
                }
            } while (nx < maxDecimalPlaces + 1 - shiftTimes && a != x12);
            x.LeftShift(x.IntegerPlaces + x.DecimalPlaces - 1 + shiftTimes);
            return x;
        }
    }
}