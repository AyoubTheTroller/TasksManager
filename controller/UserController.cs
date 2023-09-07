using TasksManager.Model;
using TasksManager.service;

namespace TasksManager.controller{
    public class UserController
    {
        private AuthenticateUser? authUser;
        private UserService userService;
        private ConsoleGui view;

        public UserController(ConsoleGui guiView)
        {
            userService = new UserService();
            view = guiView;
        }

        public void ProcessUserInput(Result? userInput)
        {
            if (userInput?.Action == "Signup")
            {
                try
                {
                    authUser = new AuthenticateUser(userInput.Username, userInput.Password);
                    authUser.signUp();
                    view.ShowDashboard();
                }
                catch (InvalidOperationException ex)
                {
                    view.DisplayError(ex.Message);
                }
            }
        }
    }
}
