using System.Text.Json;
using TasksManager.Model;
using TasksManager.exception;

namespace TasksManager.service{
    public class UserService{
        public User addUser(string? username, string? password, bool? isPremium){
            List<User> users = ReadUsers();
            User newUser;
            if (users.Any(u => u.Username == username))
            {
                throw new AuthenticationException("User already exists");
            }
            if(isPremium == true && isPremium is not null){
                newUser = new PremiumUser { Username = username, Password = password };
            }
            else{
                newUser = new User { Username = username, Password = password };
            }
            users.Add(newUser);
            SaveUsers(users);
            return newUser;
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
                var usersData = JsonSerializer.Deserialize<List<Dictionary<string, JsonElement>>>(jsonString) ?? new List<Dictionary<string, JsonElement>>();
                var userData = usersData.FirstOrDefault(u => u["Username"].GetString() == username);
                                
                if (userData == null) return null;
                                
                if (userData["UserType"].GetString() == "Premium"){
                    return new PremiumUser { Username = userData["Username"].GetString(), Password = userData["Password"].GetString() };
                }
                else{
                    return new User { Username = userData["Username"].GetString(), Password = userData["Password"].GetString() };
                }
            }
            return null;
        }


    }
}