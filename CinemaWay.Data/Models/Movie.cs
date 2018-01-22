namespace CinemaWay.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [MinLength(NameMinLength)]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        [MaxLength(UrlMaxLength)]
        [Url]
        public string ImageUrl { get; set; }

        [Required]
        [MinLength(NameMinLength)]
        [MaxLength(NameMaxLength)]
        public string Director { get; set; }

        public Genre Genre { get; set; }

        public List<MovieActor> Actors { get; set; } = new List<MovieActor>();

        public List<Projection> Projections { get; set; } = new List<Projection>();

        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
