Console.WriteLine("Welcome to the Tasks Manager!\nWhat connection type do u prefer?"
                    +"- ServerMode : SM"
                    +"- ConsoleApp : CA");
string? usingType;
while(true){
    usingType = Console.ReadLine();
    if(string.IsNullOrEmpty(usingType)){
        Console.WriteLine("Incorrect or empty input, rety.");
    }
    else{
        switch(usingType){
            case "SM": 
                break;
            case "CA": 
                break;
            default:
                continue;
        }
        break;
    }
}

LaunchApplication appRun = new LaunchApplication(usingType);
appRun.Launch();