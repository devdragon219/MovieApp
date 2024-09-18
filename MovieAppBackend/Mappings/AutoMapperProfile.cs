using AutoMapper;
using MovieAppBackend.Models;
using MovieAppBackend.DTOs;

namespace MovieAppBackend.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Movie, MovieDTO>().ReverseMap();
        }
    }
}
