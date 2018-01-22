namespace CinemaWay.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Projection
    {
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [Range(0, 23)]
        public int StartTime { get; set; }

        [Range(100, 180)]
        public int Duration { get; set; }

        [Range(5, 45)]
        public decimal Price { get; set; }

        [Required]
        public int MovieId { get; set; }

        public Movie Movie { get; set; }

        public int ThemeId { get; set; }

        public Theme Theme { get; set; }

        public string LecturerId { get; set; }

        public User Lecturer { get; set; }

        public List<UserProjections> Visitors { get; set; } = new List<UserProjections>();
    }
}
