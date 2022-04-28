using MyMovies.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMovies.DataAccess.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.EnsureCreated();

            if(context.Movie.Any())
            {
                return;
            }

            var movies = new Movie[]
            {
                new Movie{Title="Wiking", ProductionYear=2022},
                new Movie{Title="Empty_Year_Movie"},
                new Movie{Title="Chłopaki nie płaczą", ProductionYear=2000},
                new Movie{Title="Leon zawodowiec", ProductionYear=1994},
                new Movie{Title="Very_Old_Movie", ProductionYear=1899},
            };
            foreach (Movie movie in movies)
            {
                context.Movie.Add(movie);
            }

            context.SaveChanges();
        }
    }
}
