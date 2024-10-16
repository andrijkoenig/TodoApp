using Microsoft.Extensions.DependencyInjection;

namespace TodoApp;

public class Startup
{
    public IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        services.AddSingleton<ITodoManager, TodoManager>();
        services.AddTransient<TodoConsoleUI>();

        return services.BuildServiceProvider();
    }
}

