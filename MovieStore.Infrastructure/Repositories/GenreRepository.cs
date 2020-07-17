using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieStore.Core.Entities;
using MovieStore.Core.RepositoryInterfaces;
using MovieStore.Infrastructure.Data;

namespace MovieStore.Infrastructure.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        protected readonly MoviesStoreDbContext _dbContext;

        public GenreRepository(MoviesStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Genre>> GetAllGenreAsync()
        {
            // ToDo [why use set instead of select(g => g)]
            // return await _dbContext.Genres.Select(g => g).ToListAsync();
            return await _dbContext.Set<Genre>().ToListAsync();
        }
    }
}