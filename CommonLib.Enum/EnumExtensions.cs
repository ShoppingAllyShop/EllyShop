using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Enums
{
    public static class EnumExtensions
    {
        public static string GetEnumDescription(this Enum value)
        {
            var type = value.GetType();
            FieldInfo? fi = value.GetType().GetField(value.ToString());

            if (fi == null) 
            {
                throw new ArgumentException($"GetEnumDescription function received has a null field !");
            }
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }
    }
}
