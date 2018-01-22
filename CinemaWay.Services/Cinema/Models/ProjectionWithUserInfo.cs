namespace CinemaWay.Services.Cinema.Models
{
    using System;

    public class ProjectionWithUserInfo
    {
        public DateTime Date { get; set; }

        public bool UserHasATicket { get; set; }

        public int VisitorsCount { get; set; }
    }
}
