using System.Text.Json;
using TasksManager.Model;

namespace TasksManager.service
{
    public class ProjectTaskUserService
    {
        public void AddAssociation(ProJectTaskUser association)
        {
            List<ProJectTaskUser> associations = ReadAssociations();
            associations.Add(association);
            SaveAssociations(associations);
        }

        public void SaveAssociations(List<ProJectTaskUser> associations)
        {
            string jsonString = JsonSerializer.Serialize(associations);
            File.WriteAllText("associations.json", jsonString);
        }

        public List<ProJectTaskUser> ReadAssociations()
        {
            if (File.Exists("associations.json"))
            {
                string jsonString = File.ReadAllText("associations.json");
                return JsonSerializer.Deserialize<List<ProJectTaskUser>>(jsonString) ?? new List<ProJectTaskUser>();
            }

            return new List<ProJectTaskUser>();
        }
    }
}
