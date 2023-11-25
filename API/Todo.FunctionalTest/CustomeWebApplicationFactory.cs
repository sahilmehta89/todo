using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Todo.Persistence.PostgreSQL;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Todo.FunctionalTest
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove the app's DBContext registration.
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<TodoDbContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // Add DBContext using an in-memory database for testing.
                services.AddDbContext<TodoDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryTodoDb");
                });

                // Get service provider.
                var serviceProvider = services.BuildServiceProvider();

                using (var scope = serviceProvider.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;

                    var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                    var todoDbContext = scopedServices.GetRequiredService<TodoDbContext>();
                    todoDbContext.Database.EnsureCreated();

                    //try
                    //{
                    //    DatabaseSetup.SeedData(todoDbContext);
                    //}
                    //catch (Exception ex)
                    //{
                    //    logger.LogError(ex, $"An error occurred seeding the Store database with test messages. Error: {ex.Message}");
                    //}
                }
            });
        }

        public void CustomConfigureServices(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Get service provider.
                var serviceProvider = services.BuildServiceProvider();

                using (var scope = serviceProvider.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;

                    var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                    var storeDbContext = scopedServices.GetRequiredService<TodoDbContext>();

                    //try
                    //{
                    //    DatabaseSetup.SeedData(storeDbContext);
                    //}
                    //catch (Exception ex)
                    //{
                    //    logger.LogError(ex, $"An error occurred seeding the Store database with test messages. Error: {ex.Message}");
                    //}
                }
            });
        }
    }
}
