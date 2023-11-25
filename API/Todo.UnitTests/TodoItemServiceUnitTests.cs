using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Todo.Core.Model;
using Todo.Core.Model.Dto;
using Todo.Persistence.PostgreSQL;
using Todo.Services;
using Todo.Services.Maps;
using Todo.UnitTests.MockData;

namespace Todo.UnitTests
{
    public class TodoItemServiceUnitTests : IDisposable
    {
        protected readonly TodoDbContext _context;

        public TodoItemServiceUnitTests()
        {
            var options = new DbContextOptionsBuilder<TodoDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new TodoDbContext(options);

            _context.Database.EnsureCreated();
        }

        [Fact]
        public async Task GetAllAsync_ReturnTodoItemCollection()
        {
            /// Arrange
            _context.TodoItem.AddRange(TodoitemMockData.GetTodoItems());
            await _context.SaveChangesAsync().ConfigureAwait(false);

            UnitOfWork unitOfWork = new UnitOfWork(_context);            
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new TodoItemProfile()));
            IMapper mapper = new Mapper(configuration);
            var sut = new TodoItemService(mapper, unitOfWork);

            /// Act
            var result = await sut.GetAllTodoItem().ConfigureAwait(false);

            /// Assert
            result.Should().HaveCount(TodoitemMockData.GetTodoItems().Count() + 1);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
