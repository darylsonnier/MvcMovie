using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMovie.Helpers
{
    public class HelperClass
    {
        /* Combine function replaces spaces with underscores.  
         * Used for creating a pattern to div IDs for the JavaScript function that displays modals for the plot description of movies.
         */
        public static string Combine(string a, string b)
        {
            return  a.Replace(" ", "_") + b;
        }
    }
}
