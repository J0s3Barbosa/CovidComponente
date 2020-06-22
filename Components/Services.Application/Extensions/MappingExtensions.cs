
using CovidComponent.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Services.Application.Interfaces;
using Services.Application.Logic;
using Services.Application.Services;
using Services.Domain.Interfaces;
using Services.Infra.Repository;

namespace Services.Application.Extensions
{
    public static class MappingExtensions
    {
        public static void SetDI(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGeneric<>), typeof(RepositoryGeneric<>));
            services.AddScoped<IAirPlane, RepositoryAirPlane>();
            services.AddScoped<IAirPlaneModel, RepositoryAirPlaneModel>();
            services.AddScoped<IPayment, RepositoryPayment>();
            services.AddScoped<IQuestions, RepositoryQuestions>();
            services.AddScoped<IUser, RepositoryUser>();
            services.AddScoped<IUserPoints, RepositoryUserPoints>();
            services.AddScoped<ITodo, RepositoryTodo>();
            services.AddScoped<IVideoPlayer, RepositoryVideoPlayer>();
            services.AddScoped<IHealthCheck, RepositoryHealthCheck>();
            services.AddScoped<IArticle, RepositoryArticle>();
            services.AddScoped<IEstimatedtimeTracker, RepositoryEstimatedtimeTracker>();

            services.AddScoped<IAirPlaneLogic, AirPlaneLogic>();
            services.AddScoped<IAirPlaneModelLogic, AirPlaneModelLogic>();
            services.AddScoped<IPaymentLogic, PaymentLogic>();
            services.AddScoped<IQuestionsLogic, QuestionsLogic>();
            services.AddScoped<ICovidLogic, CovidLogic>();
            services.AddScoped<IUserLogic, UserLogic>();
            services.AddScoped<IUserPointsLogic, UserPointsLogic>();
            services.AddScoped<ITodoLogic, TodoLogic>();
            services.AddScoped<IVideoPlayerLogic, VideoPlayerLogic>();
            services.AddScoped<IHealthCheckLogic, HealthCheckLogic>();
            services.AddScoped<ILoginServices, LoginServices>();
            services.AddScoped<IArticleLogic, ArticleLogic>();
            services.AddScoped<IEstimatedtimeTrackerLogic, EstimatedtimeTrackerLogic>();

            services.AddCovidComponentConnector();



        }


    }
}
