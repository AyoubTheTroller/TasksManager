using System.ComponentModel;
using System.Xml.Serialization;
using TasksManager.controller;
using TasksManager.Model;
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
                initializeConsoleMenu();
                break;
            default:
                break;
        }
    }

    public void initializeConsoleMenu(){
        Console.Clear();
        ConsoleGui consoleGui = new ConsoleGui(dataLoader?.Users,dataLoader?.Projects,dataLoader?.Tasks);
        UserController userController = new UserController(consoleGui);
        Result? credentials = consoleGui?.ShowLoginOrSignup();
        userController.ProcessUserInput(credentials);
    }
}