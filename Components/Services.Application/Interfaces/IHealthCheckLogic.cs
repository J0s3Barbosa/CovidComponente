using Services.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Application.Interfaces
{
    public interface IHealthCheckLogic : IGenericLogicAsync<HealthCheck>
    {
        Task<Result<HealthCheck>> AddHealthCheckAsync(HealthCheck todo);
        Task<Result<string>> CalculateTaskTimeAsync(string start, string finish);
        Task<int?> DeleteAsync(Guid id);
        Task<List<HealthCheck>> ListAsync(string appName);
        Task<Result<HealthCheck>> CopyHealthCheckAsync(Guid identifier);
        Task<Result<HealthCheck>> UpdateAsync(Guid identifier, HealthCheck todo);

    }
}
