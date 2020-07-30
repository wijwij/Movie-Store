namespace MovieStore.Core.Models.Response
{
    public class UserReviewedMovieResponseModel
    {
        public int UserId { get; set; }
        public int MovieId { get; set; }
        
        public string Title { get; set; }
        public string PosterUrl { get; set; }
        
        public string ReviewText { get; set; }
        public decimal Rating { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
    }
}