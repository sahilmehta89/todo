using Todo.Core;
using Todo.Core.Repositories;
using Todo.Persistence.PostgreSQL.Repositories;

namespace Todo.Persistence.PostgreSQL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TodoDbContext _context;
        private TodoItemRepository _todoItemRepository;

        public UnitOfWork(TodoDbContext context)
        {
            _context = context;
        }

        public ITodoItemRepository TodoItem => _todoItemRepository = _todoItemRepository ?? new TodoItemRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
