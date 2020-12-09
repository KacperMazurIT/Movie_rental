using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieRental.Models
{
    public class ActorsInMovie
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Actor")]
        public string ActorName { get; set; }

        public int MovieId { get; set; }

    }
}