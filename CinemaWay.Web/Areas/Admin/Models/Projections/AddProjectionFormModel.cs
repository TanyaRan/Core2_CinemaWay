namespace CinemaWay.Web.Areas.Admin.Models.Projections
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AddProjectionFormModel
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        [Range(0, 23)]
        public int StartTime { get; set; }

        [Range(100, 180)]
        public int Duration { get; set; }

        [Range(5, 45)]
        public decimal Price { get; set; }

        [Display(Name = "Lecturer")]
        [Required]
        public string LecturerId { get; set; }

        public IEnumerable<SelectListItem> Lecturers { get; set; }

        [Display(Name = "Movie")]
        [Required]
        public int MovieId { get; set; }

        public IEnumerable<SelectListItem> Movies { get; set; }

        [Display(Name = "Theme")]
        [Required]
        public int ThemeId { get; set; }

        public IEnumerable<SelectListItem> Themes { get; set; }
    }
}
