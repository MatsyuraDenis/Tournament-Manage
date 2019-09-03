using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace Tournament.Models.Helpers
{
    public static class UserHelper
    {
        public static string GetIp()
        {
            string address = "";
            WebRequest request = WebRequest.Create("http://checkip.dyndns.org/");

            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    address = reader.ReadToEnd();
                }
            }

            // Example of loaded page: -- Current IP Address: 45.45.45.45 --

            int first = address.IndexOf("Address: ") + 9;
            int last = address.IndexOf("</body>");

            address = address.Substring(first, last - first);

            return address;
        }
    }
}