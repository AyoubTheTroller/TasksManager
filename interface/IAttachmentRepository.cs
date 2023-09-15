using TasksManager.Model;
namespace TasksManager.interfaces{
    public interface IAttachmentRepository{
        public Attachment Add(Attachment attachment);
        public Attachment? Get(int id);
        public List<Attachment> GetAll();
    }

}