namespace MovieAppBackend.Models
{
    public class Movie
    {
        public int Id { get; set; }

        // Initialize properties with default values
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public string CoverImage { get; set; } = string.Empty;
        public string MovieFilePath { get; set; } = string.Empty;  // Path to the movie file on S3
    }
}
