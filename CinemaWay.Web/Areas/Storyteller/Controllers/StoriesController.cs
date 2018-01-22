namespace CinemaWay.Web.Areas.Storyteller.Controllers
{
    using Data.Models;
    using Infrastructure.Extensions;
    using Infrastructure.Filters;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services.Html;
    using Services.Storyteller;
    using System.Threading.Tasks;

    using static WebConstants;

    [Area(StorytellerArea)]
    [Authorize(Roles = LecturerRole)]
    public class StoriesController : Controller
    {
        private readonly IStoryService stories;
        private readonly IHtmlService html;
        private readonly UserManager<User> userManager;

        public StoriesController(
            IStoryService stories,
            IHtmlService html,
            UserManager<User> userManager)
        {
            this.stories = stories;
            this.html = html;
            this.userManager = userManager;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(int page = 1) 
            => View(new ListStoriesViewModel
            {
                Stories = await this.stories.All(page),
                TotalStories = await this.stories.Total(),
                CurrentPage = page
            });

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id) 
            => this.ViewOrNotFound(await this.stories.ById(id));

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Create(CreateStoryFormModel model)
        {
            model.Content = this.html.Sanitize(model.Content);

            var userId = this.userManager.GetUserId(User);

            await this.stories.Create(
                model.Title,
                model.Content,
                model.ImageUrl,
                model.ReviewOf,
                userId);

            return RedirectToAction(nameof(Index));
        }
    }
}
