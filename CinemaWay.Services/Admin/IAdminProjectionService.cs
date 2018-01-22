namespace CinemaWay.Services.Admin
{
    using Models.Projections;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAdminProjectionService
    {
        Task Create(
            DateTime date,
            int startTime,
            int duration,
            decimal price,
            string lecturerId,
            int movieId,
            int themeId);

        Task<IEnumerable<AdminListProjectionsModel>> All(int page = 1);

        Task<int> Total();
    }
}
