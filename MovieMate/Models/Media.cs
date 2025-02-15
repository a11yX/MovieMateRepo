using Microsoft.AspNetCore.Mvc.Formatters;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace MovieMate.Models
{
    public enum MediaType // Enum for Movie or Series
    {
        Movie, 
        Series
    }
    public class Media
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;

        [Required]
        public MediaType Type { get; set; }

        public byte[]? Cover { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Director { get; set; } = string.Empty;

        [Required]
        public int Year { get; set; }
        public string Genre { get; set; } = string.Empty;
        public int? Duration { get; set; }
        public int? Seasons { get; set; }
        public int? Episodes { get; set; }

        [Required]
        [Range(0.0, 10.0)]
        public double Rating { get; set; }

        public ICollection<PlanToWatch> PlanToWatch { get; set; } = null!;
        public ICollection<Watching> Watching { get; set; } = null!;
        public ICollection<Watched> Watched { get; set; } = null!;
        public ICollection<Favourite> Favourites { get; set; } = null!;
    }
}