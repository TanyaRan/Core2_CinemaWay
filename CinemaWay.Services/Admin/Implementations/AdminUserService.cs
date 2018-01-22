namespace CinemaWay.Services.Admin.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class AdminUserService : IAdminUserService
    {
        private readonly CinemaWayDbContext db;

        public AdminUserService(CinemaWayDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<AdminListUsersModel>> All()
            => await this.db
            .Users
            .ProjectTo<AdminListUsersModel>()
            .ToListAsync();
    }
}
