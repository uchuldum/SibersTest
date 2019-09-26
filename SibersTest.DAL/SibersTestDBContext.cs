using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SibersTest.DAL.Models;
namespace SibersTest.DAL
{
    public partial class SibersTestDBContext : DbContext
    {
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<ProjectsEmployees> ProjectsEmployees { get; set; }
        //public virtual DbSet<ProjectTask> ProjectTasks { get; set; }
        public SibersTestDBContext()
        {
        }

        public SibersTestDBContext(DbContextOptions<SibersTestDBContext> options)
            : base(options)
        {
        }

        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProjectTask>(entity =>
            {
                entity.HasOne(d => d.EmployeeAuthor)
                    .WithMany(p => p.TaskAuthor)
                    .HasForeignKey(d => d.AuthorID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Task_Author");
                entity.HasOne(d => d.EmployeeExecutor)
                    .WithMany(p => p.TaskExecutor)
                    .HasForeignKey(d => d.ExecutorID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Task_Executor");
                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectTasks)
                    .HasForeignKey(d => d.ProjectID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Task_Project");
            });
            base.OnModelCreating(modelBuilder);
        }*/
    }
}
