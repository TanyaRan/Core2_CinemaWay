namespace CinemaWay.Web.Controllers
{
    using Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Services.Cinema;
    using Services.Profile;
    using System.Threading.Tasks;

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
        public async Task<IActionResult> AddGrade(int id, Grade grade)
        {
            var userId = this.userManager.GetUserId(User);

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

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
