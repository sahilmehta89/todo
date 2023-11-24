using AutoMapper;
using Todo.Core.Model;
using Todo.Core.Model.Dto;

namespace Todo.Services.Maps
{
    public class TodoItemProfile : Profile
    {
        public TodoItemProfile()
        {
            CreateMap<TodoItem, TodoItemViewModel>();
            CreateMap<TodoItemUpdateModel, TodoItem>();
            CreateMap<TodoItemViewModel, TodoItemUpdateModel>();
            CreateMap<TodoItemCreateModel, TodoItem>();
        }
    }
}
