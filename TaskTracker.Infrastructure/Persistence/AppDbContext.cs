using Microsoft.EntityFrameworkCore;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Infrastructure.Persistence
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<TaskItem> TaskItems { get; set; }
        public DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Users>(user =>
            {
                user.HasKey(u => u.Id);
                user.Property(u => u.Name).IsRequired().HasMaxLength(100);
                user.Property(u => u.Surname).IsRequired().HasMaxLength(100);
                user.Property(u => u.Email).IsRequired().HasMaxLength(200);
            });


            modelBuilder.Entity<TaskItem>(task =>
            {
                task.HasKey(t => t.Id);
                task.Property(t => t.Title).IsRequired().HasMaxLength(200);
                task.Property(t => t.Description).HasMaxLength(1000);

                // Kullanıcı ve görev ilişkisi
                task.HasOne<Users>()
                    .WithMany()
                    .HasForeignKey(t => t.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                task.OwnsOne(t => t.Priority, priority =>
                {
                    priority.Property(p => p.Name).HasColumnName("PriorityName");
                    priority.Property(p => p.Level).HasColumnName("PriorityLevel");
                });

                task.OwnsOne(t => t.TaskState, state =>
                {
                    state.Property(s => s.Name).HasColumnName("TaskStateName");
                    state.Property(s => s.Level).HasColumnName("TaskStateLevel");
                });
            });
        }

    }
}
