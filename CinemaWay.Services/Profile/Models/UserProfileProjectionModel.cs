namespace CinemaWay.Services.Profile.Models
{
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;
    using System;
    using System.Linq;

    public class UserProfileProjectionModel : IMapFrom<Projection>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Movie { get; set; }

        public Grade? Grade { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            string visitorId = null;
            mapper
            .CreateMap<Projection, UserProfileProjectionModel>()
            .ForMember(p => p.Movie, cfg => cfg.MapFrom(p => p.Movie.Name))
            .ForMember(p => p.Grade, cfg => cfg
                .MapFrom(p => p.Visitors
                    .Where(v => v.VisitorId == visitorId)
                    .Select(v => v.Grade)
                    .FirstOrDefault()));
        }
    }
}
