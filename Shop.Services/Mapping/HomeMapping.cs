using Shop.Domain.Dto.Home;
using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Services.Mapping
{
    public static class HomeMapping
    {
        public static ContactUs ToEntity(this ContactUsDto source)
        {
            return new ContactUs
            {
                Description = source.Description,
                UserId = source.UserId
            };
        }
    }
}
