using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Models.Info
{
    public class InfoViewModel
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
        public string PhoneNumbers
        {
            get
            {
                var result = "";
                if (!string.IsNullOrEmpty(PhoneNumber1))
                    result += PhoneNumber1;
                if (!string.IsNullOrEmpty(PhoneNumber2))
                    result +=" - " + PhoneNumber2;
                if (!string.IsNullOrEmpty(PhoneNumber3))
                    result +=" - " + PhoneNumber3;
                if (!string.IsNullOrEmpty(PhoneNumber4))
                    result +=" - " + PhoneNumber4;
                return result;
            }
        }
    }
}
