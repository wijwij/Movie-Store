using System.Collections.Generic;
using System.Threading.Tasks;
using MovieStore.Core.Entities;
using MovieStore.Core.Models.Request;
using MovieStore.Core.Models.Response;

namespace MovieStore.Core.ServiceInterfaces
{
    public interface IUserService
    {
        Task<UserRegisterResponseModel> RegisterUser(UserRegisterRequestModel requestModel);
        Task<LoginResponseModel> ValidateUser(string email, string password);
        Task FavoriteMovie(int movieId, int userId);
        Task RemoveFavoriteMovie(int movieId, int userId);
        Task<bool> IsMovieFavoriteByUser(int userId, int movieId);
        Task PurchaseMovie(UserPurchaseRequestModel requestModel);
        Task<IEnumerable<Movie>> GetUserFavoriteMovies(int userId);
        Task<IEnumerable<UserReviewedMovieResponseModel>> GetUserReviewedMovies(int userId);
        Task<bool> IsMovieReviewedByUser(int userId, int movieId);
    }
}