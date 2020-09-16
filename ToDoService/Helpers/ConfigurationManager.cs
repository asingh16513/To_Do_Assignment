using Application.Helper;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ToDoService.Helpers
{
    /// <summary>
    /// Method to configure appsettings 
    /// </summary>
    public static class ConfigurationManager
    {
        /// <summary>
        /// Get and configure app settings
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void LoadConfigurationSettings(IServiceCollection services, IConfiguration configuration)
        {
            /*
             One advantage to using IOptions<T> is that it can detect changes to the configuration source and
             reload configuration as the application is running.
            */
            services.AddOptions();
            //Configure Option using Extensions method
            services.Configure<ApplicationSetting>(configuration.GetSection("ApplicationSettings"));
            services.Configure<ConnectionSettings>(configuration.GetSection("ConnectionStrings"));
            services.Configure<Logging>(configuration.GetSection("Logging"));
            services.AddSingleton(configuration);
            InitializeDbService.AddDbServiceConfigurations(services);
        }
    }
}
