namespace CinemaWay.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class UserProjections
    {
        public int ProjectionId { get; set; }

        public Projection Projection { get; set; }

        public string VisitorId { get; set; }

        public User Visitor { get; set; }

        public Grade? Grade { get; set; }

        [MaxLength(DataConstants.IdeasSubmissionMaxLength)]
        public byte[] IdeasSubmission { get; set; }
    }
}
