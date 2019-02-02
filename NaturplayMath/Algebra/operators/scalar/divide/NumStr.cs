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
        /// 除法运算符
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static NumStr operator /(NumStr a, NumStr b) => ((a as Digitable) / (b as Digitable)) as NumStr;

        /// <summary>
        /// 按位除法运算（不考虑正负号）
        /// </summary>
        /// <param name="num">除数</param>
        /// <param name="maxDecimalPlaces">答案保留小数位数</param>
        /// <returns></returns>
        protected override Digitable Divide_unsigned(Digitable num, int maxDecimalPlaces)
        {
            NumStr a = this, b = num as NumStr;

            //方法：手动模拟法 + 二分查找优化
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
            //特殊情况
            if (a.IsZero) return ans;
            else if (b.IsZero) throw new IllegalOperationException(IllegalOperationExceptionType.DivideByZero);

            int a_ptr = 0;
            var a_integerPlaces = a.IntegerPlaces + b.DecimalPlaces;  //被除数的小数点位置

            if (b.decimalNumList != null)
                b = b.RemoveDecimalPoint();
            var an = a.IntegerPlaces + a.DecimalPlaces;
            var bn = b.IntegerPlaces;

            var new_a = new NumStr(0, a.Space, 0);  //被除数片段
            var b_table = new NumStr[ans.NumberBase];  //除数的倍数表
            var aii = a.GetInverseEnumerator();
            var bii = b.GetInverseEnumerator();
            bool? a_less_than_b = null;
            LinkedListNode<uint> node_ptr = null;
            while (true)
            {
                //补位
                bii = b.GetInverseEnumerator();
                a_less_than_b = null;
                if (new_a.IsPositive)
                {
                    var aii2 = new_a.GetInverseEnumerator();
                    while (a_less_than_b == null && aii2.MoveNext() && bii.MoveNext())
                    {
                        if (aii2.Current < bii.Current)
                            a_less_than_b = true;
                        else if (aii2.Current > bii.Current)
                            a_less_than_b = false;
                    }
                }
                do
                {
                    var v = aii.MoveNext() ? aii.Current : 0;
                    if (v > 0 || new_a.IsPositive)
                    {
                        if (new_a.IsZero)
                        {
                            new_a.PositiveOrNegative = 1;
                            new_a.integerNumList = new LinkedList<uint>();
                        }
                        new_a.integerNumList.AddFirst(v);
                        bii.MoveNext();
                        if (a_less_than_b == null)
                        {
                            if (v < bii.Current)
                                a_less_than_b = true;
                            else if (v > bii.Current)
                                a_less_than_b = false;
                        }
                    }
                    a_ptr++;
                    if (a_ptr <= a_integerPlaces)
                    {
                        if (ans.IsZero)
                        {
                            ans.PositiveOrNegative = 1;
                            ans.integerNumList = new LinkedList<uint>();
                        }
                        if (ans.IntegerPlaces != 1 || ans.integerNumList.Last.Value > 0)
                        {
                            ans.integerNumList.AddFirst(0);
                            if (ans.IntegerPlaces > DefaultSettings.MaxIntegerPlaces)
                                throw new NumberOutOfRangeException();
                            node_ptr = ans.integerNumList.First;
                        }
                    }
                    else
                    {
                        if (ans.DecimalPlaces == maxDecimalPlaces) return ans;
                        if (ans.decimalNumList == null)
                            ans.decimalNumList = new LinkedList<uint>();
                        ans.decimalNumList.AddLast(0);
                        node_ptr = ans.decimalNumList.Last;
                    }
                } while (new_a.IntegerPlaces < bn + (a_less_than_b == true ? 1 : 0));
                //做整除
                var quotient = Divide_unsigned_b_table_binary_search(b_table, b, new_a);
                if (node_ptr != null)
                {
                    node_ptr.Value = quotient;
                    node_ptr = null;
                }
                else
                {
                    if (a_ptr <= a_integerPlaces)
                    {
                        if (ans.integerNumList == null)
                            ans.integerNumList = new LinkedList<uint>();
                        ans.integerNumList.AddFirst(quotient);
                        if (ans.IntegerPlaces > DefaultSettings.MaxIntegerPlaces)
                            throw new NumberOutOfRangeException();
                    }
                    else
                    {
                        if (ans.decimalNumList == null)
                            ans.decimalNumList = new LinkedList<uint>();
                        ans.decimalNumList.AddLast(quotient);
                    }
                }
                if (maxDecimalPlaces > 0 && ans.DecimalPlaces >= maxDecimalPlaces) return ans;
                //求余
                new_a = new_a - b_table[quotient];
                if (new_a.IsZero && a_ptr >= an)
                    return ans;
            }
        }

        /// <summary>
        /// 按位除法运算的除数倍数表的二分查找
        /// </summary>
        /// <param name="b_table">数组</param>
        /// <param name="b">除数</param>
        /// <param name="v">要查找的值</param>
        /// <returns></returns>
        protected static uint Divide_unsigned_b_table_binary_search(NumStr[] b_table, NumStr b, NumStr v)
        {
            uint h = 0, t = (uint)b_table.Length, m;
            do
            {
                m = (h + t) / 2;
                if (b_table[m] == null)
                    b_table[m] = b * m;
                var cpr = CompareAbsolute(v, b_table[m]);
                if (cpr == 0)
                    return m;
                else if (cpr < 0)
                    t = m - 1;
                else
                    h = m;
            }
            while (t - h > 1);
            if (h == t)
                return h;
            else
            {
                if (t == b_table.Length)
                    return h;
                if (b_table[t] == null)
                    b_table[t] = b * t;
                if (CompareAbsolute(v, b_table[t]) < 0)
                {
                    if (b_table[h] == null)
                        b_table[h] = b * h;
                    return h;
                }
                else
                    return t;
            }
        }
    }
}