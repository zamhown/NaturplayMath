using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturplayMath.Algebra.TeX.Elements
{
    /// <summary>
    /// TeX公式元素基类
    /// </summary>
    public class TElement
    {
        /// <summary>
        /// 解析出该元素的原始文本
        /// </summary>
        public string OriginalText = null;

        /// <summary>
        /// 获取该元素的TeX公式代码
        /// </summary>
        /// <returns></returns>
        public virtual string GetTeXCode()
        {
            return "";
        }
    }
}
