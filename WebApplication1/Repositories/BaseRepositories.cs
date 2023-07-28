using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.MappedModel;

namespace WebApplication1.Repositories
{
    public abstract class BaseRepositories<TMODEL,TDTO> : IRepository<TDTO> where TMODEL:class  where TDTO : class
    {
        private readonly  IDbContext<ApplicationDbContext> _dbContext;
        private readonly ILogger<TDTO> _logger;
        private readonly IMapper _mapper;

        public BaseRepositories(IDbContext<ApplicationDbContext> db, ILogger<TDTO> logger,IMapper mapper)
        {
            _dbContext = db;
            _logger = logger;
            _mapper = mapper;
        }
        public virtual async Task<TDTO> Create(TDTO entity)
        {
            try
            {
                var entityDTO = _mapper.Map<TDTO,TMODEL>(entity);
                _dbContext.Set<TMODEL>().Add(entityDTO);
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
                var entity = await _dbContext.Set<TMODEL>().FindAsync(id); // Recupera il modello di dominio dal database
                if (entity == null)
                {
                    throw new Exception($"Element with ID: {id} not found");
                }

                var dto = _mapper.Map<TMODEL, TDTO>(entity); // Mappa il modello di dominio al DTO
                _dbContext.Set<TMODEL>().Remove(entity); // Rimuove il modello di dominio dal contesto
                await _dbContext.SaveChangesAsync(); // Effettua la cancellazione nel database
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting the element with ID {id}.");
                return false;
            }
        }

        public virtual async Task<TDTO> Get(int id)
        {
            try
            {
                var entityDTO = await _dbContext.Set<TMODEL>().FindAsync(id);
                if (entityDTO == null)
                {
                    throw new Exception($"nessun elemento con l'ID {id}");
                }
                var entity = _mapper.Map<TMODEL,TDTO>(entityDTO);
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                throw;
            }
        }

        public virtual async Task<IEnumerable<TDTO>> GetAll()
        {
            try
            {
                var entities = await _dbContext.Set<TMODEL>().ToListAsync(); // Recupera i modelli di dominio dal database
                var dtos = _mapper.Map<IEnumerable<TMODEL>, IEnumerable<TDTO>>(entities); // Mappa i modelli di dominio ai DTO
                return dtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                throw;
            }
        }

        public virtual async Task<TDTO> Update(int id, TDTO entity)
        {
            try
            {
                var existingEntity = await _dbContext.Set<TMODEL>().FindAsync(id);
              
                if (existingEntity == null)
                {
                    throw new Exception($"Element with ID {id} not found.");
                }

                var entityDTO = _mapper.Map<TDTO,TMODEL>(entity);
                _dbContext.Set<TMODEL>().Entry(existingEntity).CurrentValues.SetValues(entityDTO);

                await _dbContext.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating the Element with ID {id}.");
                throw;
            }
        }

    }
}

