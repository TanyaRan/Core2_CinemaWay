namespace CinemaWay.Web.Areas.Admin.Controllers
{
    using Data.Models;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.Projections;
    using Services.Admin;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using static WebConstants;

    public class ProjectionsController : BaseAdminController
    {
        private readonly UserManager<User> userManager;
        private readonly IAdminProjectionService projections;
        private readonly IAdminMovieService movies;
        private readonly IAdminThemeService themes;

        public ProjectionsController(
            UserManager<User> userManager,
            IAdminProjectionService projections,
            IAdminMovieService movies,
            IAdminThemeService themes)
        {
            this.userManager = userManager;
            this.projections = projections;
            this.movies = movies;
            this.themes = themes;
        }

        public async Task<IActionResult> Index(int page = 1)
            => View(model: new AdminListProjectionsViewModel
            {
                Projections = await this.projections.All(page),
                TotalProjections = await this.projections.Total(),
                CurrentPage = page
            });

        public async Task<IActionResult> Create() 
            => View(new AddProjectionFormModel
            {
                Lecturers = await this.GetLecturers(),
                Movies = await this.GetMovies(),
                Themes = await this.GetThemes()
            });

        [HttpPost]
        public async Task<IActionResult> Create(AddProjectionFormModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Lecturers = await this.GetLecturers();
                model.Movies = await this.GetMovies();
                model.Themes = await this.GetThemes();
                return View(model);
            }

            await this.projections.Create(
                model.Date,
                model.StartTime,
                model.Duration,
                model.Price,
                model.LecturerId,
                model.MovieId,
                model.ThemeId);

            TempData.AddSuccessMessage("Projection created successfully.");

            return RedirectToAction(nameof(Index));
        }

        private async Task<IEnumerable<SelectListItem>> GetLecturers()
        {
            var lecturers = await this.userManager
                .GetUsersInRoleAsync(LecturerRole);

            var lecturerListItems = lecturers
                .Select(l => new SelectListItem
                {
                    Text = l.UserName,
                    Value = l.Id
                })
                .ToList();

            return lecturerListItems;
        }

        private async Task<IEnumerable<SelectListItem>> GetMovies()
        {
            var allMovies = await this.movies.All();

            var movieListItems = allMovies
                .Select(m => new SelectListItem
                {
                    Text = m.Name,
                    Value = m.Id.ToString()
                })
                .ToList();

            return movieListItems;
        }

        private async Task<IEnumerable<SelectListItem>> GetThemes()
        {
            var allThemes = await this.themes.AllTitles();

            var themesListItems = allThemes
                .Select(t => new SelectListItem
                {
                    Text = t.Title,
                    Value = t.Id.ToString()
                })
                .ToList();

            return themesListItems;
        }
    }
}
