namespace CinemaWay.Web.Models.Cinema
{
    using CinemaWay.Services.Cinema.Models;

    public class ProjectionDetailsViewModel
    {
        public ProjectionDetailsModel Projection { get; set; }

        public bool UserBookedATicket { get; set; }
    }
}
