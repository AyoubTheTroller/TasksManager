using TasksManager.exception;
using TasksManager.Model;
using TasksManager.service;

namespace TasksManager.controller{
    public class UserActionResult{
        public ActionResultStatus Status { get; set; }
        public string Message { get; set; }

        public User User {get; set;}

        public UserActionResult(ActionResultStatus status, string message, User user)
        {
            User = user;
            Status = status;
            Message = message;
        }
    }

    public enum ActionResultStatus
    {
        Success,
        Failure
    }


    public class UserController
    {
        private AuthenticateUser? authUser;
        private UserService? _userService;

        public UserController(UserService? userService)
        {
            _userService = userService;
        }

        public UserActionResult ProcessUserInput(Result userInput){
            try
            {
                if (userInput.Action == "Signup")
                {
                    authUser = new AuthenticateUser(userInput.Username, userInput.Password, _userService);
                    User? user = authUser.signUp(userInput.IsPremium);
                    if(user is not null) return new UserActionResult(ActionResultStatus.Success, "Signup successful.",user);
                    else return new UserActionResult(ActionResultStatus.Failure, "Signup failed.", new User());
                }
                else if (userInput.Action == "Login")
                {
                    authUser = new AuthenticateUser(userInput.Username, userInput.Password, _userService);
                    User? user = authUser.logIn(userInput.Username, userInput.Password);
                    if(user is not null) return new UserActionResult(ActionResultStatus.Success, "Login successful.",user);
                    else return new UserActionResult(ActionResultStatus.Failure, "Login failed.", new User());
                }
                return new UserActionResult(ActionResultStatus.Failure, "Invalid action.",new User());
            }
            catch (Exception ex) when (ex is InvalidUsernameException || ex is InvalidPasswordException || ex is AuthenticationException)
            {
                return new UserActionResult(ActionResultStatus.Failure, ex.Message,new User());
            }
        }

    }
}
