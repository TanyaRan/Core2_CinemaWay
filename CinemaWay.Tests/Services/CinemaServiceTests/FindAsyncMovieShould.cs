namespace CinemaWay.Tests.Services.CinemaServiceTests
{
    using AutoMapper;
    using CinemaWay.Services.Cinema;
    using CinemaWay.Services.Cinema.Implementations;
    using CinemaWay.Web.Infrastructure.Mapping;
    using Data;
    using Data.Models;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class FindAsyncMovieShould
    {
        public FindAsyncMovieShould()
        {
            Tests.Initialize();
        }

        // [Fact]
        public async Task ReturnCorrectResult_WithFilterAndOrder()
        {
            // Arrange
            var db = GetDatabase();

            var pdfGenerator = new PdfGenerator();
            var one = new Movie { Name = "One" };
            var two = new Movie { Name = "Olle" };
            var three = new Movie { Name = "Three" };
            db.AddRange(one, two, three);

            var firstProjection = new Projection { Id = 1, Date = DateTime.Now.AddDays(1), Movie = new Movie { Id = 1, Name = "One" } };
            var secondProjection = new Projection { Id = 2, Date = DateTime.Now.AddDays(2), Movie = new Movie { Id = 3, Name = "Two" } };
            var thirdProjection = new Projection { Id = 3, Date = DateTime.Now.AddDays(3), Movie = new Movie { Id = 3, Name = "Three" } };

            db.AddRange(firstProjection, secondProjection, thirdProjection);
            await db.SaveChangesAsync();

            var cinemaService = new CinemaService(db, pdfGenerator);

            // Act
            var result = await cinemaService.FindAsyncMovie("o");

            // Assert
            result
                .Should()
                .Match(r => r.ElementAt(0).Id == 1 && r.ElementAt(1).Id == 2)
                .And
                .HaveCount(2);
        }

        private CinemaWayDbContext GetDatabase()
        {
            var dbOptions = new DbContextOptionsBuilder<CinemaWayDbContext>()
    .UseInMemoryDatabase(Guid.NewGuid().ToString())
    .Options;

            return new CinemaWayDbContext(dbOptions);
        }
    }
}
