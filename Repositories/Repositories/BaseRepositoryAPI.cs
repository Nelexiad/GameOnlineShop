using AutoMapper;
using Entities.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public abstract class BaseRepositoryAPI<TMODEL, TDTO> : IRepository<TDTO> where TMODEL : class where TDTO : class
    {
        private readonly IDbContext<ApplicationDbContext> _dbContext;
        private readonly ILogger<BaseRepositoryAPI<TMODEL,TDTO>> _logger;
        private readonly IMapper _mapper;

        public  BaseRepositoryAPI(IDbContext<ApplicationDbContext> dbContext, ILogger<BaseRepositoryAPI<TMODEL, TDTO>> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
        }

        public virtual async Task<TDTO> Create(TDTO entity)
        {
            try
            {
                var dbEntity = _mapper.Map<TDTO,TMODEL>(entity);
                _dbContext.Set<TMODEL>().Add(dbEntity);
                await _dbContext.SaveChangesAsync();
                return _mapper.Map<TDTO>(dbEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during creation");
                throw;
            }
        }

        public virtual async Task<bool> Delete(int id)
        {
            try
            {
                var dbEntity = await _dbContext.Set<TMODEL>().FindAsync(id);
                if (dbEntity != null)
                {
                    _dbContext.Set<TMODEL>().Remove(dbEntity);
                    await _dbContext.SaveChangesAsync();
                    
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}", ex);  
                return false;
            }
          
        }

        public virtual async Task<TDTO> Get(int id)
        {
            try
            {
                var dbEntity = await _dbContext.Set<TMODEL>().FindAsync(id);
                return _mapper.Map<TDTO>(dbEntity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.Message}");
                throw;
            }
        }

        public virtual async Task<IEnumerable<TDTO>> GetAll()
        {
            try
            {
                var dbEntities = await _dbContext.Set<TMODEL>().ToListAsync();
                return _mapper.Map<IEnumerable<TDTO>>(dbEntities);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
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

                var entityDTO = _mapper.Map<TDTO, TMODEL>(entity);
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
