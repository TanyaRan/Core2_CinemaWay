namespace CinemaWay.Web.Areas.Admin.Controllers
{
    using Infrastructure.Extensions;
    using Infrastructure.Filters;
    using Microsoft.AspNetCore.Mvc;
    using Models.Movies;
    using Services.Admin;
    using System.Threading.Tasks;

    public class MoviesController : BaseAdminController
    {
        private readonly IAdminMovieService movies;

        public MoviesController(IAdminMovieService movies)
        {
            this.movies = movies;
        }

        public async Task<IActionResult> Index(int page = 1)
            => View(model: new AdminListMoviesViewModel
            {
                Movies = await this.movies.All(page),
                TotalMovies = await this.movies.Total(),
                CurrentPage = page
            });

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Create(AddMovieFormModel model)
        {
            await this.movies.Create(
                model.Name,
                model.Description,
                model.ImageUrl,
                model.Director, 
                model.Genre);

            TempData.AddSuccessMessage("Movie created successfully.");

            return RedirectToAction(nameof(Index));
        }
    }
}
