using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using Todo.API;
using Todo.Core.Model.Dto;

namespace Todo.FunctionalTest
{
    public class TodoItemTests : BaseControllerTests
    {
        public TodoItemTests(CustomWebApplicationFactory<Program> factory) : base(factory)
        {
        }

        [Fact]
        public async Task GetAllTodoItems()
        {
            var client = this.GetNewClient();
            var response = await client.GetAsync("/api/TodoItem");
            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var result = JsonConvert.DeserializeObject<IEnumerable<TodoItemViewModel>>(stringResponse);

            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assert.True(result?.Count() > 0);
        }

        [Fact]
        public async Task CreateTodoItem()
        {
            var client = this.GetNewClient();

            // Create
            TodoItemCreateModel todoItemCreateModel = new TodoItemCreateModel()
            {
                Title = "Create Todo Test"
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(todoItemCreateModel), Encoding.UTF8, "application/json");

            var response1 = await client.PostAsync("/api/TodoItem", stringContent).ConfigureAwait(false);
            response1.EnsureSuccessStatusCode();
            var stringResponse1 = await response1.Content.ReadAsStringAsync().ConfigureAwait(false);

            Assert.Equal(System.Net.HttpStatusCode.Created, response1.StatusCode);
            Assert.True(!string.IsNullOrEmpty(stringResponse1));
            Assert.True(Convert.ToInt32(stringResponse1) > 1);

            // Get Todo
            var response = await client.GetAsync("/api/TodoItem/" + stringResponse1);
            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var createdTodo = JsonConvert.DeserializeObject<TodoItemViewModel>(stringResponse);
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(createdTodo);
            Assert.Equal(todoItemCreateModel.Title, createdTodo.Title);
            Assert.False(createdTodo.IsDone);
        }

        [Fact]
        public async Task UpdateAndMarkDoneTodoItem()
        {
            var client = this.GetNewClient();

            // Create
            TodoItemCreateModel todoItemCreateModel = new TodoItemCreateModel()
            {
                Title = "Create Todo Test"
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(todoItemCreateModel), Encoding.UTF8, "application/json");

            var response2 = await client.PostAsync("/api/TodoItem", stringContent).ConfigureAwait(false);
            response2.EnsureSuccessStatusCode();
            var stringResponse2 = await response2.Content.ReadAsStringAsync().ConfigureAwait(false);

            Assert.Equal(System.Net.HttpStatusCode.Created, response2.StatusCode);
            Assert.True(!string.IsNullOrEmpty(stringResponse2));
            Assert.True(Convert.ToInt32(stringResponse2) > 1);

            // Update
            TodoItemUpdateModel todoItemUpdateModel = new TodoItemUpdateModel()
            {
                Id = Convert.ToInt32(stringResponse2),
                IsDone = true,
                Title = "Create Todo Test - Done"
            };

            stringContent = new StringContent(JsonConvert.SerializeObject(todoItemUpdateModel), Encoding.UTF8, "application/json");

            var response1 = await client.PutAsync("/api/TodoItem", stringContent).ConfigureAwait(false);
            response1.EnsureSuccessStatusCode();
            var stringResponse1 = await response1.Content.ReadAsStringAsync().ConfigureAwait(false);
            Assert.Equal(System.Net.HttpStatusCode.NoContent, response1.StatusCode);

            // Get Todo details
            var response = await client.GetAsync("/api/TodoItem/" + todoItemUpdateModel.Id);
            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var updatedTodo = JsonConvert.DeserializeObject<TodoItemViewModel>(stringResponse);
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(updatedTodo);
            Assert.Equal(todoItemUpdateModel.Title, updatedTodo.Title);
            Assert.True(updatedTodo.IsDone);
        }

        [Fact]
        public async Task DeleteTodoItem()
        {
            var client = this.GetNewClient();

            // Create
            TodoItemCreateModel todoItemCreateModel = new TodoItemCreateModel()
            {
                Title = "Create Todo Test"
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(todoItemCreateModel), Encoding.UTF8, "application/json");

            var response2 = await client.PostAsync("/api/TodoItem", stringContent).ConfigureAwait(false);
            response2.EnsureSuccessStatusCode();
            var stringResponse2 = await response2.Content.ReadAsStringAsync().ConfigureAwait(false);

            Assert.Equal(System.Net.HttpStatusCode.Created, response2.StatusCode);
            Assert.True(!string.IsNullOrEmpty(stringResponse2));
            Assert.True(Convert.ToInt32(stringResponse2) > 1);

            // Delete
            var response1 = await client.DeleteAsync("/api/TodoItem/" + stringResponse2).ConfigureAwait(false);
            response1.EnsureSuccessStatusCode();
            var stringResponse1 = await response1.Content.ReadAsStringAsync().ConfigureAwait(false);
            Assert.Equal(System.Net.HttpStatusCode.OK, response1.StatusCode);

            // Get All Todos
            var response = await client.GetAsync("/api/TodoItem/" + stringResponse2);
            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var result = JsonConvert.DeserializeObject<TodoItemViewModel>(stringResponse);
            Assert.Null(result);
        }
    }
}