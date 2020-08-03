using System.Collections.Generic;
using System.Threading.Tasks;
using MovieStore.Core.Entities;

namespace MovieStore.Core.RepositoryInterfaces
{
    public interface IUserRepository : IAsyncRepository<User>
    {
        Task<User> GetUserByEmailAsync(string email);
        Task<IEnumerable<Movie>> GetUserFavoriteMoviesAsync(int userId);
        Task<User> GetUserProfileAsync(int userId);
    }
}