using Microsoft.Extensions.DependencyInjection;
using Todo.Core.Services;
using Todo.Services.Maps;

namespace Todo.Services
{
    /// <summary>
    ///     Provides registration of services via extension methods.
    /// </summary>
    public static class ServiceConfig
    {
        /// <summary>
        ///     Registers common services
        /// </summary>
        /// <param name="services">Service collection (IoC container) where to register the services.</param>
        /// <returns>Service collection  (IoC container) where the services were registered.</returns>
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            return services
                .AddSingleton(MappingInitializer.Intialize())
                .AddTransient<ITodoItemService, TodoItemService>();
        }
    }
}
