using TasksManager.controller;
using TasksManager.Model;
using Terminal.Gui;
public class GuiStateManager
{
    private Window? currentWindow;
    private ConsoleGui gui;
    private UserController userController = new UserController();

    public GuiStateManager(List<User>? users, List<Project>? projects, List<Taskk>? tasks)
    {
        gui = new ConsoleGui(users, projects, tasks);
        gui.OnLogin += HandleLogin;
        gui.OnSignup += HandleSignup;
        gui.OnTaskAdded += HandleTaskAdded;

    }

    private void HandleTaskAdded(TaskResult taskResult){
        MessageBox.Query("Success", "Task has been added successfully.", "Ok");
    }



    private void HandleLogin(Result result)
    {
        var actionResult = userController.ProcessUserInput(result);
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
        var actionResult = userController.ProcessUserInput(result);
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
