using System.ComponentModel;
using System.Xml.Serialization;
using TasksManager.Model;

public class LaunchApplication{

    string? usingType;
    public LaunchApplication(string? usingType){
        this.usingType = usingType;
    }

    public void Launch(){
        switch(usingType){
            case "CA":
                initializeConsoleMenu();
                break;
            default:
                break;
        }
    }

    public void initializeConsoleMenu(){
        Console.Clear();
        ConsoleGui consoleGui = new ConsoleGui();
        Result? credentials = consoleGui.ShowLoginOrSignup();
        AuthenticateUser authenticateUser = new AuthenticateUser(credentials?.Username ,credentials?.Password);
        if(credentials?.Action == "Signup")
            authenticateUser.signUp();
    }

    /*public void initializeConsoleMenu(){
        Console.Clear();
        Console.WriteLine(loginOrSignup());
        manageUserFirstInput();
    }

    public void manageUserFirstInput(){
        while(true){
            string? userCredentialsInput = Console.ReadLine();
            if(string.IsNullOrEmpty(userCredentialsInput)){
                Console.WriteLine("Incorrect or empty input, retry.");
            }
            else{
                string[] credentialsParts = userCredentialsInput.Split('/');
                switch(credentialsParts[0]){
                    case "login": 

                        break;
                    case "signup": 
                        if(credentialsParts.Length == 4){
                            if(!credentialsParts[2].Equals(credentialsParts[3])){
                                Console.WriteLine("Credentials entered not the same, retry");
                                continue;
                            }
                            else{
                                AuthenticateUser authenticateUser = new AuthenticateUser(credentialsParts[1],credentialsParts[2]);
                                authenticateUser.signUp();
                            }
                        }
                        break;
                    default:
                        continue;
                }
                break;
            }
        }
    }*/

    public string? loginOrSignup(){
        return "Login or signup?\n"
            + "Login -> type: login/yourUsername/yourPassword\n"
            + "Signup -> type: signup/yourUsername/yourPassword/repeatPassword\n";
    }
}