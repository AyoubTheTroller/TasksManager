namespace TasksManager.Model{
    public class User{
        public string? Username { get; set;}
        public string? Password { get; set;}
        public virtual bool CanCreateProjects => false;
        public string UserType { get; set; } = "Standard";
    }

    public class PremiumUser : User {
        public override bool CanCreateProjects => true;
        public PremiumUser()
        {
            UserType = "Premium";
        }
    }
}