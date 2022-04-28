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
        [Column(TypeName = "decimal(4,0)"), MinLength(1900), MaxLength(2100)]
        public decimal? ProductionYear { get; set; }
    }
}
