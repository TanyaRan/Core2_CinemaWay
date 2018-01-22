namespace CinemaWay.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Actor
    {
        public int Id { get; set; }

        [Required]
        [MinLength(NameMinLength)]
        [MaxLength(NameMaxLength)]
        public string FirstName { get; set; }

        [MinLength(NameMinLength)]
        [MaxLength(NameMaxLength)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(UrlMaxLength)]
        [Url]
        public string ImageUrl { get; set; }

        [MaxLength(DescriptionMaxLength)]
        public string Filmography { get; set; }

        public List<MovieActor> Movies { get; set; } = new List<MovieActor>();
    }
}
