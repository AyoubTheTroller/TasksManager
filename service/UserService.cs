using System.Text.Json;

namespace TasksManager.Model{
        public class UserService
    {
        public void addUser(string? username, string? password)
        {
            List<User> users = ReadUsers();

            if (users.Any(u => u.Username == username))
            {
                Console.WriteLine($"A user with the username '{username}' already exists.");
                return;
            }

            var newUser = new User { Username = username, Password = password };
            users.Add(newUser);
            SaveUsers(users);
        }

        public void SaveUsers(List<User> users)
        {
            string jsonString = JsonSerializer.Serialize(users);
            File.WriteAllText("users.json", jsonString);
        }

        public List<User> ReadUsers()
        {
            if (File.Exists("users.json"))
            {
                string jsonString = File.ReadAllText("users.json");
                return JsonSerializer.Deserialize<List<User>>(jsonString) ?? new List<User>();
            }

            return new List<User>();
        }
    }
}