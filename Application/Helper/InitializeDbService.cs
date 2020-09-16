using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Helper
{
    /// <summary>
    /// Class to create automatic creation of database
    /// </summary>
    public class InitializeDbService
    {
        public static IServiceCollection AddDbServiceConfigurations(IServiceCollection configuration)
        {
            ToDoServiceDBContext serviceDBContext = new ToDoServiceDBContext();
            serviceDBContext.Database.Migrate();
            return configuration;
        }
    }
}
