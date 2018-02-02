namespace CinemaWay.Services.Admin
{
    using Data.Models;
    using Models.Movies;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAdminMovieService
    {
        Task Create(
            string name,
            string description,
            string imageUrl,
            string director,
            Genre genre);

        Task<IEnumerable<AdminListMoviesModel>> All(int page = 1);

        Task<int> Total();

        Task<IEnumerable<ShortMovieModel>> All();
    }
}
