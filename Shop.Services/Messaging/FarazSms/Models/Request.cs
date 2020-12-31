using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Services.Messaging.NewFolder.FarazSms.Models
{
    public abstract class Request
    {
        public string op { get; } = "pattern";
        public string user { get; } = "09197572162";
        public string pass { get; } = "0018684297";
        public string fromNum { get; } = "3000505";
        public string toNum { get; set; }
        public string patternCode { get; protected set; }
    }
}
