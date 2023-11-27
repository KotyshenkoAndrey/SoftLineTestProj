using Microsoft.EntityFrameworkCore;
using SoftLineTestProj.Database.Entities;
using TaskDb = SoftLineTestProj.Database.Entities.TaskDb;

namespace SoftLineTestProj.Database
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options) { }

        public DbSet<TaskDb> TaskDb { get; set; }
        public DbSet<Status> Status { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TaskDb>().ToTable("task");
            modelBuilder.Entity<TaskDb>().Property(x => x.Name).IsRequired();
            modelBuilder.Entity<TaskDb>().Property(x => x.Name).HasMaxLength(50);
            modelBuilder.Entity<TaskDb>().HasIndex(x => x.Name).IsUnique();

            modelBuilder.Entity<Status>().ToTable("status");

            modelBuilder.Entity<TaskDb>()
            .HasOne(t => t.Status)
            .WithMany()
            .HasForeignKey(t => t.Status_ID);
        }

    }
}
