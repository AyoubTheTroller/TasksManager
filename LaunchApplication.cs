using TasksManager.controller;
using TasksManager.service;

public class LaunchApplication{

    string? usingType;
    private UserService? userService;
    private TaskService? taskService;
    private ProjectService? projectService;
    private DataLoader? dataLoader;
    public LaunchApplication(string? usingType){
        this.usingType = usingType;
        userService = new UserService();
        taskService = new TaskService();
        projectService = new ProjectService();  
        dataLoader = new DataLoader(userService,taskService,projectService);
    }

    public void Launch(){
        switch(usingType){
            case "CA":
                initializeGui();
                break;
            default:
                break;
        }
    }

    public void initializeGui(){
        Console.Clear();
        GuiStateManager guiState = new GuiStateManager(dataLoader?.Users,dataLoader?.Projects,dataLoader?.Tasks);
        guiState.Run();
    }

}