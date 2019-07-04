using System;
using System.Collections.Generic;
using System.ComponentModel; 

namespace CommonTools
{
    /// <summary>
    /// 扩展方法
    /// </summary>
    public static class ExtendMethod
    {
 
        /// <summary>
        /// 转换为string数组
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string[] ToStrArr(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                str = "";
            }
            return str.Split(',');
        }
        public static int[] ToIntArr(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                str = "";
            }
            return Array.ConvertAll<string, int>(str.ToStrArr(), s => int.Parse(s)); 
        }
        /// <summary>
        /// 3、实现一个具体的静态方法
        /// </summary>
        /// <param name="str">4、第一个参数必须使用this关键字指定要使用扩展方法的类型</param>
        /// <returns></returns>
        public static int ToInt(this string str)
        {
            return int.Parse(str);
        }
        public static decimal ToDecimal(this string str)
        {
            return decimal.Parse(str);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        //public static decimal ToDecimal(this string str)
        //{
        //    return decimal.Parse(str);
        //}
        //public static string ToStr(this int str)
        //{
        //    return str.ToString();
        //}
        /// <summary>
        /// 获取指定枚举成员的描述
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToDescription(this Enum obj)
        {
            var attribs = (DescriptionAttribute[])obj.GetType().GetField(obj.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attribs.Length > 0 ? attribs[0].Description : obj.ToString();
        }
        /// <summary>
        /// 枚举获取(int)值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int EnumToInt(this Enum obj)
        {
            return Convert.ToInt32(obj);
        }
        /// <summary>
        /// 枚举获取(string)值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string EnumToString(this Enum obj)
        {
            return Convert.ToString(obj);
        }

        /// <summary>
        /// 总和
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static decimal ToSum(this string str)
        {
            decimal Sum = 0;
            foreach (string i in str.Split(','))
            {
                Sum += i.ToDecimal();
            }
            return Sum;
        }
        /// <summary>
        /// 平均
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static decimal ToAverage(this string str)
        {
            decimal Sum = 0;
            foreach (string i in str.Split(','))
            {
                Sum += i.ToDecimal();
            }
            return Sum / str.Split(',').Length;
        }
        /// <summary>
        /// 数量
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int ToCount(this string str)
        {
            return str.Split(',').Length;
        }
        /// <summary>
        /// 获取最大值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static decimal GetMax(this object[] obj)
        {
            if (obj.Length == 1) { return obj.ToString().ToDecimal(); }
            Array.Sort(obj);
            return obj[0].ToString().ToDecimal();
        }
        /// <summary>
        /// 获取最小值
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static decimal GetMin(this object[] obj)
        {
            if (obj.Length == 1) { return obj.ToString().ToDecimal(); }
            Array.Sort(obj);
            return obj[obj.Length - 1].ToString().ToDecimal();
        }
        /// <summary>
        /// 获取 最大值、最小值、总和、平均值、数量、倒序数组、正序数组、出现次数最多的、出现次数最少的、奇数、偶数
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static GetNum GetGetNum(this object[] obj)
        {
            GetNum m = new GetNum();

            m.Max = obj[0].ToString().ToDecimal();//Max
            m.Min = obj[0].ToString().ToDecimal();//MIn
            m.Sum = 0;
            var dict = new Dictionary<decimal, int>();
            for (int i = 0; i < obj.Length; i++)
            {
                if (obj[0].ToString().ToDecimal() < obj[i].ToString().ToDecimal())
                {
                    m.Max = obj[i].ToString().ToDecimal();
                }
                if (obj[0].ToString().ToDecimal() > obj[i].ToString().ToDecimal())
                {
                    m.Min = obj[i].ToString().ToDecimal();
                }
                m.Sum += obj[i].ToString().ToDecimal();
                if (obj[0].ToString().ToDecimal() % 2 == 0)
                {
                    m.Even += m.Even;
                }
                else
                {
                    m.Odd += m.Odd;
                }

                // 
                if (!dict.ContainsKey(obj[i].ToString().ToDecimal()))
                    dict.Add(obj[i].ToString().ToDecimal(), 0);
                dict[obj[i].ToString().ToDecimal()]++;

            }
            m.Avg = m.Sum / obj.Length;
            m.Count = obj.Length;
            Array.Sort(obj);
            m.Sort = obj;
            Array.Reverse(obj);
            m.Reverse = obj;

            return m;
        }
    }

    #region 相关类
    /// <summary>
    /// 最大值、最小值、总和、平均值、数量、倒序数组、正序数组、出现次数最多的、出现次数最少的、奇数、偶数
    /// </summary>
    public class GetNum
    {
        public decimal Max { get; set; }
        public decimal Min { get; set; }
        public decimal Sum { get; set; }
        public decimal Avg { get; set; }
        public decimal Count { get; set; }
        public Array Sort { get; set; }
        public Array Reverse { get; set; }
        public decimal Most { get; set; }
        public decimal Least { get; set; }
        public int Odd { get; set; }
        public int Even { get; set; }
    }
    #endregion
}
