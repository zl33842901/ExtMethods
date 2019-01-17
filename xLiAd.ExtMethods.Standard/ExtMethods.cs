using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace xLiAd.ExtMethods.Standard
{
    public static class ExtMethods
    {
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
