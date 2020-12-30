using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Dto.Info
{
    public class InfoDto
    {
        public int Id { get; set; }
        public string InstagramAccount { get; set; }
        public string TelegramChanal { get; set; }
        public string PhoneNumber1 { get; set; }
        public string PhoneNumber2 { get; set; }
        public string PhoneNumber3 { get; set; }
        public string PhoneNumber4 { get; set; }
        public string Address { get; set; }
        public string AboutUs { get; set; }
        public string LawUs { get; set; }
        public string HelpForBuy { get; set; }
        public static InfoDto CreateNewInstance()
        {
            return new InfoDto
            {
                TelegramChanal = "",
                PhoneNumber3 = "",
                PhoneNumber4 = "",
                PhoneNumber2 = "",
                PhoneNumber1 = "",
                AboutUs = "",
                Address = "",
                HelpForBuy = "",
                InstagramAccount = "",
                LawUs = ""
            };
        }
    }
}
