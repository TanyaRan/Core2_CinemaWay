namespace CinemaWay.Web.Areas.Admin.Models.Actors
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class AddActorFormModel
    {
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
    }
}
