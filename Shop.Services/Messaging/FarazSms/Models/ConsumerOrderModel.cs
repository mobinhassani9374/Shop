using Shop.Services.Messaging.NewFolder.FarazSms.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Services.Messaging.FarazSms.Models
{
    public class ConsumerOrderModel : Request
    {
        public ConsumerOrderModel()
        {
            this.patternCode = "fle7gyjyp3";
        }
        public List<InputDataForConsumerOrder> inputData { get; set; }
    }
}
