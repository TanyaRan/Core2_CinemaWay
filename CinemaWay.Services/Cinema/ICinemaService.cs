namespace CinemaWay.Services.Cinema
{
    using Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICinemaService
    {
        Task<IEnumerable<ListProjectionWithMovieModel>> Active(int page = 1);

        Task<int> TotalActive();

        Task<IEnumerable<ListProjectionWithMovieModel>> All(int page = 1);

        Task<int> Total();

        Task<IEnumerable<ListProjectionWithMovieModel>> FindAsyncMovie(string searchText);

        Task<IEnumerable<ListProjectionWithMovieModel>> FindAsyncTheme(string searchText);

        Task<TModel> ByIdAsync<TModel>(int id) where TModel : class;

        Task<bool> BookTicket(int projectionId, string userId);

        Task<bool> RefuseTicket(int projectionId, string userId);

        Task<bool> UserBookedATicket(int projectionId, string userId);

        Task<byte[]> GetPdfTicket(int projectionId, string userId);
    }
}
