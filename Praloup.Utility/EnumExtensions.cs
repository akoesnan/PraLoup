using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PraLoup.DataAccess.Enums;

namespace PraLoup.Utilities
{
    public static class EnumExtensions
    {
        public static TValue GetAttributeValue<TAttribute, TValue>(this Enum enumeration, Func<TAttribute, TValue> expression) where TAttribute : Attribute
        {
            TAttribute attribute = enumeration.GetType().GetMember(enumeration.ToString())[0].GetCustomAttributes(typeof(TAttribute), false).Cast<TAttribute>().SingleOrDefault();
            if (attribute == null)
                return default(TValue);
            return expression(attribute);
        }

        public static string GetFacebookValue(this Enum enumeration) 
        {
            return enumeration.GetAttributeValue<FacebookValueAttribute, string>(e => e.FacebookValue);
        }
    }
}
