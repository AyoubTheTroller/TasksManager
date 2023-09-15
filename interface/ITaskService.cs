using TasksManager.Model;
namespace TasksManager.interfaces{
    public interface ITaskService{
        List<Taskk> GetAllTasks();
        Taskk? GetTaskById(int? taskId);
        public List<Taskk> ReadTasks();
        void AddTask(Taskk? task);
        void SaveTasks(List<Taskk> tasks);
        Taskk? UpdateTaskById(Taskk task);
        Taskk? DeleteTaskById(int taskId);
    }

}