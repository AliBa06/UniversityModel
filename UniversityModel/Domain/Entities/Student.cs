namespace UniversityModel.Domain.Entities
{
    public class Student
    {
        public Student() { }
        public int StudentId { get; set; }
        public int Age { get; set; }
        public int PersonalId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}