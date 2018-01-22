namespace CinemaWay.Web.Models.Cinema
{
    using System.ComponentModel.DataAnnotations;

    public class SearchFormModel
    {
        public string SearchText { get; set; }

        [Display(Name = "Search in Movies")]
        public bool SearchInMovies { get; set; } = true;

        [Display(Name = "Search in Themes")]
        public bool SearchInThemes { get; set; } = false;
    }
}
