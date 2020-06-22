using Services.Application.Interfaces;
using Services.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using Services.Domain.Interfaces;
using Services.Infra.Repository;
using System.Linq;
using Services.Application.Services;
using Services.Application.Extensions;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Services.Application.Logic.Tests
{
    [TestClass()]
    public class UserLogicTests
    {

        private readonly IUserLogic _UserLogic;
        public UserLogicTests()
        {
            IOptions<ApiSettings> someOptions = Options.Create(new ApiSettings());
            var _container = new WindsorContainer();
            _container.Register(Component.For<IUserLogic>().ImplementedBy<UserLogic>());
            _container.Register(Component.For<IUser>().ImplementedBy<RepositoryUser>());
            _container.Register(Component.For<ILoginServices>().ImplementedBy<LoginServices>());
            _container.Register(Component.For<IOptions<ApiSettings>>().Instance(someOptions));
            _UserLogic = _container.Resolve<IUserLogic>();
        }
        [TestMethod()]
        public void ListUsersTest()
        {
            var user = new User()
            {
                Email = "Email@Email.com",
                Password = "Password",
            };
            List<User> users = this._UserLogic.List();

            Assert.IsNotNull(users);
            Assert.AreEqual(users.FirstOrDefault().Email, user.Email);

        }

        [TestMethod()]
        public async System.Threading.Tasks.Task ListUsersAdminTestAsync()
        {
            var user = new User()
            {
                Email = "Email@Email.com",
                Password = "Password",

            };
            List<User> users = await _UserLogic.ListUser(user.Email);

            Assert.IsNull(users);

        }

        [TestMethod()]
        public async System.Threading.Tasks.Task GetUserTestAsync()
        {
            var user = new User()
            {
                Email = "Email@Email.com",
                Password = "Password",

            };
            User userResponse = await _UserLogic.GetUser(user.Email);

            Assert.IsNull(userResponse);

        }

        [TestMethod()]
        public async Task UpdateUsersAdminTestAsync()
        {
            var user = new User()
            {
                Email = "Email@Email.com",
                Password = "Password",
                Admin = true
            };
            User userResponse = await _UserLogic.GetUser(user.Email);

            Assert.IsNull(userResponse);

            var result = _UserLogic.Update(userResponse.Id, user);

            Assert.IsNotNull(result.Response);

        }

    }
}