using Todo.Core.Model.Dto;

namespace Todo.Core.Model
{
    public class TodoItemServiceResponse : BaseServiceResponse
    {
        public TodoItemViewModel TodoItemViewModel { get; set; }

        public TodoItemServiceResponse()
        {
        }

        public TodoItemServiceResponse(int httpStatusCode, int returnCode, string message, TodoItemViewModel todoItemViewModel) : base(
            httpStatusCode, returnCode, message)
        {
            TodoItemViewModel = todoItemViewModel;
        }

        public TodoItemServiceResponse(int httpStatusCode, int returnCode, string message) : base(httpStatusCode,
            returnCode, message)
        {
        }

        public TodoItemServiceResponse(int httpStatusCode, string message) : base(httpStatusCode, message)
        {
            ReturnCode = httpStatusCode;
        }

        public TodoItemServiceResponse(TodoItemViewModel todoItemViewModel)
        {
            TodoItemViewModel = todoItemViewModel;
        }
    }
}
