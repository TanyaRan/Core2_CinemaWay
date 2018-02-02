namespace CinemaWay.Web.Controllers
{
    using CinemaWay.Web.Infrastructure.Extensions;
    using Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Services.Cinema;
    using Services.Profile;
    using System.Threading.Tasks;

    using static Data.DataConstants;

    public class UsersController : Controller
    {
        private readonly IUserService users;
        private readonly UserManager<User> userManager;
        private readonly ICinemaService projections;

        public UsersController(IUserService users,
            UserManager<User> userManager,
            ICinemaService projections)
        {
            this.users = users;
            this.userManager = userManager;
            this.projections = projections;
        }

        [Authorize]
        public async Task<IActionResult> Profile(string username)
        {
            var user = await this.userManager.FindByNameAsync(username);

            if (user == null)
            {
                return NotFound();
            }

            var profile = await this.users.ProfileAsync(user.Id);

            return View(profile);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddGrade(int id, Grade grade)
        {
            var userId = this.userManager.GetUserId(User);
            var username = this.userManager.GetUserName(User);

            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest();
            }

            if (!await projections.UserBookedATicket(id, userId))
            {
                return BadRequest();
            }

            var success = await this.users.AddGrade(id, userId, grade);

            if (!success)
            {
                return BadRequest();
            }

            TempData.AddSuccessMessage("Successfully added Grade!");

            return RedirectToAction(nameof(Profile), new { username });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SubmitIdeas(int id, IFormFile ideas)
        {
            var userId = this.userManager.GetUserId(User);
            var username = this.userManager.GetUserName(User);

            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest();
            }

            if (!await projections.UserBookedATicket(id, userId))
            {
                return BadRequest();
            }

            if (!ideas.FileName.EndsWith(".zip") || ideas.Length > IdeasSubmissionMaxLength)
            {
                TempData.AddErrorMessage("Your submission should be a '.zip' file with max size 2 MB!");
                return RedirectToAction(nameof(Profile), new { username });
            }

            var fileContent = await ideas.ToByteArrayAsync();

            var success = await this.users.SaveIdeas(id, userId, fileContent);

            if (!success)
            {
                return BadRequest();
            }

            TempData.AddSuccessMessage("Your Ideas-file saved successfully!");
            return RedirectToAction(nameof(Profile), new { username });
        }

        // TODO: The Lecturer can Dowload UserIdeas
        // TODO: The Lecturer can calculate the average screening grade and publish it

        // TODO: The User can comment projection or story
        // TODO: The Moderator can delete bad or offensive comments
    }
}
