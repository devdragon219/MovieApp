using MovieAppBackend.Models;

namespace MovieAppBackend.Data
{
    public static class MovieSeeder
    {
        public static void SeedData(MovieContext context)
        {
            if (!context.Movies.Any())
            {
                context.Movies.AddRange(
                    new Movie { Title = "Inception", Description = "A mind-bending thriller", Genre = "Sci-Fi", CoverImage = "inception.jpg" },
                    new Movie { Title = "The Matrix", Description = "A hacker discovers reality", Genre = "Sci-Fi", CoverImage = "matrix.jpg" }
                );
                context.SaveChanges();
            }
        }
    }
}
