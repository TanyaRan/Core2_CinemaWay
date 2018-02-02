namespace CinemaWay.Tests.Web.Areas.Admin.Controllers.ProjectionsControllerTests
{
    using CinemaWay.Data.Models;
    using CinemaWay.Services.Admin;
    using CinemaWay.Web;
    using CinemaWay.Web.Areas.Admin.Controllers;
    using CinemaWay.Web.Areas.Admin.Models.Projections;
    using FluentAssertions;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Mocks;
    using Moq;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class Create_Get_Should
    {
        private const string FirstUserId = "1";
        private const string FirstUserUsername = "First";
        private const string SecondUserId = "2";
        private const string SecondUserUsername = "Second";

        [Fact]
        public async Task ReturnViewWithValidModel()
        {
            // Arrange
            var userManager = GetUserManagerMock();

            var projectionService = new Mock<IAdminProjectionService>();
            var movieServive = new Mock<IAdminMovieService>();
            var themeService = new Mock<IAdminThemeService>();

            var controller = new ProjectionsController(userManager.Object, projectionService.Object, movieServive.Object, themeService.Object);

            // Act
            var result = await controller.Create();

            // Assert
            result.Should().BeOfType<ViewResult>();

            var model = result.As<ViewResult>().Model;
            model.Should().BeOfType<AddProjectionFormModel>();

            var formModel = model.As<AddProjectionFormModel>();
            this.AssertLecturersSelectList(formModel.Lecturers);
        }

        private Mock<UserManager<User>> GetUserManagerMock()
        {
            var userManager = UserManagerMock.New;
            userManager
                .Setup(u => u.GetUsersInRoleAsync(WebConstants.LecturerRole))
                .ReturnsAsync(new List<User>
                {
                    new User { Id = FirstUserId, UserName = FirstUserUsername },
                    new User { Id = SecondUserId, UserName = SecondUserUsername }
                });

            return userManager;
        }

        private void AssertLecturersSelectList(IEnumerable<SelectListItem> lecturers)
        {
            lecturers.Should().Match(items => items.Count() == 2);
            lecturers.First().Should().Match(u => u.As<SelectListItem>().Value == FirstUserId);
            lecturers.First().Should().Match(u => u.As<SelectListItem>().Text == FirstUserUsername);
            lecturers.Last().Should().Match(u => u.As<SelectListItem>().Value == SecondUserId);
            lecturers.Last().Should().Match(u => u.As<SelectListItem>().Text == SecondUserUsername);
        }
    }
}
