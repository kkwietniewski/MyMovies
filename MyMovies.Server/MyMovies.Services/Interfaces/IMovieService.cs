using MyMovies.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyMovies.Services.Interfaces
{
    public interface IMovieService
    {
        Task AddAsync(Movie entity);
        Task DeleteAsync(Movie entity);
        Task<List<Movie>> GetAllAsync();
        Task<Movie> GetByIdAsync(Guid id);
        Task UpdateAsync(Movie entity);
    }
}
