using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieStore.Core.Entities;
using MovieStore.Core.RepositoryInterfaces;
using MovieStore.Infrastructure.Data;

namespace MovieStore.Infrastructure.Repositories
{
    public class UserRepository : EfRepository<User>, IUserRepository
    {
        public UserRepository(MoviesStoreDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email.Equals(email));
            return user;
        }

        public async Task<IEnumerable<Movie>> GetUserFavoriteMoviesAsync(int userId)
        {
            var movies = await _dbContext.Favorites.Where(f => f.UserId == userId).Include(f => f.Movie)
                .Select(f => new Movie{Id = f.Movie.Id, Title = f.Movie.Title, PosterUrl = f.Movie.PosterUrl}).ToListAsync();
            return movies;
        }

    }
}