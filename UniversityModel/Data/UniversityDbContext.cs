using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using UniversityModel.Domain.Entities;
namespace UniversityModel.Data
{
    public class UniversityDbContext : DbContext
    {
        public UniversityDbContext(DbContextOptions<UniversityDbContext> options):base(options)
        {
        }
       
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Professor> Professors { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseOffering> CoursesOffering { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<FieldOfStudy> FieldsOfStudy { get; set; }
        public DbSet<CourseField> CourFields { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserRole>().HasKey(x => new { x.UserId, x.RoleId });
            modelBuilder.Entity<UserRole>().HasOne(x => x.User).WithMany(x => x.UserRoles).HasForeignKey(x => x.UserId);   //User->User role
            modelBuilder.Entity<UserRole>().HasOne(x => x.Role).WithMany(x => x.UserRoles).HasForeignKey(x => x.RoleId);   //Role=>UserRole
            modelBuilder.Entity<Student>().HasOne(x => x.User).WithOne(x => x.Student).HasForeignKey<Student>(x => x.UserId); //User=>Student
            modelBuilder.Entity<Professor>().HasOne(x => x.User).WithOne(x => x.Professor).HasForeignKey<Professor>(x => x.UserId); //User =>Professor
            modelBuilder.Entity<Employee>().HasOne(x => x.User).WithOne(x => x.Employee).HasForeignKey<Employee>(x => x.UserId); //User =>Employee
            modelBuilder.Entity<Enrollment>().HasOne(x => x.Student).WithMany(x =>x.Enrollments).HasForeignKey(x => x.StudentId); //student=>Enrollment
            modelBuilder.Entity<Enrollment>().HasOne(x => x.CourseOffering).WithMany(x => x.Enrollments).HasForeignKey(x => x.OfferingId); //Courseoffering=>enrollment
            modelBuilder.Entity<CourseOffering>().HasOne(x => x.Professor).WithMany(x => x.CourseOfferings).HasForeignKey(x => x.ProfessorId); //professor=>courseffering
            modelBuilder.Entity<CourseOffering>().HasOne(x => x.Semester).WithMany(x => x.CourseOfferings).HasForeignKey(x => x.SemesterId); //semeste3r=>courseoffering
            modelBuilder.Entity<CourseOffering>().HasOne(x => x.Course).WithMany(x => x.CourseOfferings).HasForeignKey(x => x.CourseId); //course=>courseoffering
            modelBuilder.Entity<CourseField>()
         .HasKey(x => new {x.CourseId,x.FieldOfStudyId});
         modelBuilder.Entity<CourseField>()
                .HasOne(x => x.Course)
                .WithMany(x => x.CourseFields)
                .HasForeignKey(x => x.CourseId);

          modelBuilder.Entity<CourseField>()
                .HasOne(x => x.FieldOfStudy)
                .WithMany(x => x.CourseFields)
                .HasForeignKey(x => x.FieldOfStudyId);
        }
           
             

           
           















    }
    }
    

