using System.Collections.Generic;
using System.Threading.Tasks;
using MovieStore.Core.Entities;
using MovieStore.Core.RepositoryInterfaces;
using MovieStore.Core.ServiceInterfaces;

namespace MovieStore.Infrastructure.Services
{
    public class GenreService : IGenreService
    {
        protected readonly IGenreRepository _genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }
        
        public async Task<IEnumerable<Genre>> GetAllGenres()
        {
            return await _genreRepository.GetAllGenreAsync();
        }
    }
}