namespace CinemaWay.Web.Areas.Admin.Models.Actors
{
    using Services.Admin.Models.Actors;
    using System;
    using System.Collections.Generic;

    using static Services.ServicesConstants;

    public class AdminListActorsViewModel
    {
        public IEnumerable<AdminListActorsModel> Actors { get; set; }

        public int TotalActors { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)this.TotalActors / MainPageSize);

        public int CurrentPage { get; set; }

        public int PreviousPage => this.CurrentPage <= 1 ? 1 : this.CurrentPage - 1;

        public int NextPage
            => this.CurrentPage == this.TotalPages
                ? this.TotalPages
                : this.CurrentPage + 1;
    }
}
