using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tournament.Models
{
    public class AuthMessageSenderOptions
    {
        public string SendGridUser { get; set; }
        public string SendGridKey { get; set; }
    }
}