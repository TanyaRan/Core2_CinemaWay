namespace CinemaWay.Data.Models
{
    public class UserProjections
    {
        public int ProjectionId { get; set; }

        public Projection Projection { get; set; }

        public string VisitorId { get; set; }

        public User Visitor { get; set; }

        public Grade? Grade { get; set; }
    }
}
