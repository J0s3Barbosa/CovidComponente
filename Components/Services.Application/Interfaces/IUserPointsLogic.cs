using Services.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Services.Application.Interfaces
{
    public interface IUserPointsLogic : IGenericLogic<UserPoints>
    {
        int? Delete(Guid id);
        Result<UserPoints> AddEntity(UserPoints entity);
        List<UserPoints> List(string email);
        Result<UserPoints> Update(Guid identifier, UserPoints entity);
        Result<UserPoints> AddingPoints(UserPoints entity);
    }
}
