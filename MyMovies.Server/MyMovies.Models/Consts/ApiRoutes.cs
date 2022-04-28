using System;
using System.Collections.Generic;
using System.Text;

namespace MyMovies.Models.Consts
{
    public class ApiRoutes
    {
        private const string Root = "api";

        public static class Movies
        {
            public const string Create = Root + "/movies";
            public const string Delete = Root + "/movies/{id}";
            public const string Update = Root + "/movies";
            public const string GetAll = Root + "/movies";
            public const string GetById = Root + "/movies/{id}";
        }
    }
}
