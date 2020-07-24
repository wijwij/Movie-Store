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
        Task UnfavoriteMovie(int movieId, int userId);
        Task<bool> IsFavorite(int userId, int movieId);
        Task PurchaseMovie(UserPurchaseRequestModel requestModel, int userId);
    }
}