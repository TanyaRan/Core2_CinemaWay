namespace CinemaWay.Services.Cinema.Models
{
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;
    using System;

    public class ListProjectionWithMovieModel : IMapFrom<Projection>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int StartTime { get; set; }

        public string Movie { get; set; }

        public string MovieImage { get; set; }

        public string Theme { get; set; }

        public void ConfigureMapping(Profile mapper)
            => mapper
            .CreateMap<Projection, ListProjectionWithMovieModel>()
            .ForMember(p => p.Movie, cfg => cfg.MapFrom(p => p.Movie.Name))
            .ForMember(p => p.MovieImage, cfg => cfg.MapFrom(p => p.Movie.ImageUrl))
            .ForMember(p => p.Theme, cfg => cfg.MapFrom(p => p.Theme.Title));
    }
}
