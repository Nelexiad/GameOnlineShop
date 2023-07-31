using AutoMapper;
using WebApplication1.Models;
using WebApplication1.Models.DTO;
using WebApplication1.Models.MappedModel;

namespace WebApplication1.MappingProfiles
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
