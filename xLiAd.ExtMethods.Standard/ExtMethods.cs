using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace System
{
    /// <summary>
    /// 扩展方法
    /// </summary>
    public static class ExtMethods
    {
        /// <summary>
        /// 把特定类型实例转换为字符串键值对
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static NameValueCollection ToNameValue(this IEnumerable<KeyValuePair<string, StringValues>> obj)
        {
            NameValueCollection rst = new NameValueCollection();
            foreach (var i in obj)
            {
                rst.Add(i.Key, i.Value);
            }
            return rst;
        }
    }
}
