public class Result
{
    public string? Action { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }

    public Result(string action, string username, string password)
    {
        Action = action;
        Username = username;
        Password = password;
    }
}