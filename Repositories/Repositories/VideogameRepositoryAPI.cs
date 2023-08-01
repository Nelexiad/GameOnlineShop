using AutoMapper;
using Entities.Data;
using Entities.Models;
using Entities.Models.DTO;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class VideogameRepositoryAPI : BaseRepositoryAPI<Videogame, VideogameDTO>
    {
        public VideogameRepositoryAPI(IDbContext<ApplicationDbContext> dbContext, ILogger<BaseRepositoryAPI<Videogame, VideogameDTO>> logger, IMapper mapper) : base(dbContext, logger, mapper)
        {
        }
    }
}
