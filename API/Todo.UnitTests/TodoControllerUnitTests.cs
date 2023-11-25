using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Todo.API.Controllers;
using Todo.Core.Services;
using Todo.UnitTests.MockData;

namespace Todo.UnitTests
{
    public class TodoControllerUnitTests
    {
        [Fact]
        public async Task GetAllTodoItemsAsync_ShouldReturn200Status()
        {
            /// Arrange
            var todoItemService = new Mock<ITodoItemService>();
            todoItemService.Setup(x => x.GetAllTodoItem()).ReturnsAsync(TodoitemMockData.GetTodoItemsViewModel());
            var sut = new TodoItemController(todoItemService.Object);

            /// Act
            var result = (OkObjectResult)await sut.GetAllTodoItem();

            // /// Assert
            result.StatusCode.Should().Be(200);
        }
    }
}