using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Publisher.Domain.Repositories;
using Publisher.Infrastructure.Persistence.Mongo;
using Publisher.Infrastructure.Repositories;

namespace Publisher.CrossCutting
{
    public static class PublisherServicesConfiguration
    {
        public static void ConfigureMongo(this IServiceCollection services)
        {
            services.AddSingleton(x =>
            {
                var configuration = x.GetService<IConfiguration>();
                var options = new MongoSettings();

                configuration.GetSection(nameof(MongoSettings)).Bind(options);

                return options;
            });

            services.AddSingleton<IMongoClient>(x =>
            {
                var mongoSettings = x.GetService<MongoSettings>();

                return new MongoClient(mongoSettings.ConnectionString);
            });

            services.AddScoped<IMongoDatabase>(x =>
            {
                var mongoSettings = x.GetService<MongoSettings>();
                var client = x.GetService<IMongoClient>();

                return client.GetDatabase(mongoSettings.DatabaseName);
            });

            services.AddScoped<IMessageRepository, MessageRepository>();
        }
    }
}