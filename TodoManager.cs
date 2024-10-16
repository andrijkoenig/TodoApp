namespace TodoApp;

public class TodoManager : ITodoManager
{
    private List<Todo> todos = new List<Todo>();

    public void AddTodo(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            throw new ArgumentException("Todo text cannot be empty.");
        
        todos.Add(new Todo(text));
    }

    public void MarkTodoAsDone(int index)
    {
        ValidateIndex(index);
        todos[index].IsDone = true;
    }

    public void EditTodo(int index, string newText)
    {
        ValidateIndex(index);
        if (string.IsNullOrWhiteSpace(newText))
            throw new ArgumentException("Todo text cannot be empty.");
        
        todos[index].Text = newText;
    }

    public void DeleteTodo(int index)
    {
        ValidateIndex(index);
        todos.RemoveAt(index);
    }

    public IEnumerable<(int Index, Todo Todo)> GetUndoneTodos()
    {
        return todos.Where(t => !t.IsDone).Select((t, i) => (i, t));
    }

    public IEnumerable<(int Index, Todo Todo)> GetDoneTodos()
    {
        return todos.Where(t => t.IsDone).Select((t, i) => (i + GetUndoneTodos().Count(), t));
    }

    private void ValidateIndex(int index)
    {
        if (index < 0 || index >= todos.Count)
            throw new ArgumentOutOfRangeException(nameof(index), "Invalid todo number.");
    }
}