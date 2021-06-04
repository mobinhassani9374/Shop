using Shop.Domain.Enumeration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Shop.Utility.Extensions
{
    public static class EnumExtension
    {
        public static string GetDisplayName(this Enum source)
        {
            var fildeInfo = source.GetType()
                .GetTypeInfo()
                .DeclaredFields
                .Where(c => c.Name == source.ToString())
                .FirstOrDefault();

            var attr = fildeInfo.CustomAttributes
                  .Where(c => c.AttributeType == typeof(System.ComponentModel.DisplayNameAttribute))
                  .FirstOrDefault();

            var value = attr.ConstructorArguments.Select(c => c.Value).FirstOrDefault();

            return value.ToString();
        }

        public static string GetFolderName(this FileType fileType)
        {
            if (fileType == FileType.ProductImage)
                return "Images";

            else if (fileType == FileType.SlideShowImage)
                return "SlideShowImages";

            else if (fileType == FileType.EducationImage)
                return "EducationImages";

            else if (fileType == FileType.EducationFile)
                return "EducationFiles";

            return "";
        }
    }
}
