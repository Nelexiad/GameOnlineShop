using AutoMapper;
using Entities.Models;
using Entities.Models.DTO;

namespace Entities.Models.DTO
{
    public class VideogameProfile : Profile
    {
        public VideogameProfile()
        {
            CreateMap<Videogame, VideogameDTO>();
            CreateMap<VideogameDTO, Videogame>();
            CreateMap<Genre, GenreDTO>();
            CreateMap<GenreDTO, Genre>();
            

        }
    }
}
