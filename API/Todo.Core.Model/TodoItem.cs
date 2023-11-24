namespace Todo.Core.Model
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? UpdationDate { get; set; }
        public bool IsDone { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
