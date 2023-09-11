using TasksManager.controller;
using TasksManager.Model;
using TasksManager.service;

class Server{

    private UserService? _userService;
    private TaskService? _taskService;
    private ProjectService? _projectService;
    private ProjectTaskUserService? _prokjectTaskUserService;

    public Server(UserService? userService, TaskService? taskService, ProjectService? projectService, ProjectTaskUserService? projectTaskUserService){
        _userService = userService;
        _projectService = projectService;
        _taskService = taskService;
        _prokjectTaskUserService = projectTaskUserService;
    }
    public void start(){
        var builder = WebApplication.CreateBuilder();
        var app = builder.Build();
        UserController userController = new UserController(_userService);
        TaskController taskController = new TaskController(_taskService);
        ProjectController projectController =  new ProjectController(_projectService);
        ProjectTaskUserController projectTaskUserController = new ProjectTaskUserController(_prokjectTaskUserService,_projectService,_userService,_taskService);

        app.MapGet("/tasks/all", () => taskController.GetAllTasks());
        app.MapGet("/tasks", (string username) => projectTaskUserController?.GetTasksByUserName(username));
        app.Run();
    }
}
