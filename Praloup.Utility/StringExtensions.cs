using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Script.Serialization;

namespace PraLoup.Utilities
{
    public static class StringExtensions
    {
        public static dynamic GetJson(this string jsontext)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            jss.RegisterConverters(new JavaScriptConverter[] { new DynamicJsonConverter() });

            return jss.Deserialize(jsontext, typeof(object)) as dynamic;            
        }

        public static string AppendQueryString(this string str, string key, string value)
        {
            string delim;
            if ((str == null) || !str.Contains("?"))
            {
                delim = "?";
            }
            else if (str.EndsWith("?") || str.EndsWith("&"))
            {
                delim = string.Empty;
            }
            else
            {
                delim = "&";
            }

            var sb = new StringBuilder(str);
            sb.Append(delim);
            sb.Append(HttpUtility.UrlEncode(key));
            sb.Append("=");
            sb.Append(HttpUtility.UrlEncode(value));
            return sb.ToString();
        }
    }
}