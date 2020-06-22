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
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Services.Application.Logic.Tests
{
    [TestClass()]
    public class LoginServicesTests
    {

        private readonly ILoginServices _UserLogic;
        public static IConfiguration Configuration { get; set; }

        public LoginServicesTests()
        {
            var builder = new ConfigurationBuilder()
.AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            var appSettingsSection = configuration.GetSection("AppSettings");
            ApiSettings appSettings = appSettingsSection.Get<ApiSettings>();
            Configuration = appSettingsSection;
            //var services = new ServiceCollection();
            //var provider = services.BuildServiceProvider();
            //_iAutomationHandler = provider.GetService<IAutomationHandler>();


            var _container = new WindsorContainer();
            _container.Register(Component.For<IUserLogic>().ImplementedBy<UserLogic>());
            _container.Register(Component.For<IUser>().ImplementedBy<RepositoryUser>());
            _container.Register(Component.For<ILoginServices>().ImplementedBy<LoginServices>());
            //IOptions<ApiSettings> someOptions = Options.Create(new ApiSettings());
            IOptions<ApiSettings> someOptions = Options.Create(appSettings);
            _container.Register(Component.For<IOptions<ApiSettings>>().Instance(someOptions));

            _UserLogic = _container.Resolve<ILoginServices>();

        }

        [TestMethod()]
        public async Task LoginTokenGenerationTestAsync()
        {
            var user = new User()
            {
                Email = "Email@Email.com",
                Password = "Password",
            };

            var userResponse = await _UserLogic.LoginAsync(user);

            Assert.IsNotNull(userResponse.Response);
            Assert.AreEqual(userResponse.Response.Email, user.Email);
            Assert.IsNotNull(userResponse.Response.Token);

        }


        [TestMethod()]
        public async Task UserIsAdminTestAsync()
        {
            var user = new User()
            {
                Email = "Email@Email.com",
                Password = "Password",
            };

            var userResponse = await _UserLogic.UserIsAdminAsync(user.Email);

            Assert.IsFalse(userResponse);

        }

        [TestMethod()]
        public async Task LoginTestAsync()
        {
            var user = new User()
            {
                Email = "Email@Email.com",
                Password = "Password",
            };

            var userResponse = await _UserLogic.LoginAsync(user.Email, user.Password);

            Assert.IsTrue(userResponse);

        }

        [TestMethod()]
        public async Task GetConfigTestAsync()
        {

            var appSettingsSection = Configuration.GetSection("AppSettings");
            ApiSettings appSettings = appSettingsSection.Get<ApiSettings>();

            var userResponse = appSettings;

            Assert.IsNotNull(userResponse);

        }






    }
}