using Microsoft.EntityFrameworkCore;
using TasksManager.Model;
using TasksManager.configurations;
public class InMemoryDb : DbContext
{
    public InMemoryDb(DbContextOptions<InMemoryDb> options) : base(options){}

    public DbSet<Attachment> Attachments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder){
        modelBuilder.ApplyConfiguration(new AttachmentConfiguration());
    }
}
