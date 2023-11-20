using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SoftLineTestProj.Database.Entities;
using SoftLineTestProj.Settings;
using TaskDb = SoftLineTestProj.Database.Entities.TaskDb;
using SettingsDB = SoftLineTestProj.Settings.Settings;

namespace SoftLineTestProj.Database
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options) { }

        //public MyDbContext()
        //{

        //    Database.EnsureDeleted();
        //    Database.EnsureCreated();
        //}

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
//            InitDb();
        }

        private void InitDb()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
            if (Status.Any())
                return;
            var a1 = new Status()
            {
                Status_name = "Создана",
            };
            Status.Add(a1);
            var a2 = new Status()
            {
                Status_name = "В работе",
            };
            Status.Add(a2);
            var a3 = new Status()
            {
                Status_name = "Завершена",
            };
            Status.Add(a3);
            SaveChanges();
        }
    }
}
