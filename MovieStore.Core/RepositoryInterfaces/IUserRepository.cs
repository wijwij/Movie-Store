using System.Threading.Tasks;
using MovieStore.Core.Entities;

namespace MovieStore.Core.RepositoryInterfaces
{
    public interface IUserRepository : IAsyncRepository<User>
    {
        Task<User> GetUserByEmail(string email);
    }
}