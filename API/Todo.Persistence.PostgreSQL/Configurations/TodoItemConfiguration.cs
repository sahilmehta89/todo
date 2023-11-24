using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.Core.Model;

namespace Todo.Persistence.PostgreSQL.Configurations
{
    public class TodoItemConfiguration : IEntityTypeConfiguration<TodoItem>
    {
        public TodoItemConfiguration() { }

        public void Configure(EntityTypeBuilder<TodoItem> builder)
        {
            builder
                .HasKey(m => m.Id);

            builder.Property(x => x.Id)
                .HasColumnName("Id")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder
                .Property(m => m.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(m => m.IsDone);

            builder
                .Property(m => m.CreationDate)
                .HasDefaultValueSql("timezone('utc', now())");

            builder
                .Property(m => m.UpdationDate);

            builder
                .Property(m => m.IsActive)
                .HasDefaultValue(1);

            builder
            .Property(m => m.IsActive)
            .HasDefaultValue(0);

            builder
                .ToTable("TodoItem");
        }
    }
}
