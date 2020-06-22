using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Application.Interfaces
{
    public interface IGenericLogicAsync<T> where T : class
    {
        Task<int> AddAsync(T Entity);

        Task<int> UpdateAsync(T Entity);

        Task<int> DeleteAsync(T Entity);

        Task<T> GetEntityAsync(Guid id);

         Task<List<T>> ListAsync();
    }
}