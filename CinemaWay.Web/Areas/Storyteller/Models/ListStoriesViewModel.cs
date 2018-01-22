namespace CinemaWay.Web.Areas.Storyteller.Models
{
    using Services.Storyteller.Models;
    using System;
    using System.Collections.Generic;

    using static Services.ServicesConstants;

    public class ListStoriesViewModel
    {
        public IEnumerable<ListStoriesModel> Stories { get; set; }

        public int TotalStories { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)this.TotalStories / StoriesPageSize);

        public int CurrentPage { get; set; }

        public int PreviousPage => this.CurrentPage <= 1 ? 1 : this.CurrentPage - 1;

        public int NextPage
            => this.CurrentPage == this.TotalPages
                ? this.TotalPages
                : this.CurrentPage + 1;
    }
}
