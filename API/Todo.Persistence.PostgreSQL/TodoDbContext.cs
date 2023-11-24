using Microsoft.EntityFrameworkCore;
using Todo.Core.Model;
using Todo.Persistence.PostgreSQL.Configurations;

namespace Todo.Persistence.PostgreSQL
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options)
            : base(options)
        {
        }

        public DbSet<TodoItem> TodoItem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfiguration(new TodoItemConfiguration());
            PopulateSeedData(modelBuilder);
        }

        private void PopulateSeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoItem>().HasData(new TodoItem
            {
                Id = 1,
                Title = "My First Todo Item"
            });
        }
    }
}
