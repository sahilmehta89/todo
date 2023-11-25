using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Core.Model.Dto;
using Todo.Core.Model;

namespace Todo.Core.Services
{
    public interface ITodoItemService
    {
        Task<TodoItemServiceResponse> AddTodoItem(TodoItemCreateModel todoItemCreateModel);
        Task<TodoItemServiceResponse> UpdateTodoItem(TodoItemUpdateModel todoItemUpdateModel);
        Task<IEnumerable<TodoItemViewModel>> GetAllTodoItem();
        Task<TodoItemViewModel> GetTodoItem(int id);
        Task<TodoItemServiceResponse> DeleteTodoItem(int id);
    }
}
