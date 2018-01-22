namespace CinemaWay.Services.Admin.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Models.Projections;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using static Services.ServicesConstants;

    public class AdminProjectionService : IAdminProjectionService
    {
        private readonly CinemaWayDbContext db;

        public AdminProjectionService(CinemaWayDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<AdminListProjectionsModel>> All(int page = 1)
            => await this.db
                .Projections
                .OrderByDescending(m => m.Date)
                .Skip((page - 1) * MainPageSize)
                .Take(MainPageSize)
                .ProjectTo<AdminListProjectionsModel>()
                .ToListAsync();

        public async Task<int> Total()
            => await this.db.Projections.CountAsync();

        public async Task Create(DateTime date, int startTime, int duration, decimal price, string lecturerId, int movieId, int themeId)
        {
            var projection = new Projection
            {
                Date = date,
                StartTime = startTime,
                Duration = duration,
                Price = price,
                LecturerId = lecturerId,
                MovieId = movieId,
                ThemeId = themeId
            };

            this.db.Add(projection);

            await this.db.SaveChangesAsync();
        }
    }
}
