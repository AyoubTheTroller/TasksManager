namespace TasksManager.exception{
    public class AuthenticationException : Exception{
        public AuthenticationException(string message) : base(message) { }
    }
    public class InvalidUsernameException : AuthenticationException{
        public InvalidUsernameException() : base("The username is invalid.") { }
    }

    public class InvalidPasswordException : AuthenticationException{
        public InvalidPasswordException() : base("The password is incorrect.") { }
    }
}