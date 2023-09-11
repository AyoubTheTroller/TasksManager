using TasksManager.Model;
using TasksManager.service;
using TasksManager.exception;

class AuthenticateUser{
    string? _username;
    string? _password;
    UserService? _userService;
    public AuthenticateUser(string? username, string? password, UserService? userService){
        this._username = username;
        this._password = password;
        _userService = userService;
    }
    public void signUp(){
        _userService?.addUser(_username, _password);
        try{
            _userService?.addUser(_username, _password);
        }
        catch (AuthenticationException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public void logIn(string? username, string? password){
        User? user = _userService?.SearchUser(username);
        if (user != null && password is not null){
            if(!password.Equals(user.Password)){
                throw new InvalidPasswordException();
            }
        }
        else{
            throw new InvalidUsernameException();
        }
    }
}