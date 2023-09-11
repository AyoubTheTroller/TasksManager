using TasksManager.Model;
using TasksManager.service;
using TasksManager.exception;

class AuthenticateUser{
    string? _username;
    string? _password;
    UserService? _userService;
    public AuthenticateUser(string? username, string? password, UserService? userService){
        _username = username;
        _password = password;
        _userService = userService;
    }
    public User? signUp(bool? isPremium){
        try{
            return _userService?.addUser(_username, _password, isPremium);
        }
        catch (AuthenticationException ex)
        {   
            Console.WriteLine(ex.Message);
            return null;
        }
    }

    public User? logIn(string? username, string? password){
        User? user = _userService?.SearchUser(username);
        if (user != null && password is not null){
            if(!password.Equals(user.Password)){
                throw new InvalidPasswordException();
            }
        }
        else{
            throw new InvalidUsernameException();
        }
        return user;
    }
}