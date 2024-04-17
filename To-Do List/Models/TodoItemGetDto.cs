namespace To_Do_List.Models
{
    public class TodoItemGetDto
    {
        public int id_todo_item { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int UserId { get; set; }
    }
}
