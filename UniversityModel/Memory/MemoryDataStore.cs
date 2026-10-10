using UniversityModel.Domain.Entities;

namespace UniversityModel.Memory;

public class MemoryDataStore
{
    public List<User> Users { get; } = new();
    public List<Role> Roles { get; } = new();
    public List<UserRole> UserRoles { get; } = new();
    public List<RoleRequest> RoleRequests { get; } = new();

    public List<Course> Courses { get; } = new();
    public List<CourseOffering> CourseOfferings { get; } = new();
    public List<Enrollment> Enrollments { get; } = new();
    public List<Student> Students { get; } = new();
    public List<Professor> Professors { get; } = new();
    public List<Semester> Semesters { get; } = new();

    public MemoryDataStore()
    {
        Roles.Add(new Role
        {
            RoleId = 1,
            RoleName = "Admin"
        });

        Roles.Add(new Role
        {
            RoleId = 2,
            RoleName = "Student"
        });

        Roles.Add(new Role
        {
            RoleId = 3,
            RoleName = "Professor"
        });

        Roles.Add(new Role
        {
            RoleId = 4,
            RoleName = "SuperAdmin"
        });

        UserRoles.Add(new UserRole
        {
            UserId = 1,
            RoleId = 1
        });

        UserRoles.Add(new UserRole
        {
            UserId = 1,
            RoleId = 4
        });

        // Add courses
        Courses.AddRange(new List<Course>
        {
            new Course
            {
                CourseId = 1,
                CourseName = "طراحی الگوریتم"
            },
            new Course
            {
                CourseId = 2,
                CourseName = "طراحی کامپایلر"
            },
            new Course
            {
                CourseId = 3,
                CourseName = "طراحی پایگاه داده‌ها"
            },
            new Course
            {
                CourseId = 4,
                CourseName = "سیستم‌های عامل"
            }
        });
        Semesters.Add(new Semester
        {
            SemesterId = 4051,
            SemesterName = "ترم اول 1405"
        });
        Professors.Add(new Professor
        {
            ProfessorId = 1,
            UserId = 2,
            FirstName = "reza",
            LastName = "basiri"
        });
        Students.Add(new Student
        {
            StudentId = 1,
            UserId = 3,
            Age = 20,
            PersonalId = 1001,
            FirstName = "sajad",
            LastName = "basiri"
        });
    }
}