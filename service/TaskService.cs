using System.Text.Json;
using TasksManager.Model;

namespace TasksManager.service{
    public class TaskService{
        public void AddTask(Taskk? task)
        {
            List<Taskk> tasks = ReadTasks();
            int newId = 1;
            if (tasks.Any())
            {
                int maxId = tasks.Max(task => task.Id);
                newId = maxId + 1;
            }
            if(task is not null){
                task.Id = newId;
                tasks.Add(task);
                SaveTasks(tasks);
            }
        }

        public void SaveTasks(List<Taskk> tasks)
        {
            string jsonString = JsonSerializer.Serialize(tasks);
            File.WriteAllText("tasks.json", jsonString); // NOT USING APPEND FOR SEMPLICITY
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

