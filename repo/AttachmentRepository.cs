using TasksManager.interfaces;
using TasksManager.Model;
namespace TasksManager.Repository
{
    public class AttachmentRepository : IAttachmentRepository
    {
        private readonly InMemoryDb _context;

        public AttachmentRepository(InMemoryDb context)
        {
            _context = context;
        }

        public Attachment Add(Attachment attachment)
        {
            _context.Attachments.Add(attachment);
            _context.SaveChanges();
            return attachment;
        }

        public Attachment? Get(int id)
        {
            return _context.Attachments.FirstOrDefault(a => a.Id == id);
        }

        public List<Attachment> GetAll()
        {
            return _context.Attachments.ToList();
        }

        /*public Attachment Update(Attachment attachment){
            Attachment? old_attachment = _context.Attachments.FirstOrDefault(a => a.Id == attachment.Id);
            
        }*/
    }
}
