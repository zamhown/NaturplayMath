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
        /// 将小数点右移
        /// </summary>
        /// <param name="offset">移动位数</param>
        public override void RightShift(int offset)
        {
            if (offset == 0)
                return;
            else if (offset < 0)
            {
                LeftShift(-offset);
                return;
            }

            var ii = decimalNumList?.First;
            for (var i = 0; i < offset; i++)
            {
                uint d = 0;
                if (ii != null)
                {
                    d = ii.Value;
                    ii = ii.Next;
                    decimalNumList.RemoveFirst();
                }
                if (d > 0 || integerNumList != null)
                {
                    if (integerNumList == null)
                        integerNumList = new LinkedList<uint>();
                    integerNumList.AddFirst(d);
                }
            }
            if (decimalNumList?.Count == 0)
                decimalNumList = null;
        }
    }
}