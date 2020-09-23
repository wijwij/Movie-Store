namespace MovieStore.Core.Models.Response
{
    public class RatedMovieCardResponseModel
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string PosterUrl { get; set; }
        public decimal? Rating { get; set; }
    }
}