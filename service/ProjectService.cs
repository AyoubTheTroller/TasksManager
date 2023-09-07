using System.Text.Json;
using TasksManager.Model;

public class ProjectService
{
    public void AddProject(Project project)
    {
        List<Project> projects = ReadProjects();
        projects.Add(project);
        SaveProjects(projects);
    }

    public void SaveProjects(List<Project> projects)
    {
        string jsonString = JsonSerializer.Serialize(projects);
        File.WriteAllText("projects.json", jsonString);
    }

    public List<Project> ReadProjects()
    {
        if (File.Exists("projects.json"))
        {
            string jsonString = File.ReadAllText("projects.json");
            return JsonSerializer.Deserialize<List<Project>>(jsonString) ?? new List<Project>();
        }

        return new List<Project>();
    }
}
