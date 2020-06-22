using Services.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Application.Interfaces
{
    public interface ITodoLogic : IGenericLogicAsync<Todo>
    {
        Task<Result<Todo>> AddTodoAsync(Todo todo);
        Task<Result<string>> CalculateTaskTimeAsync(string start, string finish);
        Task<int?> DeleteAsync(Guid id);
        Task<List<Todo>> ListAsync(string description, string userEmail);
        Task<Result<Todo>> CopyTodoAsync(Guid identifier);
        Task<Result<Todo>> UpdateAsync(Guid identifier, Todo todo);
        Task<Todo> GetEntityAsync(Guid id);

    }
}
