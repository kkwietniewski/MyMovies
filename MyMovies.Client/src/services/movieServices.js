import ApiService from "@/services/apiServices";
export default {
  add(data) {
    return ApiService.processRequest(`/movies`, ApiService.post, data);
  },
  delete(movie_id) {
    return ApiService.processRequest(`/movies/${movie_id}`, ApiService.delete);
  },
  update(data) {
    return ApiService.processRequest(`/movies`, ApiService.put, data);
  },
  getAll() {
    return ApiService.processRequest(`/movies`, ApiService.get);
  },
  getById(movie_id) {
    return ApiService.processRequest(`/movies/${movie_id}`, ApiService.get);
  },
};
