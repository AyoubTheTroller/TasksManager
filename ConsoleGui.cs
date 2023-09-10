using TasksManager.Model;
using TasksManager.service;
using Terminal.Gui;
public class ConsoleGui
{   
    public event Action<Result>? OnLogin;
    public event Action<Result>? OnSignup;
    public event Action<Taskk>? OnTaskAdded;
    public event Action<Project>? OnProjectAdded;

    private List<User>? users;
    private List<Project>? projects;
    private List<Taskk>? tasks;

    private ListView? tasksListView; 

    private ListView? projectsListView;
    
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
        ShowExitButton(win);
        ShowAddProjectForm(win);
        ShowProjectList(win);
        return win;        
    }

    private void ShowAddProjectForm(Window win){
        int yOffset = 3;

        var lblAddProject = new Label(100, yOffset - 2, "Add Project:");

        var lblProjectName = new Label(100, yOffset, "Project Name:");
        var txtProjectName = new TextField(115, yOffset, 40, "");
        win.Add(lblAddProject, lblProjectName, txtProjectName);

        var lblCreationDate = new Label(100, yOffset + 2, "Creation Date:");
        var creationDatePicker = new DateField(115, yOffset + 2, DateTime.Now);
        win.Add(lblCreationDate, creationDatePicker);

        var btnCreateProject = new Button(100, yOffset + 4, "Create Project");
        btnCreateProject.Clicked += () =>
        {
            string? projectName = txtProjectName.Text.ToString();

            if (string.IsNullOrEmpty(projectName))
            {
                MessageBox.ErrorQuery("Error", "Please fill in all fields for the project.", "Ok");
                return;
            }

            var newProject = new Project
            {
                name = projectName,
                CreationDate = creationDatePicker.Date
            };

            projects?.Add(newProject);
            OnProjectAdded?.Invoke(newProject);
            MessageBox.Query("Success", "Project has been added successfully.", "Ok");
        };

        win.Add(btnCreateProject);
    }

    private void ShowExitButton(Window win){
        int yPos = 12 + (users?.Count ?? 0) + (projects?.Count ?? 0);
        
        var btnExit = new Button(1, yPos, "Exit");
        btnExit.Clicked += () => 
        {
            Application.RequestStop();
        };
        win.Add(btnExit);
    }


    private void ShowAddTaskForm(Window win){
        var lblAddTask = new Label(1, 1, "Add new task");
        var lblTaskName = new Label(1, 2, "Task Description:");
        var txtTaskName = new TextField(19, 2, 40, "");
        win.Add(lblAddTask, lblTaskName, txtTaskName);

        var lblAssignedUser = new Label(1, 4, "Assign to User:");
        win.Add(lblAssignedUser);

        var userCheckboxes = new List<CheckBox>();
        for (int i = 0; i < users?.Count; i++)
        {
            var checkbox = new CheckBox(19, 4 + i, users?[i].Username ?? "");
            userCheckboxes.Add(checkbox);
            win.Add(checkbox);
        }

        var lblAssignedProject = new Label(1, 6 + (users?.Count ?? 0), "Assign to Project:");
        win.Add(lblAssignedProject);

        var projectCheckboxes = new List<CheckBox>();
        for (int i = 0; i < projects?.Count; i++)
        {
            var checkbox = new CheckBox(19, 6 + (users?.Count ?? 0) + i, projects?[i].name ?? "");
            projectCheckboxes.Add(checkbox);
            win.Add(checkbox);
        }

        var lblExpirationDate = new Label(1, 8 + (users?.Count ?? 0) + (projects?.Count ?? 0), "Expiration Date:");
        var expirationDatePicker = new DateField(19, 8 + (users?.Count ?? 0) + (projects?.Count ?? 0), DateTime.Now);
        win.Add(lblExpirationDate, expirationDatePicker);

        var btnCreateTask = new Button(1, 10 + (users?.Count ?? 0) + (projects?.Count ?? 0), "Create Task");
        btnCreateTask.Clicked += () =>
        {
            var selectedUsers = userCheckboxes.Where(uc => uc.Checked).Select(uc => uc.Text.ToString()).ToList();
            var selectedProjects = projectCheckboxes.Where(pc => pc.Checked).Select(pc => pc.Text.ToString()).ToList();
            TaskResult? taskData = HandleBtnCreateTask(txtTaskName, selectedUsers, selectedProjects);
            if (taskData != null) 
            {
                var newTask = new Taskk
                {
                    Description = taskData.TaskName,
                    CreationDate = DateTime.Now,
                    ExpirationDate = expirationDatePicker.Date
                };
                tasks?.Add(newTask);
                tasksListView?.SetSource(tasks?.Select(t => t.Description).ToList());
                
                var projectTaskUserService = new ProJectTaskUserService();
                // Associate users with the task
                foreach (var user in selectedUsers)
                {
                    projectTaskUserService.AddAssociation(new ProJectTaskUser
                    {
                        taskId = newTask.Id,
                        userName = user,
                        projectName = null
                    });
                }
                // Associate project with the task
                foreach (var project in selectedProjects)
                {
                    projectTaskUserService.AddAssociation(new ProJectTaskUser
                    {
                        taskId = newTask.Id,
                        userName = null,
                        projectName = project
                    });
                }
                OnTaskAdded?.Invoke(newTask);  
                MessageBox.Query("Success", "Task has been added successfully.", "Ok");
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

    private void ShowProjectList(Window win){
        int yOffset = 15; 
        var lblProjects = new Label(60, yOffset, "Projects:");
        projectsListView = new ListView(new Rect(60, yOffset + 2, 40, 10), projects?.Select(p => p.name).ToArray());
        win.Add(lblProjects, projectsListView);
    }

    public Window ShowLoginOrSignup(){

        var win = new Window("Login or Signup")
        {
            X = 0,
            Y = 1,
            Width = Dim.Fill(), // Default behaviour for the Window Class, so it is not needed actually
            Height = Dim.Fill() // Default behaviour for the Window Class, so it is not needed actually
        };
        LoginOrSignup(win);
        return win;
    }

    private void LoginOrSignup(Window win){
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
        // Exit button
        var btnExit = new Button(1, 11, "Exit");
        btnExit.Clicked += () => 
        {
            Application.RequestStop();
        };
        win.Add(btnExit);
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

