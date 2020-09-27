using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMovie.Helpers
{
    public class HelperClass
    {
        public static string Combine(string a, string b)
        {
            return  a.Replace(" ", "_") + b;
        }
    }
}
