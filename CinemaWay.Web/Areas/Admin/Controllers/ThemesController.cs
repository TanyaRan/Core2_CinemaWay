namespace CinemaWay.Web.Areas.Admin.Controllers
{
    using Infrastructure.Extensions;
    using Infrastructure.Filters;
    using Microsoft.AspNetCore.Mvc;
    using Models.Themes;
    using Services.Admin;
    using System.Threading.Tasks;

    public class ThemesController : BaseAdminController
    {
        private readonly IAdminThemeService themes;

        public ThemesController(IAdminThemeService themes)
        {
            this.themes = themes;
        }

        public async Task<IActionResult> Index(int page = 1)
            => View(new AdminListThemesViewModel
            {
                Themes = await this.themes.All(page),
                TotalThemes = await this.themes.Total(),
                CurrentPage = page
            });

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Create(AddThemeFormModel model)
        {
            await this.themes.Create(
                model.Title,
                model.StartDate,
                model.EndDate);

            TempData.AddSuccessMessage("Theme created successfully.");

            return RedirectToAction(nameof(Index));
        }
    }
}
