using Services.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Application.Interfaces
{
    public interface ICovidLogic
    {
        /// <summary>
        /// GetDataByCountryandDateFromDB
        /// </summary>
        /// <returns></returns>
        IEnumerable<Covid> GetDataByCountryandDateFromDB();
        /// <summary>
        /// GetDataByCountryFromDB
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        IEnumerable<Covid> GetDataByCountryFromDB(string country);
        /// <summary>
        /// GetDataByDateFromDBAsync
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        Task<IEnumerable<Covid>> GetDataByDateFromDBAsync(string date);

        /// <summary>
        /// ListOfCovid19DataAsync
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Covid>> ListOfCovid19DataAsync();
        /// <summary>
        /// PostCovid19DataToMongoDBAsync
        /// </summary>
        /// <returns></returns>
        Task<Result<string>> PostCovid19DataToMongoDBAsync();
    }
}