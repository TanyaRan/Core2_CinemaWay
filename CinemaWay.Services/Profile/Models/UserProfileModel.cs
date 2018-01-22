namespace CinemaWay.Services.Profile.Models
{
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class UserProfileModel : IMapFrom<User>, IHaveCustomMapping
    {
        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public IEnumerable<UserProfileProjectionModel> Tickets { get; set; }

        public void ConfigureMapping(Profile mapper)
            => mapper
            .CreateMap<User, UserProfileModel>()
            .ForMember(u => u.Tickets, cfg => cfg.MapFrom(v => v.Tickets.Select(t => t.Projection)));
    }
}
