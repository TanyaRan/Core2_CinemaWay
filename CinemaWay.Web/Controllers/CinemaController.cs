namespace CinemaWay.Web.Controllers
{
    using Data.Models;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.Cinema;
    using Services.Cinema;
    using Services.Cinema.Models;
    using System;
    using System.Threading.Tasks;

    public class CinemaController : Controller
    {
        private readonly ICinemaService cinema;
        private readonly UserManager<User> userManager;

        public CinemaController(ICinemaService cinema,
            UserManager<User> userManager)
        {
            this.cinema = cinema;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index(int page = 1)
            => View(new ListProjectionWithMovieViewModel
            {
                Projections = await this.cinema.Active(page),
                TotalProjections = await this.cinema.TotalActive(),
                CurrentPage = page
            });

        public async Task<IActionResult> All(int page = 1)
            => View(new ListProjectionsAndSearchViewModel
            {
                Projections = await this.cinema.All(page),
                TotalProjections = await this.cinema.Total(),
                CurrentPage = page
            });

        public async Task<IActionResult> Search(SearchFormModel model)
        {
            var viewModel = new SearchViewModel
            {
                SearchText = model.SearchText
            };

            if (string.IsNullOrWhiteSpace(model.SearchText))
            {
                return RedirectToAction(nameof(All));
            }

            // TODO: Fix Search logic
            if (model.SearchInMovies)
            {
                viewModel.Projections = await this.cinema.FindAsyncMovie(model.SearchText);
            }

            if (model.SearchInThemes)
            {
                viewModel.Projections = await this.cinema.FindAsyncTheme(model.SearchText);
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = new ProjectionDetailsViewModel
            {
                Projection = await this.cinema.ByIdAsync<ProjectionDetailsModel>(id)
            };

            if (model.Projection == null)
            {
                return NotFound();
            }

            if (User.Identity.IsAuthenticated)
            {
                var userId = this.userManager.GetUserId(User);
                model.UserBookedATicket = await this.cinema.UserBookedATicket(id, userId);
            }

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> BookTicket(int id)
        {
            var userId = this.userManager.GetUserId(User);

            var success = await this.cinema.BookTicket(id, userId);

            if (!success)
            {
                return BadRequest();
            }

            TempData.AddSuccessMessage("You booked a ticket.");

            return RedirectToAction(nameof(Details), new { id });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RefuseTicket(int id)
        {
            var userId = this.userManager.GetUserId(User);

            var success = await this.cinema.RefuseTicket(id, userId);

            if (!success)
            {
                return BadRequest();
            }

            TempData.AddSuccessMessage("Sorry to give up!");

            return RedirectToAction(nameof(Details), new { id });
        }

        [Authorize]
        public async Task<IActionResult> DownloadTicket(int id)
        {
            var userId = this.userManager.GetUserId(User);

            var ticket = await this.cinema.GetPdfTicket(id, userId);

            if (ticket == null)
            {
                return BadRequest();
            }

            var fileName = string.Format("CinemaWay_{0}.{1}.{2}.pdf", DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year);

            return File(ticket, "application/pdf", fileName);
        }
    }
}
