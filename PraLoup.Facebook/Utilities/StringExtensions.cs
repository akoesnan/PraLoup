using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Script.Serialization;

namespace PraLoup.Facebook.Utilities
{
    public static class StringExtensions
    {
        public static dynamic GetJSON(this string jsontext)
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

            return str + delim + HttpUtility.UrlEncode(key) + "=" + HttpUtility.UrlEncode(value);
        }
    }
}