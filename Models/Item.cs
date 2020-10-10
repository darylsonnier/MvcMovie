namespace MvcMovie.Models
{
    /// <summary>
    /// The Item class is the model for the shopping cart.
    /// </summary>
    public class Item
    {
        /// <summary>
        /// The Movie field is a movie object.
        /// </summary>
        public Movie Movie { get; set; }
        /// <summary>
        /// The Quantity field is the number of copies of a movie in the cart.
        /// </summary>
        public int Quantity { get; set; }
    }
}
