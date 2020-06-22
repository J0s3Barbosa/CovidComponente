using CovidComponent.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services.Domain.Entities;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Covid19.UnitTest
{
    [TestClass]
    public class CovidFunctionalityUnitTest
    {

        readonly ICovidActions _ICovidLogic;
        public CovidFunctionalityUnitTest()
        {
            var services = new ServiceCollection();
            services.AddCovidComponentConnector();

            var provider = services.BuildServiceProvider();

            _ICovidLogic = provider.GetService<ICovidActions>();


        }
        public static string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            return (propertyExpression.Body as MemberExpression).Member.Name;
        }

        [TestMethod]
        [Description("Get Number of cases in the country")]
        public void GetDataFilteredConfirmedCasesTest()
        {
            var covidObj = new Covid();

            var local_Filter = "Brasil";

            var sortingField = GetPropertyName(
    () => covidObj.Confirmados);

            var totalCases = _ICovidLogic.GetReportFromCountry(local_Filter, sortingField);
            Assert.AreNotEqual("No Content", totalCases);

        }


        [TestMethod]
        [Description("Get latests covid19 numbers")]
        public async Task GetLatestsCovid19NumbersTestAsync()
        {
            var covid19Cases = await _ICovidLogic.GetCovid19CasesAsync();
            Assert.AreNotEqual("No Content", covid19Cases);

            //Assert.(covid19Cases)
            //.As(new covid19Cases())
            //.Given(covid19Cases)
            //.When(covid19Cases)
            //.Then(covid19Cases);



        }


    }
}
