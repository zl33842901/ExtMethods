using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Linq.Expressions;
using System.Reflection;
using System.ComponentModel;
using System.Collections.Specialized;

namespace System
{
    /// <summary>
    /// 扩展方法
    /// </summary>
    public static class ExtMethods
    {
        /// <summary>
        /// Determines whether a sequence contains any elements.
        /// </summary>
        /// <typeparam name="T">The type of the elements of source.</typeparam>
        /// <param name="s">The System.Collections.Generic.IEnumerable`1 to check for emptiness.</param>
        /// <returns>true if the source sequence contains any elements; otherwise, false.</returns>
        public static bool AnyX<T>(this IEnumerable<T> s)
        {
            if (s == null)
                return false;
            else
                return s.Any();
        }
        /// <summary>
        /// 拼成逗号分隔的字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="s">源列表</param>
        /// <param name="split">分隔符，默认为英文逗号</param>
        /// <returns></returns>
        public static string ToStringBy<T>(this IEnumerable<T> s, string split = ",")
        {
            if (s == null || s.Count() < 1)
                return string.Empty;
            return string.Join(split, s);
        }
        /// <summary>
        /// ToStringBy 方法的友好名重载
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToStringDot<T>(this IEnumerable<T> s)
        {
            return s.ToStringBy();
        }
        /// <summary>
        /// 获取某个实体的某个字段（为了防止直接 .引用报错）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="t">实体</param>
        /// <param name="keySelector">投影选择器</param>
        /// <returns></returns>
        public static TKey Field<T, TKey>(this T t, Expression<Func<T, TKey>> keySelector) where T : class
        {
            if (t == null)
                return default(TKey);
            else
                return keySelector.Compile().Invoke(t);
        }
        /// <summary>
        /// 转换成字符串（防止 .ToString 报错）
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToStringForce(this object s)
        {
            if (s == null)
                return string.Empty;
            else
                return s.ToString();
        }
        /// <summary>
        /// 字符串转数字（防止 Convert.ToInt32 报错）
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defValue">转换失败时的默认值</param>
        /// <returns></returns>
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
        /// <summary>
        /// 字符串转数字 失败时返回0
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int ToInt(this string s)
        {
            return s.ToInt(0);
        }
        /// <summary>
        /// 限制数字值大小
        /// </summary>
        /// <param name="i">原数字</param>
        /// <param name="min">最小值（可包含）</param>
        /// <param name="max">最大值（可包含）</param>
        /// <param name="defaultVal">默认值（超出范围时返回此值）</param>
        /// <returns></returns>
        public static int Limit(this int i, int min,int max,int defaultVal)
        {
            if (i < min)
                return defaultVal;
            else if (i > max)
                return defaultVal;
            else
                return i;
        }
        /// <summary>
        /// 字符串转换为日期
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defValue">转换失败时的默认值</param>
        /// <returns></returns>
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
        /// <summary>
        /// 字符串转换为日期
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static DateTime? ToDateTime(this string s)
        {
            bool b = DateTime.TryParse(s, out var dt);
            if (b)
                return dt;
            else
                return null;
        }
        /// <summary>
        /// 时间转时间戳
        /// </summary>
        /// <param name="time">要转换的时间</param>
        /// <param name="thirteen">是否需要13位结果（否则为10位）</param>
        /// <returns></returns>
        public static long ToTimeStamp(DateTime time, bool thirteen = false)
        {
            var t = TimeZoneInfo.ConvertTime(new System.DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc), TimeZoneInfo.Local);
            var sp = time - t;
            if(thirteen)
                return (long)sp.TotalMilliseconds;
            else
                return (long)sp.TotalSeconds;
        }
        /// <summary>
        /// 时间戳转时间
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime ToTime(this string timeStamp)
        {
            if (timeStamp.NullOrEmpty() || (timeStamp.Length != 10 && timeStamp.Length != 13))
                throw new Exception("format error");
            long lt = long.Parse(timeStamp.Length == 10 ? (timeStamp + "0000000") : (timeStamp + "0000"));
            var t = TimeZoneInfo.ConvertTime(new System.DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc), TimeZoneInfo.Local);
            TimeSpan toNow = new TimeSpan(lt);
            return t.Add(toNow);
        }
        /// <summary>
        /// 数字转 byte
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defValue">转换失败时的默认值</param>
        /// <returns></returns>
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
        /// <summary>
        /// 按字节数截取字符串
        /// </summary>
        /// <param name="s"></param>
        /// <param name="length">字节数</param>
        /// <param name="isDot">结尾是否加省略号</param>
        /// <returns></returns>
        public static string Cut(this string s, int length, bool isDot = false)
        {
            return Common.StringHelper.CutString(s, length, isDot);
        }
        /// <summary>
        /// string.IsNullOrEmpty 的友好形式
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool NullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }
        /// <summary>
        /// string.IsNullOrWhiteSpace 的友好形式
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool NullOrWhiteSpace(this string s)
        {
            return string.IsNullOrWhiteSpace(s);
        }
        /// <summary>
        /// 数字逗号分隔的字符串，转为数字列表
        /// </summary>
        /// <param name="s">1,2,3,4 这样的字符串</param>
        /// <param name="Allow0">是否允许0</param>
        /// <param name="Distinct">是否去重</param>
        /// <returns></returns>
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
        /// <summary>
        /// 字符串转换为 decimal 失败时返回0
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
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
        /// <summary>
        /// object 转为字符串 避免 .ToString 报错
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToStringNoError(this object s)
        {
            if (s == null)
                return string.Empty;
            else
                return s.ToString();
        }
        /// <summary>
        /// 把日期字符串 转成指定格式
        /// </summary>
        /// <param name="s"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string FormatDate(this string s, string format = "yyyy-MM-dd")
        {
            DateTime d;
            bool b = DateTime.TryParse(s, out d);
            if (b)
            {
                return d.ToString("yyyy-MM-dd");
            }
            else
            {
                return s;
            }
        }
        /// <summary>
        /// 把类实例的可读写属性转换为字符串键值对
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static NameValueCollection ToNameValue<T>(this T obj) where T : class
        {
            var ps = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetField | BindingFlags.SetField);
            ps = ps.Where(x => x.CanRead && x.GetMethod.IsPublic && x.CanWrite && x.SetMethod.IsPublic).ToArray();
            NameValueCollection rst = new NameValueCollection();
            foreach(var p in ps)
            {
                rst.Add(p.Name, p.GetValue(obj)?.ToString());
            }
            return rst;
        }

        /// <summary>
        /// 找到迭代器里第一个符合条件的元素的序号
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objs"></param>
        /// <param name="func"></param>
        /// <returns>返回序号 没有满足条件的返回 -1</returns>
        public static int GetIndex<T>(this IEnumerable<T> objs, Func<T,bool> func)
        {
            int rst = -1;
            int i = 0;
            foreach(var o in objs)
            {
                if (func(o))
                {
                    rst = i;
                    break;
                }
                i++;
            }
            return rst;
        }
    }

    /// <summary>
    /// 枚举扩展
    /// </summary>
    public static class EnumExtension
    {
        /// <summary>
        /// 把枚举缓存起来，提高系统性能
        /// </summary>
        private static Dictionary<string, object> _EnumItemCollectionsStock;
        static EnumExtension()
        {
            _EnumItemCollectionsStock = new Dictionary<string, object>();
        }
        /// <summary>
        /// 获取枚举的 Description 特性值
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescription<TEnum>(this TEnum value) where TEnum : Enum
        {
            FieldInfo fileInfo = value.GetType().GetField(value.ToString());

            if (fileInfo != null)
            {
                var attributes = (DescriptionAttribute[])fileInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes.Length > 0)
                {
                    return attributes[0].Description;
                }
            }

            return value.ToString();
        }
        /// <summary>
        /// 把枚举转换为适合作下拉选项的形式
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="addDefault"></param>
        /// <param name="addDefaultText"></param>
        /// <returns></returns>
        public static IEnumerable<EnumListItem> GetEnumItemCollection<TEnum>(bool addDefault = true, string addDefaultText = "全部") where TEnum : Enum
        {
            string enumKey = typeof(TEnum).FullName + "_" + addDefault;
            if (!_EnumItemCollectionsStock.ContainsKey(enumKey) || _EnumItemCollectionsStock[enumKey] == null)
            {
                lock (_EnumItemCollectionsStock)
                {
                    IEnumerable<EnumListItem> defaultItem = new List<EnumListItem>();
                    if (addDefault)
                    {
                        defaultItem = new List<EnumListItem>() { new EnumListItem() { ItemText = addDefaultText } };
                    }
                    IEnumerable<EnumListItem> collection = defaultItem.Concat(Enum.GetValues(typeof(TEnum)).Cast<TEnum>().Select<TEnum, EnumListItem>(e => new EnumListItem { ItemValue = Convert.ToInt32(Enum.Parse(typeof(TEnum), e.ToString())), ItemText = e.GetDescription() }));

                    _EnumItemCollectionsStock[enumKey] = collection;
                }
            }
            return _EnumItemCollectionsStock[enumKey] as IEnumerable<EnumListItem>;
        }
    }
    /// <summary>
    /// 下拉选项格式的类
    /// </summary>
    public class EnumListItem
    {
        /// <summary>
        /// 选项值
        /// </summary>
        public int ItemValue { get; set; }
        /// <summary>
        /// 选项文字
        /// </summary>
        public string ItemText { get; set; }
    }
}

namespace Common
{
    /// <summary>
    /// 不太适合在 System 命名空间下的扩展方法
    /// </summary>
    public static class ExtMethods
    {
        /// <summary>
        /// IP（V4）的字符串形式转为数字形式
        /// </summary>
        /// <param name="IP"></param>
        /// <returns></returns>
        public static int ConvertIpToInt(string IP)
        {
            byte[] IPArr = IPAddress.Parse(IP).GetAddressBytes();
            int intAddress = BitConverter.ToInt32(IPArr, 0);
            return intAddress;
        }

        /// <summary>
        /// 两个时间进行比较 返回友好的中文提示
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
