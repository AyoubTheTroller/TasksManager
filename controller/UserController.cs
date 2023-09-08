using TasksManager.Model;
using TasksManager.service;

namespace TasksManager.controller{
    public enum UserActionResult{
        Success,
        Failure
    }

    public class UserController
    {
        private AuthenticateUser? authUser;
        private UserService? userService;

        public UserController()
        {
            userService = new UserService();
        }

        public UserActionResult ProcessUserInput(Result userInput)
        {
            try
            {
                if (userInput.Action == "Signup")
                {
                    authUser = new AuthenticateUser(userInput.Username, userInput.Password);
                    authUser.signUp();
                    return UserActionResult.Success;
                }
                else if (userInput.Action == "Login")
                {
                    authUser = new AuthenticateUser(userInput.Username, userInput.Password);
                    authUser.logIn(userInput.Username, userInput.Password);
                    return UserActionResult.Success;
                }
                return UserActionResult.Failure;
            }
            catch (InvalidOperationException)
            {
                return UserActionResult.Failure;
            }
        }
    }
}
