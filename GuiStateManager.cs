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

    public GuiStateManager(List<User>? users, List<Project>? projects, List<Taskk>? tasks,UserService? userService,TaskService? taskService,ProjectService? projectService){
        _userController = new UserController(userService);
        _taskController = new TaskController(taskService);
        _projectController = new ProjectController(projectService);
        gui = new ConsoleGui(users, projects, tasks);
        gui.OnLogin += HandleLogin;
        gui.OnSignup += HandleSignup;
        gui.OnTaskAdded += HandleAddTask;
        gui.OnProjectAdded += HandleAddProject;

    }

    private void HandleAddTask(Taskk task){
        _taskController.UpdateTaskData(task);
        ShowDashboard();
    }

    private void HandleAddProject(Project project){
        _projectController.UpdateProjectData(project);
        ShowDashboard();
    }

    private void HandleLogin(Result result)
    {
        var actionResult = _userController.ProcessUserInput(result);
        if (actionResult == UserActionResult.Success)
        {
            ShowDashboard();
        }
        else
        {
            DisplayError("Login failed.");
        }
    }

    private void HandleSignup(Result result)
    {
        var actionResult = _userController.ProcessUserInput(result);
        if (actionResult == UserActionResult.Success)
        {
            ShowDashboard();
        }
        else
        {
            DisplayError("Signup failed.");
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
