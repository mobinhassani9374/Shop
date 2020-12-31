using Shop.Services.Messaging.NewFolder.FarazSms.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Services.Messaging.FarazSms.Models
{
    public class AdminOrderModel : Request
    {
        public AdminOrderModel()
        {
            this.patternCode = "4t4ftwoqce";
        }
        public List<InputDataForAdminOrder> inputData { get; set; }
    }
}
