using TasksManager.controller;
using TasksManager.Model;
using TasksManager.service;
using Terminal.Gui;
public class ConsoleGui
{   
    public event Action<Result> OnLogin;
    public event Action<Result> OnSignup;
    public event Action<TaskResult> OnTaskAdded;

    private List<User>? users;
    private List<Project>? projects;
    private List<Taskk>? tasks;

    private ListView? tasksListView; 
    public Result? LastLoginSignupResult { get; private set; }

    public ConsoleGui(List<User>? users, List<Project>? projects, List<Taskk>? tasks)
    {
        this.users = users;
        this.projects = projects;
        this.tasks = tasks;
    }

    public ConsoleGui(){}

    public Window ShowDashboard()
    {
        var win = new Window("Dashboard")
        {
            X = 0,
            Y = 1,
            Width = Dim.Fill(),
            Height = Dim.Fill()
        };
        ShowAddTaskForm(win);
        ShowTaskList(win);
        return win;        
    }

    private void ShowAddTaskForm(Window win){
        var lblTaskName = new Label(1, 2, "Task Description:");
        var txtTaskName = new TextField(15, 2, 40, "");
        win.Add(lblTaskName, txtTaskName);

        var lblAssignedUser = new Label(1, 4, "Assign to User:");
        win.Add(lblAssignedUser);

        var userCheckboxes = new List<CheckBox>();
        for (int i = 0; i < users?.Count; i++)
        {
            var checkbox = new CheckBox(15, 4 + i, users?[i].Username ?? "");
            userCheckboxes.Add(checkbox);
            win.Add(checkbox);
        }

        var lblAssignedProject = new Label(1, 6 + (users?.Count ?? 0), "Assign to Project:");
        win.Add(lblAssignedProject);

        var projectCheckboxes = new List<CheckBox>();
        for (int i = 0; i < projects?.Count; i++)
        {
            var checkbox = new CheckBox(15, 6 + (users?.Count ?? 0) + i, projects?[i].name ?? "");
            projectCheckboxes.Add(checkbox);
            win.Add(checkbox);
        }

        var lblExpirationDate = new Label(1, 8 + (users?.Count ?? 0) + (projects?.Count ?? 0), "Expiration Date:");
        var expirationDatePicker = new DateField(15, 8 + (users?.Count ?? 0) + (projects?.Count ?? 0), DateTime.Now);
        win.Add(lblExpirationDate, expirationDatePicker);

        var btnCreateTask = new Button(1, 10 + (users?.Count ?? 0) + (projects?.Count ?? 0), "Create Task");
        btnCreateTask.Clicked += () =>
        {
            var selectedUsers = userCheckboxes.Where(uc => uc.Checked).Select(uc => uc.Text.ToString()).ToList();
            var selectedProjects = projectCheckboxes.Where(pc => pc.Checked).Select(pc => pc.Text.ToString()).ToList();
            TaskResult? taskData = HandleBtnCreateTask(txtTaskName, selectedUsers, selectedProjects);
            if (taskData != null) {
                var newTask = new Taskk
                {
                    Description = taskData.TaskName,
                    CreationDate = DateTime.Now,
                    ExpirationDate = expirationDatePicker.Date
                };
                tasks?.Add(newTask);
                tasksListView?.SetSource(tasks?.Select(t => t.Description).ToList());
                new TaskService().AddTask(newTask);

                OnTaskAdded?.Invoke(taskData);
            }
        };
        win.Add(btnCreateTask);
    }

    private TaskResult? HandleBtnCreateTask(TextField txtTaskName, List<string?> assignedUsers, List<string?> assignedProjects){
        string? taskName = txtTaskName.Text.ToString();

        if (string.IsNullOrEmpty(taskName))
        {
            MessageBox.ErrorQuery("Error", "Please fill in all fields and select at least one user and one project.", "Ok");
            return null;
        }

        string usersString = string.Join(", ", assignedUsers);
        string projectsString = string.Join(", ", assignedProjects);

        return new TaskResult(taskName, usersString, projectsString);
    }



    private void ShowTaskList(Window win){
        var lblTasks = new Label(60, 2, "Tasks:");
        tasksListView = new ListView(new Rect(60, 4, 40, 10), tasks?.Select(t => t.Description).ToArray());
        win.Add(lblTasks, tasksListView);
    }

    public Window ShowLoginOrSignup(){

        var win = new Window("Login or Signup")
        {
            X = 0,
            Y = 1,
            Width = Dim.Fill(), // Default behaviour for the Window Class, so it is not needed actually
            Height = Dim.Fill() // Default behaviour for the Window Class, so it is not needed actually
        };

        var radioGroup = new RadioGroup(1, 1, new NStack.ustring[] { "_Login", "_Signup" });
        win.Add(radioGroup);

        var lblUsername = new Label(1, 3, "Username:");
        var txtUsername = new TextField(15, 3, 40, "");
        win.Add(lblUsername, txtUsername);

        var lblPassword = new Label(1, 5, "Password:");
        var txtPassword = new TextField(15, 5, 40, "") { Secret = true };
        win.Add(lblPassword, txtPassword);

        var lblConfirmPassword = new Label(1, 7, "Confirm Password:");
        var txtConfirmPassword = new TextField(15, 7, 40, "") { Secret = true, Visible = false };
        lblConfirmPassword.Visible = false;
        win.Add(lblConfirmPassword, txtConfirmPassword);

        Result? result = null;

        var btnAction = new Button(1, 9, "Login");
        btnAction.Clicked += () =>
        {
            HandleBtnSignupAndLogicAction(ref result, radioGroup, txtUsername, txtPassword, txtConfirmPassword);
        };
        win.Add(btnAction);

        radioGroup.SelectedItemChanged += (prev) =>
        {
            if (radioGroup.SelectedItem == 0)
            {
                btnAction.Text = "Login";
                lblConfirmPassword.Visible = false;
                txtConfirmPassword.Visible = false;
            }
            else
            {
                btnAction.Text = "Signup";
                lblConfirmPassword.Visible = true;
                txtConfirmPassword.Visible = true;
            }
        };

        return win;
    }

    private void HandleBtnSignupAndLogicAction(ref Result? result, RadioGroup radioGroup, TextField txtUsername, TextField txtPassword, TextField txtConfirmPassword){
        string? username = txtUsername.Text.ToString();
        string? password = txtPassword.Text.ToString();

        if (radioGroup.SelectedItem == 0)
        {
            if(username is not null && password is not null)
            {
                result = new Result("Login", username, password);
                LastLoginSignupResult = result;
                OnLogin?.Invoke(result);
            }
            else
            {
                MessageBox.ErrorQuery("Error", "username and password might null", "Ok");
                return;
            }
        }
        else
        {
            string? confirmPassword = txtConfirmPassword.Text.ToString();
            if (password != confirmPassword)
            {
                MessageBox.ErrorQuery("Error", "Passwords do not match!", "Ok");
                return;
            }
            if(username is not null && password is not null)
            {
                result = new Result("Signup", username, password);
                LastLoginSignupResult = result;
                OnSignup?.Invoke(result);
            }
            else
            {
                MessageBox.ErrorQuery("Error", "username and password might null", "Ok");
                return;
            }
        }
    }

    
    public void DisplayError(string errorMessage){
        MessageBox.ErrorQuery("Error", errorMessage, "Ok");
    }

    
}

public class Result
{
    public string? Action { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }

    public Result(string action, string username, string password)
    {
        Action = action;
        Username = username;
        Password = password;
    }
}

public class TaskResult
{
    public string? TaskName { get; set; }
    public string? AssignedUser { get; set; }
    public string? AssignedProject { get; set; }

    public TaskResult(string taskName, string assignedUser, string assignedProject)
    {
        TaskName = taskName;
        AssignedUser = assignedUser;
        AssignedProject = assignedProject;
    }
}

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
