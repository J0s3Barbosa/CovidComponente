using Microsoft.Extensions.DependencyInjection;

namespace Services.Application.Extensions
{
    public static class CorsExtensions
    {

        public static void SetCors(this IServiceCollection services, string policyName, string origin)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(policyName,
                builder =>
                {
                    builder.WithOrigins(origin
                        )
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    ;
                });

            });


        }


    }
}
