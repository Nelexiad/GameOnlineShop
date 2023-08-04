using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Repositories.Repositories
{
    public interface IRepositoryMVC<TDTO> 
    {
        Task<TDTO> Get(int id);

        Task<IEnumerable<TDTO>> GetAll(string url);

        Task<TDTO> Create(TDTO entity,string url);
        Task<TDTO> Update(int id, TDTO entity);
        Task<bool> Delete(int id);
    }
}
