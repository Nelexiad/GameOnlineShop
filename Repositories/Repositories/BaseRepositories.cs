using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Polly;
using Polly.Retry;

namespace Repositories.Repositories
{
    public abstract class BaseRepositories<TMODEL, TDTO> : IRepositoryMVC<TDTO> where TMODEL : class where TDTO : class
    {
        private readonly ILogger<TDTO> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _httpClient;
       

        public BaseRepositories(ILogger<TDTO> logger, IMapper mapper, IHttpClientFactory httpClient)
        {
            _logger = logger;
            _mapper = mapper;
            _httpClient = httpClient;
        }

      

      

        public virtual async Task<TDTO> Create(TDTO entity, string url)
        {
            try
            {
                var httpClient = _httpClient.CreateClient("videogamesapi");
                var toSaveEntity = _mapper.Map<TMODEL>(entity);

                HttpResponseMessage response = await  httpClient.PostAsJsonAsync(url, toSaveEntity);

                response.EnsureSuccessStatusCode();

                var dbEntity = await response.Content.ReadFromJsonAsync<TDTO>();

                return dbEntity;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error occurred while sending the HTTP request.");
                _logger.LogError("Request URL: " + url);
                _logger.LogError("Request Body: " + JsonConvert.SerializeObject(entity));
                _logger.LogError("Response Status Code: " + ex.StatusCode);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating a new videogame.");
                throw;
            }
        }

        // Implementa gli altri metodi del repository utilizzando la stessa logica con Polly

        public virtual async Task<bool> Delete(int id)
        {
            var httpClient = _httpClient.CreateClient("videogamesapi");
            try
            {
                await httpClient.DeleteAsync($"{id}");

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
            var httpClient = _httpClient.CreateClient("videogamesapi");
            try
            {
                var dbEntity = await httpClient.GetFromJsonAsync<TMODEL>($"{id}");
                var DTOentity = _mapper.Map<TDTO>(dbEntity);
                
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
            var httpClient = _httpClient.CreateClient("videogamesapi");
            try
            {
                var entities = await httpClient.GetFromJsonAsync<IEnumerable<TMODEL>>(url);
                var dtos = _mapper.Map<IEnumerable<TDTO>>(entities);
                 
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
            var httpClient = _httpClient.CreateClient("videogamesapi");
            try
            {
                var existingEntity = await httpClient.PutAsJsonAsync($"{id}", entity).Result.Content.ReadFromJsonAsync<TMODEL>();
                var modifiedDTO = _mapper.Map<TDTO>(existingEntity);
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