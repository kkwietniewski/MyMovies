using MyMovies.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovies.Repositories.Interfaces
{
    public interface IMovieRepository
    {
        Task<Movie> AddAsync(Movie entity);
        Task<Guid> DeleteAsync(Guid id);
        IQueryable<Movie> GetAll();
        Task<List<Movie>> GetAllAsync(Func<IQueryable<Movie>, IQueryable<Movie>> func = null);
        Task<Movie> GetByIdAsync(Guid id);
        Task<Movie> UpdateAsync(Movie entity);
    }
}
