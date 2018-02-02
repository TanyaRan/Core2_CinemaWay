namespace CinemaWay.Tests.Web.Controllers.UsersControllerTests
{
    using CinemaWay.Web.Controllers;
    using FluentAssertions;
    using Microsoft.AspNetCore.Authorization;
    using System.Linq;
    using Xunit;

    public class SubmitIdeasShould
    {
        [Fact]
        public void BeOnlyForAuthorizedUsers()
        {
            // Arrange 
            var method = typeof(UsersController)
                .GetMethod(nameof(UsersController.SubmitIdeas));

            // Act
            var attributes = method
                .GetCustomAttributes(true);

            // Assert
            attributes
                .Should()
                .Match(attr => attr.Any(a => a.GetType() == typeof(AuthorizeAttribute)));
        }
    }
}
