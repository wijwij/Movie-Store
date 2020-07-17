using System.Collections.Generic;
using System.Threading.Tasks;
using MovieStore.Core.Entities;

namespace MovieStore.Core.ServiceInterfaces
{
    public interface IGenreService
    {
        Task<IEnumerable<Genre>> GetAllGenres();
    }
}