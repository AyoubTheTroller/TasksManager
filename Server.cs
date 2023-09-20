using Microsoft.EntityFrameworkCore;
using TasksManager.controller;
using TasksManager.Model;
using TasksManager.Repository;
using TasksManager.service;
using TasksManager.interfaces;
class Server
{
    private WebApplicationBuilder _builder; 
    public Server(WebApplicationBuilder builder)
    {   
        _builder = builder;
        var services = _builder.Services;
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
    }

    public void Start()
    {
        var app = _builder.Build();
        
        app.MapGet("/tasks/all", (TaskController taskController) => taskController.GetAllTasks());
        app.MapGet("/tasks", (string username, ProjectTaskUserController projectTaskUserController) => projectTaskUserController.GetTasksByUserName(username));
        app.MapGet("/task/{id}", (int id, TaskController taskController) => taskController.getTaskkById(id));
        app.MapPut("/task/update", (Taskk toBeUpdated, TaskController taskController) => taskController.updateTaskById(toBeUpdated));
        app.MapDelete("/task/delete/{id}", (int id, TaskController taskController) => taskController.deleteTaskById(id));
        app.MapPut("/attachment", (Attachment attachment, AttachmentController attachmentController) => attachmentController.addAttachment(attachment));
        app.Run();
    }
}

