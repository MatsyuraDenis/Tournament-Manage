using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Web;

namespace Tournament.Models
{
    public class GenderType
    {
        public const string Male = "male";
        public const string Female = "female";
        public const string Unknown = "unknown";

        public static List<string> GetAllTypes()
        {
            return new List<string> {Unknown, Male, Female};
        }
    }
}