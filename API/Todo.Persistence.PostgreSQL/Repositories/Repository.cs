using Microsoft.EntityFrameworkCore;
using Todo.Core.Repositories;

namespace Todo.Persistence.PostgreSQL.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;

        public Repository(DbContext context)
        {
            Context = context;
        }

        public async Task AddAsync(TEntity entity)
        {
            await Context.Set<TEntity>().AddAsync(entity);
            await Context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await Context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Context.Set<TEntity>().ToListAsync();
        }
    }
}
