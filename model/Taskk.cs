namespace TasksManager.Model{
    public class Taskk{
        public int Id { get; set;}
        public string? Description { get; set;}
        public DateTime CreationDate { get; set;}
        public DateTime ExpirationDate { get; set;}

        public override bool Equals(object? obj){
            if (obj is Taskk otherTask)
            {
                return CreationDate == otherTask.CreationDate &&
                    Description == otherTask.Description &&
                    ExpirationDate == otherTask.ExpirationDate &&
                    Id == otherTask.Id;
            }
            return false;
        }
        public override int GetHashCode(){
            return HashCode.Combine(CreationDate, Description, ExpirationDate, Id);
        }
    }
}