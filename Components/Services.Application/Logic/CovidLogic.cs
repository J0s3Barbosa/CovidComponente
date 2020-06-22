using CovidComponent.Interfaces;
using Services.Application.Interfaces;
using Services.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Application.Logic
{
    public class CovidLogic : ICovidLogic
    {
        readonly ICovidActions _iCoronaComponent;
        readonly string dateOnlyFormat = "dd/MM/yyyy";

        public CovidLogic(ICovidActions iCoronaComponent)
        {
            _iCoronaComponent = iCoronaComponent;
        }


        public async Task<IEnumerable<Covid>> ListOfCovid19DataAsync()
        {
            return await _iCoronaComponent.GetCovid19CasesAsync();  
        }

        public async Task<Result<string>> PostCovid19DataToMongoDBAsync()
        {
            var result = new Result<string>();
            var resultPost = await _iCoronaComponent.PostDataToMongoDBAsync();
            return (!string.IsNullOrEmpty(resultPost.Response) ? result.ResultResponse(
         resultPost.Response
           )
           : result.ResultError(resultPost.Errors.First()));
        }

        public IEnumerable<Covid> GetDataByCountryFromDB(string country)
        {
            var result = _iCoronaComponent.GetDataFromMongoDB(country, "Confirmados");

            return result;
        }
        public async Task<IEnumerable<Covid>> GetDataByDateFromDBAsync(string date)
        {
            //get data     
            var result = await _iCoronaComponent.GetDataFromDbByDateAsync(date);

            return result;
        }

        public IEnumerable<Covid> GetDataByCountryandDateFromDB()
        {
            var local_Filter = "Brasil";
            var result = _iCoronaComponent.GetDataFromMongoDB(local_Filter, "Confirmados");

            return result;
        }

    
    }

}
