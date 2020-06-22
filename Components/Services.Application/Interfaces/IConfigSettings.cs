using Microsoft.Extensions.Options;
using Services.Application.Extensions;
using System.Threading.Tasks;

namespace Services.Application.Interfaces
{
    public interface IConfigSettings
    {
        Task<ApiSettings> GetConfig();
     }
}
