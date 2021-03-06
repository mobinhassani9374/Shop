﻿using Shop.Domain.Dto.Product;
using Shop.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using Shop.Utility.Extensions;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using DNTPersianUtils.Core;
using System.Reflection;

namespace Shop.Services.Validations
{
    public static class ProductValidation
    {
        public static ServiceResult IsValid<T>(this T dto)
        {
            var serviceResult = new ServiceResult(true);

            var properties = dto.GetType().GetProperties().ToList();

            foreach (var property in properties)
            {
                var customAttributes = property.CustomAttributes.ToList();

                foreach (var attribute in customAttributes)
                {
                    var value = property.GetValue(dto);

                    if (attribute.AttributeType == typeof(RequiredAttribute))
                        RequiredAttribute(serviceResult, value, property, attribute);

                    if (attribute.AttributeType == typeof(MaxLengthAttribute))
                        MaxLengthAttribute(serviceResult, value, property, attribute);

                    if (attribute.AttributeType == typeof(MinLengthAttribute))
                        MinLengthAttribute(serviceResult, value, property, attribute);

                    if (attribute.AttributeType == typeof(CompareAttribute))
                        CompareAttribute(serviceResult, value, property, attribute, properties, dto);

                    if (attribute.AttributeType == typeof(PhoneAttribute))
                        Phone(serviceResult, value, attribute);
                }
            }

            return serviceResult;
        }

        private static ServiceResult RequiredAttribute(ServiceResult serviceResult, object value, PropertyInfo property, CustomAttributeData attribute)
        {
            if (property.PropertyType == typeof(String))
            {
                if (value == null)
                    serviceResult.AddError(attribute.NamedArguments.Select(c => c.TypedValue.Value.ToString()).FirstOrDefault());

                else
                {
                    if (string.IsNullOrEmpty(value.ToString()))
                        serviceResult.AddError(attribute.NamedArguments.Select(c => c.TypedValue.Value.ToString()).FirstOrDefault());
                }
            }

            if (property.PropertyType == typeof(IFormFile))
            {
                if (value == null)
                    serviceResult.AddError(attribute.NamedArguments.Select(c => c.TypedValue.Value.ToString()).FirstOrDefault());
            }

            return serviceResult;
        }

        private static ServiceResult MaxLengthAttribute(ServiceResult serviceResult, object value, PropertyInfo property, CustomAttributeData attribute)
        {
            int length = (int)attribute.ConstructorArguments.Select(c => c.Value).FirstOrDefault();
            var errorMessage = "";

            if (property.PropertyType == typeof(String))
            {
                if (value != null && value.ToString().Length > length)
                    errorMessage = attribute.NamedArguments.Select(c => c.TypedValue.Value.ToString()).FirstOrDefault();
            }

            if (property.PropertyType == typeof(IFormFile))
            {
                if (value != null && ((IFormFile)value).Length > length)
                    errorMessage = attribute.NamedArguments.Select(c => c.TypedValue.Value.ToString()).FirstOrDefault();
            }

            errorMessage = errorMessage.Replace("@", length.ToPersianNumbers());

            if (!string.IsNullOrEmpty(errorMessage))
                serviceResult.AddError(errorMessage);

            return serviceResult;
        }

        private static ServiceResult MinLengthAttribute(ServiceResult serviceResult, object value, PropertyInfo property, CustomAttributeData attribute)
        {
            int length = (int)attribute.ConstructorArguments.Select(c => c.Value).FirstOrDefault();
            var errorMessage = "";

            if (property.PropertyType == typeof(String))
            {
                if (value != null && value.ToString().Length < length)
                    errorMessage = attribute.NamedArguments.Select(c => c.TypedValue.Value.ToString()).FirstOrDefault();
            }

            errorMessage = errorMessage.Replace("@", length.ToPersianNumbers());

            if (!string.IsNullOrEmpty(errorMessage))
                serviceResult.AddError(errorMessage);

            return serviceResult;
        }

        private static ServiceResult CompareAttribute(ServiceResult serviceResult, object value, PropertyInfo property, CustomAttributeData attribute, List<PropertyInfo> properties, object source)
        {
            var comparePropertyName = attribute.ConstructorArguments.Select(c => c.Value).FirstOrDefault();
            var compareProperty = properties.Where(c => c.Name == (string)comparePropertyName).FirstOrDefault();
            var compareValue = compareProperty.GetValue(source)?.ToString();

            if (compareValue != value.ToString())
                serviceResult.AddError(attribute.NamedArguments.Select(c => c.TypedValue.Value.ToString()).FirstOrDefault());

            return serviceResult;
        }

        private static ServiceResult Phone(ServiceResult serviceResult, object value, CustomAttributeData attribute)
        {
            if (!value.ToString().IsValidIranianMobileNumber())
                serviceResult.AddError(attribute.NamedArguments.Select(c => c.TypedValue.Value.ToString()).FirstOrDefault());

            return serviceResult;
        }
    }
}
