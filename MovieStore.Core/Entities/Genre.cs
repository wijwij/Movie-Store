using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieStore.Core.Entities
{
    /*
     * Genre class represents our Domain Model, all properties in Genre Table
     */
    [Table("Genre")]
    public class Genre
    {
        // By convention, EF is gonna consider any property in the entity class as Primary key
        public int  Id { get; set; }
        
        [MaxLength(64)]
        [Required]
        public string Name { get; set; }

        public ICollection<MovieGenre> MovieGenres { get; set; }
    }
}