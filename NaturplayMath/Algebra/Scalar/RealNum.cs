using NaturplayMath.Algebra.Scalar.NumberString;
using System;
using System.Collections.Generic;
using System.Text;

namespace NaturplayMath.Algebra.Scalar
{
    /// <summary>
    /// 实数类
    /// </summary>
    public abstract class RealNum : Digitable
    {
        /// <summary>
        /// 数值
        /// </summary>
        protected NumStr value = null;
        /// <summary>
        /// 监测数值是否可能改变的哨兵
        /// </summary>
        protected bool valueChanged = true;

        /// <summary>
        /// 整数位数
        /// </summary>
        public int IntegerPlaces
        {
            get
            {
                return value.IntegerPlaces;
            }
        }
        /// <summary>
        /// 小数位数
        /// </summary>
        public int DecimalPlaces
        {
            get
            {
                return value.DecimalPlaces;
            }
        }

        /// <summary>
        /// 默认构造函数，初始化为0
        /// </summary>
        public RealNum()
        {
            value = new NumStr(0, Space, MaxDecimalPlaces);
        }
        /// <summary>
        /// 用纯数字初始化
        /// </summary>
        /// <param name="num">数字</param>
        /// <param name="space">计算空间</param>
        /// <param name="maxDecimalPlaces">最大保留小数位数</param>
        public RealNum(NumStr num, OperationSpace space = null, int? maxDecimalPlaces = null)
        {
            this.Space = space ?? (num != null ? num.Space : OperationSpace.DefaultSpace);
            this.MaxDecimalPlaces = maxDecimalPlaces.HasValue ? Math.Min(maxDecimalPlaces.Value, this.Space.DefaultMaxDecimalPlaces) : this.Space.DefaultMaxDecimalPlaces;
            if (num != null)
            {
                this.PositiveOrNegative = num.PositiveOrNegative;
                value = new NumStr(num, Space, MaxDecimalPlaces);
            }
        }
        /// <summary>
        /// 拷贝初始化
        /// </summary>
        /// <param name="num">原始对象</param>
        /// <param name="maxDecimalPlaces">最大保留小数位数</param>
        /// <param name="deep">是否为深拷贝</param>
        public RealNum(RealNum num, int? maxDecimalPlaces = null, bool deep = true)
            :base(num, maxDecimalPlaces)
        {
            if (num.value != null)
            {
                if (deep)
                    value = new NumStr(num.value, null, true);
                else
                    value = num.value;
            }
            else
                value = null;
        }

        /// <summary>
        /// 计算、获得实际值
        /// </summary>
        /// <returns></returns>
        public virtual NumStr GetValue() { return value; }

        public override NumStr GetNumberString()
        {
            return this.GetValue();
        }
    }
}
