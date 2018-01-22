namespace CinemaWay.Services.Admin.Models
{
    using Common.Mapping;
    using Data.Models;

    public class AdminListUsersModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }
    }
}
