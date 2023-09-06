public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the Tasks Manager!\nWhat connection type do u prefer?"
                        + "- ServerMode : SM"
                        + "- ConsoleApp : CA");
        string? usingType;

        while (true)
        {
            usingType = Console.ReadLine();
            if (string.IsNullOrEmpty(usingType))
            {
                Console.WriteLine("Incorrect or empty input, retry.");
            }
            else
            {
                switch (usingType)
                {
                    case "SM":
                        // TODO
                        break;
                    case "CA":
                        LaunchApplication appRun = new LaunchApplication(usingType);
                        appRun.Launch();
                        break;
                    default:
                        Console.WriteLine("Not recognized input format, retry.");
                        continue;
                }
                break;
            }
        }
    }
}