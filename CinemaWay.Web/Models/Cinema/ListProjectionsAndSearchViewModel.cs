namespace CinemaWay.Web.Models.Cinema
{
    using Services.Cinema.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Services.ServicesConstants;

    public class ListProjectionsAndSearchViewModel : SearchFormModel
    {
        public IEnumerable<ListProjectionWithMovieModel> Projections { get; set; }

        public int TotalProjections { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)this.TotalProjections / AllProjectionsPageSize);

        public int CurrentPage { get; set; }

        public int PreviousPage => this.CurrentPage <= 1 ? 1 : this.CurrentPage - 1;

        public int NextPage
            => this.CurrentPage == this.TotalPages
                ? this.TotalPages
                : this.CurrentPage + 1;

    }
}
