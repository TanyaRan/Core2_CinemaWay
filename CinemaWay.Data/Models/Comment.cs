namespace CinemaWay.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Comment
    {
        public int Id { get; set; }

        [Required]
        [MinLength(CommentMinLength)]
        [MaxLength(CommentMaxLength)]
        public string Content { get; set; }

        public DateTime PublishDate { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public User Author { get; set; }

        [Required]
        public int MovieId { get; set; }

        public Movie Movie { get; set; }
    }
}
