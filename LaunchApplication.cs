using TasksManager.controller;
using TasksManager.service;

public class LaunchApplication{

    string? usingType;
    private UserService? _userService;
    private TaskService? _taskService;
    private ProjectService? _projectService;

    private ProjectTaskUserService _prokjectTaskUserService;
    private DataLoader? dataLoader;
    public LaunchApplication(string? usingType){
        this.usingType = usingType;
        _userService = new UserService();
        _taskService = new TaskService();
        _projectService = new ProjectService();  
        _prokjectTaskUserService = new ProjectTaskUserService();
        dataLoader = new DataLoader(_userService,_taskService,_projectService);
    }

    public void Launch(){
        switch(usingType){
            case "CA":
                initializeGui();
                break;
            case "SM":
                initializeServer();
                break;
            default:
                break;
        }
    }

    public void initializeGui(){
        Console.Clear();
        GuiStateManager guiState = new GuiStateManager(dataLoader?.Users,dataLoader?.Projects,dataLoader?.Tasks,_userService,_taskService,_projectService,_prokjectTaskUserService);
        guiState.Run();
    }

    public void initializeServer(){
        Console.Clear();
        var services = new ServiceCollection();
        var server = new Server(services);
        server.Start();

    }

}