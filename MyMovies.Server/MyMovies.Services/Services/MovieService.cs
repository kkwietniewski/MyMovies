using MyMovies.Models.Entities;
using MyMovies.Repositories.Interfaces;
using MyMovies.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyMovies.Services.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task AddAsync(Movie entity)
        {
            ValidMovie(entity);
            await _movieRepository.AddAsync(entity);
        }

        public async Task DeleteAsync(Movie entity)
        {
            await _movieRepository.DeleteAsync(entity);
        }

        public async Task<List<Movie>> GetAllAsync()
        {
            return await _movieRepository.GetAllAsync();
        }

        public async Task<Movie> GetByIdAsync(Guid id)
        {
            return await _movieRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Movie entity)
        {
            ValidMovie(entity);
            await _movieRepository.UpdateAsync(entity);
        }

        protected void ValidMovie(Movie entity)
        {
            if (entity.ProductionYear < 1900)
            {
                throw new ArgumentException("Movie Production Year must be equal 1900 or higher!");
            }
            else if (entity.ProductionYear > 2100)
            {
                throw new ArgumentException("Movie Production Year must be equal 2100 or lower!");
            }
            if (entity.Title == null)
            {
                throw new ArgumentException("Title parameter must be filled!");
            }
        } 
    }
}
