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
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Mocks;
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class Create_Post_Should
    {
        private const string FirstUserId = "1";
        private const string FirstUserUsername = "First";
        private const string SecondUserId = "2";
        private const string SecondUserUsername = "Second";

        [Fact]
        public async Task ReturnViewWithCorrectModel_WhenModelStateIsInvalid()
        {
            // Arrange
            var userManager = GetUserManagerMock();

            var projectionService = new Mock<IAdminProjectionService>();
            var movieServive = new Mock<IAdminMovieService>();
            var themeService = new Mock<IAdminThemeService>();

            var controller = new ProjectionsController(userManager.Object, projectionService.Object, movieServive.Object, themeService.Object);

            controller.ModelState.AddModelError(string.Empty, "Error");

            // Act
            var result = await controller.Create(new AddProjectionFormModel());

            // Assert
            result.Should().BeOfType<ViewResult>();

            var model = result.As<ViewResult>().Model;
            model.Should().BeOfType<AddProjectionFormModel>();

            var formModel = model.As<AddProjectionFormModel>();
            this.AssertLecturersSelectList(formModel.Lecturers);
        }

        [Fact]
        public async Task ReturnRedirectWithValidModel()
        {
            // Arrange
            var dateValue = DateTime.UtcNow.AddDays(10);
            const int startTimeValue = 20;
            const int durationValue = 120;
            const decimal priceValue = 10.0m;
            const string lecturerIdValue = "1";
            const int movieIdValue = 1;
            const int themeIdValue = 1;

            DateTime modelDate = DateTime.UtcNow;
            int modelStartTime = 0;
            int modelDuration = 0;
            decimal modelPrice = 0;
            string modelLecturerId = null;
            int modelMovieId = 0;
            int modelThemeId = 0;
            string successMessage = null;

            var projectionService = new Mock<IAdminProjectionService>();
            projectionService
                .Setup(p => p.Create(It.IsAny<DateTime>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<decimal>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .Callback((DateTime date, int startTime, int duration, decimal price, string lecturerId, int movieId, int themeId) =>
                {
                    modelDate = date;
                    modelStartTime = startTime;
                    modelDuration = duration;
                    modelPrice = price;
                    modelLecturerId = lecturerId;
                    modelMovieId = movieId;
                    modelThemeId = themeId;
                })
                .Returns(Task.CompletedTask);

            var tempData = new Mock<ITempDataDictionary>();
            tempData
                .SetupSet(t => t[WebConstants.TempDataSuccessMessageKey] = It.IsAny<string>())
                .Callback((string key, object message) => successMessage = message as string);

            var controller = new ProjectionsController(null, projectionService.Object, null, null);
            controller.TempData = tempData.Object;

            // Act
            var result = await controller.Create(new AddProjectionFormModel
            {
                Date = dateValue,
                StartTime = startTimeValue,
                Duration = durationValue,
                Price = priceValue,
                LecturerId = lecturerIdValue,
                MovieId = movieIdValue,
                ThemeId = themeIdValue
            });

            // Assert
            modelDate.Should().Be(dateValue);
            modelStartTime.Should().Be(startTimeValue);
            modelDuration.Should().Be(durationValue);
            modelPrice.Should().Be(priceValue);
            modelLecturerId.Should().Be(lecturerIdValue);
            modelMovieId.Should().Be(movieIdValue);
            modelThemeId.Should().Be(themeIdValue);

            successMessage.Should().Be("Projection created successfully.");

            result.Should().BeOfType<RedirectToActionResult>();

            result.As<RedirectToActionResult>().ActionName.Should().Be(nameof(ProjectionsController.Index));
            //result.As<RedirectToActionResult>().ControllerName.Should().Be(nameof(ProjectionsController));
            //result.As<RedirectToActionResult>().RouteValues.Keys.Should().Contain("area");
            //result.As<RedirectToActionResult>().RouteValues.Values.Should().Contain("Admin");
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
