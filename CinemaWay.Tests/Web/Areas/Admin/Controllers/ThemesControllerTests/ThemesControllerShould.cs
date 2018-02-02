namespace CinemaWay.Tests.Web.Areas.Admin.Controllers.ThemesControllerTests
{
    using CinemaWay.Web;
    using CinemaWay.Web.Areas.Admin.Controllers;
    using FluentAssertions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using Xunit;

    public class ThemesControllerShould
    {
        [Fact]
        public void BeInAdminArea()
        {
            // Arrange
            var controller = typeof(ThemesController);

            // Act
            var areaAttribute = controller
                .GetCustomAttributes(true)
                .FirstOrDefault(a => a.GetType() == typeof(AreaAttribute)) as AreaAttribute;

            // Assert
            areaAttribute
                .Should().NotBeNull();

            areaAttribute.RouteValue.Should().Be(WebConstants.AdminArea);
        }

        [Fact]
        public void BeOnlyForAdminUsers()
        {
            // Arrange
            var controller = typeof(ThemesController);

            // Act
            var authorizeAttribute = controller
                .GetCustomAttributes(true)
                .FirstOrDefault(a => a.GetType() == typeof(AuthorizeAttribute)) as AuthorizeAttribute;

            // Assert
            authorizeAttribute.Should().NotBeNull();
            authorizeAttribute.Roles.Should().Be(WebConstants.AdministratorRole);
        }
    }
}
