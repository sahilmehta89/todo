using Todo.Core.Model;
using Todo.Core.Model.Dto;

namespace Todo.UnitTests.MockData
{
    public class TodoitemMockData
    {
        public static IEnumerable<TodoItemViewModel> GetTodoItemsViewModel()
        {
            return new List<TodoItemViewModel>{
             new TodoItemViewModel{
                 Id = 10,
                 Title = "Todo 1",
                 IsDone = true
             },
             new TodoItemViewModel{
                 Id = 11,
                 Title = "Todo 2",
                 IsDone = true
             },
             new TodoItemViewModel{
                 Id = 12,
                 Title = "Todo 3",
                 IsDone = false
             }
         };
        }

        public static IEnumerable<TodoItem> GetTodoItems()
        {
            return new List<TodoItem>{
             new TodoItem{
                 Id = 10,
                 Title = "Todo 1",
                 IsDone = true
             },
             new TodoItem{
                 Id = 11,
                 Title = "Todo 2",
                 IsDone = true
             },
             new TodoItem{
                 Id = 12,
                 Title = "Todo 3",
                 IsDone = false
             }
         };
        }
    }
}
