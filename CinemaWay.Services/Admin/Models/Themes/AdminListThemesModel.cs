namespace CinemaWay.Services.Admin.Models.Themes
{
    using Common.Mapping;
    using Data.Models;
    using System;

    public class AdminListThemesModel : IMapFrom<Theme>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
