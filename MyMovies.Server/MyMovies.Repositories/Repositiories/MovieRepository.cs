using Microsoft.EntityFrameworkCore;
using MyMovies.DataAccess.Data;
using MyMovies.Models.Entities;
using MyMovies.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovies.Repositories.Repositiories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly AppDbContext _context;

        public async Task<Movie> AddAsync(Movie entity)
        {
            if (entity == null)
            {
                throw new ArgumentException($"{nameof(AddAsync)} passed entity must be not null!");
            }

            try
            {
                await _context.AddAsync(entity);
                await _context.SaveChangesAsync();

                _context.Entry(entity).State = EntityState.Detached;

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} couldn't be saved: {ex.Message}", ex);
            }
        }

        public async Task<Movie> DeleteAsync(Movie entity)
        {
            if (entity == null)
            {
                throw new ArgumentException($"{nameof(DeleteAsync)} passed entity must be not null!");
            }

            try
            {
                _context.Remove(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} couldn't be deleted: {ex.Message}", ex);
            }
        }

        public IQueryable<Movie> GetAll()
        {
            try
            {
                return _context.Set<Movie>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entities: {ex.Message}", ex);
            }
        }

        public async Task<List<Movie>> GetAllAsync(Func<IQueryable<Movie>, IQueryable<Movie>> func = null)
        {
            var query = GetAll();
            query = func != null ? func(query) : query;

            return await query.ToListAsync();
        }

        public async Task<Movie> GetByIdAsync(Guid id)
        {
            if (id == null)
            {
                throw new ArgumentException($"{nameof(GetByIdAsync)} passed id must be not null!");
            }

            try
            {
                return await _context.Set<Movie>().FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't find entity: {ex.Message}", ex);
            }
        }

        public async Task<Movie> UpdateAsync(Movie entity)
        {
            if (entity == null)
            {
                throw new ArgumentException($"{nameof(UpdateAsync)} passed entity must be not null!");
            }

            try
            {
                _context.Update(entity);
                await _context.SaveChangesAsync();

                _context.Entry(entity).State = EntityState.Detached;

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} couldn't be updated: {ex.Message}", ex);
            }
        }
    }
}
