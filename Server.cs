using Microsoft.EntityFrameworkCore;
using TasksManager.controller;
using TasksManager.Model;
using TasksManager.Repository;
using TasksManager.service;
using TasksManager.interfaces;
class Server
{
    // Controllers
    private readonly TaskController _taskController;
    private readonly ProjectTaskUserController _projectTaskUserController;
    private readonly AttachmentController _attachmentController;

    private readonly InMemoryDb _dbContext;

    public Server(IServiceCollection services)
    {
        services.AddDbContext<InMemoryDb>(options => options.UseInMemoryDatabase("TasksManager"));

        // DI
        services.AddSingleton<UserService>();
        services.AddSingleton<ITaskService, TaskService>();
        services.AddSingleton<ProjectService>();
        services.AddSingleton<IAttachmentRepository, AttachmentRepository>();
        services.AddSingleton<IAttachmentService, AttachmentService>();
        services.AddSingleton<ProjectTaskUserService>();
        services.AddSingleton<UserController>();
        services.AddSingleton<TaskController>();
        services.AddSingleton<ProjectController>();
        services.AddSingleton<ProjectTaskUserController>();
        services.AddSingleton<AttachmentController>();

        var serviceProvider = services.BuildServiceProvider(); // Building the service provider

        _dbContext = serviceProvider.GetRequiredService<InMemoryDb>();
        _taskController = serviceProvider.GetRequiredService<TaskController>();
        _projectTaskUserController = serviceProvider.GetRequiredService<ProjectTaskUserController>();
        _attachmentController = serviceProvider.GetRequiredService<AttachmentController>();
    }

    public void Start()
    {
        var builder = WebApplication.CreateBuilder();
        var app = builder.Build();
        
        app.MapGet("/tasks/all", () => _taskController.GetAllTasks());
        app.MapGet("/tasks", (string username) => _projectTaskUserController.GetTasksByUserName(username));
        app.MapGet("/task/{id}", (int id) => _taskController.getTaskkById(id));
        app.MapPut("/task/update", (Taskk toBeUpdated) => _taskController.updateTaskById(toBeUpdated));
        app.MapDelete("/task/delete/{id}", (int id) => _taskController.deleteTaskById(id));
        app.MapPut("/attachment", (Attachment attachment) => _attachmentController.addAttachment(attachment));
        app.Run();
    }
}

