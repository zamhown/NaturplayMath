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
        /// 用于进制转换的2次幂表
        /// </summary>
        protected static readonly List<uint> tkTable = new List<uint> { 2, 4, 8, 16, 32, 64, 128, 256, 512, 1024, 2048, 4096, 8192, 16384, 32768, 65536 };

        /// <summary>
        /// 转换内部进制
        /// </summary>
        /// <param name="container">容器（原有数值会丢失）</param>
        /// <param name="numberBase">新的进制</param>
        /// <param name="value">新的数值（不会改变原有内容）</param>
        protected static void NumberBaseConversion(NumStr container, uint numberBase, NumStr value = null)
        {
            if (value == null)
                value = new NumStr(container);
            if (numberBase == value.NumberBase)
            {
                if (value.integerNumList != null)
                {
                    container.integerNumList = new LinkedList<uint>();
                    foreach (var i in value.integerNumList)
                        container.integerNumList.AddLast(i);
                }
                if (container.MaxDecimalPlaces > 0 && value.decimalNumList != null)
                {
                    container.decimalNumList = new LinkedList<uint>();
                    var i = value.decimalNumList.First;
                    while (i != null && container.DecimalPlaces < container.MaxDecimalPlaces)
                    {
                        container.decimalNumList.AddLast(i.Value);
                        i = i.Next;
                    }
                }
            }
            else
            {
                //整数转换进制
                if (value.IntegerPlaces > 0)
                {
                    //特殊情况：2的幂进制之间互相转换
                    var i1 = tkTable.IndexOf(value.Space.NumberBase) + 1;
                    var i2 = tkTable.IndexOf(numberBase) + 1;
                    if (i1 > 0 && i2 > 0)
                    {
                        if (i2 % i1 == 0)
                        {
                            //特殊方法：相邻位组合
                            container.integerNumList = new LinkedList<uint>();
                            int q = i2 / i1, t = 0;
                            uint k = 1;
                            foreach (var v in value.integerNumList)
                            {
                                if (t == 0)
                                {
                                    container.integerNumList.AddLast(0);
                                    k = 1;
                                }
                                container.integerNumList.Last.Value += v * k;
                                t = (t + 1) % q;
                                k *= value.NumberBase;
                            }
                        }
                        else
                        {
                            //普通方法：先转为2进制
                            NumStr tmp = null;
                            if (value.Space.NumberBase == 2)
                                tmp = value;
                            else
                            {
                                NumStr tmpNum = value;
                                var two = new NumStr(2, tmpNum.Space);
                                while (!tmpNum.IsZero)
                                {
                                    var t = Mod(tmpNum, two, 0);
                                    t.Item1.PositiveOrNegative = t.Item1.IsEmpty() ? 0 : 1;
                                    if (tmp == null)
                                        tmp = new NumStr()
                                        {
                                            PositiveOrNegative = 1,
                                            integerNumList = new LinkedList<uint>()
                                        };
                                    tmp.integerNumList.AddLast((uint)t.Item1.ToInt64());
                                    tmpNum = t.Item2;
                                }
                            }
                            //再转换为目标进制，相邻位组合
                            if (numberBase == 2)
                                container.integerNumList = tmp.integerNumList;
                            else
                            {
                                container.integerNumList = new LinkedList<uint>();
                                int t = 0;
                                uint k = 1;
                                foreach (var v in tmp.integerNumList)
                                {
                                    if (t == 0)
                                    {
                                        container.integerNumList.AddLast(0);
                                        k = 1;
                                    }
                                    container.integerNumList.Last.Value += v * k;
                                    t = (t + 1) % i2;
                                    k *= 2;
                                }
                            }
                        }
                    }
                    else
                    {
                        //普通方法：先转为默认进制
                        NumStr tmp;
                        if (value.Space.NumberBase == OperationSpace.DefaultSpace.NumberBase)
                            tmp = value;
                        else
                        {
                            var ni = value.integerNumList.First;
                            tmp = ni.Value;
                            NumStr nb = null;
                            for (var i = 1; i < value.IntegerPlaces; i++)
                            {
                                ni = ni.Next;
                                if (nb == null)
                                    nb = value.NumberBase;
                                else
                                    nb *= value.NumberBase;
                                tmp += ni.Value * nb;
                            }
                        }
                        //再转换为目标进制
                        LinkedList<uint> tmpList = null;
                        if (numberBase == OperationSpace.DefaultSpace.NumberBase)
                            tmpList = tmp.integerNumList;
                        else
                        {
                            var nb = new NumStr(numberBase, tmp.Space);
                            while (!tmp.IsZero)
                            {
                                var t = Mod(tmp, nb, 0);
                                t.Item1.PositiveOrNegative = CompareAbsolute(t.Item1, 0) == 0 ? 0 : 1;
                                if (tmpList == null)
                                    tmpList = new LinkedList<uint>();
                                tmpList.AddLast((uint)t.Item1.ToInt64());
                                tmp = t.Item2;
                            }
                        }
                        container.integerNumList = tmpList;
                    }
                }
                //小数转换进制
                if (container.MaxDecimalPlaces > 0 && value.DecimalPlaces > 0)
                {
                    //先转为默认进制
                    NumStr tmp = null;
                    if (value.Space.NumberBase == OperationSpace.DefaultSpace.NumberBase)
                        tmp = value;
                    else
                    {
                        NumStr nb = new NumStr(1,
                            container.MaxDecimalPlaces == OperationSpace.DefaultSpace.DefaultMaxDecimalPlaces ?
                            OperationSpace.DefaultSpace
                            : new OperationSpace(container.MaxDecimalPlaces, OperationSpace.DefaultSpace.NumberBase));
                        tmp = new NumStr(0, nb.Space);
                        foreach (var ni in value.decimalNumList)
                        {
                            //todo: 用有理数类替换，提高精度
                            nb /= value.NumberBase;
                            tmp += ni * nb;
                        }
                    }
                    //再转换为目标进制
                    LinkedList<uint> tmpList = null;
                    if (numberBase == OperationSpace.DefaultSpace.NumberBase)
                        tmpList = tmp.decimalNumList;
                    else
                    {
                        tmpList = new LinkedList<uint>();
                        var zeroCount = 0;  //记录小数末尾0的数量
                        var nb = new NumStr(numberBase, tmp.Space);
                        do
                        {
                            tmp = new NumStr()
                            {
                                decimalNumList = tmp.decimalNumList,
                                PositiveOrNegative = tmp.DecimalPlaces == 0 ? 0 : tmp.PositiveOrNegative,
                                MaxDecimalPlaces = tmp.MaxDecimalPlaces,
                                Space = tmp.Space
                            };
                            tmp *= nb;
                            var v = (uint)tmp.ToInt64();
                            tmpList.AddLast(v);
                            if (v == 0)
                                zeroCount++;
                            else
                                zeroCount = 0;
                        }
                        while (tmp.DecimalPlaces > 0 && tmpList.Count < container.MaxDecimalPlaces);
                        //去除小数末尾0
                        if (zeroCount == tmpList.Count)
                            tmpList = null;
                        else
                            for (var i = 0; i < zeroCount; i++)
                                tmpList.RemoveLast();
                    }
                    container.decimalNumList = tmpList;
                }
            }
        }
    }
}