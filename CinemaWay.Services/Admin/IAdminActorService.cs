namespace CinemaWay.Services.Admin
{
    using Models.Actors;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAdminActorService
    {
        Task Create(
            string firstName,
            string lastName,
            string imageUrl,
            string filmography);

        Task<IEnumerable<AdminListActorsModel>> All(int page = 1);

        Task<int> Total();
    }
}
