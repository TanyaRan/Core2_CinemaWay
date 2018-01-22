namespace CinemaWay.Services.Admin.Models.Movies
{
    using Common.Mapping;
    using Data.Models;

    public class ShortMovieModel : IMapFrom<Movie>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
