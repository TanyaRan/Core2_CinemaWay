namespace CinemaWay.Services.Admin.Models.Actors
{
    using Common.Mapping;
    using Data.Models;

    public class AdminListActorsModel : IMapFrom<Actor>
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ImageUrl { get; set; }
    }
}
