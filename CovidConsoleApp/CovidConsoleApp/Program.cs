using CovidComponent.Interfaces;
using CrossCutting;
using Microsoft.Extensions.DependencyInjection;
using Services.Domain.Entities;
using System;
using System.Collections.Generic;

namespace CovidConsoleApp
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {

            try
            {
                var services = new ServiceCollection();
                services.AddICovidStatusConnector();
                var provider = services.BuildServiceProvider();
                var _ICovidStatus = provider.GetService<ICovidStatus>();

                await _ICovidStatus.ListStatusAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
