using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcMovie.Models
{
    /// <summary>
    /// The Movie class contains the model definition for a movie object.
    /// </summary>
    public class Movie
    {
        /// <summary>
        /// The ID field is the private key id.
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// The Title field is the movie title.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// The ReleaseDate field is the release date for the movie.
        /// </summary>
        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        /// <summary>
        /// The Genre field is the movie's genre.
        /// In some cases multiple genres are assigned separated by commas.
        /// </summary>
        public string Genre { get; set; }
        /// <summary>
        /// The Price field is the movie's selling price.
        /// </summary>
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        /// <summary>
        /// The ImageURL field stores a link to a movie poster.
        /// </summary>
        [Display(Name = "Trailer")]
        public string ImageURL { get; set; }
        /// <summary>
        /// The TrailerURL field stores the YouTube ID for a movie's trailer.
        /// </summary>
        public string TrailerURL { get; set; }
        /// <summary>
        /// The Description field stores the movie's plot description.
        /// </summary>
        public string Description { get; set; }
    }
}
