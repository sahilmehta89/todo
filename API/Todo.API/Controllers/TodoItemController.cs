using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using System.Net;
using Todo.Core.Model.Dto;
using Todo.Core.Services;

namespace Todo.API.Controllers
{
    [ApiController]
    public class TodoItemController : BaseController
    {
        private readonly ITodoItemService _todoItemService;

        public TodoItemController(ITodoItemService todoItemService)
        {
            _todoItemService = todoItemService;
        }

        [HttpGet]
        [Route("api/TodoItem/")]
        [ProducesResponseType(typeof(IEnumerable<TodoItemViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllTodoItem()
        {
            try
            {
                IEnumerable<TodoItemViewModel> todoItems = await _todoItemService.GetAllTodoItem().ConfigureAwait(false);
                return Ok(todoItems);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while calling GetAllTodoItem in TodoItemController");
                return GetStatusCodeWithProblemDetails(StatusCodes.Status500InternalServerError, "Some error occurred");
            }
        }

        [HttpGet]
        [Route("api/TodoItem/{id}")]
        [ProducesResponseType(typeof(TodoItemViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTodoItem(int id)
        {
            try
            {
                TodoItemViewModel todoItem = await _todoItemService.GetTodoItem(id).ConfigureAwait(false);
                return Ok(todoItem);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while calling GetTodoItem in TodoItemController");
                return GetStatusCodeWithProblemDetails(StatusCodes.Status500InternalServerError, "Some error occurred");
            }
        }

        [HttpPost]
        [Route("api/TodoItem/")]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateTodoItem(TodoItemCreateModel todoItemCreateModel)
        {
            try
            {
                var todoItemServiceResponse = await _todoItemService.AddTodoItem(todoItemCreateModel).ConfigureAwait(false);

                if (todoItemServiceResponse.ReturnCode == 0)
                    return StatusCode(StatusCodes.Status201Created, todoItemServiceResponse.TodoItemViewModel.Id);

                return GetStatusCodeWithProblemDetails(todoItemServiceResponse.HttpStatusCode,
                    todoItemServiceResponse.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error while calling CreateTodoItem in TodoItemController. Data={JsonConvert.SerializeObject(todoItemCreateModel)}");
                return GetStatusCodeWithProblemDetails(StatusCodes.Status500InternalServerError, "Some error occurred");
            }
        }

        [HttpPut]
        [Route("api/TodoItem/")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateTodoItem(TodoItemUpdateModel todoItemUpdateModel)
        {
            try
            {
                var todoItemServiceResponse = await _todoItemService.UpdateTodoItem(todoItemUpdateModel).ConfigureAwait(false);
                if (todoItemServiceResponse.ReturnCode == 0)
                    return NoContent();

                return GetStatusCodeWithProblemDetails(todoItemServiceResponse.HttpStatusCode,
                    todoItemServiceResponse.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                    $"Error while calling UpdateTodoItem in TodoItemController. Data={JsonConvert.SerializeObject(todoItemUpdateModel)}");
                return GetStatusCodeWithProblemDetails(StatusCodes.Status500InternalServerError, "Some error occurred");
            }
        }

        [HttpDelete]
        [Route("api/TodoItem/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteTodoItem(int id)
        {
            try
            {
                var todoItemServiceResponse = await _todoItemService.DeleteTodoItem(id).ConfigureAwait(false);
                if (todoItemServiceResponse.ReturnCode == 0)
                {
                    return StatusCode(todoItemServiceResponse.HttpStatusCode);
                }

                return GetStatusCodeWithProblemDetails(todoItemServiceResponse.HttpStatusCode,
                    todoItemServiceResponse.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                    $"Error while calling DeleteTodoItem todoItemId={id} in TodoItemController");
                return GetStatusCodeWithProblemDetails(StatusCodes.Status500InternalServerError, "Some error occurred");
                ;
            }
        }
    }
}