using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Services.Application.Extensions;
using Services.Application.Interfaces;
using System.Threading.Tasks;

namespace Services.Application.Services
{
    public class ConfigSettings : IConfigSettings
    {
        private readonly ApiSettings _appSettings;

        public ConfigSettings(IOptions<ApiSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public async Task<ApiSettings> GetConfig()
        {
            return await Task.Run(()=>
            {
                return _appSettings;
            }); 
        }
    }
}
