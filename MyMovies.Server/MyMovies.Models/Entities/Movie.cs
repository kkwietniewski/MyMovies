using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyMovies.Models.Entities
{
    public class Movie
    {
        [Key]
        public Guid ID { get; set; }
        [Required, MaxLength(200)]
        public string Title { get; set; }
        [Range(1900, 2100)]
        public int? ProductionYear { get; set; }
    }
}
