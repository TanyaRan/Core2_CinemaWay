namespace CinemaWay.Web.Areas.Admin.Models.Movies
{
    using Services.Admin.Models.Movies;
    using System;
    using System.Collections.Generic;

    using static Services.ServicesConstants;

    public class AdminListMoviesViewModel
    {
        public IEnumerable<AdminListMoviesModel> Movies { get; set; }

        public int TotalMovies { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)this.TotalMovies / MainPageSize);

        public int CurrentPage { get; set; }

        public int PreviousPage => this.CurrentPage <= 1 ? 1 : this.CurrentPage - 1;

        public int NextPage
            => this.CurrentPage == this.TotalPages
                ? this.TotalPages
                : this.CurrentPage + 1;
    }
}
