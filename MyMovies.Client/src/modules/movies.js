import Movie from "@/models/Movie";
import movieServices from "@/services/movieServices";

export default function useMovies() {
  const addMovie = async (data) => {
    return await movieServices.add(data);
  };

  const deleteMovie = async (id) => {
    if (id) {
      return await movieServices.delete(id);
    }
  };

  const updateMovie = async (data) => {
    return await movieServices.update(data);
  };

  const getAllMovies = async () => {
    return await movieServices.getAll().then((response) => {
      let returned_movies = response.data;
      let movies = [];
      returned_movies.forEach((movie) => {
        movies.push(new Movie(movie));
      });
      return movies;
    });
  };

  const getMovieById = async (id) => {
    if (id) {
      return await movieServices.getById(id).then((response) => {
        return new Movie(response.data);
      });
    }
  };

  return {
    addMovie,
    deleteMovie,
    updateMovie,
    getAllMovies,
    getMovieById,
  };
}
