using TasksManager.exception;
using TasksManager.Model;
using TasksManager.service;

namespace TasksManager.controller{
    public class UserActionResult{
        public ActionResultStatus Status { get; set; }
        public string Message { get; set; }

        public UserActionResult(ActionResultStatus status, string message)
        {
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
                    authUser.signUp();
                    return new UserActionResult(ActionResultStatus.Success, "Signup successful.");
                }
                else if (userInput.Action == "Login")
                {
                    authUser = new AuthenticateUser(userInput.Username, userInput.Password, _userService);
                    authUser.logIn(userInput.Username, userInput.Password);
                    return new UserActionResult(ActionResultStatus.Success, "Login successful.");
                }
                return new UserActionResult(ActionResultStatus.Failure, "Invalid action.");
            }
            catch (Exception ex) when (ex is InvalidUsernameException || ex is InvalidPasswordException || ex is AuthenticationException)
            {
                return new UserActionResult(ActionResultStatus.Failure, ex.Message);
            }
        }

    }
}
