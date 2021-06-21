using Shop.Domain.Dto.Info;
using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Services.Mapping
{
    public static class InfoMapping
    {
        public static InfoDto ToDto(this Info source)
        {
            return new InfoDto
            {
                AboutUs = source.AboutUs,
                Address = source.Address,
                HelpForBuy = source.HelpForBuy,
                Id = source.Id,
                InstagramAccount = source.InstagramAccount,
                LawUs = source.LawUs,
                PhoneNumber1 = source.PhoneNumber1,
                PhoneNumber2 = source.PhoneNumber2,
                PhoneNumber3 = source.PhoneNumber3,
                PhoneNumber4 = source.PhoneNumber4,
                TelegramChanal = source.TelegramChanal,
                GarantyCondition = source.GarantyCondition
            };
        }

        public static Info ToEntity(this InfoDto source)
        {
            return new Info
            {
                AboutUs = source.AboutUs,
                Address = source.Address,
                HelpForBuy = source.HelpForBuy,
                InstagramAccount = source.InstagramAccount,
                LawUs = source.LawUs,
                PhoneNumber1 = source.PhoneNumber1,
                PhoneNumber2 = source.PhoneNumber2,
                PhoneNumber3 = source.PhoneNumber3,
                PhoneNumber4 = source.PhoneNumber4,
                TelegramChanal = source.TelegramChanal,
                GarantyCondition = source.GarantyCondition
            };
        }
    }
}
