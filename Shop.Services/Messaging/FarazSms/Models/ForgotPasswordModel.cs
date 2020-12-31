using Shop.Services.Messaging.NewFolder.FarazSms.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Services.Messaging.FarazSms.Models
{
    public class ForgotPasswordModel : Request
    {
        public ForgotPasswordModel()
        {
            this.patternCode = "5i98nlkip7";
        }
        public List<InputDataForForgotPassword> inputData { get; set; }
    }
}
