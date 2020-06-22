using Microsoft.Extensions.DependencyInjection;

namespace CrossCutting
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddICovidStatusConnector(this IServiceCollection services)
        {
            services.AddTransient<ICovidStatus, CovidStatus>();

            return services;
        }
    }
}
