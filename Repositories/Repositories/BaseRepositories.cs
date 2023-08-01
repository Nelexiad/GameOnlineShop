using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Entities.Models.DTO;
using Entities.Data;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace Repositories.Repositories
{
    public abstract class BaseRepositories<TMODEL,TDTO> : IRepositoryMVC<TDTO> where TMODEL:class  where TDTO : class
    {
        
        private readonly ILogger<TDTO> _logger;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;

        public BaseRepositories( ILogger<TDTO> logger,IMapper mapper, HttpClient httpClient)
        {
            
            _logger = logger;
            _mapper = mapper;
            _httpClient = httpClient;
        }
        public virtual async Task<TDTO> Create(TDTO entity)
        {
            try
            {
                var dbEntity= await _httpClient.PostAsJsonAsync("",entity).Result.Content.ReadFromJsonAsync<TMODEL>();
                
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
                await _httpClient.DeleteAsync($"{id}");
           
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
                var dbEntity = await _httpClient.GetFromJsonAsync<TMODEL>($"{id}");
                var DTOentity= _mapper.Map<TDTO>(dbEntity);
                return DTOentity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                throw;
            }
        }

        public virtual async Task<IEnumerable<TDTO>> GetAll(string url)
        {
            try
            {
                
                var entities = await _httpClient.GetFromJsonAsync<IEnumerable<TMODEL>>(url);
                var dtos = _mapper.Map<IEnumerable<TDTO>>(entities); // Mappa i modelli di dominio ai DTO
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
                var existingEntity = await _httpClient.PutAsJsonAsync($"{id}",entity).Result.Content.ReadFromJsonAsync<TMODEL>();
                var modifiedDTO= _mapper.Map<TDTO>(existingEntity);
                return modifiedDTO;
              
                if (existingEntity == null)
                {
                    throw new Exception($"Element with ID {id} not found.");
                }

               
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating the Element with ID {id}.");
                throw;
            }
        }

    }
}

