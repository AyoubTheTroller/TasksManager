using TasksManager.Model;
using TasksManager.service;

class AuthenticateUser{
    string? _username;
    string? _password;
    UserService? userService;
    public AuthenticateUser(string? username, string? password){
        this._username = username;
        this._password = password;
        userService = new UserService();
    }
    public void signUp(){
        userService?.addUser(_username, _password);
        try{
            userService?.addUser(_username, _password);
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public void logIn(string? username, string? password){
        User? user = userService?.SearchUser(username);
        if (user != null && password is not null){
            if(!password.Equals(user.Password)){
                throw new InvalidOperationException("Wrong Password, retry");
            }
        }
        else{
            throw new InvalidOperationException("User not found");
        }
    }
}