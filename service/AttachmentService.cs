    using TasksManager.Model;
    using TasksManager.interfaces;
    namespace TasksManager.service
    {
        public class AttachmentService : IAttachmentService
        {
            private IAttachmentRepository _attachmentRepository;

            public AttachmentService(IAttachmentRepository attachmentRepository){
                _attachmentRepository = attachmentRepository;
            }
            public Attachment addAttachment(int? taskId, Attachment att)
            {
                var attachment = new Attachment
                {
                    FileName = att.FileName,
                    ContentType = att.ContentType,
                    Base64EncodedData = att.Base64EncodedData,
                    TaskId = taskId
                };

                _attachmentRepository.Add(attachment);
                return attachment;
            }

        }
    }