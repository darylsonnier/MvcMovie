namespace MvcMovie.Helpers
{
    /// <summary>
    /// The HelperClass class contains helper functions.
    /// </summary>
    public class HelperClass
    {
        /// <summary>
        /// The Combine function replaces spaces with underscores.   
        /// It is used for creating a pattern to div IDs for the JavaScript function that displays modals for the plot description of movies.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static string Combine(string a, string b)
        {
            return a.Replace(" ", "_") + b;
        }
    }
}
