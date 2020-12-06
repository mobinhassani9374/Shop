using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Attributes
{
    public class SelectItemAttribute : Attribute
    {
        public SelectItemAttribute(Type enumType)
        {
            EnumType = enumType;
        }
        public Type EnumType { get; set; }
    }
}
