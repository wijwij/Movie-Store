using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieStore.Core.Entities;

namespace MovieStore.Core.RepositoryInterfaces
{
    public interface IGenreRepository
    {
        Task<IEnumerable<Genre>> GetAllGenreAsync();
    }
}