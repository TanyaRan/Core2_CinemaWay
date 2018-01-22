namespace CinemaWay.Services.Profile.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System.Linq;
    using System.Threading.Tasks;

    public class UserService : IUserService
    {
        private readonly CinemaWayDbContext db;

        public UserService(CinemaWayDbContext db)
        {
            this.db = db;
        }

        public async Task<bool> AddGrade(int projectionId, string visitorId, Grade grade)
        {
            var userWithTicket = await this.db
                .FindAsync<UserProjections>(projectionId, visitorId);

            if (userWithTicket == null)
            {
                return false;
            }

            userWithTicket.Grade = grade;

            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<UserProfileModel> ProfileAsync(string id)
            => await this.db
            .Users
            .Where(u => u.Id == id)
            .ProjectTo<UserProfileModel>(new { visitorId = id })
            .FirstOrDefaultAsync();
    }
}
