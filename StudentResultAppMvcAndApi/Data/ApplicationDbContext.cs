using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentResultAppMvcAndApi.Models.Entities;

namespace StudentResultAppMvcAndApi.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Department> departments { get; set; }
        public DbSet<Student> students { get; set; }
        public DbSet<Result> results { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<Department>(entity =>
            {
                entity.ToTable("departments");
                entity.HasKey(d => d.DepartmentId);
                entity.Property(d => d.DepartmentName).IsRequired().HasMaxLength(100);
            });
            builder.Entity<Student>(entity =>
            {
                entity.ToTable("students");
                entity.HasKey(s  => s.StudentId);
                entity.Property(s  => s.StudentName).IsRequired().HasMaxLength(100);
                entity.Property(s => s.PhoneNumber).IsRequired().HasMaxLength(15);
                entity.Property(s => s.StudentAge).IsRequired().HasMaxLength(5);
                entity.Property(s => s.TotalMarks).HasColumnType("decimal(5,2)");
                entity.Property(s => s.AverageMark).HasColumnType("decimal(5,2)");
                entity.Property(s => s.LtrGrade).HasMaxLength(2);
                entity.Property(s => s.Gpa).HasColumnType("decimal(3,2)");
                entity.Property(s => s.DepartmentId).IsRequired();
                entity.HasOne(s => s.Department).WithMany(s => s.students).HasForeignKey(s => s.DepartmentId).OnDelete(DeleteBehavior.Restrict);
                

            });
            builder.Entity<Result>(entity =>
            {
                entity.ToTable("results");
                entity.HasKey(r => r.ResultId);
                entity.Property(r => r.ChemistryMark).IsRequired().HasColumnType("decimal(5,2)");
                entity.Property(r => r.PhysicsMark).IsRequired().HasColumnType("decimal(5,2)");
                entity.Property(r => r.MathMark).IsRequired().HasColumnType("decimal(5,2)");
                entity.Property(r => r.StudentId).IsRequired();
                entity.HasOne(r => r.Student).WithOne(r => r.result).HasForeignKey< Result > (r => r.StudentId).OnDelete(DeleteBehavior.Cascade).IsRequired(false); ;
            });
        }
    }
}
