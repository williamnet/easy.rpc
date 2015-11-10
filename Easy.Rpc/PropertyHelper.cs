using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easy.Rpc
{
    static class PropertyHelper
    {
        public static Object GetPropertyValue(Type type, object instance, String name)
        {
            var properties = type.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

             var property = properties.SingleOrDefault(m => m.Name.ToUpper() == name.ToUpper());
             if (property == null)
             {
                 return string.Empty;
             }
             return property.GetValue(instance);
        }
    }
}
