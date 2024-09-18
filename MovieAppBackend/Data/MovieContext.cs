using Microsoft.EntityFrameworkCore;
using MovieAppBackend.Models;

namespace MovieAppBackend.Data
{
    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> options) : base(options) { }

        public DbSet<Movie> Movies { get; set; }
    }
}
