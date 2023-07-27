


using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Repositories
{
    public class VideogameRepository : BaseRepositories<Videogame>
    {
        public VideogameRepository(IDbContext<ApplicationDbContext> db, ILogger<Videogame> logger) : base(db, logger)
        {
        }
    }
}


