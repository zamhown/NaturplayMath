using NaturplayMath.Algebra.Scalar.NumberString;
using System;
using System.Collections.Generic;
using System.Text;

namespace NaturplayMath.Algebra.Scalar
{
    /// <summary>
    /// 常量生成器
    /// </summary>
    public static class ConstantFactory
    {
        /// <summary>
        /// 0-20被1除的循环节，"/"之前为固定数位，之后为循环节
        /// </summary>
        static string[] repetend = {
            "",
            "/",
            "5/",
            "/3",
            "25/",
            "2/",
            "1/6",
            "/142857",
            "125/",
            "/1",
            "1/",
            "/09",
            "08/3",
            "/076923",
            "0/714285",
            "0/6",
            "0625/",
            "/0588235294117647",
            "0/5",
            "/052631578947368421",
            "05/",
        };
        /// <summary>
        /// 已经运算过的常量词典
        /// </summary>
        static readonly Dictionary<string, (NumStr, int)> numDic = new Dictionary<string, (NumStr, int)>();

        /// <summary>
        /// 在已储存的常量中查找，找不到则返回null
        /// </summary>
        /// <param name="numName"></param>
        /// <param name="space"></param>
        /// <param name="maxDecimalPlaces"></param>
        /// <returns></returns>
        static NumStr searchNumDic(string numName, OperationSpace space, int maxDecimalPlaces)
        {
            var key = $"{numName},{space.NumberBase}";
            if (numDic.ContainsKey(key)
                && (numDic[key].Item1.DecimalPlaces < numDic[key].Item2 || numDic[key].Item2 >= maxDecimalPlaces))
                return numDic[key].Item1;
            else
                return null;
        }
        /// <summary>
        /// 更新常量词典
        /// </summary>
        /// <param name="numName"></param>
        /// <param name="space"></param>
        /// <param name="maxDecimalPlaces"></param>
        /// <param name="value"></param>
        static void updateNumDic(string numName, OperationSpace space, int maxDecimalPlaces, NumStr value)
        {
            if (searchNumDic(numName, space, maxDecimalPlaces) == null)
            {
                var key = $"{numName},{space.NumberBase}";
                if (numDic.ContainsKey(key))
                    numDic[key] = (value, maxDecimalPlaces);
                else
                    numDic.Add(key, (value, maxDecimalPlaces));
            }
        }

        /// <summary>
        /// 获取分子为1的分数
        /// </summary>
        /// <param name="denominator">分母</param>
        /// <param name="space"></param>
        /// <param name="maxDecimalPlaces"></param>
        /// <returns></returns>
        public static NumStr FractionWithNumeratorBeingOne(int denominator, OperationSpace space, int maxDecimalPlaces)
        {
            if (denominator == 0) return null;
            int d;
            string pn = "";
            if (denominator < 0)
            {
                d = -denominator;
                pn = "-";
            }
            else
                d = denominator;
            var ans = searchNumDic($"1/{d}", space, maxDecimalPlaces);
            if (ans == null)
            {
                if (repetend.Length > d)
                {
                    if (d == 1)
                        ans = new NumStr($"{pn}1", space, maxDecimalPlaces);
                    else
                    {
                        var s = repetend[d].Split('/');
                        var digits = new StringBuilder(s[0]);
                        while (digits.Length < maxDecimalPlaces)
                            digits.Append(repetend[d]);
                        ans = new NumStr($"{pn}0.{digits}", space, maxDecimalPlaces);
                    }
                }
                else
                    ans = new NumStr(1, space, maxDecimalPlaces) / new NumStr(denominator, space, maxDecimalPlaces);
                if (ans.PositiveOrNegative >= 0)
                    updateNumDic($"1/{d}", space, maxDecimalPlaces, ans);
                else
                    updateNumDic($"1/{d}", space, maxDecimalPlaces, new NumStr(ans, space, maxDecimalPlaces, 1));
                return ans;
            }
            else
            {
                if (pn == "" && space == ans.Space && maxDecimalPlaces == ans.MaxDecimalPlaces)
                    return ans;
                else
                    return new NumStr(ans, space, maxDecimalPlaces, pn == "" ? 1 : -1);
            }
        }
    }
}
