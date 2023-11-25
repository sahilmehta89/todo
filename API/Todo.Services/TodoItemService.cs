using AutoMapper;
using Microsoft.AspNetCore.Http;
using Serilog;
using Todo.Core;
using Todo.Core.Model;
using Todo.Core.Model.Dto;
using Todo.Core.Services;

namespace Todo.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public TodoItemService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TodoItemServiceResponse> AddTodoItem(TodoItemCreateModel todoItemCreateModel)
        {
            TodoItemServiceResponse todoItemServiceResponse;

            try
            {
                var todoItemExists = await IsTodoItemExists(todoItemCreateModel.Title).ConfigureAwait(false);

                if (!todoItemExists)
                {
                    var todoItem = _mapper.Map<TodoItem>(todoItemCreateModel);
                    todoItem.IsActive = true;
                    await _unitOfWork.TodoItem.AddTodoItem(todoItem).ConfigureAwait(false);
                    var todoItemViewModel = _mapper.Map<TodoItemViewModel>(todoItem);
                    todoItemServiceResponse = new TodoItemServiceResponse(todoItemViewModel);
                }
                else
                {
                    todoItemServiceResponse = new TodoItemServiceResponse(StatusCodes.Status400BadRequest, "Todo with this title already exists");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in AddTodoItem");
                todoItemServiceResponse =
                    new TodoItemServiceResponse(StatusCodes.Status500InternalServerError, "Some error occurred");
            }

            return todoItemServiceResponse;
        }

        public async Task<TodoItemServiceResponse> UpdateTodoItem(TodoItemUpdateModel todoItemUpdateModel)
        {
            TodoItemServiceResponse todoItemServiceResponse;

            var existing = await _unitOfWork.TodoItem.GetTodoItemById(todoItemUpdateModel.Id).ConfigureAwait(false);

            var todoItemExists = await IsTodoItemExists(todoItemUpdateModel.Title).ConfigureAwait(false);

            if (todoItemExists)
            {
                todoItemServiceResponse = new TodoItemServiceResponse(StatusCodes.Status400BadRequest, "Todo with this title already exists");
            }
            else
            {
                existing.Title = todoItemUpdateModel.Title;
                existing.IsDone = todoItemUpdateModel.IsDone;
                await _unitOfWork.TodoItem.UpdateTodoItem(existing).ConfigureAwait(false);
                todoItemServiceResponse = new TodoItemServiceResponse(_mapper.Map<TodoItemViewModel>(existing));
            }

            return todoItemServiceResponse;
        }

        public async Task<IEnumerable<TodoItemViewModel>> GetAllTodoItem()
        {
            var todoItems = await _unitOfWork.TodoItem.GetAllTodoItem().ConfigureAwait(false);
            var todoItemsDto = _mapper.Map<IEnumerable<TodoItemViewModel>>(todoItems);
            return todoItemsDto;
        }

        public async Task<TodoItemViewModel> GetTodoItem(int id)
        {
            var existing = await _unitOfWork.TodoItem.GetTodoItemById(id).ConfigureAwait(false);
            return _mapper.Map<TodoItemViewModel>(existing);
        }

        public async Task<TodoItemServiceResponse> DeleteTodoItem(int id)
        {
            TodoItemServiceResponse todoItemServiceResponse;

            try
            {
                var existing = await _unitOfWork.TodoItem.GetTodoItemById(id).ConfigureAwait(false);
                if (existing != null)
                {
                    existing.IsDeleted = true;
                    await _unitOfWork.TodoItem.UpdateTodoItem(existing).ConfigureAwait(false);
                    todoItemServiceResponse = new TodoItemServiceResponse(StatusCodes.Status200OK, "Todo Deleted successfully");
                }
                else
                {
                    todoItemServiceResponse = new TodoItemServiceResponse(StatusCodes.Status400BadRequest, "Todo does not exists");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error DeleteTodoItem");
                todoItemServiceResponse =
                    new TodoItemServiceResponse(StatusCodes.Status500InternalServerError, "Some error occurred");
            }

            return todoItemServiceResponse;
        }

        private async Task<bool> IsTodoItemExists(string title)
        {
            try
            {
                title = string.IsNullOrWhiteSpace(title) ? string.Empty : title.Trim();
                return await _unitOfWork.TodoItem.IsTodoItemExists(title).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error getting IsTodoItemExists");
                throw;
            }
        }
    }
}
