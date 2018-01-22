namespace CinemaWay.Services.Admin.Models.Themes
{
    using Common.Mapping;
    using Data.Models;

    public class ShortThemeModel : IMapFrom<Theme>
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }
}
