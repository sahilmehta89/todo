using Todo.Core.Model;

namespace Todo.Core.Repositories
{
    public interface ITodoItemRepository
    {
        Task AddTodoItem(TodoItem todoItem);
        Task UpdateTodoItem(TodoItem todoItem);
        Task<IEnumerable<TodoItem>> GetAllTodoItem();
        Task<TodoItem> GetTodoItemById(int id);
        Task<bool> IsTodoItemExists(string title);
    }
}
