namespace CinemaWay.Web.Models.Cinema
{
    using Services.Cinema.Models;
    using System.Collections.Generic;

    public class SearchViewModel
    {
        public IEnumerable<ListProjectionWithMovieModel> Projections { get; set; }
            = new List<ListProjectionWithMovieModel>();

        public string SearchText { get; set; }
    }
}
