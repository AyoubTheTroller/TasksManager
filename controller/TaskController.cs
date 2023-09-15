using TasksManager.Model;
using TasksManager.service;
using TasksManager.interfaces;

namespace TasksManager.controller{

    public class TaskController
    {
        private ITaskService? _taskService;

        public TaskController(ITaskService? taskService){
            _taskService = taskService;
        }

        public void UpdateTaskData(Taskk? taskk){
            _taskService?.AddTask(taskk);
        }
        public List<Taskk> GetAllTasks()
        {
            return _taskService?.GetAllTasks() ?? new List<Taskk>();
        }

        public Taskk? getTaskkById(int id){
            return _taskService?.GetTaskById(id);
        }

        public Taskk? updateTaskById(Taskk taskToBeUpdated){
            return _taskService?.UpdateTaskById(taskToBeUpdated);
        }

        public Taskk? deleteTaskById(int id){
            return _taskService?.DeleteTaskById(id);
        }
    }
}
