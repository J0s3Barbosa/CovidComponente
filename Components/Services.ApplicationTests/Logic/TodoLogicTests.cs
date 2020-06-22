using Services.Application.Interfaces;
using Services.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using Services.Domain.Interfaces;
using Services.Infra.Repository;
using Services.Application.Services;
using Services.Application.Extensions;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Services.Application.Logic.Tests
{
    [TestClass()]
    public class TodoLogicTests
    {

        private readonly ITodoLogic _TodoLogic;
        const string timeZone = "bras";
        const string dateFormat = "dd/MM/yyyy";
        const string dateTimeFormat = "dd/MM/yyyy HH:mm:ss";
        const string timeFormat = "HH:mm:ss";

        public TodoLogicTests()

        {
            IOptions<ApiSettings> someOptions = Options.Create(new ApiSettings());
            var _container = new WindsorContainer();
            _container.Register(Component.For<ITodoLogic>().ImplementedBy<TodoLogic>());
            _container.Register(Component.For<ITodo>().ImplementedBy<RepositoryTodo>());
            _container.Register(Component.For<ILoginServices>().ImplementedBy<LoginServices>());
            _container.Register(Component.For<IOptions<ApiSettings>>().Instance(someOptions));
            _TodoLogic = _container.Resolve<ITodoLogic>();
        }
        [TestMethod()]
        public async Task TestCalcMonths()
        {
            var todo = new Todo()
            {
                UserEmail = "Email@Email.com",
                TimeStarted = TimeServices.DateTimeFormated(timeZone, DateTime.Now.AddMonths(-2)).ToString(dateTimeFormat),
                TimeFinished = TimeServices.DateTimeFormated(timeZone, DateTime.Now).ToString(dateTimeFormat),
            };

            var result = await _TodoLogic.CalculateTaskTimeAsync(todo.TimeStarted, todo.TimeFinished);

            Assert.IsNotNull(result.Response);

        }

        [TestMethod()]
        public async Task TestCalcDays()
        {
            var todo = new Todo()
            {
                UserEmail = "Email@Email.com",
                TimeStarted = TimeServices.DateTimeFormated(timeZone, DateTime.Now.AddDays(-120)).ToString(dateTimeFormat),
                TimeFinished = TimeServices.DateTimeFormated(timeZone, DateTime.Now).ToString(dateTimeFormat),
            };

            var result = await _TodoLogic.CalculateTaskTimeAsync(todo.TimeStarted, todo.TimeFinished);

            Assert.IsNotNull(result.Response);

        }

        [TestMethod()]
        public async Task TestCalcHours()
        {
            var todo = new Todo()
            {
                UserEmail = "Email@Email.com",
                TimeStarted = TimeServices.DateTimeFormated(timeZone, DateTime.Now.AddHours(-25)).ToString(dateTimeFormat),
                TimeFinished = TimeServices.DateTimeFormated(timeZone, DateTime.Now).ToString(dateTimeFormat),
            };

            var result = await _TodoLogic.CalculateTaskTimeAsync(todo.TimeStarted, todo.TimeFinished);

            Assert.IsNotNull(result.Response);

        }


    }
}