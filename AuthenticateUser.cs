using TasksManager.Model;

class AuthenticateUser{
    string? _username;
    string? _password;
    UserService? userService;
    public AuthenticateUser(string? username, string? password){
        this._username = username;
        this._password = password;
    }
    public void signUp(){
        userService = new UserService();
        userService.addUser(_username, _password);
    }
}