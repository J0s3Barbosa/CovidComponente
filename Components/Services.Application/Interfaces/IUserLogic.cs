using Services.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Application.Interfaces
{
    public interface IUserLogic : IGenericLogic<User>
    {
        int? Delete(Guid id);
        Result<User> AddEntity(User entity);
        Task<List<User>> ListUser(string email);
        Task<User> GetUser(string email);
        Result<User> Update(Guid identifier, User entity);
    }
}
