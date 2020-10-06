using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace MvcMovie.Helpers
{
    /* Class used to serialize and deserialize data into json strings.
     * Sourced from http://learningprogramming.net/net/asp-net-core-mvc/build-shopping-cart-with-session-in-asp-net-core-mvc/
     */
    public static class SessionHelper
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
