using System.ComponentModel.DataAnnotations;

namespace MovieStore.Core.Models.Request
{
    public class ReviewRequestModel
    {
        public int UserId { get; set; }
        public int MovieId { get; set; }
        
        [Required]
        [Range(0.0, 10.0)]
        public decimal Rating { get; set; }
        
        [Required]
        [MaxLength(550)]
        public string ReviewText { get; set; }
    }
}