using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace PraLoup.WebApp.Utilities
{
    public static class EnumExtensions
    {
        public static SelectList ToSelectList<TEnum>(this TEnum enumObj)
        {

            var values = from TEnum e in Enum.GetValues(typeof(TEnum))
                         select new { ID = e, Name = GetAttributeValue<TEnum, DisplayAttribute, string>(e, x => x.Name) };

            return new SelectList(values, "Id", "Name", enumObj);
        }

        public static Expected GetAttributeValue<TEnum, T, Expected>(TEnum enumeration, Func<T, Expected> expression)
       where T : Attribute
        {
            T attribute = enumeration.GetType().GetMember(enumeration.ToString())[0].GetCustomAttributes(typeof(T), false).Cast<T>().SingleOrDefault();

            if (attribute == null)
                return default(Expected);

            return expression(attribute);
        }

    }
}