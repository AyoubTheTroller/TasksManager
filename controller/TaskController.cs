using TasksManager.Model;
using TasksManager.service;

namespace TasksManager.controller{

    public class TaskController
    {
        private TaskService? _taskService;

        public TaskController(TaskService? taskService){
            _taskService = taskService;
        }

        public void UpdateTaskData(Taskk? taskk){
            _taskService?.AddTask(taskk);
        }
        
    }
}
