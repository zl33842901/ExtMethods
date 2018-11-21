using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Linq.Expressions;

namespace System
{
    public static class ExtMethods
    {
        public static string ToStringBy<T>(this IEnumerable<T> s, string split = ",")
        {
            if (s == null || s.Count() < 1)
                return string.Empty;
            System.Text.StringBuilder Stsb = new System.Text.StringBuilder();
            foreach (T a in s)
            {
                Stsb.Append(a);
                Stsb.Append(split);
            }
            return Stsb.ToString().Substring(0, Stsb.Length - split.Length);
        }
        public static string ToStringDot<T>(this IEnumerable<T> s)
        {
            return s.ToStringBy();
        }
        public static TKey Field<T, TKey>(this T t, Expression<Func<T, TKey>> keySelector) where T : class
        {
            if (t == null)
                return default(TKey);
            else
                return keySelector.Compile().Invoke(t);
        }
        public static string ToStringForce(this object s)
        {
            if (s == null)
                return string.Empty;
            else
                return s.ToString();
        }
        public static int ToInt(this string s, int defValue)
        {
            if (string.IsNullOrWhiteSpace(s))
                return defValue;
            var b = int.TryParse(s, out int v);
            if (b)
                return v;
            else
                return defValue;
        }
        public static int ToInt(this string s)
        {
            return s.ToInt(0);
        }
        public static DateTime ToDateTime(this string s, DateTime defValue)
        {
            DateTime TempDateTime;
            if (DateTime.TryParse(s, out TempDateTime))
            {
                return TempDateTime;
            }
            else
            {
                return defValue;
            }
        }
        public static byte ToByte(this int s, byte defValue)
        {
            try
            {
                return (byte)s;
            }
            catch
            {
                return defValue;
            }
        }
        public static string Cut(this string s, int length, bool isDot = false)
        {
            return Common.StringHelper.CutString(s, length, isDot);
        }
        public static bool NullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }
        public static bool NullOrWhiteSpace(this string s)
        {
            return string.IsNullOrWhiteSpace(s);
        }
        public static List<int> ToListIntByDot(this string s, bool Allow0 = false, bool Distinct = false)
        {
            List<int> Li = new List<int>();
            if (s.NullOrEmpty())
                return Li;
            foreach (string a in s.Split(','))
            {
                int i = a.ToInt(-1);
                if (Allow0)
                {
                    if (i > -1)
                        Li.Add(i);
                }
                else
                {
                    if (i > 0)
                        Li.Add(i);
                }
            }
            if (Distinct)
                Li = Li.Distinct().ToList();
            return Li;
        }
        public static decimal ToDecimal(this string s)
        {
            try
            {
                return Convert.ToDecimal(s);
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static string ToStringNoError(this object s)
        {
            if (s == null)
                return string.Empty;
            else
                return s.ToString();
        }
    }
}

namespace Common
{
    public static class ExtMethods
    {
        public static int ConvertIpToInt(string IP)
        {
            byte[] IPArr = IPAddress.Parse(IP).GetAddressBytes();
            int intAddress = BitConverter.ToInt32(IPArr, 0);
            return intAddress;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="dt">要比较的时间 如（DateTime.New）</param>
        /// <returns></returns>
        public static string CpTimeAgoOrLater(this DateTime s, DateTime dt)
        {
            TimeSpan Ts = dt - s;
            string Fb = dt > s ? "前" : "后";
            if (Math.Abs(Ts.TotalDays / 365) > 1)
            {
                return Convert.ToInt32(Math.Abs(Ts.TotalDays / 365 - 0.5)).ToString() + "年" + Fb;
            }
            if (Math.Abs(Ts.TotalDays / 30) > 1)
            {
                return Convert.ToInt32(Math.Abs(Ts.TotalDays / 30 - 0.5)).ToString() + "个月" + Fb;
            }
            if (Math.Abs(Ts.TotalDays / 7) > 1)
            {
                return Convert.ToInt32(Math.Abs(Ts.TotalDays / 7 - 0.5)).ToString() + "周" + Fb;
            }
            if (Math.Abs(Ts.TotalDays) > 1)
            {
                return Convert.ToInt32(Math.Abs(Ts.TotalDays - 0.5)).ToString() + "天" + Fb;
            }
            if (Math.Abs(Ts.TotalHours) > 1)
            {
                return Convert.ToInt32(Math.Abs(Ts.TotalHours - 0.5)).ToString() + "小时" + Fb;
            }
            if (Math.Abs(Ts.TotalMinutes) > 1)
            {
                return Convert.ToInt32(Math.Abs(Ts.TotalMinutes - 0.5)).ToString() + "分钟" + Fb;
            }
            if (Math.Abs(Convert.ToInt32(Ts.TotalSeconds)) > 20)
            {
                return "半分钟" + Fb;
            }
            return "刚刚";
        }

    }
}
