using AutoMapper;
using Entities.Models.DTO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Repositories.Repositories;
using System.Net.Http;
using System.Net.Http.Json;

namespace Services
{
    public class VideogameServices : ServiceCollection
    {
        private readonly VideogameRepository _repository;

        public const string ClientName = "videogamesapi";
        
        public VideogameServices(VideogameRepository repo)
        {
            _repository = repo;



        }

        public virtual async Task<VideogameDTO> Create(VideogameDTO entity, string url)
        {
            try
            {

            await  _repository.Create(entity, url);
                return entity;
            }
           
            catch (Exception ex)
            {
               
                throw;
            }
        }
        public virtual async Task<IEnumerable<VideogameDTO>> GetAll(string url)
        {
            
            try
            {
              var dtos=  await _repository.GetAll(url);
                return dtos;
            }
            catch (Exception ex)
            {
               
                throw;
            }
        }

    }
}