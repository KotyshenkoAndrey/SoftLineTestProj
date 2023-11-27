namespace SoftLineTestProj.Controllers
{
    public class TaskModel
    {
        public string taskName { get; set; }
        public string description { get; set; }
        public int statusId { get; set; }
    }
    public class TaskModelUpdate
    {
        public string CurrentTaskName { get; set; }
        public string CurrentTaskNameDescription { get; set; }
        public int CurrentTaskNameStatusId { get; set; }

        public string newTaskName { get; set; }
        public string newDescription { get; set; }
        public int newStatusId { get; set; }
    }
}
