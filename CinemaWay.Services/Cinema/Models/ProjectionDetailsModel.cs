namespace CinemaWay.Services.Cinema.Models
{
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;
    using System;

    public class ProjectionDetailsModel : IMapFrom<Projection>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int StartTime { get; set; }

        public int Duration { get; set; }

        public decimal Price { get; set; }

        public string Movie { get; set; }

        public string Description { get; set; }

        public string MovieImage { get; set; }

        public string Theme { get; set; }

        public string Lecturer { get; set; }

        public int Visitors { get; set; }

        public void ConfigureMapping(Profile mapper)
            => mapper
            .CreateMap<Projection, ProjectionDetailsModel>()
            .ForMember(p => p.Movie, cfg => cfg.MapFrom(p => p.Movie.Name))
            .ForMember(p => p.Description, cfg => cfg.MapFrom(p => p.Movie.Description))
            .ForMember(p => p.MovieImage, cfg => cfg.MapFrom(p => p.Movie.ImageUrl))
            .ForMember(p => p.Theme, cfg => cfg.MapFrom(p => p.Theme.Title))
            .ForMember(p => p.Lecturer, cfg => cfg.MapFrom(p => p.Lecturer.UserName))
            .ForMember(p => p.Visitors, cfg => cfg.MapFrom(p => p.Visitors.Count));
    }
}
