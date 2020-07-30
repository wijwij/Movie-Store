using System.ComponentModel.DataAnnotations.Schema;

namespace MovieStore.Core.Entities
{
    /*
     * one trailer belongs to single Movie, but one movie can have multiple Trailers
     */
    public class Trailer
    {
        public int Id { get; set; }
        public string TrailerUrl { get; set; }
        public string Name { get; set; }
        
        public int MovieId { get; set; }
        
        // Navigation property. Given the trailer id, it is easy to find the Movie
        public Movie Movie { get; set; }
    }
}