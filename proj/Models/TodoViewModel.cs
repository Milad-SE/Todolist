namespace proj.Models;

    public class TodoViewModel
    {
        public TaskItem NewTask { get; set; } = new TaskItem();
        public List<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    }
