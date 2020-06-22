using Services.Domain.Entities;
using System.Threading.Tasks;

namespace Services.Application.Interfaces
{
    public interface ILoginServices
    {
        Task<Result<User>> LoginAsync(User user);
        Task<bool> LoginAsync(string email, string password);
        Task<bool> UserIsAdminAsync(string email);
    }
}
