using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using MovieStore.Core.Entities;
using MovieStore.Core.RepositoryInterfaces;
using MovieStore.Core.ServiceInterfaces;

namespace MovieStore.Infrastructure.Services
{
    public class GenreService : IGenreService
    {
        /*
         * Genres don't change often. Genre service is invoked from layout page every time.
         * use in-memory caching for the first time, next time the genres can be got from memory.
         */
        private readonly IGenreRepository _genreRepository;
        private readonly IMemoryCache _memoryCache;
        private static readonly string _genresKey = "genres";
        // ToDo [review-caching process]
        private static readonly TimeSpan _defaultCacheDuration = TimeSpan.FromDays(30);

        public GenreService(IGenreRepository genreRepository, IMemoryCache memoryCache)
        {
            _genreRepository = genreRepository;
            _memoryCache = memoryCache;
        }
        
        public async Task<IEnumerable<Genre>> GetAllGenres()
        {
            var genres = await _memoryCache.GetOrCreateAsync(_genresKey, async entry =>
            {
                //check cache expire or not
                // ToDo debug check _memoryCache
                entry.SlidingExpiration = _defaultCacheDuration;
                return await _genreRepository.GetAllGenreAsync();
            });
            return genres;
            // return await _genreRepository.GetAllGenreAsync();
        }
    }
}