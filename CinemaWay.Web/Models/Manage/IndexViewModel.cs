namespace CinemaWay.Web.Models.Manage
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class IndexViewModel
    {
        public string Username { get; set; }

        [Required]
        [MinLength(NameMinLength)]
        [MaxLength(NameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(NameMinLength)]
        [MaxLength(NameMaxLength)]
        public string LastName { get; set; }

        [Required]
        [RegularExpression(@"\+\d{10,12}")]
        [Display(Name = "Phone number")]
        public string Phone { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string StatusMessage { get; set; }
    }
}
