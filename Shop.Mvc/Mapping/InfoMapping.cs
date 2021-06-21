using Shop.Domain.Dto.Info;
using Shop.Mvc.Models.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Mapping
{
    public static class InfoMapping
    {
        public static InfoDto ToDto(this InfoViewModel source)
        {
            return new InfoDto
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
                GarantyCondition = source.GarantyCondition,
                TelegramChanal = source.TelegramChanal
            };
        }

        public static InfoViewModel ToViewModel(this InfoDto source)
        {
            return new InfoViewModel
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
