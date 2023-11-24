using Todo.Core.Repositories;

namespace Todo.Core
{
    public interface IUnitOfWork : IDisposable
    {
        ITodoItemRepository TodoItem { get; }
    }
}
