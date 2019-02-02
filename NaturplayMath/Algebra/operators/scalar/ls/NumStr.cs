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
        /// 将小数点左移
        /// </summary>
        /// <param name="offset">移动位数</param>
        public override void LeftShift(int offset)
        {
            if (offset == 0)
                return;
            else if (offset < 0)
            {
                RightShift(-offset);
                return;
            }

            var ii = integerNumList?.First;
            for (var i = 0; i < offset; i++)
            {
                uint d = 0;
                if (ii != null)
                {
                    d = ii.Value;
                    ii = ii.Next;
                    integerNumList.RemoveFirst();
                }
                else
                {
                    integerNumList = null;
                    //前瞻
                    if (offset - i >= MaxDecimalPlaces)
                    {
                        decimalNumList = null;
                        break;
                    }
                }
                if (d > 0 || decimalNumList != null)
                {
                    if (decimalNumList == null)
                        decimalNumList = new LinkedList<uint>();
                    decimalNumList.AddFirst(d);
                    if (DecimalPlaces > MaxDecimalPlaces)
                        decimalNumList.RemoveLast();
                }
            }
        }
    }
}