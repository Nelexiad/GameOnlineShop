using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Entities.Models.DTO;
using Entities.Data;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using Azure;
using System.Text.Json.Serialization;
using Newtonsoft.Json;


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
        public virtual async Task<TDTO> Create(TDTO entity, string url)
        {
            try
            {
                var toSaveEntity = _mapper.Map<TMODEL>(entity);
                var response = await _httpClient.PostAsJsonAsync(url, toSaveEntity);

                // Assicurati che la richiesta HTTP sia stata completata con successo (200 OK)
                response.EnsureSuccessStatusCode();

                // Leggi il contenuto della risposta HTTP e deserializzalo nel tipo appropriato (TDTO)
                var dbEntity = await response.Content.ReadFromJsonAsync<TDTO>();

                return dbEntity;
            }
            catch (HttpRequestException ex)
            {
                // Gestisci l'eccezione e visualizza i dettagli dell'errore
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

