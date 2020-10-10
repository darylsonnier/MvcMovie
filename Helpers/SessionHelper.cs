using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace MvcMovie.Helpers
{
    /// <summary>
    /// The SessionHelper class is used to serialize and deserialize data into json strings. 
    /// It is sourced from http://learningprogramming.net/net/asp-net-core-mvc/build-shopping-cart-with-session-in-asp-net-core-mvc/
    /// </summary>
    public static class SessionHelper
    {
        /// <summary>
        /// The SetObjectAsJson method serializes an object into a Json string.
        /// </summary>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        /// <summary>
        /// The GetObjectFromJson method deserializes a Json string into an object of the specified type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
