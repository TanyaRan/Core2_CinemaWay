namespace CinemaWay.Services.Admin.Models.Movies
{
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;
    using System;

    public class AdminListMoviesModel : IMapFrom<Movie>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string Director { get; set; }

        public string Genre { get; set; }

        public void ConfigureMapping(Profile mapper)
            => mapper
            .CreateMap<Movie, AdminListMoviesModel>()
            .ForMember(m => m.Genre, cfg => cfg.MapFrom(m => Enum.GetName(typeof(Genre), m.Genre)));
    }
}
