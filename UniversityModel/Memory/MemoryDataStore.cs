
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
    public List<FieldOfStudy> FieldsOfStudy { get; } = new();
    public List<CourseField> CourseFields { get; } = new();

    public MemoryDataStore()
    {
        // Roles
        Roles.AddRange(new List<Role>
        {
            new Role { RoleId = 1, RoleName = "Admin" },
            new Role { RoleId = 2, RoleName = "Student" },
            new Role { RoleId = 3, RoleName = "Professor" },
            new Role { RoleId = 4, RoleName = "SuperAdmin" }
        });

        // User roles
        UserRoles.AddRange(new List<UserRole>
        {
            new UserRole { UserId = 1, RoleId = 1 },
            new UserRole { UserId = 1, RoleId = 4 }
        });

        // Courses
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

        // Semester
        Semesters.Add(new Semester
        {
            SemesterId = 4051,
            SemesterName = "ترم اول 1405"
        });

        // Professor
        Professors.Add(new Professor
        {
            ProfessorId = 1,
            UserId = 2,
            FirstName = "reza",
            LastName = "basiri"
        });

        // Fields of study
        FieldsOfStudy.AddRange(new List<FieldOfStudy>
        {
            new FieldOfStudy
            {
                FieldOfStudyId = 1,
                FieldName = "مهندسی کامپیوتر",
                DegreeName = "کارشناسی"
            },
            new FieldOfStudy
            {
                FieldOfStudyId = 2,
                FieldName = "مهندسی برق",
                DegreeName = "کارشناسی"
            },
            new FieldOfStudy
            {
                FieldOfStudyId = 3,
                FieldName = "مهندسی عمران",
                DegreeName = "کارشناسی"
            }
        });

        // Student
        Students.Add(new Student
        {
            StudentId = 1,
            UserId = 3,
            Age = 20,
            PersonalId = 1001,
            FirstName = "mehdi",
            LastName = "basiri",
            FieldOfStudyId = 1
        });


        CourseFields.AddRange(new List<CourseField>
        {
            new CourseField
            {
                CourseId = 1,
                FieldOfStudyId = 1,
                IsRequired = true
            },
            new CourseField
            {
                CourseId = 2,
                FieldOfStudyId = 1,
                IsRequired = true
            },
            new CourseField
            {
                CourseId = 3,
                FieldOfStudyId = 1,
                IsRequired = true
            },
            new CourseField
            {
                CourseId = 4,
                FieldOfStudyId = 1,
                IsRequired = true
            }
        });
    }
}
