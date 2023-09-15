using TasksManager.Model;
namespace TasksManager.interfaces{
    public interface IAttachmentService{
        Attachment addAttachment(int? taskId, Attachment att);
    }

}