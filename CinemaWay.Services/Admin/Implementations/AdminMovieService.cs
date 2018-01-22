namespace CinemaWay.Services.Admin.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Models.Movies;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using static Services.ServicesConstants;

    public class AdminMovieService : IAdminMovieService
    {
        private readonly CinemaWayDbContext db;

        public AdminMovieService(CinemaWayDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<AdminListMoviesModel>> All(int page = 1)
            => await this.db
            .Movies
            .OrderByDescending(m => m.Id)
            .Skip((page - 1) * MainPageSize)
            .Take(MainPageSize)
            .ProjectTo<AdminListMoviesModel>()
            .ToListAsync();

        public async Task<int> Total()
            => await this.db.Movies.CountAsync();

        public async Task<IEnumerable<ShortMovieModel>> AllTitles()
            => await this.db
            .Movies
            .ProjectTo<ShortMovieModel>()
            .ToListAsync();

        public async Task Create(string name, string description, string imageUrl, string director, Genre genre)
        {
            var movie = new Movie
            {
                Name = name,
                Description = description,
                ImageUrl = imageUrl,
                Director = director,
                Genre = genre
            };

            this.db.Add(movie);

            await this.db.SaveChangesAsync();
        }
    }
}
