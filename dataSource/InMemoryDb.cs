using Microsoft.EntityFrameworkCore;
using TasksManager.Model;

public class InMemoryDb : DbContext
{
    public InMemoryDb(DbContextOptions<InMemoryDb> options) : base(options){}

    public DbSet<Attachment> Attachments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attachment>()
            .HasOne<Taskk>()
            .WithMany()
            .HasForeignKey(a => a.TaskId);
    }
}
