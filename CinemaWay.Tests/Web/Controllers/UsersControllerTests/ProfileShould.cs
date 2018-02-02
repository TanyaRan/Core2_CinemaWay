namespace CinemaWay.Tests.Web.Controllers.UsersControllerTests
{
    using CinemaWay.Data.Models;
    using CinemaWay.Services.Cinema;
    using CinemaWay.Services.Profile;
    using CinemaWay.Services.Profile.Models;
    using CinemaWay.Web.Controllers;
    using FluentAssertions;
    using Microsoft.AspNetCore.Mvc;
    using Mocks;
    using Moq;
    using System.Threading.Tasks;
    using Xunit;

    public class ProfileShould
    {
        [Fact]
        public async Task ReturnNotFound_WhenInvalidUsername()
        {
            // Arrange
            var userManager = UserManagerMock.New;

            var controller = new UsersController(null, userManager.Object, null);

            // Act 
            var result = await controller.Profile("Username");

            // Assert
            result
                .Should()
                .BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task ReturnViewWithCorrectModel_WhenValidUsername()
        {
            // Arrange
            const string userId = "TestId";
            const string username = "TestUsername";

            var userManager = UserManagerMock.New;
            userManager
                .Setup(u => u.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(new User { Id = userId, UserName = username });

            var userService = new Mock<IUserService>();
            userService
                .Setup(u => u.ProfileAsync(It.Is<string>(id => id == userId)))
                .ReturnsAsync(new UserProfileModel { UserName = username });

            var cinemaServive = new Mock<ICinemaService>();

            var controller = new UsersController(userService.Object, userManager.Object, cinemaServive.Object);

            // Act
            var result = await controller.Profile(username);

            // Assert
            result
                .Should()
                .BeOfType<ViewResult>()
                .Subject
                .Model
                .Should()
                .Match(m => m.As<UserProfileModel>().UserName == username);
        }
    }
}
