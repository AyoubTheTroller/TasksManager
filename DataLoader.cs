using TasksManager.Model;
using TasksManager.service;

public class DataLoader
{
    private UserService? userService;
    private TaskService? taskService;
    private ProjectService? projectService;
    private List<User>? users;
    private List<Taskk>? tasks;
    private List<Project>? projects;

    public DataLoader(UserService? userService, TaskService? taskService, ProjectService? projectService)
    {
        this.userService = userService;
        this.taskService = taskService;
        this.projectService = projectService;
        loadInitialData();
    }

    private void loadInitialData()
    {
        users = userService?.ReadUsers();
        projects = projectService?.ReadProjects();
        tasks = taskService?.ReadTasks();
    }

    public void LoadUsers()
    {
        users = userService?.ReadUsers();
    }

    public void LoadProjects()
    {
        projects = projectService?.ReadProjects();
    }

    public void LoadTasks()
    {
        tasks = taskService?.ReadTasks();
    }

    public List<User>? Users 
    {
        get { return users; }
    }

    public List<Taskk>? Tasks 
    {
        get { return tasks; }
    }

    public List<Project>? Projects 
    {
        get { return projects; }
    }
}
