using Shop.Domain.Dto.Product;
using Shop.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using Shop.Utility.Extensions;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Shop.Services.Validations
{
    public static class ProductValidation
    {
        public static ServiceResult IsValid<T>(this T dto)
        {
            var seriveResult = new ServiceResult(true);

            var properties = dto.GetType().GetProperties().ToList();

            foreach (var property in properties)
            {
                var customAttributes = property.CustomAttributes.ToList();
                foreach (var attribute in customAttributes)
                {
                    if (attribute.AttributeType == typeof(RequiredAttribute))
                    {
                        if (property.PropertyType == typeof(String))
                        {
                            var value = property.GetValue(dto);
                            if (string.IsNullOrEmpty(value.ToString()))
                                seriveResult.AddError(attribute.NamedArguments.Select(c => c.TypedValue.Value.ToString()).FirstOrDefault());
                        }

                        if (property.PropertyType == typeof(IFormFile))
                        {
                            var value = property.GetValue(dto);
                            if(value==null)
                                seriveResult.AddError(attribute.NamedArguments.Select(c => c.TypedValue.Value.ToString()).FirstOrDefault());
                        }
                    }
                }
            }

            return seriveResult;
        }
    }
}
