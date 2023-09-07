using System.Text.Json;
using TasksManager.Model;

namespace TasksManager.service{
    public class TaskService{
    public void AddTask(Taskk task)
    {
        List<Taskk> tasks = ReadTasks();
        tasks.Add(task);
        SaveTasks(tasks);
    }

    public void SaveTasks(List<Taskk> tasks)
    {
        string jsonString = JsonSerializer.Serialize(tasks);
        File.WriteAllText("tasks.json", jsonString);
    }

    public List<Taskk> ReadTasks()
    {
        if (File.Exists("tasks.json"))
        {
            string jsonString = File.ReadAllText("tasks.json");
            return JsonSerializer.Deserialize<List<Taskk>>(jsonString) ?? new List<Taskk>();
        }

        return new List<Taskk>();
    }
}
}

