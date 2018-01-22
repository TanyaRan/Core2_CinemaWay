namespace CinemaWay.Web.Areas.Admin.Models.Themes
{
    using Services.Admin.Models.Themes;
    using System;
    using System.Collections.Generic;

    using static Services.ServicesConstants;

    public class AdminListThemesViewModel
    {
        public IEnumerable<AdminListThemesModel> Themes { get; set; }

        public int TotalThemes { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)this.TotalThemes / MainPageSize);

        public int CurrentPage { get; set; }

        public int PreviousPage => this.CurrentPage <= 1 ? 1 : this.CurrentPage - 1;

        public int NextPage
            => this.CurrentPage == this.TotalPages
                ? this.TotalPages
                : this.CurrentPage + 1;
    }
}
