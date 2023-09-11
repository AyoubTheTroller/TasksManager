using TasksManager.controller;
using TasksManager.service;
using TasksManager.Model;
using Terminal.Gui;
public class GuiStateManager
{
    private Window? currentWindow;
    private ConsoleGui gui;
    
    private UserController _userController;

    private TaskController _taskController;

    private ProjectController _projectController;

    private ProjectTaskUserController _projectTaskUserController;

    public GuiStateManager(List<User>? users, List<Project>? projects, List<Taskk>? tasks,UserService? userService,TaskService? taskService,ProjectService? projectService, ProjectTaskUserService? projectTaskUserService){
        _userController = new UserController(userService);
        _taskController = new TaskController(taskService);
        _projectController = new ProjectController(projectService);
        _projectTaskUserController = new ProjectTaskUserController(projectTaskUserService,projectService,userService,taskService);
        gui = new ConsoleGui(users, projects, tasks);
        gui.OnLogin += HandleLogin;
        gui.OnSignup += HandleSignup;
        gui.OnTaskAdded += HandleAddTask;
        gui.OnProjectAdded += HandleAddProject;
        gui.onTaskAddAssociation += HandleTasksAssociation;
    }

    private void HandleAddTask(Taskk task){
        _taskController.UpdateTaskData(task);
        ShowDashboard();
    }

    private void HandleAddProject(Project project){
        _projectController.UpdateProjectData(project);
        ShowDashboard();
    }

    private void HandleTasksAssociation(TaskResult? taskResult){
        string[]? users = taskResult?.AssignedUser?.Split(",");
        string[]? projects = taskResult?.AssignedProject?.Split(",");
        if(users is not null && projects is not null){
            // Associate users with the task
            foreach (var user in users)
            {
                _projectTaskUserController.AddAssociation(new ProJectTaskUser
                {
                    taskId = taskResult?.id,
                    userName = user,
                    projectName = null
                });
            }
            // Associate project with the task
            foreach (var project in projects)
            {
                _projectTaskUserController.AddAssociation(new ProJectTaskUser
                {
                    taskId = taskResult?.id,
                    userName = null,
                    projectName = project
                });
            }
        }
    }

    private void HandleLogin(Result result)
    {
        var actionResult = _userController.ProcessUserInput(result);
        if (actionResult.Status == ActionResultStatus.Success)
        {
            ShowDashboard();
        }
        else
        {
            DisplayError($"Login failed. Reason: {actionResult.Message}");
        }
    }

    private void HandleSignup(Result result)
    {
        var actionResult = _userController.ProcessUserInput(result);
        if (actionResult.Status == ActionResultStatus.Success)
        {
            ShowDashboard();
        }
        else
        {
            DisplayError($"Signup failed. Reason: {actionResult.Message}");
        }
    }

    public void Run()
    {
        Application.Init();
        ShowLoginOrSignup();
        Application.Run();
    }

    public void ShowLoginOrSignup()
    {
        if (currentWindow != null)
        {
            currentWindow.Visible = false;
        }
        
        currentWindow = gui.ShowLoginOrSignup();
        Application.Top.Add(currentWindow);
        currentWindow.Visible = true;
    }

    public void ShowDashboard()
    {
        if (currentWindow != null)
        {
            currentWindow.Visible = false;
        }

        currentWindow = gui.ShowDashboard();
        Application.Top.Add(currentWindow);
        currentWindow.Visible = true;
    }

    public void DisplayError(string message)
    {
        gui.DisplayError(message);
    }

    public Result? getLastLoginSignupResult()
    {
        return gui.LastLoginSignupResult;
    }

}
