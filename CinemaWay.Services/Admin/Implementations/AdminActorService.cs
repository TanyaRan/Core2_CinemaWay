namespace CinemaWay.Services.Admin.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Models.Actors;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using static Services.ServicesConstants;

    public class AdminActorService : IAdminActorService
    {
        private readonly CinemaWayDbContext db;

        public AdminActorService(CinemaWayDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<AdminListActorsModel>> All(int page = 1)
            => await this.db
            .Actors
            .OrderByDescending(a => a.Id)
            .Skip((page - 1) * MainPageSize)
            .Take(MainPageSize)
            .ProjectTo<AdminListActorsModel>()
            .ToListAsync();

        public async Task<int> Total()
            => await this.db.Actors.CountAsync();

        public async Task Create(string firstName, string lastName, string imageUrl, string filmography)
        {
            var actor = new Actor
            {
                FirstName = firstName,
                LastName = lastName,
                ImageUrl = imageUrl,
                Filmography = filmography
            };

            this.db.Add(actor);

            await this.db.SaveChangesAsync();
        }
    }
}
