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
        /// 加法运算符
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static NumStr operator +(NumStr a, NumStr b) => (a as Digitable + b as Digitable) as NumStr;

        /// <summary>
        /// 按位加法运算（不考虑正负号）
        /// </summary>
        /// <param name="num">另一个加数</param>
        /// <param name="maxDecimalPlaces">答案保留小数位数</param>
        /// <returns></returns>
        protected override Digitable Plus_unsigned(Digitable num, int maxDecimalPlaces)
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
            var ans = new NumStr(0, a.Space, maxDecimalPlaces);
            long remain = 0;
            //运算小数
            if (maxDecimalPlaces > 0)
            {
                if (a.decimalNumList != null && b.decimalNumList == null)
                {
                    ans.decimalNumList = new LinkedList<uint>();
                    var ai = a.decimalNumList.Last;
                    var an = a.DecimalPlaces;
                    //调整光标位置
                    if (a.DecimalPlaces > maxDecimalPlaces)
                        for (var i = 0; i < a.DecimalPlaces - maxDecimalPlaces; i++)
                        {
                            ai = ai.Previous;
                            an--;
                        }
                    for (var i = 0; i < an; i++)
                    {
                        ans.decimalNumList.AddFirst(ai.Value);
                        ai = ai.Previous;
                    }
                }
                else if (b.decimalNumList != null && a.decimalNumList == null)
                {
                    ans.decimalNumList = new LinkedList<uint>();
                    var bi = b.decimalNumList.Last;
                    var bn = b.DecimalPlaces;
                    //调整光标位置
                    if (b.DecimalPlaces > maxDecimalPlaces)
                        for (var i = 0; i < b.DecimalPlaces - maxDecimalPlaces; i++)
                        {
                            bi = bi.Previous;
                            bn--;
                        }
                    for (var i = 0; i < bn; i++)
                    {
                        ans.decimalNumList.AddFirst(bi.Value);
                        bi = bi.Previous;
                    }
                }
                else if (a.decimalNumList != null && b.decimalNumList != null)
                {
                    var ai = a.decimalNumList.Last;
                    var bi = b.decimalNumList.Last;
                    var an = a.DecimalPlaces;
                    var bn = b.DecimalPlaces;
                    //调整光标位置
                    if (a.DecimalPlaces > maxDecimalPlaces)
                        for (var i = 0; i < a.DecimalPlaces - maxDecimalPlaces; i++)
                        {
                            ai = ai.Previous;
                            an--;
                        }
                    if (b.DecimalPlaces > maxDecimalPlaces)
                        for (var i = 0; i < b.DecimalPlaces - maxDecimalPlaces; i++)
                        {
                            bi = bi.Previous;
                            bn--;
                        }
                    bool flag = true; //判断小数位最高位是否为0
                    if (an > bn)
                    {
                        ans.decimalNumList = new LinkedList<uint>();
                        for (var i = 0; i < an - bn; i++)
                        {
                            ans.decimalNumList.AddFirst(ai.Value);
                            ai = ai.Previous;
                        }
                        an = bn;
                        flag = false;
                    }
                    else if (bn > an)
                    {
                        ans.decimalNumList = new LinkedList<uint>();
                        for (var i = 0; i < bn - an; i++)
                        {
                            ans.decimalNumList.AddFirst(bi.Value);
                            bi = bi.Previous;
                        }
                        bn = an;
                        flag = false;
                    }
                    //按位相加
                    long sum = 0;
                    for (var i = 0; i < an; i++)
                    {
                        sum = ai.Value + bi.Value + remain;
                        remain = sum / ans.NumberBase;
                        sum = sum % ans.NumberBase;
                        if (sum > 0 || !flag)
                        {
                            if (flag)
                            {
                                flag = false;
                                ans.decimalNumList = new LinkedList<uint>();
                            }
                            ans.decimalNumList.AddFirst((uint)sum);
                        }
                        ai = ai.Previous;
                        bi = bi.Previous;
                    }
                }
            }
            //运算整数
            if (a.integerNumList != null && b.integerNumList == null)
            {
                ans.integerNumList = new LinkedList<uint>();
                foreach (var i in a.integerNumList)
                    ans.integerNumList.AddLast(i);
            }
            else if (b.integerNumList != null && a.integerNumList == null)
            {
                ans.integerNumList = new LinkedList<uint>();
                foreach (var i in b.integerNumList)
                    ans.integerNumList.AddLast(i);
            }
            else if (a.integerNumList != null && b.integerNumList != null)
            {
                ans.integerNumList = new LinkedList<uint>();
                var ai = a.integerNumList.First;
                var bi = b.integerNumList.First;
                //按位相加
                long sum = 0;
                var n = Math.Max(a.IntegerPlaces, b.IntegerPlaces);
                for (var i = 0; i < n; i++)
                {
                    sum = (ai?.Value).GetValueOrDefault() + (bi?.Value).GetValueOrDefault() + remain;
                    remain = sum / ans.NumberBase;
                    sum = sum % ans.NumberBase;
                    ans.integerNumList.AddLast((uint)sum);
                    ai = ai?.Next;
                    bi = bi?.Next;
                }
                if (remain > 0)
                {
                    if (ans.IntegerPlaces >= DefaultSettings.MaxIntegerPlaces)
                        throw new NumberOutOfRangeException();
                    ans.integerNumList.AddLast((uint)remain);
                }
            }
            return ans;
        }
    }
}