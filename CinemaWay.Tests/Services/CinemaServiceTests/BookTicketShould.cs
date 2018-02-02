namespace CinemaWay.Tests.Services.CinemaServiceTests
{
    using CinemaWay.Services.Cinema.Implementations;
    using Data;
    using Data.Models;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;

    public class BookTicketShould
    {
        public BookTicketShould()
        {
            Tests.Initialize();
        }

        [Fact]
        public async Task SaveCorrectDataWithValidProjectionIdAndUserId()
        {
            // Arrange
            var db = this.GetDatabase();

            const int projectionId = 1;
            const string userId = "TestUserId";

            var projection = new Projection
            {
                Id = projectionId,
                Date = DateTime.MaxValue,
                Visitors = new List<UserProjections>()
            };

            db.Add(projection);
            await db.SaveChangesAsync();

            var pdfGenerator = new PdfGenerator();
            var cinemaService = new CinemaService(db, pdfGenerator);

            // Act
            var result = await cinemaService.BookTicket(projectionId, userId);
            var savedEntry = db.Find<UserProjections>(projectionId, userId);

            // Assert
            result
                .Should()
                .Be(true);

            savedEntry
                .Should()
                .NotBeNull();
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
