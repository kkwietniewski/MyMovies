using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyMovies.DataAccess.Data;
using MyMovies.Models.Consts;
using MyMovies.Models.Entities;
using MyMovies.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMovies.Api.Controllers.Movies
{
    [ApiController]
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly ILogger _logger;

        public MoviesController(IMovieService movieService, ILogger<MoviesController> logger)
        {
            _movieService = movieService;
            _logger = logger;
        }

        [HttpPost(ApiRoutes.Movies.Create)]
        public async Task<IActionResult> CreateMovie([FromBody] Movie entity)
        {
            try
            {
                await _movieService.AddAsync(entity);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }

        }

        [HttpDelete(ApiRoutes.Movies.Delete)]
        public async Task<IActionResult> DeleteMovie([FromRoute] Guid id)
        {
            try
            {
                await _movieService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet(ApiRoutes.Movies.GetAll)]
        public async Task<IActionResult> GetMovies()
        {
            try
            {
                var response = await _movieService.GetAllAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet(ApiRoutes.Movies.GetById)]
        public async Task<IActionResult> GetMovieById([FromRoute] Guid id)
        {
            try
            {
                var response = await _movieService.GetByIdAsync(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut(ApiRoutes.Movies.Update)]
        public async Task<IActionResult> UpdateMovie([FromBody] Movie entity)
        {
            try
            {
                await _movieService.UpdateAsync(entity);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }
}
