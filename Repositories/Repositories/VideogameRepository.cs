


using AutoMapper;
using Entities.Data;
using Entities.Models;
using Entities.Models.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Repositories.Repositories
{
    public class VideogameRepository : BaseRepositories<Videogame,VideogameDTO>
    {
        public VideogameRepository( ILogger<VideogameDTO> logger, IMapper mapper,IHttpClientFactory httpClient) : base( logger,mapper,httpClient)
        {
        }
    }
}


