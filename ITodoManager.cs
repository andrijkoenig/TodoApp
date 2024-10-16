namespace TodoApp;
public interface ITodoManager
{
    void AddTodo(string text);
    void MarkTodoAsDone(int index);
    void EditTodo(int index, string newText);
    void DeleteTodo(int index);
    IEnumerable<(int Index, Todo Todo)> GetUndoneTodos();
    IEnumerable<(int Index, Todo Todo)> GetDoneTodos();
}