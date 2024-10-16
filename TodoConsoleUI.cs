namespace TodoApp;

public class TodoConsoleUI
{
    private readonly ITodoManager _todoManager;
    private readonly Dictionary<string, Action> _menuActions = new Dictionary<string, Action>();

    private const string CreateTodoOption = "1";
    private const string MarkTodoAsDoneOption = "2";
    private const string EditTodoOption = "3";
    private const string DeleteTodoOption = "4";
    private const string ExitOption = "5";

    public TodoConsoleUI(ITodoManager todoManager)
    {
        _todoManager = todoManager;
        _menuActions = new Dictionary<string, Action>
        {
            { CreateTodoOption, CreateTodo },
            { MarkTodoAsDoneOption, MarkTodoAsDone },
            { EditTodoOption, EditTodo },
            { DeleteTodoOption, DeleteTodo },
            { ExitOption, () => Environment.Exit(0) }
        };
    }

    public void Run()
    {
        while (true)
        {
            Console.Clear();
            ViewTodos();
            DisplayMenu();

            string? choice = Console.ReadLine();

            if (choice != null && _menuActions.TryGetValue(choice, out var action))
            {
                action();
            }
            else
            {
                Console.WriteLine("Invalid choice. Please try again.");
            }
        }
    }

    private void DisplayMenu()
    {
        Console.WriteLine("\nTodo App");
        Console.WriteLine($"{CreateTodoOption}. Create a new todo");
        Console.WriteLine($"{MarkTodoAsDoneOption}. Mark a todo as done");
        Console.WriteLine($"{EditTodoOption}. Edit a todo");
        Console.WriteLine($"{DeleteTodoOption}. Delete a todo");
        Console.WriteLine($"{ExitOption}. Exit");
        Console.Write("Enter your choice (1-5): ");
    }

    private void CreateTodo()
    {
        Console.Write("Enter a new todo: ");
        string? text = Console.ReadLine();
        try
        {
            if (!string.IsNullOrEmpty(text))
            {
                _todoManager.AddTodo(text);
                Console.WriteLine("Todo added successfully!");
            }
            else
            {
                Console.WriteLine("Todo text cannot be empty.");
            }
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private void MarkTodoAsDone()
    {
        int index = GetTodoIndex("Enter the number of the todo to mark as done: ");
        if (index != -1)
        {
            try
            {
                _todoManager.MarkTodoAsDone(index);
                Console.WriteLine("Todo marked as done!");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    private void EditTodo()
    {
        int index = GetTodoIndex("Enter the number of the todo to edit: ");
        if (index != -1)
        {
            Console.Write("Enter the new todo text: ");
            string? newText = Console.ReadLine();
            try
            {
                if (!string.IsNullOrEmpty(newText))
                {
                    _todoManager.EditTodo(index, newText);
                    Console.WriteLine("Todo updated successfully!");
                }
                else
                {
                    Console.WriteLine("Todo text cannot be empty.");
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    private void DeleteTodo()
    {
        int index = GetTodoIndex("Enter the number of the todo to delete: ");
        if (index != -1)
        {
            try
            {
                _todoManager.DeleteTodo(index);
                Console.WriteLine("Todo deleted successfully!");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    private void ViewTodos()
    {
        var undoneTodos = _todoManager.GetUndoneTodos();
        var doneTodos = _todoManager.GetDoneTodos();

        if (!undoneTodos.Any() && !doneTodos.Any())
        {
            Console.WriteLine("No todos yet.");
            return;
        }

        Console.WriteLine("Current todos:");
        foreach (var (index, todo) in undoneTodos)
        {
            Console.WriteLine($"{index + 1}. [ ] {todo.Text} (Created: {todo.CreatedDate})");
        }

        if (doneTodos.Any())
        {
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Completed todos:");
            foreach (var (index, todo) in doneTodos)
            {
                Console.WriteLine($"{index + 1}. [X] {todo.Text} (Created: {todo.CreatedDate})");
            }
        }
    }

    private int GetTodoIndex(string prompt)
    {
        Console.Write(prompt);
        string? input = Console.ReadLine();
        if (!string.IsNullOrEmpty(input) && int.TryParse(input, out int index) && index > 0)
        {
            return index - 1;
        }
        Console.WriteLine("Invalid todo number.");
        return -1;
    }
}	
