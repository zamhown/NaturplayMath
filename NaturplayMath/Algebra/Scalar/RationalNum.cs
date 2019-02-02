using NaturplayMath.Algebra.Exception;
using NaturplayMath.Algebra.Scalar.NumberString;
using System;
using System.Collections.Generic;
using System.Text;

namespace NaturplayMath.Algebra.Scalar
{
    /// <summary>
    /// 有理数类
    /// </summary>
    public partial class RationalNum : RealNum, IEquatable<RationalNum>
    {
        /// <summary>
        /// 分母
        /// </summary>
        protected NaturalNumStr denominator;
        /// <summary>
        /// 分子
        /// </summary>
        protected NaturalNumStr numerator;

        public override bool IsOne
        {
            get
            {
                return numerator.IsOne && denominator.IsOne;
            }
        }

        public override bool IsInteger
        {
            get
            {
                return denominator.IsOne;
            }
        }

        public override bool IsEven
        {
            get
            {
                return denominator.IsOne && numerator.IsEven;
            }
        }

        public override bool IsOdd
        {
            get
            {
                return denominator.IsOne && numerator.IsOdd;
            }
        }

        /// <summary>
        /// 默认构造函数，初始化为0
        /// </summary>
        public RationalNum()
        {
            numerator = 0;
            denominator = 1;
        }
        /// <summary>
        /// 用纯数字初始化
        /// </summary>
        /// <param name="num">数字</param>
        /// <param name="space">计算空间</param>
        /// <param name="maxDecimalPlaces">最大保留小数位数</param>
        public RationalNum(NumStr num, OperationSpace space = null, int? maxDecimalPlaces = null)
            : base(num, space, maxDecimalPlaces)
        {
            space = space ?? num.Space;
            if (num.DecimalPlaces == 0)
            {
                numerator = new NaturalNumStr(num, space);
                denominator = new NaturalNumStr(1, space);
            }
            else
            {
                numerator = new NaturalNumStr(num.RemoveDecimalPoint(), space);
                denominator = new NaturalNumStr(1, space);
                denominator.RightShift(num.DecimalPlaces);
                FractionReduction();
            }
        }
        /// <summary>
        /// 用分式初始化
        /// </summary>
        /// <param name="numerator">分子</param>
        /// <param name="denominator">分母</param>
        /// <param name="positiveOrNegative">正负性</param>
        /// <param name="space">计算空间</param>
        /// <param name="maxDecimalPlaces">最大保留小数位数</param>
        public RationalNum(NaturalNumStr numerator, NaturalNumStr denominator, int positiveOrNegative = 1, OperationSpace space = null, int? maxDecimalPlaces = null)
            : base(null, space ?? numerator.Space, maxDecimalPlaces)
        {
            if (space == null && numerator.Space != denominator.Space)
                throw new ProgramInterruptException(ProgramInterruptExceptionType.IllegalValue);
            else
                space = space ?? numerator.Space;
            this.numerator = (NaturalNumStr)numerator.ChangeOperationSpace(space);
            this.denominator = (NaturalNumStr)denominator.ChangeOperationSpace(space);
            this.PositiveOrNegative = positiveOrNegative;
            FractionReduction();
        }
        /// <summary>
        /// 用分式初始化
        /// </summary>
        /// <param name="numerator">分子</param>
        /// <param name="denominator">分母</param>
        /// <param name="positiveOrNegative">正负性</param>
        /// <param name="space">计算空间</param>
        /// <param name="maxDecimalPlaces">最大保留小数位数</param>
        public RationalNum(RationalNum numerator, RationalNum denominator, int? positiveOrNegative = null, OperationSpace space = null, int? maxDecimalPlaces = null)
            : this(numerator.numerator * denominator.denominator, numerator.denominator * denominator.numerator, positiveOrNegative ?? numerator.PositiveOrNegative * denominator.PositiveOrNegative, space, maxDecimalPlaces)
        { }
        /// <summary>
        /// 用分式初始化
        /// </summary>
        /// <param name="numerator">分子</param>
        /// <param name="denominator">分母</param>
        /// <param name="positiveOrNegative">正负性</param>
        /// <param name="space">计算空间</param>
        /// <param name="maxDecimalPlaces">最大保留小数位数</param>
        public RationalNum(IntegerNum numerator, IntegerNum denominator, int? positiveOrNegative = null, OperationSpace space = null, int? maxDecimalPlaces = null)
            : this(numerator.numerator, denominator.numerator, positiveOrNegative ?? numerator.PositiveOrNegative * denominator.PositiveOrNegative, space, maxDecimalPlaces)
        { }
        /// <summary>
        /// 拷贝初始化
        /// </summary>
        /// <param name="num">原始对象</param>
        /// <param name="maxDecimalPlaces">最大保留小数位数</param>
        /// <param name="deep">是否为深拷贝</param>
        public RationalNum(RationalNum num, int? maxDecimalPlaces = null, bool deep = true)
            : base(num, maxDecimalPlaces, deep)
        {
            if (deep)
            {
                numerator = new NaturalNumStr(num.numerator, true);
                denominator = new NaturalNumStr(num.denominator, true);
            }
            else
            {
                numerator = num.numerator;
                denominator = num.denominator;
            }
        }

        /// <summary>
        /// 改变元素的计算空间
        /// </summary>
        /// <param name="space">新的计算空间，为null即为默认计算空间</param>
        /// <param name="newInstance">是否返回新的实例（如果为null，即为不确定）</param>
        /// <param name="maxDecimalPlaces">最大保留小数位数</param>
        /// <returns></returns>
        public override IOperationSpaceElement ChangeOperationSpace(OperationSpace space = null, bool? newInstance = null, int? maxDecimalPlaces = null)
        {
            if (space == null)
                space = OperationSpace.DefaultSpace;
            var mdp = maxDecimalPlaces.HasValue ? Math.Min(maxDecimalPlaces.Value, space.DefaultMaxDecimalPlaces) : space.DefaultMaxDecimalPlaces;
            if (newInstance == true)
            {
                var new_num = new RationalNum(this, null, true)
                {
                    Space = space,
                    MaxDecimalPlaces = mdp,
                    numerator = (NaturalNumStr)numerator.ChangeOperationSpace(space, newInstance),
                    denominator = (NaturalNumStr)denominator.ChangeOperationSpace(space, newInstance)
                };
                return new_num;
            }
            else
            {
                valueChanged = true;
                Space = space;
                MaxDecimalPlaces = mdp;
                numerator = (NaturalNumStr)numerator.ChangeOperationSpace(space, newInstance);
                denominator = (NaturalNumStr)denominator.ChangeOperationSpace(space, newInstance);
                return this;
            }
        }

        /// <summary>
        /// 获取倒数（新的实例）
        /// </summary>
        /// <param name="deep">是否为深拷贝</param>
        /// <returns></returns>
        public RationalNum GetReciprocal(bool deep = true)
        {
            if (deep)
                return new RationalNum(new NaturalNumStr(denominator), new NaturalNumStr(numerator), PositiveOrNegative, Space, MaxDecimalPlaces);
            else
                return new RationalNum(denominator, numerator, PositiveOrNegative, Space, MaxDecimalPlaces);
        }
        /// <summary>
        /// 获得带分数的形式
        /// </summary>
        /// <param name="integer">整数部分</param>
        /// <param name="numerator">新的分子</param>
        public void GetMixedNumber(out IntegerNum integer, out IntegerNum numerator)
        {
            var ans = NaturalNumStr.Mod(this.numerator, this.denominator);
            integer = ans.Item2;
            numerator = ans.Item1;
        }

        /// <summary>
        /// 约分
        /// </summary>
        protected void FractionReduction()
        {
            var divisor = NaturalNumStr.GreatestCommonDivisor(numerator, denominator);
            if (divisor > 1)
            {
                numerator /= divisor;
                denominator /= divisor;
            }
        }

        /// <summary>
        /// 通分
        /// </summary>
        /// <param name="denominator">通分后的分母</param>
        /// <param name="numerators">通分后的分子列表</param>
        /// <param name="nums">各个分数（不改变原有的值）</param>
        public static void ReductionToACommonDenominator(out IntegerNum denominator, out IntegerNum[] numerators, params RationalNum[] nums)
        {
            denominator = null;
            numerators = new IntegerNum[nums.Length];
            if (nums.Length == 1)
            {
                denominator = new IntegerNum(nums[0].denominator);
                numerators[0] = new IntegerNum(nums[0].numerator);
            }
            else if (nums.Length > 1)
            {
                denominator = NaturalNumStr.LeastCommonMultiple(nums[0].denominator, nums[1].denominator);
                for (var i = 2; i < nums.Length; i++)
                    denominator = NaturalNumStr.LeastCommonMultiple(denominator.numerator, nums[i].denominator);
                for (var i = 0; i < nums.Length; i++)
                {
                    numerators[i] = nums[i].numerator * (denominator / nums[i].denominator);
                }
            }
        }

        public override Digitable Copy()
        {
            return new RationalNum(this);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as RationalNum);
        }

        public bool Equals(RationalNum other)
        {
            return other != null &&
                   base.Equals(other);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override NumStr GetValue()
        {
            if (valueChanged)
            {
                value = Digitable.Divide(numerator, denominator, MaxDecimalPlaces) as NumStr;
                valueChanged = false;
            }
            return value;
        }

        public override string GetNaturplayMathExp()
        {
            var sb = new StringBuilder();
            if (PositiveOrNegative < 0)
                sb.Append('-');
            sb.Append(numerator.GetNaturplayMathExp());
            if (!denominator.IsOne)
            {
                sb.Append(" / ");
                sb.Append(denominator.GetNaturplayMathExp());
            }
            return sb.ToString();
        }

        public override string GetTeXCode()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 从NumStr隐式转换
        /// </summary>
        /// <param name="num"></param>
        public static implicit operator RationalNum(NumStr num)
        {
            return new RationalNum(num);
        }
    }
}
