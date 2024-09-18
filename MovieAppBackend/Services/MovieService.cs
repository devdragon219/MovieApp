using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MovieAppBackend.Models;
using MovieAppBackend.Repositories;
using MovieAppBackend.DTOs;
using System.IO;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.FileProviders;

namespace MovieAppBackend.Services
{
    public class MovieService
    {
        private readonly MovieRepository _movieRepository;
        private readonly string _storagePath;

        public MovieService(MovieRepository movieRepository, IConfiguration configuration)
        {
            _movieRepository = movieRepository;
            var localStoragePath = configuration["LocalStoragePath"] ?? "MovieFiles";
            _storagePath = Path.Combine(Directory.GetCurrentDirectory(), localStoragePath);
            Directory.CreateDirectory(_storagePath);  // Ensure the directory exists
        }

        public IEnumerable<Movie> GetAllMovies()
        {
            return _movieRepository.GetAllMovies();
        }

        public async Task<Movie> CreateMovie(MovieDTO movieDTO)
        {
            var movie = new Movie
            {
                Title = movieDTO.Title,
                Description = movieDTO.Description,
                Genre = movieDTO.Genre,
            };

            if (movieDTO.CoverImage != null)
            {
                var coverImageName = $"{Guid.NewGuid()}_{movieDTO.CoverImage.FileName}";
                var coverImagePath = Path.Combine(_storagePath, coverImageName);

                using (var stream = new FileStream(coverImagePath, FileMode.Create))
                {
                    await movieDTO.CoverImage.CopyToAsync(stream);
                }

                // Store the relative URL in the database
                movie.CoverImage = $"/MovieFiles/{coverImageName}";
            }

            if (movieDTO.MovieFile != null)
            {
                var fileName = $"{Guid.NewGuid()}_{movieDTO.MovieFile.FileName}";
                var filePath = Path.Combine(_storagePath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await movieDTO.MovieFile.CopyToAsync(stream);
                }

                // Store the relative URL in the database
                movie.MovieFilePath = $"/MovieFiles/{fileName}";
            }

            _movieRepository.AddMovie(movie);
            return movie;
        }
    }
}
