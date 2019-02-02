using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturplayMath.Algebra
{
    /// <summary>
    /// 计算空间内的元素接口
    /// </summary>
    public interface IOperationSpaceElement
    {
        /// <summary>
        /// 改变元素的计算空间
        /// </summary>
        /// <param name="space">新的计算空间，为null即为默认计算空间</param>
        /// <param name="newInstance">是否返回新的实例（如果为null，即为不确定）</param>
        /// <param name="maxDecimalPlaces">最大保留小数位数</param>
        /// <returns></returns>
        IOperationSpaceElement ChangeOperationSpace(OperationSpace space = null, bool? newInstance = null, int? maxDecimalPlaces = null);
        /// <summary>
        /// 生成TeX公式代码
        /// </summary>
        /// <returns></returns>
        string GetTeXCode();
        /// <summary>
        /// 生成NaturplayMath表达式
        /// </summary>
        /// <returns></returns>
        string GetNaturplayMathExp();
    }
}
