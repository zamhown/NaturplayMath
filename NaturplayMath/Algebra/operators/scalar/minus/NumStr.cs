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
        /// 减法运算符
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static NumStr operator -(NumStr a, NumStr b) => (a as Digitable - b as Digitable) as NumStr;

        /// <summary>
        /// 按位减法运算（不考虑正负号）
        /// </summary>
        /// <param name="num">减数</param>
        /// <param name="maxDecimalPlaces">答案保留小数位数</param>
        /// <param name="hasSwaped">是否发生参数交换</param>
        /// <returns></returns>
        protected override Digitable Minus_unsigned(Digitable num, int maxDecimalPlaces, out bool hasSwaped)
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
            //判断两参数大小，进行调整
            if (CompareAbsolute(a, b) < 0)
            {
                var t = a;
                a = b;
                b = t;
                hasSwaped = true;
            }
            else
                hasSwaped = false;
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
                    ans.decimalNumList.AddFirst(ans.NumberBase - bi.Value);
                    bi = bi.Previous;
                    for (var i = 1; i < bn; i++)
                    {
                        ans.decimalNumList.AddFirst(ans.NumberBase - 1 - bi.Value);
                        bi = bi.Previous;
                    }
                    remain = 1;
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
                    }
                    else if (bn > an)
                    {
                        ans.decimalNumList = new LinkedList<uint>();
                        ans.decimalNumList.AddFirst(ans.NumberBase - bi.Value);
                        bi = bi.Previous;
                        for (var i = 1; i < bn - an; i++)
                        {
                            ans.decimalNumList.AddFirst(ans.NumberBase - 1 - bi.Value);
                            bi = bi.Previous;
                        }
                        bn = an;
                        remain = 1;
                    }
                    //按位相减
                    long diff = 0;
                    for (var i = 0; i < an; i++)
                    {
                        bool hasBorrowed = false;  //这次是否已借位
                        long new_av;
                        if (ai.Value < remain)
                        {
                            new_av = ans.NumberBase - 1;
                            hasBorrowed = true;
                        }
                        else
                            new_av = ai.Value - remain;
                        if (new_av >= bi.Value)
                            diff = new_av - bi.Value;
                        else
                        {
                            diff = new_av + ans.NumberBase - bi.Value;
                            hasBorrowed = true;
                        }
                        if (diff > 0 || !flag)
                        {
                            if (flag)
                            {
                                flag = false;
                                ans.decimalNumList = new LinkedList<uint>();
                            }
                            ans.decimalNumList.AddFirst((uint)diff);
                        }
                        ai = ai.Previous;
                        bi = bi.Previous;
                        remain = hasBorrowed ? 1 : 0;
                    }
                }
            }
            //运算整数
            if (a.integerNumList != null && b.integerNumList == null)
            {
                ans.integerNumList = new LinkedList<uint>();
                var ai = a.integerNumList.First;
                //按位相减
                var n = a.IntegerPlaces;
                for (var i = 0; i < n; i++)
                {
                    bool hasBorrowed = false;  //这次是否已借位
                    long new_av;
                    if (ai.Value < remain)
                    {
                        new_av = ans.NumberBase - 1;
                        hasBorrowed = true;
                    }
                    else
                        new_av = ai.Value - remain;
                    ans.integerNumList.AddLast((uint)new_av);
                    ai = ai.Next;
                    remain = hasBorrowed ? 1 : 0;
                }
                //消除高位上的0
                var ansi = ans.integerNumList.Last;
                while (ansi != null && ansi.Value == 0)
                {
                    ans.integerNumList.RemoveLast();
                    ansi = ans.integerNumList.Last;
                }
                if (ans.IntegerPlaces == 0)
                    ans.integerNumList = null;
            }
            else if (b.integerNumList != null && a.integerNumList == null)
            {
                //不可能发生
                throw new ProgramInterruptException(ProgramInterruptExceptionType.IllegalValue);
            }
            else if (a.integerNumList != null && b.integerNumList != null)
            {
                ans.integerNumList = new LinkedList<uint>();
                var ai = a.integerNumList.First;
                var bi = b.integerNumList.First;
                //按位相减
                long diff = 0;
                var n = a.IntegerPlaces;
                for (var i = 0; i < n; i++)
                {
                    bool hasBorrowed = false;  //这次是否已借位
                    long new_av;
                    if (ai.Value < remain)
                    {
                        new_av = ans.NumberBase - 1;
                        hasBorrowed = true;
                    }
                    else
                        new_av = ai.Value - remain;
                    var new_bv = bi == null ? 0 : bi.Value;
                    if (new_av >= new_bv)
                        diff = new_av - new_bv;
                    else
                    {
                        diff = new_av + ans.NumberBase - new_bv;
                        hasBorrowed = true;
                    }
                    ans.integerNumList.AddLast((uint)diff);
                    ai = ai.Next;
                    bi = bi?.Next;
                    remain = hasBorrowed ? 1 : 0;
                }
                //消除高位上的0
                var ansi = ans.integerNumList.Last;
                while (ansi != null && ansi.Value == 0)
                {
                    ans.integerNumList.RemoveLast();
                    ansi = ans.integerNumList.Last;
                }
                if (ans.IntegerPlaces == 0)
                    ans.integerNumList = null;
            }
            return ans;
        }
    }
}