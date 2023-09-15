using TasksManager.Model;
using TasksManager.interfaces;

namespace TasksManager.controller{
    public class AttachmentController{
        private IAttachmentService _attachmentService;
        private ITaskService _taskService;
        public AttachmentController(IAttachmentService attachmentService, ITaskService taskService){
            _taskService = taskService;
            _attachmentService = attachmentService;
        }

        public Attachment? addAttachment(Attachment attachment){
            Taskk? taskk = _taskService.GetTaskById(attachment.Id);
            if(taskk is not null){
                return _attachmentService.addAttachment(attachment.Id,attachment);
            }
            return null;
        }
    }
}