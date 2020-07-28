namespace MovieStore.Core.Models.Request
{
    public class UserFavoriteRequestModel
    {
        public int UserId { get; set; }
        public int MovieId { get; set; }
    }
}