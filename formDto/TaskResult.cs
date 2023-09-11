public class TaskResult
{
    public int? id {get; set;}
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