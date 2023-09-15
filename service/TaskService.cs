using System.Text.Json;
using TasksManager.Model;
using TasksManager.interfaces;

namespace TasksManager.service{
    public class TaskService : ITaskService{
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

        public List<Taskk> GetAllTasks()
        {
            return ReadTasks();
        }

        public Taskk? GetTaskById(int? id){
            List<Taskk> allTasks = ReadTasks();
            foreach(var task in allTasks){
                if(task.Id.Equals(id)){
                    return task;
                }
            }
            return null;
        }

        public Taskk? UpdateTaskById(Taskk updatedTask){
            List<Taskk> tasks = ReadTasks();
            var toBeUpdated = tasks.FirstOrDefault(t => t.Id == updatedTask.Id);
            if (toBeUpdated is not null)
            {
                toBeUpdated.Description = updatedTask.Description;
                SaveTasks(tasks);
                return toBeUpdated;
            }
            return null;
        }

        public Taskk? DeleteTaskById(int id){
            List<Taskk> taskks = ReadTasks();
            var toBeDeleted = taskks.FirstOrDefault(t=>t.Id == id);
            if(toBeDeleted is not null){
                taskks.Remove(toBeDeleted);
                SaveTasks(taskks);
                return toBeDeleted;
            }
            return null;
        }

    }
}

