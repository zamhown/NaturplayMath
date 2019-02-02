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
        /// 乘法运算符
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static NumStr operator *(NumStr a, NumStr b) => ((a as Digitable) * (b as Digitable)) as NumStr;

        /// <summary>
        /// 按位乘法运算（不考虑正负号）
        /// </summary>
        /// <param name="num">乘数</param>
        /// <param name="maxDecimalPlaces">答案保留小数位数</param>
        /// <returns></returns>
        protected override Digitable Multiply_unsigned(Digitable num, int maxDecimalPlaces)
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
            //特殊情况
            if (a.IsZero || b.IsZero)
                return ans;

            var ansDecimalPlaces = a.DecimalPlaces + b.DecimalPlaces;
            //更换乘数
            if (a.integerNumList == null && a.decimalNumList?.First.Value == 0)
                a = a.RemoveDecimalPoint();
            if (b.integerNumList == null && b.decimalNumList?.First.Value == 0)
                b = b.RemoveDecimalPoint();

            long remain = 0;
            var maxAnsPlaces = DefaultSettings.MaxIntegerPlaces + ansDecimalPlaces;
            var tmpList = new LinkedList<uint>();  //缓存链
            var ptrHead = tmpList.First;
            var ptr = tmpList.First;
            foreach (var bi in b)
            {
                if (ptrHead == null)
                {
                    tmpList.AddLast(0);
                    if (tmpList.Count > maxAnsPlaces)
                        throw new NumberOutOfRangeException();
                    ptrHead = tmpList.Last;
                }
                ptr = ptrHead;
                if (bi > 0)
                {
                    foreach (var ai in a)
                    {
                        var product = ai * bi + remain;
                        remain = product / ans.NumberBase;
                        product %= ans.NumberBase;
                        if (ptr == null)
                        {
                            tmpList.AddLast((uint)product);
                            if (tmpList.Count > maxAnsPlaces)
                                throw new NumberOutOfRangeException();
                            ptr = tmpList.Last;
                        }
                        else
                            ptr.Value += (uint)product;
                        ptr = ptr.Next;
                    }
                    if (remain > 0)
                    {
                        if (ptr == null)
                        {
                            tmpList.AddLast((uint)remain);
                            if (tmpList.Count > maxAnsPlaces)
                                throw new NumberOutOfRangeException();
                        }
                        else
                            ptr.Value += (uint)remain;
                        remain = 0;
                    }
                }
                ptrHead = ptrHead.Next;
            }
            //依次进位
            remain = 0;
            ptr = tmpList.First;
            while (ptr != null)
            {
                ptr.Value += (uint)remain;
                if (ptr.Value >= ans.NumberBase)
                {
                    remain = ptr.Value / ans.NumberBase;
                    ptr.Value %= ans.NumberBase;
                }
                else
                    remain = 0;
                ptr = ptr.Next;
            }
            if (remain > 0)
            {
                tmpList.AddLast((uint)remain);
                if (tmpList.Count > maxAnsPlaces)
                    throw new NumberOutOfRangeException();
            }
            //录入答案
            ptr = tmpList.First;
            //调整光标位置
            var tmpn = Math.Max(0, (long)ansDecimalPlaces - (long)maxDecimalPlaces);
            for (int i = 0; i < tmpn; i++)
                ptr = ptr.Next;
            var DecimalPlaces = Math.Min(ansDecimalPlaces, maxDecimalPlaces);
            bool flag = true; //判断小数位最高位是否为0
            for (int i = 0; i < DecimalPlaces; i++)
            {
                if (ptr == null)
                    ans.decimalNumList.AddFirst(0);
                else
                {
                    if (ptr.Value > 0 || !flag)
                    {
                        if (flag)
                        {
                            flag = false;
                            ans.decimalNumList = new LinkedList<uint>();
                        }
                        ans.decimalNumList.AddFirst(ptr.Value);
                    }
                    ptr = ptr.Next;
                }
            }
            if (ptr != null)
            {
                ans.integerNumList = new LinkedList<uint>();
                do
                {
                    ans.integerNumList.AddLast(ptr.Value);
                    ptr = ptr.Next;
                }
                while (ptr != null);
            }
            return ans;
        }
    }
}