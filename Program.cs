using System;
using System.Collections.Generic;
using System.Linq;

namespace TodoApp
{
    class Program
    {
        private static readonly TodoManager todoManager = new TodoManager();

        private const string CreateTodoOption = "1";
        private const string MarkTodoAsDoneOption = "2";
        private const string EditTodoOption = "3";
        private const string DeleteTodoOption = "4";
        private const string ExitOption = "5";

        private static readonly Dictionary<string, Action> menuActions = new Dictionary<string, Action>
        {
            { CreateTodoOption, CreateTodo },
            { MarkTodoAsDoneOption, MarkTodoAsDone },
            { EditTodoOption, EditTodo },
            { DeleteTodoOption, DeleteTodo },
            { ExitOption, () => Environment.Exit(0) }
        };

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                ViewTodos();
                DisplayMenu();

                string? choice = Console.ReadLine();

                if (!string.IsNullOrEmpty(choice) && menuActions.TryGetValue(choice, out Action? action) && action != null)
                {
                    action();
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                }
            }
        }

        static void DisplayMenu()
        {
            Console.WriteLine("\nTodo App");
            Console.WriteLine($"{CreateTodoOption}. Create a new todo");
            Console.WriteLine($"{MarkTodoAsDoneOption}. Mark a todo as done");
            Console.WriteLine($"{EditTodoOption}. Edit a todo");
            Console.WriteLine($"{DeleteTodoOption}. Delete a todo");
            Console.WriteLine($"{ExitOption}. Exit");
            Console.Write("Enter your choice (1-5): ");
        }

        static void CreateTodo()
        {
            Console.Write("Enter a new todo: ");
            string? text = Console.ReadLine();
            try
            {
                if (!string.IsNullOrEmpty(text))
                {
                    todoManager.AddTodo(text);
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

        static void MarkTodoAsDone()
        {
            int index = GetTodoIndex("Enter the number of the todo to mark as done: ");
            if (index != -1)
            {
                try
                {
                    todoManager.MarkTodoAsDone(index);
                    Console.WriteLine("Todo marked as done!");
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        static void EditTodo()
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
                        todoManager.EditTodo(index, newText);
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

        static void DeleteTodo()
        {
            int index = GetTodoIndex("Enter the number of the todo to delete: ");
            if (index != -1)
            {
                try
                {
                    todoManager.DeleteTodo(index);
                    Console.WriteLine("Todo deleted successfully!");
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        static void ViewTodos()
        {
            var undoneTodos = todoManager.GetUndoneTodos();
            var doneTodos = todoManager.GetDoneTodos();

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

        static int GetTodoIndex(string prompt)
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
}
