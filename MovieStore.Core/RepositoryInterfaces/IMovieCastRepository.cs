using System.Collections.Generic;
using System.Threading.Tasks;
using MovieStore.Core.Entities;

namespace MovieStore.Core.RepositoryInterfaces
{
    public interface IMovieCastRepository : IAsyncRepository<MovieCast>
    {
        public Task<IEnumerable<MovieCast>> GetCastsByMovieIdAsync(int movieId);
    }
}