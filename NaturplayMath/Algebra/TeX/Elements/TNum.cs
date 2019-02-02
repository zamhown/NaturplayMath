using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturplayMath.Algebra.TeX.Elements
{
    /// <summary>
    /// TeX公式的数字元素
    /// </summary>
    public class TNum
    {
        /// <summary>
        /// 包含的数字
        /// </summary>
        public string Value;

        public TNum(string num = "")
        {
            Value = num;
        }
    }
}
