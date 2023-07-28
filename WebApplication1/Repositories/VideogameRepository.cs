


using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.MappedModel;

namespace WebApplication1.Repositories
{
    public class VideogameRepository : BaseRepositories<Videogame,VideogameDTO>
    {
        public VideogameRepository(IDbContext<ApplicationDbContext> db, ILogger<VideogameDTO> logger, IMapper mapper) : base(db, logger,mapper)
        {
        }
    }
}


