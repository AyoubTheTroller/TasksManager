using TasksManager.Model;
using TasksManager.service;
using TasksManager.interfaces;

namespace TasksManager.controller{

    public class ProjectTaskUserController{
        private ProjectTaskUserService? _projectTaskUserService;
        private ProjectService? _projectService;
        private UserService? _userService;

        private ITaskService? _taskService;
        public ProjectTaskUserController(ProjectTaskUserService? projectTaskUserService, ProjectService? projectService, UserService? userService, ITaskService? taskService){
            _taskService = taskService;
            _projectTaskUserService = projectTaskUserService;
            _projectService = projectService;
            _userService = userService;
        }

        public object GetTasksByUserName(string? username){
            List<ProJectTaskUser>? tasks = _projectTaskUserService?.ReadAssociations();
            List<Taskk> tasksByUsername = new List<Taskk>();
            if(tasks is not null){
                foreach(var task in tasks){
                    if(task.userName is not null){
                        if(task.userName.Equals(username)){
                            Taskk? toBeAdded = _taskService?.GetTaskById(task.taskId);
                            if(toBeAdded is not null){
                                tasksByUsername.Add(toBeAdded);
                            }
                        }
                    }
                }
            }
            return Results.Ok(tasksByUsername);
        }

        public void AddAssociation(ProJectTaskUser association){
            _projectTaskUserService?.AddAssociation(association);
        }
    }
}
