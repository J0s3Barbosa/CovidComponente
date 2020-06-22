using CovidComponent.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Services.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrossCutting
{
    public class CovidStatus : ICovidStatus
    {
        private readonly ICovidActions _ICovidLogic;

        public CovidStatus()
        {
            var services = new ServiceCollection();
            services.AddCovidComponentConnector();

            var provider = services.BuildServiceProvider();

            _ICovidLogic = provider.GetService<ICovidActions>();

        }


        public async Task ListStatusAsync()
        {

            try
            {
                var data = await _ICovidLogic.GetCovid19CasesAsync();
                foreach (var item in data)
                {
                    foreach (var entityProp in data.GetType().GetProperties())
                    {
                        var entityValue = entityProp.GetValue(item);

                        if (!string.IsNullOrEmpty(entityValue as string))
                        {
                            var result = $"{entityProp.Name}: {entityValue}";
                            Console.WriteLine(result);

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
