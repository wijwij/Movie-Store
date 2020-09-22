using System.ComponentModel.DataAnnotations;

namespace MovieStore.Core.Models.Request
{
    public class ReviewRequestModel
    {
        public int UserId { get; set; }
        public int MovieId { get; set; }
        
        [Required]
        public decimal Rating { get; set; }
        
        [Required]
        [MaxLength(550)]
        public string ReviewText { get; set; }
    }
}