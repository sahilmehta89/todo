using Microsoft.EntityFrameworkCore;
using Todo.Core.Model;
using Todo.Core.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Todo.Persistence.PostgreSQL.Repositories
{
    public class TodoItemRepository : Repository<TodoItem>, ITodoItemRepository
    {
        public TodoItemRepository(TodoDbContext context)
            : base(context)
        {

        }

        private TodoDbContext DbContext => Context as TodoDbContext;

        public async Task AddTodoItem(TodoItem todoItem)
        {
            await AddAsync(todoItem).ConfigureAwait(false);
        }

        public async Task UpdateTodoItem(TodoItem todoItem)
        {
            await UpdateAsync(todoItem).ConfigureAwait(false);
        }

        public async Task<IEnumerable<TodoItem>> GetAllTodoItem()
        {
            return await DbContext.TodoItem.Where(x=> x.IsDeleted == false).ToListAsync().ConfigureAwait(false);
        }

        public async Task<TodoItem> GetTodoItemById(int id)
        {
            return await DbContext.TodoItem.SingleOrDefaultAsync(m => m.Id == id && m.IsDeleted == false).ConfigureAwait(false);
        }

        public async Task<bool> IsTodoItemExists(string title)
        {
            return await DbContext.TodoItem.AnyAsync(m => m.IsDeleted == false && m.Title.ToLower() == title.ToLower()).ConfigureAwait(false);
        }
    }
}
