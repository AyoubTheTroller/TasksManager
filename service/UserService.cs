using System.Text.Json;
using TasksManager.Model;
using TasksManager.exception;

namespace TasksManager.service{
    public class UserService{
        public void addUser(string? username, string? password)
        {
            List<User> users = ReadUsers();

            if (users.Any(u => u.Username == username))
            {
                throw new AuthenticationException("User already exists");
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

        public User? SearchUser(string? username){
            if (string.IsNullOrEmpty(username))
            {
                return null;
            }
            if (File.Exists("users.json"))
            {
                string jsonString = File.ReadAllText("users.json");
                var users = JsonSerializer.Deserialize<List<User>>(jsonString) ?? new List<User>();
                return users.FirstOrDefault(u => u.Username == username);
            }

            return null;
        }

    }
}