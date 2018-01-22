namespace CinemaWay.Services.Admin.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Models.Themes;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using static ServicesConstants;

    public class AdminThemeService : IAdminThemeService
    {
        private readonly CinemaWayDbContext db;

        public AdminThemeService(CinemaWayDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<AdminListThemesModel>> All(int page = 1)
            => await this.db
            .Themes
            .OrderByDescending(t => t.StartDate)
            .Skip((page - 1) * MainPageSize)
            .Take(MainPageSize)
            .ProjectTo<AdminListThemesModel>()
            .ToListAsync();

        public async Task<int> Total()
            => await this.db.Themes.CountAsync();

        public async Task<IEnumerable<ShortThemeModel>> AllTitles()
            => await this.db
            .Themes
            .ProjectTo<ShortThemeModel>()
            .ToListAsync();

        public async Task Create(string title, DateTime startDate, DateTime endDate)
        {
            var theme = new Theme
            {
                Title = title,
                StartDate = startDate,
                EndDate = endDate
            };

            this.db.Add(theme);

            await this.db.SaveChangesAsync();
        }
    }
}
