using Microsoft.EntityFrameworkCore;
using Services.Domain.Entities;
using Services.Infra.Map;
using System;

namespace Services.Infra.Context
{
    public class ContextBase : DbContext
    {

        public ContextBase() { }

        public ContextBase(DbContextOptions<ContextBase> options) : base(options) { }

        public DbSet<AirPlaneModel> AirPlaneModel { get; set; }

        public DbSet<AirPlane> AirPlane { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Questions> Questions { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserPoints> UserPoints { get; set; }
        public DbSet<Todo> Todo { get; set; }
        public DbSet<VideoPlayer> VideoPlayer { get; set; }
        public DbSet<HealthCheck> HealthCheck { get; set; }
        public DbSet<Article> Article { get; set; }
        public DbSet<EstimatedtimeTracker> EstimatedtimeTracker { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new AirPlaneModelConfigurations());
            modelBuilder.ApplyConfiguration(new AirPlaneConfigurations());
            modelBuilder.ApplyConfiguration(new PaymentConfigurations());
            modelBuilder.ApplyConfiguration(new QuestionsConfigurations());
            modelBuilder.ApplyConfiguration(new UserConfigurations());
            modelBuilder.ApplyConfiguration(new UserPointsConfigurations());
            modelBuilder.ApplyConfiguration(new TodoConfigurations());
            modelBuilder.ApplyConfiguration(new VideoPlayerConfigurations());
            modelBuilder.ApplyConfiguration(new healthCheckConfigurations());
            modelBuilder.ApplyConfiguration(new ArticleConfigurations());
            modelBuilder.ApplyConfiguration(new EstimatedtimeTrackerConfigurations());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                   .UseSqlServer(this.StringConnectionConfig()); //.UseLazyLoadingProxies()
            }
        }

        private string StringConnectionConfig()
        {
            //var conn = Environment.GetEnvironmentVariable("LocalConnection");
            var conn = Environment.GetEnvironmentVariable("AzureDevConnection");
            return conn;
        }
    }

}
