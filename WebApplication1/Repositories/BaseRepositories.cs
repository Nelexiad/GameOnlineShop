using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Repositories
{
    public abstract class BaseRepositories<T> : IRepository<T> where T : class
    {
        private readonly  IDbContext<ApplicationDbContext> _dbContext;
        private readonly ILogger<T> _logger;

        public BaseRepositories(IDbContext<ApplicationDbContext> db, ILogger<T> logger)
        {
            _dbContext = db;
            _logger = logger;
        }
        public virtual async Task<T> Create(T entity)
        {
            try
            {
                 _dbContext.Set<T>().Add(entity);
                await _dbContext.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating a new videogame.");
                throw;
            }
        }

        public virtual async Task<bool> Delete(int id)
        {
            try
            {
                var entity = await _dbContext.Set<T>().FindAsync(id);
                if (entity == null)
                {
                    throw new Exception($"Element with ID: {id} not found");
                }

                _dbContext.Set<T>().Remove(entity);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting the element with ID {id}.");
                return false;
            }
        }

        public virtual async Task<T> Get(int id)
        {
            try
            {

                var entity = await _dbContext.Set<T>().FindAsync(id);
                if (entity == null)
                {
                    throw new Exception($"nessun elemento con l'ID {id}");
                }
                return entity;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                throw;
            }
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            try
            {
                IEnumerable<T> entities = await _dbContext.Set<T>().ToListAsync();
                return entities;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                throw;
            }
        }

        public virtual async Task<T> Update(int id, T entity)
        {
            try
            {
                var existingEntity = await _dbContext.Set<T>().FindAsync(id);
                if (existingEntity == null)
                {
                    throw new Exception($"Element with ID {id} not found.");
                }

                // Utilizza il metodo Entry per ottenere l'entità collegata all'oggetto di tracciamento.
                // Poi copia le proprietà da "entity" a "existingEntity".
                _dbContext.Set<T>().Entry(existingEntity).CurrentValues.SetValues(entity);

                await _dbContext.SaveChangesAsync();
                return existingEntity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating the Element with ID {id}.");
                throw;
            }
        }

    }
}

