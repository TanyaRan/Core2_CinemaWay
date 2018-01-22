namespace CinemaWay.Services.Profile
{
    using Data.Models;
    using Models;
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task<UserProfileModel> ProfileAsync(string username);

        Task<bool> AddGrade(int projectionId, string visitorId, Grade grade);
    }
}
