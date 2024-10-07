using System;

namespace TodoApp
{
    public class Todo
    {
        public string Text { get; set; }
        public DateTime CreatedDate { get; }
        public bool IsDone { get; set; }

        public Todo(string text)
        {
            Text = text ?? throw new ArgumentNullException(nameof(text));
            CreatedDate = DateTime.Now;
            IsDone = false;
        }
    }
}