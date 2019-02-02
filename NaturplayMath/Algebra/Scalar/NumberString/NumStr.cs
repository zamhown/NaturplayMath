using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NaturplayMath.Algebra.Exception;

namespace NaturplayMath.Algebra.Scalar.NumberString
{
    /// <summary>
    /// 数字串类，表示一个数字
    /// </summary>
    public partial class NumStr : Digitable, IEnumerable<uint>, IEnumerable, IEquatable<NumStr>
    {
        /// <summary>
        /// 整数部分，第n个元素是第n位
        /// </summary>
        protected LinkedList<uint> integerNumList;
        /// <summary>
        /// 小数部分，第n个元素是小数第n位
        /// </summary>
        protected LinkedList<uint> decimalNumList;

        /// <summary>
        /// 整数位数
        /// </summary>
        public int IntegerPlaces
        {
            get
            {
                return integerNumList == null ? 0 : integerNumList.Count;
            }
        }
        /// <summary>
        /// 小数位数
        /// </summary>
        public int DecimalPlaces
        {
            get
            {
                return decimalNumList == null ? 0 : decimalNumList.Count;
            }
        }

        public override bool IsOne
        {
            get
            {
                return decimalNumList == null && PositiveOrNegative > 0 && integerNumList.Count == 1 && integerNumList.First.Value == 1;
            }
        }

        public override bool IsInteger
        {
            get
            {
                return decimalNumList == null;
            }
        }

        public override bool IsEven
        {
            get
            {
                return decimalNumList == null && (PositiveOrNegative == 0 || integerNumList.First.Value % 2 == 0);
            }
        }

        public override bool IsOdd
        {
            get
            {
                return decimalNumList == null && PositiveOrNegative != 0 && integerNumList.First.Value % 2 == 1;
            }
        }

        /// <summary>
        /// 用于输出的数字字符串
        /// </summary>
        protected const string numbers = "0123456789ABCDEFGHJKLMNPQRTUVWXY";

        /// <summary>
        /// 默认构造函数，初始化为0
        /// </summary>
        public NumStr() { }
        /// <summary>
        /// 用整数初始化
        /// </summary>
        /// <param name="num">整数</param>
        /// <param name="space">计算空间</param>
        /// <param name="maxDecimalPlaces">最大保留小数位数</param>
        public NumStr(Int64 num, OperationSpace space = null, int? maxDecimalPlaces = null)
        {
            if (space != null)
                this.Space = space;
            this.MaxDecimalPlaces = maxDecimalPlaces.HasValue ? Math.Min(maxDecimalPlaces.Value, this.Space.DefaultMaxDecimalPlaces) : this.Space.DefaultMaxDecimalPlaces;
            ChangeNumber(num);
        }
        /// <summary>
        /// 用整数初始化
        /// </summary>
        /// <param name="num">整数</param>
        /// <param name="space">计算空间</param>
        /// <param name="maxDecimalPlaces">最大保留小数位数</param>
        public NumStr(UInt64 num, OperationSpace space = null, int? maxDecimalPlaces = null)
        {
            if (space != null)
                this.Space = space;
            this.MaxDecimalPlaces = maxDecimalPlaces.HasValue ? Math.Min(maxDecimalPlaces.Value, this.Space.DefaultMaxDecimalPlaces) : this.Space.DefaultMaxDecimalPlaces;
            ChangeNumber(num);
        }
        /// <summary>
        /// 用浮点数初始化，确定小数位数和进制
        /// </summary>
        /// <param name="num">浮点数</param>
        /// <param name="space">计算空间</param>
        /// <param name="maxDecimalPlaces">最大保留小数位数</param>
        public NumStr(double num, OperationSpace space = null, int? maxDecimalPlaces = null)
        {
            if (space != null)
                this.Space = space;
            this.MaxDecimalPlaces = maxDecimalPlaces.HasValue ? Math.Min(maxDecimalPlaces.Value, this.Space.DefaultMaxDecimalPlaces) : this.Space.DefaultMaxDecimalPlaces;
            ChangeNumber(num);
        }
        /// <summary>
        /// 用字符串表示的数字初始化
        /// </summary>
        /// <param name="num">数字</param>
        /// <param name="space">计算空间</param>
        /// <param name="maxDecimalPlaces">最大保留小数位数</param>
        public NumStr(string num, OperationSpace space = null, int? maxDecimalPlaces = null)
        {
            if (space != null)
                this.Space = space;
            this.MaxDecimalPlaces = maxDecimalPlaces.HasValue ? Math.Min(maxDecimalPlaces.Value, this.Space.DefaultMaxDecimalPlaces) : this.Space.DefaultMaxDecimalPlaces;
            ChangeNumber(num);
        }
        /// <summary>
        /// 用相同类型的数字初始化，转换为指定的计算空间
        /// </summary>
        /// <param name="num">原始对象</param>
        /// <param name="space">计算空间</param>
        /// <param name="maxDecimalPlaces">最大保留小数位数</param>
        /// <param name="positiveOrNegative">正负性</param>
        public NumStr(NumStr num, OperationSpace space, int? maxDecimalPlaces = null, int? positiveOrNegative = null)
        {
            Space = space ?? num.Space;
            PositiveOrNegative = positiveOrNegative ?? num.PositiveOrNegative;
            MaxDecimalPlaces = maxDecimalPlaces.HasValue ?
                Math.Min(maxDecimalPlaces.Value, this.Space.DefaultMaxDecimalPlaces)
                : this.Space.DefaultMaxDecimalPlaces;
            NumberBaseConversion(this, Space.NumberBase, num);
        }
        /// <summary>
        /// 拷贝初始化
        /// </summary>
        /// <param name="num">原始对象</param>
        /// <param name="maxDecimalPlaces">最大保留小数位数</param>
        /// <param name="deep">是否为深拷贝</param>
        public NumStr(NumStr num, int? maxDecimalPlaces = null, bool deep = true)
            : base(num, maxDecimalPlaces)
        {
            Copy(num, maxDecimalPlaces, deep);
        }

        /// <summary>
        /// 输出将该数去掉小数点以后的数
        /// </summary>
        /// <returns></returns>
        public NaturalNumStr RemoveDecimalPoint()
        {
            var n = new NaturalNumStr(0, Space);
            if (IsZero)
                return n;
            else
            {
                n.integerNumList = new LinkedList<uint>();
                n.PositiveOrNegative = 1;
            }

            if (decimalNumList != null)
            {
                if (integerNumList == null)
                {
                    var i = decimalNumList.GetEnumerator();
                    //跳过高位0
                    do
                        i.MoveNext();
                    while (i.Current == 0);
                    do
                        n.integerNumList.AddFirst(i.Current);
                    while (i.MoveNext());
                    return n;
                }
                else
                {
                    foreach (var i in decimalNumList)
                        n.integerNumList.AddFirst(i);
                }
            }
            if (integerNumList != null)
            {
                foreach (var i in integerNumList)
                    n.integerNumList.AddLast(i);
            }
            return n;
        }
        /// <summary>
        /// 输出为Int64类型整数
        /// </summary>
        /// <returns></returns>
        public Int64 ToInt64()
        {
            if (IsZero || integerNumList == null)
                return 0;
            var ni = integerNumList.First;
            Int64 num = ni.Value;
            uint? nb = null;
            for (var i = 1; i < IntegerPlaces; i++)
            {
                ni = ni.Next;
                if (!nb.HasValue)
                    nb = Space.NumberBase;
                else
                    nb *= Space.NumberBase;
                num += ni.Value * nb.Value;
            }
            if (IsNegative)
                num = -num;
            return num;
        }
        /// <summary>
        /// 从小数点处分割，分别生成整数与小数部分
        /// </summary>
        /// <param name="deepCopy">分割时是否深拷贝</param>
        /// <returns>包含整数与小数的元组</returns>
        public (NumStr, NumStr) SplitByDecimalPoint(bool deepCopy = false)
        {
            var integerPart = new NumStr(this, 0, deepCopy);
            var decimalPart = new NumStr(0, Space, MaxDecimalPlaces);
            if (decimalNumList != null)
            {
                decimalPart.PositiveOrNegative = PositiveOrNegative;
                if (deepCopy)
                {
                    decimalPart.decimalNumList = new LinkedList<uint>();
                    foreach (var i in decimalNumList)
                        decimalPart.decimalNumList.AddLast(i);
                }
                else
                    decimalPart.decimalNumList = decimalNumList;
            }
            return (integerPart, decimalPart);
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
            return ChangeOS(space, newInstance, maxDecimalPlaces);
        }

        public bool Equals(NumStr other)
        {
            return other != null && this == other && MaxDecimalPlaces == other.MaxDecimalPlaces;
        }
        /// <summary>
        /// 输出字符串，最高支持32进制的直接输出
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (NumberBase > 32)
                throw new FormatErrorException();
            StringBuilder sb = new StringBuilder();
            if (PositiveOrNegative < 0)
                sb.Append('-');
            if (integerNumList != null)
            {
                var ii = integerNumList.Last;
                while (ii != null)
                {
                    sb.Append(numbers[(int)ii.Value]);
                    ii = ii.Previous;
                }
            }
            else
                sb.Append(0);
            if (decimalNumList != null)
            {
                sb.Append('.');
                foreach (var di in decimalNumList)
                    sb.Append(numbers[(int)di]);
            }
            return sb.ToString();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as NumStr);
        }

        public override int GetHashCode()
        {
            return (int)ToInt64();
        }

        public override Digitable Copy()
        {
            return new NumStr(this);
        }

        public override NumStr GetNumberString()
        {
            return this;
        }

        public override string GetNaturplayMathExp()
        {
            return ToString();
        }

        public override string GetTeXCode()
        {
            return ToString();
        }

        /// <summary>
        /// 获取枚举器
        /// </summary>
        /// <returns></returns>
        public IEnumerator<uint> GetEnumerator()
        {
            return new Enumerator(this);
        }
        /// <summary>
        /// 获取反向枚举器
        /// </summary>
        /// <returns></returns>
        public IEnumerator<uint> GetInverseEnumerator()
        {
            return new InverseEnumerator(this);
        }
        /// <summary>
        /// 获取枚举器
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(this);
        }

        /// <summary>
        /// 更改数字内容
        /// </summary>
        /// <param name="num"></param>
        protected void ChangeNumber(Int64 num)
        {
            if (num > 0)
                PositiveOrNegative = 1;
            else if (num < 0)
            {
                PositiveOrNegative = -1;
                num = -num;
            }
            else
            {
                PositiveOrNegative = 0;
                integerNumList = null;
                decimalNumList = null;
                return;
            }

            //录入数字
            integerNumList = new LinkedList<uint>();
            decimalNumList = null;
            while (num > 0)
            {
                integerNumList.AddLast((uint)(num % this.NumberBase));
                if (IntegerPlaces > DefaultSettings.MaxIntegerPlaces)
                    throw new NumberOutOfRangeException();
                num /= this.NumberBase;
            }
        }
        /// <summary>
        /// 更改数字内容
        /// </summary>
        /// <param name="num"></param>
        protected void ChangeNumber(UInt64 num)
        {
            if (num > 0)
                PositiveOrNegative = 1;
            else
            {
                PositiveOrNegative = 0;
                integerNumList = null;
                decimalNumList = null;
                return;
            }

            //录入数字
            integerNumList = new LinkedList<uint>();
            decimalNumList = null;
            while (num > 0)
            {
                integerNumList.AddLast((uint)(num % this.NumberBase));
                if (IntegerPlaces > DefaultSettings.MaxIntegerPlaces)
                    throw new NumberOutOfRangeException();
                num /= this.NumberBase;
            }
        }
        /// <summary>
        /// 更改数字内容
        /// </summary>
        /// <param name="num"></param>
        protected void ChangeNumber(double num)
        {
            if (num > 0)
                PositiveOrNegative = 1;
            else if (num < 0)
            {
                PositiveOrNegative = -1;
                num = -num;
            }
            else
            {
                PositiveOrNegative = 0;
                integerNumList = null;
                decimalNumList = null;
                return;
            }

            //录入整数
            var integer_num = (Int64)num;
            if (integer_num > 0)
            {
                integerNumList = new LinkedList<uint>();
                while (integer_num > 0)
                {
                    integerNumList.AddLast((uint)(integer_num % this.NumberBase));
                    if (IntegerPlaces > DefaultSettings.MaxIntegerPlaces)
                        throw new NumberOutOfRangeException();
                    integer_num /= this.NumberBase;
                }
            }
            else
                integerNumList = null;

            //录入小数
            var decimal_num = num % 1;
            if (decimal_num > 0)
            {
                decimalNumList = new LinkedList<uint>();
                uint i = 0;
                do
                {
                    decimal_num *= this.NumberBase;
                    decimalNumList.AddLast((uint)decimal_num);
                    decimal_num %= 1;
                    i++;
                }
                while (decimal_num > 0 && i < this.MaxDecimalPlaces);
            }
            else
                decimalNumList = null;
        }
        /// <summary>
        /// 更改数字内容
        /// </summary>
        /// <param name="num"></param>
        protected void ChangeNumber(string num)
        {
            num = num.Trim().ToUpper();
            int i = 0;
            if (num[0] == '-')
            {
                PositiveOrNegative = -1;
                i = 1;
            }
            else if (num == "0")
            {
                PositiveOrNegative = 0;
                integerNumList = null;
                decimalNumList = null;
                return;
            }
            else
                PositiveOrNegative = 1;

            var ss = num.Split(char.Parse("."));
            //录入整数
            if (ss[0].Length > DefaultSettings.MaxIntegerPlaces)
                throw new NumberOutOfRangeException();
            if (ss[0] != "0")
            {
                integerNumList = new LinkedList<uint>();
                if (this.NumberBase <= 10)
                    for (; i < ss[0].Length; i++)
                    {
                        var ele = ss[0][i] - '0';
                        if (ele >= this.NumberBase || ele < 0)
                            throw new FormatErrorException();
                        integerNumList.AddFirst((uint)ele);
                    }
                else
                    for (; i < ss[0].Length; i++)
                    {
                        var ele = numbers.IndexOf(ss[0][i]);
                        if (ele >= this.NumberBase || ele < 0)
                            throw new FormatErrorException();
                        integerNumList.AddFirst((uint)ele);
                    }
            }
            else
                integerNumList = null;

            //录入小数
            if (ss.Length > 1 && ss[1].Length > 0)
            {
                decimalNumList = new LinkedList<uint>();
                if (this.NumberBase <= 10)
                    for (i = 0; i < ss[1].Length; i++)
                    {
                        var ele = ss[1][i] - '0';
                        if (ele >= this.NumberBase || ele < 0)
                            throw new FormatErrorException();
                        decimalNumList.AddLast((uint)ele);
                        if (DecimalPlaces == this.MaxDecimalPlaces)
                            break;
                    }
                else
                    for (i = 0; i < ss[1].Length; i++)
                    {
                        var ele = numbers.IndexOf(ss[1][i]);
                        if (ele >= this.NumberBase || ele < 0)
                            throw new FormatErrorException();
                        decimalNumList.AddLast((uint)ele);
                        if (DecimalPlaces == this.MaxDecimalPlaces)
                            break;
                    }
            }
            else
                decimalNumList = null;
        }
        /// <summary>
        /// 拷贝
        /// </summary>
        /// <param name="num">原始对象</param>
        /// <param name="maxDecimalPlaces">最大保留小数位数</param>
        /// <param name="deep">是否为深拷贝</param>
        protected void Copy(NumStr num, int? maxDecimalPlaces = null, bool deep = true)
        {
            PositiveOrNegative = num.PositiveOrNegative;
            Space = num.Space;
            MaxDecimalPlaces = maxDecimalPlaces.HasValue ? Math.Min(maxDecimalPlaces.Value, this.Space.DefaultMaxDecimalPlaces) : num.MaxDecimalPlaces;
            integerNumList = null;
            decimalNumList = null;
            if (deep)
            {
                if (num.integerNumList != null)
                {
                    integerNumList = new LinkedList<uint>();
                    foreach (var i in num.integerNumList)
                        integerNumList.AddLast(i);
                }
                if (MaxDecimalPlaces > 0 && num.decimalNumList != null)
                {
                    decimalNumList = new LinkedList<uint>();
                    var i = num.decimalNumList.First;
                    while (i != null && DecimalPlaces < MaxDecimalPlaces)
                    {
                        decimalNumList.AddLast(i.Value);
                        i = i.Next;
                    }
                }
            }
            else
            {
                integerNumList = num.integerNumList;
                if (MaxDecimalPlaces < num.DecimalPlaces)
                {
                    decimalNumList = new LinkedList<uint>();
                    var i = num.decimalNumList.First;
                    while (i != null && DecimalPlaces < MaxDecimalPlaces)
                    {
                        decimalNumList.AddLast(i.Value);
                        i = i.Next;
                    }
                }
                else
                    decimalNumList = num.decimalNumList;
            }
        }
        /// <summary>
        /// 改变元素的计算空间
        /// </summary>
        /// <param name="space">新的计算空间，为null即为默认计算空间</param>
        /// <param name="newInstance">是否返回新的实例（如果为null，即为不确定）</param>
        /// <param name="maxDecimalPlaces">最大保留小数位数</param>
        /// <returns></returns>
        protected NumStr ChangeOS(OperationSpace space, bool? newInstance, int? maxDecimalPlaces)
        {
            if (space == null)
                space = OperationSpace.DefaultSpace;
            if (this.Space == space && (maxDecimalPlaces == null || maxDecimalPlaces == space.DefaultMaxDecimalPlaces))
            {
                if (newInstance == true)
                    return new NumStr(this);
                else
                    return this;
            }
            NumStr new_num = new NumStr(0, space, maxDecimalPlaces);
            if (IsZero) return new_num;
            new_num.PositiveOrNegative = PositiveOrNegative;
            NumberBaseConversion(new_num, space.NumberBase, this);
            if (newInstance == false)
            {
                Copy(new_num, null, false);
                return this;
            }
            else
                return new_num;
        }


        /// <summary>
        /// 从Int64隐式转换
        /// </summary>
        /// <param name="num"></param>
        public static implicit operator NumStr(Int64 num)
        {
            return new NumStr(num);
        }
        /// <summary>
        /// 从double隐式转换
        /// </summary>
        /// <param name="num"></param>
        public static implicit operator NumStr(double num)
        {
            return new NumStr(num);
        }
        /// <summary>
        /// 显式转换至Int64
        /// </summary>
        /// <param name="num"></param>
        public static explicit operator Int64(NumStr num)
        {
            return num.ToInt64();
        }

        /// <summary>
        /// 本类内含的枚举器（从低位到高位）
        /// </summary>
        public struct Enumerator : IEnumerator<uint>, IEnumerator, IDisposable
        {
            /// <summary>
            /// 当前集合
            /// </summary>
            private NumStr list;
            /// <summary>
            /// 当前元素
            /// </summary>
            private uint current;
            /// <summary>
            /// 当前结点
            /// </summary>
            private LinkedListNode<uint> node;

            /// <summary>
            /// 获取枚举数当前位置的元素
            /// </summary>
            public uint Current { get { return current; } }

            object IEnumerator.Current { get { return current; } }

            public Enumerator(NumStr num)
            {
                list = num;
                current = 0;
                if (list.decimalNumList != null)
                    node = list.decimalNumList.Last;
                else if (list.integerNumList != null)
                    node = list.integerNumList.First;
                else
                    node = null;
            }

            public void Dispose() { }
            /// <summary>
            /// 使枚举数前进到下一个元素
            /// </summary>
            /// <returns>如果枚举数成功地推进到下一个元素，则为 true；如果枚举数越过集合的结尾，则为 false</returns>
            public bool MoveNext()
            {
                if (node == null)
                    return false;
                current = node.Value;
                if (node.List == list.decimalNumList)
                {
                    node = node.Previous;
                    if (node == null)
                        node = list.integerNumList?.First;
                }
                else
                    node = node.Next;
                return true;
            }
            /// <summary>
            /// 将游标重置到第一个成员前面
            /// </summary>
            public void Reset()
            {
                current = 0;
                if (list.decimalNumList != null)
                    node = list.decimalNumList.Last;
                else if (list.integerNumList != null)
                    node = list.integerNumList.First;
                else
                    node = null;
            }
        }

        /// <summary>
        /// 本类内含的反向枚举器（从高位到低位）
        /// </summary>
        public struct InverseEnumerator : IEnumerator<uint>, IEnumerator, IDisposable
        {
            /// <summary>
            /// 当前集合
            /// </summary>
            private NumStr list;
            /// <summary>
            /// 当前元素
            /// </summary>
            private uint current;
            /// <summary>
            /// 当前结点
            /// </summary>
            private LinkedListNode<uint> node;

            /// <summary>
            /// 获取枚举数当前位置的元素
            /// </summary>
            public uint Current { get { return current; } }

            object IEnumerator.Current { get { return current; } }

            public InverseEnumerator(NumStr num)
            {
                list = num;
                current = 0;
                if (list.integerNumList != null)
                    node = list.integerNumList.Last;
                else if (list.decimalNumList != null)
                    node = list.decimalNumList.First;
                else
                    node = null;
            }

            public void Dispose() { }
            /// <summary>
            /// 使枚举数前进到下一个元素
            /// </summary>
            /// <returns>如果枚举数成功地推进到下一个元素，则为 true；如果枚举数越过集合的结尾，则为 false</returns>
            public bool MoveNext()
            {
                if (node == null)
                    return false;
                current = node.Value;
                if (node.List == list.integerNumList)
                {
                    node = node.Previous;
                    if (node == null)
                        node = list.decimalNumList?.First;
                }
                else
                    node = node.Next;
                return true;
            }
            /// <summary>
            /// 将游标重置到第一个成员前面
            /// </summary>
            public void Reset()
            {
                current = 0;
                if (list.integerNumList != null)
                    node = list.integerNumList.Last;
                else if (list.decimalNumList != null)
                    node = list.decimalNumList.First;
                else
                    node = null;
            }
        }
    }
}
