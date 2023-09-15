using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TasksManager.Model;

namespace TasksManager.configurations{
    public class AttachmentConfiguration : IEntityTypeConfiguration<Attachment>{
        public void Configure(EntityTypeBuilder<Attachment> builder)
        {
            builder.HasKey(a => a.Id);
        }
    }
}