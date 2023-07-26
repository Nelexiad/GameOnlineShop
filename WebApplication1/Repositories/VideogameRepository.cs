

using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Repositories
{
    public class VideogameRepository : IRepository<Videogame>
    {

        private readonly ApplicationDbContext _db;
        private readonly ILogger<VideogameRepository> _logger;

        public VideogameRepository(ApplicationDbContext db, ILogger<VideogameRepository> logger)
        {
            _db = db;
            _logger = logger;
        }
        public async Task<Videogame> Get(int id )
        {
            try
            {
                
                var videogame = await _db.Videogames.FindAsync(id);
                if( videogame == null )
                {
                    throw new Exception($"nessun libro con l'ID {id}");
                }
                return videogame;
            }
            
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                throw;
            }

            

        }

        public async Task<IEnumerable<Videogame>> GetAll()
        {
            try
            {
                IEnumerable<Videogame> videogames = await _db.Videogames.ToListAsync();
                return videogames;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                throw;
            }
        }

        public async Task<Videogame> Create(Videogame entity)
        {
            try
            {
                _db.Videogames.Add(entity);
                await _db.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating a new videogame.");
                throw;
            }
        }

        public async Task<Videogame> Update(int id, Videogame entity)
        {
            try
            {
                var existingVideogame = await _db.Videogames.FindAsync(id);
                if (existingVideogame == null)
                {
                    throw new Exception($"Videogame with ID {id} not found.");
                }

                existingVideogame.Title = entity.Title;
                existingVideogame.GenreId = entity.GenreId;
                existingVideogame.Developer = entity.Developer;
                existingVideogame.Description = entity.Description;
                existingVideogame.Price = entity.Price;
                existingVideogame.CoverVideogame = entity.CoverVideogame;
                existingVideogame.ReleaseDate = entity.ReleaseDate;
                existingVideogame.GenreName = entity.GenreName;

                await _db.SaveChangesAsync();
                return existingVideogame;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating the videogame with ID {id}.");
                throw;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var videogame = await _db.Videogames.FindAsync(id);
                if (videogame == null)
                {
                    throw new Exception($"Videogame with ID {id} not found.");
                }

                _db.Videogames.Remove(videogame);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting the videogame with ID {id}.");
                return false;
            }
        }
    }
}


