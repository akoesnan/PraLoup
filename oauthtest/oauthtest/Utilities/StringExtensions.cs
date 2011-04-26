using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace ProjectSafari.Utilities
{
    public static class StringExtensions
    {
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