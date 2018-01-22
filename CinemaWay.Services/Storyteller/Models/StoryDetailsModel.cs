namespace CinemaWay.Services.Storyteller.Models
{
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;
    using System;

    public class StoryDetailsModel : IMapFrom<Story>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string ImageUrl { get; set; }

        public string ReviewOf { get; set; }

        public DateTime PublishDate { get; set; }

        public string Author { get; set; }

        public void ConfigureMapping(Profile mapper)
            => mapper
            .CreateMap<Story, StoryDetailsModel>()
            .ForMember(s => s.Author, cfg => cfg.MapFrom(s => s.Author.UserName))
            .ForMember(s => s.ReviewOf, cfg => cfg.MapFrom(s => Enum.GetName(typeof(ReviewOf), s.ReviewOf)));
    }
}
