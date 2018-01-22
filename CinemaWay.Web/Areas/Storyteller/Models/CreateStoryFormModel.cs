namespace CinemaWay.Web.Areas.Storyteller.Models
{
    using Data.Models;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class CreateStoryFormModel
    {
        [Required]
        [MinLength(TitleMinLength)]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Content { get; set; }

        [Required]
        [MaxLength(UrlMaxLength)]
        [Url]
        public string ImageUrl { get; set; }

        [Display(Name = "Review Of")]
        public ReviewOf ReviewOf { get; set; }
    }
}
