using Services.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Application.Interfaces
{
    public interface IPaymentLogic : IGenericLogicAsync<Payment>
    {
        Task<Result<Payment>> AddEntityAsync(Payment entity);
        Task<Result<string>> CalculatePaymentsAsync(List<Payment> listPayments);
        Task<int?> DeleteAsync(Guid id);
        Task<List<Payment>> ListAsync(string userEmail, string description, string dueDate, string paymentType, string cost, string month);
        Task<Result<Payment>> NewPaymentAsync(Guid identifier);
        Task<Result<Payment>> UpdateAsync(Guid identifier, Payment entity);
    }
}
