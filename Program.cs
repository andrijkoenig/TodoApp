using Microsoft.Extensions.DependencyInjection;

namespace TodoApp;
class Program
{
    static void Main(string[] args)
    {
        var startup = new Startup();
        var serviceProvider = startup.ConfigureServices();

        var todoConsoleUI = serviceProvider.GetRequiredService<TodoConsoleUI>();
        todoConsoleUI.Run();
    }
}
