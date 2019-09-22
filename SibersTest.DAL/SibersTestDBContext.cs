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

        public SibersTestDBContext()
        {
        }

        public SibersTestDBContext(DbContextOptions<SibersTestDBContext> options)
            : base(options)
        {
        }

        

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=SibersTestDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Employees>(entity =>
            {
                entity.HasKey(e => e.EmployeeId)
                    .HasName("PK__Employee__7AD04F119A8A19CE");

                entity.Property(e => e.Email).HasColumnType("text");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Patronymic).HasMaxLength(100);

                entity.Property(e => e.SurName).HasMaxLength(100);
            });

            modelBuilder.Entity<Projects>(entity =>
            {
                entity.HasKey(e => e.ProjectId)
                    .HasName("PK__Projects__761ABEF0D8FDC794");

                entity.Property(e => e.Customer).HasMaxLength(100);

                entity.Property(e => e.FinishDate).HasColumnType("date");

                entity.Property(e => e.Performer).HasMaxLength(100);

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.Lead)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.LeadId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Projects__LeadId__619B8048");
            });

            modelBuilder.Entity<ProjectsEmployees>(entity =>
            {
                entity.ToTable("Projects_Employees");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.ProjectsEmployees)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Projects___Emplo__6477ECF3");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectsEmployees)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Projects___Proje__656C112C");
            });
        }*/
    }
}
