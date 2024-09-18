using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace MovieAppBackend.DTOs
{
    public class MovieDTO
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string Genre { get; set; } = string.Empty;

        public IFormFile CoverImage { get; set; } = null!;

        public IFormFile MovieFile { get; set; } = null!;  // Initialize with non-nullable default
    }
}
